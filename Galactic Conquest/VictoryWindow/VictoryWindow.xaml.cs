using System.Windows;

namespace Galactic_Conquest.VictoryWindow;

public partial class VictoryWindow : Window
{
    public VictoryWindow(string factionName)
    {
        InitializeComponent();
        VictoryText.Text = $"{factionName} Wins!";
    }

    private void OnCloseClicked(object sender, RoutedEventArgs e)
    {
        Close();
    }
}