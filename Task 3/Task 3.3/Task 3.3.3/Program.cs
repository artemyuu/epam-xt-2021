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
            var order1 = new Order(customer.Name, pizzeria);

            customer.AddToOrder(order1, 0);
            customer.AddToOrder(order1, 2, 2);
            customer.CompleteOrder(order1);

            var customer1 = new Customer("Artem"); 
            var order2 = new Order(customer1.Name, pizzeria);
            customer1.AddToOrder(order2,1);
            customer1.CompleteOrder(order2);

            pizzeria.StartCooking();
        }
    }

    class Order{
        private Pizzeria Pizzeria{get; set;}
        public string CustumerName{get; private set;}
        public Dictionary<int, int> PizzaIdCount;
        public Order(string custumerName, Pizzeria pizzeria){
            PizzaIdCount = new Dictionary<int, int>();
            CustumerName = custumerName;
            Pizzeria = pizzeria;
        }
        public void AddToOrder(int pizzaId, int count = 1){
            if(count < 0){
                throw new ArgumentException(); 
            }
            else {
                if(PizzaIdCount.ContainsKey(pizzaId)) PizzaIdCount[pizzaId] += count;
                else PizzaIdCount.Add(pizzaId, count);
            } 
        }
        public void RemoveFromOrder(int pizzaId, int count = 1){
            if(count < 0){
                throw new ArgumentException();
            }
            else {
                if(PizzaIdCount.ContainsKey(pizzaId) && PizzaIdCount[pizzaId] - count > 1) PizzaIdCount[pizzaId] -= count;
                else PizzaIdCount.Remove(pizzaId);
            };
        }
        public void CompleteOrder(){
            Pizzeria.AddOrder(this);
        }
    }

    class Pizza{
        public int CookingTime{get; private set;}
        public string Name{get; private set;}
        public double Price{get; private set;}
        public Pizza(string name, double price, int cookingTime){
            Name = name;
            Price = price;
            CookingTime = cookingTime;
        }
    }

    class Pizzeria{
        public string[] MenuList{get; private set;}
        public Pizza[] PizzasList{get; private set;}
        public event Action<Pizza> OnCoocked;
        public event Action<Order> OnOrderIsReady;
        private List<Pizza> PizzasForCustumer{get; set;}
        private List<Order> OrdersList{get; set;}
        public Pizzeria(){
            MenuList = new string[]{
                "№1 Сырная",
                "№2 Ветчина и сыр",
                "№3 Пепперони"
            };
            PizzasList = new Pizza[]{
                new Pizza("Сырная", 255, 1000),
                new Pizza("Ветчина и сыр", 270, 1600),
                new Pizza("Пепперони", 230, 900)
            };
            OrdersList = new List<Order>();
            PizzasForCustumer = new List<Pizza>();
            OnCoocked += PizzaIsReady;
            OnOrderIsReady += OrderIsReady;
        }

        public void AddOrder(Order order){
            OrdersList.Add(order);
        }

        public void StartCooking(){
            foreach(Order order in OrdersList){
                foreach(KeyValuePair<int, int> keyValue in order.PizzaIdCount){
                    CookPizza(PizzasList[keyValue.Key], keyValue.Value);
                }
                OnOrderIsReady?.Invoke(order);
            }
        }

        public void CookPizza(Pizza pizza, int count){
            for(int i = 0; i < count; i++){
                Thread.Sleep(pizza.CookingTime);
                OnCoocked?.Invoke(pizza);
            }
        }

        public void PizzaIsReady(Pizza pizza){
            PizzasForCustumer.Add(pizza);
        }

        public void OrderIsReady(Order order){
            Console.WriteLine($"Заказ для {order.CustumerName} готов:");
            for(int i = 0; i < PizzasForCustumer.Count; i++){
                Console.Write(PizzasForCustumer[i].Name + " ");
            }
            Console.WriteLine();
            PizzasForCustumer.Clear();
        }
    }

    class Customer{
        public string Name{get; private set;}
        public Customer(string name){
            Name = name;
        }
        public void AddToOrder(Order order, int pizzaId, int count = 1){
            order.AddToOrder(pizzaId, count);
        }
        public void RemoveFromOrder(Order order, int pizzaId, int count = 1){
            order.RemoveFromOrder(pizzaId, count);
        }
        public void CompleteOrder(Order order){
            order.CompleteOrder();
        }
    }
}
