using AdventOfCode2022.Interfaces;

namespace AdventOfCode2022.Day1
{
    public class Day1SolverPart1 : IDaySolver<int>
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

    public class Day1SolverPart2 : IDaySolver<int>
    {
        public int Solve()
        {           
            var lines = File.ReadAllLines("Day1/input");

            var caloriesCounts = new List<int>();
            var currentCalorieCount = 0;

            foreach (string line in lines)
            {
                if(!string.IsNullOrWhiteSpace(line) && int.TryParse(line, out var calorie))
                {
                    currentCalorieCount += calorie;
                }
                else
                {
                    caloriesCounts.Add(currentCalorieCount);
                    currentCalorieCount = 0;
                }
            }

            var topThreeTotalCalories = caloriesCounts.OrderByDescending(x => x).Take(3).Sum();

            return topThreeTotalCalories;
        }
    }
}