using AssymetricRSACriptPlaywrigth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssymetricRSACriptPlaywrigth
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static object Initialization()
        {
            string name = "Eu";

            string _urlGerador = HttpContext.Current.Request.Url.Authority + "/Generator.html";

            DadosRSA Keys = Utils.Utils.GerarKeysRSA(_urlGerador);

            var result = new
            {
                message = "Teste",
                nome = name,
                publicKey =Keys.PublicKey
            };

            return result;
        }


        [WebMethod]
        public static object Autenticar(string user, string pass, string diretorio)
        {
            string _urlGerador = HttpContext.Current.Request.Url.Authority + "/Generator.html";

            string usuario = Utils.Utils.DecryptRSA(user, _urlGerador);
            string senha = Utils.Utils.DecryptRSA(pass, _urlGerador);


            var result = new
            {
                message = "Sucesso"
            };

            return result;
        }

    }
}