using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giant_Robot_Killer
{
    public abstract class Robot : Entity
    {
        public enum RobotType
        {
            Healer,
            Executioner,
            Marksman
        }
        public RobotType Type { get; private set; }
        public int Power { get; private set; }
        public int MaxMagazineCapacity { get; private set; }
        public int CurrentMagazineCapacity { get; set; }
        public Defender CurrentTarget { get; set; }
        public int Range { get; private set; }
        public Robot(int maxHealthPoints, Point position, RobotType type, int power, int maxMagazineCapacity, int currentMagazineCapacity, Defender currentTarget, int range): base(maxHealthPoints)
        {
            Type = type;
            Power = power;
            MaxMagazineCapacity = maxMagazineCapacity;
            CurrentMagazineCapacity = currentMagazineCapacity;
            CurrentTarget = currentTarget;
            Range = range;
        }
    }
}
