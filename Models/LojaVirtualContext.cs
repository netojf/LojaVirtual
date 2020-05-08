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
			modelBuilder.Entity<Category>()
				.HasMany(c => c.Products)
				.WithOne(e => e.Category);
			modelBuilder.Entity<User>()
				.HasOne(u => u.ShoppingCart)
				.WithOne(s => s.User)
				.HasForeignKey<ShoppingCart>(s => s.UserFk)
				.IsRequired();
			modelBuilder
				.Entity<ShoppingCart>()
				.HasMany(s => s.Products)
				.WithOne(p => p.ShoppingCart);
		} 
		#endregion
	}
}
