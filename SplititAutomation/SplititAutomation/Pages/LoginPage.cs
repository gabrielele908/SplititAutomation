/*using OpenQA.Selenium;

namespace SplititAutomation.Pages
{
    public class LoginPage
    {
        private IWebDriver _driver;

        private By emailField = By.XPath("//input[@id='Username' and @placeholder='Email']");
        private By passwordField = By.XPath("//input[@placeholder='Password' and @id='Password']");
        private By loginButton = By.XPath("//button[text()='Login']");
        private By cookieBanner = By.XPath("//button[text()='Accept all']");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AcceptCookiesIfPresent()
        {
            try
            {
                var banner = _driver.FindElement(cookieBanner);
                if (banner.Displayed)
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", banner);
                }
            }
            catch (NoSuchElementException)
            {
                // Cookie banner not present, continue
            }
        }

        public void EnterCredentials(string email, string password)
        {
            _driver.FindElement(emailField).SendKeys(email);
            _driver.FindElement(passwordField).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            IWebElement loginElement = _driver.FindElement(loginButton);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", loginElement);
        }

        public DashboardPage Login(string email, string password)
        {
            AcceptCookiesIfPresent();
            EnterCredentials(email, password);
            ClickLoginButton();
            return new DashboardPage(_driver);
        }
    }
}*/
