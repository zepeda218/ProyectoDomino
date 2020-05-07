using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDomino
{
    public class Program
    {
        static int turno = 1;
        static Domino domino;

        static void Main(string[] args)
        {
            bool ganaSinFichas = false;
            domino = new Domino();

            domino.ModalidadJuego = (ModalidadJuego)MenuPrincipal();
            if (domino.ModalidadJuego == ModalidadJuego.Salir)
                return;
            domino.Jugadores = NombrarJugadores(SolicitarNumeroJugadores());
            

            //Test
            
            domino.IniciarJuego();

            Queue<Jugador> jugadores = domino.ObtenerOrdenJugadores();

            int pasar = 0;
            while (pasar < jugadores.Count)
            {
                while (true)
                {
                    int opcion = MenuTurno(jugadores.Peek());
                    if (opcion < 22)
                    {
                        if (domino.Tablero.Count == 0
                            || jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero == domino.Tablero.First.Value.primerNumero
                            || jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero == domino.Tablero.First.Value.primerNumero
                            || jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero == domino.Tablero.Last.Value.segundoNumero
                            || jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero == domino.Tablero.Last.Value.segundoNumero)
                        {
                            if (domino.Tablero.Count == 0)
                            {
                                domino.Tablero.AddFirst(jugadores.Peek().FichasDisponibles[opcion - 1]);
                            }
                            //Validacion para solicitar lado al jugador
                            else if ((jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero == domino.Tablero.First.Value.primerNumero
                              || jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero == domino.Tablero.First.Value.primerNumero)
                              && (jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero == domino.Tablero.Last.Value.segundoNumero
                              || jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero == domino.Tablero.Last.Value.segundoNumero))
                            {
                                int lado = SolicitarLado(jugadores.Peek(), opcion - 1);
                                if (lado == 1)
                                {
                                    if (jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero == domino.Tablero.First.Value.primerNumero)
                                    {
                                        int numeroAuxiliar = jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero;
                                        jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero = jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero;
                                        jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero = numeroAuxiliar;
                                    }
                                    domino.Tablero.AddFirst(jugadores.Peek().FichasDisponibles[opcion - 1]);
                                }
                                else
                                {
                                    if (jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero == domino.Tablero.Last.Value.segundoNumero)
                                    {
                                        int numeroAuxiliar = jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero;
                                        jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero = jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero;
                                        jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero = numeroAuxiliar;
                                    }
                                    domino.Tablero.AddLast(jugadores.Peek().FichasDisponibles[opcion - 1]);
                                }
                            }
                            else
                            {
                                if (jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero == domino.Tablero.First.Value.primerNumero)
                                {
                                    int numeroAuxiliar = jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero;
                                    jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero = jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero;
                                    jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero = numeroAuxiliar;
                                    domino.Tablero.AddFirst(jugadores.Peek().FichasDisponibles[opcion - 1]);
                                }
                                else if (jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero == domino.Tablero.First.Value.primerNumero)
                                {
                                    domino.Tablero.AddFirst(jugadores.Peek().FichasDisponibles[opcion - 1]);
                                }
                                else if (jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero == domino.Tablero.Last.Value.segundoNumero)
                                {
                                    domino.Tablero.AddLast(jugadores.Peek().FichasDisponibles[opcion - 1]);
                                }
                                else if (jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero == domino.Tablero.Last.Value.segundoNumero)
                                {
                                    int numeroAuxiliar = jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero;
                                    jugadores.Peek().FichasDisponibles[opcion - 1].primerNumero = jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero;
                                    jugadores.Peek().FichasDisponibles[opcion - 1].segundoNumero = numeroAuxiliar;
                                    domino.Tablero.AddLast(jugadores.Peek().FichasDisponibles[opcion - 1]);
                                }
                            }
                            jugadores.Peek().FichasDisponibles.Remove(jugadores.Peek().FichasDisponibles[opcion - 1]);
                            pasar = 0;
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"La ficha {jugadores.Peek().FichasDisponibles[opcion - 1].ToString()} no puede ser colocada en ningún extremo del tablero");
                        }

                    }
                    else if (opcion == 22)
                    {
                        domino.EntregarFichas(jugadores.Peek(), domino.FichasDisponibles, 1);
                    }
                    else {
                        pasar++;
                        break;
                    }
                    
                }
                if (jugadores.Peek().FichasDisponibles.Count == 0)
                {
                    ganaSinFichas = true;
                    break;
                }
                else {
                    jugadores.Enqueue(jugadores.Dequeue());
                }
                turno++;
            }

            Jugador ganador = jugadores.Peek();
            if (!ganaSinFichas)
            {
                foreach (var jugador in jugadores)
                {
                    if (jugador.ObtenerPuntajeTotal() < ganador.ObtenerPuntajeTotal())
                    {
                        ganador = jugador;
                    }
                }
            }

            //Remueve al ganador de la lista para sumar los puntajes
            while (true) {
                if (jugadores.Peek() == ganador)
                {
                    jugadores.Dequeue();
                    break;
                }
                jugadores.Enqueue(jugadores.Dequeue());
            }

            Console.WriteLine($"El ganador es el jugador \"{ganador.Nombre}\"");
            Console.WriteLine($"Gana con un puntaje total de {ganador.ObtenerPuntajeTotal()} puntos");
            int puntosGanados = 0;
            if (domino.ModalidadJuego == ModalidadJuego.Modalidad5)
            {
                Console.WriteLine("Por la modalidad 5 de domino se redondean los puntos de los jugadores al multiplo de cinco mas cercano");
                foreach (var jugador in jugadores)
                {
                    puntosGanados += 5 * (int)Math.Round(jugador.ObtenerPuntajeTotal() / 5.0);
                }
            }
            else
            {
                foreach (var jugador in jugadores)
                {
                    puntosGanados += jugador.ObtenerPuntajeTotal();
                }
            }
            Console.WriteLine($"Se otorgan los puntos de los otros jugadores para un puntaje total de {ganador.ObtenerPuntajeTotal()+ puntosGanados}");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("              Dominó ULATINA");
            Console.WriteLine();
            Console.WriteLine("Desarrolladores:");
            Console.WriteLine("   Daniela Zepeda");
            Console.WriteLine("   Johan Herrera");
            Console.WriteLine("   Yolani Sevilla");
            Console.WriteLine();
            Console.WriteLine("              GRACIAS POR JUGAR!!");
            Console.ReadKey();
        }

        /// <summary>
        /// Encargado de interactuar con el usuario para presentar el menu principal 
        /// y obtener la opción de juego deseada
        /// </summary>
        /// <returns>Entero que representa la opción elegida por el usuario</returns>
        static int MenuPrincipal()
        {
            int opcion;
            do
            {
                Console.WriteLine("              Bienvenido a Dominó ULATINA\n" +
                    "Digite el número correspondiente a la modalidad de juego que desea jugar:\n" +
                    "   1) Dominó Clasico (No se pueden obtener fichas extras)\n" +
                    "   2) Dominó Moderno (Se debe obtener ficha si lo requiere)\n" +
                    "   3) Dominó Modalidad de 5 (Los puntos obtenidos son redondeados hacia 5)\n" +
                    "   4) Salir");
                string texto = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(texto, out opcion))
                {
                    if (opcion >= 1 && opcion <= 4)
                    {
                        return opcion;
                    }
                    else
                    {
                        Console.WriteLine($"\"{opcion}\" no es una opción valida. Las opciones validas son: 1, 2, 3 y 4");
                        Console.WriteLine("Presione cualquier tecla para regresar al menú principal...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine($"\"{texto}\" no es un número valido.");
                    Console.WriteLine("Presione cualquier tecla para regresar al menú principal...");
                    Console.ReadKey();
                }

                Console.Clear();
            } while (true);
        }

        /// <summary>
        /// Encargado de interactuar con el usuario para solicitar el número de jugadores
        /// </summary>
        /// <returns>Entero que representa el número de jugadores que van a jugar</returns>
        static int SolicitarNumeroJugadores()
        {
            int numeroJugadores;
            do
            {
                Console.WriteLine("Digite el numero de jugadores que desean jugar(2,3,4)");
                string texto = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(texto, out numeroJugadores))
                {
                    if (numeroJugadores >= 2 && numeroJugadores <= 4)
                    {
                        return numeroJugadores;
                    }
                    else
                    {
                        Console.WriteLine($"\"{numeroJugadores}\" no es numero de jugadores valido. Las opciones validas son: 2, 3 y 4");
                        Console.WriteLine("Presione cualquier tecla para regresar al menú principal...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine($"\"{texto}\" no es un número valido.");
                    Console.WriteLine("Presione cualquier tecla para regresar al menú principal...");
                    Console.ReadKey();
                }

                Console.Clear();
            } while (true);
        }

        /// <summary>
        /// Encargado de interactuar con el usuario para solicitar el nombre de los jugadores
        /// y posteriormente crear una lista con los jugadores nombrados
        /// </summary>
        /// <param name="numeroJugadores">El número de jugadores que se nombrarán</param>
        /// <returns>Una lista de objetos de tipo Jugador</returns>
        static List<Jugador> NombrarJugadores(int numeroJugadores)
        {
            var jugadores = new List<Jugador>();
            for (int i = 0; i < numeroJugadores; i++)
            {
                string nombre = "";
                do
                {
                    Console.WriteLine($"Digite el nombre del jugador {i + 1}");
                    nombre = Console.ReadLine();
                    Console.Clear();
                    if (nombre.Trim() != "")
                    {
                        jugadores.Add(new Jugador { Nombre = nombre });
                        break;
                    }
                    else
                    {
                        Console.WriteLine("El nombre no puede estar vacio.");
                        Console.WriteLine("Presione cualquier tecla para volver a digitar el nombre del jugador...");
                        Console.ReadKey();
                    }
                    Console.Clear();
                } while (true);
            }
            return jugadores;
        }

        /// <summary>
        /// Encargado de interactuar con el usuario para solicitar cual movimiento de juego 
        /// desea realizar
        /// </summary>
        /// <param name="jugador">Jugador del turno actual</param>
        /// <returns></returns>
        static int MenuTurno(Jugador jugador)
        {
            int numeroFicha;
            do
            {
                Console.WriteLine($"              Turno {turno}");
                Console.WriteLine();
                if (domino.Tablero.Count == 0)
                {
                    Console.WriteLine("El tablero se encuentra vacio.");
                }
                else {
                    Console.WriteLine(domino.ObtenerEstadisticas());
                    Console.WriteLine(domino.ToString());
                }
                Console.WriteLine();
                Console.WriteLine(jugador.ToString());
                Console.WriteLine(jugador.ObtenerEstadisticas());
                Console.WriteLine(jugador.FichasDisponiblesTostring());
                if (domino.ModalidadJuego > ModalidadJuego.Clasico && domino.FichasDisponibles.Count > 0)
                {
                    Console.WriteLine("22) Ficha nueva");
                }
                if (domino.ModalidadJuego == ModalidadJuego.Clasico || domino.FichasDisponibles.Count == 0)
                {
                    Console.WriteLine("23) Pasar el turno");
                }
                Console.WriteLine();
                Console.WriteLine("Digite el número correspondiente a la ficha que desea jugar u opción de juego que desea realizar");
                string texto = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(texto, out numeroFicha))
                {
                    if ((numeroFicha >= 0 && numeroFicha <= jugador.FichasDisponibles.Count) || (numeroFicha >= 22 && numeroFicha <= 23))
                    {
                        return numeroFicha;
                    }
                    else
                    {
                        Console.WriteLine($"\"{numeroFicha}\" no es una opción valida.");
                        Console.WriteLine("Presione cualquier tecla para regresar al menú del turno...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine($"\"{texto}\" no es un número valido.");
                    Console.WriteLine("Presione cualquier tecla para regresar al menú del turno...");
                    Console.ReadKey();
                }

                Console.Clear();
            } while (true);
        }

        /// <summary>
        /// Encargado de interactuar con el usuario para solicitar en cual extremo del tablero
        /// desea ingresa la ficha de domino en caso de que pueda ser insertada en ambos extremos
        /// </summary>
        /// <param name="jugador">Jugador del turno actual</param>
        /// <param name="indiceFicha">Indice de la ficha que se desea jugar</param>
        /// <returns></returns>
        static int SolicitarLado(Jugador jugador, int indiceFicha)
        {
            int lado;
            do
            {
                Console.WriteLine($"La ficha seleccionada({jugador.FichasDisponibles[indiceFicha]}) puede ser insertada en ambos extremos del tablero.\n" +
                    $"Digite el número que corresponda al extremo del tablero en el cual desea colocar la ficha:\n" +
                    $"1) Izquierdo\n" +
                    $"2) Derecho");
                string texto = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(texto, out lado))
                {
                    if (lado >= 1 && lado <= 2)
                    {
                        return lado;
                    }
                    else
                    {
                        Console.WriteLine($"\"{lado}\" no es una opción valida. Las opciones validas son: 1, 2");
                        Console.WriteLine("Presione cualquier tecla para regresar a la selección de lado...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine($"\"{texto}\" no es un número valido.");
                    Console.WriteLine("Presione cualquier tecla para regresar a la selección de lado...");
                    Console.ReadKey();
                }

                Console.Clear();
            } while (true);
        }
    }
}
