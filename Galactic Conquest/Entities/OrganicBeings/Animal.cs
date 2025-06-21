namespace Galactic_Conquest.Entities.OrganicBeings;

public enum AnimalTypes
{
    Dog,
    Cat
}
public class Animal : Entity
{

    public AnimalTypes AnimalType { get; private set; }
    internal Animal(int maxHealthPoints, FactionType faction, AnimalTypes animalType) : base(maxHealthPoints, faction)
    {
        AnimalType = animalType;
    }      
}