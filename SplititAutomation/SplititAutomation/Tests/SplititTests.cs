/*using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SplititAutomation.Pages;
using SplititAutomation.Utils;

namespace SplititAutomation.Tests
{
    public class SplititTest
    {
        public async Task RunTests()
        {
            IWebDriver driver = Utils.WebDriverManager.InitializeDriver();

            try
            {
                driver.Navigate().GoToUrl("https://pos.sandbox.splitit.com/");

                // Login and get the access token
                var loginPage = new Pages.LoginPage(driver);
                var dashboardPage = loginPage.Login("qa@splitit.com", "A1qazxsw23434!");

                dashboardPage.ClickNewTransaction();
                await Task.Delay(10000);

                string accessToken = dashboardPage.GetAccessToken();
                if (!string.IsNullOrEmpty(accessToken))
                {
                    Console.WriteLine("Access token retrieved successfully.");

                    var apiClient = new Utils.ApiClient("https://pos.sandbox.splitit.com/", accessToken);

                    for (int i = 0; i < 20; i++)
                    {
                        var email = $"test_email_{i}@example.com";
                        var amount = 100 + i;

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

                        var responseString = await apiClient.SendTransactionRequest(queryParameters);
                        Console.WriteLine($"Test Run {i + 1}: Response - {responseString}");
                    }

                    // Final Test Case
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

                    var finalResponseString = await apiClient.SendTransactionRequest(finalQueryParameters);
                    Console.WriteLine($"Final Test: Response - {finalResponseString}");
                }
                else
                {
                    Console.WriteLine("Failed to retrieve access token.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}*/
