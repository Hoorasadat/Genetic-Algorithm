// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Linq;

namespace GeneticAlgorithm
{
    class Population
    {
        public static Problem? Problem { get; set; }
        public static int SizeOfPopulation { get; set; }
        public Population(Problem problem, int sizeOfPopulation)
        {
            Problem = problem;
            SizeOfPopulation = sizeOfPopulation;
        }

        public struct ArrayOfPopulation
        {
            public int[][] ArrayOfSequences;
            public int[] ArrayOfMakespans;
        }
        public ArrayOfPopulation CreateRandomSequences(Problem Problem)
        {
            int noOfJobs = Problem.NoOfJobs;
            int [] baseSequence = Enumerable.Range(1, noOfJobs).ToArray();
            Schedule baseSchedul = new Schedule(Problem, baseSequence);

            Console.WriteLine("Primary Jobs Sequence: " + string.Join("-", baseSchedul.Sequence));
            Console.WriteLine("Primary Total Processing Times(Makespan) " + baseSchedul.CalculateMakespan(Problem));

            ArrayOfPopulation arrayOfPopulation = new ArrayOfPopulation();
            arrayOfPopulation.ArrayOfSequences = new int [SizeOfPopulation][];
            arrayOfPopulation.ArrayOfMakespans = new int [SizeOfPopulation];

            // Console.WriteLine("Randomly generate and print all the sequences in the population");
            for (var i = 0; i < SizeOfPopulation; i++)
            {
                var rnd = new Random();
                int [] sequence = baseSequence.OrderBy(x => rnd.Next()).ToArray();
                Schedule schedul = new Schedule(Problem, sequence);

                Console.WriteLine("Jobs Sequence: " + string.Join("-", schedul.Sequence));
                Console.WriteLine("Total Processing Times(Makespan) " + schedul.CalculateMakespan(Problem));
                Console.WriteLine();

                arrayOfPopulation.ArrayOfSequences[i] = sequence;
                arrayOfPopulation.ArrayOfMakespans[i] = schedul.CalculateMakespan(Problem);
            }
            return arrayOfPopulation;
        }
    }
}
