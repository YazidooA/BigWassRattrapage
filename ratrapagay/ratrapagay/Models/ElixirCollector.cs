namespace RattrapageProjet.Models
{
    public class ElixirCollector : ResourceGenerator
    {
        public ElixirCollector(int x, int y) : base(x, y, 15, "ElixirCollector", 10)
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