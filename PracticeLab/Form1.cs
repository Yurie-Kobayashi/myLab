using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;


namespace PracticeLab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            PracticeOfXml jump = new PracticeOfXml();
            jump.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LamdaLinQ jump = new LamdaLinQ();
            jump.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] minits = "10 10 10".Split(' ');

            int honsu = int.Parse("3");

            var margedTrainTimes = new string[honsu];


            string[] input = new string[3];
            input[0] = "8 05";
            input[1] = "8 15";
            input[2] = "5 25";

            for (int count = 0; count < honsu; count++)
            {
                margedTrainTimes[count] = input[count];
            }

            int minitsOfTeisu = int.Parse(minits[0]) + int.Parse(minits[1]) + int.Parse(minits[2]);

            int hittyakuMinit = (8 * 60 + 59) - minitsOfTeisu;

            string[] answers = new string[2];

            foreach (string DensyaTime in margedTrainTimes)
            {
                var bunretuTrainTime = DensyaTime.Split(' ');
                int Denminits = int.Parse(bunretuTrainTime[0]) * 60 + int.Parse(bunretuTrainTime[1]);
                if (hittyakuMinit > Denminits)
                {

                    int syuppatu = Denminits - int.Parse(minits[0]);

                    int min = syuppatu % 60;
                    int hour = syuppatu / 60;

                    string a1 = min.ToString();
                    if (int.Parse(a1) < 10)
                    {
                        a1 = "0" + a1;
                    }
                    string a2 = hour.ToString();
                    if (int.Parse(a2) < 10)
                    {
                        a2 = "0" + a2;
                    }
                    answers[0] = a2;
                    answers[1] = a1;
                }
            }

            string answerText = answers[0] + ":" + answers[1];


            MessageBox.Show(answerText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ExcelPass = "sanplexls.xlsx";

            Excel.Application ExcelApp = null;
            Excel.Workbooks wbs = null;
            Excel.Workbook wb = null;
            Excel.Sheets shs = null;
            Excel.Worksheet ws = null;

            try
            {

                ExcelApp = new Excel.Application();
                wbs = ExcelApp.Workbooks;
                wb = ExcelApp.Workbooks.Open(@"sanplexls.xlsx");

                shs = wb.Sheets;
                ws = shs[1];
                ws.Select(Type.Missing);

                ExcelApp.Visible = true;

                for (int i = 1; i < 10; i++)
                {
                    Excel.Range w_rgn = ws.Cells;
                    Excel.Range rgn = w_rgn[i, 1];

                    try
                    {
                        rgn.Value2 = "I'm Excel speaking from VisualStudio…";
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(w_rgn);
                        Marshal.ReleaseComObject(rgn);
                        w_rgn = null;
                        rgn = null;
                    }

                }

                wb.Save();
                wb.Close(false);
                ExcelApp.Quit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(shs);
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(wbs);
                Marshal.ReleaseComObject(ExcelApp);
                ws = null;
                shs = null;
                wb = null;
                wbs = null;
                ExcelApp = null;

                GC.Collect();

            };



            //Excel.Application xel;
            //xel = new Excel.Application();
            //Excel.Workbook xelBook;
            //xel.Visible = true;

            //xelBook = (Excel.Workbook)(xel.Workbooks.Open
            //(
            //ExcelPass, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing

            //));


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Excel.Application ExcelApp = null;
            Excel.Workbooks wbs = null;
            Excel.Workbook wb = null;
            Excel.Sheets shs = null;
            Excel.Worksheet ws = null;

            try
            {
                ExcelApp = new Excel.Application();
                wbs = ExcelApp.Workbooks;
                wb = ExcelApp.Workbooks.Open(@"sanplexls.xlsx");

                shs = wb.Sheets;
                ws = shs[1];
                ws.Select(Type.Missing);

                ExcelApp.Visible = true;

                string[] HedderList = new string[] { "", "one", "two", "three", "four", "five" };
                int count = 1;

                foreach (string hed in HedderList)
                {
                    Excel.Range w_rgn = ws.Cells;
                    Excel.Range rgn = w_rgn[1, count];

                    try
                    {
                        rgn.Value2 = hed;
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(w_rgn);
                        Marshal.ReleaseComObject(rgn);
                        w_rgn = null;
                        rgn = null;
                    }
                    count++;
                }
                wb.Save();
                wb.Close(false);
                ExcelApp.Quit();

            }
            finally
            {
                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(shs);
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(wbs);
                Marshal.ReleaseComObject(ExcelApp);
                ws = null;
                shs = null;
                wb = null;
                wbs = null;
                ExcelApp = null;

                GC.Collect();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Excel.Application ExcelApp = null;
            Excel.Workbooks wbs = null;
            Excel.Workbook wb = null;
            Excel.Sheets shs = null;
            Excel.Worksheet ws = null;


            DataTable dt = new DataTable();
            dt.Columns.Add("1");
            dt.Columns.Add("2");
            dt.Columns.Add("3");
            dt.Columns.Add("4");
            dt.Columns.Add("5");

            var dr1 = dt.NewRow();
            dr1["1"] = "ワン";
            dr1["2"] = "ツー";
            dr1["3"] = "スリー";
            dr1["4"] = "フォー";
            dr1["5"] = "ファイブ";
            dt.Rows.Add(dr1);

            var dr2 = dt.NewRow();
            dr2["1"] = "いち";
            dr2["2"] = "に";
            dr2["3"] = "さん";
            dr2["4"] = "よん";
            dr2["5"] = "ご";
            dt.Rows.Add(dr2);

            var columnList = new List<string>() { "1", "2", "3", "4", "5" };

            int rowcount = 2;
            foreach (DataRow dr in dt.Rows)
            {

                try
                {

                    ExcelApp = new Excel.Application();
                    wbs = ExcelApp.Workbooks;
                    wb = ExcelApp.Workbooks.Open(@"sanplexls.xlsx");

                    shs = wb.Sheets;
                    ws = shs[1];
                    ws.Select(Type.Missing);

                    ExcelApp.Visible = true;


                    var targetList = new List<string>();
                    foreach (string column in columnList)
                    {
                        targetList.Add(dr[column].ToString());
                    }

                    int columnCount = 2;
                    foreach (string target in targetList)
                    {
                        Excel.Range w_rgn = ws.Cells;
                        Excel.Range rgn = w_rgn[rowcount, columnCount];
                        try
                        {
                            rgn.Value2 = target;
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(w_rgn);
                            Marshal.ReleaseComObject(rgn);
                            w_rgn = null;
                            rgn = null;
                        }
                        columnCount++;
                    }
                    wb.Save();
                    wb.Close(false);
                    ExcelApp.Quit();

                }
                finally
                {
                    Marshal.ReleaseComObject(ws);
                    Marshal.ReleaseComObject(shs);
                    Marshal.ReleaseComObject(wb);
                    Marshal.ReleaseComObject(wbs);
                    Marshal.ReleaseComObject(ExcelApp);
                    ws = null;
                    shs = null;
                    wb = null;
                    wbs = null;
                    ExcelApp = null;

                    GC.Collect();
                }

            }


        }

        private void button7_Click(object sender, EventArgs e)
        {

            string[] ans = Grep("C:\\Users\\Yurie\\Desktop\\test", "2020", "*.docx");

            MessageBox.Show(ans.Count().ToString());

            //Encoding enc = Encoding.GetEncoding("Shift_JIS");
            //StreamWriter sw = new StreamWriter(@"C:\Users\Yurie\Desktop",false,enc);

            string ansText = "";

            foreach (string oneans in ans)
            {
                ansText = ansText + ans;
                ansText = ansText + "\r\n";
            }

            MessageBox.Show(ansText);

            //sw.Write(ansText);
            //sw.Close();

            ////Shift JISで書き込む
            ////書き込むファイルが既に存在している場合は、上書きする
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(
            //    @"C:\test\1.txt",
            //    false,
            //    System.Text.Encoding.GetEncoding("shift_jis"));
            ////TextBox1.Textの内容を書き込む
            //sw.Write(TextBox1.Text);
            ////閉じる
            //sw.Close();

        }



        private string[] Grep(string path, string pattern, string fileWildcard, bool ignoreCase = true)
        {

            System.Collections.ArrayList fileNameMatch = new System.Collections.ArrayList();

            System.Text.RegularExpressions.RegexOptions opts = System.Text.RegularExpressions.RegexOptions.None;

            if (ignoreCase)
            {
                opts |= System.Text.RegularExpressions.RegexOptions.IgnoreCase;
            }

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(pattern, opts);

            //フォルダ内にあるファイルを取得
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] files = dir.GetFiles(fileWildcard);
            foreach (System.IO.FileInfo f in files)
            {
                //一つずつファイルを調べる
                if (ContainTextInFile(f.FullName, reg))
                    fileNameMatch.Add(f.FullName);

            }


            return (string[])fileNameMatch.ToArray(typeof(string));
        }

        //ファイルの内容がpatternに一致するか調べる
        private static bool ContainTextInFile(
            string fileName, System.Text.RegularExpressions.Regex reg)
        {
            //ファイルを読み込む
            System.IO.StreamReader strm = null;
            string txt = "";

            try
            {
                strm = new System.IO.StreamReader(
                    fileName, System.Text.Encoding.GetEncoding("Unicode"), true);
                txt = strm.ReadToEnd();
            }
            finally
            {
                strm.Close();
            }

            MessageBox.Show(txt);

            return reg.IsMatch(txt);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var jump = new ClosedXML();
            jump.ShowDialog();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string message = "";
            List<string> list = new List<string>() { "one", "two", "Three" };
            Parallel.ForEach(list, (val) =>
            {
                message = message + "\r\n" + val;
                Thread.Sleep(100);

            });


            //Parallel.Invoke(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        message = message + "T";
            //        Thread.Sleep(1);
            //    }
            //},
            //() => 
            //{
            //    for(int ii=10; ii <= 20; ii++) 
            //    {
            //        message = message + "S";
            //    }
            //});
            MessageBox.Show(message);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var str1 = new profile();
                str1.id = 1;
                str1.name = "パンダ";
                str1.test();

            var str2 = new profile();
            str2.id = 2;
            str2.name = "うさぎ";
            str2.test();
            str1.test();

            System.Reflection.PropertyInfo propertyInfo = typeof(profile).GetProperty("name");
            FixedTextAttribute att = Attribute.GetCustomAttribute(propertyInfo, typeof(FixedTextAttribute)) as FixedTextAttribute;
            MessageBox.Show(att.biko);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var jump = new LifeGame();
            jump.ShowDialog();
        }
    }


    public struct profile
    {
        [FixedText(biko="通し番号です")]
        public int id { get; set; }

        [FixedText(biko ="名前を示す文字列です")]
        public string name { get; set; }

        public void test() 
        {
            MessageBox.Show(id.ToString() + "/" + name);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class FixedTextAttribute : System.Attribute 
    {
        public string biko { get; set; }
    }

}
