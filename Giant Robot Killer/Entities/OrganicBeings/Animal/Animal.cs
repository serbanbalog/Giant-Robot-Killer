using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace Giant_Robot_Killer
{
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
            this.AnimalType = animalType;
        }      
    }
}
