using Giant_Robot_Killer.Robots;
using static Giant_Robot_Killer.Entity;
using System;

namespace Giant_Robot_Killer
{
    public class RobotFactory : IRobotFactory
    {

        public RobotFactory()
        {
        }

        public Healer CreateHealerRobot()
        {
            Random rnd = new Random();
            int maxHealthPoints = rnd.Next(50, 80);
            int power = rnd.Next(20, 30);
            int maxMagazineCapacity = 5;
            int range = 10;
            FactionType faction = FactionType.Friendly;
            return new Healer(maxHealthPoints, power, maxMagazineCapacity, range, faction);
        }
        public Executioner CreateExecutionerRobot()
        {
            Random rnd = new Random();
            int maxHealthPoints = rnd.Next(30, 35);
            int power = 0;
            int maxMagazineCapacity = 0;
            int range = 5;
            FactionType faction = FactionType.Enemy;
            return new Executioner(maxHealthPoints, power, maxMagazineCapacity, range, faction);
        }
        public Gunslinger CreateGunslingerRobot()
        {
            Random rnd = new Random();
            int maxHealthPoints = rnd.Next(70, 85);
            int power = rnd.Next(20, 30);
            int maxMagazineCapacity = 4;
            int range = 8;
            FactionType faction = FactionType.Enemy;
            return new Gunslinger(maxHealthPoints, power, maxMagazineCapacity, range, faction);
        }
    }
}
