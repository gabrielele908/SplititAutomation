using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class SplititAutomation
{
    static async Task Main(string[] args)
    {
        string accessToken = await PerformUITestingAndGetToken();

        if (!string.IsNullOrEmpty(accessToken))
        {
            await PerformApiTesting(accessToken);
        }
        else
        {
            Console.WriteLine("Failed to retrieve access token.");
        }
        Console.ReadKey();
    }

    static async Task<string> PerformUITestingAndGetToken()
    {
        string accessToken = null;

        ChromeOptions options = new ChromeOptions();
        options.AddArgument("start-maximized");
        options.AddArgument("--auto-open-devtools-for-tabs");

        using (IWebDriver driver = new ChromeDriver(options))
        {
            By emailField = By.XPath("//input[@id='Username' and @placeholder='Email']");
            By passwordField = By.XPath("//input[@placeholder='Password' and @id='Password']");
            By loginButton = By.XPath("//button[text()='Login']");
            By newTransactionButton = By.XPath("//span[text()='New Transaction']");
            By cookieBanner = By.XPath("//button[text()='Accept all']");

            try
            {
                driver.Navigate().GoToUrl("https://pos.sandbox.splitit.com/");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                try
                {
                    var banner = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(cookieBanner));
                    if (banner.Displayed)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", banner);
                        Console.WriteLine("Cookie banner closed.");
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("No cookie consent banner detected.");
                }

                wait.Until(driver =>
                {
                    var element = driver.FindElement(newTransactionButton);
                    return element.Displayed && element.Enabled;
                });

                IWebElement newTransactionElement = driver.FindElement(newTransactionButton);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", newTransactionElement);

                driver.FindElement(emailField).SendKeys("qa@splitit.com");
                driver.FindElement(passwordField).SendKeys("A1qazxsw23434!");

                IWebElement loginElement = driver.FindElement(loginButton);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", loginElement);

                await Task.Delay(10000);

                accessToken = (string)((IJavaScriptExecutor)driver).ExecuteScript(@"
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

                if (string.IsNullOrEmpty(accessToken))
                {
                    Console.WriteLine("Access token could not be automatically retrieved.");
                }
                else
                {
                    Console.WriteLine("Access token retrieved successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during UI testing: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }

        return accessToken;
    }

    static async Task PerformApiTesting(string accessToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.BaseAddress = new Uri("https://pos.sandbox.splitit.com/"); // Set the base address for relative URIs

            for (int i = 0; i < 20; i++)
            {
                var email = $"test_email_{i}@example.com";
                var amount = 100 + i; // Change amount for each run

                var queryParameters = new Dictionary<string, string>
                {
                    { "MerchantId", "Test Merchant III" }, 
                    { "Amount", amount.ToString() },
                    { "Currency", "GBP" },
                    { "OrderId", "Your Name" },
                    { "NumberOfInstallments", "6" },
                    { "CardNumber", "4111-1111-1111-1111" },
                    { "ExpirationDate", "02/25" },
                    { "Email", email },
                    { "Phone", "1234567890" },
                    { "FirstName", "Test" },
                    { "LastName", "User" }
                };

                var queryString = string.Join("&", queryParameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
                var requestUri = $"main/new-transaction?{queryString}";
                var response = await client.GetAsync(requestUri);
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Test Run {i + 1}: Response - {responseString}");
            }

            // Part Two: Change Card Number and Expiration Date
            var finalQueryParameters = new Dictionary<string, string>
            {
                { "MerchantId", "Test Merchant III" },
                { "Amount", "100" },
                { "Currency", "GBP" },
                { "OrderId", "Your Name" },
                { "NumberOfInstallments", "6" },
                { "CardNumber", "4222-2222-2222-2220" },
                { "ExpirationDate", "10/25" },
                { "Email", "final_test@example.com" },
                { "Phone", "1234567890" },
                { "FirstName", "Final" },
                { "LastName", "Test" }
            };

            var finalQueryString = string.Join("&", finalQueryParameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
            var finalRequestUri = $"main/new-transaction?{finalQueryString}";

            var finalResponse = await client.GetAsync(finalRequestUri);
            var finalResponseString = await finalResponse.Content.ReadAsStringAsync();

            Console.WriteLine($"Final Test: Response - {finalResponseString}");
        }
    }
}
