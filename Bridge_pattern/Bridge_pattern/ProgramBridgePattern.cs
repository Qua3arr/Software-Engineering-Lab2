using System;

namespace Bridge_pattern
{
    //Интерфейс реализации
    public interface ILogWriter
    {
        void Write(string message);
    }

    //Абстракция
    public abstract class Logger
    {
        protected ILogWriter _writer;

        public Logger(ILogWriter writer) => _writer = writer;

        public abstract void Log(string message);
    }

    //Конкретная абстракция
    public class EcommerceLogger : Logger
    {
        private string _module;

        public EcommerceLogger(ILogWriter writer, string module) : base(writer)
            => _module = module;

        public override void Log(string message)
            => _writer.Write($"[{_module}] {DateTime.Now:HH:mm:ss} - {message}");
    }

    //Конкретные реализации
    public class ConsoleWriter : ILogWriter
    {
        public void Write(string message) => Console.WriteLine(message);
    }

    public class FileWriter : ILogWriter
    {
        private string _filename;

        public FileWriter(string filename) => _filename = filename;

        public void Write(string message)
            => System.IO.File.AppendAllText(_filename, message + Environment.NewLine);
    }

    internal class ProgramBridgePattern
    {
        static void Main(string[] args)
        {
            //Создаем реализации
            var console = new ConsoleWriter();
            var file = new FileWriter("log.txt");

            //Создаем логгеры с разными реализациями
            var orderLogger = new EcommerceLogger(console, "ORDERS");
            var userLogger = new EcommerceLogger(file, "USERS");

            //Используем
            orderLogger.Log("New order #123");
            userLogger.Log("User registered");

            //Меняем реализацию на лету
            orderLogger = new EcommerceLogger(file, "ORDERS");
            orderLogger.Log("Order completed");
        }
    }
}
