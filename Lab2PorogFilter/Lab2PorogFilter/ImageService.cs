namespace Lab2PorogFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Drawing;
    using System.Reflection;

    public class ImageService
    {
        private string _appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        public Bitmap GetImage()
        {
            var relativePath = @"Images\BaseImage_1024x768.png";
            //var fullPath = Path.Combine(relativePath);
            return(Bitmap)Image.FromFile(relativePath);   
        }

        public void SaveImage(Bitmap image)
        {
            var relativePath = @"../../../ResultImageM_1024x768.png";
            var fullPath = Path.Combine(relativePath);
            image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
