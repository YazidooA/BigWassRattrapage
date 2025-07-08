using System.Linq;
using System.Collections.Generic;

namespace RattrapageProjet.Models
{
    public class Bomberman : Enemy
    {
        public Bomberman(int x, int y) : base(x, y, 1) { }
        // Dans Bomberman.cs
        public List<(int x, int y)> FindPathToTownHall(Board board)
        {
            var townHall = board.Buildings.FirstOrDefault(b => b is TownHall);
            if (townHall == null) return new List<(int, int)>();
            
            // Implémentation simple BFS pour le pathfinding
            // (vous pouvez implémenter A* pour plus d'efficacité)
            return BreadthFirstSearch(X, Y, townHall.X, townHall.Y, board);
        }
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

        public static List<(int x, int y)> BreadthFirstSearch(int startX, int startY, int goalX, int goalY, Board board)
        {
            var visited = new bool[15, 15];
            var queue = new Queue<(int x, int y, List<(int, int)> path)>();
            queue.Enqueue((startX, startY, new List<(int, int)>()));

            while (queue.Count > 0)
            {
                var (x, y, path) = queue.Dequeue();

                if (x == goalX && y == goalY)
                    return path;

                if (visited[x, y]) continue;
                visited[x, y] = true;

                var directions = new (int dx, int dy)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

                foreach (var (dx, dy) in directions)
                {
                    int newX = x + dx;
                    int newY = y + dy;

                    if (newX >= 0 && newX < 15 && newY >= 0 && newY < 15 &&
                        !visited[newX, newY] &&
                        !board.IsPositionOccupied(newX, newY))
                    {
                        var newPath = new List<(int, int)>(path) { (newX, newY) };
                        queue.Enqueue((newX, newY, newPath));
                    }
                }
            }

            return new List<(int, int)>(); // aucun chemin trouvé
        }

    }
}