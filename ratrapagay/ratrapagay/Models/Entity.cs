namespace RattrapageProjet.Models
{
    public abstract class Entity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; }

        public Entity(int x, int y, int health)
        {
            X = x;
            Y = y;
            Health = health;
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }
} 