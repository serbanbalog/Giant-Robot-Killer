using System.Drawing;

namespace Giant_Robot_Killer
{
    public class Defender : Entity
    {
        public Defender(int maxHealthPoints, FactionType faction) : base(maxHealthPoints, faction)
        {
        }

        public override void Draw(Graphics handler)
        {
            throw new System.NotImplementedException();
        }
    }
}