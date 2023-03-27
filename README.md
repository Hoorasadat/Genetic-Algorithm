# Getting Started with dotnet new console -o:

This project was created using dotnet new console -o.

## Available Scripts

In the project directory, you can run:

### `dotnet run`

Runs the app in the development mode.

The page will run till the end and when you make changes, you need to re-run the app.
You may also see any lint errors in the console.

# About the App:
One of the scheduling problems with various applications in industries is flow shop. In flow
shop, a series of n jobs are processed at a series of g workshops with single machine in each workshop. To simplify the model construction, in most researches on flow shop scheduling problems, the setup times of operations have been ignored, combined with their corresponding processing times, or considered non sequence-dependent.

In this app, the problem of flow shop scheduling with single-machine stages to minimize the makespan is modeled. If the number of machines and jobs are big, the problem will be an Np-hard scheduling problem. Therefore, a basic Genetic Algorithm(GA) is developed to produce a reasonable manufacturing schedule within an acceptable computational time.

GA is a suitable meta-heuristic method to find a reasonable solution for Np-hard problems which might be either the optimal or near optimal solution.

The objective in this app development is to find a permutation schedule that minimizes the makespan of a sequence of n jobs in a g-stage flow shop with one machine at each stage. Every machine can only perform one operation of a job at the same time. The pre-emption is not allowed and there is enough space for intermediate storage between two successive stages.

For making the app more usable, the user will enter the number of machines, the number of jobs, the minimum amount for a job to be processed on a machine, the maximum amount for a job to be processed on a machine, and the population of the applied GA. For making it easy, the app will randomly generate the processing time of each job on each machin and print it on the console.
