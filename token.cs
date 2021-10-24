using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Token
    {
        public Token(string nombre, string lexema, int id)
        {
            this.nombre = nombre;
            this.lexema = lexema;
            this.id = id;
        }
        public Token()
        {
            this.nombre = "";
            this.lexema = "";
            this.id = 0;
        }
        public Token(int id)
        {
            this.nombre = "";
            this.lexema = "";
            this.id = id;
        }
        string lexema, nombre;
        int id;

        public string Lexema { get => lexema; set => lexema = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Id { get => id; set => id = value; }
    }
}
