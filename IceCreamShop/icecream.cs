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
        public Flavour() { }
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
}