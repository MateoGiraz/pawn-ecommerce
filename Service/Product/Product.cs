﻿namespace Service.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private int _price;
        public int Price { get => _price;
            set {  
               if(value< 0) throw new ServiceException("Price must be a positive integer.");
                _price = value; 
            }
        }

        public Category Category { get; set; }
        public Brand Brand { get; set; }

        public List<string> Colors { get; set; }

        public Product(String name, String Description, int price,Category category,Brand brand) {
            this.Name = name;  
            this.Description = Description;
            this.Price = price;
            this.Category = category;
            this.Brand = brand;
            this.Colors = new List<string>();  
        }

        public void AddColor(string color)
        {
            if (color.Equals("")) throw new ServiceException("Color must not be null");
            if (Colors.Contains(color)) return;
            Colors.Add(color);
        }
    }
}