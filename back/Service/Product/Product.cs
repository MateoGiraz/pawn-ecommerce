﻿using Service.Exception;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Service.Product
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private int _price;
        public int Price
        {
            get => _price;
            set
            {
                if (value < 0) throw new ModelException("Price must be a positive integer.");
                _price = value;
            }
        }

        private int _stock;

        private readonly object _stockLock = new object();

        public int Stock
        {
            get => _stock;
            set
            {
                ValidateStock(value);
                _stock = value;
            }
        }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool IsExcludedFromPromotions { get; set; }

        [JsonIgnore]
        public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();

        [NotMapped]
        public IEnumerable<Color> Colors => ProductColors?.Select(pc => pc.Color);

        public void AddColor(Color color)
        {
            if (color == null) throw new ModelException("Color must not be null");

            if (!ProductColors.Any(pc => pc.Color.Id == color.Id))
            {
                var productColor = new ProductColor { Color = color, Product = this };
                ProductColors.Add(productColor);
            }
        }

        public void IncreaseStock(int stockToBeAdded)
        {
            ValidateStockChange(stockToBeAdded);

            lock (_stockLock)
            {
                this.Stock += stockToBeAdded;
            }
        }

        public void DecreaseStock(int stockToBeRemoved)
        {
            ValidateStockChange(stockToBeRemoved);

            lock (_stockLock)
            {
                if (stockToBeRemoved > this.Stock)
                {
                    throw new ModelException("Not enough stock to remove.");
                }

                this.Stock -= stockToBeRemoved;
            }
        }

        public bool IsStockAvailable(int samplesToBeBought)
        {
            return samplesToBeBought <= this.Stock;
        }

        private void ValidateStockChange(int stockChange)
        {
            if (stockChange < 0) throw new ModelException("Cannot add or remove negative stock");
        }

        private void ValidateStock(int stock)
        {
            if (stock < 0) throw new ModelException("Stock must be a positive integer.");
        }
    }
}