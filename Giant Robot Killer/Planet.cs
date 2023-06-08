using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Giant_Robot_Killer
{
    public class Planet
    {
        Random rnd = new Random();

        public enum PlanetName
        {
            Mercury,
            Venus,
            Earth,
            Mars,
            Jupiter,
            Saturn,
            Uranus,
            Neptune
        }
        public PlanetName Name { get; private set; }
        public int Capacity { get; private set; }
        public Tile[,] Tiles { get; set; }
        public int n
        {
            get
            {
                return Tiles.GetLength(0);
            }
        }
        public int m
        {
            get
            {
                return this.Tiles.GetLength(1);
            }
        }
        public Planet(PlanetName name)
        {
            this.Name = name;
            this.Capacity = rnd.Next(25,40);
            Tiles = new Tile[30, 30];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Tiles[i, j] = new Tile(new PointF(i, j));
                }
            }
            PopulatePlanet(this);
        }
        public static BitmapImage GoToPlanet(char buttonNo)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string temp;
            switch (buttonNo)
            {
                case '0':
                    temp = "Mercury.jpg";
                    break;
                case '1':
                    temp = "Venus.jpg";
                    break;
                case '2':
                    temp = "Earth.jpg";
                    break;
                case '3':
                    temp = "Mars.jpg";
                    break;
                case '4':
                    temp = "Jupiter.jpg";
                    break;
                case '5':
                    temp = "Saturn.jpg";
                    break;
                case '6':
                    temp = "Uranus.jpg";
                    break;
                case '7':
                    temp = "Neptune.jpg";
                    break;
                default:
                    temp = "MainMenu.png";
                    break;
            }
            string sFile = Path.Combine(sCurrentDirectory, $@"..\..\Resources\{temp}");
            string sFilePath = Path.GetFullPath(sFile);
            BitmapImage bmp = new BitmapImage(new Uri(sFilePath));
            return bmp;
        }
        private Planet PopulatePlanet(Planet planet)
        {
            Random rnd = new Random();

            OrganicBeingCreator organicCreator = new OrganicBeingCreator();
            int robotCapacity = planet.Capacity / 3;
            int i = 0;
            while (i < robotCapacity)
            {
                RobotFactory robotFactory = new RobotFactory();
                int tempN = rnd.Next(n);
                int tempM = rnd.Next(m);
                if (Tiles[tempN, tempM].Entity == null)
                    switch (rnd.Next(3))
                    {
                        case 0:
                            Tiles[tempN, tempM].SetEntity(robotFactory.CreateHealerRobot());
                            break;
                        case 1:
                            Tiles[tempN, tempM].SetEntity(robotFactory.CreateGunslingerRobot());
                            break;
                        case 2:
                            Tiles[tempN, tempM].SetEntity(robotFactory.CreateExecutionerRobot());
                            break;
                    }
                i++;
            }

            int currentCapacity = planet.Capacity - robotCapacity;

            while (i < currentCapacity)
            {
                int tempN = rnd.Next(n);
                int tempM = rnd.Next(m);
                if (Tiles[tempN, tempM].Entity == null)
                    switch (rnd.Next(2))
                    {
                        case 0:
                            Tiles[tempN, tempM].SetEntity(organicCreator.CreateHuman());
                            break;
                        case 1:
                            Tiles[tempN, tempM].SetEntity(organicCreator.CreateAnimal());
                            break;
                    }
                i++;
            }
            return planet;
        }
        public void Draw(Canvas canvas, Planet planet, ListBox listBox)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    planet.Tiles[i, j].Draw( canvas, i, j, n , m,  listBox, planet);
                    if (!MainWindow.areLinesDrawn)
                        Tile.DrawLines( canvas, i, j, n, m);
                }
            }
        }
    }
}
