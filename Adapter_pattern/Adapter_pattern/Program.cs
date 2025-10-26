using System;

namespace Adapter_pattern
{
    internal class Program
    {
        //Интерфейс Транспорта
        public interface ITransport
        {
            void Drive();
        }

        //Класс Машины (уже является транспортом)
        public class Car : ITransport
        {
            public void Drive()
            {
                Console.WriteLine("Машина едет по дороге");
            }
        }

        //Класс путешественника
        class Driver
        {
            public void Travel(ITransport transport)
            {
                transport.Drive();
            }
        }

        //Интерфейс животного
        interface IAnimal
        {
            void Move();
            void Eat();
        }

        //Класс Осла (НЕ является транспортом)
        public class Donkey: IAnimal
        {
            public void Eat()
            {
                Console.WriteLine("Осёл ест сено");
            }

            public void Move()
            {
                Console.WriteLine("Осёл идёт медленно по полю");
            }
        }

        //Адаптер Седло, который делает осла транспортом
        public class Saddle : ITransport
        {
            private readonly Donkey _donkey;

            public Saddle(Donkey donkey)
            {
                _donkey = donkey;
            }

            public void Drive()
            {
                _donkey.Move();
            }
        }

        static void Main(string[] args)
        {
            //Путешественник
            Driver driver = new Driver();

            //Используем машину
            ITransport car = new Car();
            driver.Travel(car);

            Console.WriteLine();

            //Используем адаптированный транспорт - осла с седлом
            Donkey donkey = new Donkey();
            ITransport donkeyWithSaddle = new Saddle(donkey);
            driver.Travel(donkeyWithSaddle);

            //Проверяем, что оригинальный осёл остался неизменным
            Console.WriteLine();
            donkey.Eat();
        }
    }
}
