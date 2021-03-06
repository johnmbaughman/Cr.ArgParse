﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cr.ArgParse.Extensions;

namespace Cr.ArgParse
{
    public class ParseResult : IEnumerable
    {
        private readonly IDictionary<string, object> results;

        public ParseResult(IEqualityComparer<string> equalityComparer = null)
        {
            EqualityComparer = equalityComparer ?? StringComparer.InvariantCultureIgnoreCase;
            results = new Dictionary<string, object>(EqualityComparer);
        }

        public IEqualityComparer<string> EqualityComparer { get; private set; }

        public object this[string s]
        {
            get { return Get(s); }
            set { Set(s, value); }
        }

        public IList<string> UnrecognizedArguments { get; set; }

        public IEnumerator GetEnumerator()
        {
            return results.GetEnumerator();
        }

        public T Get<T>(string argName, T defaultValue = default (T))
        {
            try
            {
                var res = this[argName];
                if (res is T)
                    return (T) res;
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public object Get(string argName)
        {
            lock (((ICollection) results).SyncRoot)
                return results.SafeGetValue(argName);
        }

        public void Set(string argName, object value)
        {
            if (argName == null)
                throw new ArgumentNullException("argName");
            lock (((ICollection) results).SyncRoot)
                results[argName] = value;
        }

        public void Add(string key, object value)
        {
            Set(key, value);
            this[key] = value;
        }

        public void Clear()
        {
            lock (((ICollection) results).SyncRoot)
                results.Clear();
        }

        public IDictionary<string, object> ToDictionary()
        {
            lock (((ICollection) results).SyncRoot)
                return results.ToDictionary(kv => kv.Key,
                    kv =>
                    {
                        var parseResult = kv.Value as ParseResult;
                        return parseResult != null ? parseResult.ToDictionary() : kv.Value;
                    },
                    EqualityComparer);
        }
    }
}