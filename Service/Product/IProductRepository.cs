﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Service.Product;
using Service.User;

namespace Service.Product
{
    public interface IProductRepository
    {
        int AddProduct(Product newProduct);
        Product GetProductByName(string productName);
        Product Get(int id);
        void UpdateProduct(Product newProductVersion);
        void DeleteProduct(int id);
        Product[] GetAllProducts();
        Boolean Exists(int id);
        void Reset();
    }
}