namespace RattrapageProjet.Models
{
    public class GoldMine : ResourceGenerator
    {
        public GoldMine(int x, int y) : base(x, y, 15, "GoldMine", 10)
        {
        }

        public override int GenerateResource()
        {
            return ResourcePerTurn;
        }

        public override void OnInteract()
        {

        }
    }
} 