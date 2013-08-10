using System.IO;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        ClosestPairFinder.Process(args[0]);
    }
}

public static class ClosestPairFinder
{
    public static void Process(string fileName)
    {
        var lines = new List<string>();
        using (StreamReader reader = File.OpenText(fileName))
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (null == line)
                    continue;
                lines.Add(line);
            }

        //Each pair of coordinates is a Tuple of int, int
        //Each set of coordinates to be tested is a List of the Tuples
        //Finally all sets are put in to a List of their own
        List<List<Tuple<int, int>>> inputs = ParseInput(lines);

        var output = Calculate(inputs);
        foreach (var item in output)
        {
            Console.WriteLine(item);
        }
    }

    private static ParallelQuery<string> Calculate(List<List<Tuple<int, int>>> input)
    {
        var output = input.AsParallel().AsOrdered().Select(set => FindClosestPairInSet(set));
        return output;
    }

    private static List<List<Tuple<int, int>>> ParseInput(List<string> lines)
    {
        var parsedLines = new List<List<Tuple<int, int>>>();
        int x = 0;
        int y = 0;
        string[] coordinates;
        var currentSet = new List<Tuple<int, int>>();

        for (int i = 1; i < lines.Count; i++)
        {
            if (lines[i].Length == 1)
            {
                parsedLines.Add(currentSet.ToList());
                currentSet.Clear();
                continue;
            }
            else
            {
                coordinates = lines[i].Split(' ');
            
                x = int.Parse(coordinates[0]);
                y = int.Parse(coordinates[1]);

                currentSet.Add(new Tuple<int, int>(x, y));
            }
        }

        return parsedLines;
    }

    private static string FindClosestPairInSet(List<Tuple<int, int>> set)
    {
        double closestSoFar = double.MaxValue;

        for (int i = 0; i < set.Count; i++)
        {
            for (int x = 0; x < set.Count; x++)
            {
                if (i == x) continue;

                double calculatedDistance = CalculateDistance(set[i], set[x]);
                if (calculatedDistance < closestSoFar) closestSoFar = calculatedDistance;
            }
        }

        string ret = (closestSoFar >= 10000) ? "INFINITY" : Math.Round(closestSoFar, 4).ToString("0.0000");
        return ret;
    }

    private static double CalculateDistance(Tuple<int, int> point1, Tuple<int, int> point2)
    {
        double a = point1.Item1 - point2.Item1;
        double b = point1.Item2 - point2.Item2;
        return Math.Sqrt((a * a) + (b * b));
    }
}
