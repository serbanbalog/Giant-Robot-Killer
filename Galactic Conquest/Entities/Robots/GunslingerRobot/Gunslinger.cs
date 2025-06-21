using Galactic_Conquest.ExtenstionMethods.Entities;

namespace Galactic_Conquest.Entities.Robots.GunslingerRobot;

public class Gunslinger : Robot
{
    internal Gunslinger(int maxHealthPoints, int attackPower, int maxMagazineCapacity, int range, FactionType faction) : base(maxHealthPoints, attackPower, maxMagazineCapacity, range, faction)
    {
    }
    public override void InteractWithTarget()
    {
        if (CheckIfTargetInRange() && CurrentTarget.Alive && CurrentTarget.HealthPoints >= AttackPower)
        {
            CurrentTarget.HealthPoints -= AttackPower;
            HealthPoints = CalculateHealing(this, AttackPower);
        }
        else if (this.CheckIfInteractionIsPossible() && CurrentTarget.HealthPoints < AttackPower)
        {
            int healing = AttackPower - CurrentTarget.HealthPoints;
            CurrentTarget.HealthPoints = 0;
            HealthPoints = CalculateHealing(this, healing);
        }
        else if (HealthPoints - 1 >= 0)
        {
            HealthPoints--;
        }
    }
}