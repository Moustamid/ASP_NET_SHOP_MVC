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
        ObjectCache cache = MemoryCache.Default;

        List<Product> products;

        // Constructor :
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }
        }

        // Commiting Products to the Cache :
        public void Commit()
        {
            cache["products"] = products;
        }


        /* End Point : */

        // Adding a product to the products List :
        public void Insert(Product p)
        {
            products.Add(p);
        }
        
        // UpDate a product in the products List :
        public void Update(Product product)
        {
            Product productToUpdate = products.Find( p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }


        // Find a product in the products List :
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

        // Return a List of Products : 

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        // Delete a product from the List of Products

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
