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

namespace Almacen.seguridadInfos {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SecuritySoap", Namespace="http://tempuri.org/Security/Security.asmx")]
    public partial class Security : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ValidarUsuarioOperationCompleted;
        
        private System.Threading.SendOrPostCallback InsertaLogOperationCompleted;
        
        private System.Threading.SendOrPostCallback VerificaAccesoPaginaOperationCompleted;
        
        private System.Threading.SendOrPostCallback VerificaAccesoOperacionOperationCompleted;
        
        private System.Threading.SendOrPostCallback RetornaEmpresaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Security() {
            this.Url = global::Almacen.Properties.Settings.Default.Almacen_seguridadInfos_Security;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
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
        public event ValidarUsuarioCompletedEventHandler ValidarUsuarioCompleted;
        
        /// <remarks/>
        public event InsertaLogCompletedEventHandler InsertaLogCompleted;
        
        /// <remarks/>
        public event VerificaAccesoPaginaCompletedEventHandler VerificaAccesoPaginaCompleted;
        
        /// <remarks/>
        public event VerificaAccesoOperacionCompletedEventHandler VerificaAccesoOperacionCompleted;
        
        /// <remarks/>
        public event RetornaEmpresaCompletedEventHandler RetornaEmpresaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Security/Security.asmx/ValidarUsuario", RequestNamespace="http://tempuri.org/Security/Security.asmx", ResponseNamespace="http://tempuri.org/Security/Security.asmx", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int ValidarUsuario(string usuario, string idSys, string sitio) {
            object[] results = this.Invoke("ValidarUsuario", new object[] {
                        usuario,
                        idSys,
                        sitio});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void ValidarUsuarioAsync(string usuario, string idSys, string sitio) {
            this.ValidarUsuarioAsync(usuario, idSys, sitio, null);
        }
        
        /// <remarks/>
        public void ValidarUsuarioAsync(string usuario, string idSys, string sitio, object userState) {
            if ((this.ValidarUsuarioOperationCompleted == null)) {
                this.ValidarUsuarioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnValidarUsuarioOperationCompleted);
            }
            this.InvokeAsync("ValidarUsuario", new object[] {
                        usuario,
                        idSys,
                        sitio}, this.ValidarUsuarioOperationCompleted, userState);
        }
        
        private void OnValidarUsuarioOperationCompleted(object arg) {
            if ((this.ValidarUsuarioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ValidarUsuarioCompleted(this, new ValidarUsuarioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Security/Security.asmx/InsertaLog", RequestNamespace="http://tempuri.org/Security/Security.asmx", ResponseNamespace="http://tempuri.org/Security/Security.asmx", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void InsertaLog(string usuario, string operacion, string entidad, string estado, string mensaje, string ip, int empresa) {
            this.Invoke("InsertaLog", new object[] {
                        usuario,
                        operacion,
                        entidad,
                        estado,
                        mensaje,
                        ip,
                        empresa});
        }
        
        /// <remarks/>
        public void InsertaLogAsync(string usuario, string operacion, string entidad, string estado, string mensaje, string ip, int empresa) {
            this.InsertaLogAsync(usuario, operacion, entidad, estado, mensaje, ip, empresa, null);
        }
        
        /// <remarks/>
        public void InsertaLogAsync(string usuario, string operacion, string entidad, string estado, string mensaje, string ip, int empresa, object userState) {
            if ((this.InsertaLogOperationCompleted == null)) {
                this.InsertaLogOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsertaLogOperationCompleted);
            }
            this.InvokeAsync("InsertaLog", new object[] {
                        usuario,
                        operacion,
                        entidad,
                        estado,
                        mensaje,
                        ip,
                        empresa}, this.InsertaLogOperationCompleted, userState);
        }
        
        private void OnInsertaLogOperationCompleted(object arg) {
            if ((this.InsertaLogCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsertaLogCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Security/Security.asmx/VerificaAccesoPagina", RequestNamespace="http://tempuri.org/Security/Security.asmx", ResponseNamespace="http://tempuri.org/Security/Security.asmx", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int VerificaAccesoPagina(string usuario, string sitio, string pagina, int empresa) {
            object[] results = this.Invoke("VerificaAccesoPagina", new object[] {
                        usuario,
                        sitio,
                        pagina,
                        empresa});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void VerificaAccesoPaginaAsync(string usuario, string sitio, string pagina, int empresa) {
            this.VerificaAccesoPaginaAsync(usuario, sitio, pagina, empresa, null);
        }
        
        /// <remarks/>
        public void VerificaAccesoPaginaAsync(string usuario, string sitio, string pagina, int empresa, object userState) {
            if ((this.VerificaAccesoPaginaOperationCompleted == null)) {
                this.VerificaAccesoPaginaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnVerificaAccesoPaginaOperationCompleted);
            }
            this.InvokeAsync("VerificaAccesoPagina", new object[] {
                        usuario,
                        sitio,
                        pagina,
                        empresa}, this.VerificaAccesoPaginaOperationCompleted, userState);
        }
        
        private void OnVerificaAccesoPaginaOperationCompleted(object arg) {
            if ((this.VerificaAccesoPaginaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.VerificaAccesoPaginaCompleted(this, new VerificaAccesoPaginaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Security/Security.asmx/VerificaAccesoOperacion", RequestNamespace="http://tempuri.org/Security/Security.asmx", ResponseNamespace="http://tempuri.org/Security/Security.asmx", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int VerificaAccesoOperacion(string usuario, string sitio, string pagina, string operacion, int empresa) {
            object[] results = this.Invoke("VerificaAccesoOperacion", new object[] {
                        usuario,
                        sitio,
                        pagina,
                        operacion,
                        empresa});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void VerificaAccesoOperacionAsync(string usuario, string sitio, string pagina, string operacion, int empresa) {
            this.VerificaAccesoOperacionAsync(usuario, sitio, pagina, operacion, empresa, null);
        }
        
        /// <remarks/>
        public void VerificaAccesoOperacionAsync(string usuario, string sitio, string pagina, string operacion, int empresa, object userState) {
            if ((this.VerificaAccesoOperacionOperationCompleted == null)) {
                this.VerificaAccesoOperacionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnVerificaAccesoOperacionOperationCompleted);
            }
            this.InvokeAsync("VerificaAccesoOperacion", new object[] {
                        usuario,
                        sitio,
                        pagina,
                        operacion,
                        empresa}, this.VerificaAccesoOperacionOperationCompleted, userState);
        }
        
        private void OnVerificaAccesoOperacionOperationCompleted(object arg) {
            if ((this.VerificaAccesoOperacionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.VerificaAccesoOperacionCompleted(this, new VerificaAccesoOperacionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Security/Security.asmx/RetornaEmpresa", RequestNamespace="http://tempuri.org/Security/Security.asmx", ResponseNamespace="http://tempuri.org/Security/Security.asmx", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int RetornaEmpresa(string usuario) {
            object[] results = this.Invoke("RetornaEmpresa", new object[] {
                        usuario});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void RetornaEmpresaAsync(string usuario) {
            this.RetornaEmpresaAsync(usuario, null);
        }
        
        /// <remarks/>
        public void RetornaEmpresaAsync(string usuario, object userState) {
            if ((this.RetornaEmpresaOperationCompleted == null)) {
                this.RetornaEmpresaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRetornaEmpresaOperationCompleted);
            }
            this.InvokeAsync("RetornaEmpresa", new object[] {
                        usuario}, this.RetornaEmpresaOperationCompleted, userState);
        }
        
        private void OnRetornaEmpresaOperationCompleted(object arg) {
            if ((this.RetornaEmpresaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RetornaEmpresaCompleted(this, new RetornaEmpresaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void ValidarUsuarioCompletedEventHandler(object sender, ValidarUsuarioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ValidarUsuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ValidarUsuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void InsertaLogCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void VerificaAccesoPaginaCompletedEventHandler(object sender, VerificaAccesoPaginaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VerificaAccesoPaginaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal VerificaAccesoPaginaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void VerificaAccesoOperacionCompletedEventHandler(object sender, VerificaAccesoOperacionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VerificaAccesoOperacionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal VerificaAccesoOperacionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void RetornaEmpresaCompletedEventHandler(object sender, RetornaEmpresaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RetornaEmpresaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RetornaEmpresaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591