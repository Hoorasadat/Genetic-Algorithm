// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Linq;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            // ****************  Problem setup ****************
            Console.WriteLine("Enter the number of machines in your problem:");
            int noOfMachines = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the number of jobs in your problem:");
            int noOfJobs = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the minimum amount for a job to be processed on a machine:");
            int lowerBoundProcessingTimes = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the maximum amount for a job to be processed on a machine:");
            int upperBoundProcessingTimes = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("*** Note that the processing times of jobs will be generated automatically in the provided range!");

            Console.WriteLine("Enter the size of population for Genetic Algorithm:");
            int sizeOfPopulation = Convert.ToInt32(Console.ReadLine());

            Problem myProblem = new Problem(noOfMachines, noOfJobs, lowerBoundProcessingTimes, upperBoundProcessingTimes);

            // ****************  Printing the generated processing times of the jobs: ****************
            for (int i = 0; i < myProblem.ProcessingTimesArray.GetLength(0); i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Processing Times of Job {i+1} on machines 1 to {myProblem.NoOfMachines}:");
                Console.WriteLine();
                for (int j = 0; j < myProblem.ProcessingTimesArray.GetLength(1); j++) {
                    Console.Write("{0} ", myProblem.ProcessingTimesArray[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Population pop = new Population(myProblem,sizeOfPopulation);
            pop.CreateRandomSequences(myProblem);

        }

    }



    class Problem
    {
        public int NoOfMachines { get; set; }
        public int NoOfJobs { get; set; }
        public int LowerBoundProcessingTimes { get; set; }
        public int UpperBoundProcessingTimes { get; set; }
        public int[,] ProcessingTimesArray { get; set; }
        public Problem(int noOfMachines, int noOfJobs, int lowerBoundProcessingTimes, int upperBoundProcessingTimes)
        {
            NoOfMachines = noOfMachines;
            NoOfJobs = noOfJobs;
            LowerBoundProcessingTimes = lowerBoundProcessingTimes;
            UpperBoundProcessingTimes = upperBoundProcessingTimes;
            ProcessingTimesArray = GeneratePTArray();
        }

        private int[,] GeneratePTArray()
        {
            int[,] processingTimesArray = new int[NoOfJobs, NoOfMachines];
            for (var i = 0; i < NoOfJobs; i++)
            {
                for (var j = 0; j < NoOfMachines; j++)
                {
                    Random rnd = new Random();
                    processingTimesArray[i,j] = rnd.Next(LowerBoundProcessingTimes, UpperBoundProcessingTimes + 1);
                }
            }
            return processingTimesArray;
        }
    }



    class Schedule
    {
        public int[] Sequence { get; set; }
        public Problem Prblm { get; set; }
        public Schedule(Problem prblm, int[] sequence)
        {
            Sequence = sequence;
            Prblm = prblm;
        }
        public int CalculateMakespan(Problem Prblm)
        {
            int noOfMachines = Prblm.NoOfMachines;
            int noOfJobs = Prblm.NoOfJobs;
            int[,] processingTimesArray = Prblm.ProcessingTimesArray;
            int[,] completionTimeArray = new int[noOfJobs, noOfMachines];

            // 1-- Calculating the completion times of first job on all machines
            int firstJobIndex = Sequence[0] - 1;
            int firstJobCT = 0;
            for (var j = 0; j < noOfMachines; j++)
            {
                firstJobCT += processingTimesArray[firstJobIndex,j];
                completionTimeArray[firstJobIndex,j] = firstJobCT;
            }

            // 2-- Calculating the completion times of other jobs on first machine
            for (var i =1; i < noOfJobs; i++)
            {
                int currentJobIndex = Sequence[i] - 1;
                int previousJobIndex = Sequence[i-1] - 1;
                completionTimeArray[currentJobIndex,0] = completionTimeArray[previousJobIndex,0] + processingTimesArray[currentJobIndex,0];
            }

            // 3-- Calculating the completion times of other jobs on other machines
            for (var i = 1; i < noOfJobs; i++)
            {
                int currentJobIndex = Sequence[i] - 1;
                int previousJobIndex = Sequence[i-1] - 1;
                int currentJobCT = completionTimeArray[currentJobIndex,0];
                for (var j = 1; j < noOfMachines; j++)
                {
                    int previousMachine = j - 1;
                    int previousJobCT = completionTimeArray[previousJobIndex,j];
                    int jobCTonPreviousMachin = completionTimeArray[currentJobIndex, previousMachine];
                    currentJobCT = Math.Max(previousJobCT, jobCTonPreviousMachin) + processingTimesArray[currentJobIndex,j];
                    completionTimeArray[currentJobIndex,j] = currentJobCT;
                }
            }
            int lastJobInSequence = Sequence[noOfJobs - 1];

            // ****************  Print the completion times for each job on each machine  ****************
            // for (int i = 0; i < completionTimeArray.GetLength(0); i++)
            // {
            //     Console.WriteLine();
            //     Console.WriteLine($"Completion Time of Job {i+1} on all machines:");
            //     Console.WriteLine();
            //     for (int j = 0; j < completionTimeArray.GetLength(1); j++) {
            //         Console.Write("{0} ", completionTimeArray[i, j]);
            //     }
            //     Console.WriteLine();
            // }

            return completionTimeArray[lastJobInSequence - 1 , noOfMachines - 1];
        }
    }



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

    // class Crossover
    // {
    //     public int[] Sequence { get; set; }
    //     public Crossover(int[] sequence)
    //     {
    //         Sequence = sequence;
    //     }

    // }
}
