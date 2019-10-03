using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;
using xNet.Collections;
using xNet.Text;

namespace Parser_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns[0].Name = "Items1";
            dataGridView1.Columns[0].HeaderText = "Items1";
            dataGridView1.Columns[1].Name = "Items2";
            dataGridView1.Columns[1].HeaderText = "Items2";
            dataGridView1.Rows.Add(200);
            toolStripMenuItem1.BackColor = Color.Gray;

            dataGridView1.Visible = true;
            textBox2.Visible = false;
            textBox4.Visible = false;
            textBox1.Visible = false;
            textBox3.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            progressBar1.Visible = false;
            textBox6.Visible = false;
            label9.Visible = false;
            textBox7.Visible = false;
            button2.Visible = false;

        }
        string URL;
        string[] mass1;
        string[] mass2;
        string link;
        string SourcePage;

        public void Pars()
        {
            URL = textBox5.Text;
            using (var Request = new xNet.Net.HttpRequest())
            {
                try
                {
                    Clipboard.Clear();
                    Clipboard.SetText(Request.Get(URL).ToText());
                    SourcePage = Clipboard.GetText().ToString();
                }
                catch (Exception ex)
                { MessageBox.Show("Проблеми з зєднанням!!! \n" + ex.Message); }
            }

            if (textBox1.Text == "" || textBox3.Text == "")
            { }
            else{ mass1 = SourcePage.Substrings(textBox1.Text, textBox3.Text, 0); }
            if (textBox2.Text == "" || textBox4.Text == "")
            { }
            else { mass2 = SourcePage.Substrings(textBox1.Text, textBox3.Text, 0);}


            if (checkBox1.Checked)
            {
                StreamWriter file1 = new StreamWriter("Column1.txt");
                try
                {
                    for (int i = 0; i < mass1.Length; i++)
                    {
                        file1.WriteLine(mass1[i]);
                        dataGridView1.Rows[i].Cells[0].Value = mass1[i];
                    }
                    file1.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Парсер", MessageBoxButtons.OK); }
            }
            if (checkBox2.Checked)
            {
                StreamWriter file2 = new StreamWriter("Column2.txt");
                    try
                    {
                        for (int k = 0; k < mass2.Length; k++)
                        {
                            file2.WriteLine(mass2[k]);
                            dataGridView1.Rows[k].Cells[1].Value = mass2[k];
                        }
                        file2.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Парсер", MessageBoxButtons.OK); }
                }
            if (checkBox3.Checked)
            {
                using (WebClient webClient = new WebClient())
                {
                    for (int i = 0; i < mass1.Length; i++)
                    {
                        if (checkBox4.Checked)
                        { link = textBox7.Text + mass1[i]; }
                        try
                        {
                            if (folderBrowserDialog1.SelectedPath == null)
                            {
                                string name = folderBrowserDialog1.SelectedPath + @"/Files" + i + "." + textBox6.Text;
                                webClient.DownloadProgressChanged += DownloadProgressChanged;
                                webClient.DownloadFile(new System.Uri(link), name);
                            }
                            else
                            {
                                string path1 = Directory.GetCurrentDirectory();
                                DirectoryInfo di = Directory.CreateDirectory(path1 + @"\\DownloadFiles");
                                string name = di + @"/file" + i + "." + textBox6.Text;
                                webClient.DownloadProgressChanged += DownloadProgressChanged;
                                webClient.DownloadFile(new System.Uri(link), name);
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Pars();
            MessageBox.Show("Complete!", "Парсер", MessageBoxButtons.OK);
        }
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked == false)
            {
                textBox1.Visible = true;
                textBox3.Visible = true;
                label2.Visible = true;
                label4.Visible = true;
            }
            else if (!checkBox1.Checked == true)
            {
                textBox1.Visible = false;
                textBox3.Visible = false;
                label2.Visible = false;
                label4.Visible = false;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked == false)
            {
                textBox2.Visible = true;
                textBox4.Visible = true;
                label3.Visible = true;
                label5.Visible = true;
            }
            else if (!checkBox2.Checked == true)
            {
                textBox2.Visible = false;
                textBox4.Visible = false;
                label3.Visible = false;
                label5.Visible = false;
            }
        }
        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked == false)
            {
                textBox6.Visible = true;
                label9.Visible = true;
            }
            else if (!checkBox3.Checked == true)
            {
                textBox6.Visible = false;
                label9.Visible = false;
            }
        }
        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox4.Checked == false)
            { textBox7.Visible = true; }
            else if (!checkBox4.Checked == true)
            { textBox7.Visible = false; }
        }

        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) { }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            textBox1.Width = 100;
            textBox3.Width = 100;
            textBox2.Visible = false;
            textBox4.Visible = false;
            textBox1.Visible = false;
            textBox3.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            toolStripMenuItem1.BackColor = Color.Gray;
            toolStripMenuItem2.BackColor = Color.Snow;
            checkBox3.Visible = false;
            checkBox3.Location = new Point(543, 430);
            checkBox4.Location = new Point(430, 430);
            checkBox4.Visible = false;
            progressBar1.Visible = false;
            progressBar1.Location = new Point(12, 405);
            button2.Visible = false;
            Form1.ActiveForm.Height = 564;
            Form1.ActiveForm.MaximumSize = new Size(816, 564);
            Form1.ActiveForm.MinimumSize = new Size(816, 564);

        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            progressBar1.Location = new Point(12, 405);
            progressBar1.Width = 776;
            checkBox3.Location = new Point(543, 430);
            checkBox4.Location = new Point(430, 430);
            dataGridView1.Visible = false;
            textBox2.Visible = false;
            textBox4.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            toolStripMenuItem1.BackColor = Color.Snow;
            toolStripMenuItem2.BackColor = Color.Gray;
            checkBox3.Visible = true;
            checkBox4.Visible = true;
            progressBar1.Visible = true;
            button2.Visible = true;
            textBox1.Visible = true;
            textBox1.Width = 200;
            textBox3.Visible = true;
            textBox3.Width = 200;
            textBox6.Location = new Point(355, 455);
            label9.Location = new Point(300, 458);
            label2.Visible = true;
            label4.Visible = true;
            textBox7.Location = new Point(422, 457);

            Form1.ActiveForm.Height = 200;
            Form1.ActiveForm.MaximumSize = new Size(816, 200);
            Form1.ActiveForm.MinimumSize = new Size(816, 200);
        }
    }
}
