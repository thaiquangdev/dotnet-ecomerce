using asp_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_mvc.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories {get; set;}
        public DbSet<SubCategory> SubCategories {get; set;}
        public DbSet<Product> Products {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<Role> Roles {get; set;}
        public DbSet<UserRole> UserRoles {get; set;}
        public DbSet<Publisher> Publishers {get; set;}
        public DbSet<Review> Reviews {get; set;}
        public DbSet<Cart> Carts {get; set;}
        public DbSet<CartItem> CartItems {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubCategory>()
                        .HasOne(s => s.Category)
                        .WithMany(c => c.SubCategories)
                        .HasForeignKey(s => s.CategoryId);

            modelBuilder.Entity<Product>()
                        .HasOne(s => s.SubCategory)
                        .WithMany(c => c.Products)
                        .HasForeignKey(s => s.SubCategoryId);

            modelBuilder.Entity<Product>()
                        .HasOne(s => s.Publisher)
                        .WithMany(c => c.Products)
                        .HasForeignKey(s => s.PublisherId);

            modelBuilder.Entity<UserRole>()
                        .HasKey(u => new {u.UserId, u.RoleId});
            
            modelBuilder.Entity<UserRole>()
                        .HasOne(s => s.User)
                        .WithMany(c => c.UserRoles)
                        .HasForeignKey(s => s.UserId);
            
            modelBuilder.Entity<UserRole>()
                        .HasOne(s => s.Role)
                        .WithMany(c => c.UserRoles)
                        .HasForeignKey(s => s.RoleId);
            
            modelBuilder.Entity<Review>()
                        .HasOne(s => s.User)
                        .WithMany(c => c.Reviews)
                        .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Review>()
                        .HasOne(s => s.Product)
                        .WithMany(c => c.Reviews)
                        .HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<Cart>()
                        .HasOne(s => s.User)
                        .WithMany(c => c.Carts)
                        .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<CartItem>()
                        .HasOne(s => s.Cart)
                        .WithMany(c => c.CartItems)
                        .HasForeignKey(s => s.CartId);

            modelBuilder.Entity<CartItem>()
                        .HasOne(s => s.Product)
                        .WithMany(c => c.CartItems)
                        .HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<ShippingDetail>()
                        .HasOne(s => s.User)
                        .WithMany(c => c.ShippingDetails)
                        .HasForeignKey(s => s.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ShippingDetail>()
                        .HasOne(s => s.Cart)
                        .WithMany(c => c.ShippingDetails)
                        .HasForeignKey(s => s.CartId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
        .HasOne(p => p.Cart)
        .WithOne(c => c.Payment)
        .HasForeignKey<Payment>(p => p.CartId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
        .HasOne(p => p.User)
        .WithMany(u => u.Payments)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Restrict);

                                 

                        
        }
    }
}