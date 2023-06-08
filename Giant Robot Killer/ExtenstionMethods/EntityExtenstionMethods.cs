using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Giant_Robot_Killer.ExtenstionMethods
{
    public static class EntityExtenstionMethods
    {
        public static System.Windows.Controls.Image Draw(this Entity entity, double width, double height)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile;
            if(entity is Animal)
            {
                sFile = Path.Combine(sCurrentDirectory, $@"..\..\Resources\{(entity as Animal).AnimalType}.jpg");
            }
            else
            {
                sFile = Path.Combine(sCurrentDirectory, $@"..\..\Resources\{entity.GetType().Name}.jpg");
            }
            string sFilePath = Path.GetFullPath(sFile);
            BitmapImage bmp = new BitmapImage(new Uri(sFilePath));
            System.Windows.Controls.Image img = new System.Windows.Controls.Image
            {
                Source = bmp,
                Width = width,
                Height = height
            };
            return img;
        }
        public static void Move(this Entity entity, ref Planet planet)
        {
            if(entity.Directions.Count > 0)
            {
                PointF temp = new PointF();
                temp.X = entity.Position.X + entity.Directions.First().x;
                temp.Y = entity.Position.Y + entity.Directions.First().y;
                entity.Position = temp;
                entity.Directions.Pop();
                planet.Tiles[(int)entity.Position.X, (int)entity.Position.Y].SetEntity(entity);
            }
        }
    }
}
