using System;
using NUnit.Allure.Core;
using NUnit.Framework;
using AQA.helpers;

namespace AQA
{
    [TestFixture]
    [AllureNUnit]
    public class CalculationTests
    {
        [Test]
        public void ShouldCalculateProfitCorrectly()
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
            var correctProfit = (inputValue.ClosePrice - inputValue.OpenPrice) * inputValue.Volume *
                                inputValue.ContractSize;
            var tmpFilePath = InputValueSaver.SaveToTmpFile(inputValue);

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess("-f "+tmpFilePath));

            Assert.That(Math.Abs(response[0].Profit-correctProfit), Is.LessThan(0.0001M),"Wrong profit calculation");
        }
        
        [Test]
        public void ShouldCalculateMarginCorrectly()
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
            var correctMargin = inputValue.Volume * inputValue.ContractSize / inputValue.Leverage;
            var tmpFilePath = InputValueSaver.SaveToTmpFile(inputValue);

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess("-f "+tmpFilePath));

            Assert.That(Math.Abs(response[0].Margin-correctMargin), Is.LessThan(0.0001M),"Wrong margin calculation");
        }
 
        [Test]
        public void ShouldCalculatePerLotCommissionCorrectly()
        {
            var inputValue = new InputValue
            {
                OpenPrice = 1,
                ClosePrice = 2,
                Volume = 3,
                ContractSize = 4,
                Leverage = 5,
                Commission = 7,
                CommissionType = "PerLot"
            };
            var correctCommission = inputValue.Commission * inputValue.Volume;
            var tmpFilePath = InputValueSaver.SaveToTmpFile(inputValue);

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess("-f "+tmpFilePath));

            Assert.That(Math.Abs(response[0].Commission-correctCommission), Is.LessThan(0.0001M),"Wrong Commission (PerLot) calculation");
        }
        
        [Test]
        public void ShouldCalculatePerTradeCommissionCorrectly()
        {
            var inputValue = new InputValue
            {
                OpenPrice = 1,
                ClosePrice = 2,
                Volume = 3,
                ContractSize = 4,
                Leverage = 5,
                Commission = 7,
                CommissionType = "PerTrade"
            };
            var correctCommission = inputValue.Commission * 1;
            var tmpFilePath = InputValueSaver.SaveToTmpFile(inputValue);

            var response = ResponseParser.Parse(new FileStarter().LaunchProcess("-f "+tmpFilePath));

            Assert.That(Math.Abs(response[0].Commission-correctCommission), Is.LessThan(0.0001M),"Wrong Commission (PerTrade) calculation");
        }
        

    }
}