using System;
using Tuntiseuranta.Models;

namespace Tuntiseuranta
{
    class Program
    {
        static void Main(string[] args)
        {

            StartMenu();

        }
        public static void StartMenu()
        {
            var ui = new UI();

            Console.WriteLine("[1] Lisää käyttäjä");
            Console.WriteLine("[2] Lisää tunteja");
            Console.WriteLine("[3] Raportit");

            ConsoleKeyInfo switchKey = Console.ReadKey();

            switch (switchKey.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Clear();
                    ui.LisaaKayttaja();
                    ui.LisaaTunnit();
                    break;


                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    ui.KirjauduSisään();
                    ui.LisaaTunnit();
                    break;


                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    ui.Raportointi();
                    break;


                default:
                    Console.WriteLine("Väärä vaihtoehto.");
                    break;

            }
        }
    }
}
