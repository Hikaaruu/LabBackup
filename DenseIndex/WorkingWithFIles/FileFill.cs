using Microsoft.VisualBasic;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;

namespace WorkingWithFIles
{
    [Serializable]
    public class Record
    {
        public long key;
        public string? name;
        public string? surname;
        public string? phoneNumber;
        public bool deleted;

        public Record(long _key, string? _name, string? _surname, string? _phoneNumber, bool _deleted)
        {
            key = _key;
            name = _name;
            surname = _surname;
            phoneNumber = _phoneNumber;
            deleted = _deleted;
        }

        public override string ToString()
        {
            return key.ToString() + " " + name + " " + surname + " " + phoneNumber + " " + deleted.ToString();
        }
    }

    public class FileFill
    {
        public static List<Record> RecordGenerator(int count)
        {
            List<Record> records = new List<Record>();
            Record temp;
            StringBuilder phoneNum;
            Random rnd = new Random();
            var personGenerator = new PersonNameGenerator();
            for (int i = 0; i < count; i++)
            {
                string skey = rnd.Next(1, 9).ToString() + DateTime.Now.Ticks.ToString();
                long key = Convert.ToInt64(skey);
                var name = personGenerator.GenerateRandomFirstName();
                var surname = personGenerator.GenerateRandomLastName();
                phoneNum = new StringBuilder("+120" + rnd.Next(10000000, 99999999).ToString());
                temp = new Record(key, name.ToString(), surname.ToString(), phoneNum.ToString(), false);
                records.Add(temp);
            }
            return records;
        }



        public static void WriteInFile(string fileName, List<Record> records)
        {
            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            //foreach (var item in records)
            //{
            //    formatter.Serialize(stream,item);
            //}

            //stream.Close();

            using StreamWriter file = new(fileName, true);
            foreach (Record record in records)
            {
                file.Write(record.ToString());
                file.WriteLine();
            }
            file.Close();
        }


        public static List<Record> ReadFromFile(string fileName)
        {
            //List<Record> records = new List<Record>();
            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            //while (stream.Position<stream.Length)
            //{
            //    Record obj = (Record)formatter.Deserialize(stream);
            //    records.Add(obj);
            //}



            //stream.Close();
            //return records;

            List<Record> records = new List<Record>();
            using StreamReader file = new(fileName);
            List<string> lines = new List<string>();
            string a;
            while (!file.EndOfStream)
            {
                a = file.ReadLine();
                if (a is not null && a != "\n" && a != "\r")
                {
                    lines.Add(a);
                }
            }
            Record temp;
            foreach (var line in lines)
            {
                string[] fields = line.Split(' ');
                temp = new Record(Convert.ToInt64(fields[0]), fields[1], fields[2], fields[3], Convert.ToBoolean(fields[4]));
                records.Add(temp);
            }
            return records;
        }

        public static void DisplayRecords(List<Record> records)
        {
            foreach (Record item in records)
            {
                Console.WriteLine(item.key + "          " + item.name.PadRight(15) + " " + item.surname.PadRight(15) + "        " + item.phoneNumber + "        " + item.deleted.ToString());
            }
        }


        public static void IndexRecords(List<Record> records)
        {
            var dic = new Dictionary<long, int>();
            for (int i = 0; i < records.Count; i++)
            {
                dic.Add(records[i].key, i);
            }

            int[] mas = new int[8];

            foreach (var record in records)
            {
                int number = int.Parse(record.key.ToString().First().ToString());
                mas[number - 1]++;
            }

            int max = mas.Max();

            int blockSize = max * 2;
            int blockCount = 8;

            records.Sort((p, q) => p.key.CompareTo(q.key));
           
            List<string> finalList = new List<string>();

            char curNum = records[0].key.ToString().First();
            int count = 0;

            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].key.ToString()[0] == curNum)
                {
                    finalList.Add(records[i].key.ToString() + " " + dic[records[i].key].ToString());
                    count++;
                }
                else
                {
                    int blockskipped = (records[i].key.ToString().First() - curNum)-1;

                    curNum = records[i].key.ToString().First();
                    for (int j = 0; j < blockSize-count; j++)
                    {
                        finalList.Add("");
                    }
                    count = 0;

                    for (int f = 0; f < blockSize*blockskipped; f++)
                    {
                        finalList.Add("");
                    }

                    finalList.Add(records[i].key.ToString() + " " + dic[records[i].key].ToString());
                    count++;
                }
            }
            for (int j = 0; j < blockSize - count; j++)
            {
                finalList.Add("");
            }
            if (curNum!='8')
            {
                int skipped =  '8' - curNum;
                for (int f = 0; f < blockSize * skipped; f++)
                {
                    finalList.Add("");
                }
            }
            
            File.WriteAllLines("records.ind", finalList);
        }

    }
}
