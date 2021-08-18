using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using System.Windows.Forms;

namespace PracticeLab
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string excelFilePath = textBox1.Text;
            if (string.IsNullOrEmpty(textBox1.Text)||!File.Exists(excelFilePath))
            {
                MessageBox.Show("Excelファイルが見つかりませんでした");
                return;
            }

            if(string.IsNullOrEmpty(textBox2.Text)) 
            {
                MessageBox.Show("指定のセル番地を入力してください");
                return;
            }

            XLWorkbook targetBook = new XLWorkbook(textBox1.Text);
            IXLWorksheet targetSheet = targetBook.Worksheet(1);
            if (string.IsNullOrEmpty(targetSheet.Range(textBox2.Text).FirstCell().GetString())) 
            {
                MessageBox.Show("空白");
            }
           // targetBook.SaveAs(textBox1.Text);
        }
    }
}
