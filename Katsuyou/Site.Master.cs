using System;
using System.Web.UI;

namespace Katsuyou
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool autenticado = Session["autenticado"] != null;

            lnkLogout.Visible = lnkAllVerbs.Visible = autenticado;
            lnkLogin.Visible = !autenticado;
        }
    }
}