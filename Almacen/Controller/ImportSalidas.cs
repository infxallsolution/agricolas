using Almacen.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebAppBilling.Controllers.ViewController
{
    public class ImportSalidasController : Controller
    {

        private readonly Almacen.Context.AppContext db;

        public ImportSalidasController()
        {
            db = new Almacen.Context.AppContext();
        }
        public ActionResult Index(ModelVsalidas modelVsalidas)
        {
            if (modelVsalidas.file != null && modelVsalidas.file.ContentLength > 0)
            {
                try
                {
                    using (var workbook = new XLWorkbook(modelVsalidas.file.InputStream))
                    {
                        var worksheet = workbook.Worksheet(1);

                        var rows = worksheet.RowsUsed().Skip(1);

                        foreach (var row in rows)
                        {
                            var vsalida = new vsalidas();
                            vsalida.fecha = Convert.ToDateTime(row.Cell(1).Value.ToString());
                            vsalida.numero = row.Cell(2).Value.ToString();
                            vsalida.cadenaItems = row.Cell(3).Value.ToString();
                            vsalida.cantidad = Convert.ToDouble(row.Cell(4).Value);
                            string valorUnitarioStr = row.Cell(5).Value.ToString().Replace("$", "");
                            if (double.TryParse(valorUnitarioStr, out double valorUnitario))
                            {
                                vsalida.valorUnitario = valorUnitario;
                            }
                            string valorTotalStr = row.Cell(5).Value.ToString().Replace("$", "");
                            if (double.TryParse(valorUnitarioStr, out double valorTotal))
                            {
                                vsalida.valorTotal = valorTotal;
                            }
                            if (modelVsalidas.VSalidas == null)
                            {

                                modelVsalidas.VSalidas = new List<vsalidas>();
                            }
                            modelVsalidas.VSalidas.Add(vsalida);

                        }

                        TempData["vsalidasList"] = modelVsalidas.VSalidas;

                        return View(new ModelVsalidas()
                        {
                            VSalidas = modelVsalidas.VSalidas

                        });
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Ha ocurrido un error: " + ex.Message;
                }
            }
            return View();
        }



        [HttpPost]
        public ActionResult Confirm()
        {
            List<vsalidas> vsalidasList = TempData["vsalidasList"] as List<vsalidas>;

            if (vsalidasList != null)
            {
                try
                {
                    foreach (var vsalida in vsalidasList)
                    {
                        // Mapear los datos de vsalida a Departures
                        var departure = new Departures
                        {
                            Id = Guid.NewGuid(),
                            Date = vsalida.fecha,
                            Number = vsalida.numero,
                            Amount = vsalida.cantidad,
                            Unitvalue = vsalida.valorUnitario,
                            Totalvalue = vsalida.valorTotal,
                            CostCenter = "Prueba",
                            LedgerAccount = "Prueba"
                        };

                        db.Departures.Add(departure);
                    }

                    db.SaveChanges();
                    TempData.Remove("vsalidasList");

                    return View("Index", new ModelVsalidas()
                    {
                        VSalidas = null,
                        file = null
                    });
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Ha ocurrido un error al guardar los datos: " + ex.Message;
                }
            }

            return View("Index", new ModelVsalidas()
            {
                VSalidas = vsalidasList
            });
        }

        [HttpPost]
        public ActionResult DeleteImport()
        {
            TempData.Remove("vsalidasList");
            
            return View("Index", new ModelVsalidas()
            {
                
                VSalidas = null,
                file = null
            });


        }


    }
}
