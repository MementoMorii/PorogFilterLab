namespace Lab2PorogFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Drawing;
    using System.Threading;

    public class PorogFilterService
    {
        public Bitmap FilterImage(Bitmap baseImage, double max)
        {
            var height = baseImage.Height;
            var width = baseImage.Width;
            var resultImage = new Bitmap(width, height);
            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var pixel = baseImage.GetPixel(i, j);
                    double value = (pixel.R + pixel.G + pixel.B) / 3;
                    if (value < max * 0.3)
                        resultImage.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    else
                        resultImage.SetPixel(i,j, pixel);
                }
            }

            return resultImage;
        }

        public double GetMax(Bitmap image)
        {
            var height = image.Height;
            var width = image.Width;
            double max = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var pixel = image.GetPixel(i, j);
                    double value = (pixel.R + pixel.G + pixel.B) / 3;
                    if (max < value)
                        max = value;
                }
            }

            return max;
        }

        public IList<Bitmap> FilterImageMultitreading(Bitmap baseImage, double max)
        {
            var height = baseImage.Height;
            var width = baseImage.Width;
            var resultListBitmaps = new List<Bitmap>();
            var countThreads = 4;
            var countIterationsForThread = (int)width / countThreads;
            var threads = new Thread[countThreads];
            for (int c = 0; c < countThreads; c++)
            {
                var baseImageForThread = baseImage.Clone();
                var resultImage = new Bitmap(width, height);
                var numberStartIterationForThread = countIterationsForThread * c;
                var numberEndIterationForThread = c == countThreads - 1 ? width : countIterationsForThread * (c + 1);
                threads[c] = new Thread(() =>
                {
                    for (int i = numberStartIterationForThread; i < numberEndIterationForThread; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            var pixel = (baseImageForThread as Bitmap).GetPixel(i, j);
                            double value = (pixel.R + pixel.G + pixel.B) / 3;
                            if (value < max * 0.3)
                                resultImage.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                            else
                                resultImage.SetPixel(i, j, pixel);
                        }
                    }
                    resultListBitmaps.Add(resultImage);
                });
                threads[c].Start();
            }

            var AllThreadsDone = false;

            while (!AllThreadsDone)
            {
                AllThreadsDone = true;
                foreach (var thread in threads)
                {
                    if (thread.ThreadState == ThreadState.Running)
                        AllThreadsDone = false;
                }
            }

            return resultListBitmaps;
        }

        public Bitmap ConcatBitmaps(IList<Bitmap> listBitmaps)
        {
            var width = listBitmaps[0].Width;
            var height = listBitmaps[0].Height;
            var resultImage = new Bitmap(width, height);
            var countThreads = listBitmaps.Count();
            var countIterationsForThread = (int)width / countThreads;

            for (int c = 0; c < countThreads; c++)
            {
                var numberStartIterationForThread = countIterationsForThread * c;
                var numberEndIterationForThread = c == countThreads - 1 ? width : countIterationsForThread * (c + 1);
                for (int i = numberStartIterationForThread; i < numberEndIterationForThread; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        resultImage.SetPixel(i, j, listBitmaps[c].GetPixel(i, j));
                    }
                }
            }

            return resultImage;
        }
    }
}
