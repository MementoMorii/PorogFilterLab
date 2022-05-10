namespace Lab2PorogFilter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Drawing;
    public class ImageService
    {
        public Bitmap getImage()
        {
            return(Bitmap)Image.FromFile("path");   
        }
    }
}
