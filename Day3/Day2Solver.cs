using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode2022.Interfaces;

namespace AdventOfCode2022.Day3
{
    // NOTE: Horribly unoptimised and lazily written but ... Shurgs!
    public class Day3Part1Solver : IDaySolver<int>
    {
        public class Bag
        {
            public bool InPocketA { get; set; }
            public bool InPocketB { get; set; }
        }

        public int Solve()
        {            
            var lines = File.ReadAllLines("Day3/input");
            var total = 0;

            foreach (var line in lines)
            {
                var duplicates = GetDuplicates(line);
                total += duplicates.Select(GetValueForChar).Sum();
            }

            return total;
        }

        private IEnumerable<char> GetDuplicates(string line)
        {
            var halfCount = (int)(line.Count() * 0.5);
            var duplicates = line
                .GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .ToDictionary(x => x, x => new Bag());

            var pocketA = line.Take(halfCount).ToList();
            var pocketB = line.Skip(halfCount).ToList();

            for (int i = 0; i < halfCount; i++)
            {
                var pocketACurrentValue = pocketA[i];
                var pocketBCurrentValue = pocketB[i];

                if (duplicates.ContainsKey(pocketACurrentValue))
                    duplicates[pocketACurrentValue].InPocketA = true;
                    
                if (duplicates.ContainsKey(pocketBCurrentValue))
                    duplicates[pocketBCurrentValue].InPocketB = true;
            }

            return duplicates
                .Where(x => x.Value.InPocketA && x.Value.InPocketB)
                .Select(x => x.Key);
        }

        private int GetValueForChar(char value)
        {
            var numberValue = (int)value;
            var isCapitalised = numberValue <= 90;

            return isCapitalised 
                ? value - 38
                : value - 96;
        }
    }

    public class Day3Part2Solver : IDaySolver<int>
    {
        public int Solve()
        {            
            var lines = File.ReadAllLines("Day3/input");
            var total = 0;

            for (int i = 0; i < lines.Count(); i += 3)
            {
                var lineBatch = lines.Skip(i).Take(3);
                var stickers = GetStickers(lineBatch);
                total += stickers.Select(GetValueForChar).Sum();
            }

            return total;
        }

        private IEnumerable<char> GetStickers(IEnumerable<string> lineBatch)
        {
            var lineCount = lineBatch.Count();
            var duplicates = string.Join(string.Empty, lineBatch)
                .GroupBy(x => x)
                .Where(x => x.Count() >= lineCount)
                .Select(x => x.Key)
                .ToDictionary(
                    x => x, 
                    x => Enumerable.Repeat(false, lineCount).ToList()
                );

            for (int i = 0; i < lineCount; i++)
            {
                var line = lineBatch.ToList()[i];
                foreach (var character in line)
                {
                    if (!duplicates.ContainsKey(character))
                        continue;
                    
                    var duplicate = duplicates[character];
                    duplicate[i] = true;
                }
            }

            return duplicates
                .Where(x => x.Value.All(x => x))
                .Select(x => x.Key);
        }

        private int GetValueForChar(char value)
        {
            var numberValue = (int)value;
            var isCapitalised = numberValue <= 90;

            return isCapitalised 
                ? value - 38
                : value - 96;
        }
    }
}