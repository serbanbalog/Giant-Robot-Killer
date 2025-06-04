using System;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Giant_Robot_Killer.Entities;
using Giant_Robot_Killer.Entities.OrganicBeings.OrganicBeingCreator;
using Giant_Robot_Killer.Entities.Robots.RobotFactory;
using Giant_Robot_Killer.Enums;

namespace Giant_Robot_Killer
{
    public class Planet
    {
        private Random _rnd = new Random();
        private readonly int _robotCapacity, _organicBeingCapacity;
        public PlanetName Name { get; private set; }
        public Tile[,] Tiles { get; set; }
        public int N => Tiles.GetLength(0);
        public int M => Tiles.GetLength(1);
        public int Turn { get; set; }

        public Planet(PlanetName name)
        {
            Name = name;
            var capacity = _rnd.Next(25,40);
            _robotCapacity = capacity / 3;
            _organicBeingCapacity = capacity - _robotCapacity;
            Turn = 0;
            
            Tiles = new Tile[30, 30];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Tiles[i, j] = new Tile(new PointF(i, j));
                }
            }
            PopulatePlanet();
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
        
        private void PopulatePlanet()
        {            
            RobotFactory robotFactory = new RobotFactory();
            GenerateEntities(_robotCapacity,() => robotFactory.GenerateRandomRobot());
            
            OrganicBeingCreator organicBeingCreator = new OrganicBeingCreator();
            GenerateEntities(_organicBeingCapacity,() => organicBeingCreator.CreateOrganicBeing());
        }

        private void GenerateEntities(int capacity, Func<Entity> generator)
        {
            for (int i=0; i < capacity; i++)
            {
                int tempN = _rnd.Next(N);
                int tempM = _rnd.Next(M);
                if (Tiles[tempN, tempM].Entity == null)
                    Tiles[tempN, tempM].SetEntity(generator());
            }
        }
        
        public void Draw(Canvas canvas, Planet planet, ListBox listBox)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    planet.Tiles[i, j].Draw( canvas, i, j, N , M,  listBox, planet);
                    if (!MainWindow.areLinesDrawn)
                        Tile.DrawLines( canvas, i, j, N, M);
                }
            }

            Turn++;
        }
    }
}
