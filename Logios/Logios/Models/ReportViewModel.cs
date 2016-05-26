using Logios.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class ReportViewModel
    {
        public Exercise Exercise { get; set; }

        [Required(ErrorMessage = "&diams; Debe especificar la causa del reporte.")]
        public string Cause { get; set; }
    }
}