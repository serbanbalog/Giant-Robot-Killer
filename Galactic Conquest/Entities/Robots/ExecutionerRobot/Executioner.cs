using Galactic_Conquest.ExtenstionMethods;
using Galactic_Conquest.ExtenstionMethods.Entities;

namespace Galactic_Conquest.Entities.Robots.ExecutionerRobot;

public class Executioner : Robot
{
        
    internal Executioner(int maxHealthPoints, int attackPower, int maxMagazineCapacity, int range, FactionType faction) : base(maxHealthPoints, attackPower, maxMagazineCapacity, range, faction)
    {
    }
    public override void InteractWithTarget()
    {
        if (CheckIfTargetInRange() && CurrentTarget.HealthPoints <= 25)
        {
            int healing = 25 - CurrentTarget.HealthPoints;
            CurrentTarget.HealthPoints = 0;
            HealthPoints += CalculateHealing(this, healing);
        }
        else if(HealthPoints - 1 >= 0)
        {
            HealthPoints--;
        }
    }
}