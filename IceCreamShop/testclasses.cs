using System.Linq;
using System.Security.Cryptography.X509Certificates;
using IceCreamShop;
using static System.Formats.Asn1.AsnWriter;
// Notes:
// 1) Implementing so that each topping is limited to 1
// 2) Message shows up when ice cream has 0 scoops and only toppings
// ("Cannot order only toppings")
// 3) Assuming number of flavours do not need to match number of scoops
//      - Modify Ice cream will be a mess
//      - Adding flavour will need to check and add scoop
//      - Removing flvour would need to remove a scoop
//      - Changing scoop number needs to add/remove flavours
//      - Reducing scoop number needs to promopt which flavour to remove
//      - Adding scoops needs to also rpompt flavours
// (Possible but much much longer)
// 4) 
namespace IceCreamShop
{
    //Class Customer
    public class Customer
    {
        private string name;
        private int memberid;
        private DateTime dob;
        private Order? currentOrder;
        private PointCard rewards;
        public string Name { get { return name; } set { name = value; } }
        public int Memberid { get { return memberid; } set { memberid = value; } }
        public DateTime Dob { get { return dob; } set { dob = value; } }
        public Order? CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; }
        }
        public List<Order> orderHistory { get; set; }
            = new List<Order>();
        public PointCard Rewards { get { return rewards; } set { rewards = value; } }
        public Customer()
        {
            name = "";
            memberid = 0;
            dob = DateTime.MinValue;
            currentOrder = new Order();
            orderHistory = new List<Order>();
            rewards = new PointCard();
        }
        public Customer(string Name, int Memberid, DateTime Dob)
        {
            name = Name;
            memberid = Memberid;
            dob = Dob;
            currentOrder = new Order();
            orderHistory = new List<Order>();
            rewards = new PointCard();
        }
        //MakeOrder Method
        //upper all
        public Order MakeOrder()
        {
            //Declaration of variable
            Order order = new Order();
            bool premium;
            //store file data
            List<string> IceCreamOption = new List<string>();
            List<string> WaffleFlavourOption = new List<string>();
            List<string> ToppingsOption = new List<string>();
            List<string> FlavoursOption = new List<string>();

            List<string> FlavourData = new List<string>();

            List<string> OptionData = new List<string>();
            List<Flavour> flavours = new List<Flavour>();
            List<Topping> toppings = new List<Topping>();

            string[] icecreamfile = File.ReadAllLines("IceCreamOption.csv");
            for (int i = 1; i < icecreamfile.Length - 1; i++)
            {
                IceCreamOption.Add(icecreamfile[i]);
            }
            string[] waffleFlavourfile = File.ReadAllLines("waffleflavour.csv");
            for (int i = 1; i < waffleFlavourfile.Length - 1; i++)
            {
                WaffleFlavourOption.Add(waffleFlavourfile[i]);
            }
            string[] toppingfile = File.ReadAllLines("toppings.csv");
            for (int i = 1; i < toppingfile.Length - 1; i++)
            {
                string[] topping = toppingfile[i].Split(",");
                ToppingsOption.Add(topping[0]);
            }
            string[] flavoursfile = File.ReadAllLines("flavours.csv");
            for (int i = 1; i < flavoursfile.Length - 1; i++)
            {
                string[] flavour = flavoursfile[i].Split(",");
                FlavoursOption.Add(flavour[0]);
            }
            while (true)
            {
                Console.WriteLine("Ice Cream Option");
                int tempint = 1;
                foreach (string icecream in IceCreamOption)
                {
                    Console.WriteLine("{0}. {1}", tempint, icecream);
                    tempint++;
                }
                Console.WriteLine("Your choice: ");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {
                    Console.WriteLine("You have chosen {0}", IceCreamOption[option - 1]);
                }
                else if (option == 2)
                {
                    Console.WriteLine("You have chosen {0}", IceCreamOption[option - 1]);
                    Console.WriteLine("Would you like chocolate dipped cone? Y/N: ");
                    if (Console.ReadLine() == "Y")
                    {
                        premium = true;
                    }
                    else if (Console.ReadLine() == "N")
                    {
                        premium = false;
                    }
                }
                else if (option == 3)
                {
                    Console.WriteLine("You have chosen {0}", IceCreamOption[option - 1]);
                    Console.WriteLine("What flavour would you like?");
                    for (int i = 0; i < WaffleFlavourOption.Count; i++)
                    {
                        Console.WriteLine("{0}. {1}", i + 1, WaffleFlavourOption[i]);
                    }
                    Console.WriteLine("Your choice: ");


                }
                else
                {
                    Console.WriteLine("Enter only number 1 to 3.");
                }
            }
        }
        public bool isBirthday()
        {
            string birthday = Dob.ToString("MM/dd");
            if (birthday == DateTime.Now.ToString("MM/dd"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return "Name: " + Name + " Member ID: " + Memberid + " Date of Birth: " + Dob;
        }
    }
    //Class PointCard
    public class PointCard
    {
        private int points;
        private int punchCard;
        private string tier;
        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        public int PunchCard
        {
            get { return punchCard; }
            set { punchCard = value; }
        }
        public string Tier
        {
            get { return tier; }
            set { tier = value; }
        }
        public PointCard()
        {
            points = 0;
            PunchCard = 0;
            tier = "Ordinary";
        }
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
            tier = "Ordinary";
        }
        public void AddPoints(int point)
        {
            Points += point;
        }
        public void RedeemPoints(int deduct)
        {
            Points -= deduct;
        }
        public void Punch()
        {
            if (Points < 10)
            {
                PunchCard++;
            }
            else
            {
                Console.WriteLine("Free Ice Cream!");
                PunchCard = 0;
            }
        }
        public override string ToString()
        {
            return "Points: " + Points + "\tPunch Card Tally: " + PunchCard + "\tTier: " + Tier;
        }
    }

    //Class Flavour
    public class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public bool Premium
        {
            get { return premium; }
            set { premium = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        //Setting default values
        public Flavour()
        {
            type = "";
            premium = false;
            quantity = 0;
        }
        public Flavour(string type, bool premium, int quantity)
        {
            this.type = type;
            this.premium = premium;
            this.quantity = quantity;
        }
        public override string ToString()
        {
            return "Type: " + Type + " Premium: " + Premium + " Quantity: " + Quantity;
        }
    }
    //Class Topping 
    public class Topping
    {
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public Topping()
        {
            type = "";
        }
        public Topping(string type) { this.type = type; }
        //ToString
        public override string ToString()
        {
            return "Type: " + Type;
        }

    }
    // Ice Cream class
    public abstract class IceCream
    {
        // Setting attributes
        private string option;
        public string Option
        {
            get { return option; }
            set { option = value; }
        }
        private int scoops;
        public int Scoops
        {
            get { return scoops; }
            set { scoops = value; }
        }
        private List<Flavour> flavours;
        public List<Flavour> Flavours
        {
            get { return flavours; }
            set { flavours = value; }
        }
        private List<Topping> toppings;
        public List<Topping> Toppings
        {
            get { return toppings; }
            set { toppings = value; }
        }
        // Empty constructor
        public IceCream()
        {
            // Default to empty cup
            option = "cup";
            scoops = 0;
            flavours = new List<Flavour>();
            toppings = new List<Topping>();
        }
        // Main constructor
        public IceCream(string Option, int Scoops, List<Flavour> Flavours, List<Topping> Toppings)
        {
            option = Option;
            scoops = Scoops;
            flavours = Flavours;
            toppings = Toppings;
        }
        // Abstract method CalculatePrice()
        public abstract double CalculatePrice();
        // ToString()
        public override string ToString()
        {
            string returnStr = "Option: " + option + "\nScoops: " + Convert.ToString(scoops) + "\nFlavours: ";
            foreach (Flavour flavour in flavours)
            {
                // Lists out all the flavours stored in the List
                returnStr += "\n" + flavour.Type + $" x{flavour.Quantity}";
            }
            returnStr += "\nToppings: ";
            foreach (Topping topping in toppings)
            {
                // Lists out all the toppings in the List
                returnStr += "\n" + topping.Type;
            }
            return returnStr;
        }
    }
    //Cup
    public class Cup : IceCream
    {
        public Cup()
        {
            this.Option = "Cup";
            this.Scoops = 0;
            this.Flavours = new List<Flavour>();
        }
        public Cup(string Option, int Scoops, List<Flavour> Flavours, List<Topping> Toppings) : base(Option, Scoops, Flavours, Toppings)
        {
            this.Option = Option;
        }
        public override double CalculatePrice()
        {
            double basePrice = 0;
            List<string[]> data = Utils.GetInfo("options.csv", false);
            try
            {
                foreach (string[] filedata in data)
                {
                    Console.WriteLine(filedata[1]);
                    if (filedata[0] == this.Option && Convert.ToInt32(filedata[1]) == this.Scoops)
                    {
                        basePrice = Convert.ToDouble(filedata[4]);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                basePrice = 0;
                Console.WriteLine(e.ToString());
                return 0;
            }

            double toppingsPrice = Toppings.Count * 1; // Each topping costs $1
            double flavourPrice = 0;
            foreach (Flavour flavour in this.Flavours)
            {
                if (flavour.Premium == true)
                {
                    flavourPrice += 2;
                }
            }
            // Assuming scoop amount is already corrected to within 1-3 from other methods
            return basePrice + toppingsPrice + flavourPrice;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    //Cone
    public class Cone : IceCream
    {
        private bool dipped;
        public bool Dipped
        {
            get { return dipped; }
            set { dipped = value; }
        }
        public Cone()
        {
            this.Option = "Cone";
            this.Scoops = 0;
            this.Flavours = new List<Flavour>();
            this.Toppings = new List<Topping>();
            dipped = false;
        }
        public Cone(string Option, int Scoops, List<Flavour> Flavours, List<Topping> Toppings, bool Dipped) : base(Option, Scoops, Flavours, Toppings)
        {
            this.Dipped = Dipped;
        }
        public override double CalculatePrice()
        {
            double basePrice = 0;
            List<string[]> data = Utils.GetInfo("options.csv", false);
            foreach (string[] filedata in data)
            {
                if (filedata[0] == this.Option && Convert.ToInt32(filedata[1]) == this.Scoops && Convert.ToBoolean(filedata[2]) == this.Dipped)
                {
                    basePrice = Convert.ToDouble(filedata[4]);
                    break;
                }
            }
            double flavourPrice = 0;
            foreach (Flavour flavour in this.Flavours)
            {
                if (flavour.Premium == true)
                {
                    flavourPrice += 2;
                }
            }
            double toppingsPrice = Toppings.Count * 1; // Each topping costs $1
            // Assuming scoop amount is already corrected to within 1-3 from other methods
            return basePrice + toppingsPrice + flavourPrice;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    //Waffle
    public class Waffle : IceCream
    {
        private string waffleFlavour;
        public string WaffleFlavour
        {
            get { return waffleFlavour; }
            set { waffleFlavour = value; }
        }
        public Waffle()
        {
            this.Option = "Waffle";
            this.Scoops = 0;
            this.Flavours = new List<Flavour>();
            this.Toppings = new List<Topping>();
            waffleFlavour = "None";
        }
        public Waffle(string Option, int Scoops, List<Flavour> Flavours, List<Topping> Toppings, string WaffleFlavour) : base(Option, Scoops, Flavours, Toppings)
        {
            waffleFlavour = WaffleFlavour;
        }
        public override double CalculatePrice()
        {
            double basePrice = 0;
            List<string[]> data = Utils.GetInfo("options.csv", false);
            foreach (string[] filedata in data)
            {
                if (filedata[0] == this.Option && Convert.ToInt32(filedata[1]) == this.Scoops && filedata[3] == this.WaffleFlavour)
                {
                    basePrice = Convert.ToDouble(filedata[4]);
                    break;
                }
            }
            double flavourPrice = 0;
            foreach (Flavour flavour in this.Flavours)
            {
                if (flavour.Premium == true)
                {
                    flavourPrice += 2;
                }
            }
            double toppingsPrice = Toppings.Count * 1; // Each topping costs $1
            // Assuming scoop amount is already corrected to within 1-3 from other methods
            return basePrice + toppingsPrice + flavourPrice;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    // Class Order
    public class Order
    {
        // Setting Attributes
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime timeReceived;
        public DateTime TimeReceived
        {
            get { return timeReceived; }
            set { timeReceived = value; }
        }
        private DateTime? timeFulfilled;
        public DateTime? TimeFulfilled
        {
            get { return timeFulfilled; }
            set { timeFulfilled = value; }
        }
        private List<IceCream> iceCreamList;
        public List<IceCream> IceCreamList
        {
            get { return iceCreamList; }
            set { iceCreamList = value; }
        }
        // Empty Contructor
        public Order()
        {
            id = 0;
            timeReceived = DateTime.Now;
            timeFulfilled = null;
            iceCreamList = new List<IceCream>();
        }
        // Regular Constructor based on chart (Order(int, DateTime))
        public Order(int Id, DateTime TimeReceived)
        {
            id = Id;
            timeReceived = TimeReceived;
            timeFulfilled = null;
            iceCreamList = new List<IceCream>();
        }
        // Editing Ice Creams in order
        // Modifying ice cream

        public void ModifyIceCream(int index)
        {
            if (index <= 0 || index > IceCreamList.Count)
            {
                Console.WriteLine("Not a valid selection.");
            }
            else
            {
                bool loop = true;
                while (loop)
                {
                    try
                    {
                        // Get ice cream
                        IceCream icecream = iceCreamList[index - 1];
                        // Print menu
                        Console.Write("============================\n" +
                                      "===== Modify ice cream =====\n" +
                                      "============================\n" +
                                      "Options:\n" +
                                      "[1] Change ice cream options / scoops\n" +
                                      "[2] Change flavours\n" +
                                      "[3] Change toppings\n" +
                                      "[0] Exit\n" +
                                      "Enter option: ");
                        // Get input
                        string? userinput = Console.ReadLine();
                        // Switch for options
                        switch (userinput)
                        {
                            // Option 1
                            case "1":
                                // Show current option
                                Console.WriteLine($"Current option is: {icecream.Option}.");
                                List<string[]> data = Utils.GetInfo("options.csv", true);
                                string[] newSelection = Utils.Readinfo(data, null);
                                List<Flavour> flavours = new List<Flavour>();
                                switch (newSelection[0])
                                {
                                    case "Cup":
                                        Cup newCup = new Cup(newSelection[0], Convert.ToInt32(newSelection[1]), icecream.Flavours, icecream.Toppings);
                                        iceCreamList[index - 1] = newCup;
                                        break;
                                    case "Cone":
                                        Cone newCone = new Cone(newSelection[0], Convert.ToInt32(newSelection[1]), icecream.Flavours, icecream.Toppings, Convert.ToBoolean(newSelection[2]));
                                        iceCreamList[index - 1] = newCone;
                                        break;
                                    case "Waffle":
                                        Waffle newWaffle = new Waffle(newSelection[0], Convert.ToInt32(newSelection[1]), icecream.Flavours, icecream.Toppings, newSelection[3]);
                                        iceCreamList[index - 1] = newWaffle;
                                        break;
                                    default:
                                        throw new Exception("Error");
                                }
                                break;
                            // Modify Flavours
                            case "2":
                                // Print all flavours
                                Console.WriteLine("Flavours:");
                                // Get all flavours in a line with quantity
                                string returnStr = "";
                                // Count for remove flavour
                                int count = 1;
                                foreach (Flavour flavour in icecream.Flavours)
                                {
                                    // Lists out all the flavours stored in the List
                                    returnStr += $"\n[{count}]" + flavour.Type + $" x{flavour.Quantity}";
                                    count++;
                                }
                                // Print all flavours
                                Console.WriteLine(returnStr + "\n");
                                // Modify flavours
                                Console.Write("Modify Flavours:\n" +
                                    "[1] Add flavour\n" +
                                    "[2] Remove flavour\n" +
                                    "[3] View current flavours\n" +
                                    "[0] Cancel\n" +
                                    "Enter Option: ");
                                userinput = Console.ReadLine();
                                switch (userinput)
                                {
                                    // Add flavour
                                    case "1":
                                        Console.WriteLine("Choose a flavour to add: ");
                                        // Get values and data
                                        List<string[]> flavourData = Utils.GetInfo("flavours.csv", true);
                                        string[] fullFlavour = Utils.Readinfo(flavourData, null);
                                        // Create new flavour
                                        bool check = false;
                                        foreach (Flavour flavour in icecream.Flavours)
                                        {
                                            if (flavour.Type == fullFlavour[0])
                                            {
                                                flavour.Quantity += 1;
                                                check = true;
                                            }
                                        }
                                        // Fail check
                                        if (!check)
                                        {
                                            Flavour newFlavour = new Flavour(fullFlavour[0], Convert.ToBoolean(fullFlavour[2]), 1);
                                            icecream.Flavours.Add(newFlavour);
                                        }
                                        break;
                                    case "2":
                                        Console.Write("Choose a flavour to remove (Eg: \"Chocolate\"): ");
                                        userinput = Console.ReadLine();
                                        foreach (Flavour flavour in icecream.Flavours)
                                        {
                                            if (flavour.Type.ToUpper() == userinput.ToUpper())
                                            {
                                                Console.WriteLine("Amount to remove: ");
                                                string? amtRemove = Console.ReadLine();
                                                int amt = Convert.ToInt32(amtRemove);
                                                if (amt > flavour.Quantity)
                                                {
                                                    Console.WriteLine("Removing all flavours of this type.");
                                                    flavour.Quantity = 0;
                                                    icecream.Flavours.Remove(flavour);
                                                    break;
                                                }
                                                else if (amt < 0)
                                                {
                                                    Console.WriteLine("Invalid amount.");
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Removing {amt}.");
                                                    flavour.Quantity -= amt;
                                                    Console.WriteLine($"New flavour quantity: {flavour.Quantity}");
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    case "3":
                                        // Print all flavours
                                        Console.WriteLine("\nFlavours:");
                                        Console.WriteLine(returnStr + "\n");
                                        break;
                                    case "0":
                                        Console.WriteLine("Cancelled.");
                                        break;
                                    default:
                                        Console.WriteLine("Not an option. Cancelled.");
                                        break;
                                }
                                break;
                            case "3":
                                // Print all toppings
                                Console.WriteLine("Toppings:");
                                // Get all toppings
                                string toppingStr = "";
                                // Count for remove topping
                                int removeCount = 1;
                                foreach (Topping toppings in icecream.Toppings)
                                {
                                    // Lists out all the flavours stored in the List
                                    toppingStr += $"\n[{removeCount}]" + toppings.Type;
                                    removeCount++;
                                }
                                //Print all toppings
                                Console.WriteLine(toppingStr + "\n");
                                // Modify toppings
                                Console.Write("Modify Toppings:\n" +
                                    "[1] Add toppings\n" +
                                    "[2] Remove toppings\n" +
                                    "[3] View all toppings\n" +
                                    "[0] Cancel\n" +
                                    "Enter Option: ");
                                userinput = Console.ReadLine();
                                switch (userinput)
                                {
                                    case "1":
                                        List<string[]> toppingData = Utils.GetInfo("toppings.csv", true);
                                        string[] fullData = Utils.Readinfo(toppingData, null);
                                        Topping addTopping = new Topping(fullData[0]);
                                        icecream.Toppings.Add(addTopping);
                                        break;
                                    case "2":
                                        // Print all toppings
                                        Console.WriteLine(toppingStr + "\n");
                                        Console.Write("Choose topping to remove: ");
                                        userinput = Console.ReadLine();
                                        try
                                        {
                                            // Check if empty input
                                            if (userinput != null)
                                            {
                                                // Convert to a choice
                                                int choices = Convert.ToInt32(userinput);
                                                // Check if valid option
                                                if (choices < 0 || choices > (icecream.Toppings.Count))
                                                {
                                                    // If invalid throw error
                                                    throw new Exception("Invalid option.");
                                                }
                                                icecream.Toppings.RemoveAt(choices - 1);
                                            }
                                            else
                                            {
                                                throw new Exception("Option cannot be blank.");
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Error, cancelling.");
                                            return;
                                        }
                                        break;
                                    case "3":
                                        Console.WriteLine("\nToppings: ");
                                        // Print all toppings
                                        Console.WriteLine(toppingStr + "\n");
                                        break;
                                    case "0":
                                        Console.WriteLine("Cancelled.");
                                        break;
                                    default:
                                        Console.WriteLine("Not an option. Cancelled.");
                                        break;
                                }
                                break;
                            case "0":
                                loop = false;
                                Console.WriteLine("Exiting.");
                                break;
                            default:
                                Console.WriteLine("Invalid input, try again.");
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Error.");
                    }
                }
            }
        }
        // Add ice cream
        public void AddIceCream(IceCream icecream)
        {
            // Ice cream option
            this.iceCreamList.Add(icecream);
        }
        // Remove ice cream
        public void DeleteIceCream(int indexing)
        {
            iceCreamList.RemoveAt(indexing - 1);
        }
        // Calculate Total
        public double CalculateTotal()
        {
            double totalAmt = 0;
            // For every ice cream in ice cream list
            foreach (IceCream iceCream in iceCreamList)
            {
                // Add the price of the ice cream
                totalAmt += iceCream.CalculatePrice();
            }
            return totalAmt;
        }
        // ToString
        public override string ToString()
        {
            string startStr = "Order:\n" +
                               "====================\n";
            int count = 1;
            foreach (IceCream iceCream in iceCreamList)
            {
                string toppingStr = "";
                foreach (Topping topping in iceCream.Toppings)
                {
                    toppingStr += $" - {topping.Type}\n";
                }
                string flavourStr = "";
                foreach (Flavour flavour in iceCream.Flavours)
                {
                    double flavourPrice = 0;
                    if (flavour.Premium == true)
                    {
                        flavourPrice = flavour.Quantity * 2;
                    }
                    flavourStr += $"{$" - {flavour.Type,-13} x{flavour.Quantity,-3}",-20}${flavourPrice,-5}\n";
                }
                startStr += $"[ {count,-2}] {iceCream.Option,-10}{"$" + iceCream.CalculatePrice(),10}\n" +
                    $" - {iceCream.Scoops} scoops\n" +
                    $"{toppingStr}{flavourStr}\n";
                count++;
            }
            startStr += "====================\n" +
                $"Total: ${this.CalculateTotal()}";
            return startStr;
        }
    }

    // Utilities class
    public static class Utils
    {
        // Gets info from csv, and also rpints options menu
        public static List<string[]> GetInfo(string csvfile, bool printMenu)
        {
            // Create new empty list of string arrays for both
            List<string[]> returnInfo = new List<string[]> { };
            List<string[]> values = new List<string[]> { };
            try
            {
                // Reads file
                string[] getInfo = File.ReadAllLines(csvfile);
                if (getInfo.Length < 1)
                {
                    throw new Exception("Insufficient File Length.");
                }
                // Split the values into respective info
                string[] keys = getInfo[0].Split(",");
                foreach (string line in getInfo[1..^0])
                {
                    // Split info
                    string[] info = line.Split(",");
                    values.Add(info);
                }
                // Adds to return info
                returnInfo.Add(keys);
                returnInfo.AddRange(values);
                // Prints menu if true
                if (printMenu)
                {
                    string keyDisplay = "No.   " + string.Join(",", keys.Select(k => $"{k}"));
                    string valDisplay = "";
                    for (int i = 0; i < values.Count; i++)
                    {
                        valDisplay += $"[ {i + 1,-2}] " + string.Join(",", values[i].Select(k => $"{k}")) + "\n";
                    }
                    Console.WriteLine(keyDisplay);
                    Console.WriteLine(valDisplay);
                }
                return returnInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Error reading file");
                Console.WriteLine(returnInfo.ToString());
                return returnInfo;
            }
        }
        public static string[] Readinfo(List<string[]> data, int? choice)
        {
            string? userinput = null;
            if (choice == null)
            {
                Console.Write("Enter option: ");
                userinput = Console.ReadLine();
            }
            else
            {
                // Without moving too much code
                userinput = Convert.ToString(choice);
            }
            try
            {
                // Check if empty input
                if (userinput != null)
                {
                    // Convert to a choice
                    choice = Convert.ToInt32(userinput);
                    // Check if valid option
                    if (choice < 0 || choice > (data.Count - 1))
                    {
                        // If invalid throw error
                        throw new Exception("Invalid option.");
                    }
                    // Select the given item
                    // nullable ints are weird so here's a coversion
                    int choiceIndex = choice.Value;
                    string[] selectedItem = data[choiceIndex];
                    return selectedItem;
                }
                else
                {
                    throw new Exception("Option cannot be blank.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new string[] { };
            }
        }
    }
}