using System.ComponentModel.DataAnnotations;

namespace CosmosDB_inlmn2.Models
{
    public class ProductsResponse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string ArticleNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public List<Specification> Specifications { get; set; }
    }
}
