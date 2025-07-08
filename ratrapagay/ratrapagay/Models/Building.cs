namespace RattrapageProjet.Models
{
    public abstract class Building
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }

        public Building(int x, int y, int health, string name)
        {
            X = x;
            Y = y;
            Health = health;
            Name = name;
        }

        public abstract void OnInteract();
    }
} 