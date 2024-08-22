/*using OpenQA.Selenium;

namespace SplititAutomation.Pages
{
    public class DashboardPage
    {
        private IWebDriver _driver;

        private By newTransactionButton = By.XPath("//span[text()='New Transaction']");

        public DashboardPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickNewTransaction()
        {
            IWebElement newTransactionElement = _driver.FindElement(newTransactionButton);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", newTransactionElement);
        }

        public string GetAccessToken()
        {
            return (string)((IJavaScriptExecutor)_driver).ExecuteScript(@"
                var token = null;
                if (window.localStorage) {
                    token = window.localStorage.getItem('access_token');
                }
                if (!token && document.cookie) {
                    var cookies = document.cookie.split('; ');
                    for (var i = 0; i < cookies.length; i++) {
                        var cookie = cookies[i].split('=');
                        if (cookie[0] === 'access_token') {
                            token = cookie[1];
                            break;
                        }
                    }
                }
                return token;");
        }
    }
}*/
