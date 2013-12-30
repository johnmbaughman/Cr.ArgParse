﻿using System;

namespace Cr.ArgParse
{
    public class ArgumentError : Exception
    {
        public ArgumentAction Action { get; private set; }

        public ArgumentError(ArgumentAction action, string message):base(message)
        {
            Action = action;
        }
    }
}