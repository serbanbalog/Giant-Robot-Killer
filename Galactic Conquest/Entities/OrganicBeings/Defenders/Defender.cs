namespace Galactic_Conquest.Entities.OrganicBeings.Defenders
{
    public abstract class Defender : CombatEntity
    {
        public int DefensePower { get; private set; }
        private int ShieldCapacity { get; set; }
        protected int CurrentShield { get; set; }

        public Defender(int maxHealthPoints, int defensePower, int shieldCapacity, int range, int attackPower,
            FactionType faction) : base(maxHealthPoints, attackPower, range, faction)
        {
            DefensePower = defensePower;
            ShieldCapacity = shieldCapacity;
            CurrentShield = shieldCapacity;
            CurrentTarget = null;
        }

        public abstract void Protect(Entity entity);

        public int CalculateShieldRegeneration(int regenerationValue)
        {
            if (CheckShieldOverflow(regenerationValue))
                return ShieldCapacity;

            return CurrentShield + regenerationValue;
        }

        private bool CheckShieldOverflow(int regenerationValue)
        {
            return (CurrentShield + regenerationValue) > ShieldCapacity;
        }
    }
}