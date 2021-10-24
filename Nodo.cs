using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Nodo
    {
        public Nodo Sig { get; set; }
        public Token Token { get; set; }
        public string Ambito { get; set; }
        public char TipoDato { get; set; }
        public string CadenaParametros { get; set; }
        public Nodo(Nodo siguiente, Token token, string ambito)
        {
            Ambito = ambito;
            Token = token;
            Sig = siguiente;
            CadenaParametros = "";
            TipoDato = '\0';
        }
        public Nodo()
        {
            Ambito = "";
            Token = new Token();
            Sig = null;
            CadenaParametros = "";
            TipoDato = '\0';
        }
        public Nodo(Token token)
        {
            Ambito = "";
            Token = token;
            Sig = null;
            CadenaParametros = "";
            TipoDato = '\0';
        }
        public Nodo(Nodo nodo)
        {
            Ambito = nodo.Ambito;
            Token = nodo.Token;
            Sig = nodo.Sig;
            CadenaParametros = "";
            TipoDato = '\0';
        }
        public char GetTipoDato(string s)
        {
            if (s == "int") return 'i';
            else if (s == "float") return 'f';
            else if (s == "char") return 'c';
            return 'v';
        }
        public virtual void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            if (Sig != null) Sig.ValidaTipo(tablaSimbolos, errores);
            if (Token != null)
            {
                if (Token.Nombre != "null")
                {
                    if(EsInt(Token.Lexema)) TipoDato = 'i';
                    else if (EsFloat(Token.Lexema)) TipoDato = 'f';
                    else TipoDato = BuscarTablaSimbolos(tablaSimbolos, Token.Lexema, Ambito);
                }
            }

        }
        #region Funciones de ayuda validar int/float/esnum/buscartipodato
        public char BuscarTablaSimbolos(List<ElementoTS> tablaSimbolos, string id, string ambito)
        {
            foreach (ElementoTS e in tablaSimbolos) if (e.id == id && e.ambito == ambito)return e.tipo;
            return 'e';
        }

        public char BuscarTablaSimbolos(List<ElementoTS> tablaSimbolos, string id)
        {
            foreach (ElementoTS e in tablaSimbolos) if (e.id == id) return e.tipo;
            return 'e';
        }
        
        public bool EsInt(string cadena)
        {
            if (cadena == "") return false;
            foreach (char c in cadena) if (!EsNum(c)) return false;
            return true;
        }

        public bool EsFloat(string cadena)
        {
            if (cadena == "") return false;
            bool punto = false;
            foreach (char c in cadena)
            {
                if (EsNum(c)) continue;
                else if (c == '.')
                {
                    if (!punto) punto = true;
                    else return false;
                }
                else return false;
            }
            return true;
        }

        bool EsNum(char x)
        {
            return (x >= '0' && x <= '9');
        }

        #endregion
    }

    #region No Terminales
    //listo
    public class DefVar : Nodo
    {
        string tipo;
        Id id;
        ListaVar listavar;
        public DefVar(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            pila.Pop();//;
            pila.Pop();//estado
            if (pila.Peek().Token.Nombre == "null")
            {
                pila.Pop();
                listavar = null;
            }
            else
                listavar = (ListaVar)pila.Pop();//listavar
            id = new Id(pila);
            pila.Pop();//estado
            tipo = pila.Pop().Token.Lexema;
        }

        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            TipoDato = GetTipoDato(tipo);
            if(BuscarTablaSimbolos(tablaSimbolos, id.Token.Lexema, Ambito) != 'e')
            {
                errores.Enqueue("La variable: " + id.Token.Lexema + " en: " + Ambito + " ya esta declarada");
            }
            else
                tablaSimbolos.Add(new ElementoTS(id.Token.Lexema, TipoDato, Ambito, ""));
            if (listavar != null)
            {
                listavar.Ambito = Ambito;
                listavar.TipoDato = GetTipoDato(tipo);
                listavar.ValidaTipo(tablaSimbolos, errores);
            }
            if (Sig != null)
            {
                Sig.Ambito = Ambito;
                Sig.ValidaTipo(tablaSimbolos, errores);
            }
        }
    }
    //listo
    public class DefFunc : Nodo
    {
        string tipo; 
        Nodo BloqFunc;
        Id id;
        Parametros parametros;
        public DefFunc(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            BloqFunc = pila.Pop();//bloqfunc
            pila.Pop();//estado
            pila.Pop();//)
            pila.Pop();//estado
            if (pila.Peek().Token.Nombre == "null")
            {
                pila.Pop();
                parametros = null;
            }
            else
            {
                parametros = (Parametros)pila.Pop();//parametros
            }
            pila.Pop();//estado
            pila.Pop();//(
            id = new Id(pila);
            pila.Pop();//estado
            tipo = pila.Pop().Token.Lexema;
        }
        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            TipoDato = GetTipoDato(tipo);
            char TipoDtemp = TipoDato;
            string nombre = id.Token.Lexema;
            Ambito = nombre;
            if (parametros != null)
            {
                parametros.Ambito = Ambito;
                parametros.ValidaTipo(tablaSimbolos, errores);
                CadenaParametros = parametros.CadenaParametros;
            }

            if (BuscarTablaSimbolos(tablaSimbolos, nombre, "global") != 'e')
                errores.Enqueue(" La funcion: " + nombre + " ya existe.");
            else
                tablaSimbolos.Add(new ElementoTS(nombre, TipoDtemp, "global", CadenaParametros));

            CadenaParametros = "";
            if (BloqFunc != null)
            {
                BloqFunc.Ambito = Ambito;
                BloqFunc.ValidaTipo(tablaSimbolos, errores);
            }
            Ambito = "";
            if (Sig != null)
            {
                Sig.Ambito = Ambito;
                Sig.ValidaTipo(tablaSimbolos, errores);
            }
        }

    }
    //listo
    public class Parametros : Nodo
    {
        string tipo;
        Id id;
        ListaParam listaParam;

        public Parametros(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            if (pila.Peek().Token.Nombre == "null")
            {
                pila.Pop();
                listaParam = null;
            }
            else
            {
                listaParam = (ListaParam)pila.Pop();//parametros
            }
            id = new Id(pila);
            pila.Pop();//estado
            tipo = pila.Pop().Token.Lexema;//tipo
        }

        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            TipoDato = GetTipoDato(tipo);
            string nombre = id.Token.Lexema;
            if (BuscarTablaSimbolos(tablaSimbolos, nombre, Ambito) != 'e')
                errores.Enqueue("La variable: " + nombre + " ya fue declarada");
            else
                tablaSimbolos.Add(new ElementoTS(nombre, TipoDato, Ambito, ""));
            CadenaParametros += tipo[0];
            if (listaParam != null)
            {
                listaParam.Ambito = Ambito;
                listaParam.CadenaParametros = CadenaParametros;
                listaParam.ValidaTipo(tablaSimbolos, errores);
                CadenaParametros = listaParam.CadenaParametros;
            }
            if (Sig != null)
            {
                Sig.Ambito = Ambito;
                Sig.ValidaTipo(tablaSimbolos, errores);
            }
        }
    }
    //listo
    public class ListaVar : Nodo
    {
        Id id;
        ListaVar listavar;

        public ListaVar(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            if (pila.Peek().Token.Nombre == "null")
            {
                pila.Pop();
                listavar = null;
            }
            else
            {
                listavar = (ListaVar)pila.Pop();//listavar
            }
            id = new Id(pila);
            pila.Pop();//estado
            pila.Pop();//,
        }

        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            if (BuscarTablaSimbolos(tablaSimbolos, id.Token.Lexema, Ambito) != 'e')
            {
                errores.Enqueue("La variable: " + id.Token.Lexema + " en: " + Ambito + " ya esta declarada");
            }
            else
                tablaSimbolos.Add(new ElementoTS(id.Token.Lexema, TipoDato, Ambito, ""));
            if (listavar != null)
            {
                listavar.Ambito = Ambito;
                listavar.TipoDato = TipoDato;
                listavar.ValidaTipo(tablaSimbolos, errores);
            }
            if (Sig != null)
            {
                Sig.Ambito = Ambito;
                Sig.ValidaTipo(tablaSimbolos, errores);
            }
        }
    }
    //listo
    public class ListaParam : Nodo
    {
        string tipo;
        Id id;
        ListaParam listaParam;

        public ListaParam(Stack<Nodo>  pila)
        {
            pila.Pop();//estado
            if (pila.Peek().Token.Nombre == "null")
            {
                pila.Pop();
                listaParam = null;
            }
            else
            {
                listaParam = (ListaParam)pila.Pop();//parametros
            }
            id = new Id(pila);//id
            pila.Pop();//estado
            tipo = pila.Pop().Token.Lexema;//tipo
            pila.Pop();//estado
            pila.Pop();//,
        }

        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            TipoDato = GetTipoDato(tipo);
            string nombre = id.Token.Lexema;
            if (BuscarTablaSimbolos(tablaSimbolos, nombre, Ambito) != 'e')
                errores.Enqueue("La variable: " + nombre + " ya fue declarada");
            else
                tablaSimbolos.Add(new ElementoTS(nombre, TipoDato, Ambito, ""));
            CadenaParametros += tipo[0];
            if (listaParam != null)
            {
                listaParam.Ambito = Ambito;
                listaParam.CadenaParametros = CadenaParametros;
                listaParam.ValidaTipo(tablaSimbolos, errores);
                CadenaParametros = listaParam.CadenaParametros;
            }
            if (Sig != null)
            {
                Sig.Ambito = Ambito;
                Sig.ValidaTipo(tablaSimbolos, errores);
            }
        }

    }
    //listo
    public class Asignacion : Nodo
    {
        Id id;
        Operacion expresion;
        public Asignacion(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            pila.Pop();//;
            expresion = new Operacion(pila);//expresion
            pila.Pop();//estado
            pila.Pop();//=
            id = new Id(pila);//id
        }
        //Esta se crea con el objetivo de validar los diferentes tipos que se puede presentar en la semantica.
        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            TipoDato = BuscarTablaSimbolos(tablaSimbolos, id.Token.Lexema, Ambito);
            char tipo1 = TipoDato;
            if (TipoDato == 'e') errores.Enqueue("La variable " + id.Token.Lexema + " no ha sido declarada");
            if (expresion != null)
            {
                expresion.Ambito = Ambito;
                expresion.TipoDato = tipo1;
                expresion.ValidaTipo(tablaSimbolos, errores);
            }
            char tipo2 = expresion.TipoDato;
            if (tipo1 != tipo2) errores.Enqueue("El tipo de dato de " + id.Token.Lexema+ " en la funcion " + Ambito + " es diferente de la expresion.");
            if (Sig != null)
            {
                Sig.Ambito = Ambito;
                Sig.ValidaTipo(tablaSimbolos, errores);
            }
        }
    }
    //listo
    public class ClaseIf : Nodo
    {
        string _if; 
        Nodo SentenciaBloque;
        Nodo Otro;
        Operacion expresion; 

        public ClaseIf(Stack<Nodo>pila)
        {
            pila.Pop();//estado
            if (pila.Peek().Token.Nombre == "null")
            {
                Otro = null;
                pila.Pop();
            }
            else Otro = pila.Pop();//otro
            pila.Pop();//estado
            if (pila.Peek().Token.Nombre == "null")
            {
                SentenciaBloque = null;//sentenciabloque
                pila.Pop();
            }
            else SentenciaBloque = pila.Pop();//sentenciabloque
            pila.Pop();//estado
            pila.Pop();//)
            expresion = new Operacion(pila);//Expresion
            pila.Pop();//estado
            pila.Pop();//(
            pila.Pop();//estado
            _if = pila.Pop().Token.Lexema;//if
        }

        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            if(expresion != null)
            {
                expresion.Ambito = Ambito;
                expresion.ValidaTipo(tablaSimbolos, errores);
            }
            if (SentenciaBloque != null)
            {
                SentenciaBloque.Ambito = Ambito;
                SentenciaBloque.ValidaTipo(tablaSimbolos, errores);
            }
            if (Otro != null)
            {
                Otro.Ambito = Ambito;
                Otro.ValidaTipo(tablaSimbolos, errores);
            }
            if (Sig != null)
            {
                Sig.Ambito = Ambito;
                Sig.ValidaTipo(tablaSimbolos, errores);
            }
        }
    }
    //listo
    public class ClaseWhile : Nodo
    {
        string _while;
        Nodo Bloque;
        Operacion expresion;

        public ClaseWhile(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            if (pila.Peek().Token.Nombre == "null")
            {
                pila.Pop();
                Bloque = null;
            }
            else Bloque = pila.Pop();
            pila.Pop();//estado
            pila.Pop();//)
            expresion = new Operacion(pila);//Expresion
            pila.Pop();//estado
            pila.Pop();//(
            pila.Pop();//estado
            _while = pila.Pop().Token.Lexema;//while
        }

        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            if (expresion != null)
            {
                expresion.Ambito = Ambito;
                expresion.ValidaTipo(tablaSimbolos, errores);
            }
            if (Bloque != null)
            {
                Bloque.Ambito = Ambito;
                Bloque.ValidaTipo(tablaSimbolos, errores);
            }
            if (Sig != null)
            {
                Sig.Ambito = Ambito;
                Sig.ValidaTipo(tablaSimbolos, errores);
            }
        }
    }
    //listo
    public class ClaseReturn : Nodo
    {
        Operacion expresion;
        public ClaseReturn(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            pila.Pop();//;
            if(pila.Peek().Token.Nombre == "null")
            {
                pila.Pop();
                expresion = null;
            }
            else
                expresion = new Operacion(pila);//expresion
            pila.Pop();//estado
            pila.Pop();//return
        }

        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            if (expresion.Token != null)
            {
                expresion.Ambito = Ambito;
                expresion.ValidaTipo(tablaSimbolos, errores);
                TipoDato = expresion.TipoDato;
            }
            char tipodatofuncion = BuscarTablaSimbolos(tablaSimbolos, Ambito);
            if (TipoDato!= tipodatofuncion)
                errores.Enqueue("El tipo de dato que regresa " + expresion.derecha.Token.Lexema + " no es el mismo que el de la funcion " + Ambito);
        }
    }
    //listo
    public class Id : Nodo
    {
        string lexema;
        public Id(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            Token = pila.Pop().Token;//id
            lexema = Token.Lexema;
        }

    }
    //listo
    public class Constante : Nodo
    {
        string lexema;
        public Constante(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            Token = pila.Pop().Token;//constante
            lexema = Token.Lexema;
        }
    }
    //listo
    public class LlamadaFunc : Nodo
    {
        Id id;
        Nodo argumentos;
        public LlamadaFunc(Stack<Nodo> pila)
        {
            pila.Pop();//estado
            pila.Pop();//(
            pila.Pop();//estado
            if (pila.Peek().Token.Nombre == "null")
            {
                argumentos = null;
                pila.Pop();
            }
            else argumentos = pila.Pop();//argumentos
            pila.Pop();//estado
            pila.Pop();//)
            id = new Id(pila);
        }

        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            Nodo aux = argumentos;
            TipoDato = BuscarTablaSimbolos(tablaSimbolos, id.Token.Lexema);
            string cadenaParametros = "";
            if (aux.Token != null)
            {
                while (aux != null)
                {
                    if (aux.Token.Nombre != "null")
                    {
                        if (EsInt(aux.Token.Lexema)) cadenaParametros += 'i';
                        else if (EsFloat(aux.Token.Lexema)) cadenaParametros += 'f';
                        else cadenaParametros += BuscarTablaSimbolos(tablaSimbolos, aux.Token.Lexema, Ambito);
                    }
                    aux = aux.Sig;
                }
            }
            if (existefunc(tablaSimbolos, new ElementoTS(id.Token.Lexema, '\0', Ambito, cadenaParametros), errores))
            {
                id.Ambito = Ambito;
                id.ValidaTipo(tablaSimbolos, errores);
            }
            if (Sig != null)
            {
                Sig.Ambito = Ambito;
                Sig.ValidaTipo(tablaSimbolos, errores);
            }
        }

        public bool existefunc(List<ElementoTS> tablaSimbolos, ElementoTS buscado, Queue<string> errores)
        {
            bool existe = false;
            foreach (ElementoTS e in tablaSimbolos)
            {
                if (e.id == buscado.id)
                {
                    existe = true;
                    if (e.cadenaParametros == buscado.cadenaParametros)
                        return true;
                    else
                    {
                        errores.Enqueue("Los parametros para la funcion " + buscado.id + " en la funcion " + Ambito + " son incorrectos");
                        return true;
                    }
                }
            }
            if (!existe) errores.Enqueue("La funcion " + buscado.id + " no existe.");
            return false;
        }
    }
    //listo
    public class Operacion : Nodo
    {
        string operador;
        public Nodo izquierda, derecha;
        
        public Operacion(Stack<Nodo> pila)
        {
            izquierda = derecha = null;
            this.Token.Nombre = "Operacion";

            pila.Pop();//estado
            derecha = pila.Pop();
            Nodo temp = pila.Pop();//estado
            if (pila.Peek().Token.Lexema != "=" && pila.Peek().Token.Lexema != "return" && pila.Peek().Token.Lexema != "(" && pila.Peek().Token.Lexema != ",")
            {
                operador = pila.Pop().Token.Lexema;//operador
                pila.Pop();//estado
                izquierda = pila.Pop();
            }
            else pila.Push(temp);
        }
        public override void ValidaTipo(List<ElementoTS> tablaSimbolos, Queue<string> errores)
        {
            char tipodato2, tipodato3;//si el tipo dato es '\0' entonces no hay que validar que coincida
            derecha.Ambito = Ambito;
            derecha.TipoDato = TipoDato;
            derecha.ValidaTipo(tablaSimbolos, errores);
            tipodato3 = derecha.TipoDato;
            if (tipodato3 == 'e')
            {
                if(derecha.Token.Nombre != "Operacion" && derecha.Token.Nombre != "")
                    errores.Enqueue("La variable: " + derecha.Token.Lexema + " no ha sido declarada");
            }
            if (izquierda != null)//tiene izquierda
            {
                izquierda.Ambito = Ambito;
                izquierda.TipoDato = TipoDato;
                izquierda.ValidaTipo(tablaSimbolos, errores);
                tipodato2 = izquierda.TipoDato;
                if (tipodato2 == 'e')
                {
                    if (izquierda.Token.Nombre != "Operacion" && derecha.Token.Nombre != "")
                        errores.Enqueue("La variable: " + izquierda.Token.Lexema + " no ha sido declarada");
                }
                if (tipodato2 == tipodato3 && tipodato2 != 'e') {
                        if (Sig != null) Sig.ValidaTipo(tablaSimbolos, errores);
                }
                else
                {
                    string der, izq;
                    if (derecha.Token.Nombre != "Operacion" && derecha.Token.Nombre != "") der = derecha.Token.Lexema;
                    else der = "Operacion derecha";
                    if (izquierda.Token.Nombre != "Operacion" && izquierda.Token.Nombre != "") izq = izquierda.Token.Lexema;
                    else izq = "Operacion izquieda";
                    errores.Enqueue("Los tipos de datos entre " + izq + " y " + der + " son distintos");
                    tipodato3 = 'e';
                }
            }
            TipoDato = tipodato3;
        }
    }

    #endregion

    #region Elemento tabla simbolos

    public class ElementoTS
    {
        public string id { get; set; }
        public char tipo { get; set; }
        public string cadenaParametros { get; set; }
        public string ambito { get; set; }

        public ElementoTS(string id, char tipo, string ambito, string cadenaParametros)
        {
            this.id = id;
            this.tipo = tipo;
            this.ambito = ambito;
            this.cadenaParametros = cadenaParametros;
        }
    }
    #endregion
}
