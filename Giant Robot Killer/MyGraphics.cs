using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Giant_Robot_Killer.ExtenstionMethods.Drawing;

namespace Giant_Robot_Killer
{
    public class MyGraphics
    {
        BitmapImage _bmp;
        public int ResX, ResY;

        public void MainPage(Grid display)
        {
            // string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // string sFile = Path.Combine(sCurrentDirectory, @"..\..\Resources\MainMenu.png");
            // string sFilePath = Path.GetFullPath(sFile);
            // // _bmp = new BitmapImage(new Uri(sFilePath));
            // // display.Background = _bmp.ToImageBrush();
            // // display.Width = _bmp.Width;
            // // display.Height = _bmp.Height;
        }
        public void ChangeMap(Grid display, BitmapImage bmp)
        {
            display.Background = _bmp.ToImageBrush();
;
        }
/*        public void ClearGraph(System.Windows.Controls.Image display)
        {
            display.Source = null;
        }

        public void RefreshGraph(System.Windows.Controls.Image display)
        {
            display.Source = bmp;
        }*/

    }
}
