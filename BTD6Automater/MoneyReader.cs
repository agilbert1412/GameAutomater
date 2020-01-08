using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace BTD6Automater
{
    public class MoneyReader
    {
        public int ReadMoney()
        {
            var rect = new Rectangle(270, 15, 150, 40);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

            bmp.Save("Test.jpg", ImageFormat.Jpeg);

            return ReadAmountFromPicture(bmp);
        }

        public int ReadAmountFromPicture(Bitmap image)
        {
            int amount = 0;

            int.TryParse(ReadTextFromPicture(image), out amount);

            return amount;
        }

        public string ReadTextFromPicture(Bitmap image)
        {
            var bmp = MakeBlackAndWhite(image);

            var charSeparators = GetCharSeparators(bmp);

            if (charSeparators.Count % 2 != 0)
            {
                return "0";
            }

            var text = "";

            for (var i = 0; i < charSeparators.Count - 1; i+=2)
            {
                var charStartX = charSeparators[i];
                var charEnd = charSeparators[i + 1];
                var widthChar = charEnd - charStartX;

                int charStartY, charHeight;
                GetCharBoundsY(image, out charStartY, out charHeight);

                text += AnalyzeChar(bmp, charStartX, charStartY, widthChar, charHeight);
            }

            return text;
        }

        private string AnalyzeChar(Bitmap image, int charStartX, int charStartY, int charWidth, int charHeight)
        {

            var middleX = charStartX + (charWidth / 2);
            var middleY = charStartY + (charHeight / 2);

            var digitReader = new DigitDetector();

            if (digitReader.IsOne(image, charWidth))
            {
                return "1";
            }

            if (digitReader.IsZero(image, middleX, middleY))
            {
                return "0";
            }

            if (digitReader.IsThree(image, charHeight, middleX, middleY))
            {
                return "3";
            }

            if (digitReader.IsFive(image, charHeight, middleX, middleY))
            {
                return "5";
            }

            if (digitReader.IsSix(image, charHeight, middleX, middleY))
            {
                return "6";
            }

            if (digitReader.IsNine(image, charHeight, middleX, middleY))
            {
                return "9";
            }

            if (digitReader.IsEight(image, charHeight, middleX, middleY))
            {
                return "8";
            }

            if (digitReader.IsFour(image, charStartX, charStartY, charWidth, charHeight))
            {
                return "4";
            }

            if (digitReader.IsSeven(image, charStartX, charStartY, charWidth))
            {
                return "7";
            }

            if (digitReader.IsTwo(image, charStartX, charStartY, charWidth, charHeight))
            {
                return "2";
            }

            return "X";
        }

        private static void GetCharBoundsY(Bitmap image, out int charStartY, out int charHeight)
        {
            charStartY = 0;
            charHeight = 0;
            var lastWasWhite = false;

            for (var y = 0; y < image.Height; y++)
            {
                var rowHasWhite = false;
                for (var x = 0; x < image.Width; x++)
                {
                    var color = image.GetPixel(x, y);
                    if (color.ToArgb() == Color.White.ToArgb())
                    {
                        rowHasWhite = true;
                        break;
                    }
                }

                if (rowHasWhite)
                {
                    if (charStartY == 0)
                    {
                        charStartY = y;
                    }
                }
                else if (lastWasWhite)
                {
                    charHeight = y - charStartY;
                }
                lastWasWhite = rowHasWhite;
            }
        }

        public List<int> GetCharSeparators(Bitmap bmp)
        {
            var separators = new List<int>();
            var lastWasWhite = false;

            var lastSeenWhite = 0;

            for (var x = 5; x < bmp.Width; x++)
            {
                var columnHasWhite = false;
                for (var y = 0; y < bmp.Height; y++)
                {
                    var color = bmp.GetPixel(x, y);
                    if (color.ToArgb() == Color.White.ToArgb())
                    {
                        columnHasWhite = true;
                        break;
                    }
                }

                if (columnHasWhite)
                {
                    lastSeenWhite = x;
                    if (!lastWasWhite)
                    {
                        separators.Add(x);
                    }
                }
                else if (lastWasWhite)
                {
                    separators.Add(x);
                }
                lastWasWhite = columnHasWhite;
            }

            //separators.Add(lastSeenWhite + 1);

            return separators;
        }

        private bool HasCharacter(Bitmap bmp, Rectangle rect)
        {
            var numWhite = 0;
            for (var x = 0; x < bmp.Width; x++)
            {
                for (var y = 0; y < bmp.Height; y++)
                {
                    var color = bmp.GetPixel(x, y);
                    if (color == Color.White)
                    {
                        numWhite++;
                    }
                }
            }

            return numWhite > rect.Width + rect.Height;
        }

        public Bitmap MakeBlackAndWhite(Bitmap bitmap)
        {
            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    var color = bitmap.GetPixel(x, y);
                    var total = color.R + color.G + color.B;
                    var totalDiff = Math.Abs(color.R - color.G) + Math.Abs(color.R - color.B) + Math.Abs(color.B - color.G);
                    if (total > 700 || (total > 680 && totalDiff < 10))
                    {
                        bitmap.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                    }
                }
            }

            return bitmap;
        }
    }
}
