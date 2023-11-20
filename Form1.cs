using CsvHelper;
using ModularityTestingApp.model;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using CsvHelper.Configuration;

namespace ModularityTestingApp
{
    public partial class Form1 : Form
    {

        List<ClassList> classList = new List<ClassList>();
        List<ObjectList> objectList = new List<ObjectList>();

        int objectUsage = 0;
        int totalComment = 0;
        int totalOccurance = 0;

        string stringNumber = "";
        string stringObjectName = "";
        string stringObjectUsage = "";
        string stringComment = "";
        string stringOccurance = "";
        string stringSimplicitySum = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            StringBuilder sb = new StringBuilder();
            string? line;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] files = Directory.GetFiles(dialog.SelectedPath);
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string fileExt = Path.GetExtension(file);
                        if (fileExt.CompareTo(".cs") == 0)
                        {
                            ClassList classItem = new ClassList();
                            classItem.ClassName = fileName;
                            classList.Add(classItem);
                        }
                    }
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string fileExt = Path.GetExtension(file);
                        string newLine = "";
                        string[] lineArr = Array.Empty<string>();
                        string[] lineArr2;

                        if (fileExt.CompareTo(".cs") == 0)
                        {
                            for (int i = 0; i < classList?.Count; i++)
                            {
                                if (classList[i].ClassName != fileName)
                                {
                                    StreamReader sr = new StreamReader(file);
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        if (Regex.IsMatch(line, $" {classList[i].ClassName} " ?? ""))
                                        {
                                            lineArr = line.Split(classList[i].ClassName);
                                            newLine = lineArr[1];
                                            lineArr2 = newLine.Split(" ");
                                            ObjectList objectItem = new ObjectList();
                                            objectItem.ClassName = classList[i].ClassName;
                                            objectItem.ObjectName = lineArr2[1];
                                            objectItem.ObjectUsage = 0;
                                            objectItem.Comment = 0;
                                            objectItem.Occurance = 0;
                                            objectItem.Simplicity = 0;
                                            objectList.Add(objectItem);
                                        }
                                        sb.AppendLine(line);
                                    }
                                    sr.Close();
                                }
                                else
                                {
                                    ObjectList objectItem = new ObjectList();
                                    objectItem.ClassName = classList[i].ClassName;
                                    objectItem.ObjectName = "";
                                    objectItem.ObjectUsage = 0;
                                    objectItem.Comment = 0;
                                    objectItem.Occurance = 0;
                                    objectItem.Simplicity = 0;
                                    objectList.Add(objectItem);
                                }
                            }
                        }

                    }
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string fileExt = Path.GetExtension(file);
                        if (fileExt.CompareTo(".cs") == 0)
                        {
                            foreach (ObjectList objectItem in objectList)
                            {
                                StreamReader sr = new StreamReader(file);
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (Regex.IsMatch(line, objectItem.ObjectName ?? "") && objectItem.ObjectName != "")
                                    {
                                        objectUsage++;
                                        objectItem.ObjectUsage += objectUsage;
                                    }
                                    sb.AppendLine(line);
                                }
                                sr.Close();
                                objectUsage = 0;
                            }
                        }
                    }
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string fileExt = Path.GetExtension(file);
                        if (fileExt.CompareTo(".cs") == 0)
                        {
                            foreach (ObjectList objectItem in objectList)
                            {
                                StreamReader sr = new StreamReader(file);
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (objectItem.ClassName != fileName)
                                    {
                                        if (Regex.IsMatch(line, @"^\s*//") || Regex.IsMatch(line, @"/\*.*\*/"))
                                        {
                                            totalComment++;
                                        }
                                    }
                                    sb.AppendLine(line);
                                }
                                objectItem.Comment += totalComment;
                                sr.Close();
                                totalComment = 0;
                            }
                        }
                    }
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string fileExt = Path.GetExtension(file);
                        if (fileExt.CompareTo(".cs") == 0)
                        {
                            foreach (ObjectList objectItem in objectList)
                            {
                                StreamReader sr = new StreamReader(file);
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (objectItem.ClassName != fileName)
                                    {
                                        if (Regex.IsMatch(line, @"^\s*//") || Regex.IsMatch(line, @"/\*.*\*/"))
                                        {
                                            totalOccurance++;
                                        }
                                    }
                                    sb.AppendLine(line);
                                }
                                objectItem.Occurance += totalOccurance;
                                sr.Close();
                                totalOccurance = 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                    int number = 0;
                    foreach (var item in objectList)
                    {
                        if (item.ObjectName != "")
                        {
                            stringObjectName = $"{item.ClassName} / {item.ObjectName}";
                        }
                        else
                        {
                            stringObjectName = $"{item.ClassName} / -";
                        }
                        item.Simplicity = item.Comment / item.Comment;
                        number++;
                        stringNumber = number.ToString();
                        stringObjectUsage = item.ObjectUsage.ToString() ?? "";
                        stringComment = item.Comment.ToString() ?? "";
                        stringOccurance = item.Occurance.ToString() ?? "";
                        stringSimplicitySum = item.Simplicity.ToString() ?? "";
                        dataGridView1.Rows.Add(stringNumber, stringObjectName, stringObjectUsage, stringComment, stringOccurance, stringSimplicitySum);
                    }                
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Select the folder of project which you want to test first!");
            }
        }


        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int number = 0;
                var records = new List<Data>();
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
                {
                    sfd.Title = "Select the folder of project which you want to test";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        List<string> numbers = new List<string>();
                        List<string> classNames = new List<string>();
                        List<string> objUsage = new List<string>();
                        List<string> comment = new List<string>();
                        List<string> occurance = new List<string>();
                        List<string> simplicity = new List<string>();
                        foreach (var item in objectList)
                        {
                            if (item.ObjectName != "")
                            {
                                classNames.Add($"{item.ClassName} / {item.ObjectName}");
                            }
                            else
                            {
                                classNames.Add($"{item.ClassName} / -");
                            }
                            number++;
                            item.Simplicity = item.Comment / item.Comment;
                            numbers.Add(number.ToString());
                            objUsage.Add(item.ObjectUsage.ToString() ?? "");
                            comment.Add(item.Comment.ToString() ?? "");                            
                            occurance.Add(item.Occurance.ToString() ?? "");
                            simplicity.Add(item.Simplicity.ToString() ?? "");
                        }
                        int lim = numbers.Count();
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            sw.WriteLine("sep=,");
                            using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
                            {
                                for (int i = 0; i < lim; i++)
                                {

                                    records.Add(new Data { Number = numbers[i].ToString(), Objects = classNames[i], Usage = objUsage[i].ToString(), Comment = comment[i].ToString(), Occurance = occurance[i].ToString(), Simplicity = simplicity[i].ToString() });
                                   
                                }
                                cw.Context.RegisterClassMap<csvMap>();
                                cw.WriteRecords(records);
                            }
                        }

                        MessageBox.Show("Your data has been successfully saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Select the folder of project which you want to test first!");
            }
        }
    }
}