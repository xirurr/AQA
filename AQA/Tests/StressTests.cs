using System;
using System.Linq;
using System.Text;
using NUnit.Allure.Core;
using NUnit.Framework;
using AQA.helpers;

namespace AQA
{
    [TestFixture]
    [AllureNUnit]
    public class StressTests
    {
        [Test]
        public void ShouldCalculateFromTreeFilesCorrectly()
        {
            var parameters = new StringBuilder();
            var correctMargins = new decimal();
            
            for (var i = 0; i < 3; i++)
            {
                var inputValue = new InputValue
                {
                    OpenPrice = 1,
                    ClosePrice = 2,
                    Volume = 3,
                    ContractSize = 4,
                    Leverage = 5,
                    Commission = 7,
                };
                correctMargins = (inputValue.Volume * inputValue.ContractSize / inputValue.Leverage);
                var tmpFilePath = InputValueSaver.SaveToTmpFile(inputValue);

                parameters.Append(tmpFilePath + " ");
            }
            

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess("-f "+parameters));

            Assert.That(response.FirstOrDefault(o =>
                Math.Abs(o.Margin-correctMargins)>0.0001M), Is.Null,"Error in group calculation (3)");
        }

        
        [Test]
        public void ShouldCalculateFromHundredFilesCorrectly()
        {
            var parameters = new StringBuilder();
            var correctMargins = new decimal();
            
            for (var i = 0; i < 100; i++)
            {
                var inputValue = new InputValue
                {
                    OpenPrice = 1,
                    ClosePrice = 2,
                    Volume = 3,
                    ContractSize = 4,
                    Leverage = 5,
                    Commission = 7,
                };
                correctMargins = (inputValue.Volume * inputValue.ContractSize / inputValue.Leverage);
                var tmpFilePath = InputValueSaver.SaveToTmpFile(inputValue);

                parameters.Append(tmpFilePath + " ");
            }
            

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess("-f "+parameters));

            Assert.That(response.FirstOrDefault(o =>
                Math.Abs(o.Margin-correctMargins)>0.0001M), Is.Null,"Error in group calculation (3)");
        }
     }
}