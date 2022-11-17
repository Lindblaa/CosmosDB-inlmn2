using CosmosDB_inlmn2.Contexts;
using CosmosDB_inlmn2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CosmosDB_inlmn2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var _products = new List<ProductsResponse>();
                foreach (var item in await _context.Products.ToListAsync())
                    _products.Add(new ProductsResponse
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        ArticleNumber = item.ArticleNumber,
                        Description = item.Description,
                        Specifications = item.Specifications,
                        CategoryName = item.Category.ToString()
                    });
                return new OkObjectResult(_products);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetProductsByCategory(int id)
        {
            var _products = new List<ProductsResponse>();

            foreach (var item in await _context.Products.ToListAsync())
            {
                if (((int)item.Category) == id)
                {
                    _products.Add(new ProductsResponse
                    {
                        Id = item.Id,
                        ArticleNumber = item.ArticleNumber,
                        Name = item.Name,
                        Price = item.Price,
                        Description = item.Description,
                        Specifications = item.Specifications,
                        CategoryName = item.Category.ToString()
                    });
                }
            }
            return new OkObjectResult(_products);
        }


        [HttpGet("category")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var enumValues = Enum.GetValues((typeof(Categories)));
                var _categories = new List<CategoryResponse>();

                foreach (Categories category in enumValues)
                {
                    _categories.Add(new CategoryResponse
                    {
                        CategoryId = category.GetHashCode(),
                        CategoryName = category.ToString()
                    });
                }
                return new OkObjectResult(_categories);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductRequest req)
        {
            try
            {

                var specs = new List<Specification>();
                for (int i = 0; i < req.Specifications.Count; i++)
                {

                    var info = new List<SpecInfo>();
                    foreach (var _row in req.Specifications[i].SpecInformation.ToList())
                    {
                        info.Add(_row);

                    }
                    specs.Add(new Specification
                    {
                        Title = req.Specifications[i].Title,
                        SpecInformation = info
                    });

                }

                _context.Add(new Product
                {
                    ArticleNumber = req.ArticleNumber,
                    Name = req.Name,
                    Price = req.Price,
                    Description = req.Description,
                    Specifications = specs,
                    Category = req.Category

                });
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ProductUpdateRequest req)
        {
            try
            {
                var _product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (_product != null)
                {
                    _product.ArticleNumber = req.ArticleNumber;
                    _product.Name = req.Name;
                    _product.Price = req.Price;
                    _product.Description = req.Description;

                    _context.Update(_product);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
                    }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {

                var _product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (_product != null)
                {
                    _context.Remove(_product);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();

        }
    }
}
