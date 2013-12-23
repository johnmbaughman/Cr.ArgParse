﻿using System.Collections.Generic;

namespace Cr.ArgParse
{
    /// <summary>
    /// Allows to parse list of command line arguments
    /// </summary>
    public interface IArgumentParser
    {
        IParseResult ParseArguments(IEnumerable<string> args);
    }

    public interface IArgumentActionContainer
    {
        IArgumentAction AddArgument(Argument argument);
    }
}