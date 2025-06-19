using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Giant_Robot_Killer.Entities;
using Giant_Robot_Killer.Entities.Robots;
using Giant_Robot_Killer.ExtenstionMethods.Entities;
using Image = System.Windows.Controls.Image;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Drawing.Point;

namespace Giant_Robot_Killer;

public class Tile
{
    private readonly PointF _mapLocation;
    private PointF _absLocation;
    public static int dX = 20, dY = 20;
    public Entity Entity;
    private bool _areLinesDrawn = false;

    public Tile(PointF mapLocation)
    {
        _mapLocation = mapLocation;
        _absLocation = new PointF(mapLocation.X * dX, mapLocation.Y * dY);
    }

    public void SetEntity(Entity entity)
    {
        Entity = entity;
        Entity.Position = _mapLocation;
        float x = _absLocation.X + dX / 2;
        float y = _absLocation.Y + dY / 2;
        Entity.AbsPosition = new PointF(x, y);
    }

    private void DrawLines(Canvas canvas, Point startPoint, Point endPoint)
    {
        double x1 = startPoint.X;
        double y1 = startPoint.Y;
        double x2 = endPoint.X;
        double y2 = startPoint.Y;
        double x3 = endPoint.X;
        double y3 = endPoint.Y;
        double x4 = startPoint.X;
        double y4 = endPoint.Y;

        Line topLine = CreateLine(x1, y1, x2, y2);
        Line rightLine = CreateLine(x2, y2, x3, y3);
        Line bottomLine = CreateLine(x3, y3, x4, y4);
        Line leftLine = CreateLine(x4, y4, x1, y1);

        canvas.Children.Add(topLine);
        canvas.Children.Add(rightLine);
        canvas.Children.Add(bottomLine);
        canvas.Children.Add(leftLine);
    }

    private Line CreateLine(double x1, double y1, double x2, double y2)
    {
        Line line = new Line
        {
            X1 = 0,
            Y1 = 0,
            X2 = x2 - x1,
            Y2 = y2 - y1,
            StrokeThickness = 1,
            Stroke = Brushes.Black
        };

        line.Margin = new Thickness(x1, y1, 0, 0);

        return line;
    }

    public void Draw(Canvas canvas, int i, int j, int n, int m, Planet planet, bool areLinesDrawn)
    {
        Entity = planet.Tiles[i, j].Entity;

        double tileWidth = canvas.ActualWidth / m;
        double tileHeight = canvas.ActualHeight / n;

        Point startPoint = new Point((int)(j * tileWidth), (int)(i * tileHeight));
        Point endPoint = new Point((int)((j + 1) * tileWidth), (int)((i + 1) * tileHeight));

        if (i == 0 && j == 0)
        {
            var toRemove = canvas.Children.Cast<UIElement>()
                .Where(x => !(x is Line) && !(x is DockPanel) && !(x is Button) && !(x is TextBlock))
                .ToList();
            foreach (var element in toRemove)
            {
                canvas.Children.Remove(element);
            }
        }

        if (!areLinesDrawn)
            DrawLines(canvas, startPoint, endPoint);

        if (Entity != null && Entity.Alive)
        {
            double cellWidth = canvas.ActualWidth / m;
            double cellHeight = canvas.ActualHeight / n;
            double positionX = j * cellWidth;
            double positionY = i * cellHeight;

            Image entityImage = Entity.Draw(cellWidth, cellHeight, positionX, positionY);

            canvas.Children.Add(entityImage);

            double healthPercentage = Math.Min((double)Entity.HealthPoints / Entity.MaxHealthPoints * 100, 100);
            double shieldPercentage = Math.Max((double)Entity.HealthPoints / Entity.MaxHealthPoints * 100 - 100, 0);
            
            var healthbar = Entity.Faction == Entity.FactionType.Friendly ? Brushes.Aqua : Brushes.Red;

            ProgressBar healthBar = new ProgressBar
            {
                Width = 10,
                Height = canvas.ActualHeight / n - 5,
                Orientation = Orientation.Vertical,
                Value = healthPercentage,
                Foreground = healthbar,
                Background = Brushes.Black,
                BorderThickness = new Thickness(2),
                BorderBrush = Brushes.White,
                Margin = new Thickness((j + 1) * (canvas.ActualWidth / m) - 15, i * (canvas.ActualHeight / n) + 2.5,
                    0, 0),
            };
            canvas.Children.Add(healthBar);

            if (shieldPercentage > 0)
            {
                ProgressBar shieldBar = new ProgressBar
                {
                    Width = 10,
                    Height = canvas.ActualHeight / n - 5,
                    Orientation = Orientation.Vertical,
                    Value = shieldPercentage,
                    Foreground = Brushes.Blue,
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness(2),
                    BorderBrush = Brushes.Black,
                    Margin = new Thickness((j + 1) * (canvas.ActualWidth / m) - 15,
                        i * (canvas.ActualHeight / n) + 2.5, 0, 0),
                };
                canvas.Children.Add(shieldBar);
            }
        }
    }
}