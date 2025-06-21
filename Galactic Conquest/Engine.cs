using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Galactic_Conquest.Entities;
using Galactic_Conquest.Entities.OrganicBeings;
using Galactic_Conquest.Entities.OrganicBeings.Defenders;
using Galactic_Conquest.Entities.Robots;
using Galactic_Conquest.ExtenstionMethods.Entities;

namespace Galactic_Conquest;

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

    public void CalculateDistanceToClosestEntity(CombatEntity sourceEntity, Planet planet)
    {
        double minDistance = double.MaxValue;
        Entity closestEntity = null;

        for (int i = 0; i < planet.N; i++)
        {
            for (int j = 0; j < planet.M; j++)
            {
                var target = planet.Tiles[i, j].Entity;

                if (IsValidTarget(sourceEntity, target))
                {
                    double manhattanDistance =
                        Math.Abs(sourceEntity.Position.X - i) + Math.Abs(sourceEntity.Position.Y - j);

                    if (manhattanDistance < minDistance)
                    {
                        minDistance = manhattanDistance;
                        closestEntity = target;
                    }
                }
            }
        }

        sourceEntity.CurrentTarget = closestEntity;
    }

    private bool IsValidTarget(Entity sourceEntity, Entity target)
    {
        if (target == null || !target.Alive ||
            (target.Position.X == sourceEntity.Position.X && target.Position.Y == sourceEntity.Position.Y))
        {
            return false;
        }

        if (sourceEntity is Robot healerRobot && healerRobot.GetType().Name == "Healer")
        {
            return target is Robot;
        }

        if (sourceEntity is Defender)
        {
            return target is Robot;
        }

        return IsTargetTypeValid(target);
    }

    private bool IsTargetTypeValid(Entity target)
    {
        string targetType = target.GetType().Name;
        return targetType == "Human" || targetType == "Animal" || targetType == "Crusader" || targetType == "Marksman";
    }

    public void SetPathToClosestEntity(CombatEntity combatEntity, Planet planet)
    {
        CalculateDistanceToClosestEntity(combatEntity, planet);

        if (combatEntity.CurrentTarget != null)
        {
            Cell targetPos = new Cell((int)combatEntity.CurrentTarget.Position.X,
                (int)combatEntity.CurrentTarget.Position.Y);
            Cell robotPos = new Cell((int)combatEntity.Position.X, (int)combatEntity.Position.Y);

            int[,] mat = new int[planet.N, planet.M];
            for (int i = 0; i < planet.N; i++)
            {
                for (int j = 0; j < planet.M; j++)
                {
                    mat[i, j] = (planet.Tiles[i, j].Entity == null) ? 0 : 1;
                }
            }

            combatEntity.Directions = BfsLee(mat, robotPos, targetPos, planet);
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

            if (candidateTargets.Any(c => c.X == pt.X && c.Y == pt.Y))
            {
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

        return new Stack<Cell>();
    }

    public bool Tick(Canvas canvas, Planet planet)
    {
        planet.Turn++;

        if (TryShowVictoryWindow(planet))
            return true;

        for (int i = 0; i < planet.N; i++)
        {
            for (int j = 0; j < planet.M; j++)
            {
                var entity = planet.Tiles[i, j].Entity;
                if (entity is CombatEntity combatEntity && entity.Alive)
                {
                    ProcessCombatEntity(combatEntity, planet);
                }
            }
        }

        planet.Draw(canvas, true);
        return false;
    }

    private void HasAdjacentOrganicBeing(Defender entity, Planet planet)
    {
        int x = (int)entity.Position.X;
        int y = (int)entity.Position.Y;

        for (int i = 0; i < 4; i++)
        {
            int newX = x + _rowNum[i];
            int newY = y + _colNum[i];

            if (CheckValid(newX, newY, planet))
            {
                var adjacentEntity = planet.Tiles[newX, newY].Entity;
                if (adjacentEntity != null && (adjacentEntity is Human || adjacentEntity is Animal))
                {
                    entity.Protect(adjacentEntity);
                    return;
                }
            }
        }
    }

    private void ProcessCombatEntity(CombatEntity combatEntity, Planet planet)
    {
        if (combatEntity.LastMovedTurn >= planet.Turn)
            return;

        if (combatEntity is Defender defender)
        {
            HasAdjacentOrganicBeing(defender, planet);
        }

        SetPathToClosestEntity(combatEntity, planet);

        if (CanEntityInteract(combatEntity))
        {
            combatEntity.InteractWithTarget();
        }
        else
        {
            combatEntity.Move(planet);
        }
    }

    private bool TryShowVictoryWindow(Planet planet)
    {
        var remainingFaction = GetRemainingFaction(planet);
        if (remainingFaction != null)
        {
            string factionDisplayName = remainingFaction.Value.ToString();
            var victoryWindow = new VictoryWindow.VictoryWindow(factionDisplayName);
            victoryWindow.ShowDialog();
            return true;
        }

        return false;
    }

    public Entity.FactionType? GetRemainingFaction(Planet planet)
    {
        var aliveFactions = new HashSet<Entity.FactionType>();

        for (int i = 0; i < planet.N; i++)
        {
            for (int j = 0; j < planet.M; j++)
            {
                var entity = planet.Tiles[i, j].Entity;
                if (entity != null && entity.Alive)
                {
                    aliveFactions.Add(entity.Faction);
                }
            }
        }

        return aliveFactions.Count == 1 ? aliveFactions.First() : null;
    }

    private bool CanEntityInteract(CombatEntity combatEntity)
    {
        return combatEntity switch
        {
            Robot robot => robot.CheckIfInteractionIsPossible(),
            Defender defender => defender.CheckIfTargetInRange(),
            _ => false
        };
    }
}