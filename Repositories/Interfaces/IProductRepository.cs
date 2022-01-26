﻿using KhareedLo.Models;
using KhareedLo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhareedLo
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetAllProducts();

        List<Products> GetAllProductsByID(List<int> IDs);

        Products GetProductById(int prodId);

        int AddProduct(Products vari);

        Products UpdateProduct(int id, Products vari);

        void DeleteProduct(int ID);
    }
}
