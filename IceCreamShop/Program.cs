using System;

namespace IceCreamShop{
    class Program{
        static void Main(string[] args){
            while (true)
            {
                string[] menu = { "List all customers","List all current orders","Register a new Customer","Create a customer's order","Display order details of a customer","Modify order details" };
                /*Console.WriteLine("________________________________________________");
                Console.WriteLine("{0,48}","I.C. TREATS");
                Console.WriteLine("{0,48}", "Welcome to Singapore’s first robotic ice cream store!");
                Console.WriteLine("________________________________________________");*/
                Console.WriteLine("Available Options:");
                int number = 1;
                foreach (string option in menu)
                {
                    Console.WriteLine("{" +number+ "} "+ option);
                    number++;
                }
                Console.WriteLine("{0} Exit");
                Console.Write("Enter your option: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    //Option 1
                    Option1();
                }
                else if(choice == 2)
                {

                }
                else if(choice == 3) { }
                else if (choice == 0) { break; }

            }



            void Option1()
            {
                string[] FullData = File.ReadAllLines("customers.csv");
                Console.WriteLine("Option 1");
                foreach (string line in FullData)
                {
                    string[] data = line.Split(",");
                    Console.WriteLine("{0,-12}{1,-12}{2,-15}", data[0], data[1], data[2]);
                }
            }
        }

    }
}