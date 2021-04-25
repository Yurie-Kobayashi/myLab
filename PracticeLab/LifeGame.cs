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

namespace PracticeLab
{
    public partial class LifeGame : Form
    {
        private System.Windows.Forms.Panel panel;
        private readonly int CELLS_COUNT;
        List<Panel> panels = new List<Panel>();
        DataTable cellsTable = new DataTable();
        List<int> xCellIndexes = new List<int>();
        List<int> yCellIndexes = new List<int>();

        public LifeGame()
        {
            InitializeComponent();
            CELLS_COUNT = 30;
            xCellIndexes = Enumerable.Range(1, CELLS_COUNT).ToList();
            yCellIndexes = xCellIndexes; ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cellsTable.Clear();
            int aliveCount = 0;
            if (!int.TryParse(textBox1.Text, out aliveCount))
            {
                MessageBox.Show("数値を入れてください");
                return;
            };

            yCellIndexes.ForEach(y => insertDefaultValues(cellsTable));
            UpdateCells(aliveCount, cellsTable);
            UpdateDisplayCells(cellsTable);
            for (int i = 0; i <= 10; i++)
            {
                UpdateDataTable(cellsTable);
                UpdateDisplayCells(cellsTable);
                MessageBox.Show("進行しました");
            }
        }

        private void CreatePanel(int x,int y) 
        {
            this.panel = new System.Windows.Forms.Panel();
            this.panel.Name = "cell" + x.ToString() + "_" + y.ToString();
            this.panel.Location = new Point(200+10 * x, 10 * y);
            this.panel.Size = new System.Drawing.Size(10, 10);
            this.panel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.panel);
            panels.Add(this.panel);
        }

        private void insertDefaultValues(DataTable cellsTable) 
        {
                var newRow = cellsTable.NewRow();
                Enumerable.Range(1, CELLS_COUNT).ToList().ForEach(x => newRow[x.ToString()] = "0");
                cellsTable.Rows.Add(newRow);
        }

        private void SetRandomCellValues(DataTable cellsTable)
        {
            int nowLife = 1;
            int[] targetCell = new int[2];
            while (nowLife == 1)
            {
                var random = new Random();
                    targetCell = new int[2] { random.Next(CELLS_COUNT - 1), random.Next(CELLS_COUNT - 1) };
                    nowLife = int.Parse(cellsTable.Rows[targetCell[0] + 1][targetCell[1] + 1].ToString());
            }

                cellsTable.Rows[targetCell[0] + 1][targetCell[1] + 1] = "1";
        }

        private void UpdateCells(int aliveCount, DataTable cellsTable)
        {
            int realAliveCount = aliveCount;
            if (aliveCount > CELLS_COUNT*CELLS_COUNT)
            {
                realAliveCount = CELLS_COUNT*CELLS_COUNT;
            }
            Enumerable.Range(1, realAliveCount).ToList().ForEach(x => SetRandomCellValues(cellsTable));
        }

        private void UpdateDataTable(DataTable cellsTable) 
        {
            int rowindex = 0;
            foreach (DataRow dr in cellsTable.Rows)
            {
                int columnIndex = 0;
                foreach (string value in dr.ItemArray)
                {
                    List<int> ysyuhen = new List<int>() { rowindex-1,rowindex,rowindex+1};
                    List<int> xsyuhen = new List<int>() { columnIndex - 1, columnIndex, columnIndex + 1 };
                    if (rowindex == 0) 
                    {
                        ysyuhen = new List<int>() { rowindex, rowindex + 1 };
                        if (rowindex == CELLS_COUNT-1)
                        {
                            ysyuhen = new List<int>() { rowindex, rowindex };
                        }
                    }
                    if (columnIndex == 0) 
                    {
                        xsyuhen = new List<int>() { columnIndex, columnIndex + 1 };
                        if (columnIndex == CELLS_COUNT-1)
                        {
                            xsyuhen = new List<int>() { columnIndex - 1, columnIndex };
                        }
                    }

                    if (rowindex == CELLS_COUNT-1)
                    {
                        ysyuhen = new List<int>() { rowindex - 1, rowindex };
                    }
                    if (columnIndex == CELLS_COUNT-1)
                    {
                        xsyuhen = new List<int>() { columnIndex - 1, columnIndex };
                    }

                    List<DataRow> syuhenRows = ysyuhen.Select(x => cellsTable.Rows[x])
                                                .ToList();
                    List<string> syuhenValues = new List<string>();
                    xsyuhen.ForEach(x => syuhenRows.Select(y => y[(x+1).ToString()].ToString()).ToList().ForEach(z=>syuhenValues.Add(z)));
                    if (value == "0") 
                    {
                        if(syuhenValues.Where(x => x == "1").ToList().Count == 3) 
                        {
                            cellsTable.Rows[rowindex][columnIndex] = "1";
                        }
                    }
                    else 
                    {
                        if (syuhenValues.Where(x => x == "1").ToList().Count != 3)
                        {
                            cellsTable.Rows[rowindex][columnIndex] = "0";
                        }
                    }
                    columnIndex++;
                }
                rowindex++;
            }
        }

        private void UpdateDisplayCells(DataTable cellsTable) 
        {
            int rowindex = 0;
            foreach(DataRow dr in cellsTable.Rows) 
            {
                int columnIndex = 0;
                foreach (string value in dr.ItemArray)
                {
                    List<Panel> targetPanels = panels.Where(x => x.Name == "cell" + (columnIndex+1).ToString() + "_" + (rowindex+1).ToString()).ToList();
                    if (targetPanels.Count > 0)
                    {
                        Panel target = targetPanels.First();
                        target.BackColor = value == "1" ? Color.Blue : Color.Red;
                    }
                    columnIndex++;
                }
                rowindex++;
            }
        }

        private void LifeGame_Load(object sender, EventArgs e)
        {
            xCellIndexes.ForEach(x => yCellIndexes.ForEach(y => CreatePanel(x, y)));
            xCellIndexes.ForEach(x => cellsTable.Columns.Add(x.ToString()));
        }
    }
}
