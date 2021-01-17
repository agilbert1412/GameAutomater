using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTD6Automater
{
    public class DigitDetector
    {
        private double multiplierX;
        private double multiplierY;

        public DigitDetector(int resolutionX, int resolutionY)
        {
            multiplierX = resolutionX / 1024.0;
            multiplierY = resolutionY / 768.0;
        }

        internal bool IsZero(Bitmap image, int middleX, int middleY)
        {
            return IsBlack(image.GetPixel(middleX, middleY))
                            && IsBlack(image.GetPixel(middleX - 1, middleY))
                            && IsBlack(image.GetPixel(middleX + 1, middleY))
                            && IsBlack(image.GetPixel(middleX, middleY - 1))
                            && IsBlack(image.GetPixel(middleX, middleY + 1))
                            && IsBlack(image.GetPixel(middleX, middleY - 2))
                            && IsBlack(image.GetPixel(middleX, middleY + 2));
        }

        internal bool IsOne(int charWidth)
        {
            return charWidth < 12 * ((multiplierX - 1) * 0.5 + 1);
        }

        internal bool IsTwo(Bitmap image, int x, int y, int width, int height)
        {
            var marginX = (int)(2 * multiplierX) + 1;
            var marginY = (int)(2 * multiplierY) + 1;

            var heightWhiteBar = y + height - marginY;

            for (var i = x + marginX; i < x + width - marginX; i++)
            {
                if (IsBlack(image.GetPixel(i, heightWhiteBar)))
                {
                    return false;
                }
            }

            return true;
        }

        internal bool IsThree(Bitmap image, int height, int middleX, int middleY)
        {
            var yFirstHole = middleY - (height / 6);
            var ySecondHole = middleY + (height / 6) + 1;

            var is3 = IsWhite(image.GetPixel(middleX, middleY))
                && IsWhite(image.GetPixel(middleX + 1, middleY))
                && IsWhite(image.GetPixel(middleX - 1, middleY))
                && IsWhite(image.GetPixel(middleX + 2, middleY))
                && IsWhite(image.GetPixel(middleX - 2, middleY))
                && IsBlack(image.GetPixel(middleX, yFirstHole))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 2))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX, ySecondHole))
                && IsBlack(image.GetPixel(middleX, ySecondHole - 1))
                && IsBlack(image.GetPixel(middleX, ySecondHole - 2));

            for (var i = 1; i <= (5 * multiplierX); i++)
            {
                is3 = is3 && IsBlack(image.GetPixel(middleX - i, yFirstHole - 1));
                is3 = is3 && IsBlack(image.GetPixel(middleX - i, ySecondHole - 1));
            }

            return is3;
        }

        internal bool IsFour(Bitmap image, int x, int y, int width, int height)
        {
            var marginX = (int)(2 * multiplierX);
            var marginY = (int)(2 * multiplierY);

            var heightWhiteBar = y + (height*2/3) - marginY;

            for (var i = x + marginX; i < x + width - marginX; i++)
            {
                if (IsBlack(image.GetPixel(i, heightWhiteBar)))
                {
                    return false;
                }
            }

            return true;
        }

        internal bool IsFive(Bitmap image, int height, int middleX, int middleY)
        {
            var yFirstHole = middleY - (height / 8) - 1;
            var ySecondHole = middleY + (height / 8);

            var is5 = IsWhite(image.GetPixel(middleX, middleY))
                      && IsWhite(image.GetPixel(middleX + 1, middleY))
                      && IsWhite(image.GetPixel(middleX - 1, middleY))
                      && IsWhite(image.GetPixel(middleX + 2, middleY))
                      && IsWhite(image.GetPixel(middleX - 2, middleY))
                      && IsBlack(image.GetPixel(middleX, yFirstHole))
                      && IsBlack(image.GetPixel(middleX, yFirstHole - 1))
                      && IsBlack(image.GetPixel(middleX, yFirstHole - 2))
                      && IsBlack(image.GetPixel(middleX, ySecondHole))
                      && IsBlack(image.GetPixel(middleX, ySecondHole + 1))
                      && IsBlack(image.GetPixel(middleX, ySecondHole - 1));

            for (var i = 1; i <= (5 * multiplierX); i++)
            {
                is5 = is5 && IsBlack(image.GetPixel(middleX + i, yFirstHole));
                is5 = is5 && IsBlack(image.GetPixel(middleX - i, ySecondHole));
            }

            return is5;
        }

        internal bool IsSix(Bitmap image, int height, int middleX, int middleY)
        {
            var yFirstHole = middleY - (height / 6);
            var ySecondHole = middleY + (height / 6) + 1;

            var is6 = IsWhite(image.GetPixel(middleX, middleY))
                      && IsWhite(image.GetPixel(middleX + 1, middleY))
                      && IsWhite(image.GetPixel(middleX - 1, middleY))
                      && IsWhite(image.GetPixel(middleX + 2, middleY))
                      && IsWhite(image.GetPixel(middleX - 2, middleY))
                      && IsBlack(image.GetPixel(middleX, yFirstHole))
                      && IsBlack(image.GetPixel(middleX, yFirstHole + 1))
                      && IsBlack(image.GetPixel(middleX, yFirstHole - 1))
                      && IsBlack(image.GetPixel(middleX, yFirstHole - 2))
                      && IsBlack(image.GetPixel(middleX - 1, yFirstHole))
                      && IsBlack(image.GetPixel(middleX, ySecondHole))
                      && IsBlack(image.GetPixel(middleX, ySecondHole + 1))
                      && IsBlack(image.GetPixel(middleX, ySecondHole - 1))
                      && IsBlack(image.GetPixel(middleX, ySecondHole + 2))
                      && IsBlack(image.GetPixel(middleX + 1, ySecondHole))
                      && IsBlack(image.GetPixel(middleX - 1, ySecondHole))
                      && IsBlack(image.GetPixel(middleX + 1, ySecondHole + 1))
                      && IsBlack(image.GetPixel(middleX - 1, ySecondHole + 1));

            for (var i = 1; i <= (5 * multiplierX); i++)
            {
                is6 = is6 && IsBlack(image.GetPixel(middleX + i, yFirstHole));
            }

            return is6;
        }

        internal bool IsSeven(Bitmap image, int x, int y, int width)
        {
            var marginX = (int)(2 * multiplierX);
            var marginY = (int)(2 * multiplierY);

            var heightWhiteBar = y + marginY;

            for (var i = x + marginX; i < x + width - marginX; i++)
            {
                if (IsBlack(image.GetPixel(i, heightWhiteBar)))
                {
                    return false;
                }
            }

            return true;
        }

        internal bool IsEight(Bitmap image, int height, int middleX, int middleY)
        {
            var yFirstHole = middleY - (height / 8);
            var ySecondHole = middleY + (height / 8) + 1;

            return IsWhite(image.GetPixel(middleX, middleY))
                && IsWhite(image.GetPixel(middleX + 1, middleY))
                && IsWhite(image.GetPixel(middleX - 1, middleY))
                && IsWhite(image.GetPixel(middleX + 2, middleY))
                && IsWhite(image.GetPixel(middleX - 2, middleY))
                && IsBlack(image.GetPixel(middleX, yFirstHole))
                && IsBlack(image.GetPixel(middleX, yFirstHole + 1))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 2))
                && IsBlack(image.GetPixel(middleX + 1, yFirstHole))
                && IsBlack(image.GetPixel(middleX - 1, yFirstHole))
                && IsBlack(image.GetPixel(middleX + 2, yFirstHole))
                && IsBlack(image.GetPixel(middleX - 2, yFirstHole))
                && IsBlack(image.GetPixel(middleX, ySecondHole))
                && IsBlack(image.GetPixel(middleX, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX, ySecondHole - 1))
                && IsBlack(image.GetPixel(middleX, ySecondHole + 2))
                && IsBlack(image.GetPixel(middleX + 1, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 1, ySecondHole))
                && IsBlack(image.GetPixel(middleX + 1, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX - 1, ySecondHole + 1));

        }

        internal bool IsNine(Bitmap image, int height, int middleX, int middleY)
        {
            var yFirstHole = middleY - (height / 8) - 1;
            var ySecondHole = middleY + (height / 8) + 1;

            var is9 = IsWhite(image.GetPixel(middleX, middleY))
                      && IsWhite(image.GetPixel(middleX + 1, middleY))
                      && IsWhite(image.GetPixel(middleX - 1, middleY))
                      && IsWhite(image.GetPixel(middleX + 2, middleY))
                      && IsWhite(image.GetPixel(middleX - 2, middleY))
                      && IsBlack(image.GetPixel(middleX, yFirstHole))
                      && IsBlack(image.GetPixel(middleX, yFirstHole + 1))
                      && IsBlack(image.GetPixel(middleX, yFirstHole - 1))
                      && IsBlack(image.GetPixel(middleX, yFirstHole - 2))
                      && IsBlack(image.GetPixel(middleX + 1, yFirstHole))
                      && IsBlack(image.GetPixel(middleX - 1, yFirstHole))
                      && IsBlack(image.GetPixel(middleX + 2, yFirstHole))
                      && IsBlack(image.GetPixel(middleX - 2, yFirstHole))
                      && IsBlack(image.GetPixel(middleX, ySecondHole))
                      && IsBlack(image.GetPixel(middleX, ySecondHole + 1))
                      && IsBlack(image.GetPixel(middleX, ySecondHole - 1))
                      && IsBlack(image.GetPixel(middleX + 1, ySecondHole));

            for (var i = 1; i <= (5 * multiplierX); i++)
            {
                is9 = is9 && IsBlack(image.GetPixel(middleX - i, ySecondHole));
            }

            return is9;
        }

        private bool IsBlack(Color color)
        {
            return color.R == 0 && color.G == 0 && color.B == 0;
        }

        private bool IsWhite(Color color)
        {
            return color.R == 255 && color.G == 255 && color.B == 255;
        }
    }
}
