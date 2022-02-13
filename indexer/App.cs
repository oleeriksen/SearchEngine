using System;
using System.Collections.Generic;
using System.IO;

namespace Indexer
{
    public class App
    {
        public App()
        {
        }

        public void Run()
        {
            Database db = new Database();
            Crawler crawler = new Crawler(db);
            

            var root = new DirectoryInfo(@"/Users/ole/data");

            DateTime start = DateTime.Now;

            crawler.IndexFilesIn(root, new List<string> { ".txt"});
            

            TimeSpan used = DateTime.Now - start;
            Console.WriteLine("DONE! used " + used.TotalMilliseconds);

            var all = db.GetAllWords();

            foreach (var p in all)
            {
                Console.WriteLine("<" + p.Key + ", " + p.Value + ">");
                break;
            }


        }


    }
}
