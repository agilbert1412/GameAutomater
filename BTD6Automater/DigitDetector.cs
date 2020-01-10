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

        internal bool IsOne(Bitmap image, int charWidth)
        {
            return charWidth < 14;
        }

        internal bool IsTwo(Bitmap image, int x, int y, int width, int height)
        {
            var heightWhiteBar = y + height - 2;

            for (var i = x + 2; i < x + width - 2; i++)
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
            var yFirstHole = middleY - (height / 8);
            var ySecondHole = middleY + (height / 8) + 1;

            return IsWhite(image.GetPixel(middleX, middleY))
                && IsWhite(image.GetPixel(middleX + 1, middleY))
                && IsWhite(image.GetPixel(middleX - 1, middleY))
                && IsWhite(image.GetPixel(middleX + 2, middleY))
                && IsWhite(image.GetPixel(middleX - 2, middleY))
                && IsBlack(image.GetPixel(middleX, yFirstHole))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 2))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX - 1, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX - 2, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX - 3, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX - 4, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX - 5, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX, ySecondHole))
                && IsBlack(image.GetPixel(middleX, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX - 1, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX - 2, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX - 3, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX - 4, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX - 5, ySecondHole + 1));
        }

        internal bool IsFour(Bitmap image, int x, int y, int width, int height)
        {
            var heightWhiteBar = y + (height*2/3) - 2;

            for (var i = x + 2; i < x + width - 2; i++)
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

            return IsWhite(image.GetPixel(middleX, middleY))
                && IsWhite(image.GetPixel(middleX + 1, middleY))
                && IsWhite(image.GetPixel(middleX - 1, middleY))
                && IsWhite(image.GetPixel(middleX + 2, middleY))
                && IsWhite(image.GetPixel(middleX - 2, middleY))
                && IsBlack(image.GetPixel(middleX, yFirstHole))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 1))
                && IsBlack(image.GetPixel(middleX, yFirstHole - 2))
                && IsBlack(image.GetPixel(middleX + 1, yFirstHole))
                && IsBlack(image.GetPixel(middleX + 2, yFirstHole))
                && IsBlack(image.GetPixel(middleX + 3, yFirstHole))
                && IsBlack(image.GetPixel(middleX + 4, yFirstHole))
                && IsBlack(image.GetPixel(middleX + 5, yFirstHole))
                && IsBlack(image.GetPixel(middleX, ySecondHole))
                && IsBlack(image.GetPixel(middleX, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX, ySecondHole - 1))
                && IsBlack(image.GetPixel(middleX - 1, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 2, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 3, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 4, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 5, ySecondHole));
        }

        internal bool IsSix(Bitmap image, int height, int middleX, int middleY)
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
                && IsBlack(image.GetPixel(middleX + 3, yFirstHole))
                && IsBlack(image.GetPixel(middleX + 4, yFirstHole))
                && IsBlack(image.GetPixel(middleX + 5, yFirstHole))
                && IsBlack(image.GetPixel(middleX, ySecondHole))
                && IsBlack(image.GetPixel(middleX, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX, ySecondHole - 1))
                && IsBlack(image.GetPixel(middleX, ySecondHole + 2))
                && IsBlack(image.GetPixel(middleX + 1, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 1, ySecondHole))
                && IsBlack(image.GetPixel(middleX + 1, ySecondHole + 1))
                && IsBlack(image.GetPixel(middleX - 1, ySecondHole + 1));
        }

        internal bool IsSeven(Bitmap image, int x, int y, int width)
        {
            var heightWhiteBar = y + 2;

            for (var i = x + 2; i < x + width - 2; i++)
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
                && IsBlack(image.GetPixel(middleX + 1, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 1, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 2, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 3, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 4, ySecondHole))
                && IsBlack(image.GetPixel(middleX - 5, ySecondHole));
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
