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
        public event LoginEventHandler EmailChanged;

        private string _Email;
        private string _Password;
        private LoginLayout _Layout;
        private string _EmailTitle;
        private string _PasswordTitle;

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

        //Submit event
        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
            Page.RegisterRequiresPostBack(this);
            Page.RegisterRequiresControlState(this);
        }


        protected virtual void OnSubmit(LoginEventArgs e) {
            var Submit = this.Submit;
            if (Submit != null) {
                Submit(this, e);
            }
        }

        protected virtual void OnEmailChanged(LoginEventArgs e)
        {
            var EmailChanged = this.EmailChanged;
            if (EmailChanged != null)
            {
                EmailChanged(this, e);
            }
        }

        public void RaisePostBackEvent(string eventArgument) {
            OnSubmit(new LoginEventArgs(Email, Password));
        }

        public void RaisePostDataChangedEvent()
        {
            OnEmailChanged(new LoginEventArgs(Email, Password));
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object thisState = new object[] { obj, this.Email, this.EmailTitle, this.Password, this.PasswordTitle, this.Layout };
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
                }
            }
        }
        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            Email = postCollection[UniqueID + IdSeparator + "Email"];
            Password = postCollection[UniqueID + IdSeparator + "Password"];
            Page.RegisterRequiresRaiseEvent(this);
            return false;
        }
        //End submit event

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        //ToDo: apply layout

        //ToDo: build label for Email
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write(EmailTitle);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();


            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID + this.IdSeparator + "Email");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID + this.IdSeparator + "Email");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "Text");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, EmailTitle);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Email);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();


            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID + this.IdSeparator + "Password");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID + this.IdSeparator + "Password");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "Text");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, PasswordTitle);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Password);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();


            writer.AddAttribute(HtmlTextWriterAttribute.Type, "submit");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "Entrar");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            //writer.AddAttribute(HtmlTextWriterAttribute.Onclick,
            //        Page.ClientScript.GetPostBackEventReference(this, Serialize(new LoginEventArgs(Email, Password))));
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        //internal string Serialize(LoginEventArgs e)
        //{
        //    return (new JavaScriptSerializer()).Serialize(e);
        //}

        //private EventArgs Deserialize(string eventArgument)
        //{
        //    return (new JavaScriptSerializer()).Deserialize<LoginEventArgs>(eventArgument);
        //}

        public enum LoginLayout : byte
        {
            Portrait,
            Landscape
        }

        [Serializable]
        public class LoginEventArgs : EventArgs
        {
            public string Email;
            public string Password;

            public LoginEventArgs() { }
            public LoginEventArgs(string Email, string Password)
            {
                this.Email = Email;
                this.Password = Password;
            }
        }
    }
}
