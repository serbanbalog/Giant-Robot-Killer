using System;

namespace Galactic_Conquest.Entities;

public abstract class CombatEntity : Entity
{
    public int Range { get; }
    public Entity CurrentTarget { get; set; }
    public int AttackPower { get; private set; }

    protected CombatEntity(int maxHealthPoints, int attackPower, int range, FactionType faction) : base(maxHealthPoints,
        faction)
    {
        Range = range;
        CurrentTarget = null;
        AttackPower = attackPower;
    }
    public abstract void InteractWithTarget();

    public bool CheckIfTargetInRange()
    {
        if (CurrentTarget != null && CurrentTarget.Alive)
        {
            float distX = Math.Abs(CurrentTarget.Position.X - Position.X);
            float distY = Math.Abs(CurrentTarget.Position.Y - Position.Y);
            if (Math.Pow(distX, 2) + Math.Pow((distY), 2) <= Math.Pow(Range, 2))
            {
                return true;
            }
        }

        return false;
    }
}