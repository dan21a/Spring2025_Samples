using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
    public class Cart 
    {
        public int Id { get; set; }
        public List<Product> Items {get; private set;}

        public Cart()
        {
            Items = new List<Product>();
        }

        public void AddtoCart(Product product, int Quantity)
        {
           var existingItem = Items.FirstOrDefault(p => p.Id == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += Quantity; 
            }
            else
            {
                Items.Add(new Product(product.Id, product.Name, product.Price, Quantity)); 
            }
                
        }

        public void RemoveFromCart(Product product, int Quantity)
        {
            var existingItem= Items.FirstOrDefault(p => p.Id == product.Id);
            if(existingItem != null)
            {
                existingItem.Quantity -= Quantity;
                if(existingItem.Quantity <= 0)
                {
                    Items.Remove(existingItem);
                }
            }
        }
        public double GetTotalPrice()
        {
            double total = 0;
            foreach (var item in Items)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }

        public override string ToString()
        {
            return string.Join("\n", Items.Select(p => $"{p.Id}. {p.Name} - {p.Quantity} @ ${p.Price:F2} each"));
        }
    }

}
