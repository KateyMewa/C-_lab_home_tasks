using EquationSolver;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchronyMultithreading
{
    class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        static async Task Main(string[] args)
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logfile.txt" };
            
            config.AddRule(LogLevel.Info, LogLevel.Info, logfile);
            config.AddRule(LogLevel.Debug, LogLevel.Debug, logfile);
            config.AddRule(LogLevel.Error, LogLevel.Fatal, logfile);
                   
            LogManager.Configuration = config;

            Logger.Debug("Starting the application...");
            Logger.Debug("Read equations from the given file.");

            try
            {
                Equation[] equations = File.ReadAllLines(@"Input.txt").Select(line =>
            {
                return new Equation { A = Parse(line, 0), B = Parse(line, 1), C = Parse(line, 2) };
            }
            ).ToArray();

                Logger.Info($"Equations have been read from the file. Found {equations.Length} equations.");
                Logger.Debug("Strat solving equations in one thread.");

                var solver = new EqSolver();
                var tasks = equations.Select(equation => solver.ResolveEquation(equation)).ToArray();
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var results = new List<string>();
                foreach (var task in tasks)
                {
                    results.Add(task.Result.Explanation);
                }
                stopwatch.Stop();
                File.WriteAllLines(@"Output.txt", results);
                Console.WriteLine($"Sync execution time elapsed (in one thread): {stopwatch.Elapsed}");

                Logger.Info($"Sync execution time elapsed (in one thread): {stopwatch.Elapsed}");
                Logger.Debug("Strat solving equations in several threads.");

                stopwatch.Restart();
                foreach (var task in tasks)
                {
                    var result = await task;
                    results.Add(result.Explanation);
                }
                stopwatch.Stop();
                File.WriteAllLines(@"Output.txt", results);

                Console.WriteLine($"Async execution time elapsed (in several threads): {stopwatch.Elapsed}");

                Logger.Info($"Sync execution time elapsed (in several threads): {stopwatch.Elapsed}");
                Logger.Debug("The calculations have been finished.");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Error($"Something went wrong. Exception occured: {ex.Message}");
                throw;
            }
        }

        private static double Parse(string line, int numberPosition)
        {
            return double.Parse(line.Split(' ')[numberPosition]);
        }
    }
}
