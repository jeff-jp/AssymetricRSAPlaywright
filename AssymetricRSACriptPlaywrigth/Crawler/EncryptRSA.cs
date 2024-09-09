using AssymetricRSACriptPlaywrigth.Model;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AssymetricRSACriptPlaywrigth.Crawler
{
    public static class EncryptRSA
    {
        private static string _PathExe;
        public static string PathExe
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_PathExe))
                {
                    
                    _PathExe = "";
                }

                return _PathExe;
            }
        }

        private static string GeneratorURL { get; set; }

        private static string _PathURL;
        public static string PathURL
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_PathURL))
                {
                    _PathURL = "";
                }

                return _PathURL;
            }
        }

        private static bool IsExecuting { get; set; } = false;

        private static DadosRSA KeysRSA { get; set; } = new DadosRSA();

        public static DadosRSA GerarChave(string urlGerador)
        {
            GeneratorURL = urlGerador;

            //DadosRSA dados = new DadosRSA();
            DadosRSA dados = null;
            IsExecuting = true;

            Thread LerGeradorRSA = new Thread(async delegate ()
            {

                dados = await GenerateKeys();
                var strCHave = dados != null ? dados.PublicKey : "-";
                IsExecuting = false;
            });

            LerGeradorRSA.Start();

            while (IsExecuting)
            {
                Thread.Sleep(2000);
            }

            return dados;
        }

        public static string DecryptRSA(string urlGerador, string encryptedText, DadosRSA keys)
        {
            string resulDecryptedText = "";

            GeneratorURL = urlGerador;
            KeysRSA = keys;

            IsExecuting = true;

            Thread DecryptGeradorRSA = new Thread(async delegate ()
            {

                resulDecryptedText = await DecriptTextRSA(encryptedText);
                IsExecuting = false;
            });

            DecryptGeradorRSA.Start();

            while (IsExecuting)
            {
                Thread.Sleep(2000);
            }

            return resulDecryptedText;
        }

        private static async Task<DadosRSA> GenerateKeys()
        {
            DadosRSA dadosRSA = null;

            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true,
                SlowMo = 500
            });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            //Navigate to page
            await page.GotoAsync(GeneratorURL);

            string _campoPrivateKey = await page.InputValueAsync("[id=\"privkey\"]");
            string _campoPublicKey = await page.InputValueAsync("[id=\"pubkey\"]");

            dadosRSA = new DadosRSA();
            dadosRSA.PrivateKey = _campoPrivateKey;
            dadosRSA.PublicKey = _campoPublicKey;

            //Close the page 
            await page.CloseAsync();

            return dadosRSA;

        }

        private static IPlaywright _PlayWrightDecrypt;
        private static IPlaywright PlayWrightDecrypt
        {
            get
            {
                if(_PlayWrightDecrypt == null)
                {
                    bool auxIsExecuting = true;
                    Thread PrepararObjTela = new Thread(async delegate ()
                    {
                        _PlayWrightDecrypt = await PrepararPlaywrightDecrypt();
                        auxIsExecuting = false;
                    });
                    PrepararObjTela.Start();

                    while (auxIsExecuting)
                    {
                        Thread.Sleep(2000);
                    }
                }

                return _PlayWrightDecrypt;
            }
        }

        private static IBrowser _BrowserDecrypt;
        private static IBrowser BrowserDecrypt
        {
            get
            {
                if(_BrowserDecrypt == null)
                {
                    bool auxIsExecuting = true;
                    Thread PrepararObjTela = new Thread(async delegate ()
                    {
                        _PlayWrightDecrypt = await PrepararPlaywrightDecrypt();
                        auxIsExecuting = false;
                    });
                    PrepararObjTela.Start();

                    while (auxIsExecuting)
                    {
                        Thread.Sleep(2000);
                    }
                }
                return _BrowserDecrypt;
            }
        }


        private static IBrowserContext _ContextBrowserDecrypt;
        private static IBrowserContext ContextBrowserDecrypt
        {
            get
            {
                if(_ContextBrowserDecrypt == null)
                {
                    bool auxIsExecuting = true;
                    Thread PrepararObjTela = new Thread(async delegate ()
                    {
                        _PlayWrightDecrypt = await PrepararPlaywrightDecrypt();
                        auxIsExecuting = false;
                    });
                    PrepararObjTela.Start();

                    while (auxIsExecuting)
                    {
                        Thread.Sleep(2000);
                    }
                }
                return _ContextBrowserDecrypt;
            }
        }

        private static IPage _PageDecrypt = null;
        private static IPage PageDecrypt
        {
            get
            {
                if(_PageDecrypt == null)
                {
                    bool auxIsExecuting = true;
                    Thread PrepararObjTela = new Thread(async delegate ()
                    {
                        _PageDecrypt = await PrepararPageDecrypt();
                        auxIsExecuting = false;
                    });
                    PrepararObjTela.Start();

                    while (auxIsExecuting)
                    {
                        Thread.Sleep(2000);
                    }

                }
                return _PageDecrypt;
            }
        }

        private static async Task<IPlaywright> PrepararPlaywrightDecrypt()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                //TO DO- JJP
                //Este ExecutablePath tem que ser mudado para o caminho do Chromium que fica no T:\SicofWeb\Bibliotecas\Playwright
                //Talvez tenha que atualizar a versão do Chromium da Attest, no console do PowerShell usar:
                //pwsh bin/Debug/netX/playwright.ps1 install
                //Vai ser baixado os arquivos na pasta "C:\Users\SeuUsuario\AppData\Local\ms-playwright\chromium-1129\chrome-win\chrome.exe"


                //ExecutablePath = @"C:\path\to\chromium\chromium.exe", 
                Headless = true, //true: roda em background / false: ver tela do Chromium (ideal para testar e acompanhar)
                SlowMo = 750
            });
            
            var context = await browser.NewContextAsync();

            _BrowserDecrypt = browser;
            _ContextBrowserDecrypt = context;
            return playwright;
        }

        private static async Task<IPage> PrepararPageDecrypt()
        {
            IPage page = await ContextBrowserDecrypt.NewPageAsync();
            return page;
        }


        private static async Task<string> DecriptTextRSA(string encryptedText)
        {
            //Navigate to page
            await PageDecrypt.GotoAsync(GeneratorURL);

            //Insert Keys to fields
            await PageDecrypt.FillAsync("[id=\"privkey\"]", KeysRSA.PrivateKey);
            await PageDecrypt.FillAsync("[id=\"pubkey\"]", KeysRSA.PublicKey);

            //Insert Keys to fields
            await PageDecrypt.FillAsync("[id=\"input\"]", "");
            await PageDecrypt.FillAsync("[id=\"crypted\"]", encryptedText);

            //Click button
            await PageDecrypt.ClickAsync("[id=\"execute\"]");

            //Get decrypted Text from field
            string resultDecrypted = await PageDecrypt.InputValueAsync("[id=\"input\"]");

            //JJP - Comentado para não fechar a pagina de decriptação no background.
            //Dessa forma agiliza as decriptações futuras caso precise
            //await PageDecrypt.CloseAsync();

            return resultDecrypted;
        }
    }
}