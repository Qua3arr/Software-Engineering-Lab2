using System;
using System.Collections.Generic;

namespace Composite_pattern
{
    //Базовый интерфейс для всех компонентов заказа
    public interface IOrderComponent
    {
        decimal GetPrice();
    }

    //Класс Продукта
    public class Product : IOrderComponent
    {
        public string Name { get; }
        public decimal Price { get; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public decimal GetPrice() => Price;
    }

    //Класс Коробки (компоновщик)
    public class Box : IOrderComponent
    {
        private readonly List<IOrderComponent> _children = new List<IOrderComponent>();

        public string Name { get; }

        public Box(string name)
        {
            Name = name;
        }

        public void Add(IOrderComponent component) => _children.Add(component);
        public void Remove(IOrderComponent component) => _children.Remove(component);

        public decimal GetPrice()
        {
            decimal total = 0;
            foreach (var child in _children)
            {
                total += child.GetPrice();
            }
            return total;
        }
    }

    //Класс Заказа
    public class Order
    {
        private readonly List<IOrderComponent> _components = new List<IOrderComponent>();

        public void AddComponent(IOrderComponent component) => _components.Add(component);

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var component in _components)
            {
                total += component.GetPrice();
            }
            return total;
        }
    }

    internal class ProgramCompositePattern
    {
        static void Main(string[] args)
        {
            //Создаем продукты
            var laptop = new Product("Laptop", 1500m);
            var mouse = new Product("Mouse", 25m);
            var keyboard = new Product("Keyboard", 75m);
            var headphones = new Product("Headphones", 100m);

            //Создаем коробки разных уровней
            var smallBox = new Box("Small Box");
            var mediumBox = new Box("Medium Box");
            var largeBox = new Box("Large Box");

            //Формируем иерархию
            smallBox.Add(mouse);
            smallBox.Add(keyboard);

            mediumBox.Add(smallBox);
            mediumBox.Add(headphones);

            largeBox.Add(mediumBox);
            largeBox.Add(laptop);

            //Создаем заказ
            var order = new Order();
            order.AddComponent(largeBox);
            order.AddComponent(new Product("USB Cable", 10m));

            decimal total = order.CalculateTotal();
            Console.WriteLine($"Total order price: ${total}");
        }
    }
}
