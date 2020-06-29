using System;
using System.IO;
using TakeHomeTestCheetah.Methods;

namespace TakeHomeTestCheetah
{
    class Program
    {
        static void Main(string[] args)
        {
            string output;
            ReadJSON readJSON = new ReadJSON();
            //readJSON.FilePath = "C:\\Source\\TakeHomeTestCheetah\\data.json";
            readJSON.FilePath =   AppDomain.CurrentDomain.BaseDirectory  + "data.json";
            readJSON.ReadJsonFile();
            output = readJSON.RecepientsSelected;
            Console.Write(output);
        }
    }
}
