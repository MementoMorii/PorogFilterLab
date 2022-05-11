namespace Lab2PorogFilter
{
    using System;
    using System.Diagnostics;
    class Program
    {
        private static ImageService _imageService = new ImageService();
        private static PorogFilterService _porogFilterService = new PorogFilterService();
        //static void Main(string[] args)
        //{
        //    var image = _imageService.GetImage();
        //    var max = _porogFilterService.GetMax(image);
        //    var stopWath = new Stopwatch();
        //    stopWath.Start();
        //    var resultImage = _porogFilterService.FilterImage(image, max);
        //    stopWath.Stop();
        //    var timeSpan = stopWath.Elapsed;
        //    Console.WriteLine($"Время выполнения фильтрации картинки 1024*768 В синхронном режиме: {timeSpan.Minutes}:{timeSpan.Seconds}.{timeSpan.Milliseconds / 10} MM:SS.Milliseconds / 10");
        //    _imageService.SaveImage(resultImage);
        //}

        static void Main(string[] args)
        {
            var image = _imageService.GetImage();
            var max = _porogFilterService.GetMax(image);
            var stopWath = new Stopwatch();
            stopWath.Start();
            var resultListImage = _porogFilterService.FilterImageMultitreading(image, max);
            stopWath.Stop();
            var timeSpan = stopWath.Elapsed;
            Console.WriteLine($"Время выполнения фильтрации картинки 1024*768 В Многопоточном режиме: {timeSpan.Minutes}:{timeSpan.Seconds}.{timeSpan.Milliseconds / 10} MM:SS.Milliseconds / 10");
            var resultImage = _porogFilterService.ConcatBitmaps(resultListImage);
            _imageService.SaveImage(resultImage);
        }
    }
}
