using System.Xml.Linq;

namespace DenseIndex
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listView1.HideSelection = false;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
            LoadListView();
            WorkingWithFiles.blockSize = GetBlockSize();
            Console.WriteLine();
        }


        private void LoadListView()
        {

            listView1.Items.Clear();
            List<Record> records = WorkingWithFiles.ReadFromFile("records.dat");



            foreach (Record record in records)
            {
                if (!record.deleted)
                {
                    string[] row = { record.key.ToString(), record.name, record.surname, record.phoneNumber };
                    var listItem = new ListViewItem(row);
                    listView1.Items.Add(listItem);
                }
            }




        }


        private int GetBlockSize()
        {
            //List<string> lines = File.ReadAllLines("records.ind").ToList();
            //char curNum = '-';
            //int num = 0;

            //foreach (var line in lines)
            //{
            //    if (line != "" && line != "/n" && line != "\r" && line is not null)
            //    {
            //        curNum = line.Split(' ')[0][0];
            //        num = lines.FindIndex(x => x == line);
            //        break;
            //    }


            //}

            //if (curNum == '-')
            //{
            //    return 0;
            //}


            //int count = 1;

            //for (int i = num + 1; i < lines.Count; i++)
            //{
            //    if (lines[i] != "" && lines[i] != "/n" && lines[i] != "\r" && lines[i] is not null)
            //    {
            //        string[] fields = lines[i].Split(' ');

            //        char ch = fields[0][0];
            //        if (ch != curNum)
            //        {
            //            return count;
            //        }

            //    }

            //    count++;
            //}

            //return count;

            List<string> lines = File.ReadAllLines("records.ind").ToList();
            return lines.Count / 8;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string key_s = textBox_key.Text;
            bool keyVal = ValidateKey(key_s);

            if (!keyVal)
            {
                Activate();
                DialogResult infoAns = MessageBox.Show(
                        "Key is incorrect. It must be 19 digits number",
                        "Alert",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);

                if (infoAns == DialogResult.OK)
                {
                    Activate();
                }

                return;
            }

            long key = Convert.ToInt64(key_s);

            List<Record> records = WorkingWithFiles.ReadFromFile("records.dat");
            List<string> indexLines = File.ReadAllLines("records.ind").ToList();

            int blockNum = int.Parse(key.ToString()[0].ToString());
            List<Index> indexes = new List<Index>();
            for (int i = WorkingWithFiles.blockSize * (blockNum - 1); i < WorkingWithFiles.blockSize * blockNum; i++)
            {
                if (indexLines[i] == "\n" || indexLines[i] == "\r" || indexLines[i] == "" || indexLines[i] is null)
                {
                    break;
                }
                else
                {
                    string[] fields = indexLines[i].Split(' ');
                    indexes.Add(new Index(Convert.ToInt64(fields[0]), Convert.ToInt32(fields[1])));
                }
            }

            long[] index_arr = new long[indexes.Count];
            int inc = 0;
            foreach (var ind in indexes)
            {
                index_arr[inc] = ind.key;
                inc++;
            }

            int count_rf = 0;
            int position = SharSearch.Search(index_arr, key, ref count_rf);
            if (index_arr[position]!=key || position==-1)
            {
                position = Array.BinarySearch(index_arr, key);
            }

            if (position < 0)
            {
                Activate();
                DialogResult infoAns = MessageBox.Show(
                        "The record with specified key was not found",
                        "Alert",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);

                if (infoAns == DialogResult.OK)
                {
                    Activate();
                }

                return;
            }

            int foundRecNum = indexes[position].number;
            int delcount = 0;
            for (int i = 0; i < foundRecNum; i++)
            {
                if (records[i].deleted)
                {
                    delcount++;
                }
            }
            listView1.Focus();
            listView1.Items[foundRecNum - delcount].Selected = true;
            listView1.EnsureVisible(foundRecNum - delcount);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DELETE
            DialogResult delAns = MessageBox.Show(
                            "Are you sure you want to delete the entry",
                            "Alert",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2,
                            MessageBoxOptions.DefaultDesktopOnly);

            if (delAns == DialogResult.Yes)
            {
                bool selected = listView1.SelectedItems.Count > 0;
                if (!selected)
                {
                    Activate();
                    DialogResult infoAns = MessageBox.Show(
                            "The record is not selected",
                            "Alert",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);

                    if (infoAns==DialogResult.OK)
                    {
                        Activate();
                    }

                    return;
                }
                this.Activate();
                ListViewItem item = listView1.SelectedItems[0];
                long key = Convert.ToInt64(item.Text.Split(' ')[0]);

                List<string> indexLines = File.ReadAllLines("records.ind").ToList();
                List<Record> records = WorkingWithFiles.ReadFromFile("records.dat");

                int blockNum = int.Parse(key.ToString()[0].ToString());
                List<Index> indexes = new List<Index>();
                for (int i = WorkingWithFiles.blockSize * (blockNum - 1); i < WorkingWithFiles.blockSize * blockNum; i++)
                {
                    if (indexLines[i] == "\n" || indexLines[i] == "\r" || indexLines[i] == "" || indexLines[i] is null)
                    {
                        break;
                    }
                    else
                    {
                        string[] fields = indexLines[i].Split(' ');
                        indexes.Add(new Index(Convert.ToInt64(fields[0]), Convert.ToInt32(fields[1])));
                    }
                }

                long[] index_arr = new long[indexes.Count];
                int inc = 0;
                foreach (var ind in indexes)
                {
                    index_arr[inc] = ind.key;
                    inc++;
                }

                int count_rf = 0;
                int position = SharSearch.Search(index_arr, key, ref count_rf);
                if (index_arr[position] != key || position == -1)
                {
                    position = Array.BinarySearch(index_arr, key);
                }

                int foundRecNum = indexes[position].number;

                indexLines[WorkingWithFiles.blockSize * (blockNum - 1) + position] = "";

                for (int i = WorkingWithFiles.blockSize * (blockNum - 1) + position; i < WorkingWithFiles.blockSize * blockNum; i++)
                {
                    if (indexLines[i + 1] == "" || indexLines[i + 1] == "/n" || indexLines[i + 1] == "/r" || indexLines[i + 1] is null)
                    {
                        break;
                    }
                    string temp = indexLines[i + 1];
                    indexLines[i + 1] = indexLines[i];
                    indexLines[i] = temp;
                }
                records[foundRecNum].deleted = true;

                File.WriteAllLines("records.ind", indexLines);
                WorkingWithFiles.WriteInFile("records.dat", records);

                LoadListView();


            }
            else
            {
                Activate();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_surname_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //EDIT
            DialogResult editAns = MessageBox.Show(
                            "Are you sure you want to edit the entry",
                            "Alert",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2,
                            MessageBoxOptions.DefaultDesktopOnly);

            if (editAns == DialogResult.Yes)
            {
                #region prep

                Activate();
                string name = textBox_name.Text;
                string surname = textBox_surname.Text;
                string phoneNum = textBox_phone_num.Text;
                bool nameVal = ValidateName(name);
                bool surnameVal = ValidateSurname(surname);
                bool phoneVal = ValidatePhoneNum(phoneNum);

                if (listView1.SelectedItems.Count < 1)
                {
                    Activate();
                    DialogResult infoAns = MessageBox.Show(
                            "The record is not selected",
                            "Alert",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);

                    if (infoAns == DialogResult.OK)
                    {
                        Activate();
                    }

                    return;
                }

                if (!nameVal)
                {
                    Activate();
                    DialogResult infoAns = MessageBox.Show(
                            "Name is incorrect. Fitst letter must be upper case, it must not contain spaces, maximum 30 chars",
                            "Alert",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);

                    if (infoAns == DialogResult.OK)
                    {
                        Activate();
                    }

                    return;
                }

                if (!surnameVal)
                {
                    Activate();
                    DialogResult infoAns = MessageBox.Show(
                            "Suranme is incorrect. Fitst letter must be upper case, it must not contain spaces, maximum 30 chars",
                            "Alert",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);

                    if (infoAns == DialogResult.OK)
                    {
                        Activate();
                    }

                    return;
                }

                if (!phoneVal)
                {
                    Activate();
                    DialogResult infoAns = MessageBox.Show(
                            "Phone number is incorrect. Fitst symbol must +, other chars must be digits, length must be 12",
                            "Alert",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);

                    if (infoAns == DialogResult.OK)
                    {
                        Activate();
                    }

                    return;
                }

                #endregion

                ListViewItem item = listView1.SelectedItems[0];
                long key = Convert.ToInt64(item.Text);

                List<string> indexLines = File.ReadAllLines("records.ind").ToList();
                List<Record> records = WorkingWithFiles.ReadFromFile("records.dat");

                int blockNum = int.Parse(key.ToString()[0].ToString());
                List<Index> indexes = new List<Index>();
                for (int i = WorkingWithFiles.blockSize * (blockNum - 1); i < WorkingWithFiles.blockSize * blockNum; i++)
                {
                    if (indexLines[i] == "\n" || indexLines[i] == "\r" || indexLines[i] == "" || indexLines[i] is null)
                    {
                        break;
                    }
                    else
                    {
                        string[] fields = indexLines[i].Split(' ');
                        indexes.Add(new Index(Convert.ToInt64(fields[0]), Convert.ToInt32(fields[1])));
                    }
                }

                long[] index_arr = new long[indexes.Count];
                int inc = 0;
                foreach (var ind in indexes)
                {
                    index_arr[inc] = ind.key;
                    inc++;
                }

                int count_rf = 0;
                int position = SharSearch.Search(index_arr, key, ref count_rf);
                if (index_arr[position] != key || position == -1)
                {
                    position = Array.BinarySearch(index_arr, key);
                }

                int foundRecNum = indexes[position].number;

                records[foundRecNum].name = name;
                records[foundRecNum].surname = surname;
                records[foundRecNum].phoneNumber = phoneNum;

                WorkingWithFiles.WriteInFile("records.dat", records);

                LoadListView();
            }
            else
            {
                Activate();
            }
        }


        private bool ValidateName(string name)
        {

            if (name.Length == 0 || name == "/n" || name is null || name == "\r" || name == "")
            {
                return false;
            }

            if (name.Contains(' '))
            {
                return false;
            }

            if (name.Length > 30)
            {
                return false;
            }

            if (name.Any(x => !char.IsLetter(x)))
            {
                return false;
            }

            if (!Char.IsUpper(name[0]))
            {
                return false;
            }

            return true;
        }

        private bool ValidateSurname(string surname)
        {
            if (surname.Length == 0 || surname == "/n" || surname is null || surname == "\r" || surname == "")
            {
                return false;
            }
            if (surname.Contains(' '))
            {
                return false;
            }
            if (surname.Length > 30)
            {
                return false;
            }

            if (surname.Any(x => !char.IsLetter(x)))
            {
                return false;
            }

            if (!Char.IsUpper(surname[0]))
            {
                return false;
            }

            return true;
        }

        private bool ValidatePhoneNum(string num)
        {
            if (num is null)
            {
                return false;
            }

            if (num.Length != 12)
            {
                return false;
            }

            if (num[0] != '+')
            {
                return false;
            }

            for (int i = 1; i < 12; i++)
            {
                if (!char.IsDigit(num[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateKey(string key)
        {
            if (key is null)
            {
                return false;
            }

            if (key.Length != 19)
            {
                return false;
            }

            if (key.Any(x => !char.IsDigit(x)))
            {
                return false;
            }

            if (key[0] == '0' || key[0] == '9')
            {
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ADD
            DialogResult editAns = MessageBox.Show(
                            "Are you sure you want to add the entry",
                            "Alert",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2,
                            MessageBoxOptions.DefaultDesktopOnly);

            if (editAns == DialogResult.Yes)
            {
                #region prep

                Activate();
                string name = textBox_name.Text;
                string surname = textBox_surname.Text;
                string phoneNum = textBox_phone_num.Text;
                bool nameVal = ValidateName(name);
                bool surnameVal = ValidateSurname(surname);
                bool phoneVal = ValidatePhoneNum(phoneNum);


                if (!nameVal)
                {
                    Activate();
                    DialogResult infoAns = MessageBox.Show(
                            "Name is incorrect. Fitst letter must be upper case, it must not contain spaces, maximum 30 chars",
                            "Alert",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);

                    if (infoAns == DialogResult.OK)
                    {
                        Activate();
                    }

                    return;
                }

                if (!surnameVal)
                {
                    Activate();
                    DialogResult infoAns = MessageBox.Show(
                            "Suranme is incorrect. Fitst letter must be upper case, it must not contain spaces, maximum 30 chars",
                            "Alert",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);

                    if (infoAns == DialogResult.OK)
                    {
                        Activate();
                    }

                    return;
                }

                if (!phoneVal)
                {
                    Activate();
                    DialogResult infoAns = MessageBox.Show(
                            "Phone number is incorrect. Fitst symbol must +, other chars must be digits, length must be 12",
                            "Alert",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);

                    if (infoAns == DialogResult.OK)
                    {
                        Activate();
                    }

                    return;
                }

                #endregion

                List<string> indexLines = File.ReadAllLines("records.ind").ToList();
                List<Record> records = WorkingWithFiles.ReadFromFile("records.dat");
                Random rnd = new Random();
                string skey = rnd.Next(1, 9).ToString() + DateTime.Now.Ticks.ToString();
                long key = Convert.ToInt64(skey);
                Record newRecord = new Record(key, name, surname, phoneNum, false);
                records.Add(newRecord);

                int blockNum = int.Parse(key.ToString()[0].ToString());
                List<Index> indexes = new List<Index>();
                for (int i = WorkingWithFiles.blockSize * (blockNum - 1); i < WorkingWithFiles.blockSize * blockNum; i++)
                {
                    if (indexLines[i] == "\n" || indexLines[i] == "\r" || indexLines[i] == "" || indexLines[i] is null)
                    {
                        break;
                    }
                    else
                    {
                        string[] fields = indexLines[i].Split(' ');
                        indexes.Add(new Index(Convert.ToInt64(fields[0]), Convert.ToInt32(fields[1])));
                    }
                }

                if (indexes.Count < WorkingWithFiles.blockSize)
                {
                    Index newInd = new Index(key, records.Count - 1);
                    indexes.Add(newInd);
                    indexes.Sort((p, q) => p.key.CompareTo(q.key));

                    List<string> newBlockInd = new List<string>();

                    foreach (var item in indexes)
                    {
                        newBlockInd.Add(item.ToString());
                    }

                    for (int l = 0; l < WorkingWithFiles.blockSize - indexes.Count; l++)
                    {
                        newBlockInd.Add("");
                    }

                    int index = 0;

                    for (int i = WorkingWithFiles.blockSize * (blockNum - 1); i < WorkingWithFiles.blockSize * blockNum; i++)
                    {
                        indexLines[i] = newBlockInd[index];
                        index++;
                    }

                    WorkingWithFiles.WriteInFile("records.dat", records);
                    File.WriteAllLines("records.ind", indexLines);
                    LoadListView();
                    int addeddRecNum = records.Count - 1;
                    int delcount = 0;
                    for (int i = 0; i < addeddRecNum; i++)
                    {
                        if (records[i].deleted)
                        {
                            delcount++;
                        }
                    }
                    listView1.Focus();
                    listView1.Items[addeddRecNum - delcount].Selected = true;
                    listView1.EnsureVisible(addeddRecNum - delcount);

                }
                else
                {
                    List<string> newIndexLines = new List<string>();

                    for (int i = 1; i <= 8; i++)
                    {

                        for (int j = WorkingWithFiles.blockSize * (i - 1); j < WorkingWithFiles.blockSize * i; j++)
                        {
                            newIndexLines.Add(indexLines[j]);
                        }

                        for (int l = 0; l < WorkingWithFiles.blockSize; l++)
                        {
                            newIndexLines.Add("");
                        }


                    }

                    WorkingWithFiles.blockSize *= 2;



                    Index newInd = new Index(key, records.Count - 1);
                    indexes.Add(newInd);
                    indexes.Sort((p, q) => p.key.CompareTo(q.key));

                    List<string> newBlockInd = new List<string>();

                    foreach (var item in indexes)
                    {
                        newBlockInd.Add(item.ToString());
                    }

                    for (int l = 0; l < WorkingWithFiles.blockSize - indexes.Count; l++)
                    {
                        newBlockInd.Add("");
                    }

                    int index = 0;

                    for (int i = WorkingWithFiles.blockSize * (blockNum - 1); i < WorkingWithFiles.blockSize * blockNum; i++)
                    {
                        newIndexLines[i] = newBlockInd[index];
                        index++;
                    }



                    WorkingWithFiles.WriteInFile("records.dat", records);
                    File.WriteAllLines("records.ind", newIndexLines);
                    LoadListView();
                    int addeddRecNum = records.Count - 1;
                    int delcount = 0;
                    for (int i = 0; i < addeddRecNum; i++)
                    {
                        if (records[i].deleted)
                        {
                            delcount++;
                        }
                    }
                    listView1.Focus();
                    listView1.Items[addeddRecNum - delcount].Selected = true;
                    listView1.EnsureVisible(addeddRecNum - delcount);


                }


            }
            else
            {
                Activate();
            }

        }
    }
}