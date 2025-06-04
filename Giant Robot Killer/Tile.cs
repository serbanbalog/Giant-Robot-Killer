using System.Windows.Controls;
using System.Drawing;
using Image = System.Windows.Controls.Image;
using System.Windows.Shapes;
using Giant_Robot_Killer.Entities;
using Giant_Robot_Killer.Entities.Robots;
using Giant_Robot_Killer.Entities.Robots.ExecutionerRobot;
using Giant_Robot_Killer.Entities.Robots.GunslingRobot;
using Giant_Robot_Killer.Entities.Robots.HealerRobot;
using Giant_Robot_Killer.ExtenstionMethods;

namespace Giant_Robot_Killer
{
    public class Tile
    {
        private readonly PointF _mapLocation;
        private PointF _absLocation;
        public static int dX = 20, dY = 20;
        public Entity Entity;
        public Tile(PointF mapLocation)
        {
            _mapLocation = mapLocation;
            _absLocation = new PointF(mapLocation.X * dX, mapLocation.Y * dY);
        }
        public void SetEntity(Entity toSet)
        {
            Entity = toSet;
            Entity.Position = _mapLocation;
            float x = _absLocation.X + dX / 2;
            float y = _absLocation.Y + dY / 2;
            Entity.AbsPosition = new PointF(x, y);
        }
        public static void DrawLines(Canvas canvas, int i, int j, int n, int m)
        {
            double cellHeight = canvas.ActualHeight / n;
            double cellWidth = canvas.ActualWidth / m;

            Line line1 = new Line
            {
                Stroke = System.Windows.Media.Brushes.Black,
                X1 = 0,
                Y1 = (i + 1) * cellHeight,
                X2 = canvas.ActualWidth,
                Y2 = (i + 1) * cellHeight
            };
            canvas.Children.Add(line1);

            Line line2 = new Line
            {
                Stroke = System.Windows.Media.Brushes.Black,
                X1 = (j + 1) * cellWidth,
                Y1 = 0,
                X2 = (j + 1) * cellWidth,
                Y2 = canvas.ActualHeight
            };
            canvas.Children.Add(line2);

            TextBlock label = new TextBlock
            {
                Text = $"({i + 1}, {j + 1})",
                Foreground = System.Windows.Media.Brushes.Red,
                FontSize = 12
            };

            Canvas.SetLeft(label, (j + 1) * cellWidth + 2);
            Canvas.SetTop(label, (i + 1) * cellHeight + 2);

            canvas.Children.Add(label);
        }
        public void Draw(Canvas canvas, int i, int j, int n, int m, ListBox listBox, Planet planet)
        {
            Engine eng = new Engine();
            Entity = planet.Tiles[i, j].Entity;
            if (Entity != null && Entity.Alive)
            {
                Image tempImg = Entity.Draw(canvas.ActualWidth / m, canvas.ActualHeight / n);
                canvas.Children.Add(tempImg);
                Canvas.SetLeft(tempImg, j * canvas.ActualWidth / m);
                Canvas.SetTop(tempImg, i * canvas.ActualHeight / n);
                if (Entity is Gunslinger gunslinger && Entity.LastMovedTurn != planet.Turn )
                {
                    eng.SetPathToClosestEntity(gunslinger, planet);
                    string temp = $"Target:{gunslinger.CurrentTarget.Position.X}, {gunslinger.CurrentTarget.Position.Y} Gunslinger:{gunslinger.Position.X}, {gunslinger.Position.Y}";
                    listBox.Items.Add(temp);
                    gunslinger.Move(planet);
                }
                else if (Entity is Executioner executioner && Entity.LastMovedTurn != planet.Turn )
                {
                    eng.SetPathToClosestEntity(executioner, planet);
                    string temp = $"Target:{executioner.CurrentTarget.Position.X}, {executioner.CurrentTarget.Position.Y} Executioner:{executioner.Position.X}, {executioner.Position.Y}";
                    listBox.Items.Add(temp);
                    Entity.Move(planet);
                }
                else if (Entity is Healer healer && Entity.LastMovedTurn != planet.Turn)
                {
                    eng.SetPathToClosestEntity(healer, planet);
                    string temp = $"Target:{healer.CurrentTarget.Position.X}, {healer.CurrentTarget.Position.Y}  Healer:{healer.Position.X}, {healer.Position.Y}";
                    listBox.Items.Add(temp);
                    Entity.Move(planet);
                }
            }
        }
        public static void UpdateGrid(Canvas canvas, Line line)
        {
            if (line.X1 == 0)
            {
                line.X2 = canvas.ActualWidth;
            }
            else if (line.Y1 == 0)
            {
                line.Y2 = canvas.ActualHeight;
            }
        }
    }
}
