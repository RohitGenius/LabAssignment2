using System.ComponentModel.DataAnnotations;

namespace LabAssignment2.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

       
    }
}
