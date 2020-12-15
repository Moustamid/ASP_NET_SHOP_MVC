using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{   
    public class ProductManagerController : Controller
    {

        IRepository<Product> context;
        IRepository<ProductCategory>  productCategories;

        // Constructer : 
        public ProductManagerController(IRepository<Product> productContext , IRepository<ProductCategory> productCategoriesContext)
        {
            context = productContext;
            productCategories = productCategoriesContext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        // Create a Product-page-1:  "Fill in the product details"

        public ActionResult Create()
        {
            ProductManagerViewModel ViewModel = new ProductManagerViewModel();

            ViewModel.Product = new Product();
            ViewModel.productCategories = productCategories.Collection();

            return View(ViewModel);
        }

        // Create a Product-page-2  "Posting the product details "
         [HttpPost]
        public ActionResult Create(Product product)
        {
            // Check if the Product validation are Correct .
            if (!ModelState.IsValid)
            {   
                // return to the page : it will send back the validation issues to the user 
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        // Edit a Product-page-1 :  "Find the product to Edit"
        public ActionResult Edit(string Id)
        {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel ViewModel = new ProductManagerViewModel();

                ViewModel.Product = new Product();
                ViewModel.productCategories = productCategories.Collection();

                return View(ViewModel);
            }
        }

        // Edit a Product-page-2 :  "Edit the product Found"
        [HttpPost]
        public ActionResult Edit(Product product , string Id)
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

                productToEdit.Name = product.Name;
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        // Delete :  Load the product to the BD

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

        //  Comfirm Delete : 
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