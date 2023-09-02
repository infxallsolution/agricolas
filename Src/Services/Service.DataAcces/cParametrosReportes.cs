using Service.DataAcces.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataAcces
{
    public class cParametrosReportes
    {
        private string usuario;
        private string contraseña;
        private string dominio;

        public string Usuario { get => usuario; set => usuario = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string Dominio { get => dominio; set => dominio = value; }

        public cParametrosReportes()
        {
            Usuario = Settings.Default.usuarioReportes;
            Contraseña = Settings.Default.password;
            Dominio = Settings.Default.dominio;

        }
    }
}
