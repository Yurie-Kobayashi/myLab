using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace PracticeLab
{
    public partial class parallel : Form
    {

        int _message_numver = 0;
        //int _message_numver2 = 0;

        public parallel(string triger = "")
        {
            #region マルチプロセス
            InitializeComponent();

            if (!string.IsNullOrEmpty(triger))
            {
                First(triger);
            }
            #endregion



        }


        private void parallel_Load(object sender, EventArgs e)
        {
            SetEvent();
        }

        private void SetEvent()
        {
            #region taskなしでの並列処理(マルチプロセス)
            button1.Click += (sender, e) =>
            {
                First();
                Second();
            };

            button2.Click += (sender, e) =>
            {
                int a = 0;
                int b = 0;
                System.Threading.ThreadPool.GetMaxThreads(out a, out b);
                MessageBox.Show(a.ToString() + "/" + b.ToString());
                System.Threading.ThreadPool.GetMinThreads(out a, out b);
                MessageBox.Show(a.ToString() + "/" + b.ToString());
            };

            #endregion

            button3.Click += (sender, e) =>
            {

                string testMessage = "これはダメなんじゃないかな？";

                //Task task = Task.Factory.StartNew(
                //()=> 
                //{
                //    for (int i = 0; i < 5; i++)
                //    {
                //        testMessage = "これは？";
                //    }
                //    MessageBox.Show(testMessage);
                //}
                //);

                //Task task2 = Task.Factory.StartNew (
                //() =>
                //{
                //    for (int i = 0; i < 5; i++)
                //    {
                //        testMessage = "？";
                //    }
                //    MessageBox.Show(testMessage);
                //}
                //);

            };
        }


        #region マルチプロセス
        private void First(string Iam="")
        {
            _message_numver++;
            if (string.IsNullOrEmpty(Iam))
            {
                MessageBox.Show("メッセージボックス1:" + _message_numver.ToString() + "回目のメッセージです");
            }
            else 
            {
                MessageBox.Show(Iam);
            }
        }

        private void Second()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            ProcessStartInfo info = new ProcessStartInfo(assembly.Location);
            info.Arguments = "私は呼び出されました。名前はセカンダリです";
            Process p = new Process();
            p.StartInfo = info;
            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(OnExit);
            p.SynchronizingObject = this;
            p.Start();

            //_message_numver2++;
            //MessageBox.Show("メッセージボックス2:" + _message_numver2.ToString() + "回目のメッセージです");
        }

        private void Therd() 
        {
            MessageBox.Show("作業中");
        }

        private void OnExit(object sender,EventArgs e) 
        {
            Process p = sender as Process;
            label1.Text = p.ExitCode.ToString();
        }
        #endregion
    }

}
