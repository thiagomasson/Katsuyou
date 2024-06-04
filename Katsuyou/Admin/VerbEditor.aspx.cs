using Datapost.DB;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;

namespace Katsuyou.Admin
{
    public partial class VerbEditor : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString.HasKeys())
            {
                string verbID = Request.QueryString["verb"].ToString();

                if (int.TryParse(verbID, out int id))
                    GetVerb(id);
            }
        }

        protected void GetVerb(int id)
        {
            string comandoSQL = $"SELECT * FROM Verbs WHERE VerbID={id}";
            DataTable tb = GetSQLTable(comandoSQL);

            lblID.Visible = true;
            lblID.Text = tb.Rows[0]["VerbID"].ToString();
            txtVerb.Value = tb.Rows[0]["DictionaryForm"].ToString();
            txtTranslation.Text = tb.Rows[0]["English"].ToString();
            ddlType.SelectedValue = tb.Rows[0]["Type"].ToString();
            ddlHonorificType.SelectedValue = tb.Rows[0]["HonorificType"].ToString();
            ddlStatus.SelectedValue = tb.Rows[0]["Status"].ToString();
            string img = tb.Rows[0]["ImageURL"].ToString();
            txtImage.Text = img;
            if (Uri.IsWellFormedUriString(img, UriKind.Absolute))
                imgVerb.ImageUrl = img;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblAlert.ForeColor = Color.Red;
            
            if (txtVerb.Value.Trim() == "")
            {
                lblAlert.Text = "Type the dictionary form";
                txtVerb.Focus();
            }
            else if (!ValidVerb(txtVerb.Value.Trim()))
            {
                lblAlert.Text = "This verb already exists";
                txtVerb.Focus();
            }
            else if (txtTranslation.Text.Trim() == "")
            {
                lblAlert.Text = "Type the translation";
                txtTranslation.Focus();
            }
            else
            {
                DAO db = new DAO
                {
                    DataProviderName = DAO.ProviderName.OleDb,
                    ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/App_Data/Database.accdb") + ";Persist Security Info=False;"
                };

                string sql;
                if (int.TryParse(lblID.Text, out int id))
                {
                    sql = $"UPDATE Verbs SET DictionaryForm='{txtVerb.Value}', English='{txtTranslation.Text}', Type='{ddlType.SelectedValue}', HonorificType='{ddlHonorificType.SelectedValue}', Status={ddlStatus.SelectedValue}, ImageURL='{txtImage.Text}' WHERE VerbID={id};";
                }
                else
                {
                    sql = $"INSERT INTO Verbs(DictionaryForm, English, Type, HonorificType, Status, ImageURL) VALUES('{txtVerb.Value}', '{txtTranslation.Text}','{ddlType.SelectedValue}', '{ddlHonorificType.SelectedValue}', {ddlStatus.SelectedValue}, '{txtImage.Text}');";
                }

                try
                {
                    db.Query(sql); 
                    Response.Redirect("AllVerbs.aspx");
                }
                catch (Exception ex)
                {
                    lblAlert.Text = ex.Message;
                }
            }
        }

        protected bool ValidVerb(string verb)
        {
            string sql = $"SELECT DictionaryForm, VerbID FROM Verbs WHERE DictionaryForm='{verb}';";
            DataTable dt = GetSQLTable(sql);

            return dt.Rows.Count <= 0 || Equals(dt.Rows[0]["VerbID"].ToString(), lblID.Text);
        }

        protected DataTable GetSQLTable(string query)
        {
            DAO db = new DAO
            {
                DataProviderName = DAO.ProviderName.OleDb,
                ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/App_Data/Database.accdb") + ";Persist Security Info=False;"
            };
  
            return (DataTable)db.Query(query);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            imgVerb.ImageUrl = txtImage.Text + "?t=" + DateTime.Now.Ticks.ToString();
        }
    }
}