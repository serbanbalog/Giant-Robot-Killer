using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Giant_Robot_Killer.Entities;
using Giant_Robot_Killer.Entities.OrganicBeings;
using Image = System.Windows.Controls.Image;

namespace Giant_Robot_Killer.ExtenstionMethods.Entities
{
    public static class EntityExtenstionMethods
    {
        public static Image Draw(this Entity entity, double width, double height)
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
            Image img = new Image
            {
                Source = bmp,
                Width = width,
                Height = height
            };
            return img;
        }
        public static void Move(this Entity entity, Planet planet)
        {
            if(entity.Directions.Count > 0)
            {
                PointF temp = new PointF();
                planet.Tiles[(int)entity.Position.X, (int)entity.Position.Y].Entity = null;
                entity.Position = new PointF(entity.Directions.First().X, entity.Directions.First().Y);
                entity.Directions.Pop();
                planet.Tiles[(int)entity.Position.X, (int)entity.Position.Y].SetEntity(entity);
                entity.LastMovedTurn = planet.Turn;
            }
        }
    }
}
