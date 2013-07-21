﻿// ----------------------------------------------------------------------------
// <copyright file="ProgramTests.cs" company="NanoTaboada">
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

namespace Dotnet.Samples.MSTest
{
    using System;
    using System.Data;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProgramTests
    {
        private Program program;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            this.program = new Program();
        }

        [DeploymentItem("Dotnet.Samples.MSTest\\ProgramTestCases.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "ProgramTestCases.xml",
            "Row",
            DataAccessMethod.Sequential),
        TestMethod]
        public void TestCases_WhenRowsExecutedSequentially_ExpectedAndActualValuesAreEqual()
        {
            // Arrange
            var pangrams = this.program.GetPangrams();
            var expectedLanguage = (string)TestContext.DataRow["Language"];
            var expectedPangram = (string)TestContext.DataRow["Pangram"];

            // Act
            var actualLanguage = pangrams
                .Where(language => language.Key == expectedLanguage)
                .SingleOrDefault().Key;

            var actualPangram = pangrams
                .Where(pangram => pangram.Value == expectedPangram)
                .SingleOrDefault().Value;

            // Assert
            Assert.AreEqual(expectedLanguage, actualLanguage);
            Assert.AreEqual(expectedPangram, actualPangram);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.program = null;
        }
    }
}
