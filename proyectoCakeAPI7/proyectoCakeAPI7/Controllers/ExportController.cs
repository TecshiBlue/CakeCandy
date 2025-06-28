using proyectoCakeAPI7.DAO.DaoImpl;
using proyectoCakeAPI7.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using iText.Kernel.Font;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System;
using System.Linq;
using proyectoCakeAPI7.DAO.DaoImpl.TransaccionImpl;
using System.Text;
using iText.IO.Font;
using iText.IO.Font.Constants;

namespace proyectoCakeAPI7.Controllers
{
    [RoutePrefix("api/export")]
    public class ExportController : ApiController
    {
        private readonly DulceDaoImpl dulceDao = new DulceDaoImpl();
        private readonly CategoriaDaoImpl categoriaDao = new CategoriaDaoImpl();
        private readonly MarcaDaoImpl marcaDao = new MarcaDaoImpl();
        private readonly PresentacionDaoImpl presentacionDao = new PresentacionDaoImpl();
        private readonly SaborDaoImpl saborDao = new SaborDaoImpl();
        private readonly VentaDaoImpl ventaDao = new VentaDaoImpl();
        private readonly ClienteDaoImpl clienteDao = new ClienteDaoImpl();

        public ExportController()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Estefania");
        }

        // PDF Genérico - Versión mejorada
        private HttpResponseMessage GenerarPDF<T>(List<T> datos, string titulo, string archivo, string[] columnas, Func<T, string[]> extraer)
        {
            HttpResponseMessage response = null; // Declaramos fuera del try
            MemoryStream stream = null;

            try
            {
                stream = new MemoryStream();
                var writer = new PdfWriter(stream);
                writer.SetCloseStream(false);

                using (var pdf = new PdfDocument(writer))
                using (var document = new Document(pdf))
                {
                    PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    document.Add(new Paragraph(titulo)
                        .SetFont(boldFont)
                        .SetFontSize(16)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                    var tabla = new Table(columnas.Length, true);

                    foreach (var col in columnas)
                    {
                        tabla.AddHeaderCell(new Cell()
                            .Add(new Paragraph(col))
                            .SetFont(boldFont));
                    }

                    foreach (var item in datos)
                    {
                        var valores = extraer(item);
                        foreach (var valor in valores)
                        {
                            tabla.AddCell(new Cell().Add(new Paragraph(valor ?? "N/A")));
                        }
                    }

                    document.Add(tabla);
                }

                stream.Position = 0;

                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray())
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = archivo
                };

                return response;
            }
            catch (Exception ex)
            {
                stream?.Dispose();
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al generar PDF: " + ex.Message);
            }
            finally
            {
                // Solo cerramos el stream si hubo error (cuando response es null o no es exitoso)
                if (stream != null && (response == null || !response.IsSuccessStatusCode))
                {
                    stream.Dispose();
                }
            }
        }

        // Excel Genérico - Versión mejorada
        private HttpResponseMessage GenerarExcel<T>(List<T> datos, string hoja, string archivo, string[] columnas, Func<T, object[]> extraer, Color color)
        {
            try
            {
                var stream = new MemoryStream();

                using (var package = new ExcelPackage())
                {
                    var ws = package.Workbook.Worksheets.Add(hoja);

                    // 1. AGREGAR TÍTULO (fila 1)
                    ws.Cells[1, 1].Value = $"Lista de {hoja}";
                    ws.Cells[1, 1, 1, columnas.Length].Merge = true; // Combinar celdas para el título
                    ws.Cells[1, 1].Style.Font.Bold = true;
                    ws.Cells[1, 1].Style.Font.Size = 14;
                    ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // 2. ENCABEZADOS (fila 3)
                    int headerRow = 3;
                    for (int i = 0; i < columnas.Length; i++)
                    {
                        ws.Cells[headerRow, i + 1].Value = columnas[i];
                    }

                    // Estilo para encabezados
                    using (var range = ws.Cells[headerRow, 1, headerRow, columnas.Length])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(color);
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    // 3. DATOS (fila 4 en adelante)
                    int dataRow = headerRow + 1;
                    foreach (var item in datos)
                    {
                        var valores = extraer(item);
                        for (int col = 0; col < valores.Length; col++)
                        {
                            // Formatear fechas y números
                            if (valores[col] is DateTime)
                            {
                                ws.Cells[dataRow, col + 1].Style.Numberformat.Format = "dd/MM/yyyy";
                            }
                            else if (valores[col] is decimal)
                            {
                                ws.Cells[dataRow, col + 1].Style.Numberformat.Format = "#,##0.00";
                            }

                            ws.Cells[dataRow, col + 1].Value = valores[col];
                        }
                        dataRow++;
                    }

                    // 4. AUTO-AJUSTAR COLUMNAS
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    package.SaveAs(stream);
                }

                stream.Position = 0;

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = archivo
                };

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    $"Error al generar Excel: {ex.Message}");
            }
        }

        // Endpoints PDF (sin cambios)
        [HttpGet, Route("dulces")]
        public HttpResponseMessage ExportarDulcesPDF() =>
            GenerarPDF(dulceDao.GetAll(), "Lista de Dulces", "dulces.pdf",
                new[] { "ID", "Nombre", "Precio", "Stock", "ID Sabor" },
                d => new[] { d.idDulce.ToString(), d.nombreDulce, d.precioDulce.ToString("F2"), d.stockDulce.ToString(), d.idSabor.ToString() });

        [HttpGet, Route("categorias")]
        public HttpResponseMessage ExportarCategoriasPDF() =>
            GenerarPDF(categoriaDao.GetAll(), "Lista de Categorías", "categorias.pdf",
                new[] { "ID", "Nombre" },
                c => new[] { c.idCategoria.ToString(), c.nombreCategoria });

        [HttpGet, Route("marcas")]
        public HttpResponseMessage ExportarMarcasPDF() =>
    GenerarPDF(marcaDao.GetAll(), "Lista de Marcas", "marcas.pdf",
        new[] { "ID", "Nombre", "País de Origen" },
        m => new[] { m.idMarca.ToString(), m.nombreMarca, m.paisOrigenMarca });

        [HttpGet, Route("sabores")]
        public HttpResponseMessage ExportarSaboresPDF() =>
        GenerarPDF(saborDao.GetAll(), "Lista de Sabores", "sabores.pdf",
            new[] { "ID", "Nombre" },
            s => new[] { s.idSabor.ToString(), s.nombreSabor });

        [HttpGet, Route("presentaciones")]
        public HttpResponseMessage ExportarPresentacionesPDF() =>
        GenerarPDF(presentacionDao.GetAll(), "Lista de Presentaciones", "presentaciones.pdf",
            new[] { "ID", "Nombre" },
            p => new[] { p.idPresentacion.ToString(), p.nombrePresentacion });

        [HttpGet, Route("clientes")]
        public HttpResponseMessage ExportarClientesPDF() =>
        GenerarPDF(clienteDao.GetAll(), "Lista de Clientes", "clientes.pdf",
            new[] { "ID", "Nombre", "Apellido", "Email", "Teléfono" },
            c => new[] { c.idCliente.ToString(), c.nombreCliente, c.apellidoCliente, c.emailCliente, c.telefonoCliente });

        [HttpGet, Route("ventas")]
        public HttpResponseMessage ExportarVentasPDF() =>
        GenerarPDF(ventaDao.GetAll(), "Lista de Ventas", "ventas.pdf",
            new[] { "ID Venta", "Fecha", "ID Cliente", "ID Usuario" },
            v => new[] { v.idVenta.ToString(), v.fechaVenta.ToString("dd/MM/yyyy"), v.idCliente.ToString(), v.usuarioID.ToString() });


        // Endpoints Excel (sin cambios)
        [HttpGet, Route("excel/dulces")]
        public HttpResponseMessage ExportarDulcesExcel() =>
            GenerarExcel(dulceDao.GetAll(), "Dulces", "dulces.xlsx",
                new[] { "ID", "Nombre", "Precio", "Stock", "ID Sabor" },
                d => new object[] { d.idDulce, d.nombreDulce, d.precioDulce, d.stockDulce, d.idSabor },
                Color.LightPink);

        [HttpGet, Route("excel/categorias")]
        public HttpResponseMessage ExportarCategoriasExcel() =>
        GenerarExcel(categoriaDao.GetAll(), "Categorías", "categorias.xlsx",
            new[] { "ID", "Nombre" },
            c => new object[] { c.idCategoria, c.nombreCategoria },
            Color.LightSkyBlue);

        [HttpGet, Route("excel/marcas")]
        public HttpResponseMessage ExportarMarcasExcel() =>
    GenerarExcel(marcaDao.GetAll(), "Marcas", "marcas.xlsx",
        new[] { "ID", "Nombre", "País de Origen" },
        m => new object[] { m.idMarca, m.nombreMarca, m.paisOrigenMarca },
        Color.LightGreen);

        [HttpGet, Route("excel/sabores")]
        public HttpResponseMessage ExportarSaboresExcel() =>
        GenerarExcel(saborDao.GetAll(), "Sabores", "sabores.xlsx",
            new[] { "ID", "Nombre" },
            s => new object[] { s.idSabor, s.nombreSabor },
            Color.LightSalmon);

        [HttpGet, Route("excel/presentaciones")]
        public HttpResponseMessage ExportarPresentacionesExcel() =>
        GenerarExcel(presentacionDao.GetAll(), "Presentaciones", "presentaciones.xlsx",
            new[] { "ID", "Nombre" },
            p => new object[] { p.idPresentacion, p.nombrePresentacion },
            Color.Plum);

        [HttpGet, Route("excel/clientes")]
        public HttpResponseMessage ExportarClientesExcel() =>
        GenerarExcel(clienteDao.GetAll(), "Clientes", "clientes.xlsx",
            new[] { "ID", "Nombre", "Apellido", "Email", "Teléfono" },
            c => new object[] { c.idCliente, c.nombreCliente, c.apellidoCliente, c.emailCliente, c.telefonoCliente },
            Color.LightCyan);

        [HttpGet, Route("excel/ventas")]
        public HttpResponseMessage ExportarVentasExcel() =>
        GenerarExcel(ventaDao.GetAll(), "Ventas", "ventas.xlsx",
            new[] { "ID Venta", "Fecha", "ID Cliente", "ID Usuario" },
            v => new object[] { v.idVenta, v.fechaVenta.ToString("dd/MM/yyyy"), v.idCliente, v.usuarioID },
            Color.LightYellow);

    }
}