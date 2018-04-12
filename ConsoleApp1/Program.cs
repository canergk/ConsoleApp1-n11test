using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string URL = "https://www.n11.com/";
            IWebDriver IWD = new ChromeDriver();
            IJavaScriptExecutor js = (IJavaScriptExecutor)IWD;
            IWD.Navigate().GoToUrl(URL);
            
            if (IWD.Url == URL)
            {
                Console.WriteLine("İstenilen sayfa açılmıştır:" + IWD.Url);

                //IWD.Manage().Timeouts().PageLoad = new TimeSpan(0,0,40);
                //WebDriverWait wait = new WebDriverWait();

                //System.Threading.Thread.Sleep(13000);

                // bu neden çalışmadı anlamadım, beklemesi gerekiyordu
                //IWD.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15000);

                bool mustWait = true;
                IWD.Manage().Window.Maximize();

                
                string onay = (string)js.ExecuteScript("return location.href");
                if (IWD.Url==onay)   
                {
                    onay = "bulunduğunuz sayfa:" + onay;
                    js.ExecuteScript("alert('" + onay + "')");
                    Console.WriteLine(onay);
                }
                

                while (mustWait)
                {
                    try
                    {
                        if (IWD.FindElement(By.ClassName("fancybox-body")).Displayed)
                        {
                            mustWait = false;
                            
                            IWD.FindElement(By.ClassName("sgm-notification-close")).Click();
                            //if (IWD.FindElement(By.ClassName("fancybox-body")).Displayed.Equals(false))
                            //{
                            //    continue;
                            //}
                        }
                        else
                        {
                            IWD.Navigate().GoToUrl(IWD.Url);
                        }
                    }
                    catch (Exception e)
                    {
                        //throw e;
                    }
                    

                }
                System.Threading.Thread.Sleep(1500);

                IWD.FindElement(By.ClassName("btnSignIn")).Click();

                string urlLogin = "https://www.n11.com/giris-yap";
                string email = "cangokku@gmail.com";
                string pass = "Caner18*";
                if (IWD.Url == urlLogin)

                {
                    IWD.FindElement(By.Id("email")).Clear();
                    IWD.FindElement(By.Id("password")).Clear();
                    IWD.FindElement(By.Id("email")).SendKeys(email);
                    IWD.FindElement(By.Id("password")).SendKeys(pass);
                    IWD.FindElement(By.Id("loginButton")).Click();
                }
                
            }
            else
            {
                Console.WriteLine("Bir hata oluştu, istenilen URL:" + URL +"Gidilen URL:"+IWD.Url);
                IWD.Quit();
            }
            string searchParameter = "samsung";
            IWD.FindElement(By.Id("searchData")).Clear();
            IWD.FindElement(By.Id("searchData")).SendKeys(searchParameter);
            IWD.FindElement(By.ClassName("searchBtn")).Click();

            string searchResultCheck = "Aramalarda " + searchParameter + " bulundu.";

            if (IWD.FindElement(By.ClassName("content")).Text.Contains(searchParameter))
            {
                
                Console.Write(searchResultCheck);
                //js.ExecuteScript("alert('"+searchResultCheck+"')");
                
            }
            else
            {
                searchResultCheck = "Aradığınız veri: " + searchParameter + " sayfada bulunamadı.";
                Console.WriteLine(searchResultCheck);
                js.ExecuteScript("alert('" + searchResultCheck + "')");
                IWD.Quit();
            }


            //RemoteWebElement contentElement = (RemoteWebElement)(IWD.FindElement(By.Id("wrapper")));

            //contentElement.SendKeys(Keys.Space);
            //pagination

            var bottomElement = IWD.FindElement(By.ClassName("pagination"));
            Actions bottomActions = new Actions(IWD);
            bottomActions.MoveToElement(bottomElement);
            bottomActions.Perform();
            
              // IWD.FindElement(By.LinkText("2")).Click();
            IWD.FindElement(By.CssSelector("#contentListing > div > div > div.productArea > div.pagination > a:nth-child(2)")).Click();

            //tekrar sayfa altına inmek istedim,patladı.
            //var bottomElement2 = IWD.FindElement(By.ClassName("pagination"));
            //Actions bottomActions2 = new Actions(IWD);
            //bottomActions.MoveToElement(bottomElement);
            //bottomActions.Perform();

            string result = IWD.FindElement(By.CssSelector("div.pagination a.active")).Text;
            if(result == "2")
            {
                //click this to add fav
                //eski takiple bulduğum selector ".resultListGroup div.listView ul li: nth - child(3) div.columnContent div.proDetail span.followBtn"

                var favorite = IWD.FindElement(By.CssSelector(".resultListGroup div.listView ul li:nth-child(3) div.columnContent div.proDetail span.followBtn"));
                Actions favorAction = new Actions(IWD);
                favorAction.MoveToElement(favorite);
                favorAction.Perform();
                IWD.FindElement(By.CssSelector(".resultListGroup div.listView ul li:nth-child(3) div.columnContent div.proDetail span.followBtn")).Click();
                
            }

            Actions menuActions = new Actions(IWD);
            RemoteWebElement menuElement = (RemoteWebElement)(IWD.FindElement(By.CssSelector("#header > div > div > div.hMedMenu > div.customMenu > div.myAccountHolder.customMenuItem.hasMenu.withLocalization > div.myAccount > a.menuTitle")));

            menuActions.MoveToElement(menuElement).Build().Perform();

            // bread crumb a kadar çık.
            //var topElement = IWD.FindElement(By.CssSelector("#breadCrumb > ul > li:nth-child(2) > a > span"));
            //Actions topActions = new Actions(IWD);
            //topActions.MoveToElement(topElement);
            //topActions.Perform();

            IWD.FindElement(By.CssSelector(cssSelectorToFind: "#header > div > div > div.hMedMenu > div.customMenu > div.myAccountHolder.customMenuItem.hasMenu.withLocalization > div.myAccountMenu.hOpenMenu > div > a:nth-child(2)")).Click();

        }

    }
}
