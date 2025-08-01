using Microsoft.AspNetCore.Http;
using SuperShop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
	public class ProductViewModel : Product
	{
		//todos os campoos do model mais os campos adicionais que queremos mostrar na view
		[Display(Name = "Image")] // Display attribute to set the label for the image field
		public IFormFile ImageFile { get; set; } //para receber o ficheiro da imagem

	}
}
