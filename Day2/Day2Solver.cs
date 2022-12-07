using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode2022.Interfaces;

namespace AdventOfCode2022.Day2
{
    public class Day2Part1Solver : IDaySolver<int>
    {
        private Dictionary<string, int> _values { get; set; } = new()
        {
            { "A", 0 },
            { "B", 1 },
            { "C", 2 },
            { "X", 0 },
            { "Y", 1 },
            { "Z", 2 }
        };

        public int Solve()
        {
            var lines = File.ReadAllLines("Day2/input");
            var total = 0;

            foreach (var line in lines)
            {
                var guesses = line.Split(' ');
                var player1Guess = _values[guesses.First()];
                var player2Guess = _values[guesses.ElementAt(1)];

                total += EvaluateGame(player1Guess, player2Guess);
            }
            
            return total;
        }

        public int EvaluateGame(int a, int b)
        {
            var result = 0;

            if (IsWin(a, b)) result = 6; 
            else if (IsDraw(a, b)) result = 3;
            else result = 0;

            return result + b + 1;
        }

        private bool IsWin(int a, int b) => (a + 1) % 3 == b;
        private bool IsDraw(int a, int b) => a == b;
    }

    public class Day2Part2Solver : IDaySolver<int>
    {
        private enum Move { Rock, Paper, Scissors }

        private enum EndGame { Lose, Draw, Win }

        private Dictionary<string, Move> _moves { get; set; } = new()
        {
            { "A", Move.Rock },
            { "B", Move.Paper },
            { "C", Move.Scissors },
            { "X", Move.Rock },
            { "Y", Move.Paper },
            { "Z", Move.Scissors }
        };

        private Dictionary<string, EndGame> _endGames { get; set; } = new()
        {
            { "X", EndGame.Lose },
            { "Y", EndGame.Draw },
            { "Z", EndGame.Win }
        };

        private Dictionary<EndGame, int> _prizes { get; set; } = new()
        {
            { EndGame.Lose, 0 },
            { EndGame.Draw, 3 },
            { EndGame.Win, 6 }
        };

        public int Solve()
        {
            var lines = File.ReadAllLines("Day2/input");
            var total = 0;

            foreach (var line in lines)
            {
                var guesses = line.Split(' ');

                var moveA = _moves[guesses.First()];
                var moveB = _moves[guesses.ElementAt(1)];
                
                var desiredEndGame = _endGames[guesses.ElementAt(1)];
                var chosenPlay = MakePlay(moveA, desiredEndGame);

                total += 1 + (int)chosenPlay + _prizes[desiredEndGame];
            }
            
            return total;
        }

        private Move MakePlay(Move moveA, EndGame endGame)
        {
            if(endGame == EndGame.Draw)
                return moveA;
            else
                return (Move)(((int)moveA + (endGame == EndGame.Win ? 1 : 2)) % 3);
        }
    }
}