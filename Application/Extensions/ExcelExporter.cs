using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public class ExcelExporter
    {
        public static byte[] ExportToExcel<T>(List<T> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ExportedData");
                worksheet.Cells.LoadFromCollection(data, true);

                return package.GetAsByteArray();
            }
        }
    }
}
