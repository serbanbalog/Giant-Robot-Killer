using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Giant_Robot_Killer;

public class StatsBar
{
    public event Action MainMenuClicked;
    public event Action TickClicked;
    public event Action<bool> AutoPlayToggled;

    private readonly DockPanel _bar;
    private readonly TextBlock _turnCounter;
    private readonly Button _mainMenuButton;
    private readonly Button _tickButton;
    private readonly Button _autoPlayButton;
    private readonly DispatcherTimer _autoPlayTimer;
    private bool _autoPlayEnabled;

    public UIElement BarUI => _bar;

    public StatsBar()
    {
        _bar = new DockPanel
        {
            LastChildFill = true,
            Background = new SolidColorBrush(Color.FromArgb(220, 20, 20, 30)),
            Height = 50,
            VerticalAlignment = VerticalAlignment.Bottom,
            Margin = new Thickness(0),
            Tag = "StatsBar"       
        };

        _mainMenuButton = new Button
        {
            Content = "Main Menu",
            Width = 100,
            Margin = new Thickness(8, 5, 8, 5)
        };
        _mainMenuButton.Click += (s, e) => MainMenuClicked?.Invoke();

        _turnCounter = new TextBlock
        {
            Text = $"Turn: 0",
            Foreground = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            FontWeight = FontWeights.Bold,
            FontSize = 18,
            Margin = new Thickness(18, 0, 18, 0)
        };

        _tickButton = new Button
        {
            Content = "Tick",
            Width = 75,
            Margin = new Thickness(8, 5, 8, 5)
        };
        _tickButton.Click += (s, e) => TickClicked?.Invoke();

        _autoPlayButton = new Button
        {
            Content = "Auto Play",
            Width = 90,
            Margin = new Thickness(8, 5, 8, 5)
        };
        _autoPlayButton.Click += (s, e) => ToggleAutoPlay();

        _bar.Children.Add(_mainMenuButton);
        _bar.Children.Add(_turnCounter);
        _bar.Children.Add(_tickButton);
        _bar.Children.Add(_autoPlayButton);

        _autoPlayTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _autoPlayTimer.Tick += (s, e) => TickClicked?.Invoke();
    }

    public void SetTurn(int turn)
    {
        _turnCounter.Text = $"Turn: {turn}";
    }

    private void ToggleAutoPlay()
    {
        _autoPlayEnabled = !_autoPlayEnabled;
        if (_autoPlayEnabled)
        {
            _autoPlayTimer.Start();
            _autoPlayButton.Content = "Stop Auto";
        }
        else
        {
            _autoPlayTimer.Stop();
            _autoPlayButton.Content = "Auto Play";
        }

        AutoPlayToggled?.Invoke(_autoPlayEnabled);
    }

    public void StopAutoPlay()
    {
        _autoPlayEnabled = false;
        _autoPlayTimer.Stop();
        _autoPlayButton.Content = "Auto Play";
    }
}