using System;
using Giant_Robot_Killer.Entities.Robots;

namespace Giant_Robot_Killer.ExtenstionMethods.Entities;

public static class RobotExtensionMethods
{
    public static bool CheckIfInteractionIsPossible(this Robot robot)
    {

        if (robot.CurrentMagazineCapacity > 0 && robot.CheckIfTargetInRange())
        {
            robot.CurrentMagazineCapacity--;
            return true;
        }

        if (robot.CurrentMagazineCapacity == 0)
            robot.CurrentMagazineCapacity = robot.MaxMagazineCapacity;
        
        return false;
    }
}