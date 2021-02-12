using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Pharmacy;
using Pharmacy.Controllers;
using Pharmacy.Models.DTO;
using Pharmacy.Models.Entities;
using Pharmacy.Areas.Identity.Pages.Account;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Models.Entities.Users;
using System.Web.Http.Results;
using Pharmacy.Data;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestPharmacy.IntegrationTests
{
    class IntegrationTest11
    {
        public SupplyOrdersController supplyOrdersController;
        public DrugsController drugsController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;
        private IWebDriver _webDriver;
        private WebDriverWait _wait;
        private int _timeoutInSeconds = 30;
        private static string _driverPath = Environment.CurrentDirectory + "\\..\\..\\..\\WebDriverGoogleChome\\";

        [SetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver(_driverPath);
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_timeoutInSeconds));
        }

        #region Integration  Test

        public bool LogInEmployee(IWebDriver webDriver, WebDriverWait wait, LoginDTO loginDTO)
        {
            bool result = false;
            try
            {
                webDriver.Navigate().GoToUrl("https://localhost:44396/Identity/Account/Login");
                webDriver.Manage().Window.Maximize();
                IWebElement webElement;

                // Username
                wait.Until(ExpectedConditions.ElementIsVisible (By.Id("username_input")));
                webElement = webDriver.FindElement(By.Id("username_input"));
                webElement.Clear();
                webDriver.FindElement(By.Id("username_input")).SendKeys(loginDTO.username);

                // password
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("password_input")));
                webElement = webDriver.FindElement(By.Id("password_input"));
                webElement.Clear();
                webDriver.FindElement(By.Id("password_input")).SendKeys(loginDTO.password);

                webDriver.FindElement(By.Id("submit_login")).Click();

                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("daq")));
                webElement = webDriver.FindElement(By.Id("daq"));
                webElement.Click();

                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("crid")));
                webElement = webDriver.FindElement(By.Id("crid"));
                webElement.Click();



                var uls = webDriver.FindElements(By.TagName("select"));

                int elemInt = 0;

                foreach(var ul in uls)
                {
                    if(elemInt > uls.Count()-3)
                    {
                        ul.FindElement(By.TagName("option")).Click();
                    }
                    elemInt++;
                }

                wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("Quantity")));
                webElement = webDriver.FindElement(By.Name("Quantity"));
                webElement.Clear();
                webDriver.FindElement(By.Name("Quantity")).SendKeys("11");

                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("inmp")));
                webElement = webDriver.FindElement(By.Id("inmp"));
                webElement.Click();

                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("daq")));
                webElement = webDriver.FindElement(By.Id("daq"));

                if(webElement == null)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Source + " - " + ex.Message + " - " + ex.StackTrace);
                return result;
            }
            return result;
        }

        [Test]
        public async Task LoginOutTest_Valid()
        {
            LoginDTO loginDTO = new LoginDTO("hafik45066@hrandod.com", "Admin.123");
            var results = LogInEmployee(_webDriver, _wait, loginDTO);
            Assert.IsTrue(results);
        }
        #endregion
    }
}
