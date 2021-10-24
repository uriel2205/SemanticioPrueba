using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ElementoTS
    {
        public string id;
        public char tipo;
        public string ambito;
        public string parametros;
        public ElementoTS(string id, char tipo, string ambito, string stpara)
        {
            this.id = id;
            this.tipo = tipo;
            this.ambito = ambito;
            this.parametros = stpara;
        }
        public ElementoTS(string id, char tipo, string ambito)
        {
            this.id = id;
            this.tipo = tipo;
            this.ambito = ambito;
            parametros = "";
        }
    }
}
