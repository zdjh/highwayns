﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace highwayns
{
    public partial class FormNjssDetail : Form
    {
        private string CompanyName;
        public FormNjssDetail(string CompanyName)
        {
            this.CompanyName = CompanyName;
            InitializeComponent();
        }
        Hashtable bid = new Hashtable();
        /// <summary>
        /// load from Csv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadCsv_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(dlg.FileName, Encoding.UTF8))
                {
                    dgvData.Rows.Clear();
                    bid.Clear();
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        line = line.Trim();
                        line += sr.ReadLine().Trim();
                        line += sr.ReadLine().Trim();
                        string[] temp = line.Split(',');
                        if (temp[0] == CompanyName)
                        {
                            string[] rows = new string[8];
                            rows[0] = dgvData.Rows.Count.ToString();
                            rows[1] = temp[0];//名称
                            rows[2] = temp[1];//プロジェクト
                            rows[3] = temp[2].Replace("都道府県", "");//区域                    
                            rows[4] = temp[3].Replace("入札形式", "");//入札形式
                            rows[5] = temp[4].Replace("公示日", "");//公示日
                            rows[6] = temp[5];//web
                            rows[7] = "";//other
                            dgvData.Rows.Add(rows);
                            bid[rows[0]] = rows;
                        }
                        line = sr.ReadLine();
                    }
                }
                MessageBox.Show("Load Csv Over!\r\n there are " + bid.Keys.Count.ToString() + " record!");
            }
        }
        /// <summary>
        /// SavetoCsv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavetoCsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(dlg.FileName, false, Encoding.UTF8))
                {
                    // add table list
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        string companyName = row.Cells[0].Value.ToString();
                        string[] data = (string[])bid[companyName];
                        sw.WriteLine(string.Join(",", data, 1, data.Length - 1));
                    }
                }
                MessageBox.Show("Save Csv Over!\r\n there are " + bid.Keys.Count.ToString() + " record!");
            }

        }
    }
}