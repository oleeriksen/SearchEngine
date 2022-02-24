using System;
using System.Collections.Generic;
using CommonStuff.BE;

namespace ConsoleSearch
{
    public class SearchResult
    {
        public SearchResult(int hits, List<DocumentHit> documents)
        {
            Hits = hits;
            DocumentHits = documents;
            
        }

        public int Hits { get; }

        public List<DocumentHit> DocumentHits { get;  }
    }
}
