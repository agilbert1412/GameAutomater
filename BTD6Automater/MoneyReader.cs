using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace BTD6Automater
{
    public class MoneyReader
    {
        private const string FILE_NAME = "Test.jpg";
        
        private DigitDetector _digitReader;
        private double multiplierX;
        private double multiplierY;

        private static readonly Rectangle SMALL_SCREEN_RECTANGLE = new Rectangle(270, 15, 150, 40);
        private static readonly Rectangle BIG_SCREEN_RECTANGLE = new Rectangle(340, 25, 166, 40);

        public MoneyReader(int resolutionX, int resolutionY)
        {
            _digitReader = new DigitDetector(resolutionX, resolutionY);
            multiplierX = resolutionX / 1024.0;
            multiplierY = resolutionY / 768.0;
        }

        public int ReadMoney(int amountToLog = int.MaxValue)
        {
            var bmp = TakeMoneyPic();

            var amount = ReadAmountFromPicture(bmp);
            //File.Copy(FILE_NAME, $"Amount {amount} - {DateTime.Now.Ticks}.jpg");

            if (amount > amountToLog)
            {
                File.Copy(FILE_NAME, $"Amount {amount} - {DateTime.Now.Ticks}.jpg");
                //bmp.Save($"Amount {amount} - {DateTime.Now.Ticks}.jpg", ImageFormat.Jpeg);
            }

            return amount; // + 15000;
        }

        public Bitmap TakeMoneyPic(string amountInName = "")
        {
            var rect = BIG_SCREEN_RECTANGLE;
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

            var filename = amountInName == "" ? FILE_NAME : (amountInName + (multiplierX > 1.5 ? "_Big" : "") + ".jpg");

            bmp.Save(filename, ImageFormat.Jpeg);

            return bmp;
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
                GetCharBoundsY(image, charStartX, charEnd, out charStartY, out charHeight);

                text += AnalyzeChar(bmp, charStartX, charStartY, widthChar, charHeight);
            }

            return text;
        }

        private string AnalyzeChar(Bitmap image, int charStartX, int charStartY, int charWidth, int charHeight)
        {

            var middleX = charStartX + (charWidth / 2);
            var middleY = charStartY + (charHeight / 2);

            if (charHeight < 22)
            {
                return "";
            }

            if (_digitReader.IsOne(charWidth))
            {
                return "1";
            }

            if (_digitReader.IsZero(image, middleX, middleY))
            {
                return "0";
            }

            if (_digitReader.IsThree(image, charHeight, middleX, middleY))
            {
                return "3";
            }

            if (_digitReader.IsFive(image, charHeight, middleX, middleY))
            {
                return "5";
            }

            if (_digitReader.IsSix(image, charHeight, middleX, middleY))
            {
                return "6";
            }

            if (_digitReader.IsNine(image, charHeight, middleX, middleY))
            {
                return "9";
            }

            if (_digitReader.IsEight(image, charHeight, middleX, middleY))
            {
                return "8";
            }

            if (_digitReader.IsTwo(image, charStartX, charStartY, charWidth, charHeight))
            {
                return "2";
            }

            if (_digitReader.IsFour(image, charStartX, charStartY, charWidth, charHeight))
            {
                return "4";
            }

            if (_digitReader.IsSeven(image, charStartX, charStartY, charWidth))
            {
                return "7";
            }

            return "X";
        }

        private void GetCharBoundsY(Bitmap image, int charStartX, int charEndX, out int charStartY, out int charHeight)
        {
            charStartY = 0;
            charHeight = 0;
            var lastWasWhite = false;

            for (var y = 0; y < image.Height; y++)
            {
                var rowHasWhite = false;
                for (var x = charStartX; x < charEndX; x++)
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

            CutIfBoundsAreTooFar(ref charStartY, ref charHeight);
        }

        private void CutIfBoundsAreTooFar(ref int charStartY, ref int charHeight)
        {
            if (charStartY < 2)
            {
                charStartY = 2;
            }
            if (charHeight > 23 * multiplierY)
            {
                charHeight = (int)(23 * multiplierY);
            }
        }

        public List<int> GetCharSeparators(Bitmap bmp)
        {
            var separators = new List<int>();
            var lastWasWhite = false;

            var lastSeenWhite = 0;

            for (var x = 5; x < bmp.Width; x++)
            {
                if (lastSeenWhite > 0 && x - lastSeenWhite > 6)
                {
                    break;
                }

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
            var indexesToReCheck = new List<Point>();

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
                    else if (total < 660 || totalDiff > 12)
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        indexesToReCheck.Add(new Point(x, y));
                    }
                }
            }

            ReCheckInconclusivePixels(bitmap, indexesToReCheck);

            return bitmap;
        }

        private static void ReCheckInconclusivePixels(Bitmap bitmap, List<Point> indexesToReCheck)
        {
            while (indexesToReCheck.Any())
            {
                var index = indexesToReCheck.First();
                indexesToReCheck.RemoveAt(0);

                var numAdjacentWhites = 0;

                for (var i = -1; i < 2; i++)
                {
                    for (var j = -1; j < 2; j++)
                    {
                        if ((i == 0 && j == 0) || index.X + i < 0 || index.X + i >= bitmap.Width || index.Y + j < 0 || index.Y + j >= bitmap.Height)
                        {
                            continue;
                        }

                        var color = bitmap.GetPixel(index.X + i, index.Y + j);
                        if (color.R == 255 && color.G == 255 && color.B == 255)
                        {
                            numAdjacentWhites++;
                            if (i == 0 || j == 0)
                            {
                                numAdjacentWhites++;
                            }
                        }
                    }
                }

                if (numAdjacentWhites >= 8)
                {
                    bitmap.SetPixel(index.X, index.Y, Color.White);
                }
                else
                {
                    bitmap.SetPixel(index.X, index.Y, Color.Black);
                }
            }
        }
    }
}
