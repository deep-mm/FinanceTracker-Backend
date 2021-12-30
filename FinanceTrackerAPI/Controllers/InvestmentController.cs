using FinanceTrackerAPI.Models.DTOs;
using FinancialTracker.Common;
using FinancialTracker.Core.Lib;
using FinancialTracker.Core.Lib.CoreServices;
using FinancialTracker.Models.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackerAPI.Controllers
{
    [Route("api/investment")]
    [ApiController]
    [Authorize]
    //[Filters.Authorization()]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService investmentService;
        private readonly ILogger<InvestmentController> _logger;
        private Helper helper;

        public InvestmentController(ILogger<InvestmentController> logger, IInvestmentService investmentService, IOptions<AppInsightsOptions> options, ISessionService sessionService)
        {
            _logger = logger;
            this.investmentService = investmentService;
            this.helper = new Helper(options, sessionService);
        }

        [HttpGet()]
        public async Task<IActionResult> GetInvestments()
        {
            try
            {
                this._logger.LogTrace("User " + this.helper.getLoggedInUsername() + " has accessed the API: GET api/investment");
                IEnumerable<Investment> investments = await investmentService.GetInvestments();
                return Ok(investments);
            }
            catch (Exception e)
            {
                this._logger.LogError(new Exception(), "User " + this.helper.getLoggedInUsername() + " has accessed the API: GET api/investment with " + "Exception: An error occurred with message: " + e.Message);
                return BadRequest("Error occured while getting investments");
            }
        }

        [HttpGet("{investmentId}")]
        public async Task<IActionResult> GetInvestmentById(int investmentId)
        {
            try
            {
                this._logger.LogTrace("User " + this.helper.getLoggedInUsername() + " has accessed the API: GET api/investment/{investmentId}");
                Investment investment = await investmentService.GetInvestmentById(investmentId);
                return Ok(investment);
            }
            catch (Exception e)
            {
                this._logger.LogError(new Exception(), "User " + this.helper.getLoggedInUsername() + " has accessed the API: GET api/investment/{investmentId} with " + "Exception: An error occurred with message: " + e.Message);
                return BadRequest("Error occured while getting investment by id");
            }
        }

        [HttpGet("pagedInvestments")]
        public async Task<IActionResult> GetPagedInvestments([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string searchText, [FromQuery] string filter)
        {
            try
            {
                this._logger.LogTrace("User " + this.helper.getLoggedInUsername() + " has accessed the API: GET api/investment/pagedInvestments");
                PagedInvestmentResponse investments = await investmentService.GetPagedInvestments(pageNumber, pageSize, searchText, filter.Split(",").Select(int.Parse).ToList());
                return Ok(investments);
            }
            catch (Exception e)
            {
                this._logger.LogError(new Exception(), "User " + this.helper.getLoggedInUsername() + " has accessed the API: GET api/investment/pagedInvestments?pageNumber=?pageSize=?searchText=?filter= with " + "Exception: An error occurred with message: " + e.Message);
                return BadRequest("Error occured while getting investments");
            }
        }

        [HttpGet("activeInvestments")]
        public async Task<IActionResult> GetActiveInvestments()
        {
            try
            {
                this._logger.LogTrace("User " + this.helper.getLoggedInUsername() + " has accessed the API: GET api/investment/activeInvestments");
                IEnumerable<FinanceTrackerAPI.Models.DAOs.ActiveInvestment> investments = await investmentService.GetActiveInvestments();
                return Ok(investments);
            }
            catch (Exception e)
            {
                this._logger.LogError(new Exception(), "User " + this.helper.getLoggedInUsername() + " has accessed the API: GET api/investment/activeInvestments with " + "Exception: An error occurred with message: " + e.Message);
                return BadRequest("Error occured while getting investments");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddInvestment([FromBody] Investment investment)
        {
            try
            {
                this._logger.LogTrace("User " + this.helper.getLoggedInUsername() + " has accessed the API: POST api/investment");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Investment addedInvestment = await investmentService.AddInvestment(investment);

                if (addedInvestment != null)
                {
                    return Ok(addedInvestment);
                }
                else
                {
                    return BadRequest("Error occured while adding a new investment");
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(new Exception(), "User " + this.helper.getLoggedInUsername() + " has accessed the API: POST api/investment with " + "Exception: An error occurred with message: " + e.Message);
                return BadRequest("Error occured while adding a new investment");
            }
        }

        [HttpDelete("{investmentId}")]
        public async Task<IActionResult> DeleteInvestment(int investmentId)
        {
            try
            {
                this._logger.LogTrace("User " + this.helper.getLoggedInUsername() + " has accessed the API: DELETE api/investment/{investmentId}");
                bool result = await investmentService.DeleteInvestment(investmentId);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Error occured while deleting investment");
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(new Exception(), "User " + this.helper.getLoggedInUsername() + " has accessed the API: DELETE api/investment/{investmentId} with " + "Exception: An error occurred with message: " + e.Message);
                return BadRequest("Error occured while deleting investment");
            }
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateInvestment([FromBody] Investment investment)
        {
            try
            {
                this._logger.LogTrace("User " + this.helper.getLoggedInUsername() + " has accessed the API: PUT api/investment");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Investment updatedInvestment = await investmentService.UpdateInvestmentPut(investment);
                if (updatedInvestment != null)
                    return Ok(updatedInvestment);
                else
                    return BadRequest("Error occured while updating investment");
            }
            catch (Exception e)
            {
                this._logger.LogError(new Exception(), "User " + this.helper.getLoggedInUsername() + " has accessed the API: PUT api/investment with " + "Exception: An error occurred with message: " + e.Message);
                return BadRequest("Error occured while updating investment");
            }
        }

        [HttpPatch("{investmentId}")]
        public async Task<IActionResult> Update(int investmentId, [FromBody] JsonPatchDocument<FinanceTrackerAPI.Models.DAOs.Investment> investment)
        {
            try
            {
                this._logger.LogTrace("User " + this.helper.getLoggedInUsername() + " has accessed the API: PATCH api/investment/{investmentId}");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var entity = await investmentService.GetInvestmentDAOById(investmentId);
                if (entity == null)
                {
                    return NotFound();
                }
                else
                {
                    investment.ApplyTo(entity);
                }
                var output = await investmentService.UpdateInvestment(entity);

                return Ok(output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred: " + e);
                this._logger.LogError(new Exception(), "User " + this.helper.getLoggedInUsername() + " has accessed the API: PATCH api/investment/{investmentId} with " + "Exception: An error occurred with message: " + e.Message);
                return BadRequest("Error occured while updating investment");
            }
        }

    }
}
