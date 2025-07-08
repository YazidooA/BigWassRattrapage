namespace RattrapageProjet.Models
{
    public class Player : Entity
    {
        public int Gold { get; set; }
        public int Elixir { get; set; }

        public Player(int x, int y) : base(x, y, 20)
        {
            Gold = 0;
            Elixir = 0;
        }

        public void CollectGold(int amount)
        {
            Gold += amount;
        }

        public void CollectElixir(int amount)
        {
            Elixir += amount;
        }
    }
} 