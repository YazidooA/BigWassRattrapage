using System;

namespace RattrapageProjet.Models
{
    public class TownHall : Building
    {
        public TownHall(int x, int y) : base(x, y, 50, "TownHall")
        {
        }

        public override void OnInteract()
        {
        }
    }
} 