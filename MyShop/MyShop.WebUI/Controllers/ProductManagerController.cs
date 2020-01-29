using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.Models.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //Product instance repository
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        //Constractor 
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoriesContext) 
        {
            context = productContext;
            productCategories = productCategoriesContext;
           
        }

        // Return a list of products
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }


        //Action to create a product 

        //Product capture form
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        //Action to add product to the repository
        [HttpPost]
        public ActionResult Create(Product product,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid) 
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductsImages//")+product.Image);  
                }
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index"); 
            }
        }


        //Updating a record 
        //Action to pull a record to be updated
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }                  
        }
        //Action to actually update product
        [HttpPost]
        public ActionResult Edit(Product product,string Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductsImages//") + productToEdit.Image);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();
                return RedirectToAction("Index");
            }
        }


        //Delete a product from the repository
        //Action to pull and display the product to be deleted 
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }

        }

        //Action to actually delete a product
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();  
                return RedirectToAction("Index");
            }
        }
    }
}