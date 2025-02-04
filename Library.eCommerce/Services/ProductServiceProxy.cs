using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        
        private ProductServiceProxy()
        {
            Products = new List<Product?>();
            Cart = new Cart();
        }

        private int LastKey
        {
            get
            {
                if(!Products.Any())
                {
                    return 0;
                }

                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Product?> Products { get; private set; }
        public Cart Cart { get; private set; }
        
        public Product AddOrUpdate(Product product)
        {
            if(product.Id == 0)
            {
                product.Id = LastKey + 1;
                Products.Add(product);
            }


            return product;
        }

        public Product? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }

            Product? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);

            return product;
        }
        public void AddToCart(int id, int quantity)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Error: Product not found.");
                return;
            }

            if (product.Quantity >= quantity)
            {
                Cart.AddtoCart(product, quantity);  
                product.Quantity -= quantity;
            }
            else
            {
                Console.WriteLine("Error: Not enough stock available.");
            }
        }

        public void RemoveFromCart(int id, int quantity)
        {
            var product = Cart.Items.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Error: Product not found in cart.");
                return;
            }

            Cart.RemoveFromCart(product, quantity);
            
            var inventoryProduct = Products.FirstOrDefault(p => p.Id == id);
            if (inventoryProduct != null)
            {
                inventoryProduct.Quantity += quantity;
            }
        }

        public void Checkout()
        {
            Console.WriteLine("===== Receipt =====");
            Console.WriteLine(Cart);
            double subtotal = Cart.GetTotalPrice();
            double tax = subtotal * 0.07;
            double total = subtotal + tax;
            Console.WriteLine($"Subtotal: ${subtotal:F2}");
            Console.WriteLine($"Tax (7%): ${tax:F2}");
            Console.WriteLine($"Total: ${total:F2}");
            Console.WriteLine("===================");

            // Clear cart after checkout
            Cart = new Cart();
        }
    }
}
