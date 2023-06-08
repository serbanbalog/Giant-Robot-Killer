using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Giant_Robot_Killer
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
            this.Power = power;
            this.MaxMagazineCapacity = maxMagazineCapacity;
            this.CurrentMagazineCapacity = maxMagazineCapacity;
            this.Range = range;
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
