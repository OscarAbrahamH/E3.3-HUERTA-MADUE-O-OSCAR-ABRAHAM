using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Player
    {
        private List<Card> Mano;
        private Deck Baraja;

        public string Name { get; private set; }
        public int Score { get; private set; }

        public Player(Deck deck, string name)
        {
            Name = name;
            Baraja = deck;
            Mano = new List<Card>();
            Score = 0;
            AskCard();
        }

        public void AskCard()
        {
            Mano.Add(Baraja.DarCarta());
            Score += Mano.Last<Card>().Score;
        }

        public void VMano()
        {
            UserIO.ShowHand(this, Mano.ToArray());
        }

        public void Reset()
        {
            Score = 0;
            Mano.Clear();
            AskCard();
        }
    }
}
