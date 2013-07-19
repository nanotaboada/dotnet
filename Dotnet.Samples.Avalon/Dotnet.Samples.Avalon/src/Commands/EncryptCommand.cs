﻿// -----------------------------------------------------------------------﻿------
// <copyright file="EncryptCommand.cs" company="NanoTaboada">
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
// -----------------------------------------------------------------------﻿------

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "For educational purposes only.")]

namespace Dotnet.Samples.Avalon
{
    using System;
    using System.Windows.Input;

    public class EncryptCommand : ICommand
    {
        private readonly CipherViewModel cipherViewModel;
        private readonly Action action;

        public EncryptCommand(CipherViewModel cipherViewModel, Action action)
        {
            this.cipherViewModel = cipherViewModel;
            this.action = action;

            this.cipherViewModel.PropertyChanged += (s, e) =>
            {
                if (CanExecuteChanged != null &&
                    (e.PropertyName == "Plaintext"
                    || e.PropertyName == "Passphrase"
                    || e.PropertyName == "Salt"))
                {
                    CanExecuteChanged(this, EventArgs.Empty);
                }
            };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(this.cipherViewModel.Plaintext)
                && !string.IsNullOrEmpty(this.cipherViewModel.Passphrase)
                && (!string.IsNullOrEmpty(this.cipherViewModel.Salt) 
                    && this.cipherViewModel.Salt.Length >= 8))
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            this.action.Invoke();
        }
    }
}
