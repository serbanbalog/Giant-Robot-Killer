using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Giant_Robot_Killer.Enums;

namespace Giant_Robot_Killer;

public class MainPage
{
    private readonly Canvas _display;
    private readonly string _filePath;
    private List<Planet> _planets;
    private readonly StatsBar _statsBar;
    private readonly Engine _engine;
    private Planet _selectedPlanet;


    public MainPage(Canvas display)
    {
        _display = display;
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\Planets\{0}.png");
        _planets = new List<Planet>();
        _engine = new Engine();

        _statsBar = new StatsBar();
        _statsBar.MainMenuClicked += RenderMainPage;
        _statsBar.TickClicked += TickPlanet;
    }

    public void RenderMainPage()
    {
        _display.Children.Clear();
        _display.Background = Brushes.Black;
        HideStatsBar();

        DrawStars(120); // You can adjust the number of stars
        DrawMilkyWay();

        (double centerX, double centerY) = GetDisplayCenter();
        AddSun(centerX, centerY);

        PlanetName[] planetNames =
        {
            PlanetName.Mercury, PlanetName.Venus, PlanetName.Earth, PlanetName.Mars, PlanetName.Jupiter,
            PlanetName.Saturn, PlanetName.Uranus, PlanetName.Neptune
        };
        double[] planetDiameters = { 4879, 12104, 16742, 6779, 139820, 116460, 50724, 49244 };

        double[] orbitRadii = [150, 220, 290, 360, 500, 600, 720, 850];
        double[] planetSizes = ScalePlanetSizes(planetDiameters);

        if (_planets.Count == 0)
            GeneratePlanets(planetNames);

        RandomlyDistributePlanetsAcrossQuadrants(planetNames, planetSizes, orbitRadii, centerX, centerY);
    }

    private void DrawStars(int count)
    {
        Random rnd = new Random();
        double width = _display.ActualWidth;
        double height = _display.ActualHeight;

        for (int i = 0; i < count; i++)
        {
            double starSize = rnd.NextDouble() * 2 + 1;
            Brush starBrush =
                rnd.NextDouble() > 0.85
                    ? Brushes.LightYellow
                    : Brushes.White;

            var star = new System.Windows.Shapes.Ellipse
            {
                Width = starSize,
                Height = starSize,
                Fill = starBrush,
                Opacity = rnd.NextDouble() * 0.5 + 0.45,
                Margin = new Thickness(rnd.NextDouble() * width, rnd.NextDouble() * height, 0, 0),
                IsHitTestVisible = false
            };
            _display.Children.Add(star);
        }
    }

    private void DrawMilkyWay()
    {
        double width = _display.ActualWidth;
        double height = _display.ActualHeight;

        var milkyWay = new System.Windows.Shapes.Ellipse
        {
            Width = width * 1.5,
            Height = height * 0.25,
            Fill = new RadialGradientBrush(
                Color.FromArgb(120, 255, 255, 255), // Soft center
                Color.FromArgb(0, 255, 255, 255) // Fully transparent at edge
            ),
            Opacity = 0.18,
            Margin = new Thickness(
                width * 0.1,
                height * 0.5 - (height * 0.25) / 2,
                0,
                0),
            RenderTransform = new RotateTransform(25, width * 0.75, height * 0.625),
            IsHitTestVisible = false
        };
        _display.Children.Add(milkyWay);
    }

    private void GeneratePlanets(PlanetName[] planetNames)
    {
        foreach (var name in planetNames)
        {
            _planets.Add(new Planet(name));
        }
    }

    private void AddSun(double centerX, double centerY)
    {
        var size = 250;
        var positionX = centerX - size / 2;
        var positionY = centerY - size / 2;

        Image sun = new Image
        {
            Source = new BitmapImage(new Uri(string.Format(_filePath, "Sun"))),
            Width = size,
            Height = size,
            Margin = new Thickness(positionX, positionY, 0, 0),
        };

        _display.Children.Add(sun);

        Image sunLight = new Image
        {
            Source = new BitmapImage(new Uri(string.Format(_filePath, "Sun-light"))),
            Width = size,
            Height = size,
            Margin = new Thickness(positionX, positionY, 0, 0),
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

    private void RandomlyDistributePlanetsAcrossQuadrants(PlanetName[] planetNames, double[] planetSizes,
        double[] orbitRadii, double centerX, double centerY)
    {
        int planetCount = planetNames.Length;
        Random rnd = new Random();

        Dictionary<int, List<(double X, double Y, double PlanetSize)>> quadrantAssignedPlanets = new()
        {
            { 0, new List<(double, double, double)>() },
            { 1, new List<(double, double, double)>() },
            { 2, new List<(double, double, double)>() },
            { 3, new List<(double, double, double)>() }
        };

        for (int i = 0; i < planetCount; i++)
        {
            double planetSize = planetSizes[i];
            double orbitRadius = orbitRadii[i];
            int quadrant = i % 4;

            (double X, double Y) position = FindPositionUsingOrbit(
                quadrant, planetSize, orbitRadius, quadrantAssignedPlanets, centerX, centerY
            );

            if (!quadrantAssignedPlanets.ContainsKey(quadrant))
                quadrantAssignedPlanets[quadrant] = new List<(double X, double Y, double PlanetSize)>();

            quadrantAssignedPlanets[quadrant].Add((position.X, position.Y, planetSize));
            PlacePlanetInQuadrant(planetNames[i], planetSize, orbitRadius, quadrant, centerX, centerY);
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

    private void PlacePlanetInQuadrant(PlanetName planetName, double planetSize, double orbitRadius,
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
            Margin = new Thickness(planetX, planetY, 0, 0),
            ToolTip = planetName
        };

        planet.MouseLeftButtonDown += (sender, args) => PlanetsButton_Click(planetName);

        _display.Children.Add(planet);

        TextBlock planetLabel = new TextBlock
        {
            Text = planetName.ToString(),
            Foreground = Brushes.White,
            Background = Brushes.Transparent,
            FontSize = Math.Min(18, planetSize / 3),
            TextAlignment = TextAlignment.Center,
            Width = planetSize,
            Margin = new Thickness(planetX, planetY + planetSize - (planetSize * 0.25), 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            IsHitTestVisible = false,
            Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = Colors.Black,
                BlurRadius = 4,
                ShadowDepth = 0,
                Opacity = 0.7
            }
        };
        _display.Children.Add(planetLabel);
    }

    private void PlanetsButton_Click(PlanetName planetName)
    {
        _display.Children.Clear();
        var planet = _planets.Find(x => x.Name == planetName);
        _selectedPlanet = planet;
        planet.GoToPlanet(_display);
        planet.Draw(_display, false);
        ShowStatsBar();
    }

    private void TickPlanet()
    {
        if (_selectedPlanet != null)
        {
            var isGameDone = _engine.Tick(_display, _selectedPlanet); // Calls logic from Engine.cs
            _statsBar.SetTurn(_selectedPlanet.Turn);
            if(isGameDone)
                _statsBar.StopAutoPlay();
        }
    }

    private void ShowStatsBar()
    {
        if (_selectedPlanet != null)
            _statsBar.SetTurn(_selectedPlanet.Turn);
        Canvas.SetLeft(_statsBar.BarUI, 0);
        Canvas.SetTop(_statsBar.BarUI, _display.ActualHeight - 50);
        _display.Children.Add(_statsBar.BarUI);
    }

    private void HideStatsBar()
    {
        _display.Children.Remove(_statsBar.BarUI);
        _statsBar.StopAutoPlay();
    }
}