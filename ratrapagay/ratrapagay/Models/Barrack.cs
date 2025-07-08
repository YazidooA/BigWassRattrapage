namespace RattrapageProjet.Models
{
    public class Barrack : Building
    {
        public Barrack(int x, int y) : base(x, y, 30, "Barrack")
        {
        }

        public override void OnInteract()
        {
        }
    }
} 