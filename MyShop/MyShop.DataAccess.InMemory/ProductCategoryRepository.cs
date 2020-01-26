using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using MyShop.Core.Models;


namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        //In memory cache 
        ObjectCache cache = MemoryCache.Default;

        //Product list
        List<ProductCategory> productCategories;

        //Contructor
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        //Save the products in the cache
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        //Add product
        public void Insert(ProductCategory c)
        { 
            productCategories.Add(c);
        }

        //Update a product
        public void Update(ProductCategory Category)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == Category.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = Category;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        //Find a product 
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }

        //Return a list of products
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        //Delete a product 
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
    }
}
