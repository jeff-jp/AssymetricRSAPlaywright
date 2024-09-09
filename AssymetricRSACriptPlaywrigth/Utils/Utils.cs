using AssymetricRSACriptPlaywrigth.Crawler;
using AssymetricRSACriptPlaywrigth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssymetricRSACriptPlaywrigth.Utils
{
    public static class Utils
    {

        public static DadosRSA GerarKeysRSA(string urlGenerator)
        {
            DadosRSA resultCredenciais = new DadosRSA();

            try
            {
                resultCredenciais = EncryptRSA.GerarChave(urlGenerator);
                CachingTest.AddCache("CRIPT_PRIVATE_KEY_RSA", resultCredenciais.PrivateKey);
                CachingTest.AddCache("CRIPT_PUBLIC_KEY_RSA", resultCredenciais.PublicKey);
            }
            catch (Exception ex)
            {
                resultCredenciais = null;
            }

            return resultCredenciais;
        }

        public static string DecryptRSA(string textEncrypted, string urlGenerator)
        {
            string resultDecryptedText = "";

            DadosRSA keysRSA = new DadosRSA();
            keysRSA.PrivateKey = CachingTest.GetCache<string>("CRIPT_PRIVATE_KEY_RSA");
            keysRSA.PublicKey = CachingTest.GetCache<string>("CRIPT_PUBLIC_KEY_RSA");

            resultDecryptedText = EncryptRSA.DecryptRSA(urlGenerator, textEncrypted, keysRSA);

            return resultDecryptedText;
        }

    }
}