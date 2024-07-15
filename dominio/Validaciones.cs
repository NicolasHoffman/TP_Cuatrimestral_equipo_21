using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.SessionState;

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
        public static bool HayUsuarioEnSesion(HttpSessionState session)
        {
            return session["usuario"] != null;
        }
        public static bool EsUsuarioAdministradorOVendedor(HttpSessionState session)
        {
            if (session["usuario"] != null)
            {
                Usuario usuario = (Usuario)session["usuario"];
                return usuario.tipoUsuario.TipoUsuarioId == 1 || usuario.tipoUsuario.TipoUsuarioId == 2;
            }
            return false;
        }
    }
}
