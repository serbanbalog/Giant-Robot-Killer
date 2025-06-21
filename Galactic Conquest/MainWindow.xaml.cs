using System.Windows;
using System.Windows.Controls;

namespace Galactic_Conquest;

public partial class MainWindow : Window
{
    private bool _initialized;
    private readonly MainPage _mainPage;

    public MainWindow()
    {
        InitializeComponent();
        _mainPage = new MainPage(ContentGrid);
    }

    private void MainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (!_initialized)
        {
            _mainPage.RenderMainPage();
            _initialized = true;
            return;
        }

        double widthScale = e.NewSize.Width / e.PreviousSize.Width;
        double heightScale = e.NewSize.Height / e.PreviousSize.Height;

        foreach (UIElement element in ContentGrid.Children)
        {
            if (element is FrameworkElement frameworkElement)
            {
                frameworkElement.Width *= widthScale;
                frameworkElement.Height *= heightScale;

                if (frameworkElement is Control control)
                {
                    control.FontSize = control.FontSize * heightScale;
                }

                frameworkElement.Margin = new Thickness(
                    frameworkElement.Margin.Left * widthScale,
                    frameworkElement.Margin.Top * heightScale,
                    frameworkElement.Margin.Right * widthScale,
                    frameworkElement.Margin.Bottom * heightScale);
            }
        }
    }
}