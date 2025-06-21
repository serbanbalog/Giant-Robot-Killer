namespace Galactic_Conquest.Entities.OrganicBeings.Defenders
{
    public class Crusader : Defender
    {
        internal Crusader(int maxHealthPoints, int defensePower, int shieldCapacity, int range, int attackPower, FactionType faction)
            : base(maxHealthPoints, defensePower, shieldCapacity, range, attackPower, faction)
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
                else
                {
                    entity.HealthPoints += 2;
                }
            }
        }

        public override void InteractWithTarget()
        {
            if (CheckIfTargetInRange() && CurrentTarget.Alive && CurrentTarget.HealthPoints >= AttackPower)
            {
                CurrentTarget.HealthPoints -= AttackPower;

                CurrentShield = CalculateShieldRegeneration(AttackPower / 2);
            }
            else if (CheckIfInteractionIsPossible(CurrentTarget) && CurrentTarget.HealthPoints < AttackPower)
            {
                int excessDamage = AttackPower - CurrentTarget.HealthPoints;
                CurrentTarget.HealthPoints = 0;

                CurrentShield = CalculateShieldRegeneration(excessDamage * 2);
            }
        }

        

        private bool CheckIfInteractionIsPossible(Entity CurrentTarget)
        {
            return CurrentTarget != null && CurrentTarget.Faction != Faction && Range > 0;
        }
    }
}