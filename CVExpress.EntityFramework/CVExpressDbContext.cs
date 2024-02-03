using CVExpress.Entities.Efos;
using CVExpress.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CVExpress.EntityFramework
{
    public class CVExpressDbContext : DbContext
    {
        public CVExpressDbContext(DbContextOptions<CVExpressDbContext> options) : base(options) { }

        public DbSet<RegisterUsersEfo> RegisterUsers {  get; set; }
        public DbSet<UsersEfo> Users { get; set; }
        public DbSet<HabilitationsEfo> Habilitations { get; set; }
        public DbSet<ExperienceEfo> Experiences { get; set; }
        public DbSet<ContactsEfo> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegisterUsersEfc());
            modelBuilder.ApplyConfiguration(new UsersEfc());
            modelBuilder.ApplyConfiguration(new HabilitationsEfc());
            modelBuilder.ApplyConfiguration(new ExperienceEfc());
            modelBuilder.ApplyConfiguration(new ContactsEfc());
        }
    }
}
