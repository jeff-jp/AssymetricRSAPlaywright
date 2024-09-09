<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AssymetricRSACriptPlaywrigth.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="js/jquery-3.6.0.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            console.log("document loaded");
            PreparaAJAX2();            
        });

        $(window).on("load", function () {
            console.log("window loaded");
        });

        function Show() {

            alert('Voltou Ajax')
            console.log("Voltou Ajax")
            debugger;

            //$('#div_Loader').show();
        }

        function Hide() {

            alert('Voltou Ajax')
            console.log("Voltou Ajax")
            debugger;

            //$('#div_Loader').hide();
        }

        function PreparaAJAX2() {
            //var name = $('#txtName').val();
            var name = 'TesteAjax';
            $.ajax({
                type: "POST",
                url: "Login.aspx/Initialization",
                data: null,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d.publicKey);
                    localStorage.setItem("__CK__", response.d.publicKey);
                    console.log(response.d);
                    //$('#lblMessage').text(response.d);
                },
                failure: function (response) {
                    console.log("Error: " + response.d);
                    alert("Error: " + response.d);
                    //$('#lblMessage').text("Error: " + response.d);
                }
            });
        }

    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>

            <label>Login</label>
            <input type="text" id="CampoLogin" />

        </div>
        <div>

            <label>Senha</label>
            <input type="password" id="CampoPass"  />

        </div>
        <div>
            <button type="button" id="btnAcessar">Acessar</button>
        </div>
    </form>

    <script src="js/jquery-3.6.0.js" type="text/javascript"></script>
    <script src="js/jsencrypt.min.js" type="text/javascript"></script>

    <%-- TODO - Talvez tenha que por o nonce na chamada desse javascript ppra poliitica de segurança não barrar --%>
    <%-- Só copiar e colar um nonce já existente e colocar aqui --%>
    <script type="text/javascript">

        function ExecAjax(method, params, callback) {

            var _url = "Login.aspx/" + method;
            var _dataParams = null

            if (params != null && params != undefined) {
                _dataParams = JSON.stringify(params)
            }

            var result = null

            $.ajax({
                type: "POST",
                url: _url,
                data: _dataParams,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d.message);
                    console.log(response.d);
                    //$('#lblMessage').text(response.d);
                    //result = response.d;

                    if (callback != undefined && callback != null) {
                        callback(response.d)
                    }

                },
                failure: function (response) {
                    console.log("Error: " + response.d);
                    alert("Error: " + response.d);
                    //$('#lblMessage').text("Error: " + response.d);
                    //result = response.d;
                }
            });
        }

        function EncryptRSA(text) {

            var _key = localStorage.getItem("__CK__");

            var crypt = new JSEncrypt();

            crypt.setPublicKey(_key);
            var crypted = crypt.encrypt(text);

            return crypted;
        }

        $('#btnAcessar').on('click', function () {

            var _usuario = $('#CampoLogin').val()
            var _pass = $('#CampoPass').val()
            var _diretorio = '';

            debugger;

            //ExecAjax("Autenticar", '{user: "' + EncryptRSA(_usuario) + '", pass: "' + EncryptRSA(_pass) + '",  diretorio: "' + EncryptRSA(_diretorio)+ '"}', function (data) {
            ExecAjax("Autenticar", {user: EncryptRSA(_usuario), pass: EncryptRSA(_pass), diretorio: EncryptRSA(_diretorio)}, function (data) {

                alert('Voltou Autenticar');
                console.log(data);
                debugger;

            });

        });

    </script>

</body>
</html>
