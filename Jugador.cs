using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDomino
{
    public class Jugador
    {
        public Jugador()
        {
            FichasDisponibles = new List<Ficha>();
        }

        public string Nombre { get; set; }
        public List<Ficha> FichasDisponibles { get; set; }

        /// <summary>
        /// Busca entre las fichas del jugador la doble con el mayor valor
        /// </summary>
        /// <returns>Un objeto de tipo ficha con el doble encontrado o null si no se pudo encontrar</returns>
        public Ficha ObtenerMayorDoble() {
            Ficha mayorDoble = new Ficha();
            foreach (var ficha in FichasDisponibles)
            {
                if (ficha.esDoble && (ficha.ObtenerPuntaje() > mayorDoble.ObtenerPuntaje()))
                    mayorDoble = ficha;
            }
            if (mayorDoble.esDoble) {
                return mayorDoble;
            }
            return null;
        }

        /// <summary>
        /// Busca entre las fichas del jugador la ficha con el mayor valor
        /// </summary>
        /// <returns>Un objeto de tipo ficha que corresponde al mayor valor</returns>
        public Ficha ObtenerMayorFicha()
        {
            Ficha mayor = new Ficha();
            foreach (var ficha in FichasDisponibles)
            {
                if (ficha.ObtenerPuntaje() >= mayor.ObtenerPuntaje())
                {

                    if (ficha.ObtenerPuntaje() > mayor.ObtenerPuntaje())
                    {
                        mayor = ficha;
                    }
             //Si el puntaje es igual, devuelve el mayor numero
                    else{
                        if (ficha.ObtenerMayorNumero() > mayor.ObtenerMayorNumero()) {
                            mayor = ficha;
                        }
                    }
                }
                    
            }
            return mayor;
        }

        public override string ToString()
        {
            return $"Jugador: {Nombre}";
           
        }

        public string FichasDisponiblesTostring() {
            string texto = "Fichas disponibles:\n";
            for (int i = 0; i < FichasDisponibles.Count - 1; i++)
            {
                texto += $"{i+1}) {FichasDisponibles[i].ToString()}\n";
            }
            if (FichasDisponibles.Count > 0)
                texto += $"{FichasDisponibles.Count}) {FichasDisponibles[FichasDisponibles.Count - 1].ToString()}";
            return texto;
        }

        public int ObtenerPuntajeTotal() {
            int puntaje = 0;
            foreach (var ficha in FichasDisponibles)
            {
                puntaje += ficha.ObtenerPuntaje();
            }
            return puntaje;
        }

        public string ObtenerEstadisticas() {
            string texto = "Estadisticas de las fichas del jugador(Numero de ficha|cantidad de fichas): \n";
            for (int i = 0; i < 7; i++)
            {
                int contador = 0;
                foreach (var ficha in FichasDisponibles)
                {
                    if (ficha.primerNumero == i || ficha.segundoNumero == i)
                        contador++;
                }
                texto += $"({i}|{contador}) ";
            }
            return texto;
        }
    }
}
