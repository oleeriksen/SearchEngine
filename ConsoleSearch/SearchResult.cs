using System;
using System.Collections.Generic;
using CommonStuff.BE;

namespace ConsoleSearch
{
    public class SearchResult
    {
        public SearchResult(int hits, List<DocumentHit> documents, List<string> ignored, TimeSpan timeUsed)
        {
            Hits = hits;
            DocumentHits = documents;
            Ignored = ignored;
            TimeUsed = timeUsed;
        }

        public int Hits { get; }

        public List<DocumentHit> DocumentHits { get;  }

        public List<string> Ignored { get; }

        public TimeSpan TimeUsed { get;  }
    }
}
