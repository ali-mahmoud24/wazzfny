using Microsoft.EntityFrameworkCore;
using ApplicantAPI.Models;

namespace ApplicantAPI.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ComputerSkill> ComputerSkills { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobCategory> JobCategories { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<RequestApplicant> RequestApplicants { get; set; }

    public virtual DbSet<RequestApplicantLanguage> RequestApplicantLanguages { get; set; }

    public virtual DbSet<RequestApplicantSkill> RequestApplicantSkills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<ComputerSkill>(entity =>
        {
            entity.ToTable("ComputerSkill");

            entity.Property(e => e.ComputerSkillId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ComputerSkillID");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.SkillName).HasMaxLength(150);
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("Experience");

            entity.Property(e => e.ExperienceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ExperienceID");
            entity.Property(e => e.ExperienceName).HasMaxLength(150);
            entity.Property(e => e.Notes).HasMaxLength(250);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.ToTable("Job");

            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.JobCategoryId).HasColumnName("JobCategoryID");
            entity.Property(e => e.JobName).HasMaxLength(250);
            entity.Property(e => e.Notes).HasMaxLength(250);

            entity.HasOne(d => d.JobCategory).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Jobs_Jobs");
        });

        modelBuilder.Entity<JobCategory>(entity =>
        {
            entity.ToTable("JobCategory");

            entity.Property(e => e.JobCategoryId).HasColumnName("JobCategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(250);
            entity.Property(e => e.Notes).HasMaxLength(250);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Language");

            entity.Property(e => e.LanguageId)
                .ValueGeneratedOnAdd()
                .HasColumnName("LanguageID");
            entity.Property(e => e.LanguageName).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(250);
        });

        modelBuilder.Entity<RequestApplicant>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.ToTable("RequestApplicant");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Details).HasMaxLength(550);
            entity.Property(e => e.EndPublish).HasColumnType("smalldatetime");
            entity.Property(e => e.ExperienceId).HasColumnName("ExperienceID");
            entity.Property(e => e.IsEnd).HasColumnName("Is_end");
            entity.Property(e => e.IsNegotiate).HasColumnName("Is_negotiate");
            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.PostDate)
                .HasColumnType("smalldatetime")
                .HasColumnName("postDate");
            entity.Property(e => e.StartPublish).HasColumnType("smalldatetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Experience).WithMany(p => p.RequestApplicants)
                .HasForeignKey(d => d.ExperienceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestApplicant_Experience");

            entity.HasOne(d => d.Job).WithMany(p => p.RequestApplicants)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestApplicant_Job");

            entity.HasOne(d => d.User).WithMany(p => p.RequestApplicants)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RequestApplicant_User");
        });

        modelBuilder.Entity<RequestApplicantLanguage>(entity =>
        {
            entity.HasKey(e => e.ReqLangId);

            entity.ToTable("RequestApplicantLanguage");

            entity.Property(e => e.ReqLangId).HasColumnName("ReqLangID");
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");

            entity.HasOne(d => d.Language).WithMany(p => p.RequestApplicantLanguages)
                .HasForeignKey(d => d.LanguageId)
                .HasConstraintName("FK_RequestApplicantLanguage_Language");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestApplicantLanguages)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK_RequestApplicantLanguage_RequestApplicant");
        });

        modelBuilder.Entity<RequestApplicantSkill>(entity =>
        {
            entity.HasKey(e => e.ReqSkillComId);

            entity.ToTable("RequestApplicantSkill");

            entity.Property(e => e.ReqSkillComId).HasColumnName("ReqSkillComID");
            entity.Property(e => e.ComputerSkillId).HasColumnName("ComputerSkillID");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");

            entity.HasOne(d => d.ComputerSkill).WithMany(p => p.RequestApplicantSkills)
                .HasForeignKey(d => d.ComputerSkillId)
                .HasConstraintName("FK_RequestApplicantSkill_ComputerSkill");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestApplicantSkills)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK_RequestApplicantSkill_RequestApplicant");
        });

        modelBuilder.Entity<User>(entity =>
        {

            entity.HasKey(e => e.UserId);

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "IX_User").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
