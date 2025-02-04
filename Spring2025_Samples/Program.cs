//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Xml.Serialization;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Amazon!");

            Console.WriteLine("1. Create new inventory item");
            Console.WriteLine("2. Read all inventory items");
            Console.WriteLine("3. Update an inventory item");
            Console.WriteLine("4. Delete an inventory item");
            Console.WriteLine("5. Add item to cart");
            Console.WriteLine("6. View cart");
            Console.WriteLine("7. Remove item from cart");
            Console.WriteLine("8. Checkout");
            Console.WriteLine("Q. Quit");

            List<Product?> list = ProductServiceProxy.Current.Products;

            char choice;
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {
                    case '1':
                        Product newProduct = Product.CreateProduct();
                        ProductServiceProxy.Current.AddOrUpdate(newProduct);
                        break;
                    case '2':
                        list.ForEach(Console.WriteLine);
                        break;
                    case '3':
                        //select one of the products
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection);

                        if(selectedProd != null)
                        {
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";
                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;
                    case '4':
                        //select one of the products
                        //throw it away
                        Console.WriteLine("Which product would you like to update?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;

                    case '5':
                        Console.Write("Enter product ID to add to cart: ");
                        int cartId = int.Parse(Console.ReadLine() ?? "-1");

                        Console.Write("Enter quantity: ");
                        int cartQty = int.Parse(Console.ReadLine() ?? "1");

                        ProductServiceProxy.Current.AddToCart(cartId, cartQty);
                        break;

                    case '6': // View cart
                        Console.WriteLine(ProductServiceProxy.Current.Cart);
                        break;

                    case '7': // Remove from cart
                        Console.Write("Enter product ID to remove from cart: ");
                        int removeId = int.Parse(Console.ReadLine() ?? "-1");

                        Console.Write("Enter quantity to remove: ");
                        int removeQty = int.Parse(Console.ReadLine() ?? "1");

                        ProductServiceProxy.Current.RemoveFromCart(removeId, removeQty);
                        break;

                    case '8': // Checkout
                        ProductServiceProxy.Current.Checkout();
                        break;
                    case 'Q':
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }

            
            } while (choice != 'Q' && choice != 'q');

            Console.ReadLine();
        }
    }


}
