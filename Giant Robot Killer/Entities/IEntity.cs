using System.Drawing;

namespace Giant_Robot_Killer.Entities
{
    public interface IEntity
    {
        bool Alive { get; }
        Entity.FactionType Faction { get; }
        int HealthPoints { get; set; }
        int MaxHealthPoints { get; }
        PointF Position { get; set; }
        PointF AbsPosition { get; set; }
    }
}