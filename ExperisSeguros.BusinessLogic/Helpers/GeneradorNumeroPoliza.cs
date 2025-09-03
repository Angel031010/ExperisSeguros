using System;
using System.Collections.Generic;
using System.Text;

namespace ExperisSeguros.BusinessLogic.Helpers
{
    public static class GeneradorNumeroPoliza
    {
        public static string Generar()
        {
            var fecha = DateTime.Now.ToString("yyyyMMdd");
            var random = new Random().Next(1000, 9999);
            return $"POL-{fecha}-{random}";
        }
    }
}
