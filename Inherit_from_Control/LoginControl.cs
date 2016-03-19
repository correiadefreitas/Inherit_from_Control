using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Company.CustomControls", "ccc")]

namespace Company.CustomControls
{
    [ToolboxData("<{0}:LoginControl runat=server></{0}:LoginControl>")]
    [System.Drawing.ToolboxBitmap(typeof(TextBox))]
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    [Serializable]
    public class LoginControl : Control, IPostBackDataHandler, IPostBackEventHandler
    {
        public delegate void LoginEventHandler(object sender, LoginEventArgs e);
        public event LoginEventHandler Submit;

        private string _Email;
        private string _Password;
        private LoginLayout _Layout;
        private string _EmailTitle;
        private string _PasswordTitle;

        public string Email { get { return _Email; } private set { _Email = value; } }
        public string Password { get { return _Password; } private set { _Password = value; } }
        public LoginLayout Layout { get { return _Layout; } set { _Layout = value; } }

        public string EmailTitle { get { return _EmailTitle; } set { _EmailTitle = value; } }
        public string PasswordTitle { get { return _PasswordTitle; } set { _PasswordTitle = value; } }

        //Submit event
        protected virtual void OnSubmit(LoginEventArgs e)
        {
            var Submit = this.Submit;
            if (Submit != null)
            {
                Submit(this, e);
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            var arg = this.Deserialize(eventArgument);
            OnSubmit(new LoginEventArgs(Email,Password));
        }

        internal string Serialize(LoginEventArgs e)
        {
            return (new JavaScriptSerializer()).Serialize(e);
        }

        private EventArgs Deserialize(string eventArgument)
        {
            return (new JavaScriptSerializer()).Deserialize<LoginEventArgs>(eventArgument);
        }

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            Email = postCollection[this.UniqueID.Replace("$", "_") + "Email"];
            Password = postCollection[this.UniqueID.Replace("$", "_") + "Password"];
            //Page.RegisterRequiresRaiseEvent(this);
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
            OnSubmit(new LoginEventArgs(Email, Password));
        }
        //End submit event

        protected override void Render(HtmlTextWriter writer)
        {
            //ToDo: apply layout

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace("$", "_") + "Email");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "Text");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, _EmailTitle);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            //ToDo: write password tag
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "submit");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "Entrar");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            
        }

        public enum LoginLayout : byte
        {
            Portrait,
            Landscape
        }

        [Serializable]
        public class LoginEventArgs : EventArgs
        {
            public string _Email;
            public string _Password;

            public string Email { get { return _Email; } private set { _Email = value; } }
            public string Password { get { return _Password; } private set { _Password = value; } }

            public LoginEventArgs() { }
            public LoginEventArgs(string Email, string Password)
            {
                this.Email = Email;
                this.Password = Password;
            }
        }
    }
}
    