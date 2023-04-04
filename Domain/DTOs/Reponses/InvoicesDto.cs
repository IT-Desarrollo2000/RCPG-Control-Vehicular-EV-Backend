using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class InvoicesDto
    {
        public int? ExpensesId { get; set; }
        public int Id { get; set; }
        public string Folio { get; set; }
        public string FilePath1 { get; set; }
        public string FileURL1 { get; set; }
        public string FilePath2 { get; set; }
        public string FileURL2 { get; set; }
        public DateTime InvoicedDate { get; set; }
    }
}
