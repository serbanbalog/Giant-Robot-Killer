using System;

namespace Giant_Robot_Killer.Entities.OrganicBeings.OrganicBeingCreator
{
    public class OrganicBeingCreator : IOrganicBeingCreator
    {
        private Random _rnd;

        public OrganicBeingCreator()
        {
         _rnd = new Random();   
        }

        public Entity CreateOrganicBeing()
        {
            switch (_rnd.Next(2))
            {
                case 0:
                     return CreateHuman();
                case 1:
                    return CreateAnimal();
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
    }
}
