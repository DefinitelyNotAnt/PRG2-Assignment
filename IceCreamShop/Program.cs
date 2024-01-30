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
                            string membershipstatus = "Ordinary";
                            PointCard pointcard = new PointCard(0, 0);
                            //assigning Pointcard object to customer
                            customer.Rewards = pointcard;
                            using (StreamWriter sw = new StreamWriter("customers.csv", true))
                            {
                                sw.WriteLine(name + "," + id + "," + dob.ToString("yyyy/MM/dd") + "," + membershipstatus + "," + customer.Rewards.Points + "," + customer.Rewards.PunchCard);
                            }
                            string h = File.ReadAllText("customers.csv");
                            Console.WriteLine(h);
                            break;

                        // Option 4: Create a customer's order
                        case 4:
                            string[] FullData = File.ReadAllLines("customers.csv");
                            for (int i = 1; i < FullData.Length-1; i++)
                            {
                                string[] data = FullData[i].Split(",");
                                Console.WriteLine("{0}. {1}", i , data[0]);
                            }
                            Console.WriteLine("Enter Customer Number : ");
                            int newcustomer = Convert.ToInt32(Console.ReadLine());
                            string[] datalist = FullData[newcustomer].Split(",");
                            string customername = datalist[0];
                            int MemberID = Convert.ToInt32(datalist[1]);
                            DateTime DOB = Convert.ToDateTime(datalist[2]);
                            Customer customer1 = new Customer(customername, MemberID, DOB);
                            Order icecreamorder;
                            List<Order> ordershistory = new List<Order>();
                            while (true)
                            {
                                icecreamorder = customer1.MakeOrder();
                                customer1.CurrentOrder= icecreamorder;
                                ordershistory.Add(icecreamorder);
                                Console.WriteLine("Would you like to make another order? Y/N ");
                                if (Console.ReadLine().ToUpper() == "N")
                                {
                                    customer1.OrderHistory = ordershistory;
                                    break;
                                }
                                else if (Console.ReadLine().ToUpper() != "Y")
                                {
                                    customer1.OrderHistory = ordershistory;
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
                            SelectCustomerAndOrder(customerlist, true);
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
                    Console.WriteLine("{0,-12}{1,-12}{2,-15}{3,-20}{4,-20}{5,-20}", data[0], data[1], data[2], data[3], data[4], data[5]);
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
                                    newOrder.TimeFulfilled = DateTime.Parse(orderdata[i][3]);
                                    customer.CurrentOrder = newOrder;
                                    orders.Enqueue(newOrder);
                                    customer.OrderHistory.Add(newOrder);
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
            // Display Orders
            void DisplayOrders(Queue<Order> goldQ, Queue<Order> regQ)
            {
                // Prints gold queue
                Console.WriteLine("Gold Queue:");
                Console.WriteLine($"[No.] {"Order ID",-10}{"Time Received",-25}{"Number of ice cream",-5}");
                int count = 1;
                foreach (Order order in goldQ)
                {
                    Console.WriteLine($"[ {count,2}] {order.Id,-10}{order.TimeReceived,-25}{order.IceCreamList.Count,-5}");
                    count++;
                }
                // Prints Regular queue
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
                    Order custOrder = SelectCustomerAndOrder(customerlist, false);
                    bool menulooping = true;
                    while (menulooping)
                    {
                        Console.Write("_______________________________________________\n" +
                                      "_________________ Modify Order ________________\n" +
                                      "_______________________________________________\n" +
                                          "[ 1 ] Modify ice cream\n" +
                                          "[ 2 ] Add ice cream\n" +
                                          "[ 3 ] Remove ice cream\n" +
                                          "[ 4 ] View current order\n" +
                                          "[ 0 ] Cancel\n" +
                                          "Select an option: ");
                        string? userinput = Console.ReadLine();
                        switch (userinput)
                        {
                            case "1":
                                Console.WriteLine(custOrder.ToString());
                                Console.Write("Choose ice cream to modify: ");
                                userinput = Console.ReadLine();
                                // try catch will send any invalid options into catch below
                                int icecreamIndex = Convert.ToInt32(userinput);
                                custOrder.ModifyIceCream(icecreamIndex);
                                break;
                            case "2":
                                List<string[]> data = Utils.GetInfo("options.csv", true);
                                // Gets option selected and stores it inside newSelection
                                string[] newSelection = Utils.Readinfo(data, null);
                                List<Flavour> flavours = new List<Flavour>();
                                // Go to catch block here if invalid option selected
                                int flavourcount = Convert.ToInt32(newSelection[1]);
                                // Empty Flavour and Topping Lists
                                List<Flavour> flavours1 = new List<Flavour>();
                                List<Topping> topping1 = new List<Topping>();
                                // 1 Flavour per scoop
                                List<string[]> flavourData = Utils.GetInfo("flavours.csv", true);
                                // 1 Flavour per scoop
                                for (int i = 0; i < flavourcount; i++)
                                {
                                    // Get flavour
                                    Console.WriteLine($"Flavour {i + 1}");
                                    string[] fullFlavour = Utils.Readinfo(flavourData, null);
                                    // Create new flavour
                                    bool check = false;
                                    // If flavour already inside
                                    foreach (Flavour flavour in flavours1)
                                    {
                                        if (flavour.Type == fullFlavour[0])
                                        {
                                            // Increase quantity
                                            flavour.Quantity += 1;
                                            check = true;
                                        }
                                    }
                                    // Fail check
                                    if (!check)
                                    {
                                        // Add flavour
                                        Flavour newFlavour = new Flavour(fullFlavour[0], Convert.ToBoolean(fullFlavour[2]), 1);
                                        flavours1.Add(newFlavour);
                                    }
                                }
                                switch (newSelection[0])
                                {
                                    case "Cup":
                                        Cup newCup = new Cup(newSelection[0], Convert.ToInt32(newSelection[1]), flavours1, topping1);
                                        custOrder.AddIceCream(newCup);
                                        break;
                                    case "Cone":
                                        Cone newCone = new Cone(newSelection[0], Convert.ToInt32(newSelection[1]), flavours1, topping1, Convert.ToBoolean(newSelection[2]));
                                        custOrder.AddIceCream(newCone);
                                        break;
                                    case "Waffle":
                                        Waffle newWaffle = new Waffle(newSelection[0], Convert.ToInt32(newSelection[1]), flavours1, topping1, newSelection[3]);
                                        custOrder.AddIceCream(newWaffle);
                                        break;
                                    default:
                                        throw new Exception("Error");
                                }

                                break;
                            case "3":
                                Console.Write("Select ice cream to remove: ");
                                int removeIndex = Convert.ToInt32(Console.ReadLine());
                                custOrder.IceCreamList.RemoveAt(removeIndex - 1);
                                break;
                            case "4":
                                Console.WriteLine(custOrder.ToString());
                                break;
                            case "0":
                                menulooping = false;
                                break;
                            default:
                                Console.WriteLine("Not an option.");
                                break;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Error.");
                }
            }
            Order SelectCustomerAndOrder(List<Customer> customerlist, bool pastOrder)
            {
                // Print all customers and select option
                int count = 1;
                Console.WriteLine($"[No.] {"Name",-10}{"Member ID",-10}{"Tier",-10}");
                foreach (Customer cust in customerlist)
                {
                    Console.WriteLine($"[ {count,-2}] {cust.Name,-10}{cust.Memberid,-10}{cust.Rewards.Tier,-10}");
                    count++;
                }
                Console.Write("Select customer: ");
                string? userinput = Console.ReadLine();
                int indexing = Convert.ToInt32(userinput) - 1;
                Customer selectedCustomer = customerlist[indexing];
                Order custOrder = selectedCustomer.CurrentOrder;
                // Prints all past orders
                if (pastOrder)
                {
                    // Prints for every order in order history
                    foreach (Order orderhist in selectedCustomer.OrderHistory)
                    {
                        Console.WriteLine();
                        double ordertotal = orderhist.CalculateTotal();
                        Console.WriteLine($"{"Order Id",-10}{"Date Received",-25}{"Date Fulfilled",-25}{"Total Cost"}");
                        Console.WriteLine($"{orderhist.Id,-10}{orderhist.TimeReceived,-25}{orderhist.TimeFulfilled,-25}${ordertotal}");
                        Console.WriteLine(orderhist.ToString());
                    }
                }
                // Prints for current order
                else
                {
                    // Prints current order
                    Console.WriteLine();
                    double ordertotal = custOrder.CalculateTotal();
                    Console.WriteLine($"{"Order Id",-10}{"Date Received",-25}{"Date Fulfilled",-25}{"Total Cost"}");
                    Console.WriteLine($"{custOrder.Id,-10}{custOrder.TimeReceived,-25}{custOrder.TimeFulfilled,-25}${ordertotal}");
                    Console.WriteLine(custOrder.ToString());
                }
                return custOrder;
            }
        }
    }
}