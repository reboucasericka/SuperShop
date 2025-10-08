using Microsoft.AspNetCore.Identity.EntityFrameworkCore; //responsavel pela autenticacao
using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
	public class DataContext : IdentityDbContext<User>
	{
		public DbSet<Product> Products { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderDetail> OrderDetails { get; set; }	


		public DbSet<OrderDetailTemp> OrderDetailsTemp { get; set; }	


        public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}


		//APAGA TUDO EM CASCATA 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

			var cascadeFks = modelBuilder.Model
				.GetEntityTypes()
				.SelectMany(t => t.GetDeclaredForeignKeys())
				.Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
			foreach(var fk in cascadeFks)
			{
				fk.DeleteBehavior = DeleteBehavior.Restrict;
			}
			base.OnModelCreating(modelBuilder);
        }
	}
}
