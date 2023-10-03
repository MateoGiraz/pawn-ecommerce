using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.DTO.Product;
using PawnEcommerce.DTO.Sale;
using PawnEcommerce.Middlewares;
using Service.Product;
using Service.Sale;

namespace PawnEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExceptionMiddleware]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;


        public SaleController(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] SaleCreationModel newSale)
        {
            var sale = newSale.ToEntity();
            sale.Id = _saleService.Create(sale);
            sale.Products = newSale.CreateSaleProducts(sale, _productService);
            _saleService.Update(sale);
            
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sales = _saleService.GetAll();
            return Ok(sales);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var sales = _saleService.Get(id);
            return Ok(sales);
        }
        
        [HttpPost("Discount")]
        public IActionResult GetDiscount([FromBody] List<ProductDTO> products)
        {
            var newPrice = _saleService.GetDiscount(products.Select(product => product.ToEntity()).ToList());
            return Ok(newPrice);
        }

    }
}