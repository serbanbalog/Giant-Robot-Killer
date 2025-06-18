using System.Windows;

namespace Giant_Robot_Killer.VictoryWindow;

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