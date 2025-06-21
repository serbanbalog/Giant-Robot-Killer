using System;
using Galactic_Conquest.Entities.Robots.ExecutionerRobot;
using Galactic_Conquest.Entities.Robots.GunslingerRobot;
using Galactic_Conquest.Entities.Robots.HealerRobot;
using static Galactic_Conquest.Entities.Entity;

namespace Galactic_Conquest.Entities.Robots.RobotFactory;

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
        int range = 1;
        Entity.FactionType faction = Entity.FactionType.Enemy;
        return new Healer(maxHealthPoints, power, maxMagazineCapacity, range, faction);
    }
    private Executioner CreateExecutionerRobot()
    {
        int maxHealthPoints = _rnd.Next(30, 35);
        int power = 0;
        int maxMagazineCapacity = 0;
        int range = 1;
        Entity.FactionType faction = Entity.FactionType.Enemy;
        return new Executioner(maxHealthPoints, power, maxMagazineCapacity, range, faction);
    }
    private Gunslinger CreateGunslingerRobot()
    {
        int maxHealthPoints = _rnd.Next(70, 85);
        int power = _rnd.Next(20, 30);
        int maxMagazineCapacity = 4;
        int range = _rnd.Next(3, 5);
        Entity.FactionType faction = Entity.FactionType.Enemy;
        return new Gunslinger(maxHealthPoints, power, maxMagazineCapacity, range, faction);
    }
}