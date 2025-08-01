using System;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Data.Entities
{
	public class Product : IEntity // Implementing IEntity interface to ensure that Product has an Id property
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50, ErrorMessage ="The field {0} can contain {1} characters length.")] // Assuming a maximum length of 50 characters for the product name

		public string Name { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}",  ApplyFormatInEditMode = false)]
		public decimal Price { get; set; }

		[Display(Name = "Image")]
		public string ImageUrl { get; set; }

		[Display(Name = "Last Purchase ")] //datas obrigatorias
		public  DateTime? LastPurchase { get; set; } //?opcionais

		[Display(Name = "Last Sale ")]
		public DateTime? LastSale { get; set; } //optionais


		[Display(Name = "Is Available")]
		public bool IsAvailable { get; set; }


		[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
		public double Stock { get; set; }

		public User User { get; set; } // Assuming a User entity exists and is related to Product


	}
}
