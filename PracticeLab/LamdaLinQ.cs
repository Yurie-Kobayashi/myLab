using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeLab
{
    public partial class LamdaLinQ : Form
    {
        public LamdaLinQ()
        {
            InitializeComponent();
        }

        public delegate string GetMesseDeli(string messe);

        //引数が同じメソッドにしか利用できない？＝＞void型と相性が良い？

        private void button1_Click(object sender, EventArgs e)
        {
            string[] messes = new string[3];

            messes[0] = "焼肉食べたい";
            messes[1] = "家にこもっていたい";
            messes[2] = "え、これ録音してるの？";

            GetMesseDeli del = GetMesseText;

            ShowMesse(messes, del);

            GetMesseDeli Hisyo = HisyoMesseText;

            messes[0] = "焼肉は体脂肪のもとだ";
            messes[1] = "家にこもるならテレワーク設備を準備すべき";
            messes[2] = "録音は当然";

            ShowMesse(messes, Hisyo);
        }

        public string GetMesseText(string messetext)
        {
            return "社長は" + messetext + "とおっしゃいました";
        }

        public string HisyoMesseText(string messetext)
        {
            return "私は" + messetext + "と思いますが";
        }

        public void ShowMesse(string[] messes,GetMesseDeli deli) 
        {

            foreach (string m in messes)
            {
                MessageBox.Show(deli(m));
            }
        }
    }

}
