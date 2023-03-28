// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            // *****************************  Problem setup ********************************
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

            // Console.WriteLine("Enter the number of generations for Genetic Algorithm:");
            // int numberOfGenerations = Convert.ToInt32(Console.ReadLine());

            Problem myProblem = new Problem(noOfMachines, noOfJobs, lowerBoundProcessingTimes, upperBoundProcessingTimes);
            //******************************************************************************

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
            //******************************************************************************

            Population pop = new Population(myProblem,sizeOfPopulation);
            pop.getResult();

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
