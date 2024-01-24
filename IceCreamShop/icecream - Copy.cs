using System.Security.Cryptography.X509Certificates;
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
namespace IceCreamShop{
    //Class Customer
    public class Customer
    {
        private string name;
        private int memberid;
        private DateTime dob;
        private Order ?currentOrder;
        private PointCard rewards;
        public string Name {  get { return name; } set {  name = value; } }
        public int Memberid { get {  return memberid; } set {  memberid = value; } }
        public DateTime Dob { get {  return dob; } set {  dob = value; } }
        public Order CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; }
        }
        public List<Order> orderHistory { get; set; }
            = new List<Order>();
        public PointCard Rewards { get { return rewards; } set {  rewards = value; } }
        public Customer() { }
        public Customer(string Name, int Memberid, DateTime Dob)
        {
            Name = name;
            Memberid = memberid;
            Dob = dob;
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
        }/*
            Console.WriteLine("How many scoops? (1 to 3) : ");
            int scoops = Convert.ToInt32(Console.ReadLine());

            string[] flavourfile = File.ReadAllLines("flavours.csv");
            for(int i = 1; i < flavourfile.Length-1; i++)
            {
                stringflavourfile.split   (",");
            }
            while (true)
            {                
                Console.WriteLine("Enter your choice: ");
                int flavour = Convert.ToInt32(Console.ReadLine());
                string temp;
                switch (flavour)
                {
                    case 1:
                        temp = "Vanilla";
                        premium = false;
                        break;
                    case 2:
                        temp = "Chocolate";
                        premium = false;
                        break;
                    case 3:
                        temp = "Strawberry";
                        premium = false;
                        break;
                    case 4:
                        temp = "Durian";
                        premium = true;
                        break;
                    case 5:
                        temp = "Sea Salt";
                        premium = true;
                        break;
                    case 6:
                        temp = "Ube";
                        premium = true;
                        break;
                }
                Console.WriteLine("Do you want to add flavour? Y/N: ");
                if (Console.ReadLine() == "Y")
                {
                    continue;
                }
                else if (Console.ReadLine() == "N")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("You have entered an invalid option.");
                }
            }
            List<string> 


        }*/
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
        }
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
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
            return "Points: " + Points + "\tPunch Card Tally: " + PunchCard + "\tTier: "+Tier;
        }
    }

    //Class Flavour
    public class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;
        public string Type {
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
        public Flavour() {
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
        public Topping() {
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
    public abstract class IceCream{
        // Setting attributes
        private string option;
        public string Option{
            get { return option; }
            set { option = value; }
        }
        private int scoops;
        public int Scoops{
            get { return scoops; }
            set { scoops = value; }
        }
        private List<Flavour> flavours;
        public List<Flavour> Flavours{
            get { return flavours; }
            set { flavours = value; }
        }
        private List<Topping> toppings;
        public List<Topping> Toppings{
            get { return toppings; }
            set { toppings = value;}
        }
        // Empty constructor
        public IceCream(){
            // Default to empty cup
            option = "cup";
            scoops = 0;
            flavours = new List<Flavour>();
            toppings = new List<Topping>();
        }
        // Main constructor
        public IceCream(string Option, int Scoops, List<Flavour> Flavours, List<Topping> Toppings){
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
            string returnStr = "Option: "+option+"\nScoops: "+Convert.ToString(scoops)+"\nFlavours: ";
            foreach (Flavour flavour in flavours){
                // Lists out all the flavours stored in the List
                returnStr += "\n"+flavour.Type+$" x{flavour.Quantity}";
            }
            returnStr += "\nToppings: ";
            foreach (Topping topping in toppings){
                // Lists out all the toppings in the List
                returnStr += "\n"+topping.Type;
            }
            return returnStr;
        }
    }
    //Cup
    public class Cup : IceCream
    {
        public Cup() {
            this.Option = "Cup";
            this.Scoops = 0;
            this.Flavours = new List<Flavour>();
        }
        // Why does the table require another input for option ;/
        public Cup(string Option, int Scoops, List<Flavour> Flavours, List<Topping> Toppings) : base(Option, Scoops, Flavours, Toppings)
        {
            this.Option = Option;
        }
        public override double CalculatePrice()
        {
            double basePrice = 0;
            string[] normal = {""};
            // Assuming you can't order only toppings
            if (this.Scoops <= 0){
                Console.WriteLine("Cup cannot be empty");
            }
            else{
                foreach (Flavour flavour in Flavours){
                    if (flavour.Premium){
                        // +$2 per premium
                        basePrice += 2;
                    }
                }
                switch (this.Scoops){
                    case 1:
                        basePrice += 4;
                        break;
                    case 2:
                        basePrice += 5.5;
                        break;
                    case 3:
                        basePrice += 6.5;
                        break;
                    default:
                    // In case more than 4 scoops
                        Console.WriteLine("Cannot have more than 3 scoops.");
                        basePrice = 0;
                        return basePrice;
                }
                double toppingsPrice = Toppings.Count * 1; // Each topping costs $1

                return basePrice + toppingsPrice;
            }
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
        public Cone() {
            this.Option = "Cone";
            this.Scoops = 0;
            this.Flavours = new List<Flavour>();
            this.Toppings = new List<Topping>();
            dipped = false;
         }
        public Cone(string Option, int Scoops, List<Flavour> Flavours, List<Topping> Toppings,bool Dipped) : base(Option, Scoops, Flavours, Toppings)
        {
            this.Dipped = Dipped;
        }
        public override double CalculatePrice()
        {
            double basePrice = 0;

            if (Scoops == 1)
            {
                basePrice = 4.00;
            }
            else if (Scoops == 2)
            {
                basePrice = 5.50;
            }
            else if (Scoops == 3)
            {
                basePrice = 6.50;
            }
            if (dipped)
            {
                basePrice += 2;
            }
            basePrice += Toppings.Count * 1;
            return basePrice;
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
        public Waffle() {
            this.Option = "Waffle";
            this.Scoops = 0;
            this.Flavours = new List<Flavour>();
            this.Toppings = new List<Topping>();
            waffleFlavour = "None";
         }
        public Waffle(string Option, int Scoops, List<Flavour> Flavours, List<Topping> Toppings, string WaffleFlavour) : base(Option, Scoops, Flavours, Toppings)
        {
            this.WaffleFlavour= WaffleFlavour;
        }
        public override double CalculatePrice()
        {
            double basePrice = 0;

            if (Scoops == 1)
            {
                basePrice = 7.00;
            }
            else if (Scoops == 2)
            {
                basePrice = 8.50;
            }
            else if (Scoops == 3)
            {
                basePrice = 9.50;
            }
            // Implement +$3 for flavoured waffle
            if (WaffleFlavour != "None")
            {
                basePrice += 3;
            }
            double toppingsPrice = Toppings.Count * 1; // Each topping costs $1
            return basePrice + toppingsPrice;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    // Class Order
    public class Order{
        // Setting Attributes
        private int id;
        public int Id{
            get { return id; }
            set { id = value; }
        }
        private DateTime timeReceived;
        public DateTime TimeReceived{
            get { return timeReceived; }
            set { timeReceived = value; }
        }
        private DateTime? timeFulfilled;
        public DateTime? TimeFulfilled{
            get { return timeFulfilled; }
            set { timeFulfilled = value; }
        }
        private List<IceCream> iceCreamList;
        public List<IceCream> IceCreamList{
            get { return iceCreamList; }
            set { iceCreamList = value; }
        }
        // Empty Contructor
        public Order(){
            id = 0;
            timeReceived = DateTime.Now;
            timeFulfilled = null;
            iceCreamList = new List<IceCream>();
        }
        // Regular Constructor based on chart (Order(int, DateTime))
        public Order(int Id, DateTime TimeReceived){
            id = Id;
            timeReceived = TimeReceived;
            timeFulfilled = null;
            iceCreamList = new List<IceCream>();
        }
        // Editing Ice Creams in order
        // Modifying ice cream

        public void ModifyIceCream(int index)
        {
            if (index <= 0 || index > IceCreamList.count)
            {
                Console.WriteLine("Not a valid selection.");
            }
            else
            {
                try
                {
                    bool loop = true;
                    while (loop)
                    {
                        // Get ice cream
                        Icecream icecream = iceCreamList[index - 1];
                        // Print menu
                        Console.Write("============================\n" +
                                      "===== Modify ice cream =====\n" +
                                      "============================\n" +
                                      "options:\n" +
                                      "[1] Change ice cream option\n" +
                                      "[2] Change number of scoops\n" +
                                      "[3] Change flavours\n" +
                                      "[4] Change toppings\n" +
                                      "[0] Exit\n" +
                                      "Enter option: ");
                        // Get input
                        string userinput = Console.ReadLine();
                        // Switch for options
                        switch (userinput)
                        {
                            // Option 1
                            case "1":
                                // Show current option
                                Console.WriteLine($"Current option is: {icecream.Option}.");
                                // Display menu
                                Console.Write("============================\n" +
                                              "======= Change option ======\n" +
                                              "============================\n" +
                                              "Options:\n" +
                                              "[1] Cup\n" +
                                              "[2] Cone\n" +
                                              "[3] Waffle\n" +
                                              "[0] Exit\n" +
                                              "Enter option: ");
                                userinput = Console.ReadLine();
                                switch (userinput)
                                {
                                    // Change to cup
                                    case "1":
                                        // Check if option is already cup
                                        if (icecream.Option == "Cup")
                                        {
                                            Console.WriteLine("Option is already cup.");
                                        }
                                        else
                                        {
                                            // Change to cup
                                            icecream.Option = "Cup";
                                            Console.WriteLine("Option changed to cup.");
                                        }
                                        // Get values from ice cream
                                        Cup cup = icecream;
                                        iceCreamList[index - 1] = cup;
                                        break;
                                    // Change to Cone
                                    case "2":
                                        // Check if cone
                                        bool dipped = false;
                                        if (icecream.Option == "Cone")
                                        {
                                            Console.WriteLine("Option is already cone.");
                                            // Downcasting is risky so I'll just take the value again
                                            Cone cone = iceCreamList[index - 1];
                                        }
                                        else
                                        {
                                            // Display change to cone
                                            // Copy values of ice cream and put it inside the cone
                                            // Option set as cone, undipped
                                            // Downcasting is risky
                                            Cone cone = new Cone("Cone", icecream.Scoops, icecream.Flavours, icecream.Toppings, false);
                                            Console.WriteLine("Option changed to cone.");
                                        }
                                        // Get dipped option
                                        Console.Write("Do you want to chocolate-dip the cone? [y/n]: ");
                                        userinput = Console.ReadLine().ToUpper();
                                        switch (userinput.ToLower())
                                        {
                                            case "Y":
                                                // Change to dipped
                                                cone.Dipped = true;
                                                break;
                                            case "N":
                                                // Change 
                                                cone.Dipped = false;
                                                break;
                                            default:
                                                Console.WriteLine("No change will be made.");
                                        }
                                        // Print dip status
                                        Console.WriteLine($"Dipped: {cone.Dipped}.");
                                        break;
                                    // Change to Waffle
                                    case "3":
                                        // Check if Waffle
                                        if (icecream.Option == "Waffle")
                                        {
                                            console.writeline("Option is already waffle.");
                                        }
                                        else
                                        {
                                            icecream.Option = "Waffle";
                                            console.writeline("Option changed to waffle.");
                                        }
                                        // Downcast for now (temporary)
                                        Waffle waffle = icecream;
                                        // Print waffle flavours menu
                                        Console.WriteLine("Change waffle flavour: \n" +
                                            "[1] Red velvet (+$3)\n" +
                                            "[2] Charcoal (+$3)\n" +
                                            "[3] Pandan (+$3)\n" +
                                            "[4] Default");
                                        Console.Write("Enter option: ");
                                        userinput = Console.ReadLine();
                                        switch (userinput)
                                        {
                                            // Change to red velvet
                                            case "1":
                                                waffle.WaffleFlavour = "Red velvet";
                                                break;
                                            // Change to charcoal
                                            case "2":
                                                waffle.WaffleFlavour = "Charcoal";
                                                break;
                                            // Change to pandan
                                            case "3":
                                                waffle.WaffleFlavour = "Pandan";
                                                break;
                                            // Default flavour (none)
                                            case "4":
                                                waffle.WaffleFlavour = "None";
                                                break;
                                            // No change
                                            default:
                                                Console.WriteLine("No change will be made.");
                                        }
                                        // Display new flavour
                                        console.writeline($"Your waffle is now {waffle.WaffleFlavour}.");
                                        break;
                                }
                                break;
                            // Change number of scoops
                            case "2":
                                Console.WriteLine("How many scoops do you want?: ");
                                userinput = Console.ReadLine();
                                // 1 by 1 because easy solution
                                switch (userinput)
                                {
                                    case "1":
                                        icecream.Scoops = 1;
                                        Console.WriteLine("Scoop number changed to 1.");
                                        break;
                                    case "2":
                                        icecream.Scoops = 2;
                                        Console.WriteLine("Scoop number changed to 2.");
                                        break;
                                    case "3":
                                        icecream.Scoops = 3;
                                        Console.WriteLine("Scoop number changed to 3.");
                                        break;
                                    default:
                                        Console.WriteLine("you can only have 1, 2, or 3 scoops.");
                                        break;
                                }
                                break;
                            // Change Flavours
                            // DISCLAIMER: If it turns out I need to match
                            // number of flavours to number of scoops I'm going to scream
                            // I will also just break every option into different methods
                            // Which have submethods because this is too long
                            case "3":
                                // Print all flavours
                                Console.WriteLine("Flavours:");
                                // Get all flavours in a line with quantity
                                string returnStr = "";
                                // Count for remove flavour
                                string count = 1;
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
                                    "[1] Add flavour" +
                                    "[2] Remove flavour" +
                                    "[0] Cancel" +
                                    "Enter Option: ");
                                userinput = Console.ReadLine();
                                switch (userinput)
                                {
                                    // Add flavour
                                    case "1":
                                        Console.Write("What flavour would you like to add?: ");
                                        userinput = Console.ReadLine();
                                        // Check empty input
                                        if (userinput == "" || userinput == null)
                                        {
                                            Console.WriteLine("Cancelled.");
                                        }
                                        else
                                        {
                                            // Check if flavour added is already in flavour list
                                            bool check = false;
                                            foreach (Flavour flavour in icecream.Flavours)
                                            {
                                                // If flavour in flavour list
                                                if (flavour.Type.ToLower() == newFlav.ToLower())
                                                {
                                                    // Increase quantity
                                                    flavour.Quantity += 1;
                                                    check = true;
                                                    break;
                                                }
                                            }
                                            // If flavour not in flavour list
                                            if (check == false)
                                            {
                                                // Read flavours.csv to see flavours info
                                                bool check2 = false;
                                                string[] flavourList = File.ReadAllLines("flavours.csv");
                                                // For each flavour in flavours.csv
                                                foreach (string line in flavourList)
                                                {
                                                    // Split info
                                                    string[] info = line.split(",");
                                                    // If flavour name matches
                                                    if (userinput.ToLower() == info[0].ToLower())
                                                    {
                                                        // Add the flavour
                                                        icecream.Flavours.Add(new Flavour(info[0], bool.Parse(info[2]),1));
                                                        check2 = true;
                                                        break;
                                                    }
                                                }
                                                // If flavour not in flavours.csv too = not an available flavour
                                                if (check2 == false)
                                                {
                                                    Console.WriteLine("Not an available flavour.");
                                                }
                                            }
                                        }
                                    // Remove flavour
                                    case "2":
                                        Console.Write("Select which flavour to remove: ");
                                        userinput = Console.ReadLine();
                                        // Check empty input
                                        if (string.IsNullOrEmpty(userinput))
                                        {
                                            Console.WriteLine("Cancelling.");
                                        }
                                        else
                                        {
                                            // try block to skip most other data validations here
                                            // For example trying to convert to int if its not a number
                                            try
                                            {
                                                int indexing = Convert.ToInt32(userinput);
                                                // Check if the indexing selection is within bounds
                                                if (indexing < 0 || indexing > icecream.Flavours.Count)
                                                {
                                                    Console.Write("Not a valid selection.");
                                                }
                                                else
                                                {
                                                    // Select flavour
                                                    Flavour flavourSelected = icecream.Flavours[indexing-1];
                                                    // Check flavour quantity
                                                    if (flavourSelected.Quantity > 1)
                                                    {
                                                        Console.Write($"How many {flavourSelected} do you want to remove? (1-{flavourSelected.Quantity}): ");
                                                        int removeQuantity = Convert.ToInt32(Console.ReadLine());
                                                        // Check number of removed
                                                        if (removeQuantity<=0 || removeQuantity > flavourSelected.Quantity)
                                                        {
                                                            Console.WriteLine("Please remove a proper amount.");
                                                        }
                                                        else
                                                        {
                                                            // Reduce quantity
                                                            flavourSelected.Quantity -= removeQuantity;
                                                            // If less than or equals zero remove whole thing (less than because in case of a bug)
                                                            if (flavourSelected <= 0)
                                                            {
                                                                // Remove flavour
                                                                icecream.Flavours.RemoveAt(indexing-1);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            // Conversion error and input errors go here
                                            catch
                                            {
                                                Console.WriteLine("Invalid input.");
                                            }
                                        }
                                        break;
                                    // No case 3 because any other input will also
                                    // just continue the program.
                                    default:
                                        Console.WriteLine("Continuing.");
                                        break;
                                }
                                break;
                            // Change toppings
                            case "4":
                                // Print all toppings
                                Console.WriteLine("Toppings:");
                                // Get all toppings
                                string returnStr = "";
                                // Count for remove topping
                                string count = 1;
                                foreach (Topping toppings in icecream.Toppings)
                                {
                                    // Lists out all the flavours stored in the List
                                    returnStr += $"\n[{count}]" + toppings.Type;
                                    count++;
                                }
                                // Print all toppings
                                Console.WriteLine(returnStr + "\n");
                                // Modify toppings
                                Console.Write("Modify Toppings:\n" +
                                    "[1] Add toppings" +
                                    "[2] Remove toppings" +
                                    "[0] Cancel" +
                                    "Enter Option: ");
                                userinput = Console.ReadLine();
                                switch (userinput)
                                {
                                    // Add Topping
                                    case "1":
                                        // Check empty input
                                        if (userinput == "" || userinput == null)
                                        {
                                            Console.WriteLine("Cancelled.");
                                        }
                                        else
                                        {
                                            // Check if topping added is already in topping list
                                            bool check = false;
                                            foreach (Topping topping in icecream.Toppings)
                                            {
                                                // If flavour in flavour list
                                                if (topping.Type.ToLower() == userinput.ToLower())
                                                {
                                                    // Print that it's already in
                                                    Console.WriteLine($"{topping.Type} is already");
                                                    break;
                                                }
                                            }
                                            // If topping not in topping list
                                            if (check == false)
                                            {
                                                // Read toppings.csv to see toppings info
                                                bool check2 = false;
                                                string[] toppingsList = File.ReadAllLines("toppings.csv");
                                                // For each topping in toppings.csv
                                                foreach (string line in toppingsList)
                                                {
                                                    // Split info
                                                    string[] info = line.split(",");
                                                    // If topping name matches
                                                    if (userinput.ToLower() == info[0].ToLower())
                                                    {
                                                        // Add the topping
                                                        icecream.Toppings.Add(new Topping(info[0]));
                                                        check2 = true;
                                                        break;
                                                    }
                                                }
                                                // If topping not in toppings.csv too = not an available topping
                                                if (check2 == false)
                                                {
                                                    Console.WriteLine("Not an available topping.");
                                                }
                                            }
                                        }
                                        break;
                                    // Remove topping
                                    case "2":
                                        Console.Write("Select which topping to remove: ");
                                        userinput = Console.ReadLine();
                                        // Check empty input
                                        if (string.IsNullOrEmpty(userinput))
                                        {
                                            Console.WriteLine("Cancelling.");
                                        }
                                        else
                                        {
                                            // try block to skip most other data validations here
                                            // For example trying to convert to int if its not a number
                                            try
                                            {
                                                int indexing = Convert.ToInt32(userinput);
                                                // Check if the indexing selection is within bounds
                                                if (indexing < 0 || indexing > icecream.Toppings.Count)
                                                {
                                                    Console.Write("Not a valid selection.");
                                                }
                                                else
                                                {
                                                    // Select topping
                                                    Topping toppingSelected = icecream.Toppings[indexing - 1];
                                                    icecream.Toppings.Remove(toppingSelected);
                                                }
                                            }
                                            // Conversion error and input errors go here
                                            catch
                                            {
                                                Console.WriteLine("Invalid input.");
                                            }
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("Continuing");
                                        break;
                                }
                                break;
                            case "0":
                                Console.WriteLine("Exiting.");
                                loop = false;
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        // Add ice cream
        public void AddIceCream(IceCream icecream)
        {
            // Ice cream option
            iceCreamList.Add(icecream);

            // This is for the menu ;/
            /*
            Console.Write("Enter ice cream option (Cup, Cone, Waffle): ");
            try
            {
                string userinput = Console.ReadLine();
                string ICoption = userinput.ToUpper();
                switch (ICoption)
                {
                    case "CUP":
                        string Option = "Cup";
                        break;
                    case "CONE":
                        string Option = "Cone";
                        break;
                    case "WAFFLE":
                        string Option = "Waffle";
                        break;
                    default:
                        // Send to catch
                        throw new CreationFailException("Input not valid.");
                }
                // Number of scoops
                Console.Write("Enter number of scoops: ");
                userinput = Console.ReadLine();
                if (userinput == "1" || userinput == "2" || userinput == "3")
                {
                    int Scoops = Convert.ToInt32(userinput);
                }
                else
                {
                    // Go to catch
                    throw new ScoopNumberException("There can only be 1, 2 or 3 scoops.");
                }
                // Flavours
                Console.Write("Enter number of flavours: ");
                userinput = Console.ReadLine();
                int numberFlavours = Convert.ToInt32(userinput);
                List<Flavour> flavours = new List<Flavour>();
                for (int i = 0; i < numberFlavours; i++)
                {
                    Console.Write("What flavour would you like to add?: ");
                    userinput = Console.ReadLine();
                    // Check empty input
                    if (userinput == "" || userinput == null)
                    {
                        // Cancels creation
                        throw new cancelException("Cancelled.");
                    }
                    else
                    {
                        // Check if flavour added is already in flavour list
                        bool check = false;
                        foreach (Flavour flavour in flavours)
                        {
                            // If flavour in flavour list
                            if (flavour.Type.ToLower() == userinput.ToLower())
                            {
                                // Increase quantity
                                flavour.Quantity += 1;
                                check = true;
                                break;
                            }
                        }
                        // If flavour not in flavour list
                        if (check == false)
                        {
                            // Read flavours.csv to see flavours info
                            bool check2 = false;
                            string[] flavourList = File.ReadAllLines("flavours.csv");
                            // For each flavour in flavours.csv
                            foreach (string line in flavourList)
                            {
                                // Split info
                                string[] info = line.split(",");
                                // If flavour name matches
                                if (userinput.ToLower() == info[0].ToLower())
                                {
                                    // Add the flavour
                                    flavours.Add(new Flavour(info[0], bool.Parse(info[2]), 1));
                                    check2 = true;
                                    break;
                                }
                            }
                            // If flavour not in flavours.csv too = not an available flavour
                            if (check2 == false)
                            {
                                // This is ugly but its a quick solution
                                // Exits loop and cancels creation
                                throw new flavourException("Not an available flavour.");
                            }
                        }
                    }
                }
                // Toppings
                Console.Write("Enter number of toppings: ");
                userinput = Console.ReadLine();
                List<Topping> toppings = new List<Topping>();
                if (Convert.ToInt32(userinput) > 3 || Convert.ToInt32(userinput) < 0)
                {
                    throw new cancelException("Cancelled.");
                }
                else
                {
                    for (int i = 0; i < Convert.ToInt32(userinput); i++)
                    {
                        Console.Write("Enter a topping: ");
                        userinput = Console.ReadLine();
                        // Check empty input
                        if (userinput == "" || userinput == null)
                        {
                            Console.WriteLine("Cancelled.");
                        }
                        else
                        {
                            // Check if topping added is already in topping list
                            bool check = false;
                            foreach (Topping topping in toppings)
                            {
                                // If flavour in flavour list
                                if (topping.Type.ToLower() == userinput.ToLower())
                                {
                                    // Print that it's already in
                                    Console.WriteLine($"{topping.Type} is already");
                                    break;
                                }
                            }
                            // If topping not in topping list
                            if (check == false)
                            {
                                // Read toppings.csv to see toppings info
                                bool check2 = false;
                                string[] toppingsList = File.ReadAllLines("toppings.csv");
                                // For each topping in toppings.csv
                                foreach (string line in toppingsList)
                                {
                                    // Split info
                                    string[] info = line.split(",");
                                    // If topping name matches
                                    if (userinput.ToLower() == info[0].ToLower())
                                    {
                                        // Add the topping
                                        toppings.Add(new Topping(info[0]));
                                        check2 = true;
                                        break;
                                    }
                                }
                                // If topping not in toppings.csv too = not an available topping
                                if (check2 == false)
                                {
                                    throw new toppingException("Not an available topping.");
                                }
                            }
                        }
                    }
                }
                // Output display
                string outputDisplay = "";
                // If cone get dip status
                if (option == "Cone")
                {
                    Console.Write("Do you want your cone to be chocolate-dipped (Default: No)? [Y/N])");
                    userinput = Console.ReadLine().ToUpper();
                    switch (userinput)
                    {
                        // If dipped
                        case "Y":
                            // Creates dipped cone
                            Cone newCone = new Cone(Option, Scoops, flavours, toppings, true);
                            iceCreamList.Add(newCone);
                            outputDisplay = "dipped cone";
                            break;
                        // If not dipped
                        case "N":
                            // Created regular cone
                            Cone newCone = new Cone(Option, Scoops, flavours, toppings, false);
                            iceCreamList.Add(newCone);
                            outputDisplay = "cone";
                            break;
                        default:
                            // Defaults to regular cone
                            Console.WriteLine("Default: Undipped");
                            Cone newCone = new Cone(Option, Scoops, flavours, toppings, true);
                            iceCreamList.Add(newCone);
                            outputDisplay = "cone";
                            break;
                    }
                }
                // Waffle flavour
                else if (option = "Waffle")
                {
                    Console.WriteLine("Enter waffle flavour ( Default / Red velvet / Charcoal / Pandan) : ");
                    userinput = Console.ReadLine();
                    switch (userinput.ToUpper())
                    {
                        // Red velvet
                        case "RED VELVET":
                            // Create red velvet
                            Waffle newWaffle = new Waffle(Option, Scoops, flavours, toppings, "Red velvet");
                            iceCreamList.Add(newWaffle);
                            outputDisplay = "red velvet waffle";
                            break;
                        // Charcoal
                        case "CHARCOAL":
                            Waffle newWaffle = new Waffle(Option, Scoops, flavours, toppings, "Charcoal");
                            iceCreamList.Add(newWaffle);
                            outputDisplay = "charcoal waffle";
                            break;
                        // Pandan
                        case "PANDAN":
                            Waffle newWaffle = new Waffle(Option, Scoops, flavours, toppings, "Pandan");
                            iceCreamList.Add(newWaffle);
                            outputDisplay = "pandan waffle";
                            break;
                        // Default
                        case "DEFAULT":
                            Waffle newWaffle = new Waffle(Option, Scoops, flavours, toppings, "None");
                            iceCreamList.Add(newWaffle);
                            outputDisplay = "waffle";
                            break;
                        default:
                            Console.WriteLine("Invalid option, using default.");
                            Waffle newWaffle = new Waffle(Option, Scoops, flavours, toppings, "None");
                            iceCreamList.Add(newWaffle);
                            outputDisplay = "waffle";
                            break;
                    }
                }
                // Cup
                else
                {
                    Cup newCup = new Cup(Option, Scoops, flavours, toppings);
                    outputDisplay = "cup";
                }
                // Display success
                Console.WriteLine($"Your {Scoops} scoop ice cream {outputDisplay} has been created.");
            }
            catch
            {
                Console.WriteLine("Creation cancelled. Try again.");
            }*/
        }
        // Remove ice cream
        public void DeleteIceCream(int indexing)
        {
            iceCreamList.RemoveAt(indexing-1);
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
        public override string ToString()
        {
            string startStr =  "Order:\n" +
                               "====================\n";
            int count = 1;
            foreach (IceCream iceCream in iceCreamList)
            {
                startStr += $"[{count}] {iceCream.ToString}\n";
            }
            startStr += "====================\n" +
                $"Total: {this.CalculateTotal}";
            return startStr;
        }
    }
}