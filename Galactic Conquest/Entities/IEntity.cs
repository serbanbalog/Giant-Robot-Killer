using System.Drawing;

namespace Galactic_Conquest.Entities;

public interface IEntity
{
    bool Alive { get; }
    Entity.FactionType Faction { get; }
    int HealthPoints { get; set; }
    int MaxHealthPoints { get; }
    PointF Position { get; set; }
    PointF AbsPosition { get; set; }
}