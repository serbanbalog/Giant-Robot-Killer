using System;
using Giant_Robot_Killer.Entities.Robots;

namespace Giant_Robot_Killer.ExtenstionMethods.Entities
{
    public static class RobotExenstionMethods
    {
        public static bool CheckIfInteractionIsPossible(this Robot robot)
        {

            if (robot.CurrentMagazineCapacity > 0 && robot.CheckIfTargetInRange())
            {
                robot.CurrentMagazineCapacity--;
                return true;
            }
            else if (robot.CurrentMagazineCapacity == 0)
                robot.CurrentMagazineCapacity = robot.MaxMagazineCapacity;
            return false;
        }
        public static bool CheckIfTargetInRange(this Robot robot)
        {   
            if (robot.CurrentTarget.Alive == true)
            {
                float distX = Math.Abs(robot.CurrentTarget.Position.X - robot.Position.X);
                float distY = Math.Abs(robot.CurrentTarget.Position.Y - robot.Position.Y);
                if (Math.Pow(distX , 2) + Math.Pow((distY), 2) <= Math.Pow(robot.Range, 2))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
