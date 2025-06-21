using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Galactic_Conquest.ExtenstionMethods.Drawing;

public static class BitmapExtensionMethods
{
    public static ImageBrush ToImageBrush(this BitmapImage bitmap)
    {
        return new ImageBrush(bitmap);
    }
}