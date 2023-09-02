using Microsoft.Reporting.Map.WebForms.BingMaps;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Programacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pprogramacion
{
    public partial class ProcesarArchivo : BasePage
    {
        #region Instancias



        Cturnos turnos = new Cturnos();

        #endregion Instancias

        #region Metodos




        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);


            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }


        private void CargarCombos()
        {

        }

        private void EntidadKey()
        {

        }




        private void Guardar()
        {
            try
            {
                var fileConfigurationSettings = new[]
                {
                new
                {
                    id="campo1",
                    inicio = 0,
                    longitud = 29
                },
                new
                {
                    id="campo2",
                    inicio = 30,
                    longitud = 34
                },
                new
                {
                    id="campo3",
                    inicio = 65,
                    longitud = 16
                },
                new
                {
                    id="campo4",
                    inicio = 82,
                    longitud = 7
                },
                new
                {
                    id="campo5",
                    inicio = 90,
                    longitud = 15
                }
            }.ToList();

                string[] lines = File.ReadAllLines(ViewState["ruta"].ToString());
                var i = 0;
                var valid = true;
                var lineLength = fileConfigurationSettings.Select(setting => setting.longitud).Sum();
                var dt = new DataTable();
                fileConfigurationSettings.ForEach(setting =>
                {
                    dt.Columns.Add(new DataColumn(setting.id));
                });

                lines.ToList().ForEach(line =>
                {
                    if (line.Length < lineLength)
                    {
                        nilblInformacion.Text = "Linea " + i + " inválida";
                        valid = false;
                        return;
                    }
                    var dr = dt.NewRow();
                    var j = 0;
                    fileConfigurationSettings.ForEach(setting =>
                    {
                        dr[setting.id] = line.Substring(setting.inicio, setting.longitud).Trim();
                    });
                    dt.Rows.Add(dr);
                    i++;
                });
                if (!valid)
                    return;

                this.gvLista.DataSource = dt;
                this.gvLista.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar plano. Correspondiente a: " + ex.Message, "C");
            }

        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (this.Session["usuario"] == null)
            //    this.Response.Redirect("~/WebForms/Inicio.aspx");
            //else
            //{
            //    if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) == 0)
            //        ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            //}
        }



        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.formContainer.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            //this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.nilblInformacion.Text = "";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.formContainer.Controls);
            CcontrolesUsuario.LimpiarControles(this.formContainer.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            //this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }



        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            try
            {
                if (fileUpload.HasFile)
                {
                    String ruta = Path.GetTempPath();
                    String file_ext;
                    Session["sw"] = false;
                    file_ext = System.IO.Path.GetExtension(fileUpload.FileName).ToUpper();
                    if (file_ext == ".TXT")
                    {
                        fileUpload.SaveAs(Convert.ToString(ruta + fileUpload.FileName));
                        ViewState["ruta"] = ruta + fileUpload.FileName;
                        Guardar();
                    }
                    else
                        nilblInformacion.Text = "Archivo no valido para cargar";
                }
                else
                {
                    nilblInformacion.Text = "No hay archivo";
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al registrar. Correspondiente a: " + ex.Message, "C");
            }

        }


        protected void nilblRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Novedad.aspx");
        }

        #endregion Eventos


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = null, tipo = null, evento = null;
            int id;
            DateTime fecha;
            nilblInformacion.Text = "";
            CultureInfo culture = new CultureInfo("en-US");

            foreach (GridViewRow r in gvLista.Rows)
            {
                try
                {
                    id = Convert.ToInt32(r.Cells[0].Text);
                }
                catch (Exception ex)
                {
                    nilblInformacion.Text = "Error por tipo de dato";
                    r.Cells[0].BackColor = Color.Red;
                }
                nombre = Convert.ToString(r.Cells[1].Text);
                try
                {
                    fecha = Convert.ToDateTime(r.Cells[2].Text, culture);
                }
                catch (Exception ex)
                {
                    nilblInformacion.Text = "Error por tipo de dato";
                    r.Cells[2].BackColor = Color.Red;
                }
                evento = Convert.ToString(r.Cells[3].Text);
                tipo = Convert.ToString(r.Cells[4].Text);
            }

            foreach (GridViewRow r in gvLista.Rows)
            {
                id = Convert.ToInt32(r.Cells[0].Text);
                nombre = Convert.ToString(r.Cells[1].Text);
                fecha = Convert.ToDateTime(r.Cells[2].Text, culture);
                evento = Convert.ToString(r.Cells[3].Text);
                tipo = Convert.ToString(r.Cells[4].Text);
            }
        }
    }
}