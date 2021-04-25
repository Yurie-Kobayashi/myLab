using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace PracticeLab
{
    public partial class ClosedXML : Form
    {
        XLWorkbook yozitu = new XLWorkbook();
        IXLWorksheet yozituMain;
        IXLWorksheet syukei;
        XLWorkbook genpon = new XLWorkbook();
        IXLWorksheet genponSheet;

        public ClosedXML()
        {
            InitializeComponent();
        }

        
        private void ClosedXML_Load(object sender, EventArgs e)
        {
            yozitu = new XLWorkbook("予実.xlsx");
            yozituMain = yozitu.Worksheet("津１");
            syukei = yozitu.Worksheet("集計シート");
            ramda();
        }

        private void ramda() 
        {

            textBox4.Leave += (_sender, _e) => 
            {

                if (textBox4.Text != "")
                {
                    //予実報告書の指定セルの数式を取得
                    textBox1.Text = yozituMain.Cell(textBox4.Text).FormulaA1.ToString();
                    var splitEq = textBox1.Text.Split('!');
                    var syukeiPath = new List<string>();

                    foreach (string s in splitEq)
                    {
                        syukeiPath.Add(s);
                    }
                    syukeiPath.RemoveAt(0);


                    //集計シートのセル番地を抽出して、数式を取得
                    foreach (string Path in syukeiPath) 
                    {
                        int kanmaIndex = Path.IndexOfAny(new Char[] { ',', '/','+'});
                        if (kanmaIndex > 0) 
                        {
                            textBox2.Text = syukei.Cell(Path.Substring(0, kanmaIndex)).FormulaA1.ToString() + "\r\n";
                        }
                    }

                    var SyukeiCells = textBox2.Text.Split('\r');
                    int i = 1;
                    foreach (string OneCell in SyukeiCells)
                    {
                        int kansuIndex = OneCell.LastIndexOf('(');
                        textBox3.Text = textBox3.Text+OneCell.Substring(0, kansuIndex)+"\r\n";

                        switch (textBox2.Text.Substring(0, kansuIndex))
                        {
                            case "SUM":
                                var Cells = OneCell.Split('(', ',', ')');
                                foreach(string o in Cells) 
                                {
                                    textBox3.Text = textBox3.Text + o + "\r\n";
                                }

                                break;
                        }
                        i++;
                        if (i == SyukeiCells.Count()) 
                        {
                            break;
                        }
                    }
                }
            
            };

            textBox6.Leave += (_sender, _e) => 
            {
                if (textBox6.Text != "") 
                {

                    string syukeiFormula = syukei.Cell(textBox6.Text).FormulaA1.ToString();
                    int anotherBook = syukeiFormula.IndexOf('[');
                    if (anotherBook > 0) 
                    {
                        var BookList = new List<string>();
                        var Splited = syukeiFormula.Split('[');
                        foreach(string Sp in Splited) 
                        {
                            int ind = Sp.IndexOf("]");
                            if (ind > 0)
                            {
                                BookList.Add(Sp.Substring(0, ind));
                                

                                foreach (string bookName in BookList)
                                {
                                    MessageBox.Show(bookName);
                                    genpon = new XLWorkbook(bookName);
                                    textBox5.Text = textBox5.Text + bookName + "\r\n";
                                    break;
                                }
                            }
                        }
                    }

                }
            };
        
        }
    }
}
