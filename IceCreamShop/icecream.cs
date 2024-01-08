namespace IceCreamShop{
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
    public class Topping
    {

    }
    // Ice Cream class
    public class IceCream{
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
    }
}