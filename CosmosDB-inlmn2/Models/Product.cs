using System.ComponentModel.DataAnnotations;

namespace CosmosDB_inlmn2.Models
{
    public enum Categories
    {
        Datorer,
        Mobiler,
        TV,
        Tillbehör
    }

    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string ArticleNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Categories Category { get; set; }
        public List<Specification> Specifications { get; set; }
    }
}
