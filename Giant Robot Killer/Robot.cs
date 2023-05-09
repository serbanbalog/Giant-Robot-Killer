using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Giant_Robot_Killer
{
    public abstract class Robot
    {
        public enum RobotType
        {
            Friendly,
            Hostile
        }
        public int MaxHealthPoints { get; private set; } 
        public int HealthPoints { get; set; }
        public RobotType Type { get; private set; }
        public int Power { get; private set; }
        public int MaxMagazineCapacity { get; private set; }
        public int CurrentMagazineCapacity { get; set; }
        public Point Position { get; set; }
        public Point CurrentTarget { get; set; }
        public int Range { get; private set; }
       
    }
}
