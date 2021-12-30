using FinanceTrackerAPI.Models;
using FinanceTrackerAPI.Models.DAOs;
using FinancialTracker.Core.Lib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Common;
using System.Data;

namespace FinanceTrackerAPI.Data
{
    public class InvestmentContext : BaseContext, IInvestmentRepository
    {
        private string className;
        public InvestmentContext(DbContextOptions<BaseContext> options) : base(options)
        {
            this.className = this.GetType().Name;
        }

        public async Task Commit()
        {
            try
            {
                await this.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(className + "/Commit(): Error occured while commiting to database + Exception: " + e.ToString());
            }
        }

        public async Task<Investment> AddInvestment(Investment investment)
        {
            try
            {
                if (investment == null)
                {
                    throw new ArgumentNullException(className + "/AddInvestment(): The investment object parameter is null");
                }
                else
                {
                    await this.Investments.AddAsync(investment);
                    await Commit();
                    return investment;
                }
            }
            catch (Exception e)
            {
                throw new Exception(className + "/AddInvestment(): " + e.Message);
            }
        }

        public async Task<bool> DeleteInvestment(int investmentId)
        {
            try
            {
                Investment deleteInvestment = await this.Investments.FirstOrDefaultAsync(x => x.InvestmentId == investmentId);
                this.Investments.Remove(deleteInvestment);
                await Commit();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(className + "/DeleteInvestment(): " + e.Message);
            }
        }

        public async Task<Investment> GetInvestmentById(int investmentId)
        {
            try
            {
                Investment investment = await this.Investments.Include(x => x.InvestmentStatus).Include(x => x.InvestmentType).Include(x => x.Member).FirstOrDefaultAsync(x => x.InvestmentId == investmentId);
                this.Entry<Investment>(investment).State = EntityState.Detached;
                return investment;
            }
            catch (Exception e)
            {
                throw new Exception(className + "/GetInvestmentById(): " + e.Message);
            }
        }

        public async Task<IEnumerable<Investment>> GetInvestments()
        {
            try
            {
                IEnumerable<Investment> investments;

                var query = from investment in this.Investments
                            where investment.IsActive == true
                            select investment;

                investments = query.Include(x => x.InvestmentStatus).Include(x => x.InvestmentType).Include(x => x.Member).AsEnumerable<Investment>();

                if (investments != null)
                {
                    return investments;
                }
                else
                {
                    throw new NullReferenceException(className + $"/GetInvestments(): No investments found in the database");
                }
            }
            catch(Exception e)
            {
                throw new Exception(className + "/GetInvestments(): " + e.Message);
            }
        }

        public async Task<Investment> UpdateInvestment(Investment investment)
        {
            try
            {
                if (investment != null)
                {
                    this.Update(investment);
                    await Commit();
                    return investment;
                }
                else
                {
                    throw new ArgumentNullException(className + "/UpdateInvestment(): The investment object parameter received is null");
                }
            }
            catch (Exception e)
            {
                throw new Exception(className + "/UpdateInvestment(): " + e.Message);
            }
        }

        public async Task<PagedInvestmentResponse> GetPagedInvestments(int pageNumber, int pageSize, string searchText, IList<int> filter)
        {
            try
            {
                IEnumerable<Investment> investments;

                var query = from investment in this.Investments
                            where investment.IsActive == true && investment.InvestmentName.Contains(searchText) && filter.Contains(investment.InvestmentTypeId)
                            orderby investment.CreatedDate descending
                            select investment;

                investments = query
                    .Include(x => x.InvestmentStatus).Include(x => x.InvestmentType).Include(x => x.Member)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsEnumerable<Investment>();

                var totalInvestments = query.CountAsync().Result;

                if (investments != null)
                {
                    return new PagedInvestmentResponse(totalInvestments, investments);
                }
                else
                {
                    throw new NullReferenceException(className + $"/GetPagedInvestments(): No investment found in the database");
                }
            }
            catch (Exception e)
            {
                throw new Exception(className + "/GetPagedInvestments(): " + e.Message);
            }
        }

        public async Task<IEnumerable<ActiveInvestment>> GetActiveInvestments()
        {
            try
            {
                IList<ActiveInvestment> activeInvestments = new List<ActiveInvestment>();
                string sql = $"EXEC GetActiveInvestments";

                DbCommand cmd;
                DbDataReader rdr;
                cmd = this.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sql;
                this.Database.OpenConnection();
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    var activeInv = new ActiveInvestment
                    {
                        InvestmentName = rdr.GetString(0),
                        InvestmentAmount = rdr.GetDecimal(2),
                        InvestmentTypeName = rdr.GetString(3)
                    };

                    if (!rdr.IsDBNull(1))
                    {
                        activeInv.InvestmentDate = rdr.GetDateTime(1);
                    }

                    activeInvestments.Add(activeInv);
                }

                rdr.Close();

                return activeInvestments;
            }
            catch (Exception e)
            {
                throw new Exception(className + "/GetPagedInvestments(): " + e.Message);
            }
        }
    }
}
