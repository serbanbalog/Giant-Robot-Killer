using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Giant_Robot_Killer.Enums;

namespace Giant_Robot_Killer
{
    public partial class MainWindow : Window
    {
        static bool _initializedButtons = false;
        static bool _isOnMainPage = true;
        public static bool areLinesDrawn = false;
        static Planet _currentPlanet = null;
        private readonly MainPage _mainPage;
        
        public MainWindow()
        {
            InitializeComponent();
            _mainPage = new MainPage(ContentGrid);
        }
        
        private void ReturnToMainMenu(object sender, RoutedEventArgs e)
        {
            InitializeMainMenu();
            areLinesDrawn = false;
            ListBox1.Items.Clear();
        }
        private void MainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {  
            // if(_isOnMainPage)
            // {
            //     _mainPage.RenderMainPage(ContentGrid);
            //     _isOnMainPage = false;
            // }

            double ratio = ContentGrid.ActualWidth / ContentGrid.ActualHeight;
            if(!_initializedButtons)
            {
                InitializeMainMenu();
                _initializedButtons = true;
            }
            foreach (UIElement ctrl in ContentGrid.Children)
            {
                if (ctrl is Button)
                {
                    NewBtnPosition((Button)ctrl, ratio, e);
                }
                if(ctrl is Line && _currentPlanet != null)
                {
                    Tile.UpdateGrid(ContentGrid, (Line)ctrl);
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
            _mainPage.RenderMainPage();
            
            // for (int i = 0; i < 8; i++)
            // {
            //     {
            //         _planets.Add(new Planet((PlanetName)i));          
            //     }

                // var boxSize = ContentGrid.ActualHeight * 0.6;
                // Button newBtn = new Button
                // {
                //     Name = "Button" + i,
                //     Height = 160,
                //     Width = 160,
                //     VerticalAlignment = VerticalAlignment.Center,
                //     HorizontalAlignment = HorizontalAlignment.Left,
                //     ClickMode = ClickMode.Release,
                //     Background = Brushes.Transparent,
                //     Margin = new Thickness(24 + i * (160 + 22), 0, 0, ContentGrid.ActualHeight * 0.274),
                // };
                // newBtn.Click += PlanetsButton_Click;
                //
                // ContentGrid.Children.Add(newBtn);
            // }
        }
        // private void PlanetsButton_Click(object sender, RoutedEventArgs e)
        // {
        //     FrameworkElement sourceFrameworkElement = e.Source as FrameworkElement;
        //     if (sourceFrameworkElement != null)
        //     {
        //         // _mainPage.ChangeMap(ContentGrid,
        //         //     Planet.GoToPlanet(sourceFrameworkElement.Name[sourceFrameworkElement.Name.Length - 1]));
        //         int planetNr = sourceFrameworkElement.Name[sourceFrameworkElement.Name.Length - 1] - '0';
        //         _currentPlanet = _planets[planetNr];
        //         _planets[planetNr].Draw(ContentGrid, _currentPlanet, ListBox1);
        //     }
        //
        //     ContentGrid.Children.RemoveRange(0, 8);
        // }
        private void TickButtonClick(object sender, RoutedEventArgs e)
        {
            // if(_currentPlanet != null)
            // {
            //     for(int i = 0; i < 8; i++)
            //     {
            //         if (_currentPlanet.Name == (PlanetName)i)
            //             _planets[i] = _currentPlanet;
            //     }
            //     ContentGrid.Children.Clear();
            //     Engine eng = new Engine();
            //     eng.Tick(ContentGrid, _currentPlanet, ListBox1);
            //
            // }
        }
    }
}
