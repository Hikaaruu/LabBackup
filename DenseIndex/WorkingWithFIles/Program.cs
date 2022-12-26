using RandomNameGeneratorLibrary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using WorkingWithFIles;

namespace WorkingWithFIles
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Record> genList = FileFill.RecordGenerator(200);
            FileFill.WriteInFile("records.dat", genList);
            FileFill.IndexRecords(genList);

            List<Record> fileList = FileFill.ReadFromFile("records.dat");
            FileFill.DisplayRecords(fileList);

            //long[] arr = new long[100000];
            //var rnd = new Random();
            //for (int i = 0; i < 100000; i++)
            //{
            //    arr[i] = rnd.Next();
            //}
            //Array.Sort(arr);

            //for (int i = 0; i < 15; i++)
            //{
            //    int indexToSearch = rnd.Next(0,arr.Length);
            //    long keyToSearch = arr[indexToSearch];
            //    int count = 0;
            //    int pos = SharSort.SharSearch(arr, keyToSearch, ref count);
            //    if (pos==indexToSearch)
            //    {
            //        Console.WriteLine((i+1).ToString() + ")  " + count.ToString());
            //    }
            //    else
            //    {
            //        Console.WriteLine("fail");
            //    }
            //}





        }
    }
}