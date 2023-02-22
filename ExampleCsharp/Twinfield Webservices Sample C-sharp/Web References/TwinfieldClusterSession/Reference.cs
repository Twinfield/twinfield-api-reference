﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Twinfield_Webservices_Sample_C_sharp.TwinfieldClusterSession {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SessionSoap", Namespace="http://www.twinfield.com/")]
    public partial class Session : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private Header headerValueField;
        
        private System.Threading.SendOrPostCallback LogonOperationCompleted;
        
        private System.Threading.SendOrPostCallback SmsLogonOperationCompleted;
        
        private System.Threading.SendOrPostCallback SmsSendCodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback ChangePasswordOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectCompanyOperationCompleted;
        
        private System.Threading.SendOrPostCallback KeepAliveOperationCompleted;
        
        private System.Threading.SendOrPostCallback AbandonOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetRoleOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Session() {
            this.Url = global::Twinfield_Webservices_Sample_C_sharp.Properties.Settings.Default.Twinfield_Webservices_Sample_C_sharp_TwinfieldClusterSession_Session;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public Header HeaderValue {
            get {
                return this.headerValueField;
            }
            set {
                this.headerValueField = value;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event LogonCompletedEventHandler LogonCompleted;
        
        /// <remarks/>
        public event SmsLogonCompletedEventHandler SmsLogonCompleted;
        
        /// <remarks/>
        public event SmsSendCodeCompletedEventHandler SmsSendCodeCompleted;
        
        /// <remarks/>
        public event ChangePasswordCompletedEventHandler ChangePasswordCompleted;
        
        /// <remarks/>
        public event SelectCompanyCompletedEventHandler SelectCompanyCompleted;
        
        /// <remarks/>
        public event KeepAliveCompletedEventHandler KeepAliveCompleted;
        
        /// <remarks/>
        public event AbandonCompletedEventHandler AbandonCompleted;
        
        /// <remarks/>
        public event GetRoleCompletedEventHandler GetRoleCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("HeaderValue", Direction=System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.twinfield.com/Logon", RequestNamespace="http://www.twinfield.com/", ResponseNamespace="http://www.twinfield.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public LogonResult Logon(string user, string password, string organisation, out LogonAction nextAction, out string cluster) {
            object[] results = this.Invoke("Logon", new object[] {
                        user,
                        password,
                        organisation});
            nextAction = ((LogonAction)(results[1]));
            cluster = ((string)(results[2]));
            return ((LogonResult)(results[0]));
        }
        
        /// <remarks/>
        public void LogonAsync(string user, string password, string organisation) {
            this.LogonAsync(user, password, organisation, null);
        }
        
        /// <remarks/>
        public void LogonAsync(string user, string password, string organisation, object userState) {
            if ((this.LogonOperationCompleted == null)) {
                this.LogonOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogonOperationCompleted);
            }
            this.InvokeAsync("Logon", new object[] {
                        user,
                        password,
                        organisation}, this.LogonOperationCompleted, userState);
        }
        
        private void OnLogonOperationCompleted(object arg) {
            if ((this.LogonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogonCompleted(this, new LogonCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("HeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.twinfield.com/SmsLogon", RequestNamespace="http://www.twinfield.com/", ResponseNamespace="http://www.twinfield.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SMSLogonResult SmsLogon(string smsCode, out LogonAction nextAction) {
            object[] results = this.Invoke("SmsLogon", new object[] {
                        smsCode});
            nextAction = ((LogonAction)(results[1]));
            return ((SMSLogonResult)(results[0]));
        }
        
        /// <remarks/>
        public void SmsLogonAsync(string smsCode) {
            this.SmsLogonAsync(smsCode, null);
        }
        
        /// <remarks/>
        public void SmsLogonAsync(string smsCode, object userState) {
            if ((this.SmsLogonOperationCompleted == null)) {
                this.SmsLogonOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSmsLogonOperationCompleted);
            }
            this.InvokeAsync("SmsLogon", new object[] {
                        smsCode}, this.SmsLogonOperationCompleted, userState);
        }
        
        private void OnSmsLogonOperationCompleted(object arg) {
            if ((this.SmsLogonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SmsLogonCompleted(this, new SmsLogonCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("HeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.twinfield.com/SmsSendCode", RequestNamespace="http://www.twinfield.com/", ResponseNamespace="http://www.twinfield.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SmsSendCode() {
            object[] results = this.Invoke("SmsSendCode", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SmsSendCodeAsync() {
            this.SmsSendCodeAsync(null);
        }
        
        /// <remarks/>
        public void SmsSendCodeAsync(object userState) {
            if ((this.SmsSendCodeOperationCompleted == null)) {
                this.SmsSendCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSmsSendCodeOperationCompleted);
            }
            this.InvokeAsync("SmsSendCode", new object[0], this.SmsSendCodeOperationCompleted, userState);
        }
        
        private void OnSmsSendCodeOperationCompleted(object arg) {
            if ((this.SmsSendCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SmsSendCodeCompleted(this, new SmsSendCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("HeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.twinfield.com/ChangePassword", RequestNamespace="http://www.twinfield.com/", ResponseNamespace="http://www.twinfield.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ChangePasswordResult ChangePassword(string currentPassword, string newPassword, out LogonAction nextAction) {
            object[] results = this.Invoke("ChangePassword", new object[] {
                        currentPassword,
                        newPassword});
            nextAction = ((LogonAction)(results[1]));
            return ((ChangePasswordResult)(results[0]));
        }
        
        /// <remarks/>
        public void ChangePasswordAsync(string currentPassword, string newPassword) {
            this.ChangePasswordAsync(currentPassword, newPassword, null);
        }
        
        /// <remarks/>
        public void ChangePasswordAsync(string currentPassword, string newPassword, object userState) {
            if ((this.ChangePasswordOperationCompleted == null)) {
                this.ChangePasswordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnChangePasswordOperationCompleted);
            }
            this.InvokeAsync("ChangePassword", new object[] {
                        currentPassword,
                        newPassword}, this.ChangePasswordOperationCompleted, userState);
        }
        
        private void OnChangePasswordOperationCompleted(object arg) {
            if ((this.ChangePasswordCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ChangePasswordCompleted(this, new ChangePasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("HeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.twinfield.com/SelectCompany", RequestNamespace="http://www.twinfield.com/", ResponseNamespace="http://www.twinfield.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SelectCompanyResult SelectCompany(string company) {
            object[] results = this.Invoke("SelectCompany", new object[] {
                        company});
            return ((SelectCompanyResult)(results[0]));
        }
        
        /// <remarks/>
        public void SelectCompanyAsync(string company) {
            this.SelectCompanyAsync(company, null);
        }
        
        /// <remarks/>
        public void SelectCompanyAsync(string company, object userState) {
            if ((this.SelectCompanyOperationCompleted == null)) {
                this.SelectCompanyOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectCompanyOperationCompleted);
            }
            this.InvokeAsync("SelectCompany", new object[] {
                        company}, this.SelectCompanyOperationCompleted, userState);
        }
        
        private void OnSelectCompanyOperationCompleted(object arg) {
            if ((this.SelectCompanyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectCompanyCompleted(this, new SelectCompanyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("HeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.twinfield.com/KeepAlive", RequestNamespace="http://www.twinfield.com/", ResponseNamespace="http://www.twinfield.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void KeepAlive() {
            this.Invoke("KeepAlive", new object[0]);
        }
        
        /// <remarks/>
        public void KeepAliveAsync() {
            this.KeepAliveAsync(null);
        }
        
        /// <remarks/>
        public void KeepAliveAsync(object userState) {
            if ((this.KeepAliveOperationCompleted == null)) {
                this.KeepAliveOperationCompleted = new System.Threading.SendOrPostCallback(this.OnKeepAliveOperationCompleted);
            }
            this.InvokeAsync("KeepAlive", new object[0], this.KeepAliveOperationCompleted, userState);
        }
        
        private void OnKeepAliveOperationCompleted(object arg) {
            if ((this.KeepAliveCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.KeepAliveCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("HeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.twinfield.com/Abandon", RequestNamespace="http://www.twinfield.com/", ResponseNamespace="http://www.twinfield.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Abandon() {
            this.Invoke("Abandon", new object[0]);
        }
        
        /// <remarks/>
        public void AbandonAsync() {
            this.AbandonAsync(null);
        }
        
        /// <remarks/>
        public void AbandonAsync(object userState) {
            if ((this.AbandonOperationCompleted == null)) {
                this.AbandonOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAbandonOperationCompleted);
            }
            this.InvokeAsync("Abandon", new object[0], this.AbandonOperationCompleted, userState);
        }
        
        private void OnAbandonOperationCompleted(object arg) {
            if ((this.AbandonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AbandonCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("HeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.twinfield.com/GetRole", RequestNamespace="http://www.twinfield.com/", ResponseNamespace="http://www.twinfield.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetRole() {
            object[] results = this.Invoke("GetRole", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetRoleAsync() {
            this.GetRoleAsync(null);
        }
        
        /// <remarks/>
        public void GetRoleAsync(object userState) {
            if ((this.GetRoleOperationCompleted == null)) {
                this.GetRoleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRoleOperationCompleted);
            }
            this.InvokeAsync("GetRole", new object[0], this.GetRoleOperationCompleted, userState);
        }
        
        private void OnGetRoleOperationCompleted(object arg) {
            if ((this.GetRoleCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRoleCompleted(this, new GetRoleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.twinfield.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.twinfield.com/", IsNullable=false)]
    public partial class Header : System.Web.Services.Protocols.SoapHeader {
        
        private string sessionIDField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string SessionID {
            get {
                return this.sessionIDField;
            }
            set {
                this.sessionIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.twinfield.com/")]
    public enum LogonResult {
        
        /// <remarks/>
        Ok,
        
        /// <remarks/>
        Blocked,
        
        /// <remarks/>
        Untrusted,
        
        /// <remarks/>
        Invalid,
        
        /// <remarks/>
        Deleted,
        
        /// <remarks/>
        Disabled,
        
        /// <remarks/>
        OrganisationInactive,
        
        /// <remarks/>
        ClientInvalid,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.twinfield.com/")]
    public enum LogonAction {
        
        /// <remarks/>
        None,
        
        /// <remarks/>
        SMSLogon,
        
        /// <remarks/>
        ChangePassword,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.twinfield.com/")]
    public enum SMSLogonResult {
        
        /// <remarks/>
        Ok,
        
        /// <remarks/>
        Invalid,
        
        /// <remarks/>
        TimeOut,
        
        /// <remarks/>
        Disabled,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.twinfield.com/")]
    public enum ChangePasswordResult {
        
        /// <remarks/>
        Ok,
        
        /// <remarks/>
        Invalid,
        
        /// <remarks/>
        NotDifferent,
        
        /// <remarks/>
        NotSecure,
        
        /// <remarks/>
        Disabled,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.twinfield.com/")]
    public enum SelectCompanyResult {
        
        /// <remarks/>
        Ok,
        
        /// <remarks/>
        Invalid,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void LogonCompletedEventHandler(object sender, LogonCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LogonCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LogonCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public LogonResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((LogonResult)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public LogonAction nextAction {
            get {
                this.RaiseExceptionIfNecessary();
                return ((LogonAction)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string cluster {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void SmsLogonCompletedEventHandler(object sender, SmsLogonCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SmsLogonCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SmsLogonCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public SMSLogonResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SMSLogonResult)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public LogonAction nextAction {
            get {
                this.RaiseExceptionIfNecessary();
                return ((LogonAction)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void SmsSendCodeCompletedEventHandler(object sender, SmsSendCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SmsSendCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SmsSendCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void ChangePasswordCompletedEventHandler(object sender, ChangePasswordCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ChangePasswordCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ChangePasswordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ChangePasswordResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ChangePasswordResult)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public LogonAction nextAction {
            get {
                this.RaiseExceptionIfNecessary();
                return ((LogonAction)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void SelectCompanyCompletedEventHandler(object sender, SelectCompanyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectCompanyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectCompanyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public SelectCompanyResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SelectCompanyResult)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void KeepAliveCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void AbandonCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetRoleCompletedEventHandler(object sender, GetRoleCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRoleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetRoleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591