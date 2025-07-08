using System;
using RattrapageProjet.Models;
using System.Threading;

namespace RattrapageProjet
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Run();
            Console.ReadKey();
        }
    }
}
