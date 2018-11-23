﻿using System;
using Howatworks.PlayerJournal.Serialization;

namespace Howatworks.PlayerJournal.Processing
{
    public class Handler<T> : IHandler where T : IJournalEntry
    {
        private readonly Func<T, bool> _action;

        public Handler(Func<T, bool> action)
        {
            _action = action;
        }

        public bool Invoke(IJournalEntry journal)
        {
            return _action((T)journal);
        }
    }
}