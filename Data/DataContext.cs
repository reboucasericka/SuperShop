﻿using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System;
using System.Threading.Tasks;

namespace SuperShop.Data
{
	public class DataContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		internal async Task SaveAllAsync()
		{
			throw new NotImplementedException();
		}
	}
}
