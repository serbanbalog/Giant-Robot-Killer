using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Giant_Robot_Killer;

public class MainPage
{
    private readonly Grid _display;
    private readonly string _filePath;
    private List<Planet> _planets;

    public MainPage(Grid display)
    {
        _display = display;
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\Planets\{0}.png");
        _planets = new List<Planet>();
    }

    public void RenderMainPage()
    {
        _display.Children.Clear();

        (double centerX, double centerY) = GetDisplayCenter();
        AddSun(centerX, centerY);

        string[] planetNames = { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
        double[] planetDiameters = { 4879, 12104, 16742, 6779, 139820, 116460, 50724, 49244 };

        double[] orbitRadii = [150, 220, 290, 360, 480, 600, 720, 850];
        double[] planetSizes = ScalePlanetSizes(planetDiameters);

        RandomlyDistributePlanetsAcrossQuadrants(planetNames, planetSizes, orbitRadii, centerX, centerY);
    }

    private void AddSun(double centerX, double centerY)
    {
        Image sun = new Image
        {
            Source = new BitmapImage(new Uri(string.Format(_filePath, "Sun"))),
            Width = 250,
            Height = 250,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };
        _display.Children.Add(sun);

        Image sunLight = new Image
        {
            Source = new BitmapImage(new Uri(string.Format(_filePath, "Sun-light"))),
            Width = 250,
            Height = 250,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };
        _display.Children.Add(sunLight);
    }

    private (double centerX, double centerY) GetDisplayCenter()
    {
        double displayWidth = _display.ActualWidth;
        double displayHeight = _display.ActualHeight;
        return (displayWidth / 2, displayHeight / 2);
    }

    private double[] ScalePlanetSizes(double[] planetDiameters)
    {
        double maxDiameter = 139820;
        double scaleFactor = 250 / maxDiameter;
        double minimumSize = 65;

        return planetDiameters.Select(diameter => Math.Max(diameter * scaleFactor, minimumSize)).ToArray();
    }

    private void DistributePlanetsAcrossQuadrants(string[] planetNames, double[] planetSizes,
        double[] orbitRadii, double centerX, double centerY)
    {
        int planetCount = planetNames.Length;
        int planetsPerQuadrant = planetCount / 4;
        int remainingPlanets = planetCount % 4;
        Random random = new Random();

        int[] quadrantPlanetCounts = new int[4];

        int quadrant = 1;
        for (int i = 0; i < planetCount; i++)
        {
            if (quadrantPlanetCounts[quadrant - 1] < planetsPerQuadrant || remainingPlanets > 0)
            {
                PlacePlanetInQuadrant(planetNames[i], planetSizes[i], orbitRadii[i], quadrant, centerX, centerY);

                quadrantPlanetCounts[quadrant - 1]++;
            }

            quadrant = quadrant % 4;

            if (remainingPlanets > 0 && i >= (planetCount - remainingPlanets))
            {
                quadrant = random.Next(0, 4);
            }
        }
    }

    private void RandomlyDistributePlanetsAcrossQuadrants(string[] planetNames, double[] planetSizes,
        double[] orbitRadii, double centerX, double centerY)
    {
        int planetCount = planetNames.Length;
        int planetsPerQuadrant = planetCount / 4;
        int remainingPlanets = planetCount % 4;

        Dictionary<int, List<(double X, double Y, double PlanetSize)>> quadrantAssignedPlanets = new()
        {
            { 0, new List<(double, double, double)>() },
            { 1, new List<(double, double, double)>() },
            { 2, new List<(double, double, double)>() },
            { 3, new List<(double, double, double)>() }
        };

        for (int i = 0; i < planetCount; i++)
        {
            string planetName = planetNames[i];
            double planetSize = planetSizes[i];
            double orbitRadius = orbitRadii[i];

            int quadrant = i % 4;

            (double posX, double posY) = FindPositionUsingOrbit(
                quadrant, planetSize, orbitRadius, quadrantAssignedPlanets, centerX, centerY);

            PlacePlanetInQuadrant(planetName, planetSize, orbitRadius, quadrant, posX, posY);

            quadrantAssignedPlanets[quadrant].Add((posX, posY, planetSize));
        }
    }

    private (double X, double Y) FindPositionUsingOrbit(int quadrant, double planetSize, double orbitRadius,
        Dictionary<int, List<(double X, double Y, double PlanetSize)>> quadrantAssignedPlanets,
        double centerX, double centerY)
    {
        Random random = new Random();

        double quadrantCenterX = quadrant == 1 || quadrant == 2 ? centerX / 2 : 3 * centerX / 2;
        double quadrantCenterY = quadrant == 2 || quadrant == 3 ? centerY / 2 : 3 * centerY / 2;

        while (true)
        {
            double angle = random.NextDouble() * 2 * Math.PI;

            double posX = quadrantCenterX + orbitRadius * Math.Cos(angle);
            double posY = quadrantCenterY + orbitRadius * Math.Sin(angle);

            bool overlaps = quadrantAssignedPlanets[quadrant]
                .Any(p => Math.Sqrt(Math.Pow(posX - p.X, 2) + Math.Pow(posY - p.Y, 2)) < (planetSize + p.PlanetSize));

            if (!overlaps)
            {
                return (posX, posY);
            }
        }
    }

    private void PlacePlanetInQuadrant(string planetName, double planetSize, double orbitRadius,
        int quadrant, double centerX, double centerY)
    {
        var random = new Random();

        double angle = quadrant switch
        {
            0 => random.NextDouble() * (Math.PI / 2),
            1 => Math.PI / 2 + random.NextDouble() * (Math.PI / 2),
            2 => Math.PI + random.NextDouble() * (Math.PI / 2),
            3 => 3 * Math.PI / 2 + random.NextDouble() * (Math.PI / 2),
            _ => 0
        };

        double planetX = centerX + orbitRadius * Math.Cos(angle) - planetSize / 2;
        double planetY = centerY + orbitRadius * Math.Sin(angle) - planetSize / 2;

        double displayWidth = _display.ActualWidth;
        double displayHeight = _display.ActualHeight;
        planetX = Math.Max(0, Math.Min(planetX, displayWidth - planetSize));
        planetY = Math.Max(0, Math.Min(planetY, displayHeight - planetSize));

        Image planet = new Image
        {
            Source = new BitmapImage(new Uri(string.Format(_filePath, planetName))),
            Width = planetSize,
            Height = planetSize,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(planetX, planetY, 0, 0)
        };

        // planet.MouseLeftButtonDown += PlanetsButton_Click;

        _display.Children.Add(planet);
    }
}