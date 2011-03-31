﻿#region License
// Copyright (c) 2010 Nano Taboada, http://openid.nanotaboada.com.ar
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#endregion

namespace Dotnet.Samples.Parallelism
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /// <remarks>
                /// A big thank you to the awesome community of StackOverflow
                /// for their advice and guidance with this sample project!
                /// http://stackoverflow.com/questions/5195486/
                /// </remarks>

                RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
                byte[] buffer = new byte[4];

                /// <remarks>
                /// Creates a random number of iterations that will end being
                /// the amount of parallel tasks (not less than 2 -- up to 9)
                /// </remarks>
                random.GetBytes(buffer);
                int iterations = new Random(BitConverter.ToInt32(buffer, 0)).Next(2, 9);

                /// <remarks>
                /// Creates a helper collection with number/name pairs to
                /// populate the list of parallel tasks
                /// </remarks>
                Dictionary<int, string> items = new Dictionary<int, string>();
                Console.WriteLine("Creating " + iterations + " Parallel Tasks . . .");
                Console.Write(Environment.NewLine);

                for (int i = 1; i < iterations + 1; i++) // cosmetic +1 avoids "Task 0"
                {
                    items.Add(i, String.Format("Task {0}", i));
                }

                List<Task> tasks = new List<Task>();

                // TODO: I guess we might use a Parallel.Foreach() here
                foreach (var item in items)
                {
                    /// <remarks>
                    ///  Creates a random interval (between 1 to 9 seconds)
                    ///  to pause the current thread at
                    /// </remarks>
                    random.GetBytes(buffer);
                    int interval = new Random(BitConverter.ToInt32(buffer, 0)).Next(1000, 9000);

                    /// <remarks>
                    /// NOTE: The 'temp' variable is just a console helper
                    /// </remarks>
                    var temp = item;

                    /// <remarks>
                    /// Creates and starts a new parallel task.
                    /// NOTE: the 'state' as well as the returned value
                    /// could be any type of object.
                    /// </remarks>
                    var task = Task.Factory.StartNew(state =>
                    {
                        Console.WriteLine(String.Format("[{0}] {1} started, will take {2} miliseconds to complete . . .", DateTime.Now.TimeOfDay, temp.Value, interval));
                        Thread.Sleep(interval);

                        return "Lorem ipsum dolor sit amet.";

                        /// <remarks>
                        /// Creates a continuation action so each tasks will print
                        /// its name and return value immediately after completion.  
                        /// </remarks>
                    }, temp.Value).ContinueWith(t => Console.WriteLine(String.Format("[{0}] {1} completed. Result: {2}", DateTime.Now.TimeOfDay, t.AsyncState, t.Result)));

                    tasks.Add(task);
                }

                /// <remarks>
                /// Waits for all running tasks to complete
                /// </remarks>
                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception error)
            {
                Console.WriteLine(String.Format("EXCEPTION: {0}", error.Message));
            }
            finally
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey(true);
            }
        }
    }
}