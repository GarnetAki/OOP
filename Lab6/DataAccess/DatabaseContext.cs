using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Account> Accounts { get; set; } = null!;

    public DbSet<Phone> Phones { get; set; } = null!;

    public DbSet<Email> Emails { get; set; } = null!;

    public DbSet<MessageSource> MessageSources { get; set; } = null!;

    public DbSet<EmailMessage> EmailMessages { get; set; } = null!;

    public DbSet<PhoneMessage> PhoneMessages { get; set; } = null!;

    public DbSet<Message> Messages { get; set; } = null!;

    public DbSet<ReportUserPart> ReportUserParts { get; set; } = null!;

    public DbSet<ReportSourcePart> ReportSourceParts { get; set; } = null!;

    public DbSet<Report> Reports { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageSource>()
            .HasDiscriminator<string>("ms_type")
            .HasValue<MessageSource>("ms_base")
            .HasValue<Phone>("phone")
            .HasValue<Email>("email");

        modelBuilder.Entity<Message>()
            .HasDiscriminator<string>("m_type")
            .HasValue<Message>("m_base")
            .HasValue<PhoneMessage>("phone_m")
            .HasValue<EmailMessage>("email_m");
    }
}