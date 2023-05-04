using System;
using System.Collections.Generic;
using System.Drawing;

namespace removebackground
{
    class ImageProcessing
    {
        private Bitmap image;
        public int threshold;

        public ImageProcessing(Bitmap image, int threshold)
        {
            this.image = image;
            this.threshold = threshold;
        }

        public ImageProcessing(Image image, int threshold)
        {
            this.image = new Bitmap(image);
            this.threshold = threshold;
        }

        public Bitmap DeleteBackground(Color backgroundColor)
        {
            List<Pixel> pixels = GetPixels(); // список всех пикселей картинки
            List<Pixel> background = GetBackground(pixels, backgroundColor); // список пикселей заднего фона

            for (int i = 0; i < background.Count; i++)
            {
                image.SetPixel(background[i].Point.X, background[i].Point.Y, Color.Transparent);
            }

            return image;
        }
        public Bitmap MakeSolidBackground(Color backgroundColor, Color colorChangeTo)
        {
            List<Pixel> pixels = GetPixels();
            List<Pixel> background = GetBackground(pixels, backgroundColor);

            for (int i = 0; i < background.Count; i++)
            {
                image.SetPixel(background[i].Point.X, background[i].Point.Y, colorChangeTo);
            }

            return image;
        }
        public Bitmap LightenBackground(Color backgroundColor)
        {
            List<Pixel> pixels = GetPixels();
            List<Pixel> background = GetBackground(pixels, backgroundColor);

            for (int i = 0; i < background.Count; i++)
            {
                var R = background[i].Color.R + (0.25 * (255 - background[i].Color.R));
                var G = background[i].Color.G + (0.25 * (255 - background[i].Color.G));
                var B = background[i].Color.B + (0.25 * (255 - background[i].Color.B));
                image.SetPixel(background[i].Point.X, background[i].Point.Y, Color.FromArgb((int)R, (int)G, (int)B));
            }

            return image;
        }
        private List<Pixel> GetPixels()
        {
            var pixels = new List<Pixel>(image.Width * image.Height);

            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    pixels.Add(new Pixel()
                    {
                        Color = image.GetPixel(x, y),
                        Point = new Point() { X = x, Y = y }
                    });
            return pixels;
        }

        //возвращает список пикселей, которые являются частью фона(основной алгоритм)
        private List<Pixel> GetBackground(List<Pixel> allPixels, Color backgroundColor)
        {
            List<Pixel> backgroundPixels = new List<Pixel>(allPixels.Count);

            foreach (var pixel in allPixels)
            {
                Color pixelColor = pixel.Color;

                if (ColorDistance(pixelColor, backgroundColor) < threshold)
                {
                    backgroundPixels.Add(pixel);
                }

            }

            return backgroundPixels;
        }

        private static int ColorDistance(Color c1, Color c2)
        {
            int rDiff = c1.R - c2.R;
            int gDiff = c1.G - c2.G;
            int bDiff = c1.B - c2.B;

            return (int)Math.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
        }
    }
}
