using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var chips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
            };

            var sequence = GetLongestChipSequence(chips, Color.Blue, Color.Green);
            if (sequence != null)
            {
                Console.Write("Blue ");
                foreach (var chip in sequence)
                {
                    Console.Write($"[{chip}] ");
                }
                Console.WriteLine("Green");
            }
            else
            {
                Console.WriteLine($"{Constants.ErrorMessage}");
            }
        }

        // Finds the longest sequence of chips connecting start to end
        private static List<ColorChip> GetLongestChipSequence(List<ColorChip> chips, Color start, Color end)
        {
            List<ColorChip> bestSequence = new List<ColorChip>();
            Backtrack(start, new List<ColorChip>(), chips, end, ref bestSequence);
            return bestSequence;
        }
        
        // Backtracking method to explore all paths from start to end
        private static void Backtrack(Color current, List<ColorChip> path, List<ColorChip> remaining, Color end, ref List<ColorChip> bestSequence)
        {
            if (current == end)
            {
                if (bestSequence == null || path.Count > bestSequence.Count)
                    bestSequence = new List<ColorChip>(path);
                return;
            }
            for (int i = 0; i < remaining.Count; i++)
            {
                var chip = remaining[i];
                if (chip.StartColor == current)
                {
                    var next = chip.EndColor;
                    var nextPath = new List<ColorChip>(path) { chip };
                    var nextRemaining = new List<ColorChip>(remaining);
                    nextRemaining.RemoveAt(i);
                    Backtrack(next, nextPath, nextRemaining, end, ref bestSequence);
                }
            }
        }
    }
}
