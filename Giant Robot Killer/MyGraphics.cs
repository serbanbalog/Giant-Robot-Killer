using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Giant_Robot_Killer
{
    public class MyGraphics
    {
        BitmapImage _bmp;
        public int ResX, ResY;

        public void MainPage(ImageBrush display)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = Path.Combine(sCurrentDirectory, @"..\..\Resources\MainMenu.png");
            string sFilePath = Path.GetFullPath(sFile);
            _bmp = new BitmapImage(new Uri(sFilePath));
            display.ImageSource = _bmp;
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
