using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sem2Lab1SQLServer.Utilities
{
    public class ValidСontinetNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            List<string> names = new List<string>();
            names.Add("Євразія"); names.Add("Африка"); names.Add("Австралія"); names.Add("Антарктида");
            names.Add("Північна Америка"); names.Add("Південна Америка");
            return names.Contains(value);
        }
    }
}
