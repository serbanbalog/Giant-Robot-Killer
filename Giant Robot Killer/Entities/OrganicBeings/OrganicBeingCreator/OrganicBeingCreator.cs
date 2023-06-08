using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Giant_Robot_Killer.Entity;

namespace Giant_Robot_Killer
{
    public class OrganicBeingCreator : IOrganicBeingCreator
    {
        public OrganicBeingCreator() { }
        public Human CreateHuman()
        {
            Random rnd = new Random();
            int maxHealthPoints = rnd.Next(90, 100);
            return new Human(maxHealthPoints, FactionType.Friendly);
        }
        public Animal CreateAnimal()
        {
            Random rnd = new Random();
            int maxtHealthPoints = rnd.Next(20, 35);
            AnimalTypes animalType;
            int temp = rnd.Next(2);
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
            return new Animal(maxtHealthPoints, FactionType.Friendly, animalType);
        }
    }
}
