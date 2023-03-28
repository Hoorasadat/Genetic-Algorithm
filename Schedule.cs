// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;

namespace GeneticAlgorithm
{
    class Schedule
    {
        public Problem Prblm { get; set; }
        public int[] Sequence { get; set; }
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
}
