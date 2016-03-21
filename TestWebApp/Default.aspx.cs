using System;
using System.Web.UI;
using Company.CustomControls;

namespace TestWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoginControl1.EmailTitle = "Email 1";
                LoginControl2.EmailTitle = "Email 2";
                LoginControl3.EmailTitle = "Email 3";
                LoginControl4.EmailTitle = "Email 4";
                LoginControl5.EmailTitle = "Email 5";
            }

        }

        protected void LoginControl1_Submit(object sender, LoginControl.LoginEventArgs e)
        {
            lblNewEmail.Text = e.NewEmail;
            lblOldEmail.Text = e.OldEmail;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginControl1_", "alert(" + LoginControl1.ClientScriptGetEmail + ");", true);
        }

        protected void LoginControl2_Submit(object sender, LoginControl.LoginEventArgs e)
        {
            lblNewEmail.Text = e.NewEmail;
            lblOldEmail.Text = e.OldEmail;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginControl2_", "alert(" + LoginControl2.ClientScriptGetEmail + ");", true);
        }

        protected void LoginControl5_Submit(object sender, LoginControl.LoginEventArgs e)
        {
            lblNewEmail.Text = e.NewEmail;
            lblOldEmail.Text = e.OldEmail;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginControl3_", "alert(" + LoginControl5.ClientScriptGetEmail + ");", true);
        }

        protected void LoginControl4_Submit(object sender, LoginControl.LoginEventArgs e)
        {
            lblNewEmail.Text = e.NewEmail;
            lblOldEmail.Text = e.OldEmail;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginControl4_", "alert(" + LoginControl4.ClientScriptGetEmail + ");", true);
        }

        protected void LoginControl3_Submit(object sender, LoginControl.LoginEventArgs e)
        {
            lblNewEmail.Text = e.NewEmail;
            lblOldEmail.Text = e.OldEmail;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginControl5_", "alert(" + LoginControl3.ClientScriptGetEmail + ");", true);
        }
    }
}