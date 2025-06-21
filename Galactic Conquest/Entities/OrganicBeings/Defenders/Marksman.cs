using System;

namespace Galactic_Conquest.Entities.OrganicBeings.Defenders;

public class Marksman : Defender
{
    internal Marksman(int maxHealthPoints, int defensePower, int shieldCapacity, int range, int attackPower,
        FactionType faction) : base(maxHealthPoints, defensePower, shieldCapacity, range, attackPower, faction)
    {
    }

    public override void Protect(Entity entity)
    {
        if (entity != null && entity.Alive && entity.Faction == Faction)
        {
            if (CurrentShield > 0)
            {
                entity.HealthPoints += 5;
                CurrentShield -= 5;
            }
        }
    }

    private bool CheckIfInteractionIsPossible()
    {
        return CurrentTarget != null && CurrentTarget.Faction != Faction && Range > 0;
    }

    public override void InteractWithTarget()
    {
        if (CheckIfTargetInRange() && CurrentTarget.Alive && CurrentTarget.HealthPoints >= AttackPower)
        {
            CurrentTarget.HealthPoints -= AttackPower;
        }
        else if (CheckIfInteractionIsPossible() && CurrentTarget.HealthPoints < AttackPower)
        {
            int excessDamage = AttackPower - CurrentTarget.HealthPoints;
            CurrentTarget.HealthPoints = 0;
            HealthPoints += CalculateShieldRegeneration(excessDamage);
        }
    }
}