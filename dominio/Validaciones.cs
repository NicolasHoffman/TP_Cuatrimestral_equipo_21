using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dominio
{
    public static class Validaciones
    {
        public static bool SoloNumeros(string valor)
        {
            return Regex.IsMatch(valor, @"^[0-9]+$");
        }

        public static bool SoloLetras(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$");
        }

        public static bool PuedeEstarVacia(string valor)
        {
            return string.IsNullOrEmpty(valor.Trim());
        }

        public static bool NoPuedeEstarVacia(string valor)
        {
            return !string.IsNullOrEmpty(valor.Trim());
        }
        public static bool EsSoloPositivo(int valor)
        {
            return valor >= 0;
        }

    }
}
