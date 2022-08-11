using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using System.Threading;
using Xunit;
using OpenQA.Selenium.Support.UI;

namespace UITests
{
    [TechTalk.SpecFlow.Binding]
    public class WordleSpecflow : IDisposable
    {
        private int time = 8000;

        public IWebDriver _driver;
        //String path = AppDomain.CurrentDomain.BaseDirectory + @"\Drivers";
        public String baseURL = "https://wordleagiles.azurewebsites.net/";
        //public String baseURL = "https://localhost:44321/";
        public WordleSpecflow() => _driver = new ChromeDriver();

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [BeforeScenario]

        [Fact]
        [Given(@"I have entered default Errors and Difficulty")]
        public void GivenIHaveEnteredJuanAsName()
        {
            _driver.Navigate()
                .GoToUrl(baseURL);

            Thread.Sleep(time);



            _driver.FindElement(By.Id("Name"))
                .SendKeys("Juan");

            Thread.Sleep(1000);

            _driver.FindElement(By.Id("Play"))
                .Click();

            Thread.Sleep(1000);

            //var palabraIntentada = _driver.FindElement(By.Id("palabra-intentada"));

            //var bienvenida = _driver.FindElement(By.Id("nametag"));

            //Assert.Equal("Bienvenido Juan!", _driver.FindElement(By.Id("nametag")).GetAttribute("value"));
        }

        [When(@"I enter Juan as the Name")]
        public void WhenIEnterJuanAsTheName()
        {
            _driver.FindElement(By.Id("Name"))
                .SendKeys("Juan");

            Thread.Sleep(1000);

            _driver.FindElement(By.Id("Play"))
                .Click();
        }

        [Then(@"I should be told Bienvenido JUAN")]

        public void ThenIShouldBeToldBienvenidoJUAN()
        {
            //string bienvenida = _driver.FindElement(By.Id("nametag")).Text;
           // Assert.NotEqual("Bienvenido Juan!", _driver.FindElement(By.Id("nametag")));
        }

        [AfterScenario]




        [Fact]

        [Given(@"I have entered Juan as the name and default Errors and Difficulty")]
        [When(@"I enter Juan as the word to guess")]
        [Then(@"I should see the word and be told it is incorrect")]

        public void EnterWord()
        {

            _driver.Navigate()
                .GoToUrl(baseURL);

            Thread.Sleep(time);

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Juan");

            Thread.Sleep(1000);

            _driver.FindElement(By.Id("Play"))
                .Click();

            var palabraIntentada = _driver.FindElement(By.Id("palabra-intentada"));
            var btnIntentar = _driver.FindElement(By.Id("intentar-button"));

            string palabra = "JUAN";

            Thread.Sleep(time);

            palabraIntentada.SendKeys(palabra);
            Thread.Sleep(500);
            btnIntentar.Click();

            Thread.Sleep(1000);

            for (int i = 0; i < palabra.Length; i++)
            {
                string letra = _driver.FindElement(By.Id("0" + i)).GetAttribute("innerHTML");
                Assert.Equal(letra, Char.ToString(palabra[i]));                
            }

        }

        [Fact]

        [Given(@"I have entered Juan as the name and default Errors and Difficulty")]
        [When(@"I enter Juan as the word to guess 4 times")]
        [Then(@"I should be told that I lost")]

        public void LoseGame()
        {

            _driver.Navigate()
                .GoToUrl(baseURL);

            Thread.Sleep(time);

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Juan");

            Thread.Sleep(1000);

            _driver.FindElement(By.Id("Play"))
                .Click();

            var palabraIntentada = _driver.FindElement(By.Id("palabra-intentada"));
            var btnIntentar = _driver.FindElement(By.Id("intentar-button"));
            var juegoGanado = _driver.FindElement(By.Id("juego-ganado"));

            Thread.Sleep(time);
            
            string palabra = "JUAN";

            ((IJavaScriptExecutor)_driver).ExecuteScript("document.getElementById('intentar-button').style.display='block';");

            for (int i = 0; i < 4; i++)
            {
                palabraIntentada.SendKeys(palabra);
                Thread.Sleep(500);
                btnIntentar.Click();
                Thread.Sleep(500);
            }

            Thread.Sleep(1000);
            Assert.Equal("false", juegoGanado.GetAttribute("value"));
        }

        [Fact]

        [Given(@"I have entered Juan as the name and default Errors and Difficulty")]
        [When(@"I enter the correct word")]
        [Then(@"I should be told that I won")]

        public void WinGame()
        {
            _driver.Navigate()
                .GoToUrl(baseURL);

            Thread.Sleep(time);

            _driver.FindElement(By.Id("Name"))
                .SendKeys("Juan");

            Thread.Sleep(1000);

            _driver.FindElement(By.Id("Play"))
                .Click();

            var palabraIntentada = _driver.FindElement(By.Id("palabra-intentada"));
            var btnIntentar = _driver.FindElement(By.Id("intentar-button"));
            var juegoGanado = _driver.FindElement(By.Id("juego-ganado"));

            string[] palabras = new string[] { "CASA", "PATO", "LORO", "AUTO" };

            ((IJavaScriptExecutor)_driver).ExecuteScript("document.getElementById('intentar-button').style.display='block';");

            
            foreach (var palabra in palabras)
            {
                palabraIntentada.SendKeys(palabra);
                Thread.Sleep(500);
                btnIntentar.Click();
                Thread.Sleep(500);
                if (juegoGanado.GetAttribute("value") == "true") 
                { 
                    break;
                }
            }

            
            Thread.Sleep(time);

            Assert.Equal("true", juegoGanado.GetAttribute("value"));

        }




    }
}