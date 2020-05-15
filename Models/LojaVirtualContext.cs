using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
	public class LojaVirtualContext : DbContext
	{
		#region Properties

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<ProductOrder> productOrders { get; set; }
		public DbSet<Adress> Adresses { get; set; }
		public DbSet<Image> Images { get; set; }


		#endregion

		#region Constructor
		public LojaVirtualContext([NotNullAttribute] DbContextOptions options) : base(options)
		{

		}
		public LojaVirtualContext() { }
		#endregion


		#region Methods

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(Startup.connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			#region UserConfig
			modelBuilder.Entity<User>()
					.HasOne(u => u.ShoppingCart)
					.WithOne(s => s.User)
					.HasForeignKey<ShoppingCart>(s => s.UserFk)
					.IsRequired();

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Name)
				.IsUnique();

			modelBuilder.Entity<User>()
				.Property(u => u.Name)
				.IsRequired();

			modelBuilder.Entity<User>()
				.Property(u => u.Password)
				.IsRequired();

			modelBuilder.Entity<User>()
				.Property(u => u.IsAdmin)
				.HasDefaultValue(false);
			modelBuilder.Entity<User>()
				.HasMany(p => p.Adresses)
				.WithOne(a => a.User); 
			#endregion

			#region CategoryConfig
			modelBuilder.Entity<Category>()
					.HasMany(c => c.Products)
					.WithOne(e => e.Category);
			#endregion

			#region ProductConfig
			modelBuilder.Entity<Product>()
					.HasIndex(u => u.Barcode)
					.IsUnique();

			modelBuilder
				.Entity<Product>()
				.HasMany(o => o.Products)
				.WithOne(p => p.ProductPack);

			modelBuilder
				.Entity<Product>()
				.Property(p => p.Name)
				.IsRequired();
			modelBuilder
				.Entity<Product>()
				.Property(p => p.ProductImage)
				.IsRequired();

			#endregion

			#region ShoppingCartConfig
			modelBuilder
					.Entity<ShoppingCart>()
					.HasMany(s => s.ProductOrders)
					.WithOne(p => p.ShoppingCart); 
			#endregion

			#region ProductOrderConfig
			modelBuilder
					.Entity<ProductOrder>()
					.Property(po => po.Quantity)
					.HasDefaultValue(1);

			modelBuilder
				.Entity<ProductOrder>()
				.HasMany(o => o.Products)
				.WithOne(p => p.ProductOrder); 
			#endregion

		}
		#endregion
	}
}
