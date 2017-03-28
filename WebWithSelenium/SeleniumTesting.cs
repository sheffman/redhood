using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace WebWithSelenium
{
    [TestFixture]
    public class SeleniumTesting
    {
        [Test]
        public void SearchYandex()
        {
            //comment here
			IWebDriver driver = new ChromeDriver();
            driver.Url = "http://google.com.ua";
            var searchBox = driver.FindElement(By.Id("lst-ib"));
            searchBox.SendKeys("yandex");
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);
              var firstLink = driver.FindElement(By.Id("vs0p1c0"));
            firstLink.Click();

            //validate
            if (driver.FindElement(By.CssSelector(".logo.logo_color_ru")) == null) Assert.Fail();
            driver.Quit();
        }

        [Test]
        public void DoSmthing()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://google.com.ua";
            var searchBox = driver.FindElement(By.Id("lst-ib"));
            searchBox.SendKeys("wikipedia");
            searchBox.Submit();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            
           var enWikiLinkcontainer = wait.Until(gr =>
            {
                var links = driver.FindElements(By.ClassName("rc"));
                return (links.Count > 0)?  links[1] :  null;
            });
            var enWikiLink = enWikiLinkcontainer.FindElement(By.TagName("a"));
            enWikiLink.Click();

            //validate
            if (driver.FindElement(By.CssSelector(".central-textlogo > img:nth-child(1)")) == null) Assert.Fail();
            driver.Quit();

        }

    }
}
