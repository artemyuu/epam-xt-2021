using System;
using System.Threading;
using System.Collections.Generic;

namespace Task_3._3._3
{
    class Program
    {
        static void Main(string[] args)
        {
            var pizzeria = new Pizzeria();

            var customer = new Customer("Alex"); 
            customer.AddToOrder(0);
            customer.AddToOrder(2, 2);
            customer.CompleteOrder(pizzeria);

            var customer1 = new Customer("Artem"); 
            customer1.AddToOrder(1);
            customer1.CompleteOrder(pizzeria);

            pizzeria.StartCooking();
        }
    }

    class Order{
        public event Action<Order> OnReady;
        public string CustumerName{get; private set;}
        public Dictionary<int, int> PizzaIdCount;
        public Order(string custumerName){
            PizzaIdCount = new Dictionary<int, int>();
            CustumerName = custumerName;
        }
        public void AddToOrder(int pizzaId, int count = 1){
            if(count > 0){
                if(PizzaIdCount.ContainsKey(pizzaId)) PizzaIdCount[pizzaId] += count;
                else PizzaIdCount.Add(pizzaId, count);
            }
            else throw new ArgumentException();
        }
        public void RemoveFromOrder(int pizzaId, int count = 1){
            if(count > 0){
                if(PizzaIdCount.ContainsKey(pizzaId) && PizzaIdCount[pizzaId] - count > 1) PizzaIdCount[pizzaId] -= count;
                else PizzaIdCount.Remove(pizzaId);
            }
            else throw new ArgumentException();
        }
        public void Ready(){
            OnReady?.Invoke(this);
        }
    }

    class Pizza{
        private int _CookingTime{get; set;}
        public string Name{get; private set;}
        public double Price{get; private set;}
        public event Action<Pizza> OnCoocked;
        public Pizza(string name, double price, int cookingTime){
            Name = name;
            Price = price;
            _CookingTime = cookingTime;
        }

        public void Cook(){
            Thread.Sleep(_CookingTime);
            Console.WriteLine($"Пицца {Name} готова");
            OnCoocked?.Invoke(this);
        }
    }

    class Pizzeria{
        public string[] MenuList{get; private set;}
        private List<Pizza> PizzasForCustumer{get; set;}
        private List<Order> OrdersList{get; set;}
        public Pizzeria(){
            MenuList = new string[]{
                "№1 Сырная",
                "№2 Ветчина и сыр",
                "№3 Пепперони"
            };
            OrdersList = new List<Order>();
            PizzasForCustumer = new List<Pizza>();
        }

        public void AddOrder(Order order){
            order.OnReady += OrderIsReady;
            OrdersList.Add(order);
        }

        public void StartCooking(){
            foreach(Order order in OrdersList){
                foreach(KeyValuePair<int, int> keyValue in order.PizzaIdCount){
                    switch (keyValue.Key){
                        case 0: {
                            CookPizza(new Pizza("Сырная", 255, 1000), keyValue.Value);
                            break;
                        }
                        case 1: {
                            CookPizza(new Pizza("Ветчина и сыр", 270, 1600), keyValue.Value);
                            break;
                        }
                        case 2: {
                            CookPizza(new Pizza("Пепперони", 230, 900), keyValue.Value);
                            break;
                        }
                    }
                }
                order.Ready();
            }
        }

        public void CookPizza(Pizza pizza, int count){
            for(int i = 0; i < count; i++){
                pizza.OnCoocked += PizzaIsReady;
                pizza.Cook();
            }
        }

        public void PizzaIsReady(Pizza pizza){
            PizzasForCustumer.Add(pizza);
        }

        public void OrderIsReady(Order order){
            Console.WriteLine($"Заказ для {order.CustumerName} готов.");
            PizzasForCustumer.Clear();
        }
    }

    class Customer{
        public string Name{get; private set;}
        public Order CustomerOrder{get; private set;}
        public Customer(string name){
            Name = name;
            CustomerOrder = new Order(Name);
        }
        public void AddToOrder(int pizzaId, int count = 1){
            CustomerOrder.AddToOrder(pizzaId, count);
        }
        public void RemoveFromOrder(int pizzaId, int count = 1){
            CustomerOrder.RemoveFromOrder(pizzaId, count);
        }
        public void CompleteOrder(Pizzeria pizzeria){
            pizzeria.AddOrder(CustomerOrder);
        }
    }
}
