using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging.ColorReduction;
using AForge.Imaging.Filters;
using Image = AForge.Imaging.Image;

namespace XRayCore
{
    public class ImageProcessor
    {
        protected static ColorImageQuantizer imageQuantizer;
        protected static Grayscale grayscale;
        protected static CannyEdgeDetector edgeDetector;

        static ImageProcessor()
        {
            imageQuantizer = new ColorImageQuantizer(new MedianCutQuantizer());

            grayscale = Grayscale.CommonAlgorithms.BT709;

            edgeDetector = new CannyEdgeDetector();
        }

        public static Bitmap ReduceColors(Bitmap image, uint colorsAmount)
        {
            var palette = imageQuantizer.CalculatePalette(image, (int) colorsAmount);

            return ReduceColors(image, palette);
        }

        public static Bitmap ReduceColors(Bitmap image, Color[] palette)
        {
            return imageQuantizer.ReduceColors(image, palette);
        }

        public static Bitmap ConvertTo8Grayscale(Bitmap image)
        {
            return grayscale.Apply(Image.Clone(image, PixelFormat.Format24bppRgb));
        }

        public static Bitmap ProcessCannyEdgeDetector(Bitmap image, byte lowThreshold, byte highThreshold)
        {
            edgeDetector.LowThreshold = lowThreshold;
            edgeDetector.HighThreshold = highThreshold;

            return edgeDetector.Apply(image);
        }

        public static Bitmap ProcessImageData(Bitmap image, Bitmap contourImage)
        {
            var resultingImage = new Bitmap(image);

            var s = new Point(146, 40); // upper last
            var a = new Point(114, 75); // down healthy tooth
            var b = new Point(106, 47); // up healthy tooth
            //var d = new Point(1, 1);

           //var start = new Point(s.X - 5, s.Y - 2);
            var start = new Point(137, 36); //line upper point
            var point = new Point(147, 80); //line down point

            Action<Bitmap, Point> drawPoint = (i, p) =>
            {
                i.SetPixel(p.X - 1, p.Y - 1, Color.Blue);
                i.SetPixel(p.X - 1, p.Y, Color.Blue);
                i.SetPixel(p.X - 1, p.Y + 1, Color.Blue);
                i.SetPixel(p.X, p.Y - 1, Color.Blue);
                i.SetPixel(p.X, p.Y, Color.Blue);
                i.SetPixel(p.X, p.Y + 1, Color.Blue);
                i.SetPixel(p.X + 1, p.Y - 1, Color.Blue);
                i.SetPixel(p.X + 1, p.Y, Color.Blue);
                i.SetPixel(p.X + 1, p.Y + 1, Color.Blue);
            };

            drawPoint(resultingImage, s);
            drawPoint(resultingImage, a);
            drawPoint(resultingImage, b);
            //drawPoint(resultingImage, d);
            drawPoint(resultingImage, start);
            drawPoint(resultingImage, point);

            using (Graphics g = Graphics.FromImage(resultingImage))
            {
                g.DrawLine(new Pen(Color.Red), point, start);
            }

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var pixel = contourImage.GetPixel(x, y);

                    var brightness = pixel.GetBrightness();

                    if (brightness < 70)
                    {
                        continue;
                    }

                    resultingImage.SetPixel(x, y, Color.Red);
                }
            }

            return resultingImage;
        }
    }
}