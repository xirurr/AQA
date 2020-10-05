using System;
using System.Linq;
using System.Text;
using AQA.helpers;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AQA
{
    [TestFixture]
    [AllureNUnit]
    public class AboveZeroInputsTests
    {
        [Test]
        public void ShouldRiseErrorIfOpenPriceIsZero()
        {
            var parameters = "--open 0 --close 1.2 --volume 1.5 --contract-size 100000 --leverage 10";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 1, "wrong errors count");
            Assert.That(response[0].Errors[0].Contains("Open price must be greater than 0"),
                "Wrong error: " + response[0].Errors[0]);
        }


        [Test]
        public void ShouldRiseErrorIfOpenPriceBelowZero()
        {
            var parameters = "--open -1 --close 1.2 --volume 1.5 --contract-size 100000 --leverage 10";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 1, "wrong errors count");
            Assert.That(response[0].Errors[0].Contains("Open price must be greater than 0"),
                "Wrong error: " + response[0].Errors[0]);
        }

        [Test]
        public void ShouldRiseErrorIfContractSizeIsZero()
        {
            var parameters = "--open 1 --close 2 --volume 1.5 --contract-size 0 --leverage 10";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 1, "wrong errors count");
            Assert.That(response[0].Errors[0].Contains("Contract size must be greater than 0"),
                "Wrong error: " + response[0].Errors[0]);
        }

        [Test]
        public void ShouldRiseErrorIfContractSizeBelowZero()
        {
            var parameters = "--open 1 --close 1.2 --volume 1.5 --contract-size -100000 --leverage 10";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 1, "wrong errors count");
            Assert.That(response[0].Errors[0].Contains("Contract size must be greater than 0"),
                "Wrong error: " + response[0].Errors[0]);
        }

        [Test]
        public void ShouldRiseErrorIfClosePriceIsZero()
        {
            var parameters = "--open 1 --close 0 --volume 1.5 --contract-size 100000 --leverage 10";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 1, "wrong errors count");
            Assert.That(response[0].Errors[0].Contains("Close price must be greater than 0"),
                "Wrong error: " + response[0].Errors[0]);
        }

        [Test]
        public void ShouldRiseErrorIfClosePriceBelowZero()
        {
            var parameters = "--open 1.2 --close -1.2 --volume 1.5 --contract-size 100000 --leverage 10";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 1, "wrong errors count");
            Assert.That(response[0].Errors[0].Contains("Close price must be greater than 0"),
                "Wrong error: " + response[0].Errors[0]);
        }

        [Test]
        public void ShouldRiseErrorIfLeverageIsZero()
        {
            var parameters = "--open 1 --close 2 --volume 1.5 --contract-size 100000 --leverage 0";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 1, "wrong errors count");
            Assert.That(response[0].Errors[0].Contains("Leverage must be greater than 0"),
                "Wrong error: " + response[0].Errors[0]);
        }

        [Test]
        public void ShouldRiseErrorIfLeverageBelowZero()
        {
            var parameters = "--open 1.2 --close 2 --volume 1.5 --contract-size 100000 --leverage -2";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 1, "wrong errors count");
            Assert.That(response[0].Errors[0].Contains("Leverage must be greater than 0"),
                "Wrong error: " + response[0].Errors[0]);
        }

        [Test]
        public void ShouldRiseErrorIfCommissionBelowZero()
        {
            var parameters = "--open 1 --close 2 --volume 1.5 --contract-size 100000 --leverage 10 --commission -1";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 1, "wrong errors count");
            Assert.That(response[0].Errors[0].Contains("Commission must be equal or greater than 0"), "Wrong error :" +
                response[0].Errors[0]);
        }
        
        [Test]
        public void ShouldRiseErrorIfMultipleMandatoryValuesIsZero()
        {
            var parameters = "--open 0 --close 0 --volume 1.5 --contract-size 0 --leverage 0";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 4, "wrong errors count");
            Assert.That(response[0].Errors.Any(o => o.Contains("Leverage must be greater than 0")),
                "Wrong error: " + response[0].Errors[0]);
            Assert.That(response[0].Errors.Any(o => o.Contains("Close price must be greater than 0")),
                "Wrong error: " + response[0].Errors[0]);
            Assert.That(response[0].Errors.Any(o => o.Contains("Open price must be greater than 0")),
                "Wrong error: " + response[0].Errors[0]);
            Assert.That(response[0].Errors.Any(o => o.Contains("Contract size must be greater than 0")),
                "Wrong error: " + response[0].Errors[0]);
        }

        [Test]
        public void ShouldRiseErrorIfMultipleMandatoryValuesBelowZero()
        {
            var parameters = "--open -1 --close -2 --volume 1.5 --contract-size -3 --leverage -4";

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess(parameters));

            Assert.That(response[0].Errors.Count == 4, "wrong errors count");
            Assert.That(response[0].Errors.Any(o => o.Contains("Leverage must be greater than 0")), "Wrong error");
            Assert.That(response[0].Errors.Any(o => o.Contains("Close price must be greater than 0")), "Wrong error");
            Assert.That(response[0].Errors.Any(o => o.Contains("Open price must be greater than 0")), "Wrong error");
            Assert.That(response[0].Errors.Any(o => o.Contains("Contract size must be greater than 0")), "Wrong error");
        }
        
        [Test]
        public void ShouldCalculateMarginCorrectlyAndRiseErrorForZeroInput()
        {
            var parameters = new StringBuilder();

            var correctValue = new InputValue
            {
                OpenPrice = 1,
                ClosePrice = 2,
                Volume = 3,
                ContractSize = 4,
                Leverage = 5,
                Commission = 7,
            };
            var correctMargin = correctValue.Volume * correctValue.ContractSize / correctValue.Leverage;
            var tmpFilePath = InputValueSaver.SaveToTmpFile(correctValue);
            parameters.Append(tmpFilePath + " ");
            
            var incorrectValue = new InputValue
            {
                OpenPrice = 0,
                ClosePrice = 9,
                Volume = 9,
                ContractSize = 9,
                Leverage = 9,
                Commission = 9,
            };
            
            tmpFilePath = InputValueSaver.SaveToTmpFile(incorrectValue);
            parameters.Append(tmpFilePath + " ");

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess("-f " + parameters));

            Assert.That(response.Count(o => o.Errors.Count == 1), Is.EqualTo(1));
            Assert.That(Math.Abs(response.FirstOrDefault(o=>o.Errors.Count==0).Margin-correctMargin),
                Is.LessThan(0.0001M),"Wrong margin calculation");
        }
    }
}