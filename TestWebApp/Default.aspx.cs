using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    lblEmail.Text = LoginControl1.Email;
            //}
        }

        protected void LoginControl1_Submit(object sender, Company.CustomControls.LoginControl.LoginEventArgs e)
        {
            lblEmail.Text = LoginControl1.Email;
        }
    }
}