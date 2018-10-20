using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BlackJack;

namespace BlackJack
{
    static internal class UserIO
    {
        //PREGUNTAR NOMBRE DEL JUGADOR
        public static string DeclaracionJugador()
        {
            Console.WriteLine("¿Cual es tu nombre?");
            string nombre = Console.ReadLine(); ;

            if (nombre == "")
            {
                nombre = "Jugador";
            }

            Console.WriteLine("Bienvenido a BlackJack");
            return nombre;
        }
       
        public static int ElJuego(GameStatus EstatusJuego)
        {
            while (true)
            {
                string input;
                //ELEGIR OPCION DE QUE SE DESEA HACER
                if (EstatusJuego == GameStatus.InGame)
                {
                    Console.WriteLine("¿Que deseas hacer?");
                    Console.WriteLine("1.- Dame una carta / 2.- Me retiro");
                }
                //TERMINAR EL JUEGO O VOLVER A JUGAR
                if (EstatusJuego == GameStatus.GameEnd)
                {
                    Console.WriteLine("¿Que desea hacer ahora?");
                    Console.WriteLine("1.- Jugar de nuevo / 2.- Salir");
                }

                input = Console.ReadLine();

                if (input == "1" || input == "2")
                {
                    Console.Clear();
                    return Convert.ToInt32(input);
                }
                //OPCION NO VALIDA
                Console.Clear();
                Console.WriteLine("Respuesta invalia");
            }
        }

        //DECISION SI EL JUGADOR GANO

        public static void ShowWinner(Player winner)
        {
            
            if (winner == null)
            {
                Console.WriteLine(" ");
            }

            if (winner != null)
            {
                Console.WriteLine("\n" + winner.Name + " es el ganador");
            }

        }

        public static void Matriz()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ");
        }

        public static void ShowHand(Player player, IEnumerable<Card> hand)
        {
            //DATOS DEL JUGADOR
            Console.WriteLine("\n" + player.Name + " Cartas:");

            foreach (Card card in hand)
            {
                Console.WriteLine(card.Nombre.ToString() + " " + card.Suit.ToString());
            }

            Console.WriteLine("\nTotal de puntos: " + player.Score.ToString());
        }
    }
}

