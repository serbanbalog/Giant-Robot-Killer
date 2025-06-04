using System;
using Giant_Robot_Killer.Entities.Robots.ExecutionerRobot;
using Giant_Robot_Killer.Entities.Robots.GunslingRobot;
using Giant_Robot_Killer.Entities.Robots.HealerRobot;
using static Giant_Robot_Killer.Entities.Entity;

namespace Giant_Robot_Killer.Entities.Robots.RobotFactory
{
    public class RobotFactory : IRobotFactory
    {
        private Random _rnd;

        public RobotFactory()
        {
            _rnd = new Random();
        }
        public Robot GenerateRandomRobot()
        {
            switch (_rnd.Next(3))
            {
                case 0:
                    return CreateHealerRobot();
                case 1:
                    return CreateGunslingerRobot();
                case 2:
                    return CreateExecutionerRobot();
                default:
                    return CreateGunslingerRobot();
            }
        }
        private Healer CreateHealerRobot()
        {
            int maxHealthPoints = _rnd.Next(50, 80);
            int power = _rnd.Next(20, 30);
            int maxMagazineCapacity = 5;
            int range = 10;
            FactionType faction = FactionType.Friendly;
            return new Healer(maxHealthPoints, power, maxMagazineCapacity, range, faction);
        }
        private Executioner CreateExecutionerRobot()
        {
            int maxHealthPoints = _rnd.Next(30, 35);
            int power = 0;
            int maxMagazineCapacity = 0;
            int range = 1;
            FactionType faction = FactionType.Enemy;
            return new Executioner(maxHealthPoints, power, maxMagazineCapacity, range, faction);
        }
        private Gunslinger CreateGunslingerRobot()
        {
            int maxHealthPoints = _rnd.Next(70, 85);
            int power = _rnd.Next(20, 30);
            int maxMagazineCapacity = 4;
            int range = 1;
            FactionType faction = FactionType.Enemy;
            return new Gunslinger(maxHealthPoints, power, maxMagazineCapacity, range, faction);
        }
    }
}
