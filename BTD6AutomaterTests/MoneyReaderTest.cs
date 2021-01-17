using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using BTD6Automater;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System;

namespace BTD6AutomaterTests
{
    [TestClass]
    public class MoneyReaderTest
    {
        MoneyReader reader;

        [TestInitialize]
        public void Init()
        {
            
        }

        public static IEnumerable<string> GetAmountsWithPictures()
        {
            var files = Directory.EnumerateFiles(@"..\..\TestCases\Test\", "*.jpg");
            foreach (var file in files)
            {
                yield return file;
            }
        }

        [TestMethod]
        public void ReadTextFromAllPictureTest()
        {
            /*foreach (var file in GetAmountsWithPictures())
            {
                var image = (Bitmap)Bitmap.FromFile(file);

                // Act
                var text = reader.ReadTextFromPicture(image);

                // Assert
                Debug.WriteLine($"File: {file} | Result: {text}");
            }*/
        }

        private void ExecuteTestReadTextFromPicture(string expectedText)
        {
            // Arrange
            var fileName = expectedText + ".jpg";
            reader = new MoneyReader(1024, 768);

            var amount = expectedText;
            if (amount.Contains("_"))
            {
                amount = amount.Split('_')[0];
                reader = new MoneyReader(1920, 1080);
            }
            var image = GetBitmapFromFile(fileName);

            // Act
            var text = reader.ReadTextFromPicture(image);

            // Assert
            Assert.AreEqual(amount, text);
        }

        private void ExecuteTestReadNumCharsFromPicture(string expectedText)
        {
            // Arrange
            var fileName = expectedText + ".jpg";
            var image = GetBitmapFromFile(fileName);
            reader = new MoneyReader(1024, 768);

            var amount = expectedText;
            if (amount.Contains("_"))
            {
                amount = amount.Split('_')[0];
                reader = new MoneyReader(1920, 1080);
            }

            // Act
            var separators = reader.GetCharSeparators(reader.MakeBlackAndWhite(image));

            // Assert
            Assert.AreEqual(amount.Count(), separators.Count/2);
        }

        private Bitmap GetBitmapFromFile(string file)
        {
            return (Bitmap) Bitmap.FromFile(@"..\..\TestCases\" + file);
        }
        
        #region ReadTextTests SmallScreen
        /*
        [TestMethod]
        public void ReadTextFrom851PictureTest()
        {
            ExecuteTestReadTextFromPicture("851");
        }
        
        [TestMethod]
        public void ReadTextFrom854PictureTest()
        {
            ExecuteTestReadTextFromPicture("854");
        }
        
        [TestMethod]
        public void ReadTextFrom857PictureTest()
        {
            ExecuteTestReadTextFromPicture("857");
        }
        
        [TestMethod]
        public void ReadTextFrom860PictureTest()
        {
            ExecuteTestReadTextFromPicture("860");
        }
        
        [TestMethod]
        public void ReadTextFrom863PictureTest()
        {
            ExecuteTestReadTextFromPicture("863");
        }
        
        [TestMethod]
        public void ReadTextFrom866PictureTest()
        {
            ExecuteTestReadTextFromPicture("866");
        }
        
        [TestMethod]
        public void ReadTextFrom869PictureTest()
        {
            ExecuteTestReadTextFromPicture("869");
        }
        
        [TestMethod]
        public void ReadTextFrom971PictureTest()
        {
            ExecuteTestReadTextFromPicture("971");
        }
        
        [TestMethod]
        public void ReadTextFrom977PictureTest()
        {
            ExecuteTestReadTextFromPicture("977");
        }
        
        [TestMethod]
        public void ReadTextFrom980PictureTest()
        {
            ExecuteTestReadTextFromPicture("980");
        }
        
        [TestMethod]
        public void ReadTextFrom986PictureTest()
        {
            ExecuteTestReadTextFromPicture("986");
        }
        
        [TestMethod]
        public void ReadTextFrom992PictureTest()
        {
            ExecuteTestReadTextFromPicture("992");
        }
        
        [TestMethod]
        public void ReadTextFrom995PictureTest()
        {
            ExecuteTestReadTextFromPicture("995");
        }
        
        [TestMethod]
        public void ReadTextFrom1108PictureTest()
        {
            ExecuteTestReadTextFromPicture("1108");
        }
        
        [TestMethod]
        public void ReadTextFrom1109PictureTest()
        {
            ExecuteTestReadTextFromPicture("1109");
        }
        
        [TestMethod]
        public void ReadTextFrom1116PictureTest()
        {
            ExecuteTestReadTextFromPicture("1116");
        }
        
        [TestMethod]
        public void ReadTextFrom1128PictureTest()
        {
            ExecuteTestReadTextFromPicture("1128");
        }
        
        [TestMethod]
        public void ReadTextFrom1231PictureTest()
        {
            ExecuteTestReadTextFromPicture("1231");
        }
        
        [TestMethod]
        public void ReadTextFrom1235PictureTest()
        {
            ExecuteTestReadTextFromPicture("1235");
        }
        
        [TestMethod]
        public void ReadTextFrom1240PictureTest()
        {
            ExecuteTestReadTextFromPicture("1240");
        }
        
        [TestMethod]
        public void ReadTextFrom1251PictureTest()
        {
            ExecuteTestReadTextFromPicture("1251");
        }
        
        [TestMethod]
        public void ReadTextFrom1355PictureTest()
        {
            ExecuteTestReadTextFromPicture("1355");
        }

        [TestMethod]
        public void ReadTextFrom855PictureTest()
        {
            ExecuteTestReadTextFromPicture("855");
        }

        [TestMethod]
        public void ReadTextFrom78PictureTest()
        {
            ExecuteTestReadTextFromPicture("78");
        }

        [TestMethod]
        public void ReadTextFrom338PictureTest()
        {
            ExecuteTestReadTextFromPicture("338");
        }

        [TestMethod]
        public void ReadTextFrom382PictureTest()
        {
            ExecuteTestReadTextFromPicture("382");
        }

        [TestMethod]
        public void ReadTextFrom424PictureTest()
        {
            ExecuteTestReadTextFromPicture("424");
        }

        [TestMethod]
        public void ReadTextFrom426PictureTest()
        {
            ExecuteTestReadTextFromPicture("426");
        }

        [TestMethod]
        public void ReadTextFrom1275PictureTest()
        {
            ExecuteTestReadTextFromPicture("1275");
        }

        [TestMethod]
        public void ReadTextFrom404PictureTest()
        {
            ExecuteTestReadTextFromPicture("404");
        }

        [TestMethod]
        public void ReadTextFrom2478PictureTest()
        {
            ExecuteTestReadTextFromPicture("2478");
        }

        [TestMethod]
        public void ReadTextFrom2899PictureTest()
        {
            ExecuteTestReadTextFromPicture("2899");
        }

        [TestMethod]
        public void ReadTextFrom2283PictureTest()
        {
            ExecuteTestReadTextFromPicture("2283");
        }

        [TestMethod]
        public void ReadTextFrom1396PictureTest()
        {
            ExecuteTestReadTextFromPicture("1396");
        }
        */
        #endregion ReadTextTests SmallScreen
        
        #region ReadTextTests BigScreen

        [TestMethod]
        public void ReadTextFrom851BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("851_Big");
        }

        [TestMethod]
        public void ReadTextFrom854BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("854_Big");
        }

        [TestMethod]
        public void ReadTextFrom857BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("857_Big");
        }

        [TestMethod]
        public void ReadTextFrom860BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("860_Big");
        }

        [TestMethod]
        public void ReadTextFrom863BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("863_Big");
        }

        [TestMethod]
        public void ReadTextFrom866BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("866_Big");
        }

        [TestMethod]
        public void ReadTextFrom869BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("869_Big");
        }

        [TestMethod]
        public void ReadTextFrom971BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("971_Big");
        }

        [TestMethod]
        public void ReadTextFrom977BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("977_Big");
        }

        [TestMethod]
        public void ReadTextFrom980BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("980_Big");
        }

        [TestMethod]
        public void ReadTextFrom986BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("986_Big");
        }

        [TestMethod]
        public void ReadTextFrom992BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("992_Big");
        }

        [TestMethod]
        public void ReadTextFrom995BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("995_Big");
        }

        [TestMethod]
        public void ReadTextFrom1108BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1108_Big");
        }

        [TestMethod]
        public void ReadTextFrom1109BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1109_Big");
        }

        [TestMethod]
        public void ReadTextFrom1116BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1116_Big");
        }

        [TestMethod]
        public void ReadTextFrom1128BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1128_Big");
        }

        [TestMethod]
        public void ReadTextFrom1231BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1231_Big");
        }

        [TestMethod]
        public void ReadTextFrom1235BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1235_Big");
        }

        [TestMethod]
        public void ReadTextFrom1240BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1240_Big");
        }

        [TestMethod]
        public void ReadTextFrom1251BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1251_Big");
        }

        /*[TestMethod]
        public void ReadTextFrom1355BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1355_Big");
        }

        [TestMethod]
        public void ReadTextFrom855BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("855_Big");
        }*/

        /*[TestMethod]
        public void ReadTextFrom78BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("78_Big");
        }*/

        [TestMethod]
        public void ReadTextFrom338BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("338_Big");
        }

        /*[TestMethod]
        public void ReadTextFrom382BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("382_Big");
        }*/

        /*[TestMethod]
        public void ReadTextFrom424BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("424_Big");
        }*/

        /*[TestMethod]
        public void ReadTextFrom426BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("426_Big");
        }*/

        /*[TestMethod]
        public void ReadTextFrom1275BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1275_Big");
        }

        [TestMethod]
        public void ReadTextFrom404BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("404_Big");
        }

        [TestMethod]
        public void ReadTextFrom2478BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("2478_Big");
        }

        [TestMethod]
        public void ReadTextFrom2899BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("2899_Big");
        }

        [TestMethod]
        public void ReadTextFrom2283BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("2283_Big");
        }

        [TestMethod]
        public void ReadTextFrom1396BigPictureTest()
        {
            ExecuteTestReadTextFromPicture("1396_Big");
        }*/

        #endregion ReadTextTests BigScreen

        #region ReadNumCharsTests SmallScreen
        /*
        [TestMethod]
        public void ReadNumCharsFrom851PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("851");
        }

        [TestMethod]
        public void ReadNumCharsFrom854PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("854");
        }

        [TestMethod]
        public void ReadNumCharsFrom857PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("857");
        }

        [TestMethod]
        public void ReadNumCharsFrom860PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("860");
        }

        [TestMethod]
        public void ReadNumCharsFrom863PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("863");
        }

        [TestMethod]
        public void ReadNumCharsFrom866PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("866");
        }

        [TestMethod]
        public void ReadNumCharsFrom869PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("869");
        }

        [TestMethod]
        public void ReadNumCharsFrom971PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("971");
        }

        [TestMethod]
        public void ReadNumCharsFrom977PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("977");
        }

        [TestMethod]
        public void ReadNumCharsFrom980PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("980");
        }

        [TestMethod]
        public void ReadNumCharsFrom986PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("986");
        }

        [TestMethod]
        public void ReadNumCharsFrom992PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("992");
        }

        [TestMethod]
        public void ReadNumCharsFrom995PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("995");
        }

        [TestMethod]
        public void ReadNumCharsFrom1108PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1108");
        }

        [TestMethod]
        public void ReadNumCharsFrom1109PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1109");
        }

        [TestMethod]
        public void ReadNumCharsFrom1116PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1116");
        }

        [TestMethod]
        public void ReadNumCharsFrom1128PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1128");
        }

        [TestMethod]
        public void ReadNumCharsFrom1231PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1231");
        }

        [TestMethod]
        public void ReadNumCharsFrom1235PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1235");
        }

        [TestMethod]
        public void ReadNumCharsFrom1240PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1240");
        }

        [TestMethod]
        public void ReadNumCharsFrom1251PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1251");
        }

        [TestMethod]
        public void ReadNumCharsFrom1355PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1355");
        }

        [TestMethod]
        public void ReadNumCharsFrom855PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("855");
        }

        [TestMethod]
        public void ReadNumCharsFrom78PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("78");
        }

        [TestMethod]
        public void ReadNumCharsFrom338PictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("338");
        }
        */
        #endregion ReadNumCharTests SmallScreen

        #region ReadNumCharsTests SmallScreen

        [TestMethod]
        public void ReadNumCharsFrom851BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("851_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom854BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("854_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom857BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("857_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom860BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("860_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom863BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("863_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom866BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("866_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom869BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("869_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom971BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("971_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom977BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("977_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom980BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("980_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom986BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("986_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom992BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("992_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom995BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("995_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom1108BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1108_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom1109BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1109_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom1116BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1116_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom1128BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1128_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom1231BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1231_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom1235BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1235_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom1240BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1240_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom1251BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1251_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom1355BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("1355_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom855BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("855_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom78BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("78_Big");
        }

        [TestMethod]
        public void ReadNumCharsFrom338BigPictureTest()
        {
            ExecuteTestReadNumCharsFromPicture("338_Big");
        }

        #endregion ReadNumCharTests BigScreen

    }
}
