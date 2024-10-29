using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

[TestFixture]
public class RegistrationTests
{
    private IWebDriver driver;
    private string baseUrl = "https://example-registration.com/";

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Navigate().GoToUrl(baseUrl + "register");
    }

    [Test]
    public void SuccessfulRegistrationTest()
    {
        // Проверяет успешную регистрацию новой учетной записи
        driver.FindElement(By.Id("username")).SendKeys("newUser");
        driver.FindElement(By.Id("password")).SendKeys("newPassword");
        driver.FindElement(By.Id("confirmPassword")).SendKeys("newPassword");
        driver.FindElement(By.Id("registerButton")).Click();
        var successMessage = driver.FindElement(By.Id("flash"));
        Assert.That(successMessage.Text.Contains("Registration successful!"));
    }

    [Test]
    public void DuplicateRegistrationTest()
    {
        // Проверяет сообщение об ошибке при попытке регистрации с существующим логином
        driver.FindElement(By.Id("username")).SendKeys("existingUser");
        driver.FindElement(By.Id("password")).SendKeys("password");
        driver.FindElement(By.Id("confirmPassword")).SendKeys("password");
        driver.FindElement(By.Id("registerButton")).Click();
        var errorMessage = driver.FindElement(By.Id("flash"));
        Assert.That(errorMessage.Text.Contains("Username already exists"));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
