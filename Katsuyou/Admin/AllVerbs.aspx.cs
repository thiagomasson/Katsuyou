using Datapost.DB;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Katsuyou.Admin
{
    public partial class AllVerbs : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                GetAllVerbs();
        }

        protected void GetAllVerbs()
        {
            DAO db = new DAO
            {
                DataProviderName = DAO.ProviderName.OleDb,
                ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/App_Data/Database.accdb") + ";Persist Security Info=False;"
            };

            string sql = "SELECT VerbID, DictionaryForm, English, Type, HonorificType, Status FROM Verbs ORDER BY VerbID ASC";

            GridViewVerbs.DataSource = db.Query(sql);
            GridViewVerbs.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerbEditor.aspx");
        }

        protected void GridViewVerbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label id = GridViewVerbs.SelectedRow.Cells[1].FindControl("lblID") as Label;
            Response.Redirect($"VerbEditor.aspx?verb={id.Text}");
            GetAllVerbs();
        }

        protected void GridViewVerbs_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewVerbs.EditIndex = e.NewEditIndex;
            GetAllVerbs();
        }

        protected void GridViewVerbs_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string verbID = (GridViewVerbs.Rows[e.RowIndex].FindControl("lblID") as Label).Text;
            string verb = (GridViewVerbs.Rows[e.RowIndex].FindControl("txtVerb") as TextBox).Text;
            string translation = (GridViewVerbs.Rows[e.RowIndex].FindControl("txtTranslation") as TextBox).Text;
            string type = (GridViewVerbs.Rows[e.RowIndex].FindControl("ddlType") as DropDownList).SelectedValue;
            string status = (GridViewVerbs.Rows[e.RowIndex].FindControl("ddlStatus") as DropDownList).SelectedValue;

            DAO db = new DAO
            {
                DataProviderName = DAO.ProviderName.OleDb,
                ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/App_Data/Database.accdb") + ";Persist Security Info=False;"
            };

            string sql = $"UPDATE Verbs SET DictionaryForm='{verb}', English='{translation}', Type='{type}', Status={status} WHERE VerbID={verbID};";

            db.Query(sql);

            GridViewVerbs.EditIndex = -1;
            GetAllVerbs();
        }

        protected void GridViewVerbs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewVerbs.EditIndex = -1;
            GetAllVerbs();
        }

        protected void GridViewVerbs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewVerbs.PageIndex = e.NewPageIndex;
            GetAllVerbs();
        }
    }
}