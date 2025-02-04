using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

       public string Display => $"{Id}. {Name} - Price: ${Price:F2} - Quantity: {Quantity}";

        // Default constructor
        public Product()
        {
            Name = string.Empty;
        }

        // Constructor with user input
        public Product(int id = 0, string name = "Unnamed Product", double price = 0.0, int quantity = 0)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        // Method to create a new product using user input
        public static Product CreateProduct()
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine() ?? "Unnamed Product";

            Console.Write("Enter Product Price: ");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                Console.WriteLine("Invalid price. Defaulting to $0.00.");
                price = 0.00;
            }

            Console.Write("Enter Product Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity. Defaulting to 0.");
                quantity = 0;
            }

            return new Product(0, name, price, quantity); // ID is assigned later
        }
        public override string ToString()
        {

            return Display ?? string.Empty;
        }
    }
}
