using System;

namespace IceCreamShop{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("Yes");
            Console.WriteLine("Line2");
            List<Topping> toppings  = new List<Topping>();
            toppings.Add(new Topping("Sprinkle"));
            toppings.Add(new Topping("Corn"));
            List<Flavour> flavors = new List<Flavour>();
            flavors.Add(new Flavour("Choco",false,2));
            flavors.Add(new Flavour("Strawberry",true,1));
            Cup cuppo = new Cup("Cup", 2,flavors,toppings);
            Console.WriteLine(cuppo.ToString());
        }
    }
}