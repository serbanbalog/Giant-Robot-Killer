using Giant_Robot_Killer.ExtenstionMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Giant_Robot_Killer
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
