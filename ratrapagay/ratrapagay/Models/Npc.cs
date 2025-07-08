namespace RattrapageProjet.Models
{
    public abstract class Npc : Entity
    {
        public Npc(int x, int y, int health) : base(x, y, health) { }
        public void MoveTowards(int targetX, int targetY)
        {
            int dx = targetX - X;
            int dy = targetY - Y;

            if (Math.Abs(dx) > Math.Abs(dy))
                X += Math.Sign(dx);
            else if (dy != 0)
                Y += Math.Sign(dy);
        }

    }
} 