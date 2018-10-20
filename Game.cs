using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BlackJack.BlackJack;

namespace BlackJack
{
    internal class Game
    {
        //DECLARACION  

        private const int MaximaPuntuacion = 21;
        private Player Casino;
        private Player Apostador;
        private Deck Cartas;
        private GameStatus EstadoJuego;

        
        public Game()
        {
            string NombreJugador = UserIO.DeclaracionJugador();
            Cartas = new Deck();

            if (NombreJugador == " ")
            {
                Casino = new Player(Cartas, " ");
                Apostador = new Player(Cartas, " ");
                UserIO.Matriz();
            }

            if (NombreJugador != " ")
            {
                Casino = new Player(Cartas, "Casino");
                Apostador = new Player(Cartas, NombreJugador);
            }
            JuegoCiclo();
        }

        private void JuegoCiclo()
        {
            //SE REALIZA EL CICLO DE JUEGO
            bool TurnoJugador = true;
            int input;
            Random random = new Random();
            EstadoJuego = GameStatus.InGame;

            while (true)
            {
                if (TurnoJugador)
                {
                    Apostador.VMano();

                    if (Ganador1(Apostador, Casino))
                    {
                        return;
                    }

                    while (true)
                    {
                        input = UserIO.ElJuego(EstadoJuego);

                        if (input == 1)
                        {
                            Apostador.AskCard();
                            break;
                        }

                        if (input == 2)
                        {
                            TurnoJugador = false;
                            break;
                        }

                        Apostador.VMano();
                    }
                }

                if (!TurnoJugador)
                {
                    //SE DETERMINA QUE JUGADOR TIENE LA PUNTUACION GANADORA
                    double rand = random.NextDouble();

                    if (MaximaPuntuacion - Casino.Score >= MaximaPuntuacion - Cartas.MaximodeCartasDeck ||
                        rand >= ((double)Casino.Score - Cartas.MaximodeCartasDeck) / ((double)MaximaPuntuacion - Cartas.MaximodeCartasDeck))
                    {
                        Casino.AskCard();

                        if (Ganador1(Casino, Apostador))
                        {
                            return;
                        }
                    }

                    if (MaximaPuntuacion - Casino.Score < MaximaPuntuacion - Cartas.MaximodeCartasDeck &&
                        rand < ((double)Casino.Score - Cartas.MaximodeCartasDeck) / ((double)MaximaPuntuacion - Cartas.MaximodeCartasDeck))
                    {
                        EstadoJuego = GameStatus.GameEnd;
                        Ganador1(Casino, Apostador);
                    }
                }
            }
        }

        private bool Ganador1(Player player1, Player player2)
        {
            bool VGanador = false;
            Player Ganador = null;

            if (EstadoJuego == GameStatus.InGame)
            {

                if (player1.Score > MaximaPuntuacion || player2.Score == MaximaPuntuacion)
                {
                    Ganador = player2;
                    VGanador = true;
                }

                if (player2.Score > MaximaPuntuacion || player1.Score == MaximaPuntuacion)
                {
                    Ganador = player1;
                    VGanador = true;
                }
            }

            if (EstadoJuego == GameStatus.GameEnd)
            {
                if (player1.Score > player2.Score)
                {
                    Ganador = player1;
                }

                if (player1.Score < player2.Score)
                {
                    Ganador = player2;
                }

                if (player1.Score == player2.Score)
                {
                    Ganador = null;
                }

                VGanador = true;
            }

            if (VGanador)
            {
                EstadoJuego = GameStatus.GameEnd;
                player1.VMano();
                player2.VMano();
                UserIO.ShowWinner(Ganador);
                FinJuego();
            }

            return VGanador;
        }

        //SE TERMIAN EL JUEGO 
        private void FinJuego()
        {
            int input;

            while (true)
            {
                input = UserIO.ElJuego(EstadoJuego);

                if (input == 1)
                {
                    //SE REINICIA TODOS LOS DATOS
                    Cartas.Reiniciar();
                    Casino.Reset();
                    Apostador.Reset();
                    JuegoCiclo();
                    break;
                }

                if (input == 2)
                {
                    Environment.Exit(0);
                    break;
                }
            }
        }
    }
}

