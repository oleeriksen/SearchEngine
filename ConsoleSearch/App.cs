using System;
using System.Collections.Generic;

namespace ConsoleSearch
{
    public class App
    {
        public App()
        {
        }

        public void Run()
        {
            SearchLogic mSearchLogic = new SearchLogic(new Database());
            

            Console.WriteLine("Console Search");
            
            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                string input = Console.ReadLine();
                if (input.Equals("q")) break;

                var wordIds = new List<int>();
                var searchTerms = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var t in searchTerms) {
                    int id = mSearchLogic.GetIdOf(t);
                    if ( id != -1)
                        wordIds.Add(id);
                    else
                        Console.WriteLine(t + " will be ignored");
                }

                DateTime start = DateTime.Now;

                var result = mSearchLogic.Search(wordIds, 10);

                TimeSpan used = DateTime.Now - start;

                

                int idx = 0;
                foreach (var doc in result.DocumentHits) {
                    Console.WriteLine("" + (idx+1) + ": " + doc.Document.mUrl + " -- contains " + doc.NoOfHits + " search terms");
                    Console.WriteLine("Index time: " + doc.Document.mIdxTime + ". Creation time: " + doc.Document.mCreationTime);
                    Console.WriteLine();
                    idx++;
                }
                Console.WriteLine("Documents: " + result.Hits + ". Time: " + used.TotalMilliseconds);
            }
        }
    }
}
