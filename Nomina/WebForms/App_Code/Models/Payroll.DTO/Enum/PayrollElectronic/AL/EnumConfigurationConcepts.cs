using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL
{
    public enum EnumConfigurationConcepts
    {
        [Display(Name = "Empleado - Datos básicos - Sueldo 7")]
        Empleado_Datosbásicos_Sueldo_7 = 7,
        [Display(Name = "Empleado Datos básicos -  Sueldo trabajado 8")]
        Empleado_Datosbásicos_Sueldo_trabajado_8 = 8,
        [Display(Name = "Devengos - Transporte - Auxilio Transporte 31")]
        Devengos_Transporte_Auxilio_Transporte_31 = 31,
        [Display(Name = "Devengos - Transporte - Viático Manutención Alojamiento 32")]
        Devengos_Transporte_Viático_Manutención_Alojamiento_32 = 32,
        [Display(Name = "Devengos - Transporte - Viático Manutención Alojamiento no salarial 33")]
        Devengos_Transporte_Viático_Manutención_Alojamiento_no_salarial_33 = 33,
        [Display(Name = "Devengos - Comisiones - Comisión 34")]
        Devengos_Comisiones_Comisión_34 = 34,
        [Display(Name = "Devengos - Auxilios - Auxilio salarial 35")]
        Devengos_Auxilios_Auxilio_salarial_35 = 35,
        [Display(Name = "Devengos - Auxilios - Auxilio no salarial 36")]
        Devengos_Auxilios_Auxilio_no_salarial_36 = 36,
        [Display(Name = "Devengos - Bonificaciones - Bonificación salarial 37")]
        Devengos_Bonificaciones_Bonificación_salarial_37 = 37,
        [Display(Name = "Devengos - Bonificaciones - Bonificacion no salarial 38")]
        Devengos_Bonificaciones_Bonificacion_no_salarial_38 = 38,
        [Display(Name = "Devengos - Compensaciones - Compensación Ordinaria 45")]
        Devengos_Compensaciones_Compensación_Ordinaria_45 = 45,
        [Display(Name = "Devengos - Compensaciones - Compensación Extraordinaria 46")]
        Devengos_Compensaciones_Compensación_Extraordinaria_46 = 46,
        [Display(Name = "Devengo - BonoElectrónico Vale Pago Salarial 47")]
        Devengos_BonoElectrónico_Vale_Pago_Salarial_47 = 47,
        [Display(Name = "Devengos - BonoElectrónico - Vale Pago no Salarial 48")]
        Devengos_BonoElectrónico_Vale_Pago_no_Salarial_48 = 48,
        [Display(Name = "Devengos - BonoElectrónico - Vale Pago Alimentación Salarial 49")]
        Devengos_BonoElectrónico_Vale_Pago_Alimentación_Salarial_49 = 49,
        [Display(Name = "Devengos- - BonoElectrónico - Vale Pago Alimentación no Salarial 50")]
        Devengos_BonoElectrónico_Vale_Pago_Alimentación_no_Salarial_50 = 50,
        [Display(Name = "Devengos - Pagoterceros Pago Tercero 51")]
        Devengos_Pagoterceros_Pago_Tercero_51 = 51,
        [Display(Name = "Devengos - Anticipos - Anticipo 52")]
        Devengos_Anticipos_Anticipo_52 = 52,
        [Display(Name = "Devengos - Anticipos - Dotación 53")]
        Devengos_Anticipos_Dotación_53 = 53,
        [Display(Name = "Devengos - Anticipos - Apoyo Sostenimiento 54")]
        Devengos_Anticipos_Apoyo_Sostenimiento_54 = 54,
        [Display(Name = "Devengos - Anticipos - Teletrabajo 55")]
        Devengos_Anticipos_Teletrabajo_55 = 55,
        [Display(Name = "Devengos - Anticipos Bonificación - Retiro 56")]
        Devengos_Anticipos_Bonificación_Retiro_56 = 56,
        [Display(Name = "Devengos - Anticipos - Indemnización 57")]
        Devengos_Anticipos_Indemnización_57 = 57,
        [Display(Name = "Devengos - Anticipos - Reintegro 58")]
        Devengos_Anticipos_Reintegro_58 = 58,
        [Display(Name = "Deducciones - Salud - Deducción 60")]
        Deducciones_Salud_Deducción_60 = 60,
        [Display(Name = "Deducciones - Pensión - Deducción 62")]
        Deducciones_Pensión_Deducción_62 = 62,
        [Display(Name = "Deducciones Sindicato - Deducción 68")]
        Deducciones_Sindicato_Deducción_68 = 68,
        [Display(Name = "Deducciones Sanciones - Sanción Pública 69")]
        Deducciones_Sanciones_Sanción_Pública_69 = 69,
        [Display(Name = "Deducciones - Sanciones - Sanción Privada 70")]
        Deducciones_Sanciones_Sanción_Privada_70 = 70,
        [Display(Name = "Deducciones- PagoTercero - PagoTercero 71")]
        Deducciones_PagoTercero_PagoTercero_71 = 71,
        [Display(Name = "Deducciones - Anticipo - Anticipo 72")]
        Deducciones_Anticipo_Anticipo_72 = 72,
        [Display(Name = "Deducciones - Otras deducciones - Otra Deducción 73")]
        Deducciones_Otrasdeducciones_Otra_Deducción_73 = 73,
        [Display(Name = "Deducciones - Otras deducciones - Pensión Voluntaria 74")]
        Deducciones_Otrasdeducciones_Pensión_Voluntaria_74 = 74,
        [Display(Name = "Deducciones - Otras deducciones - Retención Fuente 75")]
        Deducciones_Otrasdeducciones_Retención_Fuente_75 = 75,
        [Display(Name = "Deducciones - Otras deducciones - AFC Ahorro Fomento a la contrucción 76")]
        Deducciones_Otrasdeducciones_AFC_Ahorro_Fomento_a_la_contrucción_76 = 76,
        [Display(Name = "Deducciones - Otras deducciones - Cooperativa 77")]
        Deducciones_Otrasdeducciones_Cooperativa_77 = 77,
        [Display(Name = "Deducciones - Otras deducciones - Embargo Fiscal 78")]
        Deducciones_Otrasdeducciones_Embargo_Fiscal_78 = 78,
        [Display(Name = "Deducciones - Otras deducciones - Plan Complementario 79")]
        Deducciones_Otrasdeducciones_Plan_Complementario_79 = 79,
        [Display(Name = "Deducciones - Otras deducciones - Educación 80")]
        Deducciones_Otrasdeducciones_Educación_80 = 80,
        [Display(Name = "Deducciones - Otras deducciones - Reintegro 81")]
        Deducciones_Otrasdeducciones_Reintegro_81 = 81,
        [Display(Name = "Deducciones - Otrasdeducciones - Deuda 82")]
        Deducciones_Otrasdeducciones_Deuda_82 = 82,
        /// <summary>
        /// here start second sheet
        /// </summary>
        /// 
        [Display(Name = "Otros - Devengos Otros conceptos - Concepto salarial 27")]
        Otros_Devengos_Otros_conceptos_Concepto_salarial_27 = 227,
        [Display(Name = "Otros - Devengos Otros conceptos - Concepto no salarial 28")]
        Otros_Devengos_Otros_conceptos_Concepto_no_salarial_28 = 228,
        [Display(Name = "Deducciones - Libranza - Deducción 30")]
        Deducciones_Libranza_Deducción_30 = 230,
        [Display(Name = "Devengos - Primas 40")]
        Devengos_Primas_Pago_40 = 40,
        [Display(Name = "Devengos - Cesantias 42")]
        Devengos_Cesantías_Pago_42 = 42,
        [Display(Name = "Devengos - Cesantias Intereses 44")]
        Devengos_Cesantías_Pago_Intereses_44 = 44,
    }
}