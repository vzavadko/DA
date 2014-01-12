//using System;
//using System.Diagnostics;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using AForge.Imaging.ColorReduction;
//using AForge.Imaging.Filters;
//using Image = AForge.Imaging.Image;

//namespace XRay
//{
//    class Program
//    {
//        // PROPERTIES
//        protected static string[] arguments;

//        protected static String defaultSettingsPath = "settings.js";
//        protected static String defaultImagePath = "xray.jpg";
//        protected static XRaySettings settings;

//        protected static String FilePath
//        {
//            get
//            {
//                return arguments.Length > 0 && !String.IsNullOrEmpty(arguments[0]) ?
//                    arguments[0] : defaultImagePath;
//            }
//        }

//        protected static String TemporaryDirectory
//        {
//            get { return settings.TemporaryDirectoryPath; }
//        }

//        // METHODS
//        static void Main(string[] args)
//        {
//            arguments = args;

//            var originalImage = new Bitmap(FilePath);

//            InitializeSettings(args);

//            var image = PreProcessImage(args);

//            var resultingImage = ProcessImageData(originalImage, image);

//            var name = String.Format("result.{0}", GetImageExtension(resultingImage));

//            resultingImage.Save(name);

//            Process.Start(name);
//        }

//        private static Bitmap ProcessImageData(Bitmap image, Bitmap contourImage)
//        {
//            var resultingImage = new Bitmap(image);

//            var s = new Point(146, 26);
//            var a = new Point(132, 23);
//            var b = new Point(122, 47);
//            var d = new Point(175, 61);

//            var start = new Point(s.X - 5, s.Y - 2);
//            var point = new Point(134, 89);

//            Action<Bitmap, Point> drawPoint = (i, p) =>
//                                          {
//                                              i.SetPixel(p.X - 1, p.Y - 1, Color.Blue);
//                                              i.SetPixel(p.X - 1, p.Y, Color.Blue);
//                                              i.SetPixel(p.X - 1, p.Y + 1, Color.Blue);
//                                              i.SetPixel(p.X, p.Y - 1, Color.Blue);
//                                              i.SetPixel(p.X, p.Y, Color.Blue);
//                                              i.SetPixel(p.X, p.Y + 1, Color.Blue);
//                                              i.SetPixel(p.X + 1, p.Y - 1, Color.Blue);
//                                              i.SetPixel(p.X + 1, p.Y, Color.Blue);
//                                              i.SetPixel(p.X + 1, p.Y + 1, Color.Blue);
//                                          };

//            drawPoint(resultingImage, s);
//            drawPoint(resultingImage, a);
//            drawPoint(resultingImage, b);
//            drawPoint(resultingImage, d);
//            drawPoint(resultingImage, start);
//            drawPoint(resultingImage, point);

//            using (Graphics g = Graphics.FromImage(resultingImage))
//            {
//                g.DrawLine(new Pen(Color.Red), point, start);
//            }

//            for (int x = 0; x < image.Width; x++)
//            {
//                for (int y = 0; y < image.Height; y++)
//                {
//                    var pixel = contourImage.GetPixel(x, y);

//                    var brightness = pixel.GetBrightness();

//                    if (brightness < 70)
//                    {
//                        continue;
//                    }

//                    resultingImage.SetPixel(x, y, Color.Red);
//                }
//            }

//            return resultingImage;
//        }

//        private static void InitializeSettings(string[] args)
//        {
//            var serializer = new JavaScriptSerializer();

//            if (arguments.Length < 2)
//            {
//                InitializeDefaultSettings();

//                if (!File.Exists(defaultSettingsPath))
//                {
//                    SaveSettings(serializer);
//                }
//                else
//                {
//                    DeserializeSettingsFromFile(serializer, defaultSettingsPath);
//                }

//                return;
//            }

//            var settingsFilePath = arguments[2];

//            DeserializeSettingsFromFile(serializer, settingsFilePath);
//        }

//        private static void SaveSettings(JavaScriptSerializer serializer)
//        {
//            var serializedSettings = serializer.Serialize(settings);

//            var fileStream = new FileStream("settings.js", FileMode.OpenOrCreate);

//            var streamWriter = new StreamWriter(fileStream);

//            streamWriter.WriteLine(serializedSettings);

//            streamWriter.Close();
//            fileStream.Close();
//        }

//        private static void InitializeDefaultSettings()
//        {
//            settings = new XRaySettings
//                           {
//                               TemporaryDirectoryPath = @"D:\temp\images\",
//                               TeethCentre = new Point(90, 156),
//                               MolarTeethCentre = new Point(152, 57),
//                               UpperLeftPoint = new Point(25, 11),
//                               LowerRightPoint = new Point(202, 109)
//                           };
//        }

//        private static void DeserializeSettingsFromFile(JavaScriptSerializer serializer, string settingsFilePath)
//        {
//            if (File.Exists(settingsFilePath))
//            {
//                var fileStream = new FileStream("settings.js", FileMode.OpenOrCreate);

//                var streamWriter = new StreamReader(fileStream);

//                var serializedSettings = streamWriter.ReadToEnd();

//                streamWriter.Close();
//                fileStream.Close();

//                try
//                {
//                    settings = serializer.Deserialize<XRaySettings>(serializedSettings);
//                }
//                catch (Exception e)
//                {
//                    InitializeDefaultSettings();
//                }
//            }
//        }

//        private static Bitmap ProcessCanvasFiltering(string filePath, byte colorsAmount, byte lowThreshold, byte highThreshold)
//        {
//            var sourceImage = new Bitmap(filePath);

//            var colorImageQuantizer = new ColorImageQuantizer(new MedianCutQuantizer());

//            var colorTable = colorImageQuantizer.CalculatePalette(sourceImage, colorsAmount);

//            var temporaryImage = colorImageQuantizer.ReduceColors(sourceImage, colorTable);

//            var grayscale = Grayscale.CommonAlgorithms.BT709;

//            var temporaryImage2 = grayscale.Apply(Image.Clone(temporaryImage, PixelFormat.Format24bppRgb));

//            var edgeDetector = new CannyEdgeDetector
//            {
//                HighThreshold = highThreshold,
//                LowThreshold = lowThreshold,
//                GaussianSize = 6
//            };

//            var finalImage = edgeDetector.Apply(temporaryImage2);

//            return finalImage;
//        }

//        private static Bitmap PreProcessImage(string[] args)
//        {
//            var filePath = FilePath;
//            var tempPath = TemporaryDirectory;

//            var amount = (byte)64;
//            var lowThreshhold = (byte)25;
//            var highThreshhold = (byte)55;


//            var finalImage = ProcessCanvasFiltering(filePath, amount, lowThreshhold, highThreshhold);

//            var fileExtension = GetImageExtension(finalImage);

//            var imageCodec =
//                ImageCodecInfo.GetImageEncoders().SingleOrDefault(
//                    codec => codec.MimeType == "image/jpeg");

//            bool saveBitmaps = false;

//            string imageName;
//            if (!saveBitmaps || imageCodec == null)
//            {
//                imageName = String.Format("{3}c{0}-lt{1}-ht{2}-sgm.{4}", amount, lowThreshhold,
//                                          highThreshhold,
//                                          tempPath, fileExtension);
//                finalImage.Save(imageName);
//            }

//            var encoderParams = new EncoderParameters(1);
//            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)100);

//            imageName = String.Format("{3}c{0}-lt{1}-ht{2}-sgm.jpg", amount, lowThreshhold,
//                                      highThreshhold,
//                                      tempPath);

//            finalImage.Save(imageName, imageCodec, encoderParams);

//            return finalImage;
//        }

//        private static string GetImageExtension(System.Drawing.Image finalImage)
//        {
//            var fileExtension = "bmp";
//            var imageFormat = finalImage.RawFormat;

//            if (imageFormat.Guid == ImageFormat.Emf.Guid)
//            {
//                fileExtension = "emf";
//            }

//            if (imageFormat.Guid == ImageFormat.Exif.Guid)
//            {
//                fileExtension = "exif";
//            }

//            if (imageFormat.Guid == ImageFormat.Gif.Guid)
//            {
//                fileExtension = "gif";
//            }

//            if (imageFormat.Guid == ImageFormat.Icon.Guid)
//            {
//                fileExtension = "ico";
//            }

//            if (imageFormat.Guid == ImageFormat.Jpeg.Guid)
//            {
//                fileExtension = "jpeg";
//            }

//            if (imageFormat.Guid == ImageFormat.Png.Guid)
//            {
//                fileExtension = "png";
//            }

//            if (imageFormat.Guid == ImageFormat.Tiff.Guid)
//            {
//                fileExtension = "tiff";
//            }

//            return fileExtension;
//        }
//    }
//}
