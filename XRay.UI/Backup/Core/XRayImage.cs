using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
//using XRayCore;

namespace XRay.UI.Core
{
    [Serializable]
    public class XRayImage
    {
        public String Description { get; set; }
        public DateTime Created { get; set; }

        [XmlIgnore]
        public Image OriginalImage { get; set; }

        [XmlIgnore]
        public Image ProcessedImage { get; set; }

        public string OriginalImageSerialized
        {
            get { return ImageToString(OriginalImage); }
            set { OriginalImage = StringToImage(value); }
        }
        public string ProcessedImageSerialized 
        {
            get { return ImageToString(ProcessedImage); }
            set { ProcessedImage = StringToImage(value); }
        }

        private static string ImageToString(Image image)
        {
            if (image == null)
            {
                return String.Empty;
            }

            var ms = new MemoryStream();

            image.Save(ms, image.RawFormat);

            var array = ms.ToArray();

            return Convert.ToBase64String(array);
        }

        private static Image StringToImage(string imageString)
        {
            if (String.IsNullOrEmpty(imageString))
            {
                return null;
            }

            var array = Convert.FromBase64String(imageString);

            var image = Image.FromStream(new MemoryStream(array));

            return image;

        }

        public XRayImage()
        {
        }

        public XRayImage(string description, DateTime created, Image originalImage, Image processedImage)
        {
            Description = description;
            Created = created;
            OriginalImage = originalImage;
            ProcessedImage = processedImage;
        }

        public XRayImage(string description, string originalImagePath)
        {
            Created = DateTime.Now;
            Description = description;
            OriginalImage = Image.FromFile(originalImagePath);

            //var contourImage = ImageProcessor.ConvertTo8Grayscale(new Bitmap(OriginalImage));

            //var processedImage = ImageProcessor.ProcessImageData(new Bitmap(OriginalImage), new Bitmap(OriginalImage));

            //ProcessedImage = (Image)processedImage;
        }
    }
}