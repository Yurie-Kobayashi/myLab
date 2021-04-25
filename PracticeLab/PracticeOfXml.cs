using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PracticeLab
{

    public partial class PracticeOfXml : Form
    {

        DataTable _gridDT = new DataTable();

        public PracticeOfXml()
        {
            InitializeComponent();

            _gridDT.Columns.Add("type");
            _gridDT.Columns.Add("name");
        
        }


        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xdoc1 = XDocument.Load("C:\\Users\\Yurie\\Desktop\\プログラミングアプリ\\FilesOfPracticeLab\\XmlPra.xml");
            XElement xele1 = xdoc1.Element("practice");
            IEnumerable<XElement> Big1 = xele1.Elements("big");

            string heroname = "";
            string viranname = "";

            foreach(XElement oneele in Big1) 
            {
                XElement hero = oneele.Element("hero");
                IEnumerable<XElement> heros = hero.Elements("small");
                foreach(XElement onehero in heros)
                {
                    heroname = heroname + " / " + onehero.Value;
                }

                XElement viran = oneele.Element("viran");
                IEnumerable<XElement> virans = viran.Elements("small");
                foreach(XElement oneviran in virans) 
                {
                    viranname = viranname + " / " + oneviran.Value;
                }
                break;
            }

            MessageBox.Show("ヒーロー：" + heroname + "\r\n ヴィラン：" + viranname);

        }

        private void button6_Click(object sender, EventArgs e)
        {

            XDocument xdoc1 = XDocument.Load("C:\\Users\\Yurie\\Desktop\\プログラミングアプリ\\FilesOfPracticeLab\\XmlPra.xml");
            XElement xele1 = xdoc1.Element("practice");
            IEnumerable<XElement> Big1 = xele1.Elements("big");

            XElement nutoral = new XElement("nutoral",
                new XElement("small", "ウィンチー"));

            xele1.Add(nutoral);

            xdoc1.Save(@"C:\\Users\\Yurie\\Desktop\\プログラミングアプリ\\FilesOfPracticeLab\\XmlPra.xml");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XDocument xdoc1 = XDocument.Load("C:\\Users\\Yurie\\Desktop\\プログラミングアプリ\\FilesOfPracticeLab\\XmlPra.xml");
            XElement xele1 = xdoc1.Element("practice");
            IEnumerable<XElement> Big1 = xele1.Elements("big");


            foreach (XElement oneBig in Big1)
            {
                if (checkBox1.Checked)
                {
                    XElement hero = oneBig.Element("hero");
                    XElement inputedhero = new XElement("small", textBox1.Text);

                    hero.Add(inputedhero);
                }

                if (checkBox2.Checked)
                {
                    XElement viran = oneBig.Element("viran");
                    XElement inputedviran = new XElement("small", textBox1.Text);

                    viran.Add(inputedviran);
                }

                xdoc1.Save("C:\\Users\\Yurie\\Desktop\\プログラミングアプリ\\FilesOfPracticeLab\\XmlPra.xml");

                break;
            }
        }

        private void PracticeOfXml_Load(object sender, EventArgs e)
        {

            XDocument xdoc1 = XDocument.Load("C:\\Users\\Yurie\\Desktop\\プログラミングアプリ\\FilesOfPracticeLab\\XmlPra.xml");
            XElement xele1 = xdoc1.Element("practice");
            IEnumerable<XElement> Big1 = xele1.Elements("big");

            foreach(XElement oneBig in Big1) 
            {
                    XElement hero = oneBig.Element("hero");
                    IEnumerable<XElement> smalls = hero.Elements("small");

                    foreach(XElement onesmall in smalls) 
                    {
                        DataRow heroRow = _gridDT.NewRow();
                        heroRow["type"] = "ヒーロー";
                        heroRow["name"] = onesmall.Value;
                        _gridDT.Rows.Add(heroRow);
                    }

                   XElement viran = oneBig.Element("viran");
                   IEnumerable<XElement> smalls2 = viran.Elements("small");

                    foreach(XElement small in smalls2) 
                    {
                      DataRow viranRow = _gridDT.NewRow();
                      viranRow["type"] = "敵";
                      viranRow["name"] = small.Value;
                      _gridDT.Rows.Add(viranRow);
                    }

                dataGridView1.DataSource = _gridDT;

                break;
            }

        }
    }



    ///学習メモ
    ///XDocument:XMLファイルを格納できるインスタンス。ファイル一個全体
    ///XElement:XMLの要素のひとつを格納できるインスタンス。XMLファイル内の、<hoge></hoge>ひとくくりぶん。大きさは問われない
    /// IEnumerable<>　<>内の要素を配列のように格納できる。複数の要素が含まれる場合、ここに格納して配列として処理を行う
}
