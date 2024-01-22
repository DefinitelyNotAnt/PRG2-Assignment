using System.Security.Cryptography.X509Certificates;
using static System.Formats.Asn1.AsnWriter;

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
            string;
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
                return 0;
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
        /*
        public void ModifyIceCream(int Index)
        {
            if (Index <=0 || Index > IceCreamList.Count)
            {
                Console.WriteLine("Not a valid selection.");
            }
            else
            {
                try
                {
                    IceCream iceCream = iceCreamList[Index-1];
                    Console.Write("============================\n" +
                                  "===== Modify Ice Cream =====\n" +
                                  "============================\n" +
                                  "Options:\n" +
                                  "[1] Change ice cream option\n" +
                                  "[2] Change number of scoops\n" +
                                  "[3] Change flavours\n" +
                                  "[4] Change toppings\n" +
                                  "[0] Exit\n" +
                                  "Enter option: ");
                    string userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            Console.WriteLine($"Current option is: {iceCream.Option}.");
                            Console.Write("============================\n" +
                                          "======= Change Option ======\n" +
                                          "============================\n" +
                                          "Options:\n" +
                                          "[1] Cup\n" +
                                          "[2] Cone\n" +
                                          "[3] Waffle\n" +
                                          "[0] Exit\n" +
                                          "Enter option: ");
                            userInput = Console.ReadLine();
                            switch (userInput)
                            {
                                case "1":
                                    if (iceCream.Option == "Cup")
                                    {
                                        Console.WriteLine("Option is already cup.");
                                    }
                                    else
                                    {
                                        iceCream.Option = "Cup";
                                        Console.WriteLine("Option changed to Cup.");
                                    }
                                    Cup cup = iceCream;
                                    iceCreamList[Index-1] = cup;
                                    break;
                                case "2":
                                    if (iceCream.Option == "Cone")
                                    {
                                        Console.WriteLine("Option is already cone.");
                                    }
                                    else
                                    {
                                        iceCream.Option = "Cone";
                                        Console.WriteLine("Option changed to Cone.");
                                    }
                                    Cone cone = iceCream;
                                    Console.Write("Do you want regular cone or chocolate-dipped cone? [Y/N]: ");
                                    userInput = Console.ReadLine().ToUpper();
                                    switch (userInput)
                                    {
                                        case "Y":
                                            cone.Dipped = true;
                                            break;         
                                        case "N":
                                            cone.Dipped = false;
                                            break;
                                        default:
                                            Console.WriteLine("No change will be made.");
                                    }
                                    Console.WriteLine($"Your cone is now {cone.Dipped}.");
                                    break;
                            }
                            break;
                        case "2":
                            Console.WriteLine("How many scoops do you want?: ");
                            userInput = Console.ReadLine();
                            switch (userInput)
                            {
                                case "1":
                                    iceCream.Scoops = 1;
                                    Console.WriteLine("Scoop number changed to 1.");
                                    break;
                                case "2":
                                    iceCream.Scoops = 2;
                                    Console.WriteLine("Scoop number changed to 2.");
                                    break;
                                case "3":
                                    iceCream.Scoops = 3;
                                    Console.WriteLine("Scoop number changed to 3.");
                                    break;
                                default:
                                    Console.WriteLine("You can only have 1, 2, or 3 scoops.");
                                    break;
                            }
                            break;
                        case "3":
                            Console.WriteLine("");
                            break;
                        case "4":
                            break;
                        case "0":
                            break;
                    }
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}