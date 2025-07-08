using System.Collections.Generic;
using System.Linq;


namespace RattrapageProjet.Models
{
    public class Board
    {
        public List<Building> Buildings { get; set; }
        public Player Player { get; set; }
        public List<Trainable> Troops { get; set; }
        public List<Enemy> Enemies { get; set; }

        private int raiderSpawnCounter = 0;
        private int raiderSpawnInterval = 10;
        private int bombermanSpawnCounter = 0;
        private int bombermanSpawnInterval = 20;
        private int raiderSpawnCount = 1;
        private int bombermanSpawnCount = 1;
        private int turnsElapsed = 0;

        public Board()
        {
            Buildings = new List<Building>();
            Player = new Player(0, 0);
            Troops = new List<Trainable>();
            Enemies = new List<Enemy>();
            Buildings.Add(new TownHall(5, 5));
        }

        public void AddBuilding(Building building)
        {
            Buildings.Add(building);
        }

        public void AddTroop(Trainable troop)
        {
            Troops.Add(troop);
        }

        public void AddEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
        }
public Enemy GetClosestEnemy(Trainable troop)
{
    Enemy closest = null;
    double minDist = double.MaxValue;
    foreach (var enemy in Enemies)
    {
        double dist = Math.Pow(troop.X - enemy.X, 2) + Math.Pow(troop.Y - enemy.Y, 2);
        if (dist < minDist)
        {
            minDist = dist;
            closest = enemy;
        }
    }
    return closest;
}

        public bool BuildBuilding(string buildingType)
        {
            int x = Player.X;
            int y = Player.Y;
            Building building = null;
            int cost = 0;
            if (IsPositionOccupied(x, y)) return false;
            switch (buildingType)
            {
                case "GoldMine":
                    cost = BuildingCosts.GoldMine;
                    if (Player.Gold >= cost)
                    {
                        building = new GoldMine(x, y);
                        Player.Gold -= cost;
                    }
                    break;
                case "ElixirCollector":
                    cost = BuildingCosts.ElixirCollector;
                    if (Player.Gold >= cost)
                    {
                        building = new ElixirCollector(x, y);
                        Player.Gold -= cost;
                    }
                    break;
                case "Wall":
                    cost = BuildingCosts.Wall;
                    if (Player.Gold >= cost)
                    {
                        building = new Wall(x, y);
                        Player.Gold -= cost;
                    }
                    break;
                case "Barrack":
                    cost = BuildingCosts.Barrack;
                    if (Player.Gold >= cost)
                    {
                        building = new Barrack(x, y);
                        Player.Gold -= cost;
                    }
                    break;
            }
            if (building != null)
            {
                Buildings.Add(building);
                return true;
            }
            return false;
        }

        public bool CollectResource()
        {
            foreach (var building in Buildings)
            {
                if (building.X == Player.X && building.Y == Player.Y)
                {
                    if (building is GoldMine goldMine)
                    {
                        Player.CollectGold(goldMine.GenerateResource());
                        return true;
                    }
                    if (building is ElixirCollector elixirCollector)
                    {
                        Player.CollectElixir(elixirCollector.GenerateResource());
                        return true;
                    }
                }
            }
            return false;
        }

        public Building GetClosestBuilding(int x, int y)
        {
            Building closest = null;
            double minDist = double.MaxValue;
            foreach (var building in Buildings)
            {
                double dist = (building.X - x) * (building.X - x) + (building.Y - y) * (building.Y - y);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = building;
                }
            }
            return closest;
        }

        public void EnemyActions()
        {
            foreach (var enemy in Enemies)
            {
                Building target = GetClosestBuilding(enemy.X, enemy.Y);
                if (target != null)
                {
                    if (enemy.X == target.X && enemy.Y == target.Y)
                    {
                        enemy.Attack(target);
                    }
                    else
                    {
                        enemy.MoveTowards(target.X, target.Y);
                    }
                }
            }
        }

        public bool UpdateBuildingsAndCheckDefeat()
        {
            foreach (var building in Buildings)
            {
                if (building is TownHall && building.Health <= 0)
                {
                    return true;
                }
            }
            Buildings.RemoveAll(b => b.Health <= 0);
            return false;
        }

        public void SpawnRaider(int x, int y)
        {
            Enemies.Add(new Raider(x, y));
        }

        public void RaiderAutoSpawn(int spawnX, int spawnY)
        {
            raiderSpawnCounter++;
            turnsElapsed++;
            if (raiderSpawnCounter >= raiderSpawnInterval)
            {
                for (int i = 0; i < raiderSpawnCount; i++)
                    SpawnRaider(spawnX, spawnY);
                raiderSpawnCounter = 0;
            }
            if (turnsElapsed % 25 == 0 && raiderSpawnCount < 5)
                raiderSpawnCount++;
        }

        public bool TrainTroop(string troopType)
        {
            foreach (var building in Buildings)
            {
                if (building is Barrack && building.X == Player.X && building.Y == Player.Y)
                {
                    if (troopType == "Archer" && Player.Elixir >= TroopCosts.Archer)
                    {
                        Troops.Add(new Archer(building.X, building.Y));
                        Player.Elixir -= TroopCosts.Archer;
                        return true;
                    }
                    if (troopType == "Barbarian" && Player.Elixir >= TroopCosts.Barbarian)
                    {
                        Troops.Add(new Barbarian(building.X, building.Y));
                        Player.Elixir -= TroopCosts.Barbarian;
                        return true;
                    }
                }
            }
            return false;
        }

        // Dans Board.cs - méthode TroopActions()
        public void TroopActions()
        {
            var townHall = Buildings.FirstOrDefault(b => b is TownHall);
            
            foreach (var troop in Troops)
            {
                Enemy closest = GetClosestEnemy(troop);
                
                if (closest != null)
                {
                    if (troop.X == closest.X && troop.Y == closest.Y)
                    {
                        troop.Attack(closest);
                    }
                    else
                    {
                        troop.MoveTowards(closest.X, closest.Y);
                    }
                }
                else if (townHall != null)
                {
                    // Retourner au Town Hall si pas d'ennemi
                    if (troop.X != townHall.X || troop.Y != townHall.Y)
                    {
                        troop.MoveTowards(townHall.X, townHall.Y);
                    }
                }
            }
        }
        public void BombermanAutoSpawn(int spawnX, int spawnY)
        {
            bombermanSpawnCounter++;
            if (bombermanSpawnCounter >= bombermanSpawnInterval)
            {
                for (int i = 0; i < bombermanSpawnCount; i++)
                    SpawnBomberman(spawnX, spawnY);
                bombermanSpawnCounter = 0;
            }
            if (turnsElapsed % 50 == 0 && bombermanSpawnCount < 3)
                bombermanSpawnCount++;
        }

        public void SpawnBomberman(int x, int y)
        {
            Enemies.Add(new Bomberman(x, y));
        }
        // Dans Board.cs
        public bool IsPositionOccupied(int x, int y)
        {
            return Buildings.Any(b => b.X == x && b.Y == y) || 
                Troops.Any(t => t.X == x && t.Y == y) || 
                Enemies.Any(e => e.X == x && e.Y == y);
        }
        public void CleanupDeadEntities()
        {
            Enemies.RemoveAll(e => e.Health <= 0);
            Troops.RemoveAll(t => t.Health <= 0);
        }
    }

} 