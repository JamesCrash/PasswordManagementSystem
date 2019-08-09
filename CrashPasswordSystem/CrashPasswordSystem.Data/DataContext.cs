namespace CrashPasswordSystem.Data
{
    using CrashPasswordSystem.Models;
    using Microsoft.EntityFrameworkCore;

    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        { }

        public virtual DbSet<CrashCompany> CrashCompanies { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<UpdateHistory> UpdateHistories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        /**********
         * MIGRATIONS COMMANDS
         *********/

        /********
         * Steps
         * 
         * 1) Uncomment the following method. Make sure it targets the database in your machine
         * 
         *********/
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=ITDatabase;persist security info=True;user id=sa;password=12345abC;MultipleActiveResultSets=True;App=EntityFramework");
        }*/

        /********
         * 2) Comment the constructor-parameter of this class in order to be a parameterless constructor 
         * by default. Also check check-out for other depending on the constructor
         * 
         
         3) Run from the following command from "Package Manager Console" against this project

            EntityFrameworkCore\Add-Migration InitalMigration

            **** Requiered packages to be installed in this project include before running the commands (already installed):
        
            dotnet tool install --global dotnet-ef

            Install-Package Microsoft.EntityFrameworkCore.SqlServer
            Install-Package Microsoft.EntityFrameworkCore.Tools
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrashCompany>(entity =>
            {
                entity.HasKey(e => e.CCID);

                entity.Property(e => e.CCID);

                entity.Property(e => e.CCName)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.PCID);

                entity.Property(e => e.PCID);

                entity.Property(e => e.PCName)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductID);

                entity.Property(e => e.ProductID);

                entity.Property(e => e.CCID);

                entity.Property(e => e.PCID);

                entity.Property(e => e.ProductDateAdded);
                    //.HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductExpiry);

                entity.Property(e => e.ProductPassword)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductURL)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProductUsername)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.StaffID);

                entity.Property(e => e.SupplierID);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CCID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PCID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StaffID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierID);

                entity.Property(e => e.SupplierID);

                entity.Property(e => e.SupplierAddress).HasMaxLength(200);

                entity.Property(e => e.SupplierContactNumber).HasMaxLength(30);

                entity.Property(e => e.SupplierDateAdded);
                //    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SupplierEmail).HasMaxLength(100);

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SupplierWebsite).HasMaxLength(100);
            });

            modelBuilder.Entity<UpdateHistory>(entity =>
            {
                entity.HasKey(e => e.UHID);

                entity.Property(e => e.UHID);

                entity.Property(e => e.DateUpdated);

                entity.Property(e => e.ProductID);

                entity.Property(e => e.StaffID);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.UpdateHistories)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID);

                entity.Property(e => e.UserID);

                entity.Property(e => e.UserActive);
                //    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserDateCreated);
                //    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserEmail).HasMaxLength(100);

                entity.Property(e => e.UserFirstName).HasMaxLength(50);

                entity.Property(e => e.UserHash).HasMaxLength(100);

                entity.Property(e => e.UserInitials);
                //    .HasColumnType("nchar(10)");

                entity.Property(e => e.UserLastName).HasMaxLength(50);

                entity.Property(e => e.UserSalt).HasMaxLength(20);
            });
        }
    }
}
