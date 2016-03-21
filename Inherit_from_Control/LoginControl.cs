using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Company.CustomControls", "ccc")]

namespace Company.CustomControls
{
    [ToolboxData("<{0}:LoginControl runat=server></{0}:LoginControl>")]
    [System.Drawing.ToolboxBitmap(typeof(TextBox))]
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    [DefaultProperty("Email")]
    [Serializable]
    public class LoginControl : Control, IPostBackEventHandler , IPostBackDataHandler
    {
        public delegate void LoginEventHandler(object sender, LoginEventArgs Old);
        public event LoginEventHandler Submit;

        private string _Email;
        private string _Password;
        private LoginLayout _Layout;
        private string _EmailTitle;
        private string _PasswordTitle;
        private string _ClientScriptGetEmail;


        [Category("Appearance")]
        [DefaultValue("")]
        private string Email {
            get {
                return _Email;
            }
            set {
                _Email = value;
            }
        }

        private string Password {
            get {
                return _Password;
            }
            set {
                _Password = value;
            }
        }

        public LoginLayout Layout {
            get {
                return _Layout;
            }
            set {
                _Layout = value;
            }
        }

        public string EmailTitle {
            get {
                return _EmailTitle;
            }
            set {
                _EmailTitle = value;
            }
        }

        public string PasswordTitle {
            get {
                return _PasswordTitle;
            }
            set {
                _PasswordTitle = value;
            }
        }

        public string ClientScriptGetEmail
        {
            get
            {
                return _ClientScriptGetEmail;
            }

            private set
            {
                _ClientScriptGetEmail = value;
            }
        }

        //Submit event
        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            ClientScriptGetEmail = "LoginControlGetEmail('"+ this.UniqueID + this.IdSeparator + "Email')";
            Page.RegisterRequiresPostBack(this);
            Page.RegisterRequiresControlState(this);
            //Page.RegisterRequiresRaiseEvent(this);
        }


        protected virtual void OnSubmit(LoginEventArgs e) {
            var Submit = this.Submit;
            if (Submit != null) {
                Submit(this, e);
            }
        }

        public void RaisePostBackEvent(string eventArgument) {
            var OldLoginEventArgs = this.Deserialize(eventArgument);
            this.OnSubmit(new LoginEventArgs(OldLoginEventArgs, Email, Password));
        }

        public void RaisePostDataChangedEvent()
        {
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object thisState = new object[] { obj, Email, EmailTitle, Password, PasswordTitle, Layout, ClientScriptGetEmail };
            return thisState;
        }

        protected override void LoadControlState(object state)
        {
            if (state != null)
            {
                object[] thisState = state as object[];
                if (thisState != null)
                {
                    base.LoadControlState(thisState[0]);
                    Email = thisState[1] as string;
                    EmailTitle = thisState[2] as string;
                    Password = thisState[3] as string;
                    PasswordTitle = thisState[4] as string;
                    Layout = (LoginLayout)(thisState[5] ?? 1);
                    ClientScriptGetEmail = thisState[6] as string;
                }
            }
        }

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            Email = postCollection[UniqueID + IdSeparator + "Email"];
            Password = postCollection[UniqueID + IdSeparator + "Password"];
            return false;
        }
        //End submit event

        protected override void Render(HtmlTextWriter writer)
        {
            //ToDo: apply layout
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write(EmailTitle);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();


            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID + this.IdSeparator + "Email");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID + this.IdSeparator + "Email");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, EmailTitle);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Email);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write(PasswordTitle);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Id,  this.IdSeparator + "Password");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID + this.IdSeparator + "Password");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "password");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, PasswordTitle);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Password);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();


            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "Entrar");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID + this.IdSeparator + "Submit");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID + this.IdSeparator + "Submit");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick,
                    Page.ClientScript.GetPostBackEventReference(this, Serialize(new LoginEventArgs(Email, Password))));
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            RegisterClientStartupScript("LoginControlGetEmail", "function LoginControlGetEmail(e) { return document.getElementById(e).value; }");

            base.Render(writer);

        }

        //http://stackoverflow.com/questions/1952817/asp-net-javascript-inside-ajax-updatepanel/1953122#1953122
        private void RegisterClientStartupScript(string scriptKey, string scriptText)
        {
            ScriptManager sManager = ScriptManager.GetCurrent(this.Page);

            if (sManager != null && sManager.IsInAsyncPostBack)
            {
                //if a MS AJAX request, use the Scriptmanager class
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), scriptKey, scriptText, true);
            }
            else
            {
                //if a standard postback, use the standard ClientScript method
                //scriptText = string.Concat("Sys.Application.add_load(function(){", scriptText, "});");
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), scriptKey, scriptText, true);
            }
        }

        internal string Serialize(LoginEventArgs e)
        {
            return (new JavaScriptSerializer()).Serialize(e);
        }

        private LoginEventArgs Deserialize(string eventArgument)
        {
            return (new JavaScriptSerializer()).Deserialize<LoginEventArgs>(eventArgument);
        }

        public enum LoginLayout : byte
        {
            Portrait,
            Landscape
        }

        [Serializable]
        public class LoginEventArgs : EventArgs
        {
            public string OldEmail;
            public string OldPassword;
            public string NewEmail;
            public string NewPassword;

            public LoginEventArgs() { }
            public LoginEventArgs(string OldEmail, string OldPassword)
            {
                this.OldEmail = OldEmail;
                this.OldPassword = OldPassword;
            }
            public LoginEventArgs(LoginEventArgs OldLoginEventArgs, string NewEmail, string NewPassword)
            {
                this.OldEmail = OldLoginEventArgs.OldEmail;
                this.OldPassword = OldLoginEventArgs.OldPassword;
                this.NewEmail = NewEmail;
                this.NewPassword = NewPassword;
            }
        }
    }
}
