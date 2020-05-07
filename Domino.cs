using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDomino
{
    public class Domino
    {
        private Random aleatorio = new Random();

        public List<Jugador> Jugadores { get; set; }
        public List<Ficha> FichasDisponibles { get; set; }
        public ModalidadJuego ModalidadJuego { get; set; }
        public LinkedList<Ficha> Tablero { get; set; }

        public Domino()
        {
            FichasDisponibles = GenerarFichas();
            Tablero = new LinkedList<Ficha>();
        }

        public void IniciarJuego()
        {
            foreach (var jugador in Jugadores)
            {
                EntregarFichas(jugador, FichasDisponibles, 7);
            }
        }

        /// <summary>
        /// Genera una lista con las 28 fichas del domino
        /// </summary>
        /// <returns>Una lista que contiene 28 objetos de tipo Ficha</returns>
        public List<Ficha> GenerarFichas()
        {
            List<Ficha> fichasOriginales = new List<Ficha>();
            for (int i = 0; i <= 6; i++)
            {
                for (int j = i; j <= 6; j++)
                {
                    fichasOriginales.Add(new Ficha()
                    {
                        primerNumero = i,
                        segundoNumero = j,
                        esDoble = (i == j) ? true : false
                    });
                }
            }
            return fichasOriginales;
        }

        /// <summary>
        /// Entrega fichas aleatorias al jugador que necesite fichas
        /// </summary>
        /// <param name="jugador">Un objeto de tipo jugador al que se desea entregar fichas</param>
        /// <param name="fichasDisponibles">Una lista de objetos de tipo ficha que contengan las fichas disponibles restantes</param>
        /// <param name="numeroFichas">Un entero conteniendo el numero de fichas que se desean entregar</param>
        public void EntregarFichas(Jugador jugador, List<Ficha> fichasDisponibles, int numeroFichas)
        {
            aleatorio = new Random();
            for (int i = 0; i < numeroFichas && fichasDisponibles.Count > 0; i++)
            {
                int indice = aleatorio.Next(fichasDisponibles.Count);
                jugador.FichasDisponibles.Add(fichasDisponibles[indice]);
                fichasDisponibles.Remove(fichasDisponibles[indice]);
            }
        }

        /// <summary>
        /// Valora las fichas de los jugadores y escoje cual jugador empezará el juego
        /// </summary>
        /// <returns>Un objeto de tipo jugador que empezará la partida</returns>
        public Jugador ElegirJugadorInicial()
        {
            Jugador primerJugador = null;
            foreach (var jugador in Jugadores)
            {
                var mayorDoble = jugador.ObtenerMayorDoble();
                if (mayorDoble != null) {
                    if (primerJugador == null) {
                        primerJugador = jugador;
                    }
                    if (mayorDoble.ObtenerPuntaje() > primerJugador.ObtenerMayorDoble().ObtenerPuntaje()) {
                        primerJugador = jugador;
                    }
                }
            }

            if (primerJugador == null) {
                primerJugador = Jugadores.First();
                foreach (var jugador in Jugadores)
                {
                    var mayorFicha = jugador.ObtenerMayorFicha();
                    if (mayorFicha.ObtenerPuntaje() > primerJugador.ObtenerMayorFicha().ObtenerPuntaje())
                    {
                        primerJugador = jugador;
                    } else if (mayorFicha.ObtenerPuntaje() == primerJugador.ObtenerMayorFicha().ObtenerPuntaje()) {
                        if (mayorFicha.ObtenerMayorNumero() > primerJugador.ObtenerMayorFicha().ObtenerMayorNumero()) {
                            primerJugador = jugador;
                        }
                    }
                }
            }

            return primerJugador;
        }

        /// <summary>
        /// Ordena a los jugadores en una cola priorizando al primer jugador y los demas de
        /// forma aleatoria
        /// </summary>
        /// <returns>Una cola con los jugadores ordenados para iniciar el juego</returns>
        public Queue<Jugador> ObtenerOrdenJugadores() {
            Queue<Jugador> jugadoresOrdenados = new Queue<Jugador>();
            var jugador = ElegirJugadorInicial();
            jugadoresOrdenados.Enqueue(jugador);
            Jugadores.Remove(jugador);
            while (Jugadores.Count>0) {
                jugador = Jugadores[aleatorio.Next(Jugadores.Count)];
                jugadoresOrdenados.Enqueue(jugador);
                Jugadores.Remove(jugador);
            }
            return jugadoresOrdenados;
            
        }

        //Muestra el tablero
        public override string ToString()
        {
            string texto = "Tablero:\n";

            foreach (var ficha in Tablero)
            {
                texto += ficha.ToString();
            }
            return texto;
        }


        //Muestra la cantidad de numeros jugados
        public string ObtenerEstadisticas()
        {
            string texto = "Estadisticas de las fichas del tablero(Numero de ficha|cantidad de fichas): \n";
            for (int i = 0; i < 7; i++)
            {
                int contador = 0;
                foreach (var ficha in Tablero)
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
