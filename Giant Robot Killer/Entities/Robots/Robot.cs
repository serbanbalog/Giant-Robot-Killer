namespace Giant_Robot_Killer.Entities.Robots;

public abstract class Robot : CombatEntity
{
    public int MaxMagazineCapacity { get; private set; }
    public int CurrentMagazineCapacity { get; set; }

    public Robot(int maxHealthPoints, int attackPower, int maxMagazineCapacity, int range, FactionType faction) : base(
        maxHealthPoints, attackPower, range, faction)
    {
        MaxMagazineCapacity = maxMagazineCapacity;
        CurrentMagazineCapacity = maxMagazineCapacity;
    }

    public int CalculateHealing(Entity entity, int healingValue)
    {
        if (CheckHealingOverflow(entity, healingValue))
            return entity.MaxHealthPoints;

        return entity.HealthPoints + healingValue;
    }

    private bool CheckHealingOverflow(Entity entity, int healingValue)
    {
        if ((entity.HealthPoints + healingValue) > entity.MaxHealthPoints)
            return true;
        return false;
    }
}