using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Giant_Robot_Killer
{
    public class MyGraphics
    {
        BitmapImage bmp;
        public int resX, resY;

        public void MainPage(ImageBrush display)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = Path.Combine(sCurrentDirectory, @"..\..\Resources\MainMenu.png");
            string sFilePath = Path.GetFullPath(sFile);
            bmp = new BitmapImage(new Uri(sFilePath));
            display.ImageSource = bmp;
        }
        public void ChangeMap(ImageBrush display, BitmapImage bmp)
        {
            display.ImageSource = bmp;
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
