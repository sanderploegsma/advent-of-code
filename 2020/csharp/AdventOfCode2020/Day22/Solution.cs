using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day22
{
    internal class Solution
    {
        private readonly IReadOnlyCollection<int> _player1;
        private readonly IReadOnlyCollection<int> _player2;

        public Solution(string input)
        {
            var players = input.Split(Environment.NewLine + Environment.NewLine);
            _player1 = players[0].Split(Environment.NewLine).Skip(1).Select(int.Parse).ToList();
            _player2 = players[1].Split(Environment.NewLine).Skip(1).Select(int.Parse).ToList();
        }

        public int PartOne()
        {
            var game = new CrabCombat(_player1, _player2);
            var (player1, player2) = game.Play();
            return Math.Max(player1.Score(), player2.Score());
        }

        public int PartTwo()
        {
            var game = new CrabRecursiveCombat(_player1, _player2);
            var (player1, player2) = game.Play();
            return Math.Max(player1.Score(), player2.Score());
        }
    }

    internal class CrabCombat
    {
        private readonly Queue<int> _player1;
        private readonly Queue<int> _player2;

        public CrabCombat(IEnumerable<int> player1, IEnumerable<int> player2)
        {
            _player1 = new Queue<int>(player1);
            _player2 = new Queue<int>(player2);
        }

        public (IEnumerable<int> Player1, IEnumerable<int> Player2) Play()
        {
            while (_player1.Count > 0 && _player2.Count > 0)
            {
                var card1 = _player1.Dequeue();
                var card2 = _player2.Dequeue();

                if (card1 > card2)
                {
                    _player1.Enqueue(card1);
                    _player1.Enqueue(card2);
                }
                else
                {
                    _player2.Enqueue(card2);
                    _player2.Enqueue(card1);
                }
            }

            return (_player1, _player2);
        }
    }

    internal class CrabRecursiveCombat
    {
        private readonly Queue<int> _player1;
        private readonly Queue<int> _player2;
        private readonly ISet<IEnumerable<int>> _rounds;

        public CrabRecursiveCombat(IEnumerable<int> player1, IEnumerable<int> player2)
        {
            _player1 = new Queue<int>(player1);
            _player2 = new Queue<int>(player2);
            _rounds = new HashSet<IEnumerable<int>>(new SpaceCardDeckEqualityComparer());
        }

        public (IEnumerable<int> Player1, IEnumerable<int> Player2) Play()
        {
            while (_player1.Count > 0 && _player2.Count > 0)
            {
                // Before either player deals a card, if there was a previous round in this game that had exactly
                // the same cards in the same order in the same players' decks,the game instantly ends in a win for
                // player 1
                if (!_rounds.Add(_player1.Concat(_player2)))
                {
                    return (_player1.ToArray(), Enumerable.Empty<int>());
                }

                // Otherwise, this round's cards must be in a new configuration; the players begin the round by each
                // drawing the top card of their deck as normal
                var card1 = _player1.Dequeue();
                var card2 = _player2.Dequeue();
                var player1Wins = card1 > card2;

                if (card1 <= _player1.Count && card2 <= _player2.Count)
                {
                    // To play a sub-game of Recursive Combat, each player creates a new deck by making a copy of the
                    // next cards in their deck (the quantity of cards copied is equal to the number on the card they
                    // drew to trigger the sub-game).
                    var subGame = new CrabRecursiveCombat(_player1.Take(card1), _player2.Take(card2));
                    var (p1, p2) = subGame.Play();
                    player1Wins = p1.Any();
                }

                if (player1Wins)
                {
                    _player1.Enqueue(card1);
                    _player1.Enqueue(card2);
                }
                else
                {
                    _player2.Enqueue(card2);
                    _player2.Enqueue(card1);
                }
            }

            return (_player1, _player2);
        }

        private class SpaceCardDeckEqualityComparer : IEqualityComparer<IEnumerable<int>>
        {
            public bool Equals(IEnumerable<int> x, IEnumerable<int> y) => x.SequenceEqual(y);

            public int GetHashCode(IEnumerable<int> obj) => obj.GetHashCode();
        }
    }

    internal static class SpaceCardsExtensions
    {
        public static int Score(this IEnumerable<int> cards) =>
            cards.Reverse().Indexed().Sum(card => (card.Index + 1) * card.Value);
    }
}
