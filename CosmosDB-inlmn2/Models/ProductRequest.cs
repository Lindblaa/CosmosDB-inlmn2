namespace CosmosDB_inlmn2.Models
{
    public class ProductRequest
    {
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Categories Category { get; set; }
        public List<Specification> Specifications { get; set; }
    }
}
