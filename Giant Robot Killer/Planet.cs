using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Giant_Robot_Killer.Entities;
using Giant_Robot_Killer.Entities.OrganicBeings.OrganicBeingCreator;
using Giant_Robot_Killer.Entities.Robots.RobotFactory;
using Giant_Robot_Killer.Enums;

namespace Giant_Robot_Killer;

public class Planet
{
    private Random _rnd ;
    private readonly int _robotCapacity, _organicBeingCapacity;
    public PlanetName Name { get; private set; }
    public Tile[,] Tiles { get; set; }
    public int N => Tiles.GetLength(0);
    public int M => Tiles.GetLength(1);
    public int Turn { get; set; }

    public Planet(PlanetName name, int capacity)
    {
        _rnd = new Random();
        Name = name;
        _robotCapacity = capacity / (100 / _rnd.Next(35, 50));
        _organicBeingCapacity = capacity - _robotCapacity;
        Turn = 0;

        Tiles = new Tile[20, 20];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                Tiles[i, j] = new Tile(new PointF(i, j));
            }
        }

        PopulatePlanet();
    }

    public void GoToPlanet(Canvas canvas)
    {
        string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string temp;
        switch (Name)
        {
            case PlanetName.Mercury:
                temp = "Mercury_surface.jpg";
                break;
            case PlanetName.Venus:
                temp = "Venus_surface.jpg";
                break;
            case PlanetName.Earth:
                temp = "Earth_surface.jpg";
                break;
            case PlanetName.Mars:
                temp = "Mars_surface.jpg";
                break;
            case PlanetName.Jupiter:
                temp = "Jupiter_surface.jpg";
                break;
            case PlanetName.Saturn:
                temp = "Saturn_surface.jpg";
                break;
            case PlanetName.Uranus:
                temp = "Uranus_surface.jpg";
                break;
            case PlanetName.Neptune:
                temp = "Neptune_surface.jpg";
                break;
            default:
                temp = "Mercury_surface.png";
                break;
        }

        string sFile = Path.Combine(sCurrentDirectory, $@"..\..\Resources\Planet_Surfaces\{temp}");
        string sFilePath = Path.GetFullPath(sFile);
        BitmapImage bmp = new BitmapImage(new Uri(sFilePath));
        ImageBrush imageBrush = new ImageBrush(bmp);
        canvas.Background = imageBrush;
    }

    private void PopulatePlanet()
    {
        RobotFactory robotFactory = new RobotFactory();
        GenerateEntities(_robotCapacity, () => robotFactory.GenerateRandomRobot());

        OrganicBeingCreator organicBeingCreator = new OrganicBeingCreator();
        GenerateEntities(_organicBeingCapacity, () => organicBeingCreator.CreateOrganicBeing());
    }

    private void GenerateEntities(int capacity, Func<Entity> generator)
    {
        for (int i = 0; i < capacity; i++)
        {
            int tempN = _rnd.Next(N);
            int tempM = _rnd.Next(M);
            if (Tiles[tempN, tempM].Entity == null)
                Tiles[tempN, tempM].SetEntity(generator());
        }
    }

    public void Draw(Canvas canvas, bool areLinesDrawn)
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                Tiles[i, j].Draw(canvas, i, j, N, M, this, areLinesDrawn);
            }
        }
    }
}