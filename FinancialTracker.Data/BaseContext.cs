using FinanceTrackerAPI.Models.DAOs;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FinanceTrackerAPI.Data
{
    public partial class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Investment> Investments { get; set; }
        public virtual DbSet<InvestmentStatus> InvestmentStatuses { get; set; }
        public virtual DbSet<InvestmentType> InvestmentTypes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<VwArchivedInvestment> VwArchivedInvestments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.ToTable("Investment");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InvestmentAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.InvestmentDate).HasColumnType("datetime");

                entity.Property(e => e.InvestmentName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.InvestmentNotes)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.InvestmentStatus)
                    .WithMany(p => p.Investments)
                    .HasForeignKey(d => d.InvestmentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Investment_InvestmentStatus");

                entity.HasOne(d => d.InvestmentType)
                    .WithMany(p => p.Investments)
                    .HasForeignKey(d => d.InvestmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Investment_InvestmentType");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Investments)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Investment_Member");
            });

            modelBuilder.Entity<InvestmentStatus>(entity =>
            {
                entity.ToTable("InvestmentStatus");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InvestmentStatusName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<InvestmentType>(entity =>
            {
                entity.ToTable("InvestmentType");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InvestmentTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwArchivedInvestment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ArchivedInvestments");

                entity.Property(e => e.InvestmentAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.InvestmentDate).HasColumnType("datetime");

                entity.Property(e => e.InvestmentName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
