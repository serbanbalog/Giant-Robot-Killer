using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Giant_Robot_Killer.Entities;
using Giant_Robot_Killer.Entities.OrganicBeings;
using Giant_Robot_Killer.Entities.Robots;
using Image = System.Windows.Controls.Image;

namespace Giant_Robot_Killer.ExtenstionMethods.Entities;

public static class EntityExtenstionMethods
{
    public static Image Draw(this Entity entity, double width, double height, double x, double y)
    {
        string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string sFile;
        if (entity is Animal)
        {
            sFile = Path.Combine(sCurrentDirectory, $@"..\..\Resources\Organic\{(entity as Animal).AnimalType}.png");
        }
        else if (entity is Robot)
        {
            sFile = Path.Combine(sCurrentDirectory, $@"..\..\Resources\Robots\{entity.GetType().Name}.png");
        }
        else
        {
            sFile = Path.Combine(sCurrentDirectory, $@"..\..\Resources\Organic\{entity.GetType().Name}.png");
        }

        string sFilePath = Path.GetFullPath(sFile);
        BitmapImage bmp = new BitmapImage(new Uri(sFilePath));
        Image img = new Image
        {
            Source = bmp,
            Width = width,
            Height = height,
            Margin = new Thickness(x, y, 0, 0),
        };
        return img;
    }

    public static void Move(this Entity entity, Planet planet)
    {
        if (entity.Directions.Count == 0)
            return;
        
        var locationToMoveAt = new PointF(entity.Directions.First().X, entity.Directions.First().Y);
        if (planet.Tiles[(int)locationToMoveAt.X, (int)locationToMoveAt.Y].Entity != null)
            return;

        planet.Tiles[(int)entity.Position.X, (int)entity.Position.Y].Entity = null;
        entity.Position = locationToMoveAt;
        entity.Directions.Pop();
        planet.Tiles[(int)entity.Position.X, (int)entity.Position.Y].SetEntity(entity);
        entity.LastMovedTurn = planet.Turn;
    }
}