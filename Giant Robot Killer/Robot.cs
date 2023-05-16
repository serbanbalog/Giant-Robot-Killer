using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        public Robot(int maxHealthPoints, RobotType type, int power, int maxMagazineCapacity, int currentMagazineCapacity, Defender currentTarget, int range, FactionType faction) : base(maxHealthPoints, faction)
        {
            Type = type;
            Power = power;
            MaxMagazineCapacity = maxMagazineCapacity;
            CurrentMagazineCapacity = currentMagazineCapacity;
            CurrentTarget = currentTarget;
            Range = range;
        }
        public override void Draw(Graphics handler)
        {
            throw new NotImplementedException();
        }
        public void InteractWithTarget(Defender target, Robot robot)
        {
            if(CheckIfTargetInRange(target, robot) && robot.CurrentMagazineCapacity > 0)
            {
                if (robot.Faction == FactionType.Friendly)
                {
                    target.HealthPoints = CalculateHealing(target, robot.Power); 
                    robot.HealthPoints = CalculateHealing(robot, robot.Power);
                }
                else if (target.Faction == FactionType.Enemy)
                {
                    if ((target.HealthPoints - robot.Power) <= 0)
                    {
                        robot.HealthPoints = CalculateHealing(robot, robot.Power);
                        target.Alive = false;
                        target.HealthPoints = 0;
                    }
                    else
                        target.HealthPoints -= robot.Power;
                }
            }
            else if (robot.CurrentMagazineCapacity == 0)
                robot.CurrentMagazineCapacity = MaxMagazineCapacity;
        }
        public int CalculateHealing(Entity entity, int healingValue)
        {
            if (CheckHealingOverflow(entity, healingValue))
                return entity.MaxHealthPoints;
            else
                return entity.HealthPoints + healingValue;

        }
        public bool CheckHealingOverflow(Entity entity, int healingValue)
        {
            if ((entity.HealthPoints + healingValue) > entity.MaxHealthPoints)
                return true;
            return false;
        }
        private bool CheckIfTargetInRange(Defender target, Robot robot)
        {
            if (target.Alive == true)
            {
                int distX = Math.Abs(target.Position.X - robot.Position.X);
                int distY = Math.Abs(target.Position.Y - robot.Position.Y);
                if ((distX + distY) <= robot.Range)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
