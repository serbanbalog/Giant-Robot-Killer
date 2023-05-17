using System.Drawing;

namespace Giant_Robot_Killer
{
    public abstract class Defender : Entity
    {
        public Defender(int maxHealthPoints, FactionType faction) : base(maxHealthPoints, faction)
        {
        }
    }
}