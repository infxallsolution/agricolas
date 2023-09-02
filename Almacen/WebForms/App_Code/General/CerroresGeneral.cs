using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Almacen.WebForms.App_Code.General
{
    public class CerroresGeneral
    {

        public CerroresGeneral()
        {


        }

        static string limpiarMensaje(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        public static dynamic ObtenerErroresGeneral(string errores, string[] args = null)
        {

            var nivel = "error";
            var mensaje = "Se ha presentado un error general en el sistema. Por favor, contacte con el administrador.";
            var dataSet = CentidadMetodos.EntidadGet(
                    "sErrores",
                    "ppa");
            var dataTable = dataSet.Tables[0];
            foreach (DataRow row in dataTable.Rows)
            {
                var errorTemplate = new Regex((string)row["plantillaError"]);
                var traduccionTemplate = (string)row["plantillaTraduccion"];
                if (errorTemplate.IsMatch(errores))
                {
                    Match m = errorTemplate.Match(errores);
                    if (args != null)
                        mensaje = string.Format(errorTemplate.Replace(m.Value, traduccionTemplate), args);
                    else
                        mensaje = errorTemplate.Replace(m.Value, traduccionTemplate);
                    nivel = (string)row["severidad"];
                }
            }
            return new
            {
                mensaje = mensaje,
                nivel = nivel
            };
        }


        public static void ManejoErrorCatch(Control control, Type type, Exception exepcion, string[] args = null)
        {
            string errorOriginal = exepcion.ToString();
            //Elmah.ErrorSignal.FromCurrentContext().Raise(exepcion);
            var nivel = "error";
            var mensaje = "Se ha presentado un error general en el sistema. Por favor, contacte con el administrador.";
            var errores = new
            {
                mensaje = limpiarMensaje(mensaje),
                nivel = nivel
            };
            try
            {
                errores = ObtenerErroresGeneral(errorOriginal, args);
            }
            catch (Exception e)
            {
                errorOriginal = errorOriginal + "\n\n\nError Adicional Buscar Mensajes: " + e.ToString();
                //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            StringBuilder cstext2 = new StringBuilder();
            cstext2.Append(@"
            function clientScriptError(title, text, type) {
                $(document).ready(function () {
                    swal({
                        title: title,
                        html: text,
                        type: type,
                        confirmButtonText: ""Aceptar"",
                        showCancelButton: true,
			            cancelButtonText: 'Mostrar detalle <i class=""fa fa-angle-right""></i>'
                    }).then(
			            function () {}, 
			            function (dismiss) {
				            if (dismiss === 'cancel') {
					            swal({
						            title: ""<small>Detalle de la excepción </small>"",
                                    html: ""<textarea disabled style='width:100%; height:300px;max-height:299px;resize:none;font-size:initial;font-family:monospace;'>" + errorOriginal.Replace(Environment.NewLine, "\\r\\n").Replace("\n", "\\n").Replace("\"", "\\\"") + @"</textarea>"",
						            confirmButtonText: ""Aceptar"",
						            animation: false,
                                });
				            }
			            });
                });
            }
            clientScriptError(""Error"",""" + errores.mensaje + @""",""" + errores.nivel + @""");
        ");

            //clientScript.RegisterStartupScript(type, Guid.NewGuid().ToString(), cstext2.ToString(), true);

            ScriptManager.RegisterStartupScript(control, type, Guid.NewGuid().ToString(), cstext2.ToString(), true);
        }

        public static void ManejoError(Control upPanel, Type type, string mensaje, string nivel, string[] args = null)
        {
            mensaje = limpiarMensaje(mensaje);

            var errores = new
            {
                mensaje = mensaje,
                nivel = nivel
            };

            string titulo = "";

            if (nivel == "e" || nivel == "error")
            {
                titulo = "Error";
                nivel = "error";
            }

            if (nivel == "i" || nivel == "info")
            {
                titulo = "Información";
                nivel = "info";
            }

            if (nivel == "e" || nivel == "warning")
            {
                titulo = "Advertencia";
                nivel = "warning";
            }




            StringBuilder cstext2 = new StringBuilder();
            cstext2.Append(@"
            function clientScriptError(title, text, type) {
                $(document).ready(function () {
                    swal({
                        title: title,
                        html: text,
                        type: type,
                        confirmButtonText: ""Aceptar"",
                        showCancelButton: true,
			            cancelButtonText: 'Mostrar detalle <i class=""fa fa-angle-right""></i>'
                    }).then(
			            function () {}, 
			            function (dismiss) {
				            if (dismiss === 'cancel') {
					            swal({
						            title: ""<small>Detalle de la excepción </small>"",
                                    html: ""<textarea disabled style='width:100%; height:300px;max-height:299px;resize:none;font-size:initial;font-family:monospace;'>" + mensaje.Replace("\n", "\\n").Replace(Environment.NewLine, "\\r\\n").Replace("\"", "\\\"") + @"</textarea>"",
						            confirmButtonText: ""Aceptar"",
						            animation: false,
                                });
				            }
			            });
                });
            }
            clientScriptError(""" + titulo + @""",""" + errores.mensaje + @""",""" + errores.nivel + @""");
        ");
            //  clientScript.RegisterStartupScript(type, Guid.NewGuid().ToString(), cstext2.ToString(), true);

            ScriptManager.RegisterStartupScript(upPanel, type, Guid.NewGuid().ToString(), cstext2.ToString(), true);
        }
    }
}