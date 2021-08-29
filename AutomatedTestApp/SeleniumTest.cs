using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Configuration;
using System;

namespace AutomatedTestApp
{
    [TestFixture]
    public class SeleniumTest
    {
        static void Main(string[] args)
        {

        }

        [Test]
        public void Addtocart()
        {
            IWebDriver driver = new ChromeDriver();

            string url = ConfigurationManager.AppSettings["SiteUrl"];

            driver.Navigate().GoToUrl("https://www.bunnings.com.au/");

            Thread.Sleep(1000);

            driver.Manage().Window.Maximize();

            Thread.Sleep(1000);

            IWebElement searchtextfield = driver.FindElement(By.XPath(".//*[@id='custom-css-outlined-input']"));
            searchtextfield.SendKeys("Paint");

            Thread.Sleep(1000);

            searchtextfield.SendKeys(Keys.Enter);

            Thread.Sleep(2000);

            IWebElement spanElement = driver.FindElement(By.XPath("//span[@data-locator='Store & Availability']"));
            spanElement.Click();

            IWebElement divElement = driver.FindElement(By.XPath("//div[@data-locator='Store & AvailabilityResult-Click & Collect']"));
            divElement.Click();

            Thread.Sleep(5000);

            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            IWebElement spanElement2 = driver.FindElement(By.XPath("//span[@data-locator='Store & Availability']"));
            spanElement2.Click(); // to close the filter drop-down

            Thread.Sleep(2000);

            IWebElement totalamount = driver.FindElement(By.XPath("//p[@data-locator='search-product-tile-price-0']"));
            string amount = totalamount.Text;

            ReadOnlyCollection<IWebElement> buttonList = driver.FindElements(By.XPath("//button[@class='MuiButtonBase-root MuiButton-root MuiButton-text Buttonstyle__StyledButton-sc-1af200m-0 Buttonstyle__PrimaryProductButton-sc-1af200m-1 IhEHZ YuKQb cta MuiButton-textSecondary']"));

            IWebElement btnAddtoCart = buttonList.FirstOrDefault();

            btnAddtoCart.Click();

            //WebDriverWait waitAddToCart = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            Thread.Sleep(5000);

            IWebElement confirmElement = driver.FindElement(By.XPath("//div[@class='ConfirmationOverlaystyle__ConfirmationOverlayWrapper-sc-18fdi7v-0 jleQhC']"));

            Thread.Sleep(2000);

            IWebElement linkCartElement = confirmElement.FindElement(By.XPath("//a[@class='Anchor__styledAnchor-sc-1gq32ow-0 eePBcM']"));

            // wanted to get href attribute value but somehow wasn't coming up here!
            // string url = linkCartElement.GetAttribute("href"); 

            driver.Navigate().GoToUrl(url + "cart");

            IWebElement divCartTotal = driver.FindElement(By.XPath("//div[@data-locator='tile_TotalPrice']"));
            IWebElement inputQty = driver.FindElement(By.XPath("//input[@class='quantityEdit']"));

            // verify amount & quantity on the cart page
            Assert.AreEqual(amount, divCartTotal.Text);
            Assert.AreEqual("1", inputQty.GetAttribute("value"));

            Thread.Sleep(2000);

            driver.Quit();
        }
    }
}