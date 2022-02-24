using System;
using CommonStuff.BE;

namespace ConsoleSearch
{
    public class DocumentHit
    {
        public DocumentHit(BEDocument doc, int noOfHits)
        {
            Document = doc;
            NoOfHits = noOfHits;
        }

        public BEDocument Document { get;  }

        public int NoOfHits { get;  }
    }
}
