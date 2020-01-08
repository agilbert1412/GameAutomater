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
            reader = new MoneyReader();
        }

        public static IEnumerable<string> GetAmountsWithPictures()
        {
            var files = Directory.EnumerateFiles(@"..\..\TestCases\", "*.jpg");
            foreach (var file in files)
            {
                Debug.WriteLine(file);
                yield return file.Split('\\').Last().Split('.')[0];
            }
        }

        [TestMethod]
        public void ReadTextFromAllPictureTest()
        {
            foreach (var file in GetAmountsWithPictures())
            {
                ExecuteTestReadTextFromPicture(file);
            }
        }

        private void ExecuteTestReadTextFromPicture(string expectedText)
        {
            // Arrange
            var fileName = expectedText + ".jpg";
            var image = GetBitmapFromFile(fileName);

            // Act
            var text = reader.ReadTextFromPicture(image);

            // Assert
            Assert.AreEqual(expectedText, text);
        }

        private void ExecuteTestReadNumCharsFromPicture(string expectedText)
        {
            // Arrange
            var fileName = expectedText + ".jpg";
            var image = GetBitmapFromFile(fileName);

            // Act
            var separators = reader.GetCharSeparators(reader.MakeBlackAndWhite(image));

            // Assert
            Assert.AreEqual(expectedText.Count(), separators.Count/2);
        }

        private Bitmap GetBitmapFromFile(string file)
        {
            return (Bitmap) Bitmap.FromFile(@"..\..\TestCases\" + file);
        }
        
        #region ReadTextTests

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

        #endregion ReadTextTests

        #region ReadNumCharsTests

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

        #endregion ReadNumCharTests

    }
}
