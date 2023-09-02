using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Programacion
{
    public class Cprogramacion
    {
        public Cprogramacion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView BuscaRegistros(string texto, DateTime fi, DateTime ff, int empresa)
        {
            string[] iParametros = new string[] { "@fi", "@ff", "@filtro", "@empresa" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { fi, ff, texto, empresa };
            return Cacceso.DataSetParametros("spSeleccionaProgramacionFechaFiltro", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int GuardaRegistroManual(string cuadrilla, DateTime fecha, DateTime fechaE, DateTime fechaS, string funcionario, string tipo, string turno, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@cuadrilla", "@fecha", "@fechaEntrada", "@fechaSalida", "@funcionario", "@tipoEntrada", "@turno", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { cuadrilla, fecha, fechaE, fechaS, funcionario, tipo, turno, usuario, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("SpInsertanRegistroPorteriaManual", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public int ReniciaProgramacion(DateTime fecha, string funcionario, string turno, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@fecha", "@funcionario", "@turno", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { fecha, funcionario, turno, usuario, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("SpnProgramacionAnularIngreso", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public void LunesDomingo(DateTime fecha, out DateTime fechaI, out DateTime fechaF)
        {
            fechaI = DateTime.Today;
            fechaF = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    fechaI = fecha;
                    fechaF = fecha.AddDays(6);
                    break;

                case DayOfWeek.Tuesday:

                    fechaI = fecha.AddDays(-1);
                    fechaF = fecha.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:

                    fechaI = fecha.AddDays(-2);
                    fechaF = fecha.AddDays(4);
                    break;

                case DayOfWeek.Thursday:

                    fechaI = fecha.AddDays(-3);
                    fechaF = fecha.AddDays(3);
                    break;

                case DayOfWeek.Friday:

                    fechaI = fecha.AddDays(-4);
                    fechaF = fecha.AddDays(2);
                    break;

                case DayOfWeek.Saturday:

                    fechaI = fecha.AddDays(-5);
                    fechaF = fecha.AddDays(1);
                    break;

                case DayOfWeek.Sunday:

                    fechaI = fecha.AddDays(-6);
                    fechaF = fecha;
                    break;
            }


        }

        public DataView GetProgramacionFuncionariosCuadrilla(string cuadrilla, string turno, DateTime fecha, int empresa)
        {
            DateTime fechaI = DateTime.Today, fechaF = DateTime.Today;

            LunesDomingo(fecha, out fechaI, out fechaF);

            string[] iParametros = new string[] { "@cuadrilla", "@turno", "@fechaI", "@fechaF", "@empresa" };
            object[] objValores = new object[] { cuadrilla, turno, fechaI, fechaF, empresa };

            return Cacceso.DataSetParametros("spSeleccionaProgramacionFuncionariosCuadrilla", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetProgramacionFuncionariosCuadrillaRegistroPorteria(string cuadrilla, string turno, string usuario, DateTime fecha, int empresa)
        {
            DateTime fechaI = DateTime.Today, fechaF = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    fechaI = fecha;
                    fechaF = fecha.AddDays(6);
                    break;

                case DayOfWeek.Tuesday:

                    fechaI = fecha.AddDays(-1);
                    fechaF = fecha.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:

                    fechaI = fecha.AddDays(-2);
                    fechaF = fecha.AddDays(4);
                    break;

                case DayOfWeek.Thursday:

                    fechaI = fecha.AddDays(-3);
                    fechaF = fecha.AddDays(3);
                    break;

                case DayOfWeek.Friday:

                    fechaI = fecha.AddDays(-4);
                    fechaF = fecha.AddDays(2);
                    break;

                case DayOfWeek.Saturday:

                    fechaI = fecha.AddDays(-5);
                    fechaF = fecha.AddDays(1);
                    break;

                case DayOfWeek.Sunday:

                    fechaI = fecha.AddDays(-6);
                    fechaF = fecha;
                    break;
            }

            string[] iParametros = new string[] { "@cuadrilla", "@turno", "@fechaI", "@fechaF", "@empresa" };
            object[] objValores = new object[] { cuadrilla, turno, fechaI, fechaF, empresa };

            return Cacceso.DataSetParametros("spSeleccionaProgramacionFuncionariosCuadrillaRegistroProteria", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetProgramacionFuncionariosAprobar(DateTime fecha, string cuadrilla, int empresa)
        {
            DateTime fechaI = DateTime.Today, fechaF = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    fechaI = fecha;
                    fechaF = fecha.AddDays(6);
                    break;

                case DayOfWeek.Tuesday:

                    fechaI = fecha.AddDays(-1);
                    fechaF = fecha.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:

                    fechaI = fecha.AddDays(-2);
                    fechaF = fecha.AddDays(4);
                    break;

                case DayOfWeek.Thursday:

                    fechaI = fecha.AddDays(-3);
                    fechaF = fecha.AddDays(3);
                    break;

                case DayOfWeek.Friday:

                    fechaI = fecha.AddDays(-4);
                    fechaF = fecha.AddDays(2);
                    break;

                case DayOfWeek.Saturday:

                    fechaI = fecha.AddDays(-5);
                    fechaF = fecha.AddDays(1);
                    break;

                case DayOfWeek.Sunday:

                    fechaI = fecha.AddDays(-6);
                    fechaF = fecha;
                    break;
            }

            string[] iParametros = new string[] { "@fechaI", "@fechaF", "@empresa", "@cuadrilla" };
            object[] objValores = new object[] { fechaI, fechaF, empresa, cuadrilla };

            return Cacceso.DataSetParametros("spSeleccionaProgramacionFuncionariosExtras", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetPermisoFuncionarioFechaTurno(DateTime fecha, string funcionario, int empresa)
        {
            string[] iParametros = new string[] { "@fecha", "@funcionario", "@empresa" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { fecha, funcionario, empresa };

            return Cacceso.DataSetParametros(
                "SpSeleccionaNovedadesFuncionarioFecha",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public string traerFuncionario(string funcionario, int empresa)
        {
            string[] iParametros = new string[] { "@idFuncionario", "@empresa" };
            string[] oParametros = new string[] { "@nombre" };
            object[] objValores = new object[] { funcionario, empresa };
            return Convert.ToString(Cacceso.ExecProc("spRetornaNombreFuncionario", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int VerificaProgramacionExiste(DateTime fecha, string turno, string cuadrilla, int empresa)
        {
            DateTime fechaI = DateTime.Today, fechaF = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    fechaI = fecha;
                    fechaF = fecha.AddDays(6);
                    break;

                case DayOfWeek.Tuesday:

                    fechaI = fecha.AddDays(-1);
                    fechaF = fecha.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:

                    fechaI = fecha.AddDays(-2);
                    fechaF = fecha.AddDays(4);
                    break;

                case DayOfWeek.Thursday:

                    fechaI = fecha.AddDays(-3);
                    fechaF = fecha.AddDays(3);
                    break;

                case DayOfWeek.Friday:

                    fechaI = fecha.AddDays(-4);
                    fechaF = fecha.AddDays(2);
                    break;

                case DayOfWeek.Saturday:

                    fechaI = fecha.AddDays(-5);
                    fechaF = fecha.AddDays(1);
                    break;

                case DayOfWeek.Sunday:

                    fechaI = fecha.AddDays(-6);
                    fechaF = fecha;
                    break;
            }

            string[] iParametros = new string[] { "@FI", "@FF", "@turno", "@cuadrilla", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fechaI, fechaF, turno, cuadrilla, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("spVerificaProgramacionExistentes", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView GetProgramacionFuncionariosDias(DateTime fecha, string turno, string cuadrilla, string funcionario, int empresa)
        {
            DateTime fechaI = DateTime.Today, fechaF = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    fechaI = fecha;
                    fechaF = fecha.AddDays(6);
                    break;

                case DayOfWeek.Tuesday:

                    fechaI = fecha.AddDays(-1);
                    fechaF = fecha.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:

                    fechaI = fecha.AddDays(-2);
                    fechaF = fecha.AddDays(4);
                    break;

                case DayOfWeek.Thursday:

                    fechaI = fecha.AddDays(-3);
                    fechaF = fecha.AddDays(3);
                    break;

                case DayOfWeek.Friday:

                    fechaI = fecha.AddDays(-4);
                    fechaF = fecha.AddDays(2);
                    break;

                case DayOfWeek.Saturday:

                    fechaI = fecha.AddDays(-5);
                    fechaF = fecha.AddDays(1);
                    break;

                case DayOfWeek.Sunday:

                    fechaI = fecha.AddDays(-6);
                    fechaF = fecha;
                    break;
            }

            string[] iParametros = new string[] { "@FI", "@FF", "@turno", "@cuadrilla", "@funcionario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fechaI, fechaF, turno, cuadrilla, funcionario, empresa };

            return Cacceso.DataSetParametros("spSeleccionaProgramacionFuncionariosDias", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetProgramacionFuncionariosDiasP(DateTime fecha, string usuario, string turno, string cuadrilla, string funcionario, int empresa)
        {
            DateTime fechaI = DateTime.Today, fechaF = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    fechaI = fecha;
                    fechaF = fecha.AddDays(6);
                    break;

                case DayOfWeek.Tuesday:

                    fechaI = fecha.AddDays(-1);
                    fechaF = fecha.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:

                    fechaI = fecha.AddDays(-2);
                    fechaF = fecha.AddDays(4);
                    break;

                case DayOfWeek.Thursday:

                    fechaI = fecha.AddDays(-3);
                    fechaF = fecha.AddDays(3);
                    break;

                case DayOfWeek.Friday:

                    fechaI = fecha.AddDays(-4);
                    fechaF = fecha.AddDays(2);
                    break;

                case DayOfWeek.Saturday:

                    fechaI = fecha.AddDays(-5);
                    fechaF = fecha.AddDays(1);
                    break;

                case DayOfWeek.Sunday:

                    fechaI = fecha.AddDays(-6);
                    fechaF = fecha;
                    break;
            }

            string[] iParametros = new string[] { "@FI", "@FF", "@usuario", "@turno", "@cuadrilla", "@funcionario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fechaI, fechaF, usuario, turno, cuadrilla, funcionario, empresa };

            return Cacceso.DataSetParametros("spSeleccionaProgramacionFuncionariosDiasP", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetProgramacionFuncionariosDiasAprobar(DateTime fecha, string funcionario, string cuadrilla, int empresa)
        {
            DateTime fechaI = DateTime.Today, fechaF = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    fechaI = fecha;
                    fechaF = fecha.AddDays(6);
                    break;

                case DayOfWeek.Tuesday:

                    fechaI = fecha.AddDays(-1);
                    fechaF = fecha.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:

                    fechaI = fecha.AddDays(-2);
                    fechaF = fecha.AddDays(4);
                    break;

                case DayOfWeek.Thursday:

                    fechaI = fecha.AddDays(-3);
                    fechaF = fecha.AddDays(3);
                    break;

                case DayOfWeek.Friday:

                    fechaI = fecha.AddDays(-4);
                    fechaF = fecha.AddDays(2);
                    break;

                case DayOfWeek.Saturday:

                    fechaI = fecha.AddDays(-5);
                    fechaF = fecha.AddDays(1);
                    break;

                case DayOfWeek.Sunday:

                    fechaI = fecha.AddDays(-6);
                    fechaF = fecha;
                    break;
            }

            string[] iParametros = new string[] { "@FI", "@FF", "@funcionario", "@empresa", "@cuadrilla" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fechaI, fechaF, funcionario, empresa, cuadrilla };

            return Cacceso.DataSetParametros("spSeleccionaProgramacionFuncionariosDiasAprobar", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int GuardaProgramacion(DateTime fecha, string turno, string funcionario, string cuadrilla, string usuario, string dia, bool programado, int empresa, string observacion)
        {
            DateTime fechaRegisto = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha;
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(3);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(4);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(5);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(6);
                            break;
                    }
                    break;

                case DayOfWeek.Tuesday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "mar":

                            fechaRegisto = fecha;
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(3);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(4);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(5);
                            break;
                    }
                    break;

                case DayOfWeek.Wednesday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "mie":

                            fechaRegisto = fecha;
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(3);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(4);
                            break;
                    }
                    break;

                case DayOfWeek.Thursday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "jue":

                            fechaRegisto = fecha;
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(3);
                            break;
                    }
                    break;

                case DayOfWeek.Friday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-4);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "vie":

                            fechaRegisto = fecha;
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(2);
                            break;
                    }
                    break;

                case DayOfWeek.Saturday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-5);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-4);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "sab":

                            fechaRegisto = fecha;
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(1);
                            break;
                    }
                    break;

                case DayOfWeek.Sunday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-6);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-5);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-4);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "dom":

                            fechaRegisto = fecha;
                            break;
                    }
                    break;
            }

            string[] iParametros = new string[] { "@fecha", "@turno", "@funcionario", "@cuadrilla", "@dia", "@usuario", "@programado", "@empresa", "@observacion" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { fechaRegisto, turno, funcionario, cuadrilla, dia, usuario, programado, empresa, observacion };

            return Convert.ToInt16(Cacceso.ExecProc("SpInsertanProgramacionFuncionarios", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int GuardaAprobacionAdicionExtras(DateTime fecha, string funcionario, string usuario, string dia, decimal cantidad, bool aprobada, int empresa)
        {
            DateTime fechaRegisto = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha;
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(3);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(4);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(5);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(6);
                            break;
                    }
                    break;

                case DayOfWeek.Tuesday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "mar":

                            fechaRegisto = fecha;
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(3);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(4);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(5);
                            break;
                    }
                    break;

                case DayOfWeek.Wednesday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "mie":

                            fechaRegisto = fecha;
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(3);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(4);
                            break;
                    }
                    break;

                case DayOfWeek.Thursday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "jue":

                            fechaRegisto = fecha;
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(3);
                            break;
                    }
                    break;

                case DayOfWeek.Friday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-4);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "vie":

                            fechaRegisto = fecha;
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(2);
                            break;
                    }
                    break;

                case DayOfWeek.Saturday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-5);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-4);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "sab":

                            fechaRegisto = fecha;
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(1);
                            break;
                    }
                    break;

                case DayOfWeek.Sunday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-6);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-5);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-4);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "dom":

                            fechaRegisto = fecha;
                            break;
                    }
                    break;
            }

            string[] iParametros = new string[] { "@fecha", "@funcionario", "@usuario", "@cantidad", "@empresa", "@aprobada" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { fechaRegisto, funcionario, usuario, cantidad, empresa, aprobada };

            return Convert.ToInt16(Cacceso.ExecProc("SpApruebaHorasAdicionales", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int AutorizaHorasExtras(DateTime fecha, string turno, string funcionario, decimal horas, string dia, string usuario, int empresa, string observacion)
        {
            DateTime fechaRegisto = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha;
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(3);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(4);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(5);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(6);
                            break;
                    }
                    break;

                case DayOfWeek.Tuesday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "mar":

                            fechaRegisto = fecha;
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(3);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(4);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(5);
                            break;
                    }
                    break;

                case DayOfWeek.Wednesday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "mie":

                            fechaRegisto = fecha;
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(3);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(4);
                            break;
                    }
                    break;

                case DayOfWeek.Thursday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "jue":

                            fechaRegisto = fecha;
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(2);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(3);
                            break;
                    }
                    break;

                case DayOfWeek.Friday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-4);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "vie":

                            fechaRegisto = fecha;
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(1);
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(2);
                            break;
                    }
                    break;

                case DayOfWeek.Saturday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-5);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-4);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "sab":

                            fechaRegisto = fecha;
                            break;

                        case "dom":

                            fechaRegisto = fecha.AddDays(1);
                            break;
                    }
                    break;

                case DayOfWeek.Sunday:

                    switch (dia)
                    {
                        case "lun":

                            fechaRegisto = fecha.AddDays(-6);
                            break;

                        case "mar":

                            fechaRegisto = fecha.AddDays(-5);
                            break;

                        case "mie":

                            fechaRegisto = fecha.AddDays(-4);
                            break;

                        case "jue":

                            fechaRegisto = fecha.AddDays(-3);
                            break;

                        case "vie":

                            fechaRegisto = fecha.AddDays(-2);
                            break;

                        case "sab":

                            fechaRegisto = fecha.AddDays(-1);
                            break;

                        case "dom":

                            fechaRegisto = fecha;
                            break;
                    }
                    break;
            }

            string[] iParametros = new string[] { "@fecha", "@turno", "@funcionario", "@horas", "@usuario", "@empresa", "@observacion" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fechaRegisto, turno, funcionario, horas, usuario, empresa, observacion };

            return Convert.ToInt16(Cacceso.ExecProc("spAutorizaHorasExtraProgramacion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView GetProgramacionFuncionariosDiasHorasExtras(DateTime fecha, string usuario, string turno, string cuadrilla, string funcionario, int empresa)
        {
            DateTime fechaI = DateTime.Today, fechaF = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    fechaI = fecha;
                    fechaF = fecha.AddDays(6);
                    break;

                case DayOfWeek.Tuesday:

                    fechaI = fecha.AddDays(-1);
                    fechaF = fecha.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:

                    fechaI = fecha.AddDays(-2);
                    fechaF = fecha.AddDays(4);
                    break;

                case DayOfWeek.Thursday:

                    fechaI = fecha.AddDays(-3);
                    fechaF = fecha.AddDays(3);
                    break;

                case DayOfWeek.Friday:

                    fechaI = fecha.AddDays(-4);
                    fechaF = fecha.AddDays(2);
                    break;

                case DayOfWeek.Saturday:

                    fechaI = fecha.AddDays(-5);
                    fechaF = fecha.AddDays(1);
                    break;

                case DayOfWeek.Sunday:

                    fechaI = fecha.AddDays(-6);
                    fechaF = fecha;
                    break;
            }

            string[] iParametros = new string[] { "@FI", "@FF", "@usuario", "@turno", "@cuadrilla", "@funcionario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fechaI, fechaF, usuario, turno, cuadrilla, funcionario, empresa };

            return Cacceso.DataSetParametros("spSeleccionaProgramacionFuncionariosDiasHorasExtras", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetFuncionariosSinProgramacionCuadrilla(DateTime fecha, string cuadrilla, int empresa)
        {
            DateTime fechaI = DateTime.Today, fechaF = DateTime.Today;

            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:

                    fechaI = fecha;
                    fechaF = fecha.AddDays(6);
                    break;

                case DayOfWeek.Tuesday:

                    fechaI = fecha.AddDays(-1);
                    fechaF = fecha.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:

                    fechaI = fecha.AddDays(-2);
                    fechaF = fecha.AddDays(4);
                    break;

                case DayOfWeek.Thursday:

                    fechaI = fecha.AddDays(-3);
                    fechaF = fecha.AddDays(3);
                    break;

                case DayOfWeek.Friday:

                    fechaI = fecha.AddDays(-4);
                    fechaF = fecha.AddDays(2);
                    break;

                case DayOfWeek.Saturday:

                    fechaI = fecha.AddDays(-5);
                    fechaF = fecha.AddDays(1);
                    break;

                case DayOfWeek.Sunday:

                    fechaI = fecha.AddDays(-6);
                    fechaF = fecha;
                    break;
            }

            string[] iParametros = new string[] { "@fechaI", "@fechaF", "@cuadrilla", "@empresa" };
            object[] objValores = new object[] { fechaI, fechaF, cuadrilla, empresa };

            return Cacceso.DataSetParametros("spSeleccionaFuncionariosSinProgramacionCuadrilla", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int AutorizaPermiso(DateTime fecha, string turno, string funcionario, string usuario, int horaInicio, int horasPermiso, bool remunerado, string motivo, string observacion, bool regresa, int empresa)
        {
            string[] iParametros = new string[] { "@fecha", "@turno", "@funcionario", "@usuario", "@horaInicio", "@horasPermiso", "@remunerado", "@motivo", "@observacion", "@regresa", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fecha, turno, funcionario, usuario, horaInicio, horasPermiso, remunerado, motivo, observacion, regresa, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spAutorizaPermiso", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView GetPermisoFuncionarioFechaTurno(DateTime fecha, string turno, string funcionario, int empresa)
        {
            string[] iParametros = new string[] { "@fecha", "@turno", "@funcionario", "@empresa" };
            object[] objValores = new object[] { fecha, turno, funcionario, empresa };
            return Cacceso.DataSetParametros("spSeleccionaPermisoFuncionarioFechaTurno", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int AnulaPermiso(DateTime fecha, string turno, string funcionario, string observacion, int empresa)
        {
            string[] iParametros = new string[] { "@fecha", "@turno", "@funcionario", "@observacion", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fecha, turno, funcionario, observacion, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spAnulaPermisoFuncionario", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int SeleccionaHoraInicioTurno(string turno, int empresa)
        {
            string[] iParametros = new string[] { "@turno", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { turno, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spSeleccionaHoraInicioTurno", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView SeleccionaNovedadesFechaFuncionarios(DateTime fecha, int empresa)
        {
            DateTime fi, ff;
            LunesDomingo(fecha, out fi, out ff);
            string[] iParametros = new string[] { "@fi", "@ff", "@empresa" };
            object[] objValores = new object[] { fi, ff, empresa };
            return Cacceso.DataSetParametros("spSeleccionaNovedadesFechaFuncionarios", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

    }
}