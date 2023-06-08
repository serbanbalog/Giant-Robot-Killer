using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Giant_Robot_Killer
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
        public bool Alive { get; set; }
        public Stack<Cell> Directions { get; set; }
        public Entity(int maxHealthPoints, FactionType faction)
        {
            this.MaxHealthPoints = maxHealthPoints;
            this.HealthPoints = maxHealthPoints;
            this.Faction = faction;
            this.Alive = true;
            this.Position = new PointF(0, 0);
            this.Directions = new Stack<Cell>();
        }

    }
}
