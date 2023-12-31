using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Service.DTO.Product;
using Service.Product;
using Service.Sale;

namespace Service.DTO.Sale;

[ExcludeFromCodeCoverage]
public class SaleCreationModel
{
    public int[] ProductIds { get; set; }
    public string PaymentMethod { get; set; }
    
    public Service.Sale.Sale ToEntity()
    {
        return new Service.Sale.Sale();
    }

    public List<SaleProduct> CreateSaleProducts(Service.Sale.Sale sale, Service.Product.Product[] updatedCart, IProductService productService)
    {
        var saleProducts = updatedCart
            .Select(prod =>
            {
                productService.DecreaseStock(prod.Id, 1);

                return new SaleProduct
                {
                    Product = prod,
                    SaleId = sale.Id,
                    Sale = sale,
                    ProductId = prod.Id
                };
            });

        return saleProducts.ToList();
    }

}