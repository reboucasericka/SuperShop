using SuperShop.Data.Entities;
using SuperShop.Models;

namespace SuperShop.Helpers
{
	public interface IConverterHelper
	{
		//converet um view modo em produtos
		Product ToProduct(ProductViewModel model, string path, bool isNew); 
		ProductViewModel ToProductViewModel(Product product);
	}
}
