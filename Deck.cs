using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BlackJack;

namespace BlackJack
{
    internal class Deck
    {
        public int MaximodeCartasDeck { get; private set; }
        private int CartasIn;
        private int CartasScoreOFF;
        private int CartasScoreOFF1;
        private uint Interacciones;
        private List<Card> deck;
        private Dictionary<CardName, int> CartaPuntos;

        public Deck()
        {
            CartasIn = 3;
            CartasScoreOFF = 1;
            CartasScoreOFF1 = 2;
            Interacciones = 50;

            CartaPuntos = new Dictionary<CardName, int>();
            deck = new List<Card>();

            for (int i = 0; i < CartasIn + 1; i++)
            {
                CartaPuntos.Add((CardName)i, i + CartasScoreOFF);
            }

            for (int i = CartasIn + 1; i < Enum.GetValues(typeof(CardName)).Length; i++)
            {
                CartaPuntos.Add((CardName)i, i + CartasScoreOFF1);
            }
            Reiniciar();
        }


        //PROCESO PARA DAR UNA CARTA ALEATORIA
        public Card DarCarta()
        {
            Card buffCard;
            buffCard = deck.Last<Card>();
            deck.Remove(buffCard);
            return buffCard;
        }

        public void Reiniciar()
        {
            deck.Clear();
            int maxCard = 0;
            int suitsCount = Enum.GetValues(typeof(CardSuit)).Length;

            for (int i = 0; i < suitsCount; i++)
            {
                for (int j = 0; j < CartaPuntos.Count; j++)
                {
                    Card card = new Card();
                    card.Nombre = (CardName)j;
                    card.Suit = (CardSuit)i;
                    card.Score = CartaPuntos[(CardName)j];

                    if (maxCard < CartaPuntos[(CardName)j])
                    {
                        maxCard = CartaPuntos[(CardName)j];
                    }

                    deck.Add(card);
                }
            }
            MaximodeCartasDeck = maxCard;
            MixCards(Interacciones);
        }

        private void MixCards(uint iterations)
        {
            int randomIndex = 0;
            int randomSize = 0;
            Random random = new Random();

            for (int i = 0; i < iterations; i++)
            {
                randomSize = random.Next(deck.Count / 2);
                randomIndex = random.Next(deck.Count - randomSize);
                Card[] buff = new Card[randomSize];
                deck.CopyTo(randomIndex, buff, 0, randomSize);
                deck.RemoveRange(randomIndex, randomSize);
                deck.InsertRange(0, buff);
            }
        }

    }
}