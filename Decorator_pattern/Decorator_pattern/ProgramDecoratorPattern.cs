using System;

namespace Decorator_pattern
{
    //Абстрактный класс системы доставки
    public abstract class DeliverySystem
    {
        public abstract decimal CalculateCost();
        public abstract string GetDescription();
        public abstract int GetDeliveryTime();
    }

    //Класс-потомок для экспресс-доставки
    public class ExpressDeliverySystem : DeliverySystem
    {
        private readonly DeliverySystem _baseSystem;

        public ExpressDeliverySystem(DeliverySystem baseSystem)
        {
            _baseSystem = baseSystem;
        }

        public override decimal CalculateCost()
        {
            //Использование API курьерской службы для расчета стоимости (заглушка)
            decimal baseCost = _baseSystem.CalculateCost();
            decimal expressSurcharge = CalculateExpressSurcharge();
            return baseCost + expressSurcharge;
        }

        public override string GetDescription()
        {
            return _baseSystem.GetDescription() + " (Экспресс)";
        }

        public override int GetDeliveryTime()
        {
            //Экспресс доставка в 2 раза быстрее
            int baseTime = _baseSystem.GetDeliveryTime();
            return Math.Max(1, baseTime / 2); //минимум 1 день
        }

        //Специфические методы для экспресс-доставки
        public string TrackDelivery(string trackingNumber)
        {
            //Заглушка для API курьерской службы отслеживания
            return $"Статус экспресс-доставки {trackingNumber}: В пути, прибытие через {GetDeliveryTime()} дней";
        }

        public decimal CalculateExpressCost()
        {
            //Заглушка для API расчета стоимости экспресс-доставки
            return CalculateCost();
        }

        private decimal CalculateExpressSurcharge()
        {
            //Логика расчета надбавки за экспресс (заглушка API)
            return 10.0m;
        }
    }

    //Декоратор для системы доставки
    public class ExpressDeliveryDecorator : DeliverySystem
    {
        private readonly ExpressDeliverySystem _expressSystem;

        public ExpressDeliveryDecorator(DeliverySystem baseSystem)
        {
            _expressSystem = new ExpressDeliverySystem(baseSystem);
        }

        public override decimal CalculateCost()
        {
            return _expressSystem.CalculateCost();
        }

        public override string GetDescription()
        {
            return _expressSystem.GetDescription();
        }

        public override int GetDeliveryTime()
        {
            return _expressSystem.GetDeliveryTime();
        }

        //Новые методы, добавляемые декоратором
        public string TrackDelivery(string trackingNumber)
        {
            return _expressSystem.TrackDelivery(trackingNumber);
        }

        public decimal CalculateExpressCost()
        {
            return _expressSystem.CalculateExpressCost();
        }
    }

    //Конкретные реализации систем доставки
    public class CourierDelivery : DeliverySystem
    {
        public override decimal CalculateCost() => 5.0m;
        public override string GetDescription() => "Курьерская доставка";
        public override int GetDeliveryTime() => 3;
    }

    public class PostalDelivery : DeliverySystem
    {
        public override decimal CalculateCost() => 2.5m;
        public override string GetDescription() => "Почтовая доставка";
        public override int GetDeliveryTime() => 7;
    }

    public class PickupDelivery : DeliverySystem
    {
        public override decimal CalculateCost() => 0m;
        public override string GetDescription() => "Самовывоз";
        public override int GetDeliveryTime() => 1;
    }

    internal class ProgramDecoratorPattern
    {
        static void Main(string[] args)
        {
            //Создаем базовые системы доставки
            DeliverySystem courier = new CourierDelivery();
            DeliverySystem postal = new PostalDelivery();
            DeliverySystem pickup = new PickupDelivery();

            //Демонстрация базовых способов доставки
            Console.WriteLine("=== Базовые способы доставки ===");
            TestDeliverySystem(courier);
            TestDeliverySystem(postal);
            TestDeliverySystem(pickup);

            Console.WriteLine("\n=== Экспресс-доставка через декоратор ===");

            //Создаем экспресс-доставку через декоратор
            DeliverySystem expressCourier = new ExpressDeliveryDecorator(new CourierDelivery());
            DeliverySystem expressPostal = new ExpressDeliveryDecorator(new PostalDelivery());
            DeliverySystem expressPickup = new ExpressDeliveryDecorator(new PickupDelivery());

            //Тестируем экспресс-доставку
            TestExpressDelivery(expressCourier);
            TestExpressDelivery(expressPostal);
            TestExpressDelivery(expressPickup);

            Console.ReadLine();
        }

        static void TestDeliverySystem(DeliverySystem system)
        {
            Console.WriteLine($"Способ: {system.GetDescription()}");
            Console.WriteLine($"Стоимость: {system.CalculateCost()} руб.");
            Console.WriteLine($"Срок доставки: {system.GetDeliveryTime()} дней");
            Console.WriteLine();
        }

        static void TestExpressDelivery(DeliverySystem system)
        {
            if (system is ExpressDeliveryDecorator express)
            {
                Console.WriteLine($"Способ: {system.GetDescription()}");
                Console.WriteLine($"Стоимость: {system.CalculateCost()} руб.");
                Console.WriteLine($"Срок доставки: {system.GetDeliveryTime()} дней");

                //Используем специфические методы экспресс-доставки
                string trackingNumber = "TRK" + DateTime.Now.Ticks;
                Console.WriteLine($"Отслеживание: {express.TrackDelivery(trackingNumber)}");
                Console.WriteLine($"Стоимость экспресс-доставки: {express.CalculateExpressCost()} руб.");
                Console.WriteLine();
            }
        }
    }
    
}
