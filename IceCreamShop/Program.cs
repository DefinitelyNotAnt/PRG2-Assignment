using System;
using System.Reflection;
using IceCreamShop;
using static System.Net.Mime.MediaTypeNames;

namespace IceCreamShop
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customerlist = new List<Customer>();
            List<Customer> regCust = new List<Customer>();
            List<Customer> goldCust = new List<Customer>();
            Queue<Order> orderqueue = new Queue<Order>();
            Queue<Order> goldqueue = new Queue<Order>();
            Queue<Order> regqueue = new Queue<Order>();
            bool menuLoop = true;
            /*
             * testing
            List<Flavour> bobflavour = new List<Flavour>();
            Flavour bobflav1 = new Flavour("Strawberry", false, 1);
            bobflavour.Add(bobflav1);
            Topping bobtop = new Topping("Sprinkles");
            List<Topping> bobtopping = new List<Topping>();
            bobtopping.Add(bobtop);
            Cup bobcup = new Cup("Cup", 2, bobflavour, bobtopping);
            Customer boob = new Customer("Bob", 123456, DateTime.Parse("11/11/2000"));
            Order bobOrder = new Order(2, DateTime.Parse("12/12/2002"));
            boob.CurrentOrder = bobOrder;
            boob.CurrentOrder.AddIceCream(bobcup);*/
            while (menuLoop)
            {
                try
                {
                    customerlist = CreateCustomers();
                    orderqueue = CreateOrders(customerlist);
                    /*
                     * Testing
                    boob.CurrentOrder.ModifyIceCream(1);
                    Console.WriteLine(boob.CurrentOrder.ToString());
                    Console.WriteLine(customerlist.Count);
                    Console.WriteLine(customerlist[1]);
                    Console.WriteLine(customerlist[2]);*/
                    regCust = SortCustomer(customerlist, false);
                    goldCust = SortCustomer(customerlist, true);
                    regqueue = SortQueues(regCust, orderqueue);
                    goldqueue = SortQueues(goldCust, orderqueue);
                    /*
                     * Testing
                    foreach (Customer customer2 in regCust)
                    {
                        Console.WriteLine(customer2.ToString());
                    }
                    Console.WriteLine();
                    foreach (Customer customer3 in goldCust)
                    {
                        Console.WriteLine(customer3.ToString());
                    }*/
                    string[] menu = { "List all customers", "List all current orders", "Register a new Customer", "Create a customer's order", "Display order details of a customer", "Modify order details" };
                    /*Console.WriteLine("________________________________________________");
                    Console.WriteLine("{0,48}","I.C. TREATS");
                    Console.WriteLine("{0,48}", "Welcome to Singapore’s first robotic ice cream store!");
                    Console.WriteLine("________________________________________________");*/
                    Console.WriteLine("Available Options:");
                    int number = 1;
                    foreach (string option in menu)
                    {
                        Console.WriteLine("{" + number + "} " + option);
                        number++;
                    }
                    Console.WriteLine("{0} Exit");
                    Console.Write("Enter your option: ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        // Option 1: Display All Customers
                        case 1:
                            Console.WriteLine("Option {0} - {1}", choice, menu[choice - 1]);
                            DisplayCustomer();
                            break;
                        // Option 2: Display All Orders
                        case 2:
                            DisplayOrders(goldqueue, regqueue);
                            break;
                        // Option 3: Register new customer
                        case 3:
                            Console.WriteLine("Option {0} - {1}", choice, menu[choice - 1]);
                            Console.Write("Enter your name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter your ID (Enter 6 digits): ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter your date of birth (YYYY/MM/DD): ");
                            DateTime dob = Convert.ToDateTime(Console.ReadLine());
                            Customer customer = new Customer(name, id, dob);
                            PointCard pointcard = new PointCard(0, 0);
                            //Need to assign pointcard to customer
                            using (StreamWriter sw = new StreamWriter("customers.csv", true))
                            {
                                sw.WriteLine(name + "," + id + "," + dob.ToString("yyyy/MM/dd") + "," + "Ordinary" + "," + "0" + "," + "0");
                            }
                            break;
                        // Option 4: Create a customer's order
                        case 4:
                            string[] FullData = File.ReadAllLines("customers.csv");
                            for (int i = 0; i < FullData.Length; i++)
                            {
                                string[] data = FullData[i].Split(",");
                                Console.WriteLine("{0}. {1}", i + 1, data[0]);
                            }
                            Console.WriteLine("Enter Customer Number : ");
                            int newcustomer = Convert.ToInt32(Console.ReadLine());
                            string[] datalist = FullData[newcustomer - 1].Split(",");
                            string customername = datalist[0];
                            int MemberID = Convert.ToInt32(datalist[1]);
                            DateTime DOB = Convert.ToDateTime(datalist[2]);
                            Customer customer1 = new Customer(customername, MemberID, DOB);
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
                            break;
                        // Display order details of a customer
                        case 5:
                            Console.WriteLine();
                            break;
                        // Modify Order Details
                        case 6:
                            ModifyOrderDetails();

                            break;
                        case 0:
                            menuLoop = false;
                            Console.WriteLine("Thank you!");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Input error.");
                }
            }

            void DisplayCustomer()
            {
                string[] FullData = File.ReadAllLines("customers.csv");
                foreach (string line in FullData)
                {
                    string[] data = line.Split(",");
                    Console.WriteLine("{0,-12}{1,-12}{2,-15}", data[0], data[1], data[2]);
                }
            }
            List<Customer> CreateCustomers()
            {
                List<Customer> customers = new List<Customer>();
                try
                {
                    // Read customer data
                    List<string[]> customerdata = Utils.GetInfo("customers.csv", false);
                    // For every customer (Skip first line because that is display)
                    for (int i = 1; i < customerdata.Count; i++)
                    {
                        string customerName = customerdata[i][0];
                        int customerID = Convert.ToInt32(customerdata[i][1]);
                        string status = customerdata[i][3];
                        // Culture is correct format for now
                        string[] customerDOBinfo = customerdata[i][2].Split("/");
                        DateTime customerDOB = DateTime.Parse(customerdata[i][2]);
                        Customer newcustomer = new Customer(customerName, customerID, customerDOB);
                        newcustomer.Rewards.Tier = status;
                        newcustomer.Rewards.Points = Convert.ToInt32(customerdata[i][4]);
                        newcustomer.Rewards.PunchCard = Convert.ToInt32(customerdata[i][5]);
                        customers.Add(newcustomer);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("Error getting customer data.");
                }
                return customers;
            }
            Queue<Order> CreateOrders(List<Customer> custlist)
            {
                Queue<Order> orders = new Queue<Order>();
                try
                {
                    // Read order data
                    List<string[]> orderdata = Utils.GetInfo("orders.csv", false);
                    List<string[]> flavourdata = Utils.GetInfo("flavours.csv", false);
                    List<string[]> toppingdata = Utils.GetInfo("toppings.csv", false);
                    for (int i = 1; i < orderdata.Count; i++)
                    {
                        Order newOrder = new Order(Convert.ToInt32(orderdata[i][0]), DateTime.Parse(orderdata[i][2]));
                        bool check = false;
                        List<Flavour> flavours = new List<Flavour>();
                        List<Topping> toppings = new List<Topping>();
                        for (int flavcount = 0; flavcount < 3; flavcount++)
                        {
                            foreach (string[] flavdata in flavourdata)
                            {
                                if (flavdata[0].ToUpper() == orderdata[i][flavcount + 8].ToUpper())
                                {
                                    Flavour newflavour = new Flavour(flavdata[0], Convert.ToBoolean(flavdata[2]), 1);
                                    flavours.Add(newflavour);
                                    break;
                                }
                            }
                        }
                        for (int toppingcnt = 0; toppingcnt < 4; toppingcnt++)
                        {
                            foreach (string[] topdata in toppingdata)
                            {
                                if (topdata[0].ToUpper() == orderdata[i][toppingcnt + 11].ToUpper())
                                {
                                    Topping newtopping = new Topping(topdata[0]);
                                    toppings.Add(newtopping);
                                    break;
                                }
                            }
                        }
                        foreach (Order order in orders)
                        {

                            if (order.Id == newOrder.Id)
                            {
                                string ictype = orderdata[i][4];
                                switch (ictype)
                                {
                                    case "Cup":
                                        Cup newCup = new Cup(ictype, Convert.ToInt32(orderdata[i][5]), flavours, toppings);
                                        order.AddIceCream(newCup);
                                        break;
                                    case "Cone":
                                        Cone newCone = new Cone(ictype, Convert.ToInt32(orderdata[i][5]), flavours, toppings, Convert.ToBoolean(orderdata[i][6]));
                                        order.AddIceCream(newCone);
                                        break;
                                    case "Waffle":
                                        Waffle newWaffle = new Waffle(ictype, Convert.ToInt32(orderdata[i][5]), flavours, toppings, orderdata[i][7]);
                                        order.AddIceCream(newWaffle);
                                        break;
                                    default:
                                        throw new Exception("Error");
                                }
                                check = true;
                                break;
                            }
                        }
                        if (!check)
                        {
                            // Finding customer
                            foreach (Customer customer in custlist)
                            {
                                if (customer.Memberid == Convert.ToInt32(orderdata[i][1]))
                                {
                                    string ictype = orderdata[i][4];
                                    switch (ictype)
                                    {
                                        case "Cup":
                                            Cup newCup = new Cup(ictype, Convert.ToInt32(orderdata[i][5]), flavours, toppings);
                                            newOrder.AddIceCream(newCup);
                                            break;
                                        case "Cone":
                                            Cone newCone = new Cone(ictype, Convert.ToInt32(orderdata[i][5]), flavours, toppings, Convert.ToBoolean(orderdata[i][6]));
                                            newOrder.AddIceCream(newCone);
                                            break;
                                        case "Waffle":
                                            Waffle newWaffle = new Waffle(ictype, Convert.ToInt32(orderdata[i][5]), flavours, toppings, orderdata[i][7]);
                                            newOrder.AddIceCream(newWaffle);
                                            break;
                                        default:
                                            throw new Exception("Error");
                                    }
                                    customer.CurrentOrder = newOrder;
                                    orders.Enqueue(newOrder);
                                    customer.orderHistory.Add(newOrder);
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("Failed to load all orders.");
                }
                return orders;
            }
            List<Customer> SortCustomer(List<Customer> custList, bool gold)
            {
                try
                {
                    List<List<Customer>> sortMembers
                        = custList.GroupBy(x => x.Rewards.Tier)
                        .Select(group => group.ToList())
                        .ToList();
                    if (gold)
                    {
                        custList = sortMembers[0];
                    }
                    else
                    {
                        custList = sortMembers[1];
                        custList.AddRange(sortMembers[2]);
                    }
                }
                catch
                {
                    Console.WriteLine("Error");
                }
                return custList;
            }
            Queue<Order> SortQueues(List<Customer> typecust, Queue<Order> orderqueue)
            {
                Queue<Order> returnQ = new Queue<Order>();
                try
                {
                    foreach (Customer cust in typecust)
                    {
                        if (cust.CurrentOrder != null)
                        {
                            if (cust.CurrentOrder.IceCreamList.Count > 0)
                            {
                                returnQ.Enqueue(cust.CurrentOrder);
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Error");
                }
                return returnQ;
            }
            void DisplayOrders(Queue<Order> goldQ, Queue<Order> regQ)
            {
                Console.WriteLine("Gold Queue:");
                Console.WriteLine($"[No.] {"Order ID",-10}{"Time Received",-25}{"Number of ice cream",-5}");
                int count = 1;
                foreach (Order order in goldQ)
                {
                    Console.WriteLine($"[ {count,2}] {order.Id,-10}{order.TimeReceived,-25}{order.IceCreamList.Count,-5}");
                    count++;
                }
                Console.WriteLine("\nRegular Queue:");
                count = 1;
                foreach (Order order in regQ)
                {
                    Console.WriteLine($"[ {count,2}] {order.Id,-10}{order.TimeReceived,-25}{order.IceCreamList.Count,-5}");
                }

                return;
            }
            void ModifyOrderDetails()
            {
                try
                {
                    // Print all customers and select option
                    int count = 1;
                    Console.WriteLine($"[No.] {"Name",-10}{"Member ID",-10}{"Tier",-10}");
                    foreach (Customer cust in customerlist)
                    {
                        Console.WriteLine($"[ {count,-2}] {cust.Name,-10}{cust.Memberid,-10}{cust.Rewards.Tier,-10}");
                        count++;
                    }/*
                    Console.Write("Select customer: ");
                    userinput = Console.ReadLine();
                    int indexing = Convert.ToInt32(userinput) - 1;
                    Customer selectedCustomer = customerlist[indexing];
                    Order custOrder = selectedCustomer.CurrentOrder;*/
                }
                catch
                {
                    Console.WriteLine("Error.");
                }
            }
        }
    }
}