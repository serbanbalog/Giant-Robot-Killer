using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giant_Robot_Killer
{
    public abstract class Entity
    {
        public enum FactionType
        {
            Enemy,
            Friendly
        }
        public FactionType Faction { get; private set; }
        public int MaxHealthPoints { get; private set; }
        public int HealthPoints { get; set; }
        public Point Position { get; set; }
        public bool Alive { get; set; }
        public Entity(int maxHealthPoints, FactionType faction) 
        {
            this.MaxHealthPoints = maxHealthPoints;
            this.HealthPoints = maxHealthPoints;
            this.Faction = faction;
            Alive = true;
        }
        public void SetOnMap(Point position)
        {
            Position = position;
        }
        public abstract void Draw(Graphics handler);
        public 
 
    }
}
