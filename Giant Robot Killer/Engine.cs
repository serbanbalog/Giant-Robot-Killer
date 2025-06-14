using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Giant_Robot_Killer.Entities;
using Giant_Robot_Killer.Entities.Robots;

namespace Giant_Robot_Killer
{
    struct QueueNode
    {
        public Cell Pt;
        public int Dist;
        
        public QueueNode(Cell pt, int dist)
        {
            Pt = pt;
            Dist = dist;
        }
    };
    
    public struct Cell
    {
        public int X;
        public int Y;
        
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    };
    
    public class Engine
    {
        int[] _rowNum = { -1, 0, 0, 1 };
        int[] _colNum = { 0, -1, 1, 0 };

        private bool CheckValid(int row, int col, Planet planet)
        {
            return row >= 0 && row < planet.N &&
                   col >= 0 && col < planet.M;
        }
        
        public void CalculateDistanceToClosestEntity(Robot robot, Planet planet)
        {
            double min = double.MaxValue;
            Entity closest = null;

            for (int i = 0; i < planet.N; i++)
            {
                for (int j = 0; j < planet.M; j++)
                {
                    var target = planet.Tiles[i, j].Entity;
                    if (target != null &&
                        (target.GetType().Name == "Human" || target.GetType().Name == "Animal") &&
                        (target.Position.X != robot.Position.X || target.Position.Y != robot.Position.Y))
                    {
                        double distance = Math.Abs(robot.Position.X - i) + Math.Abs(robot.Position.Y - j); // Manhattan

                        if (distance < min)
                        {
                            min = distance;
                            closest = target;
                        }
                    }
                }
            }
            robot.CurrentTarget = closest;
            Console.WriteLine($"Robot:{robot.Position.X}, {robot.Position.Y} Target:{robot.CurrentTarget.Position.X}, {robot.CurrentTarget.Position.Y}");
        }
        
        public void SetPathToClosestEntity(Robot robot, Planet planet)
        {
            if (robot.CurrentTarget == null || !robot.CurrentTarget.Alive)
                CalculateDistanceToClosestEntity(robot, planet); 

            if (robot.CurrentTarget != null)
            {
                Cell targetPos = new Cell((int)robot.CurrentTarget.Position.X, (int)robot.CurrentTarget.Position.Y);
                Cell robotPos = new Cell((int)robot.Position.X, (int)robot.Position.Y);

                int[,] mat = new int[planet.N, planet.M];
                for (int i = 0; i < planet.N; i++)
                {
                    for (int j = 0; j < planet.M; j++)
                    {
                        mat[i, j] = (planet.Tiles[i, j].Entity == null) ? 0 : 1;
                    }
                }

                robot.Directions = BfsLee(mat, robotPos, targetPos, planet);
            }
        }
        
        public Stack<Cell> BfsLee(int[,] mat, Cell src, Cell dest, Planet planet)
        {
            int rows = mat.GetLength(0);
            int cols = mat.GetLength(1);

            bool[,] visited = new bool[rows, cols];
            Cell[,] parent = new Cell[rows, cols];

            Queue<QueueNode> q = new Queue<QueueNode>();
            visited[src.X, src.Y] = true;
            q.Enqueue(new QueueNode(src, 0));

            int[] dRow = { -1, 1, 0, 0 };
            int[] dCol = { 0, 0, -1, 1 };

            // Check 4 possible adjacent tiles around the destination
            List<Cell> candidateTargets = new List<Cell>();

            for (int i = 0; i < 4; i++)
            {
                int adjX = dest.X + dRow[i];
                int adjY = dest.Y + dCol[i];

                if (CheckValid(adjX, adjY, planet) && mat[adjX, adjY] == 0)
                {
                    candidateTargets.Add(new Cell(adjX, adjY));
                }
            }

            while (q.Count > 0)
            {
                QueueNode current = q.Dequeue();
                Cell pt = current.Pt;

                // If current point is one of the adjacent valid target positions
                if (candidateTargets.Any(c => c.X == pt.X && c.Y == pt.Y))
                {
                    // Reconstruct path from src to pt
                    Stack<Cell> path = new Stack<Cell>();
                    Cell crawl = pt;

                    while (!(crawl.X == src.X && crawl.Y == src.Y))
                    {
                        path.Push(crawl);
                        crawl = parent[crawl.X, crawl.Y];
                    }

                    return path;
                }

                for (int i = 0; i < 4; i++)
                {
                    int newRow = pt.X + dRow[i];
                    int newCol = pt.Y + dCol[i];

                    if (CheckValid(newRow, newCol, planet) &&
                        !visited[newRow, newCol] &&
                        mat[newRow, newCol] == 0)
                    {
                        visited[newRow, newCol] = true;
                        parent[newRow, newCol] = pt;
                        q.Enqueue(new QueueNode(new Cell(newRow, newCol), current.Dist + 1));
                    }
                }
            }

            return new Stack<Cell>(); // No path found
        }
        
        public void Tick(Grid grid, Planet planet, ListBox listBox)
        {
            listBox.Items.Clear();
            planet.Draw(grid, planet, listBox);
        }
    }
}
