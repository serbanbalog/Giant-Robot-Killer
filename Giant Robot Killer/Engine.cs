using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Giant_Robot_Killer
{
    struct queueNode
    {
        public Cell pt;
        public int dist;
    };
    public struct Cell
    {
        public int x;
        public int y;
    };
    public class Engine
    {
        int[] rowNum = { -1, 0, 0, 1 };
        int[] colNum = { 0, -1, 1, 0 };


        bool checkValid(int row, int col, Planet planet)
        {
            return ((row >= 0) && (row < planet.n) && (col >= 0) && (col < planet.m));
        }
        public void CalculateDistanceToClosestEntity(Robot robot, Planet planet)
        {
            double min = 100;
            for (int i = 0; i < planet.n; i++)
            {
                for (int j = 0; j < planet.m; j++)
                {
                    if (planet.Tiles[i, j].Entity != null && planet.Tiles[i, j].Entity.Position != robot.Position && (planet.Tiles[i, j].Entity.GetType().Name == "Human" || planet.Tiles[i, j].Entity.GetType().Name == "Animal"))
                    {
                        /*if (min < robot.Range && !((i < robot.Position.X - robot.Range) || (i > robot.Position.X + robot.Range) ||
                            (j < robot.Position.Y - robot.Range) || (j > robot.Position.Y + robot.Range)))
                            break;
                        else
                        {*/
                        double temp = (planet.Tiles[i, j].Entity.Position.X + planet.Tiles[i, j].Entity.Position.Y) / 2;
                        if (min > temp)
                        {
                            min = temp;
                            robot.CurrentTarget = planet.Tiles[i, j].Entity;
                        }
                        /*  }*/
                    }
                }
            }
        }
        public void SetPathToClosestEntity( Robot robot, Planet planet)
        {
            if (robot.CurrentTarget == null || robot.CurrentTarget.Alive == false)
                CalculateDistanceToClosestEntity(robot, planet);

            if (robot.CurrentTarget != null)
            {
                Cell targetPos;
                targetPos.x = (int)robot.CurrentTarget.Position.X;
                targetPos.y = (int)robot.CurrentTarget.Position.Y;
                Cell robotPos;
                robotPos.x = (int)Math.Abs(robot.Position.X);
                robotPos.y = (int)Math.Abs(robot.Position.Y);
                int[,] mat = new int[30, 30];
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    for (int j = 0; j < mat.GetLength(1); j++)
                    {
                        if (planet.Tiles[i, j].Entity == null)
                            mat[i, j] = 0;
                        else
                            mat[i, j] = 1;
                    }
                }
                robot.Directions = bfsLee(mat, robotPos, targetPos, planet);
            }
        }
        Stack<Cell> bfsLee(int[,] mat, Cell src, Cell dest, Planet planet)
        {
            Stack<Cell> toR = new Stack<Cell>();
            if (src.x == dest.x && src.y ==dest.y)
                return new Stack<Cell>();

            bool[,] visited = new bool[mat.GetLength(0), mat.GetLength(1)];

            visited[src.x, src.y] = true;

            Queue<queueNode> q = new Queue<queueNode>();

            queueNode s;
            s.pt = src;
            s.dist = 0;
            q.Enqueue(s);
            int minDist = 0;

            while (q.Count() > 0)
            {
                queueNode curr = q.Peek();
                Cell pt = curr.pt;
                Cell temp = new Cell();

                if (pt.x == dest.x && pt.y == dest.y)
                    return toR;


                q.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    int row = pt.x + rowNum[i];
                    int col = pt.y + colNum[i];

                    if (checkValid(row, col, planet) && !visited[row, col])
                    {
                        visited[row, col] = true;
                        queueNode Adjcell;
                        Adjcell.pt.x = row;
                        Adjcell.pt.y = col;
                        Adjcell.dist = curr.dist + 1;

                        temp.x = rowNum[i];
                        temp.y = colNum[i];

                        q.Enqueue(Adjcell);
                    }
                }
                if(minDist < q.Peek().dist)
                {
                    minDist = q.Peek().dist;
                    toR.Push(temp);
                }
            }
            return new Stack<Cell>();
        }
        public void Tick(Canvas canvas, Planet planet, ListBox listBox)
        {
            listBox.Items.Clear();
            planet.Draw(canvas, planet, listBox);
        }
    }
}
