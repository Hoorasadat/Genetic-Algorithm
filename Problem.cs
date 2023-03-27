// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Linq;

namespace GeneticAlgorithm
{
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
}
