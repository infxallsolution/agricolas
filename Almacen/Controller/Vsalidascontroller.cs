using Almacen.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebAppBilling.Controllers.ViewController
{
    public class VsalidasController : Controller
    {

        private readonly Almacen.Context.AppContext db;

        public VsalidasController()
        {
            db = new Almacen.Context.AppContext();
        }
        // GET: UnitsMeasurements
        public ActionResult Index(ModelVsalidas modelVsalidas)
        {
            var query = db.Vsalidas.AsQueryable();
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (modelVsalidas.Dates != null)
            {
                startDate = modelVsalidas.Dates.StartDate;
                endDate = modelVsalidas.Dates.FinaltDate;

                if (startDate != null && endDate != null)
                {
                    // Filtrar por rango de fechas
                    query = query.Where(x => x.fecha >= startDate && x.fecha <= endDate);
                }
            }
            var vsalidasData = query.ToList();
            if (modelVsalidas.Dates != null)
            {
                TempData["StartDate"] = modelVsalidas.Dates.StartDate;
                TempData["EndDate"] = modelVsalidas.Dates.FinaltDate;
            }

            return View(new ModelVsalidas()
            {
                VSalidas = vsalidasData,
                Dates = new ModelStartFinalDate() { FinaltDate = endDate, StartDate = startDate }
            });
        }

        [HttpPost]
        public ActionResult Export()
        {
            try
            {
                IQueryable<vsalidas> query = db.Vsalidas;
                var vsalidas = query.ToList();
                if (TempData["StartDate"] != null || TempData["EndDate"] != null)
                {
                    DateTime startDate = (DateTime)TempData["StartDate"];
                    DateTime endDate = (DateTime)TempData["EndDate"];



                    if (startDate == null && endDate == null)
                    {

                        vsalidas = query.ToList();
                    }
                    else if (startDate != null && endDate != null)
                    {

                        vsalidas = query.Where(x => x.fecha >= startDate.Date && x.fecha <= endDate.Date).ToList();
                    }
                    else if (startDate != null)
                    {

                        vsalidas = query.Where(x => x.fecha.Date >= startDate).ToList();
                    }
                    else if (endDate != null)
                    {

                        vsalidas = query.Where(x => x.fecha.Date <= endDate.Date).ToList();
                    }
                    else
                    {

                        return null;
                    }
                }

                var book = new XLWorkbook();
                var page = book.AddWorksheet("Registro de salidas");
                int rowIndex = 2;
                addHeader(page);
                foreach (var m in vsalidas)
                {
                    page.Row(rowIndex).Cell(1).Value = m.fecha;
                    page.Row(rowIndex).Cell(2).Value = m.numero;
                    page.Row(rowIndex).Cell(3).Value = m.cadenaItems;
                    page.Row(rowIndex).Cell(4).Value = m.cantidad;
                    page.Row(rowIndex).Cell(5).Value = $"$ {m.valorUnitario}";
                    page.Row(rowIndex).Cell(6).Value = $"$ {m.valorTotal}";
                    page.Row(rowIndex).Cell(7).Value = "NULL";
                    page.Row(rowIndex).Cell(8).Value = "NULL";
                    page.Row(rowIndex).Cell(1).Style.NumberFormat.Format = "dd/MM/yyyy";
                    page.Row(rowIndex).Cell(6).Style.NumberFormat.Format = "$#,##0.00";
                    rowIndex++;
                }
                // Estilos de las celdas de datos
                var dataRange = page.Range(page.Cell(2, 1), page.Cell(rowIndex <= 2 ? 2 : rowIndex - 1, 8));
                dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                dataRange.Style.Fill.BackgroundColor = XLColor.White;
                dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.OutsideBorderColor = XLColor.Gray;
                dataRange.Style.Border.InsideBorderColor = XLColor.Gray;
                // Ajustar el ancho de las columnas
                page.Column(1).AdjustToContents();
                page.Column(2).AdjustToContents();
                page.Column(3).AdjustToContents();
                page.Column(4).AdjustToContents();
                page.Column(5).AdjustToContents();
                page.Column(6).AdjustToContents();
                page.Column(7).AdjustToContents();
                page.Column(8).AdjustToContents();
                // Bordes
                var allRange = page.Range(page.Cell(1, 1), page.Cell(rowIndex - 1, 8));
                allRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                allRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                using (var memory = new MemoryStream())
                {
                    book.SaveAs(memory);
                    memory.Position = 0;
                    var nameExcel = $"Salida de almacen {DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.xlsx";
                    return File(memory.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessages"] = "Ha ocurrido un error" + ex;
                return RedirectToAction(nameof(Index));
            }
        }

        private static void addHeader(IXLWorksheet page)
        {
            page.Row(1).Cell(1).Value = "Fecha";
            page.Row(1).Cell(2).Value = "Número de salida";
            page.Row(1).Cell(3).Value = "Producto";
            page.Row(1).Cell(4).Value = "Cantidad";
            page.Row(1).Cell(5).Value = "Valor unitario($)";
            page.Row(1).Cell(6).Value = "Valor total";
            page.Row(1).Cell(7).Value = "Cuenta contable";
            page.Row(1).Cell(8).Value = "Centro de costo";
            // Estilos de encabezado
            page.Row(1).Style.Font.Bold = true;
            page.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;
            page.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

    }
}
