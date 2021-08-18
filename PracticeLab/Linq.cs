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
    public partial class Linq : Form
    {
        public Linq()
        {
            InitializeComponent();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Linq_Load(object sender, EventArgs e)
        {

            DataTable animalNameTable = new DataTable();
            animalNameTable.Columns.Add("id");
            animalNameTable.Columns.Add("name");
            List<string> animalId = new List<string>() { "A", "B", "C" };
            List<string> animalNames = new List<string>() { "猫", "犬", "ゾウ" };
            for (int i = 0; i < 3; i++) 
            {
                DataRow dr = animalNameTable.NewRow();
                dr["id"] = animalId[i];
                dr["name"] = animalNames[i];
                animalNameTable.Rows.Add(dr);
            }

            DataTable animaltypeTable = new DataTable();
            animaltypeTable.Columns.Add("id");
            animaltypeTable.Columns.Add("type");
            List<string> animalTypes = new List<string>() { "かわいい", "忠実", "大きい" };
            for (int i = 0; i < 3; i++)
            {
                DataRow dr = animaltypeTable.NewRow();
                dr["id"] = animalId[i];
                dr["type"] = animalTypes[i];
                animaltypeTable.Rows.Add(dr);
            }

            DataTable aniNames2 = animalNameTable.Copy();
            DataRow row = animalNameTable.NewRow();
            row["id"] = "D";
            row["name"] ="人間";
            animalNameTable.Rows.Add(row);
            List<string> an = animalNameTable.AsEnumerable().Select(x => x["id"].ToString() + x["name"].ToString()).ToList();
            List<string> bn = aniNames2.AsEnumerable().Select(x => x["id"].ToString() + x["name"].ToString()).ToList();
            //var a = an.Intersect(bn);
            //var b = an.Except(bn);

            var a = animalNameTable.AsEnumerable()
                                   .Where(x=>!aniNames2.AsEnumerable().Any(y=>(x["id"].ToString() + x["name"].ToString())==(y["id"].ToString() + y["name"].ToString())))
                                   .ToList();


            var b = animalNameTable.AsEnumerable()
                                   .Select(x => x["id"].ToString() + x["name"].ToString())
                                   .Except(aniNames2.AsEnumerable().Select(x => x["id"].ToString() + x["name"].ToString())
                                   .ToList());

            MessageBox.Show(a.Count().ToString());
            MessageBox.Show(a.First()["name"].ToString());

            var merged = from table1 in animalNameTable.AsEnumerable()
                         join table2 in animaltypeTable.AsEnumerable()
                         on table1["id"].ToString() equals table2["id"].ToString().ToString()
                         select new
                         {
                             id= table1["id"].ToString(),
                             name= table1["name"].ToString(),
                             type=table2["type"].ToString()
                         };

            DataTable answer = new DataTable();
            answer.Columns.Add("id");
            answer.Columns.Add("name");
            answer.Columns.Add("type");

            List<List<string>> rowsDatas = animalNameTable.AsEnumerable()
                                 .Join(animaltypeTable.AsEnumerable(),
                                 table1=>table1["id"].ToString(),
                                 table2=>table2["id"].ToString(),
                                 (row1,row2)=>
                                  new List<string>() { row1["id"].ToString(), row1["name"].ToString(), row2["type"].ToString() }
                                 ).ToList();

            rowsDatas.ForEach(x => AddDataJoinedRow(answer, x));

            var nameOnlyes = animalNameTable.AsEnumerable()
                       .Where(x => !animaltypeTable.AsEnumerable().Any(y => x["id"].ToString() == y["id"].ToString()))
                       .ToList();

            foreach(DataRow roww in nameOnlyes) 
            {
                var addrow = answer.NewRow();
                addrow["id"] = roww["id"].ToString();
                addrow["name"] = roww["name"].ToString();
                addrow["type"] = "";
                answer.Rows.Add(addrow);
            }

            dataGridView1.DataSource = answer;
        } 


        private void AddDataJoinedRow(DataTable joined ,List<string> oneRowData) 
        {
            DataRow dr = joined.NewRow();
            dr["id"] = oneRowData[0];
            dr["name"] = oneRowData[1];
            dr["type"] = oneRowData[2];
            joined.Rows.Add(dr);
        }
    }
}
