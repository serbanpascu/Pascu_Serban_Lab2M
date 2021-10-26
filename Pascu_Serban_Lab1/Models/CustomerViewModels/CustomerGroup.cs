using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pascu_Serban_Lab1.Models.CustomerViewModels
{
    public class CustomerGroup
    {
        public string CustomerName { get; set; }
        public int BookCount { get; set; }
    }
}