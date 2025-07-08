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

        // Dans Entity.cs
        public void Move(int dx, int dy)
        {
            int newX = X + dx;
            int newY = Y + dy;
            
            // Vérifier les limites (0-14)
            if (newX >= 0 && newX < 15 && newY >= 0 && newY < 15)
            {
                X = newX;
                Y = newY;
            }
        }
    }
} 