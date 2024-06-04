using Datapost.DB;
using System;
using System.Data;
using System.Web.UI;
using WanaKanaNet;

namespace Katsuyou
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    lblID.Text = Request.QueryString["verb"].ToString();

                    if (int.TryParse(lblID.Text, out int id))
                        GetVerb(id);
                }
                else
                    GetVerb();
            }
        }

        protected void GetVerb(int id = 0)
        {
            if (Request.QueryString.HasKeys())
            {
                if (int.TryParse(Request.QueryString["verb"].ToString(), out int queryId))
                    id = queryId;
            }

            string comandoSQL = $"SELECT TOP 1 * FROM Verbs ORDER BY Rnd(-(100000*VerbID)*Time())";
            if (id > 0)
                comandoSQL = $"SELECT * FROM Verbs WHERE VerbID={id}";
            DataTable tb = GetSQLTable(comandoSQL);

            if (tb.Rows.Count > 0)
            {
                lblID.Text = tb.Rows[0]["VerbID"].ToString();
                var type = tb.Rows[0]["Type"].ToString();
                lblType.Text = type + " verb";
                var verb = tb.Rows[0]["DictionaryForm"].ToString();
                lblVerb.Text = verb;
                lblConjugated.Text = GetRandomConjugation(verb, type);
                lblTranslation.Text = tb.Rows[0]["English"].ToString();
                string img = tb.Rows[0]["ImageURL"].ToString();
                if (Uri.IsWellFormedUriString(img, UriKind.Absolute))
                    imgVerb.ImageUrl = img;
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected string GetRandomConjugation(string verb, string type)
        {
            string romaji = WanaKana.ToRomaji(verb);
            string finalVerb = "";

            Random rand = new Random();
            bool negative = rand.Next(2) == 1;
            bool polite = rand.Next(2) == 1;
            bool past = rand.Next(2) == 1;

            lblConjugations.Text = polite ? "🕴️Polite" : "🏡Plain";
            lblConjugations.Text += negative ? ", 🚫Negative" : "";
            lblConjugations.Text += past ? ", ⏳Past" : "";

            switch (rand.Next(10))
            {
                case 0:
                    romaji = GetPotential(romaji, ref type);
                    lblConjugations.Text += ", ❓Potential";
                    break;
                case 1:
                    romaji = GetPassive(romaji, ref type);
                    lblConjugations.Text += ", 👍Passive";
                    break;
                case 2:
                    romaji = GetCausative(romaji, ref type);
                    lblConjugations.Text += ", ☝️Causative";
                    break;
                case 3:
                    romaji = GetCausativePassive(romaji, ref type);
                    lblConjugations.Text += ", ☝️👍Causative passive";
                    break;
            }

            if (type.ToLower() != "ichidan" && type.ToLower() != "godan")
            {
                switch (type.ToLower())
                {
                    case "desu":
                        finalVerb = romaji.Substring(0, romaji.Length - 4);
                        if (polite)
                        {
                            if (negative)
                            {
                                finalVerb += "jaarimasen";
                                if (past)
                                    finalVerb += "deshita";
                            }
                            else
                            {
                                if (past)
                                    finalVerb += "deshita";
                                else
                                    finalVerb = romaji;
                            }
                        }
                        else // plain
                        {
                            if (negative)
                            {
                                if (past)
                                    finalVerb += "janakatta";
                                else
                                    finalVerb += "janai";
                            }
                            else
                            {
                                if (past)
                                    finalVerb += "datta";
                                else
                                    finalVerb += "da";
                            }
                        }
                        break;
                    case "suru":
                        finalVerb = romaji.Substring(0, romaji.Length - 4) + "shi";
                        if (polite)
                        {
                            if (negative)
                            {
                                finalVerb += "masen";
                                if (past)
                                    finalVerb += "deshita";
                            }
                            else
                            {
                                if (past)
                                    finalVerb += "mashita";
                                else
                                    finalVerb += "masu";
                            }
                        }
                        else // plain
                        {
                            if (negative)
                            {
                                if (past)
                                    finalVerb += "nakatta";
                                else
                                    finalVerb += "nai";
                            }
                            else
                            {
                                if (past)
                                    finalVerb += "ta";
                                else
                                    finalVerb = romaji;
                            }
                        }
                        break;
                    case "kuru":
                        if (polite)
                        {
                            finalVerb = romaji.Substring(0, romaji.Length - 4) + "ki";
                            if (negative)
                            {
                                finalVerb += "masen";
                                if (past)
                                    finalVerb += "masdeshita";
                            }
                            else
                            {
                                if (past)
                                    finalVerb += "mashita";
                                else
                                    finalVerb += "masu";
                            }
                        }
                        else // plain
                        {
                            finalVerb = romaji.Substring(0, romaji.Length - 4);
                            if (negative)
                            {
                                if (past)
                                    finalVerb += "konakatta";
                                else
                                    finalVerb += "konai";
                            }
                            else
                            {
                                if (past)
                                    finalVerb += "kita";
                                else
                                    finalVerb = romaji;
                            }
                        }
                        break;
                }
            }
            else
            {
                if (polite)
                {
                    finalVerb = GetStem(romaji, type) + "mas";
                    if (negative)
                    {
                        finalVerb += "en";
                        if (past)
                            finalVerb += "deshita";
                    }
                    else
                    {
                        if (past)
                            finalVerb += "hita";
                        else
                            finalVerb += "u";
                    }
                }
                else // plain
                {
                    if (negative)
                    {
                        if (romaji == "aru")
                            finalVerb = "na";
                        else
                            finalVerb = GetAStep(romaji, type) + "na";
                        if (past)
                            finalVerb += "katta";
                        else
                            finalVerb += "i";
                    }
                    else
                    {
                        if (past)
                            finalVerb = GetTeOrTaForm(romaji, type, true);
                        else
                            finalVerb += romaji;
                    }
                }
            }

            return WanaKana.ToHiragana(finalVerb);
        }

        public string GetCausativePassive(string romaji, ref string type) => GetPassive(GetCausative(romaji, ref type), ref type);

        public string GetCausative(string romaji, ref string type)
        {
            if (type.ToLower() == "godan")
                romaji = GetAStep(romaji, type);
            else if (type.ToLower() == "ichidan")
                romaji = romaji.Substring(0, romaji.Length - 2) + "sa";
            else if (type.ToLower() == "suru")
                romaji = romaji.Substring(0, romaji.Length - 4) + "sa";
            else if (type.ToLower() == "kuru")
                romaji = romaji.Substring(0, romaji.Length - 4) + "kosa";

            type = "ichidan";
            return romaji + "seru";
        }

        public string GetPassive(string romaji, ref string type)
        {
            if (type.ToLower() == "godan")
                romaji = GetAStep(romaji, type);
            else if (type.ToLower() == "ichidan")
                romaji = romaji.Substring(0, romaji.Length - 2) + "ra";
            else if (type.ToLower() == "suru")
                romaji = romaji.Substring(0, romaji.Length - 4) + "sa";
            else if (type.ToLower() == "kuru")
                romaji = romaji.Substring(0, romaji.Length - 4) + "kora";

            type = "ichidan";
            return romaji + "reru";
        }

        public string GetPotential(string romaji, ref string type, bool shorterIchidan = false)
        {
            if (type.ToLower() == "godan")
            {
                if (romaji == "aru")
                    romaji = "arieru";
                else
                    romaji = GetEStep(romaji, type);
            }
            else if (type.ToLower() == "ichidan")
            {
                romaji = romaji.Substring(0, romaji.Length - 2);
                if (!shorterIchidan)
                    romaji += "ra";
                romaji += "re";
            }
            else if (type.ToLower() == "suru")
                romaji = romaji.Substring(0, romaji.Length - 4) + "deki";
            else if (type.ToLower() == "kuru")
                romaji = romaji.Substring(0, romaji.Length - 4) + "korare";

            type = "ichidan";
            return romaji + "ru";
        }

        private string GetTeOrTaForm(string romaji, string type, bool ta = false)
        {
            if (type.ToLower() == "godan")
            {
                if (romaji.Equals("iku"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 2) + "tt";
                }
                else if (romaji.EndsWith("tsu"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 3) + "tt";
                }
                else if (romaji.EndsWith("ru"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 2) + "tt";
                }
                else if (romaji.EndsWith("su"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 2) + "shit";
                }
                else if (romaji.EndsWith("ku"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 2) + "it";
                }
                else if (romaji.EndsWith("gu"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 2) + "id";
                }
                else if (romaji.EndsWith("mu") || romaji.EndsWith("nu") || romaji.EndsWith("bu"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 2) + "nd";
                }
                else if (romaji.EndsWith("u"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 1) + "tt";
                }
            }
            else if (type.ToLower() == "ichidan")
                romaji = romaji.Substring(0, romaji.Length - 2) + "t";
            else if (type.ToLower() == "suru")
                romaji = romaji.Substring(0, romaji.Length - 2) + "shit";
            else if (type.ToLower() == "kuru")
                romaji = romaji.Substring(0, romaji.Length - 2) + "kit";

            return romaji + (ta ? "a" : "e");
        }

        private string GetStem(string romaji, string type)
        {
            if (type.ToLower() == "godan")
            {
                if (romaji.EndsWith("tsu"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 3);
                    romaji += "chi";
                }
                else if (romaji.EndsWith("su"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 2);
                    romaji += "shi";
                }
                else if (romaji.EndsWith("u"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 1);
                    romaji += "i";
                }
            }
            else if (type.ToLower() == "ichidan")
                romaji = romaji.Substring(0, romaji.Length - 2);

            return romaji;
        }

        private string GetAStep(string romaji, string type)
        {
            if (type.ToLower() == "godan")
            {
                if (romaji.EndsWith("tsu"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 3);
                    romaji += "ta";
                }
                else if (romaji.EndsWith("su") || romaji.EndsWith("ku") || romaji.EndsWith("gu")
                    || romaji.EndsWith("bu") || romaji.EndsWith("nu") || romaji.EndsWith("mu") || romaji.EndsWith("ru"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 1);
                    romaji += "a";
                }
                else if (romaji.EndsWith("u"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 1);
                    romaji += "wa";
                }
            }
            else if (type.ToLower() == "ichidan")
                romaji = romaji.Substring(0, romaji.Length - 2);

            return romaji;
        }

        private string GetEStep(string romaji, string type)
        {
            if (type.ToLower() == "godan")
            {
                if (romaji.EndsWith("tsu"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 2);
                }
                else if (romaji.EndsWith("u"))
                {
                    romaji = romaji.Substring(0, romaji.Length - 1);
                }
                romaji += "e";
            }
            else if (type.ToLower() == "ichidan")
                romaji = romaji.Substring(0, romaji.Length - 2);

            return romaji;
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string answer = txtAnswer.Value;

            if (!string.IsNullOrWhiteSpace(answer))
            {
                lblConjugated.Visible = btnGiveUp.Visible = false;
                divAnswer.Visible = true;
                if (lblConjugated.Text == answer)
                {
                    divAnswer.Style["background-color"] = "green";
                    lblCorrection.Text = lblVerb.Text + " -> " + lblConjugated.Text;
                    if (int.TryParse(lblStreak.Text, out int streak))
                    {
                        lblStreak.Text = (++streak).ToString();
                        if (int.TryParse(lblMaxStreak.Text, out int maxStreak) && maxStreak < streak)
                            lblMaxStreak.Text = streak.ToString();
                    }
                    GetVerb();
                }
                else
                {
                    divAnswer.Style["background-color"] = "red";
                    lblCorrection.Text = lblVerb.Text + " -> X" + txtAnswer.Value;
                    btnGiveUp.Visible = true;
                    lblStreak.Text = "0";
                }
                txtAnswer.Value = "";
                txtAnswer.Focus();
            }
        }

        protected void btnGiveUp_Click(object sender, EventArgs e)
        {
            lblConjugated.Visible = true;
            btnGiveUp.Visible = false;
            lblStreak.Text = "0";
        }
    }
}