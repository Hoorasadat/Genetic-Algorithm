// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Linq;

namespace GeneticAlgorithm
{
    class Population
    {
        public static Problem Prblm { get; set; }
        public static int SizeOfPopulation { get; set; }
        public Population(Problem prblm, int sizeOfPopulation)
        {
            Prblm = prblm;
            SizeOfPopulation = sizeOfPopulation;
        }

        public void getResult()
        {
            MakespanEvaluation(CreateRandomSequences(Prblm).ArrayOfMakespans);
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
            int baseMakespan = baseSchedul.CalculateMakespan(Problem);

            Console.WriteLine("Primary Jobs Sequence: " + string.Join("-", baseSchedul.Sequence));
            Console.WriteLine("Primary Total Processing Times(Makespan) " + baseMakespan);

            ArrayOfPopulation arrayOfPopulation = new ArrayOfPopulation();
            arrayOfPopulation.ArrayOfSequences = new int [SizeOfPopulation][];
            arrayOfPopulation.ArrayOfMakespans = new int [SizeOfPopulation];

            // first sequence in the population is the basic one generated above
            arrayOfPopulation.ArrayOfSequences[0] = baseSequence;
            arrayOfPopulation.ArrayOfMakespans[0] = baseMakespan;

            // Console.WriteLine("Randomly generate and print all the other sequences in the population");
            for (var i = 1; i < SizeOfPopulation; i++)
            {
                var rnd = new Random();
                int [] sequence = baseSequence.OrderBy(x => rnd.Next()).ToArray();
                Schedule schedul = new Schedule(Problem, sequence);
               int makespan = schedul.CalculateMakespan(Problem);

                Console.WriteLine("Jobs Sequence: " + string.Join("-", schedul.Sequence));
                Console.WriteLine("Total Processing Times(Makespan) " + makespan);
                Console.WriteLine();

                arrayOfPopulation.ArrayOfSequences[i] = sequence;
                arrayOfPopulation.ArrayOfMakespans[i] = makespan;
            }
            return arrayOfPopulation;
        }

        public struct SequencesEvaluation
        {
            public double[] FitnessOfSequences;
            public double[] ChanceOfSelection;
            public double[] CumulativeChanceOfSelection;
        }

        public SequencesEvaluation MakespanEvaluation (int[] arrayOfMakespans)
        {
            SequencesEvaluation sequencesEvaluation = new SequencesEvaluation();
            sequencesEvaluation.FitnessOfSequences = new double [SizeOfPopulation];
            sequencesEvaluation.ChanceOfSelection = new double [SizeOfPopulation];
            sequencesEvaluation.CumulativeChanceOfSelection = new double [SizeOfPopulation];

            for (var i = 0; i < SizeOfPopulation; i++)
            {
                sequencesEvaluation.FitnessOfSequences[i] = Math.Truncate(
                    ((double)1 / ((double)1 + (double)arrayOfMakespans[i])) * 10000) / 10000;
            }

            double sumOfFitnesses = Math.Truncate(sequencesEvaluation.FitnessOfSequences.Sum()* 10000) / 10000;
            for (var i = 0; i < SizeOfPopulation; i++)
            {
                sequencesEvaluation.ChanceOfSelection[i] = Math.Truncate(
                (sequencesEvaluation.FitnessOfSequences[i] / sumOfFitnesses) * 10000) / 10000;
            }

            double cumulativeProbability = 0.00;
            for (var i = 0; i < SizeOfPopulation; i++)
            {
                cumulativeProbability += sequencesEvaluation.ChanceOfSelection[i];
                sequencesEvaluation.CumulativeChanceOfSelection[i] = cumulativeProbability;
            }
            return sequencesEvaluation;
        }
    }
}
