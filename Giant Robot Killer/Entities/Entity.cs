using System.Collections.Generic;
using System.Drawing;

namespace Giant_Robot_Killer.Entities
{
    public abstract class Entity : IEntity
    {
        public enum FactionType
        {
            Enemy,
            Friendly
        }
        public FactionType Faction { get; private set; }
        public int MaxHealthPoints { get; private set; }
        public int HealthPoints { get; set; }
        public PointF Position { get; set; }
        public PointF AbsPosition { get; set; }
        public bool Alive => HealthPoints > 0 ? true : false;
        public Stack<Cell> Directions { get; set; }
        public int LastMovedTurn { get; set; }
        public Entity(int maxHealthPoints, FactionType faction)
        {
            MaxHealthPoints = maxHealthPoints;
            HealthPoints = maxHealthPoints;
            Faction = faction;
            Position = new PointF(0, 0);
            Directions = new Stack<Cell>();
        }
    }
}
