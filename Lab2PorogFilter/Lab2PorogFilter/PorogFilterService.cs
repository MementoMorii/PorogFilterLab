namespace Lab2PorogFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Drawing;
    public class PorogFilterService
    {
        public Bitmap FilterImage(Bitmap baseImage, int max)
        {
            var height = baseImage.Height;
            var width = baseImage.Width;
            var resultImage = new Bitmap(width, height);
            
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var pixel = baseImage.GetPixel(i, j);
                    double value = (pixel.R + pixel.G + pixel.B) / 3;
                    if (value < max * 0.1)
                        resultImage.SetPixel(i, j, Color.FromArgb(0,0,0));
                }
            }

            return resultImage;
        }

        public double getMax(Bitmap image)
        {
            var height = image.Height;
            var width = image.Width;
            double max = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var pixel = image.GetPixel(i,j);
                    double value = (pixel.R + pixel.G + pixel.B) / 3;
                    if (max < value)
                        max = value;
                }
            }

            return max;
        }
    }
}
