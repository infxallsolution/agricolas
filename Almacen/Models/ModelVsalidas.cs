using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almacen.Models
{
    public class ModelVsalidas
    {
        public List<vsalidas> VSalidas { get; set; }
        public Departures Departures { get; set; }
        public ModelStartFinalDate Dates { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase file { get; set; }
    }
}