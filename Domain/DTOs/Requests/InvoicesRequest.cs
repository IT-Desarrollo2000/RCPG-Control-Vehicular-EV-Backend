using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class InvoicesRequest
    {
        public string? Folio { get; set; }
        public IFormFile? FilePath1 { get; set; }
        public IFormFile? FilePath2 { get; set; }
        public DateTime? InvoicedDate { get; set; }
    }

    public class InvoicesUpdate
    {
        public string? Folio { get; set; }
        public DateTime? InvoicedDate { get; set; }
    }
}

