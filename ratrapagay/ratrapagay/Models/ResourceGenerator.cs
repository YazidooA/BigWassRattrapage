namespace RattrapageProjet.Models
{
    public abstract class ResourceGenerator : Building
    {
        public int ResourcePerTurn { get; set; }
        public ResourceGenerator(int x, int y, int health, string name, int resourcePerTurn)
            : base(x, y, health, name)
        {
            ResourcePerTurn = resourcePerTurn;
        }

        public abstract int GenerateResource();
    }
} 