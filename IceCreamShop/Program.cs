using System;
using IceCreamShop;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IceCreamShop{
    class Program{
        static void Main(string[] args){
            //not split(",")
            List<string> customerlist = new List<string>();
            //
            List<IceCream>icecreamlist= new List<IceCream>();
            List<Order>orderHistory=new List<Order>();
            
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
                    Console.WriteLine("Option {0} - {1}", choice, menu[choice - 1]);
                    string[] FullData = File.ReadAllLines("customers.csv");
                    foreach (string line in FullData)
                    {
                        customerlist.Add(line);
                        string[] data = line.Split(",");
                        Console.WriteLine("{0,-12}{1,-12}{2,-15}{3,-20}{4,-20}{5,-20}", data[0], data[1], data[2], data[3], data[4], data[5]);
                    } 
                }
                else if(choice == 2)
                {

                }
                else if(choice == 3)
                {
                    Console.WriteLine("Option {0} - {1}", choice, menu[choice - 1]);
                    Console.Write("Enter your name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter your ID (Enter 6 digits): ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter your date of birth (DD/MM/YYYY): ");
                    DateTime dob = Convert.ToDateTime(Console.ReadLine());
                    Customer customer = new Customer(name, id, dob);
                    string membershipstatus = "Ordinary";
                    PointCard pointcard = new PointCard(0, 0);
                    //assigning Pointcard object to customer
                    customer.Rewards = pointcard;
                    using (StreamWriter sw = new StreamWriter("customers.csv", true))
                    {
                        sw.WriteLine(name + "," + id + "," + dob.ToString("yyyy/MM/dd")+","+membershipstatus+"," + customer.Rewards.Points + ","+customer.Rewards.PunchCard);
                    }
                    string h = File.ReadAllText("customers.csv");
                    Console.WriteLine(h);
                }
                else if (choice == 4)
                {
                    string[] FullData = File.ReadAllLines("customers.csv");
                    for (int i = 0; i < FullData.Length; i++)
                    {
                        string[] data = FullData[i].Split(",");
                        Console.WriteLine("{0}. {1}", i + 1, data[0]);
                    }
                    Console.WriteLine("Enter Customer Number : ");
                    int customer = Convert.ToInt32(Console.ReadLine());
                    string[] datalist = FullData[customer-1].Split(",");
                    string name = datalist[0];
                    int MemberID = Convert.ToInt32(datalist[1]);
                    DateTime DOB = Convert.ToDateTime(datalist[2]);
                    Customer customer1 = new Customer(name, MemberID, DOB);
                    Order icecreamorder;
                    while (true)
                    {
                        icecreamorder = customer1.MakeOrder();
                        Console.WriteLine("Would you like to make another order? Y/N ");
                        if (Console.ReadLine().ToUpper() == "N")
                        {
                            break;
                        }
                        else if (Console.ReadLine().ToUpper() != "Y")
                        {
                            Console.WriteLine("Neither option have been selected. Assuming no more orders.");
                            break;
                        }
                    }
                    //link the new order to the customer’s current order
                    customer1.CurrentOrder = icecreamorder;
                    string tier = customer1.Rewards.Tier;
                    Console.Write(tier);

                }
                else if (choice == 0) { break; }

            }          
        }

    }
}