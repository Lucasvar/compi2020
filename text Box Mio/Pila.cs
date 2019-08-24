using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace at.jku.ssw.cc
{
    public class Pila //Manuel
    {
        protected int cantMaxDeElem;
        public int tope;
        protected object[] elementos;
        public Pila(int espacio)
        {
            cantMaxDeElem = espacio;
            elementos = new object[espacio];
            tope = -1;
        }
        public bool estaVacia()
        {
            /*bool retorno;
            if (tope == -1)
                retorno = true;
            else
                retorno = false;
            return retorno;*/
            if (tope == -1) return true; else return false;

        }
        public void push(object elemento)
        {
            if (tope == elementos.Length)
                Console.WriteLine("Nos quedamos sin espacio");
            else
            {
                tope++;
                elementos[tope] = elemento;
            }
        }
        public object pop()
        {
            object retorno;
            if (!estaVacia())
            {
                retorno = elementos[tope];
                elementos[tope] = null;
                tope--;
            }
            else
                retorno = null;
            return retorno;
        }

        public object verElementoTope()
        {
            object retorno;
            if (!estaVacia())
                retorno = elementos[tope];
            else
                retorno = null;
            return retorno;
            
        }
        protected virtual void mostrarPilita() {}
    }
}
