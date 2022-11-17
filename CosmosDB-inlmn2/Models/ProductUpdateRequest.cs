namespace CosmosDB_inlmn2.Models
{
    public class ProductUpdateRequest
    {
        public string Name { get; set; }
        public string ArticleNumber { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
