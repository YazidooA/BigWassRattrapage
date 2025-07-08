namespace RattrapageProjet.Models
{
    public abstract class Enemy : Npc
    {
        public Enemy(int x, int y, int health) : base(x, y, health) { }

        public virtual void Attack(Building target)
        {
            if (target != null)
            {
                target.Health -= 5;
                if (target.Health < 0)
                    target.Health = 0;
            }
        }
    }
} 