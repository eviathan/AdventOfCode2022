using System;
using System.Globalization;
using AdventOfCode2022.Interfaces;

namespace AdventOfCode2022.Day1
{
    public class Day1Solver : IDaySolver<int>
    {
        public int Solve()
        {           
            var lines = File.ReadAllLines("Day1/input");

            var largestCalorieCount = 0;
            var currentCalorieCount = 0;

            foreach (string line in lines)
            {
                if(!string.IsNullOrWhiteSpace(line) && int.TryParse(line, out var calorie))
                {
                    currentCalorieCount += calorie;
                }
                else if (currentCalorieCount > largestCalorieCount) 
                {
                    largestCalorieCount = currentCalorieCount;
                    currentCalorieCount = 0;
                }
                else
                {
                    currentCalorieCount = 0;
                }
            }

            return largestCalorieCount;
        }
    }
}