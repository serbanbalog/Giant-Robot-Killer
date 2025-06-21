using System;
using Galactic_Conquest.Entities.Robots;

namespace Galactic_Conquest.ExtenstionMethods.Entities;

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