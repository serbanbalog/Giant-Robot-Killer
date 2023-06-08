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
    public class Human : Entity
    {
        internal Human(int maxHealthPoints, FactionType faction) : base(maxHealthPoints, faction)
        {
        }
    }
}
