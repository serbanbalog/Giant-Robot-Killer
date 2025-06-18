using System;
using Giant_Robot_Killer.Entities.OrganicBeings.Defenders;

namespace Giant_Robot_Killer.Entities.OrganicBeings.OrganicBeingCreator;

public class OrganicBeingCreator : IOrganicBeingCreator
{
    private Random _rnd;

    public OrganicBeingCreator()
    {
        _rnd = new Random();
    }

    public Entity CreateOrganicBeing()
    {
        switch (_rnd.Next(4))
        {
            case 0:
                return CreateHuman();
            case 1:
                return CreateAnimal();
            case 2:
                return CreateMarksman();
            case 3:
                return CreateCrusader();
            default:
                return CreateHuman();
        }
    }

    private Human CreateHuman()
    {
        int maxHealthPoints = _rnd.Next(90, 100);
        return new Human(maxHealthPoints, Entity.FactionType.Friendly);
    }

    private Animal CreateAnimal()
    {
        int maxHealthPoints = _rnd.Next(20, 35);
        AnimalTypes animalType;
        int temp = _rnd.Next(2);

        switch (temp)
        {
            case 0:
                animalType = AnimalTypes.Dog;
                break;
            case 1:
                animalType = AnimalTypes.Cat;
                break;
            default:
                animalType = AnimalTypes.Dog;
                break;
        }

        return new Animal(maxHealthPoints, Entity.FactionType.Friendly, animalType);
    }

    private Marksman CreateMarksman()
    {
        int maxHealthPoints = _rnd.Next(70, 90);
        int defensePower = _rnd.Next(10, 15);
        int shieldCapacity = _rnd.Next(20, 30);
        int range = _rnd.Next(3, 4);
        int attackPower = _rnd.Next(30, 35);
        return new Marksman(maxHealthPoints, defensePower, shieldCapacity, range, attackPower,
            Entity.FactionType.Friendly);
    }

    private Crusader CreateCrusader()
    {
        int maxHealthPoints = _rnd.Next(80, 100);
        int defensePower = _rnd.Next(20, 30);
        int shieldCapacity = _rnd.Next(30, 40);
        int range = 1;
        int attackPower = _rnd.Next(40, 50);
        return new Crusader(maxHealthPoints, defensePower, shieldCapacity, range, attackPower,
            Entity.FactionType.Friendly);
    }
}