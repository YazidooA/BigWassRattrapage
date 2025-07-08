namespace RattrapageProjet.Models
{
    public abstract class Trainable : Npc
    {
        public Trainable(int x, int y, int health) : base(x, y, health) { }

        public void Attack(Enemy target)
        {
            if (target != null)
            {
                target.Health -= 5;
                if (target.Health < 0)
                    target.Health = 0;
            }
        }

        public void MoveTowards(int targetX, int targetY)
        {
            int dx = targetX - X;
            int dy = targetY - Y;
            if (dx != 0)
                X += dx / System.Math.Abs(dx);
            else if (dy != 0)
                Y += dy / System.Math.Abs(dy);
        }
    }
} 