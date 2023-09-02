using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class TipoNovedadSS : BasePage
    {
        #region Instancias



        CtipoNovedadSS tipoIncapacidad = new CtipoNovedadSS();



        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = tipoIncapacidad.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente apublic partial class Nomina_Padministracion_TipoIncapacidad : BasePage\r\n{\r\n    #region Instancias\r\n\r\n\r\n    \r\n    CtipoNovedadSS tipoIncapacidad = new CtipoNovedadSS();\r\n    \r\n\r\n\r\n    #endregion Instancias\r\n\r\n    #region Metodos\r\n\r\n    private void GetEntidad()\r\n    {\r\n        try\r\n        {\r\n            if (seguridad.VerificaAccesoOperacion(this.Session[\"usuario\"].ToString(), ConfigurationManager.AppSettings[\"Modulo\"].ToString(), nombrePaginaActual(), \"C\", Convert.ToInt16(Session[\"empresa\"])) == 0)\r\n            {\r\n                ManejoError(\"Usuario no autorizado para ejecutar esta operación\", \"C\");\r\n                return;\r\n            }\r\n\r\n            this.gvLista.DataSource = tipoIncapacidad.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session[\"empresa\"]));\r\n            this.gvLista.DataBind();\r\n\r\n            this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + \" Registros encontrados\";\r\n\r\n            seguridad.InsertaLog(this.Session[\"usuario\"].ToString(), \"C\", ConfigurationManager.AppSettings[\"Modulo\"].ToString() + '-' + this.Page.ToString(), \"ex\",\r\n                this.gvLista.Rows.Count.ToString() + \" Registros encontrados\", ObtenerIP(), Convert.ToInt16(Session[\"empresa\"]));\r\n        }\r\n        catch (Exception ex)\r\n        {\r\n            ManejoError(\"Error al cargar la tabla correspondiente a: \" + ex.Message, \"C\");\r\n        }\r\n    }\r\n   \r\n    private void ManejoExito(string mensaje, string operacion)\r\n    {\r\n        CerroresGeneral.ManejoError(this, GetType(),mensaje, \"info\");\r\n\r\n        CcontrolesUsuario.InhabilitarControles(this.Page.Controls);\r\n        CcontrolesUsuario.LimpiarControles(this.Page.Controls);\r\n        this.nilbNuevo.Visible = true;\r\n\r\n        seguridad.InsertaLog(this.Session[\"usuario\"].ToString(), operacion, ConfigurationManager.AppSettings[\"Modulo\"].ToString() + '-' + this.Page.ToString(), \"ex\",\r\n            mensaje, ObtenerIP(), Convert.ToInt16(Session[\"empresa\"]));\r\n\r\n        GetEntidad();\r\n    }\r\n\r\n    private void Guardar()\r\n    {\r\n        string operacion = \"inserta\";\r\n        decimal pArp = 0, pCaja = 0, pSalud = 0, pPension = 0, pICBF = 0, pSolidaridad = 0, pSena = 0;\r\n        try\r\n        {\r\n            if (Convert.ToBoolean(this.Session[\"editar\"]) == true)\r\n                operacion = \"actualiza\";\r\n\r\n            if (chkARP.Checked)\r\n                pArp = Convert.ToDecimal(txvPorcentajeARP.Text);\r\n            if (chkCaja.Checked)\r\n                pCaja = Convert.ToDecimal(txvPorcentajeCaja.Text);\r\n            if (chkFondoSolidaridad.Checked)\r\n                pSolidaridad = Convert.ToDecimal(txvPorcentajeFondoSolidaridad.Text);\r\n            if (chkICBF.Checked)\r\n                pICBF = Convert.ToDecimal(txvPorcentajeICBF.Text);\r\n            if (chkPension.Checked)\r\n                pPension = Convert.ToDecimal(txvPorcentajePension.Text);\r\n            if (chkSalud.Checked)\r\n                pSalud = Convert.ToDecimal(txvPorcentajeSalud.Text);\r\n            if (chkSena.Checked)\r\n                pSena = Convert.ToDecimal(txvPorcentajeSena.Text);\r\n\r\n            object[] objValores = new object[]{\r\n                   chkARP.Checked,\r\n                   chkCaja.Checked,\r\n                   Convert.ToInt16(Session[\"empresa\"]),\r\n                   chkICBF.Checked,\r\n                   pArp,\r\n                   pCaja,\r\n                   chkPension.Checked,\r\n                   pICBF,\r\n                   pPension,\r\n                   pSalud,\r\n                   pSena,\r\n                   pSolidaridad,\r\n                   chkSalud.Checked,\r\n                   chkSena.Checked,\r\n                   chkFondoSolidaridad.Checked,\r\n                   ddlTipoNovedad.SelectedValue\r\n                };\r\n\r\n            switch (CentidadMetodos.EntidadInsertUpdateDelete(\"nParametroNovedadSeguridadSocial\", operacion, \"ppa\", objValores))\r\n            {\r\n                case 0:\r\n                    ManejoExito(\"Datos insertados satisfactoriamente\", operacion.Substring(0, 1).ToUpper());\r\n                    break;\r\n                case 1:\r\n                    ManejoError(\"Errores al insertar el registro. Operación no realizada\", operacion.Substring(0, 1).ToUpper());\r\n                    break;\r\n            }\r\n        }\r\n        catch (Exception ex)\r\n        {\r\n            ManejoError(\"Error al guardar los datos correspondiente a: \" + ex.Message, operacion.Substring(0, 1).ToUpper());\r\n        }\r\n    }\r\n\r\n\r\n    #endregion Metodos\r\n\r\n    #region Eventos\r\n\r\n    protected void Page_Load(object sender, EventArgs e)\r\n    {\r\n        if (this.Session[\"usuario\"] == null)\r\n            this.Response.Redirect(\"~/WebForms/Inicio.aspx\");\r\n        else\r\n        {\r\n            if (seguridad.VerificaAccesoPagina(this.Session[\"usuario\"].ToString(),\r\n                ConfigurationManager.AppSettings[\"Modulo\"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session[\"empresa\"])) != 0)\r\n                this.ddlTipoNovedad.Focus();\r\n            else\r\n                ManejoError(\"Usuario no autorizado para ingresar a esta página\", \"IN\");\r\n        }\r\n    }\r\n    protected void lbNuevo_Click(object sender, EventArgs e)\r\n    {\r\n        if (seguridad.VerificaAccesoOperacion(this.Session[\"usuario\"].ToString(), ConfigurationManager.AppSettings[\"Modulo\"].ToString(),\r\n                              nombrePaginaActual(), \"I\", Convert.ToInt16(Session[\"empresa\"])) == 0)\r\n        {\r\n            ManejoError(\"Usuario no autorizado para ejecutar esta operación\", \"C\");\r\n            return;\r\n        }\r\n\r\n        CcontrolesUsuario.HabilitarControles(this.Page.Controls);\r\n        CcontrolesUsuario.LimpiarControles(Page.Controls);\r\n\r\n        this.nilbNuevo.Visible = false;\r\n        this.Session[\"editar\"] = false;\r\n\r\n        ddlTipoNovedad.Focus();\r\n        this.nilblInformacion.Text = \"\";\r\n    }\r\n    protected void lbCancelar_Click(object sender, EventArgs e)\r\n    {\r\n        CcontrolesUsuario.InhabilitarControles(this.Page.Controls);\r\n        CcontrolesUsuario.LimpiarControles(this.Page.Controls);\r\n\r\n        this.gvLista.DataSource = null;\r\n        this.gvLista.DataBind();\r\n\r\n        this.nilbNuevo.Visible = true;\r\n        this.nilblInformacion.Text = \"\";\r\n    }\r\n\r\n    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)\r\n    {\r\n        try\r\n        {\r\n            if (seguridad.VerificaAccesoOperacion(this.Session[\"usuario\"].ToString(), ConfigurationManager.AppSettings[\"Modulo\"].ToString(),\r\n                                     nombrePaginaActual(), \"E\", Convert.ToInt16(Session[\"empresa\"])) == 0)\r\n            {\r\n                ManejoError(\"Usuario no autorizado para ejecutar esta operación\", \"C\");\r\n                return;\r\n            }\r\n\r\n            object[] objValores = new object[] { Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)), Convert.ToInt16(Session[\"empresa\"]) };\r\n\r\n            switch (CentidadMetodos.EntidadInsertUpdateDelete(\"nParametroNovedadSeguridadSocial\", \"elimina\", \"ppa\", objValores))\r\n            {\r\n                case 0:\r\n                    ManejoExito(\"Registro eliminado satisfactoriamente\", \"E\");\r\n                    break;\r\n                case 1:\r\n                    ManejoError(\"Error al eliminar el registro. Operación no realizada\", \"E\");\r\n                    break;\r\n            }\r\n        }\r\n        catch (Exception ex)\r\n        {\r\n            if (ex.HResult.ToString() == \"-2146233087\")\r\n            {\r\n                ManejoError(\"El código ('\" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +\r\n                \"' - ' \" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + \"') tiene una asociación, no es posible eliminar el registro.\", \"E\");\r\n            }\r\n            else\r\n                ManejoError(\"Error al eliminar el registro. Correspondiente a: \" + ex.Message, \"E\");\r\n        }\r\n    }\r\n    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)\r\n    {\r\n        gvLista.PageIndex = e.NewPageIndex;\r\n        GetEntidad();\r\n        gvLista.DataBind();\r\n    }\r\n    protected void lbRegistrar_Click(object sender, EventArgs e)\r\n    {\r\n        if (chkARP.Checked)\r\n        {\r\n            if (Convert.ToDecimal(txvPorcentajeARP.Text) == 0)\r\n            {\r\n                this.nilblInformacion.Text = \"Valore debe ser mayor a cero(0)\";\r\n                return;\r\n            }\r\n        }\r\n        if (chkCaja.Checked)\r\n        {\r\n            if (Convert.ToDecimal(txvPorcentajeCaja.Text) == 0)\r\n            {\r\n                this.nilblInformacion.Text = \"Valore debe ser mayor a cero(0)\";\r\n                return;\r\n            }\r\n        }\r\n        if (chkFondoSolidaridad.Checked)\r\n        {\r\n            if (Convert.ToDecimal(txvPorcentajeFondoSolidaridad.Text) == 0)\r\n            {\r\n                this.nilblInformacion.Text = \"Valore debe ser mayor a cero(0)\";\r\n                return;\r\n            }\r\n        }\r\n        if (chkICBF.Checked)\r\n        {\r\n            if (Convert.ToDecimal(txvPorcentajeICBF.Text) == 0)\r\n            {\r\n                this.nilblInformacion.Text = \"Valore debe ser mayor a cero(0)\";\r\n                return;\r\n            }\r\n        }\r\n        if (chkPension.Checked)\r\n        {\r\n            if (Convert.ToDecimal(txvPorcentajePension.Text) == 0)\r\n            {\r\n                this.nilblInformacion.Text = \"Valore debe ser mayor a cero(0)\";\r\n                return;\r\n            }\r\n        }\r\n        if (chkSalud.Checked)\r\n        {\r\n            if (Convert.ToDecimal(txvPorcentajeSalud.Text) == 0)\r\n            {\r\n                this.nilblInformacion.Text = \"Valore debe ser mayor a cero(0)\";\r\n                return;\r\n            }\r\n        }\r\n        if (chkSena.Checked)\r\n        {\r\n            if (Convert.ToDecimal(txvPorcentajeSena.Text) == 0)\r\n            {\r\n                this.nilblInformacion.Text = \"Valore debe ser mayor a cero(0)\";\r\n                return;\r\n            }\r\n        }\r\n\r\n        Guardar();\r\n    }\r\n\r\n    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)\r\n    {\r\n        CcontrolesUsuario.HabilitarControles(this.Page.Controls);\r\n\r\n        this.nilbNuevo.Visible = false;\r\n        this.Session[\"editar\"] = true;\r\n\r\n        this.ddlTipoNovedad.Enabled = false;\r\n\r\n        try\r\n        {\r\n            if (this.gvLista.SelectedRow.Cells[2].Text != \"&nbsp;\")\r\n                this.ddlTipoNovedad.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);\r\n\r\n            foreach (Control objControl in this.gvLista.SelectedRow.Cells[3].Controls)\r\n            {\r\n                if (objControl is CheckBox)\r\n                    this.chkSalud.Checked = ((CheckBox)objControl).Checked;\r\n            }\r\n\r\n            if (this.gvLista.SelectedRow.Cells[4].Text != \"&nbsp;\")\r\n                this.txvPorcentajeSalud.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);\r\n            else\r\n                this.txvPorcentajeSalud.Text = \"0\";\r\n\r\n            foreach (Control objControl in this.gvLista.SelectedRow.Cells[5].Controls)\r\n            {\r\n                if (objControl is CheckBox)\r\n                    this.chkPension.Checked = ((CheckBox)objControl).Checked;\r\n            }\r\n\r\n            if (this.gvLista.SelectedRow.Cells[6].Text != \"&nbsp;\")\r\n                this.txvPorcentajePension.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);\r\n            else\r\n                this.txvPorcentajePension.Text = \"0\";\r\n\r\n            foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)\r\n            {\r\n                if (objControl is CheckBox)\r\n                    this.chkFondoSolidaridad.Checked = ((CheckBox)objControl).Checked;\r\n            }\r\n\r\n            if (this.gvLista.SelectedRow.Cells[8].Text != \"&nbsp;\")\r\n                this.txvPorcentajeFondoSolidaridad.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);\r\n            else\r\n                this.txvPorcentajeFondoSolidaridad.Text = \"0\";\r\n\r\n            foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)\r\n            {\r\n                if (objControl is CheckBox)\r\n                    this.chkARP.Checked = ((CheckBox)objControl).Checked;\r\n            }\r\n\r\n            if (this.gvLista.SelectedRow.Cells[10].Text != \"&nbsp;\")\r\n                this.txvPorcentajeARP.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[10].Text);\r\n            else\r\n                this.txvPorcentajeARP.Text = \"0\";\r\n\r\n            foreach (Control objControl in this.gvLista.SelectedRow.Cells[11].Controls)\r\n            {\r\n                if (objControl is CheckBox)\r\n                    this.chkCaja.Checked = ((CheckBox)objControl).Checked;\r\n            }\r\n\r\n            if (this.gvLista.SelectedRow.Cells[12].Text != \"&nbsp;\")\r\n                this.txvPorcentajeCaja.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[12].Text);\r\n            else\r\n                this.txvPorcentajeCaja.Text = \"0\";\r\n\r\n            foreach (Control objControl in this.gvLista.SelectedRow.Cells[13].Controls)\r\n            {\r\n                if (objControl is CheckBox)\r\n                    this.chkSena.Checked = ((CheckBox)objControl).Checked;\r\n            }\r\n\r\n            if (this.gvLista.SelectedRow.Cells[14].Text != \"&nbsp;\")\r\n                this.txvPorcentajeSena.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[14].Text);\r\n            else\r\n                this.txvPorcentajeSena.Text = \"0\";\r\n\r\n            foreach (Control objControl in this.gvLista.SelectedRow.Cells[15].Controls)\r\n            {\r\n                if (objControl is CheckBox)\r\n                    this.chkICBF.Checked = ((CheckBox)objControl).Checked;\r\n            }\r\n\r\n            if (this.gvLista.SelectedRow.Cells[16].Text != \"&nbsp;\")\r\n                this.txvPorcentajeICBF.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[16].Text);\r\n            else\r\n                this.txvPorcentajeICBF.Text = \"0\";\r\n\r\n\r\n        }\r\n        catch (Exception ex)\r\n        {\r\n            ManejoError(\"Error al cargar los campos correspondiente a: \" + ex.Message, \"C\");\r\n        }\r\n    }\r\n\r\n    protected void niimbBuscar_Click(object sender, EventArgs e)\r\n    {\r\n        CcontrolesUsuario.InhabilitarControles(this.Page.Controls);\r\n        this.nilbNuevo.Visible = true;\r\n        GetEntidad();\r\n    }\r\n\r\n  \r\n  \r\n\r\n    protected void chkSalud_CheckedChanged(object sender, EventArgs e)\r\n    {\r\n        if (chkSalud.Checked)\r\n            txvPorcentajeSalud.Enabled = true;\r\n        else\r\n        {\r\n            txvPorcentajeSalud.Enabled = false;\r\n            txvPorcentajeSalud.Text = \"0\";\r\n        }\r\n    }\r\n    protected void chkPension_CheckedChanged(object sender, EventArgs e)\r\n    {\r\n        if (chkPension.Checked)\r\n            txvPorcentajePension.Enabled = true;\r\n        else\r\n        {\r\n            txvPorcentajePension.Enabled = false;\r\n            txvPorcentajePension.Text = \"0\";\r\n        }\r\n    }\r\n    protected void chkFondoSolidaridad_CheckedChanged(object sender, EventArgs e)\r\n    {\r\n        if (chkFondoSolidaridad.Checked)\r\n            txvPorcentajeFondoSolidaridad.Enabled = true;\r\n        else\r\n        {\r\n            txvPorcentajeFondoSolidaridad.Enabled = false;\r\n            txvPorcentajeFondoSolidaridad.Text = \"0\";\r\n        }\r\n\r\n    }\r\n    protected void chkARP_CheckedChanged(object sender, EventArgs e)\r\n    {\r\n        if (chkARP.Checked)\r\n            txvPorcentajeARP.Enabled = true;\r\n        else\r\n        {\r\n            txvPorcentajeARP.Enabled = false;\r\n            txvPorcentajeARP.Text = \"0\";\r\n        }\r\n    }\r\n    protected void chkCaja_CheckedChanged(object sender, EventArgs e)\r\n    {\r\n        if (chkCaja.Checked)\r\n            txvPorcentajeCaja.Enabled = true;\r\n        else\r\n        {\r\n            txvPorcentajeCaja.Enabled = false;\r\n            txvPorcentajeCaja.Text = \"0\";\r\n        }\r\n    }\r\n    protected void chkSena_CheckedChanged(object sender, EventArgs e)\r\n    {\r\n        if (chkSena.Checked)\r\n            txvPorcentajeSena.Enabled = true;\r\n        else\r\n        {\r\n            txvPorcentajeSena.Enabled = false;\r\n            txvPorcentajeSena.Text = \"0\";\r\n        }\r\n    }\r\n    protected void chkICBF_CheckedChanged(object sender, EventArgs e)\r\n    {\r\n        if (chkICBF.Checked)\r\n            txvPorcentajeICBF.Enabled = true;\r\n        else\r\n        {\r\n            txvPorcentajeICBF.Enabled = false;\r\n            txvPorcentajeICBF.Text = \"0\";\r\n        }\r\n    }\r\n\r\n    #endregion Eventos\r\n}: " + ex.Message, "C");
            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }

        private void Guardar()
        {
            string operacion = "inserta";
            decimal pArp = 0, pCaja = 0, pSalud = 0, pPension = 0, pICBF = 0, pSolidaridad = 0, pSena = 0;
            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                if (chkARP.Checked)
                    pArp = Convert.ToDecimal(txvPorcentajeARP.Text);
                if (chkCaja.Checked)
                    pCaja = Convert.ToDecimal(txvPorcentajeCaja.Text);
                if (chkFondoSolidaridad.Checked)
                    pSolidaridad = Convert.ToDecimal(txvPorcentajeFondoSolidaridad.Text);
                if (chkICBF.Checked)
                    pICBF = Convert.ToDecimal(txvPorcentajeICBF.Text);
                if (chkPension.Checked)
                    pPension = Convert.ToDecimal(txvPorcentajePension.Text);
                if (chkSalud.Checked)
                    pSalud = Convert.ToDecimal(txvPorcentajeSalud.Text);
                if (chkSena.Checked)
                    pSena = Convert.ToDecimal(txvPorcentajeSena.Text);

                object[] objValores = new object[]{
                   chkARP.Checked,
                   chkCaja.Checked,
                   Convert.ToInt16(Session["empresa"]),
                   chkICBF.Checked,
                   pArp,
                   pCaja,
                   chkPension.Checked,
                   pICBF,
                   pPension,
                   pSalud,
                   pSena,
                   pSolidaridad,
                   chkSalud.Checked,
                   chkSena.Checked,
                   chkFondoSolidaridad.Checked,
                   ddlTipoNovedad.SelectedValue
                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nParametroNovedadSeguridadSocial", operacion, "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 1:
                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }


        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.ddlTipoNovedad.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            ddlTipoNovedad.Focus();
            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] { Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)), Convert.ToInt16(Session["empresa"]) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nParametroNovedadSeguridadSocial", "elimina", "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                        break;
                    case 1:
                        ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (chkARP.Checked)
            {
                if (Convert.ToDecimal(txvPorcentajeARP.Text) == 0)
                {
                    this.nilblInformacion.Text = "Valore debe ser mayor a cero(0)";
                    return;
                }
            }
            if (chkCaja.Checked)
            {
                if (Convert.ToDecimal(txvPorcentajeCaja.Text) == 0)
                {
                    this.nilblInformacion.Text = "Valore debe ser mayor a cero(0)";
                    return;
                }
            }
            if (chkFondoSolidaridad.Checked)
            {
                if (Convert.ToDecimal(txvPorcentajeFondoSolidaridad.Text) == 0)
                {
                    this.nilblInformacion.Text = "Valore debe ser mayor a cero(0)";
                    return;
                }
            }
            if (chkICBF.Checked)
            {
                if (Convert.ToDecimal(txvPorcentajeICBF.Text) == 0)
                {
                    this.nilblInformacion.Text = "Valore debe ser mayor a cero(0)";
                    return;
                }
            }
            if (chkPension.Checked)
            {
                if (Convert.ToDecimal(txvPorcentajePension.Text) == 0)
                {
                    this.nilblInformacion.Text = "Valore debe ser mayor a cero(0)";
                    return;
                }
            }
            if (chkSalud.Checked)
            {
                if (Convert.ToDecimal(txvPorcentajeSalud.Text) == 0)
                {
                    this.nilblInformacion.Text = "Valore debe ser mayor a cero(0)";
                    return;
                }
            }
            if (chkSena.Checked)
            {
                if (Convert.ToDecimal(txvPorcentajeSena.Text) == 0)
                {
                    this.nilblInformacion.Text = "Valore debe ser mayor a cero(0)";
                    return;
                }
            }

            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.ddlTipoNovedad.Enabled = false;

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.ddlTipoNovedad.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[3].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkSalud.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.txvPorcentajeSalud.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                else
                    this.txvPorcentajeSalud.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[5].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkPension.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.txvPorcentajePension.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);
                else
                    this.txvPorcentajePension.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkFondoSolidaridad.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    this.txvPorcentajeFondoSolidaridad.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);
                else
                    this.txvPorcentajeFondoSolidaridad.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkARP.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[10].Text != "&nbsp;")
                    this.txvPorcentajeARP.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[10].Text);
                else
                    this.txvPorcentajeARP.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkCaja.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[12].Text != "&nbsp;")
                    this.txvPorcentajeCaja.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[12].Text);
                else
                    this.txvPorcentajeCaja.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[13].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkSena.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[14].Text != "&nbsp;")
                    this.txvPorcentajeSena.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[14].Text);
                else
                    this.txvPorcentajeSena.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[15].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkICBF.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[16].Text != "&nbsp;")
                    this.txvPorcentajeICBF.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[16].Text);
                else
                    this.txvPorcentajeICBF.Text = "0";


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }




        protected void chkSalud_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSalud.Checked)
                txvPorcentajeSalud.Enabled = true;
            else
            {
                txvPorcentajeSalud.Enabled = false;
                txvPorcentajeSalud.Text = "0";
            }
        }
        protected void chkPension_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPension.Checked)
                txvPorcentajePension.Enabled = true;
            else
            {
                txvPorcentajePension.Enabled = false;
                txvPorcentajePension.Text = "0";
            }
        }
        protected void chkFondoSolidaridad_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFondoSolidaridad.Checked)
                txvPorcentajeFondoSolidaridad.Enabled = true;
            else
            {
                txvPorcentajeFondoSolidaridad.Enabled = false;
                txvPorcentajeFondoSolidaridad.Text = "0";
            }

        }
        protected void chkARP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkARP.Checked)
                txvPorcentajeARP.Enabled = true;
            else
            {
                txvPorcentajeARP.Enabled = false;
                txvPorcentajeARP.Text = "0";
            }
        }
        protected void chkCaja_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCaja.Checked)
                txvPorcentajeCaja.Enabled = true;
            else
            {
                txvPorcentajeCaja.Enabled = false;
                txvPorcentajeCaja.Text = "0";
            }
        }
        protected void chkSena_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSena.Checked)
                txvPorcentajeSena.Enabled = true;
            else
            {
                txvPorcentajeSena.Enabled = false;
                txvPorcentajeSena.Text = "0";
            }
        }
        protected void chkICBF_CheckedChanged(object sender, EventArgs e)
        {
            if (chkICBF.Checked)
                txvPorcentajeICBF.Enabled = true;
            else
            {
                txvPorcentajeICBF.Enabled = false;
                txvPorcentajeICBF.Text = "0";
            }
        }

        #endregion Eventos
    }
}