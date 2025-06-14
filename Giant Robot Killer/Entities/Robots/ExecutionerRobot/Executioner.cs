using Giant_Robot_Killer.ExtenstionMethods;
using Giant_Robot_Killer.ExtenstionMethods.Entities;

namespace Giant_Robot_Killer.Entities.Robots.ExecutionerRobot
{
    public class Executioner : Robot
    {
        
        internal Executioner(int maxHealthPoints, int power, int maxMagazineCapacity, int range, FactionType faction) : base(maxHealthPoints, power, maxMagazineCapacity, range, faction)
        {
        }
        public override void InteractWithTarget(Robot robot)
        {
            if (robot.CheckIfTargetInRange() && robot.CurrentTarget.HealthPoints <= 25)
            {
                int healing = 25 - robot.CurrentTarget.HealthPoints;
                robot.CurrentTarget.HealthPoints = 0;
                robot.HealthPoints += CalculateHealing(robot, healing);
            }
            else if(robot.HealthPoints - 1 > 0)
            {
                robot.HealthPoints--;
            }
            else if(robot.HealthPoints - 1 == 0)
            {
                robot.HealthPoints--;
            }
        }
    }
}
