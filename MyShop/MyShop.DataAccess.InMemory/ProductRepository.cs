using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        //In memory cache 
        ObjectCache cache = MemoryCache.Default;

        //Product list
        List<Product> products = new List<Product>();

        //Contructor
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        //Save the products in the cache
        public void Commit()
        {
            cache["products"] = products;
        }

        //Add product
        public void Insert(Product p)
        {
            products.Add(p);
        }

        //Update a product
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        //Find a product 
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        //Return a list of products
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        //Delete a product 
        public void Delete(string Id)
        {
             Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
