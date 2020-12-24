using System.Collections.Generic;
using System.IO;

namespace AoC2020.Days
{
    public class Day22 : IDay
    {
        private string[] input;
        public Day22(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        public string PartOne()
        {
            Queue<int> player1 = new Queue<int>();
            Queue<int> player2 = new Queue<int>();
            InitPlayerDecks(player1, player2);

            while (player1.Count > 0 && player2.Count > 0)
            {
                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();

                if (card1 > card2)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else if (card2 > card1)
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }
                else throw new InvalidDataException();
            }

            var winner = player1.Count == 0 ? player2 : player1;
            return PlayerScore(winner).ToString();
        }

        private object PlayerScore(Queue<int> player)
        {
            var score = 0;
            for (var multiplier = player.Count; multiplier > 0; multiplier--)
            {
                var card = player.Dequeue();
                score += multiplier * card;
            }
            return score;
        }

        private void InitPlayerDecks(Queue<int> player1, Queue<int> player2)
        {   
            var currentDeck = player1;
            foreach (var line in input)
            {
                if (line.Length == 0)
                {
                    currentDeck = player2;
                    continue;
                }
                if (line[0] == 'P') continue; // Skip "Player N:" line.
                currentDeck.Enqueue(int.Parse(line));
            }
        }

        public string PartTwo()
        {
            Queue<int> player1 = new Queue<int>();
            Queue<int> player2 = new Queue<int>();
            InitPlayerDecks(player1, player2);

            var winner = PlayRecursiveGame(player1, player2);
            return PlayerScore(winner).ToString();
        }

        private Queue<int> PlayRecursiveGame(Queue<int> player1, Queue<int> player2)
        {
            var pastGameStates = new HashSet<string>();

            while (player1.Count > 0 && player2.Count > 0)
            {
                var gameState = GameStateString(player1, player2);
                if (pastGameStates.Contains(gameState))
                    return player1;
                pastGameStates.Add(gameState);             

                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();

                Queue<int> roundWinner;

                if (player1.Count >= card1 && player2.Count >= card2)
                {
                    var subPlayer1 = MakeSubDeck(player1, card1);
                    var subPlayer2 = MakeSubDeck(player2, card2);
                    var subWinner = PlayRecursiveGame(subPlayer1, subPlayer2);
                    roundWinner = subWinner == subPlayer1 ? player1 : player2;                   
                }
                else if (card1 > card2)
                    roundWinner = player1;
                else if (card2 > card1)
                    roundWinner = player2;
                else throw new InvalidDataException();

                if (roundWinner == player1)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }                
            }

            return player1.Count == 0 ? player2 : player1;
        }

        private Queue<int> MakeSubDeck(Queue<int> player, int nCards)
        {
            var subPlayer = new Queue<int>();
            var card_i = 0;
            foreach (var card in player)
            {
                subPlayer.Enqueue(card);

                card_i += 1;
                if (card_i == nCards) break;
            }
            return subPlayer;
        }

        private string GameStateString(Queue<int> player1, Queue<int> player2)
        {
            var state = player1.Count.ToString() + "_";
            foreach (var card in player1)
                state += card.ToString().PadLeft(2, '0');
            foreach (var card in player2)
                state += card.ToString().PadLeft(2, '0');
            return state;
        }
    }
}