namespace RattrapageProjet.Models
{
    public class Wall : Building
    {
        public Wall(int x, int y) : base(x, y, 20, "Wall")
        {
        }

        public override void OnInteract()
        {
        }
    }
} 