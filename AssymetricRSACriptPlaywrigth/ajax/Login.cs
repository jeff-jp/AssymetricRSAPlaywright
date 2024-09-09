using AssymetricRSACriptPlaywrigth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AssymetricRSACriptPlaywrigth.ajax
{
    public class Login  : System.Web.UI.Page
    {
        [WebMethod]
        public object TestInitialization(object paramTest)
        {
            string name = "JJP - Jeff Junio";

            string _urlGerador = HttpContext.Current.Request.Url.Authority + "/Generator.html";
            DadosRSA Keys = Utils.Utils.GerarKeysRSA(_urlGerador);

            var result = new
            {
                message = "Teste",
                nome = name,
                publicKey = Keys.PublicKey
            };

            return result;
        }
    }
}