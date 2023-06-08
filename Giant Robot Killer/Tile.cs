using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;
using Image = System.Windows.Controls.Image;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;
using Giant_Robot_Killer.ExtenstionMethods;

namespace Giant_Robot_Killer
{
    public class Tile
    {
        public PointF mapLocation;
        public PointF absLocation;
        public static int dX = 20, dY = 20;
        public Entity Entity;
        public Tile(PointF mapLocation)
        {
            this.mapLocation = mapLocation;
            this.absLocation = new PointF(mapLocation.X * dX, mapLocation.Y * dY);
        }
        public void SetEntity(Entity toSet)
        {
            this.Entity = toSet;
            this.Entity.Position = this.mapLocation;
            float X = this.absLocation.X + dX / 2;
            float Y = this.absLocation.Y + dY / 2;
            this.Entity.AbsPosition = new PointF(X, Y);
        }
        public static void DrawLines(ref Canvas canvas, int i, int j, int n, int m)
        {
            Line line1 = new Line();

            line1.Stroke = System.Windows.Media.Brushes.Black;
            line1.Fill = System.Windows.Media.Brushes.Black;
            line1.X1 = 0;
            line1.Y1 = (i + 1) * (canvas.ActualHeight / n);
            line1.Y2 = line1.Y1;
            line1.X2 = canvas.ActualWidth;
            canvas.Children.Add(line1);

            Line line2 = new Line();

            line2.Stroke = System.Windows.Media.Brushes.Black;
            line2.Fill = System.Windows.Media.Brushes.Black;
            line2.X1 = (j + 1) * (canvas.ActualWidth / m);
            line2.Y1 = 0;
            line2.Y2 = canvas.ActualHeight;
            line2.X2 = line2.X1;
            canvas.Children.Add(line2);
        }
        public void Draw(ref Canvas canvas, int i, int j, int n, int m, ref ListBox listBox,ref Planet planet)
        {
            Engine eng = new Engine();

            if (this.Entity != null && this.Entity.Alive == true)
            {
                Image tempImg = this.Entity.Draw(canvas.ActualWidth / m, canvas.ActualHeight / n);
                canvas.Children.Add(tempImg);
                Canvas.SetLeft(tempImg, j * canvas.ActualWidth / m);
                Canvas.SetTop(tempImg, i * canvas.ActualHeight / n);
                if (this.Entity.GetType().Name == "Gunslinger")
                {
                    Robot tempRobot = Entity as Robot;
                    eng.SetPathToClosestEntity(ref tempRobot, planet);
                    string temp = $"{tempRobot.GetType().Name}, {tempRobot.CurrentTarget.Position.X}, {tempRobot.CurrentTarget.Position.Y}, {tempRobot.Position.X}, {tempRobot.Position.Y}";
                    listBox.Items.Add(temp);
                    Entity = tempRobot;
                    Entity.Move(ref planet);
                }
                else if (this.Entity.GetType().Name == "Executioner")
                {
                    Robot tempRobot = Entity as Robot;
                    eng.SetPathToClosestEntity(ref tempRobot, planet);
                    string temp = $"{tempRobot.GetType().Name}, {tempRobot.CurrentTarget.Position.X}, {tempRobot.CurrentTarget.Position.Y}, {tempRobot.Position.X}, {tempRobot.Position.Y}";
                    listBox.Items.Add(temp);
                    Entity = tempRobot;
                    Entity.Move(ref planet);
                }
                else if (this.Entity.GetType().Name == "Healer")
                {
                    Robot tempRobot = Entity as Robot;
                    eng.SetPathToClosestEntity(ref tempRobot, planet);
                    string temp = $"{tempRobot.GetType().Name}, {tempRobot.CurrentTarget.Position.X}, {tempRobot.CurrentTarget.Position.Y}, {tempRobot.Position.X}, {tempRobot.Position.Y}";
                    listBox.Items.Add(temp);
                    Entity = tempRobot;
                    Entity.Move(ref planet);
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
