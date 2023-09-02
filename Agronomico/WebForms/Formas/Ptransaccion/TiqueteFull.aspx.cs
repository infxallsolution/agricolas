using Agronomico.seguridadinfos;
using Agronomico.WebForms.App_Code.Administracion;
using Agronomico.WebForms.App_Code.General;
using Agronomico.WebForms.App_Code.Transaccion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agronomico.WebForms.Formas.Ptransaccion
{
    public partial class TiqueteFull : BasePage
    {
        #region Instancias
        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        List<CterceroTiquete> listaTerceros = new List<CterceroTiquete>();
        List<CterceroTiquete> listaTercerosCargue = new List<CterceroTiquete>();
        List<CterceroTiquete> listaTercerosTransporte = new List<CterceroTiquete>();
        Ccuadrillas cuadrillas = new Ccuadrillas();
        CterceroTiquete terceros;
        CnovedadCosecha novedadCosecha = new CnovedadCosecha();
        CnovedadTransporte novedadTransporte = new CnovedadTransporte();
        CnovedadCargue novedadCargue = new CnovedadCargue();
        List<CnovedadCosecha> listaNovedadesCosecha = new List<CnovedadCosecha>();
        List<CnovedadTransporte> listaNovedadesTransporte = new List<CnovedadTransporte>();
        List<CnovedadCargue> listaNovedadesCargue = new List<CnovedadCargue>();
        Cseccion seccion = new Cseccion();
        CIP ip = new CIP();
        Ctransacciones transacciones = new Ctransacciones();
        Cnovedad Cnovedad = new Cnovedad();
        CListaPrecios listaPrecios = new CListaPrecios();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();

        Clotes lotes = new Clotes();
        Cempresa empresa = new Cempresa();
        string numerotransaccion = "";
        Cnovedad novedad = new Cnovedad();
        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        CpromedioPeso peso = new CpromedioPeso();
        #endregion Instancias

        #region Metodos web

        [WebMethod(EnableSession = true)]
        public static string Guardar(string lTransaccion, string otiquete, string _tipo, string _numero)
        {
            try
            {
                cTransaccionAgro cTransaccionAgro = new cTransaccionAgro();
                Ctransacciones ctransacciones = new Ctransacciones();
                bool validarTransaccion = false;
                using (var ts = new TransactionScope())
                {
                    string operacionEncabezado = "inserta";
                    var transaccion = JsonConvert.DeserializeObject<List<cTransaccionAgro>>(lTransaccion);
                    var tiquete = JsonConvert.DeserializeObject<cTiquete>(otiquete);
                    tiquete.empresa = Convert.ToInt16(HttpContext.Current.Session["empresa"]);
                    string tipo = ConfigurationManager.AppSettings["RegistroBascula"].ToString();
                    string numerotransaccion = "";
                    if (_tipo.Trim().Length == 0 & _numero.Trim().Length == 0)
                    {
                        numerotransaccion = ctransacciones.RetornaNumeroTransaccion(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), tiquete.empresa);
                    }
                    else
                    {
                        tipo = _tipo;
                        numerotransaccion = _numero;
                        operacionEncabezado = "actualiza";
                    }
                    if (_tipo.Trim().Length > 0 & _numero.Trim().Length > 0)
                    {
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "elimina",
                               "ppa",
                               new object[] {
                                    tiquete.empresa,    //@empresa    int
                                      numerotransaccion,  //@numero varchar
                                      tipo,  //@tipo   varchar
                               }
                               ))
                        {
                            case 1:
                                validarTransaccion = true;
                                break;
                        }

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "elimina",
                              "ppa",
                              new object[] {
                                    tiquete.empresa,    //@empresa    int
                                      numerotransaccion,  //@numero varchar
                                      tipo,  //@tipo   varchar
                              }
                              ))
                        {
                            case 1:
                                validarTransaccion = true;
                                break;
                        }

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("atransaccionBascula", "elimina",
                               "ppa",
                               new object[] {
                                    tiquete.empresa,    //@empresa    int
                                      numerotransaccion,  //@numero varchar
                                      tipo,  //@tipo   varchar
                               }
                               ))
                        {
                            case 1:
                                validarTransaccion = true;
                                break;
                        }

                    }
                    switch (cTransaccionAgro.guardaTiquete(
                        empresa: tiquete.empresa,
                        tipo: tipo, numero: numerotransaccion, extractora: tiquete.extractora,
                        tiquete: tiquete.tiquete, pesoNeto: tiquete.pesoNeto, sacos: tiquete.sacos,
                        racimos: tiquete.racimos, cedulaConductor: tiquete.cedulaConductor,
                        nombreConductor: tiquete.nombreConductor, fechaTiquete: Convert.ToDateTime(tiquete.fechaTiquete),
                        interno: tiquete.interno, vehiculo: tiquete.vehiculo, remolque: tiquete.remolque
                        ))
                    {
                        case 1:
                            validarTransaccion = true;
                            break;
                        case 0:
                            DateTime fecha = Convert.ToDateTime(tiquete.fechaTiquete);
                            switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionCosechaFull", operacionEncabezado,
                                "ppa",
                                new object[] {
                                   false,     //@anulado    bit
                                   fecha.Year,   //@año    int
                                    tiquete.empresa,    //@empresa    int
                                     fecha,   //@fecha  date
                                     DateTime.Now,   //@fechaAnulado   datetime
                                      DateTime.Now,  //@fechaFinal date
                                      DateTime.Now,  //@fechaRegistro  datetime
                                      fecha.Month,  //@mes    int
                                      numerotransaccion,  //@numero varchar
                                      "",  //@observacion    varchar
                                      null,  //@referencia varchar
                                      null,   //@remision   varchar
                                      tipo,  //@tipo   varchar
                                      null,  //@usuarioAnulado varchar
                                      HttpContext.Current.Session["usuario"].ToString()  //@usuarioRegistro    varchar
                                }
                                ))
                            {
                                case 0:
                                    transaccion.ForEach(y =>
                                    {
                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedadCosechaFull", "inserta",
                                    "ppa",
                                        new object[] {
                                         fecha.Year,   //  @año    int
                                         y.totalKilogramos,   //@cantidad   decimal
                                         false,   //@ejecutado  bit
                                         tiquete.empresa,   //@empresa    int
                                         tiquete.fechaTiquete,   //@fecha  date
                                         null,   //@finca  varchar
                                         y.lCosecheros.Sum(x=>x.jornalesTercero) + y.lCargadores.Sum(x=>x.jornalesTercero)+ y.lTransportadores.Sum(x=>x.jornalesTercero), //@jornales   decimal
                                         y.jerarquia,   //@lote   varchar
                                         fecha.Month,   //@mes    int
                                         "",   //@novedad    varchar
                                         numerotransaccion,  //@numero varchar
                                         y.pesoPromedio,   //@pesoRacimo float
                                         0,   //@precioLabor    money
                                         y.racimos,   //@recimos    int
                                         y.item,  //@registro   int
                                         y.item,   //@registroNovedad    int
                                         y.sacos,   //@sacos  int
                                         y.totalKilogramos,   //@saldo  decimal
                                         null,   //@seccion    varchar
                                         tipo,   //@tipo   varchar
                                         null   //@uMedida    varchar
                                         }
                                    ))
                                        {
                                            case 0:

                                                var lTerceros = new List<List<cTerceros>>();
                                                lTerceros.Add(y.lCargadores);
                                                lTerceros.Add(y.lCosecheros);
                                                lTerceros.Add(y.lTransportadores);
                                                int registro = 1;
                                                lTerceros.ForEach(z =>
                                                {
                                                    registro++;
                                                    z.ForEach(w =>
                                                    {
                                                        DateTime fechaDetalle = Convert.ToDateTime(w.fecha);
                                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTerceroCosechaFull", "inserta",
                                                            "ppa",
                                                                new object[] {
                                                             fecha.Year,   //@año   int
                                                             w.cantidadActividad, //@cantidad   decimal
                                                             false,//@ejecutado  bit
                                                             tiquete.empresa, //@empresa    int
                                                             fechaDetalle, //@fechaNovedad   date
                                                             null,              //@finca  varchar
                                                             w.jornalesTercero,//@jornales   decimal
                                                             y.jerarquia, //@lote   char
                                                             fecha.Month,  //@mes    int
                                                             w.actividad, //@novedad    varchar
                                                             numerotransaccion,//@numero varchar
                                                             w.precioLabor,//@precioLabor    money
                                                             y.racimos,//@racimos    int
                                                             registro,//@registro   int
                                                             y.item, //@registroNovedad    int
                                                             w.cantidadActividad,//@saldo  decimal
                                                             null,             //@seccion    varchar
                                                             w.codigoTercero,  //@tercero    int
                                                             tipo,              //@tipo   varchar
                                                             null  //@zCuadrilla varchar
                                                                }
                                                            ))
                                                        {
                                                            case 1:
                                                                validarTransaccion = true;
                                                                break;
                                                        }
                                                    });
                                                });
                                                break;
                                            case 1:
                                                validarTransaccion = true;
                                                break;
                                        }

                                    });
                                    break;
                                case 1:
                                    validarTransaccion = true;
                                    break;
                            }
                            break;
                    }
                    if (!validarTransaccion)
                    {
                        ctransacciones.ActualizaConsecutivo(tipo, tiquete.empresa);
                        ts.Complete();
                        return JsonConvert.SerializeObject(new
                        {
                            id = 0,
                            titulo = "Exitoso!",
                            mensaje = "Dato registrado correctamente " + tipo + ": " + numerotransaccion,
                            tipo = "success"
                        });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            id = 1,
                            titulo = "Error!!",
                            mensaje = "Algo paso!! por favor comunicarse con el administrador",
                            tipo = "error"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    id = 1,
                    titulo = "Error!!",
                    mensaje = ex.Message,
                    tipo = "error"
                });
            }
        }

        [WebMethod(EnableSession = true)]
        public static string SeleccionaPrecioNovedadAñoTercero(string actividad, int tercero, string jerarquia, string fecha)
        {
            try
            {
                CListaPrecios listaPrecios = new CListaPrecios();
                decimal precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroJerarquia(Convert.ToInt16(HttpContext.Current.Session["empresa"]), actividad, Convert.ToInt32(tercero), Convert.ToDateTime(fecha), jerarquia);
                return JsonConvert.SerializeObject(new { precio = precioLabor });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { titulo = "Error", mensaje = ex.Message });
            }
        }


        [WebMethod(EnableSession = true)]
        public static string ObtenerPesoPromedio(string jerarquia, string fecha)
        {
            try
            {
                var fechaD = Convert.ToDateTime(fecha);
                Cgeneral general = new Cgeneral();
                CpromedioPeso peso = new CpromedioPeso();
                string manejaPesoPromedio = "0";
                manejaPesoPromedio = general.RetornoParametroGeneral("tablaPesoPromedio", Convert.ToInt16(HttpContext.Current.Session["empresa"]));
                //if (manejaPesoPromedio == "1")
                //{
                var retorno = Convert.ToDecimal(peso.valorPesoPeriodoJerarquia(Convert.ToInt32(HttpContext.Current.Session["empresa"]), fechaD, jerarquia));
                return JsonConvert.SerializeObject(new { pesoPromedio = Math.Round(retorno, 2) });
                //}
                //else
                //{
                //    return JsonConvert.SerializeObject(new { pesoPromedio = 1 });
                //}
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { titulo = "Error", mensaje = ex.Message });
            }

        }
        [WebMethod(EnableSession = true)]
        public static string cargarTiquetes(string filtro, string extractora)
        {
            try
            {
                int empresa = Convert.ToInt32(HttpContext.Current.Session["empresa"]);
                Cbascula bascula = new Cbascula();
                DataView tiquetes = bascula.SeleccionaTiquetesBasculaExtractora(Convert.ToInt32(extractora), empresa, filtro);

                if (tiquetes.Count > 0)
                {
                    return JsonConvert.SerializeObject(tiquetes.Table.AsEnumerable()
                        .Select(x => new
                        {
                            tiquete = x.Field<string>("tiquete"),
                            neto = x.Field<int>("pesoNeto"),
                            extractora = x.Field<string>("nombreExtractora"),
                            conductor = x.Field<string>("nombreConductor"),
                            sacos = x.Field<int>("sacos"),
                            racimos = x.Field<int>("racimos"),
                            fecha = x.Field<DateTime>("fecha"),
                            codigoConductor = x.Field<string>("codigoConductor"),
                            vehiculo = x.Field<string>("vehiculo"),
                            remolque = x.Field<string>("remolque"),
                        }).ToList());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { titulo = "Error", mensaje = ex.Message });
            }

        }
        [WebMethod(EnableSession = true)]
        public static string cargarNovedad(string tipo, int añoSiembra)
        {
            try
            {
                var claseLabor = 0;
                switch (tipo)
                {
                    case "1":
                        claseLabor = 2;
                        break;
                    case "2":
                        claseLabor = 3;
                        break;
                    case "3":
                        claseLabor = 4;
                        break;

                }
                int año = 0;
                if (añoSiembra > 0)
                    año = DateTime.Now.Year - añoSiembra;

                var dataNovedad = CentidadMetodos.EntidadGet("anovedad", "ppa").Tables[0].DefaultView
                      .Table.AsEnumerable()
                      .Where(x => x.Field<int>("empresa") == Convert.ToInt32(HttpContext.Current.Session["empresa"])
                      & x.Field<int>("claseLabor") == claseLabor
                       &
                      ((año >= x.Field<int>("añoDesde") & año <= x.Field<int>("añoHasta"))
                      || (x.Field<int>("añoDesde") == 0 & x.Field<int>("añoHasta") == 0))
                      )
                      .Select(y => new
                      {
                          codigo = y.Field<string>("codigo"),
                          descripcion = string.Format("{0} - {1} [{2}] ", y.Field<string>("codigo"), y.Field<string>("descripcion"), y.Field<string>("nombreUmedida").Trim().ToUpper()),
                          nombreActividad = y.Field<string>("descripcion"),
                          nombreUnidadMedida = y.Field<string>("nombreUmedida").Trim().ToUpper(),
                      }).OrderBy(x => x.codigo).ToList();

                return JsonConvert.SerializeObject(dataNovedad);

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { mensaje = ex.Message });
            }
        }
        #endregion Metodos web
        #region Metodos
        private void cargarTiqueteDetalle(string tipo, string numero)
        {
            DataView dvTiquete = transacciones.RetornaEncabezadoTransaccionTiquete(tipo, numero, Convert.ToInt16(this.Session["empresa"]));
            var otiquete = dvTiquete.Table.AsEnumerable()
                  .Select(x => new cTiquete
                  {
                      cedulaConductor = x.Field<string>("codigoConductor"),
                      empresa = Convert.ToInt16(Session["empresa"]),
                      extractora = x.Field<int>("terceroExtractrora").ToString(),
                      fechaTiquete = x.Field<DateTime>("fecha").ToString("yyyy-MM-dd"),
                      interno = x.Field<bool>("interno"),
                      nombreConductor = x.Field<string>("nombreConductor"),
                      numero = numero,
                      pesoNeto = x.Field<int>("pesoNeto"),
                      racimos = x.Field<int>("racimos"),
                      remolque = x.Field<string>("remolque"),
                      sacos = x.Field<int>("sacos"),
                      tipo = tipo,
                      tiquete = x.Field<string>("tiquete"),
                      vehiculo = x.Field<string>("vehiculo")
                  }).ToList();
            DataView dvItem = transacciones.RetornaEncabezadoTransaccionLaboresDetalle(tipo, numero, Convert.ToInt16(this.Session["empresa"]));

            var lTransaccion = dvItem.Table.AsEnumerable()
                .Select(x => new cTransaccionAgro
                {
                    añoSiembra = x.Field<int>("añoSiembra"),
                    item = x.Field<int>("registro"),
                    jerarquia = x.Field<string>("lote"),
                    pesoPromedio = Convert.ToDecimal(x.Field<double>("pesoRacimo")),
                    racimos = x.Field<int>("racimos"),
                    sacos = x.Field<int>("sacos"),
                    totalKilogramos = Convert.ToDecimal(x.Field<double>("cantidad"))
                }).ToList();
            DataView dvTerceros = transacciones.RetornaEncabezadoTransaccionLaboresTercero(tipo, numero, Convert.ToInt16(this.Session["empresa"]));

            lTransaccion.ForEach(w =>
            {

                var lTercerosCosecha = dvTerceros.Table.AsEnumerable()
                    .Where(y => y.Field<int>("claseLabor") == 2 && y.Field<int>("registroNovedad") == w.item)
                    .Select(x => new cTerceros()
                    {
                        actividad = x.Field<string>("actividad"),
                        cantidadActividad = Convert.ToDecimal(x.Field<double>("cantidad")),
                        codigoTercero = x.Field<int>("id").ToString(),
                        fecha = x.Field<DateTime>("fechaNovedad").ToString("yyyy-MM-dd"),
                        jornalesTercero = Convert.ToDecimal(x.Field<double>("jornales")),
                        nombreTercero = x.Field<string>("razonSocial"),
                        precioLabor = Convert.ToDecimal(x.Field<double>("precioLabor")),
                        racimos = 0,
                        uMedidaActividad = ""
                    }).ToList();

                var lTercerosCargue = dvTerceros.Table.AsEnumerable()
                   .Where(y => y.Field<int>("claseLabor") == 3 && y.Field<int>("registroNovedad") == w.item)
                   .Select(x => new cTerceros()
                   {
                       actividad = x.Field<string>("actividad"),
                       cantidadActividad = Convert.ToDecimal(x.Field<double>("cantidad")),
                       codigoTercero = x.Field<int>("id").ToString(),
                       fecha = x.Field<DateTime>("fechaNovedad").ToString("yyyy-MM-dd"),
                       jornalesTercero = Convert.ToDecimal(x.Field<double>("jornales")),
                       nombreTercero = x.Field<string>("razonSocial"),
                       precioLabor = Convert.ToDecimal(x.Field<double>("precioLabor")),
                       racimos = 0,
                       uMedidaActividad = ""
                   }).ToList();

                var lTercerosTransporte = dvTerceros.Table.AsEnumerable()
                 .Where(y => y.Field<int>("claseLabor") == 4 && y.Field<int>("registroNovedad") == w.item)
                 .Select(x => new cTerceros()
                 {
                     actividad = x.Field<string>("actividad"),
                     cantidadActividad = Convert.ToDecimal(x.Field<double>("cantidad")),
                     codigoTercero = x.Field<int>("id").ToString(),
                     fecha = x.Field<DateTime>("fechaNovedad").ToString("yyyy-MM-dd"),
                     jornalesTercero = Convert.ToDecimal(x.Field<double>("jornales")),
                     nombreTercero = x.Field<string>("razonSocial"),
                     precioLabor = Convert.ToDecimal(x.Field<double>("precioLabor")),
                     racimos = 0,
                     uMedidaActividad = ""
                 }).ToList();
                w.lCosecheros = new List<cTerceros>();
                w.lCosecheros = lTercerosCosecha;
                w.lCargadores = new List<cTerceros>();
                w.lCargadores = lTercerosCargue;
                w.lTransportadores = new List<cTerceros>();
                w.lTransportadores = lTercerosTransporte;

            });
            hfTipo.Value = tipo;
            hfNumero.Value = numero;
            hfTiquete.Value = JsonConvert.SerializeObject(otiquete);
            hfEdicion.Value = JsonConvert.SerializeObject(lTransaccion);
            string javaScript = "cargarEdicion();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        }

        private void EstadoInicialGrillaTransacciones()
        {
            for (int i = 0; i < this.gvTransaccion.Columns.Count; i++)
            {
                this.gvTransaccion.Columns[i].Visible = true;
            }

            foreach (GridViewRow registro in this.gvTransaccion.Rows)
            {
                this.gvTransaccion.Rows[registro.RowIndex].Visible = true;
            }
        }
        public void getNivelAplicacionCosecha()
        {
            int minNivel = 0;
            int empresa = Convert.ToInt32(Session["empresa"]);
            CpresupuestoAgronomico cpresupuestoAgronomico = new CpresupuestoAgronomico();
            var resultado = cpresupuestoAgronomico.getNivelAplicacionCosecha(empresa)
               .Table.AsEnumerable()
               .Select(y => new
               {
                   codigo = y.Field<string>("codigo"),
                   descripcion = y.Field<string>("descripcion"),
                   nivel = y.Field<int>("nivel"),
                   nivelPadre = y.Field<int>("nivelPadre"),
                   codigoPadre = y.Field<string>("codigoPadre"),
                   nombreJerarquia = y.Field<string>("nombreJerarquia").Trim().ToUpper(),
                   cadena = string.Format("{0}: {1}-{2} ", y.Field<string>("nombreJerarquia").Trim().ToUpper(),
                   y.Field<string>("codigo"), y.Field<string>("descripcion")),
                   añoSiembra = y.Field<int>("añoSiembra"),
                   auxiliar = y.Field<bool>("auxiliar")
               }).ToList();
            ddlNivel.Attributes.Add("data", JsonConvert.SerializeObject(resultado));

            if (resultado.Count > 0) {
                minNivel = resultado.Min(x => x.nivel);
                resultado = resultado.FindAll(y => y.nivel == minNivel);
            }
             
            ddlNivel.DataSource = resultado;
            ddlNivel.DataValueField = "codigo";
            ddlNivel.DataTextField = "cadena";
            ddlNivel.DataBind();
            ddlNivel.Items.Insert(0, new ListItem("", ""));

        }
        private void TabRegistro()
        {
            imbConsulta.BorderStyle = BorderStyle.None;
            niimbRegistro.BorderStyle = BorderStyle.Solid;
            niimbRegistro.BorderColor = System.Drawing.Color.Silver;
            niimbRegistro.BorderWidth = Unit.Pixel(1);
            niimbRegistro.Enabled = false;
            niimbRegistro.Enabled = false;
            nilbNuevo.Visible = true;
            lbCancelar.Visible = true;
            imbConsulta.Visible = true;
            niimbRegistro.Enabled = false;
            imbConsulta.Enabled = true;
            ddlNivel.ClearSelection();
            cargarExtractoras();
            getNivelAplicacionCosecha();
            cargarTerceros();
            upConsulta.Visible = false;
            niimbRegistro.Visible = true;
        }
        protected void cargarExtractoras()
        {
            try
            {
                ddlExtractoraFiltro.DataSource = empresa.SeleccionaEmpresasExtractoras();
                ddlExtractoraFiltro.DataValueField = "id";
                ddlExtractoraFiltro.DataTextField = "razonSocial";
                ddlExtractoraFiltro.DataBind();

                ddlExtractora.DataSource = empresa.SeleccionaEmpresasExtractoras();
                ddlExtractora.DataValueField = "id";
                ddlExtractora.DataTextField = "razonSocial";
                ddlExtractora.DataBind();
                ddlExtractora.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar terceros. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void TabConsulta()
        {
            CcontrolesUsuario.HabilitarControles(upConsulta.Controls);
            this.upConsulta.Visible = true;
            this.niimbRegistro.BorderStyle = BorderStyle.None;
            this.imbConsulta.BorderStyle = BorderStyle.Solid;
            this.imbConsulta.BorderColor = System.Drawing.Color.Silver;
            this.imbConsulta.BorderWidth = Unit.Pixel(1);
            imbBusqueda.Visible = true;
            nitxtValor2.Visible = false;
            this.niimbRegistro.Enabled = true;
            lbCancelar.Visible = false;
            niimbRegistro.Visible = true;
            this.nilbNuevo.Visible = false;
            this.imbConsulta.Enabled = false;
            gvParametros.DataSource = null;
            gvParametros.DataBind();
            gvTransaccion.DataSource = null;
            gvTransaccion.DataBind();
            this.Session["operadores"] = null;
            niimbRegistro.Enabled = true;
        }
        protected void cargarTerceros()
        {
            try
            {
                var terceros = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]))
                    .Table.AsEnumerable().Select(y => new { id = y.Field<int>("id"), cadena = y.Field<string>("cadena") }).ToList();
                this.ddlTerceros.DataSource = terceros;
                this.ddlTerceros.Attributes.Add("data", JsonConvert.SerializeObject(terceros));
                this.ddlTerceros.DataValueField = "id";
                this.ddlTerceros.DataTextField = "cadena";
                this.ddlTerceros.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de cosecha. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void BusquedaTransaccion()
        {
            try
            {
                if (this.gvParametros.Rows.Count > 0)
                {
                    string where = operadores.FormatoWhere((List<Coperadores>)Session["operadores"]);

                    this.gvTransaccion.DataSource = transacciones.GetTransaccionCompletaTiquete(where, Convert.ToInt32(Session["empresa"]));
                    this.gvTransaccion.DataBind();

                    this.nilblRegistros.Text = "Nro. Registros " + Convert.ToString(this.gvTransaccion.Rows.Count);

                    EstadoInicialGrillaTransacciones();
                }
                else
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe agregar un filtro antes de la busqueda", "warning");
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al procesar la consulta de transacciones. Correspondiente a: " + ex.Message, "C");
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
                if (!IsPostBack)
                    TabRegistro();
            }
        }

        protected void imbBusqueda_Click(object sender, EventArgs e)
        {
            BusquedaTransaccion();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {

        }
        protected void imbConsulta_Click(object sender, EventArgs e)
        {
            TabConsulta();
        }
        protected void niimbRegistro_Click(object sender, EventArgs e)
        {
            TabRegistro();
        }
        protected void niimbAdicionar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow registro in this.gvParametros.Rows)
            {
                if (Convert.ToString(this.niddlCampo.SelectedValue) == registro.Cells[1].Text && Convert.ToString(this.niddlOperador.SelectedValue) == Server.HtmlDecode(registro.Cells[2].Text) &&
                    this.nitxtValor1.Text == registro.Cells[3].Text)
                    return;
            }

            operadores = new Coperadores(Convert.ToString(this.niddlCampo.SelectedValue), Server.HtmlDecode(Convert.ToString(this.niddlOperador.SelectedValue)),
                this.nitxtValor1.Text, this.nitxtValor2.Text);

            List<Coperadores> listaOperadores = null;

            if (this.Session["operadores"] == null)
            {
                listaOperadores = new List<Coperadores>();
                listaOperadores.Add(operadores);
            }
            else
            {
                listaOperadores = (List<Coperadores>)Session["operadores"];
                listaOperadores.Add(operadores);
            }

            this.Session["operadores"] = listaOperadores;

            this.imbBusqueda.Visible = true;
            this.gvParametros.DataSource = listaOperadores;
            this.gvParametros.DataBind();
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.nilblRegistros.Text = "Nro. Registros 0";

            EstadoInicialGrillaTransacciones();
            imbBusqueda.Focus();
        }

        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            bool anulado = false;
            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[7].Controls)
            {
                anulado = ((CheckBox)objControl).Checked;
            }

            if (anulado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible su edición", "warning");
                return;
            }

            if (transacciones.validaEjecutarTransaccion(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim(), this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text.Trim(), Convert.ToInt32(Session["empresa"])) == 1)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro ejecutado no es posible su edición", "warning");
                return;
            }

            try
            {
                DateTime fecha = Convert.ToDateTime(this.gvTransaccion.Rows[e.RowIndex].Cells[4].Text);

                if (periodo.RetornaPeriodoCerradoNomina(fecha.Year, fecha.Month, Convert.ToInt32(Session["empresa"]), fecha) == 1)
                {
                    ManejoError("Periodo cerrado de nomina. No es posible realizar nuevas transacciones", "I");
                }
                else
                {
                    TabRegistro();
                    cargarTiqueteDetalle(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text);
                    lbGuardar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los datos. Correspondiente a: " + ex.Message, "C");
            }

        }

        protected void gvTransaccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransaccion.PageIndex = e.NewPageIndex;
            BusquedaTransaccion();
            gvTransaccion.DataBind();
        }

        protected void niddlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.niddlOperador.SelectedValue.ToString() == "between")
                this.nitxtValor2.Visible = true;
            else
            {
                this.nitxtValor2.Visible = false;
                this.nitxtValor1.Text = "";
            }
            this.nitxtValor1.Focus();
        }
        protected void nitxtValor1_TextChanged(object sender, EventArgs e)
        {
            if (this.nitxtValor1.Text.Length > 0 && Convert.ToString(this.niddlCampo.SelectedValue).Length > 0)
            {
                this.niimbAdicionar.Enabled = true;
                this.imbBusqueda.Enabled = true;
            }
            else
            {
                this.niimbAdicionar.Enabled = false;
                this.imbBusqueda.Enabled = false;
            }

            this.niimbAdicionar.Focus();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvParametros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Coperadores> listaOperadores = null;
            try
            {
                listaOperadores = (List<Coperadores>)Session["operadores"];
                listaOperadores.RemoveAt(e.RowIndex);

                this.gvParametros.DataSource = listaOperadores;
                this.gvParametros.DataBind();
                this.gvTransaccion.DataSource = null;
                this.gvTransaccion.DataBind();
                this.nilblRegistros.Text = "Nro. registros 0";
                if (this.gvParametros.Rows.Count == 0)
                    this.imbBusqueda.Visible = false;
                EstadoInicialGrillaTransacciones();
            }
            catch
            {
            }
        }

        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    this.Session["numerotransaccion"] = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text;
                    if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt16(this.Session["empresa"])) != 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Transacción ejecutada / anulada no es posible su edición", "warning");
                        return;
                    }

                    if (tipoTransaccion.RetornaTipoBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(this.Session["empresa"])) == "E")
                    {
                        switch (transacciones.EliminarTransaccionLabores(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt16(this.Session["empresa"])))
                        {
                            case 0:
                                CerroresGeneral.ManejoError(this, GetType(), "Registro Anulado satisfactoriamente", "warning");
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                CerroresGeneral.ManejoError(this, GetType(), "Error al eliminar registros ", "warning");
                                break;
                        }
                    }
                    else
                    {
                        switch (transacciones.AnulaTransaccion(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, this.Session["usuario"].ToString().Trim(), Convert.ToInt16(this.Session["empresa"])))
                        {
                            case 0:
                                CerroresGeneral.ManejoError(this, GetType(), "Registro Anulado satisfactoriamente", "warning");
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                CerroresGeneral.ManejoError(this, GetType(), "Error al anular la transacción. Operación no realizada", "warning");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ManejoErrorCatch(ex);
                }
            }

        }


        #endregion Eventos
    }
}