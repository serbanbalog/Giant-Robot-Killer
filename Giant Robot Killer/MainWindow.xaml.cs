using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Giant_Robot_Killer
{
    public partial class MainWindow : Window
    {
        MyGraphics g = new MyGraphics();
        public static List<Planet> planets = new List<Planet>();
        static bool initializedButtons = false;
        static bool isOnMainPage = true;
        public static bool areLinesDrawn = false;
        static bool planetsAreGenerated = false;
        static Planet currentPlanet = null;
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
            if(isOnMainPage)
            {
                g.MainPage(mainImage);
                isOnMainPage = false;
            }

            double ratio = mainImage.ImageSource.Width / mainImage.ImageSource.Height;
            if(!initializedButtons)
            {
                InitializeMainMenu();
                initializedButtons = true;
            }
            foreach (UIElement ctrl in mainCanvas.Children)
            {
                if (ctrl is Button)
                {
                    newBtnPosition((Button)ctrl, ratio, e);
                }
                if(ctrl is Line && currentPlanet != null)
                {
                    Tile.UpdateGrid(mainCanvas, (Line)ctrl);
                }
            }

        }
        private void newBtnPosition(Control ctrl, double ratio, SizeChangedEventArgs e)
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
            g.MainPage(mainImage);
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
                if(!planetsAreGenerated)
                {
                    planets.Add(new Planet((Planet.PlanetName)i));          
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
            planetsAreGenerated = true;
        }
        private void PlanetsButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement sourceFrameworkElement = e.Source as FrameworkElement;
            g.ChangeMap(mainImage, Planet.GoToPlanet(sourceFrameworkElement.Name[sourceFrameworkElement.Name.Length - 1]));
            int planetNr = (int)(sourceFrameworkElement.Name[sourceFrameworkElement.Name.Length - 1] - '0');
            currentPlanet = planets[planetNr];
            planets[planetNr].Draw(ref mainCanvas, ref currentPlanet, ref ListBox1);

            mainCanvas.Children.RemoveRange(0, 8);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(currentPlanet != null)
            {
                Engine eng = new Engine();
                eng.Tick(ref mainCanvas,ref currentPlanet, ref ListBox1);
            }
        }
    }
}
