using System;
using System.ServiceModel;

// Задача простая, запуститься и висеть в процессах, обрабатывая логику сервиса

namespace ChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Chat.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Хост запущен...");
                Console.ReadLine();
            }
        }
    }
}