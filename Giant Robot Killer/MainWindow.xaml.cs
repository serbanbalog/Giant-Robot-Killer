using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Giant_Robot_Killer.Enums;

namespace Giant_Robot_Killer
{
    public partial class MainWindow : Window
    {
        MyGraphics _g = new MyGraphics();
        private static List<Planet> _planets = new List<Planet>();
        static bool _initializedButtons = false;
        static bool _isOnMainPage = true;
        public static bool areLinesDrawn = false;
        static bool _planetsAreGenerated = false;
        static Planet _currentPlanet = null;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ReturnToMainMenu(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Clear();
            InitializeMainMenu();
            areLinesDrawn = false;
            ListBox1.Items.Clear();
        }
        private void mainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {  
            if(_isOnMainPage)
            {
                _g.MainPage(mainImage);
                _isOnMainPage = false;
            }

            double ratio = mainImage.ImageSource.Width / mainImage.ImageSource.Height;
            if(!_initializedButtons)
            {
                InitializeMainMenu();
                _initializedButtons = true;
            }
            foreach (UIElement ctrl in mainCanvas.Children)
            {
                if (ctrl is Button)
                {
                    NewBtnPosition((Button)ctrl, ratio, e);
                }
                if(ctrl is Line && _currentPlanet != null)
                {
                    Tile.UpdateGrid(mainCanvas, (Line)ctrl);
                }
            }

        }
        private void NewBtnPosition(Control ctrl, double ratio, SizeChangedEventArgs e)
        {

            Size oldImgSize, newImgSize;

            if (e.PreviousSize.Width * e.PreviousSize.Height * e.NewSize.Width * e.NewSize.Height == 0) { return; }

            oldImgSize = RefSize(ratio, e.PreviousSize);
            newImgSize = RefSize(ratio, e.NewSize);

            Point oldImgPos, newImgPos;
            oldImgPos = new Point((e.PreviousSize.Width - oldImgSize.Width) / 2, (e.PreviousSize.Height - oldImgSize.Height) / 2);
            newImgPos = new Point((e.NewSize.Width - newImgSize.Width) / 2, (e.NewSize.Height - newImgSize.Height) / 2);

            Point ctrlPos = new Point((double)ctrl.GetValue(Canvas.LeftProperty) - oldImgPos.X,
                                     (double)ctrl.GetValue(Canvas.TopProperty) - oldImgPos.Y);

            double x = newImgSize.Width / oldImgSize.Width;
            double y = newImgSize.Height / oldImgSize.Height;

            ctrlPos.X *= x;
            ctrlPos.Y *= y;

            ctrl.Width *= x;
            ctrl.Height *= y;

            ctrl.SetValue(Canvas.LeftProperty, ctrlPos.X + newImgPos.X);
            ctrl.SetValue(Canvas.TopProperty, ctrlPos.Y + newImgPos.Y);
        }
        private Size RefSize(double ratio, Size containerSize)
        {
            double cH, cW;
            cW = containerSize.Width;
            cH = containerSize.Height;

            if (cH * cW == 0) { return new Size(0, 0); }

            if (cW / cH > ratio)
            {
                return new Size(cH * ratio, cH);
            }
            else
            {
                return new Size(cW, cW / ratio);
            }

        }
        private void InitializeMainMenu()
        {
            _g.MainPage(mainImage);
            string[] planetNames = new string[]
            {
                "Mercury",
                "Venus",
                "Earth",
                "Mars",
                "Jupiter",
                "Saturn",
                "Uranus",
                "Neptune"
            };
            for (int i = 0; i < 8; i++)
            {
                if(!_planetsAreGenerated)
                {
                    _planets.Add(new Planet((PlanetName)i));          
                }
                Button newBtn = new Button
                {
                    Name = "Button" + i.ToString(),
                    Height = 115,
                    Width = 115,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    ClickMode = ClickMode.Release,
                    Background = Brushes.Transparent
                };
                newBtn.Click += PlanetsButton_Click;
                Canvas.SetLeft(newBtn, 35 + i * 152.5);
                Canvas.SetTop(newBtn, mainCanvas.ActualHeight / 2 - 72.5);
                mainCanvas.Children.Add(newBtn);
            }
            _planetsAreGenerated = true;
        }
        private void PlanetsButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement sourceFrameworkElement = e.Source as FrameworkElement;
            if (sourceFrameworkElement != null)
            {
                _g.ChangeMap(mainImage,
                    Planet.GoToPlanet(sourceFrameworkElement.Name[sourceFrameworkElement.Name.Length - 1]));
                int planetNr = sourceFrameworkElement.Name[sourceFrameworkElement.Name.Length - 1] - '0';
                _currentPlanet = _planets[planetNr];
                _planets[planetNr].Draw(mainCanvas, _currentPlanet, ListBox1);
            }

            mainCanvas.Children.RemoveRange(0, 8);
        }
        private void TickButtonClick(object sender, RoutedEventArgs e)
        {
            if(_currentPlanet != null)
            {
                for(int i = 0; i < 8; i++)
                {
                    if (_currentPlanet.Name == (PlanetName)i)
                        _planets[i] = _currentPlanet;
                }
                mainCanvas.Children.Clear();
                Engine eng = new Engine();
                eng.Tick(mainCanvas, _currentPlanet, ListBox1);

            }
        }
    }
}
