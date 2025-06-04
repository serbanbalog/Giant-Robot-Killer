using Giant_Robot_Killer.ExtenstionMethods;

namespace Giant_Robot_Killer.Entities.Robots.HealerRobot
{
    public class Healer : Robot
    {
        internal Healer(int maxHealthPoints, int power, int maxMagazineCapacity, int range, FactionType faction) : base(maxHealthPoints, power, maxMagazineCapacity, range, faction)
        {
        }
        public override void InteractWithTarget(Robot robot)
        {
            if(robot.CheckIfInteractionIsPossible())
            {
                robot.CurrentTarget.HealthPoints = CalculateHealing(robot.CurrentTarget, robot.Power);
                robot.HealthPoints = CalculateHealing(robot, robot.Power);
            }
        }
    }
}
