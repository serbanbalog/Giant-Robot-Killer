namespace Giant_Robot_Killer.Entities.Robots
{
    public abstract class Robot : Entity
    {
        public int Power { get; private set; }
        public int MaxMagazineCapacity { get; private set; }
        public int CurrentMagazineCapacity { get; set; }
        public Entity CurrentTarget { get; set; }
        public int Range { get; private set; }
        public Robot(int maxHealthPoints, int power, int maxMagazineCapacity, int range, FactionType faction) : base(maxHealthPoints, faction)
        {
            Power = power;
            MaxMagazineCapacity = maxMagazineCapacity;
            CurrentMagazineCapacity = maxMagazineCapacity;
            Range = range;
        }
        public abstract void InteractWithTarget(Robot robot);
      
        public int CalculateHealing(Entity entity, int healingValue)
        {
            if (CheckHealingOverflow(entity, healingValue))
                return entity.MaxHealthPoints;
            else
                return entity.HealthPoints + healingValue;
        }
        private bool CheckHealingOverflow(Entity entity, int healingValue)
        {
            if ((entity.HealthPoints + healingValue) > entity.MaxHealthPoints)
                return true;
            return false;
        }
        
    }
}
