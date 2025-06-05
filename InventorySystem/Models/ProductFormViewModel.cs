using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.ViewModels
{
    public class ProductFormViewModel
    {
        public  int ProductID { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int SupplierID { get; set; }

        public List<SelectListItem> Suppliers { get; set; } = new();
    }
}
