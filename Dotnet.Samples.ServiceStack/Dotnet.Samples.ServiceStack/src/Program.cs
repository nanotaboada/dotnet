﻿// ----------------------------------------------------------------------------
// <copyright file="Program.cs" company="NanoTaboada">
//   Copyright (c) 2013 Nano Taboada, http://openid.nanotaboada.com.ar 
// 
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
// 
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
// 
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </copyright>
// -----------------------------------------------------------------------﻿-----

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "For educational purposes only.")]

namespace Dotnet.Samples.ServiceStack
{
    using System;

    public class Program
    {
        public static void Main()
        {
            try 
            {
                var uri = new Uri("http://127.0.0.1:8080/");

                var host = new Host();
                host.Init();

                host.Start(uri.AbsoluteUri);
                Console.WriteLine(string.Format("[RUNNING] ServiceStack Self-Hosted Service at {0}", uri.AbsoluteUri));
                Console.ReadKey(true);
            
                host.Stop();
                Console.WriteLine(string.Format("[STOPPED] ServiceStack Self-Hosted Service at {0}", uri.AbsoluteUri));
            }
            catch (Exception exception)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                Console.Write(Environment.NewLine);
                Console.Write("Press any key to continue . . .");
                Console.ReadKey(true);
            }
        }
    }
}