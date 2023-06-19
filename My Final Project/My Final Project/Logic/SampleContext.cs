using BitLink.Dao;

namespace BitLink.Logic;

public sealed class SampleContext : DbContext
{
    public DbSet<Admin> Admin { get; set; }
    public DbSet<Person> Persons { get; set; }

    public SampleContext (DbContextOptions<SampleContext> options) 
        : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
    optionsBuilder.UseSqlServer(
            "Data Source=MUKOLAOFMYKACHE;" +
            "Initial Catalog=BitLink;" +
            "Integrated Security=True;" +
            "Encrypt = True;" +
            "TrustServerCertificate=True;");
}