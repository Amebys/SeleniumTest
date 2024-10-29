using OpenQA.Selenium.Edge;
using OpenQA.Selenium;

[TestFixture]
public class SearchTests
{
    private IWebDriver driver;
    private string baseUrl = "https://example-search.com/";

    [SetUp]
    public void SetUp()
    {
        driver = new EdgeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Navigate().GoToUrl(baseUrl + "search");
    }

    [Test]
    public void SearchWithResultsTest()
    {
        // Проверяет поиск, когда есть результаты
        driver.FindElement(By.Id("searchBox")).SendKeys("test");
        driver.FindElement(By.Id("searchButton")).Click();
        var results = driver.FindElements(By.ClassName("searchResult"));
        Assert.That(results.Count > 0, "Expected search results, but found none.");
    }

    [Test]
    public void SearchWithoutResultsTest()
    {
        // Проверяет сообщение при отсутствии результатов
        driver.FindElement(By.Id("searchBox")).SendKeys("nonexistent keyword");
        driver.FindElement(By.Id("searchButton")).Click();
        var noResultsMessage = driver.FindElement(By.Id("noResultsMessage"));
        Assert.That(noResultsMessage.Displayed);
        Assert.That(noResultsMessage.Text, Is.EqualTo("No results found"));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
