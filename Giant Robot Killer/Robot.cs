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
        private int Power { get; set; }
        private int MaxMagazineCapacity { get; set; }
        private int CurrentMagazineCapacity { get; set; }
        private Defender CurrentTarget { get; set; }
        private int Range { get; set; }
        public Robot(int maxHealthPoints, int power, int maxMagazineCapacity, int currentMagazineCapacity, Defender currentTarget, int range, FactionType faction) : base(maxHealthPoints, faction)
        {
            Power = power;
            MaxMagazineCapacity = maxMagazineCapacity;
            CurrentMagazineCapacity = currentMagazineCapacity;
            CurrentTarget = currentTarget;
            Range = range;
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
                robot.CurrentMagazineCapacity--;
            }
            else if (robot.CurrentMagazineCapacity == 0)
                robot.CurrentMagazineCapacity = MaxMagazineCapacity;
        }
        private int CalculateHealing(Entity entity, int healingValue)
        {
            if (CheckHealingOverflow(entity, healingValue))
                return entity.MaxHealthPoints;
            else
                return entity.HealthPoints + healingValue;
        }
        private bool CheckHealingOverflow(Entity entity, int healingValue)
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
