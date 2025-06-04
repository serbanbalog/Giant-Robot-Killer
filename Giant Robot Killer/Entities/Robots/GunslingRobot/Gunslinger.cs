using Giant_Robot_Killer.ExtenstionMethods;

namespace Giant_Robot_Killer.Entities.Robots.GunslingRobot
{
    public class Gunslinger : Robot
    {
        internal Gunslinger(int maxHealthPoints, int power, int maxMagazineCapacity, int range, FactionType faction) : base(maxHealthPoints, power, maxMagazineCapacity, range, faction)
        {
        }
        public override void InteractWithTarget(Robot robot)
        {
            if (robot.CheckIfTargetInRange() && robot.CurrentTarget.Alive && robot.CurrentTarget.HealthPoints >= robot.Power)
            {
                robot.CurrentTarget.HealthPoints -= robot.Power;
                robot.HealthPoints += CalculateHealing(robot, robot.Power);
            }
            else if (robot.CheckIfInteractionIsPossible() && robot.CurrentTarget.HealthPoints < robot.Power)
            {
                int healing = robot.Power - robot.CurrentTarget.HealthPoints;
                robot.CurrentTarget.HealthPoints = 0;
                robot.HealthPoints += CalculateHealing(robot, healing);
            }
            else if (robot.HealthPoints - 1 > 0)
            {
                robot.HealthPoints--;
            }
            else if (robot.HealthPoints - 1 == 0)
            {
                robot.HealthPoints--;
            }
        }
    }
}
