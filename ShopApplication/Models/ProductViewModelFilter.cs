using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace ShopApplication.Models
{
    public class ProductViewModelFilter
    {
        public int? CategoryId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Aroma { get; set; }

        public SelectList Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }

    }
}
