using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TheInternetTests
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver driver;
        private string baseUrl = "https://the-internet.herokuapp.com/";

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(baseUrl + "login");
        }

        [Test]
        public void SuccessfulLoginTest()
        {
            // Проверяет успешный вход с правильными данными
            driver.FindElement(By.Id("username")).SendKeys("tomsmith");
            driver.FindElement(By.Id("password")).SendKeys("SuperSecretPassword!");
            driver.FindElement(By.CssSelector("button.radius")).Click();
            var successMessage = driver.FindElement(By.Id("flash"));
            Assert.That(successMessage.Text.Contains("You logged into a secure area!"));
        }

        [Test]
        public void InvalidLoginTest()
        {
            // Проверяет ошибку при входе с неправильными данными
            driver.FindElement(By.Id("username")).SendKeys("wrongUser");
            driver.FindElement(By.Id("password")).SendKeys("wrongPassword!");
            driver.FindElement(By.CssSelector("button.radius")).Click();
            var errorMessage = driver.FindElement(By.Id("flash"));
            Assert.That(errorMessage.Text.Contains("Your username is invalid!"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
