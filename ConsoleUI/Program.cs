using DemoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static ShoppingCartModel cart = new ShoppingCartModel();

        static void Main(string[] args)
        {
            PopulateCartWithDemoData();
            //calling GenerateTotal method from ShoppingCartModel. And passing three (delegate) methods
            Console.WriteLine($"The total for the cart is {cart.GenerateTotal(SubTotalAlert, CalculateLeveledDiscount, AlertUser):C2}");
            Console.WriteLine();

            //first method is subTotal alert. it takes in decimal.delegate doesnt care. it has one param
            //second method that we pass has two params. products list & subtotal 
            //basically we call GenerateTotal method from ShoppingCartModel. these three (delegate) methods will be called
            //from GenerateTotal method. and from there it will pass here values like "subTotal","products", "message" will be passed
            //from there too
            decimal total = cart.GenerateTotal((subTotal) => Console.WriteLine($"Subtotal for cart 2 is: " +
                $"{subTotal:C2}"), (products, subTotal) => {
                    if (products.Count > 3)
                        return subTotal * 0.5M;
                    else
                        return subTotal;
                },
                (message) => Console.WriteLine(message));
            Console.WriteLine($"The total for cart 2 is {total:C2}");
            Console.WriteLine();

            Console.Write("Please press any key to exit the application...");
            Console.ReadKey();
        }
        //thats for action delegate. it doesnt return anything. just void. 
        private static void AlertUser(string message)
        {
            Console.WriteLine(message);
        }

        private static void SubTotalAlert(decimal subTotal)
        {
            Console.WriteLine($"The subtotal is {subTotal:C2}");
        }
        //method for Func delegate. it returns something
        private static decimal CalculateLeveledDiscount(List<ProductModel> items, decimal subTotal)
        {
            if (subTotal > 100)
                return subTotal * 0.80M;
            else if (subTotal > 50)
                return subTotal * 0.85M;
            else if (subTotal > 10)
                return subTotal * 0.90M;
            else
                return subTotal;
        }

        //puts four items into cart
        private static void PopulateCartWithDemoData()
        {
            cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 3.63M });
            cart.Items.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
            cart.Items.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
            cart.Items.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });
        }
    }
}
