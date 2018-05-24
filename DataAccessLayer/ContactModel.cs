namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;

    public partial class ContactDBContext : DbContext
    {
        
        Type providerService = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        public ContactDBContext()
            : base("name=ContactDBContext")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
