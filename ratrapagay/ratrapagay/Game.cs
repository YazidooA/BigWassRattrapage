using System;
using RattrapageProjet.Models;
using System.Threading;


namespace RattrapageProjet
{
    public class Game
    {
        private Board board;
        private bool gameOver;
        private const int GridSize = 15;
        private Thread musicThread;
        

        public Game()
        {
            board = new Board();
            gameOver = false;
            board.Player.X = 5;
            board.Player.Y = 5;
            board.Player.Gold = 200;
            board.Player.Elixir = 100;
            
        }

        public void Run()
        {
            while (!gameOver)
            {
                DisplayGrid();
                PlayerAction();
                board.TroopActions();
                board.EnemyActions();
                board.RaiderAutoSpawn(0, 0);
                board.BombermanAutoSpawn(0, 0);
                gameOver = board.UpdateBuildingsAndCheckDefeat();
            }
            DisplayGrid();
            Console.WriteLine("Défaite : le TownHall a été détruit.");
            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.WriteLine("         G  A  M  E   O  V  E  R   ");
            Console.WriteLine();
            Console.WriteLine("====================================");
        }

        private void DisplayGrid()
        {
            Console.SetCursorPosition(0, 0); // ← remet le curseur en haut sans effacer tout
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string[,] grid = new string[GridSize, GridSize];
            for (int y = 0; y < GridSize; y++)
                for (int x = 0; x < GridSize; x++)
                    grid[x, y] = "⬜ ";

            foreach (var b in board.Buildings)
            {
                string c = b switch
                {
                    TownHall => "🏰 ",
                    GoldMine => "💰 ",
                    ElixirCollector => "🔮 ",
                    Wall => "🧱 ",
                    Barrack => "⚔️ ",
                    _ => "⬜ "
                };
                if (b.X >= 0 && b.X < GridSize && b.Y >= 0 && b.Y < GridSize)
                    grid[b.X, b.Y] = c;
            }

            foreach (var t in board.Troops)
            {
                string c = t is Archer ? "🎯 " : "🪓 ";
                if (t.X >= 0 && t.X < GridSize && t.Y >= 0 && t.Y < GridSize)
                    grid[t.X, t.Y] = c;
            }

            foreach (var e in board.Enemies)
            {
                string c = e is Bomberman ? "🧨 " : "👾 ";
                if (e.X >= 0 && e.X < GridSize && e.Y >= 0 && e.Y < GridSize)
                    grid[e.X, e.Y] = c;
            }

            if (board.Player.X >= 0 && board.Player.X < GridSize && board.Player.Y >= 0 && board.Player.Y < GridSize)
                grid[board.Player.X, board.Player.Y] = "🦸 ";

            // Affichage de la grille
            Console.Write("+");
            for (int x = 0; x < GridSize; x++) Console.Write("--");
            Console.WriteLine("+");

            for (int y = 0; y < GridSize; y++)
            {
                Console.Write("|");
                for (int x = 0; x < GridSize; x++)
                    Console.Write(grid[x, y]);
                Console.WriteLine("|");
            }

            Console.Write("+");
            for (int x = 0; x < GridSize; x++) Console.Write("--");
            Console.WriteLine("+");

            // Affichage des infos (fixe et lisible)
            Console.WriteLine();
            Console.WriteLine("🦸=Joueur 🏰=Hôtel de Ville 💰=Mine d'or 🔮=Collecteur d'élixir 🧱=Mur ⚔️=Caserne 🎯=Archer 🪓=Barbare 👾=Raider 🧨=Bomberman ⬜=Vide");
            Console.WriteLine($"Gold: {board.Player.Gold}   Elixir: {board.Player.Elixir}   TownHall: {GetTownHallHealth()}PV");
            Console.WriteLine("Actions : ZQSD=Déplacer, G=GoldMine, E=ElixirCollector, W=Wall, B=Barrack, C=Collecter, A=Archer, R=Barbarian, Q=Quitter");
            Console.WriteLine($"Coûts bâtiments : GoldMine={BuildingCosts.GoldMine}G, ElixirCollector={BuildingCosts.ElixirCollector}G, Wall={BuildingCosts.Wall}G, Barrack={BuildingCosts.Barrack}G");
            Console.WriteLine($"Coûts troupes : Archer={TroopCosts.Archer}E, Barbarian={TroopCosts.Barbarian}E");
        }

        private int GetTownHallHealth()
        {
            foreach (var b in board.Buildings)
                if (b is TownHall) return b.Health;
            return 0;
        }

        private void PlayerAction()
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Z:
                case ConsoleKey.UpArrow:
                    board.Player.Move(0, -1);
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    board.Player.Move(0, 1);
                    break;

                case ConsoleKey.Q:
                case ConsoleKey.LeftArrow:
                    board.Player.Move(-1, 0);
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    board.Player.Move(1, 0);
                    break;
                case ConsoleKey.G:
                    board.BuildBuilding("GoldMine");
                    break;
                case ConsoleKey.E:
                    board.BuildBuilding("ElixirCollector");
                    break;
                case ConsoleKey.W:
                    board.BuildBuilding("Wall");
                    break;
                case ConsoleKey.B:
                    board.BuildBuilding("Barrack");
                    break;
                case ConsoleKey.C:
                    board.CollectResource();
                    break;
                case ConsoleKey.A:
                    board.TrainTroop("Archer");
                    break;
                case ConsoleKey.R:
                    board.TrainTroop("Barbarian");
                    break;
            }
        }
    }
} 