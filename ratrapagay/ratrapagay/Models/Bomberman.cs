namespace RattrapageProjet.Models
{
    public class Bomberman : Enemy
    {
        public Bomberman(int x, int y) : base(x, y, 1) { }

        public override void Attack(Building target)
        {
            if (target is TownHall)
            {
                target.Health = 0;
                Console.WriteLine("💥 Le Bomberman explose et détruit instantanément le Town Hall ! 💥");
            }
            else
            {
                base.Attack(target);
            }
        }
    }
}