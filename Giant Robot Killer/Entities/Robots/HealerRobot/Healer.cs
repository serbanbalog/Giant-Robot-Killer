using Giant_Robot_Killer.ExtenstionMethods.Entities;

namespace Giant_Robot_Killer.Entities.Robots.HealerRobot;

public class Healer : Robot
{
    internal Healer(int maxHealthPoints, int attackPower, int maxMagazineCapacity, int range, FactionType faction) : base(maxHealthPoints, attackPower, maxMagazineCapacity, range, faction)
    {
    }
    public override void InteractWithTarget()
    {
        if(this.CheckIfInteractionIsPossible())
        {
            CurrentTarget.HealthPoints = CalculateHealing(CurrentTarget, AttackPower);
            HealthPoints = CalculateHealing(this, AttackPower);
        }
    }
}