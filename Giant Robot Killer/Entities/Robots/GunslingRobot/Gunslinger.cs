using Giant_Robot_Killer.ExtenstionMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace Giant_Robot_Killer.Robots
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
                robot.CurrentTarget.Alive = false;
                robot.HealthPoints += CalculateHealing(robot, healing);
            }
            else if (robot.HealthPoints - 1 > 0)
            {
                robot.HealthPoints--;
            }
            else if (robot.HealthPoints - 1 == 0)
            {
                robot.HealthPoints--;
                robot.Alive = false;
            }
        }
    }
}
