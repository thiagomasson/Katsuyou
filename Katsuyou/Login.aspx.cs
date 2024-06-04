using System;
using System.Drawing;
using System.Web;
using System.Web.Security;

namespace Katsuyou
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Admin" && txtPassword.Text == "bababoue")
            {
                // Cria a variavel de sessão para identificar que o usuário está autenticado e
                // permitir a exibição das opções do menu.
                Session["autenticado"] = "";
                // 1. Inicializa a classe de autenticação
                FormsAuthentication.Initialize();
                // 2. CRIAR O TICKET
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "admin", DateTime.Now, DateTime.Now.AddMinutes(20), false, FormsAuthentication.FormsCookiePath);
                // 3. CRIPTOGRAFA P TICKET E GRAVAR NO COOKIE DO NAVEGADOR
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
                // Redireciona para o form que o usuário tentou acessar
                Response.Redirect(FormsAuthentication.GetRedirectUrl("Admin", false));
            }
            else
            {
                txtMsg.ForeColor = Color.Red;
                txtMsg.Text = "Invalid username or password";
            }
        }
    }
}