﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPU_SCHEDULERS
{
    public partial class Form1 : Form
    {
        private int process_no=1;
        private float averageTAT;
        private float averageWT;
        private string scheduler;
        private int size;
        private int Quantum;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
            groupBox1.Visible = false;
            label6.Visible = false;
            textBox2.Visible = false;
            label7.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            tableLayoutPanel1.RowStyles[1].Height = 0;
            tableLayoutPanel1.RowStyles[3].Height = 0;
            tableLayoutPanel2.Visible = false;
            tableLayoutPanel3.Visible = false;
            tableLayoutPanel5.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (button1.Text == "Generate")
            {
                button1.Text = "Clear";
                label7.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                size = Int32.Parse(textBox1.Text);
                int[] at = new int[size];
                int[] bt = new int[size];
                int[] st = new int[size + 1];
                int[] ps = new int[size];
                int[] prio = new int[size];
                foreach (Control c in tableLayoutPanel2.Controls)
                {
                    if (c is TextBox)
                    {
                        ps[process_no - 1] = process_no;
                        if (tableLayoutPanel2.GetColumn(c) == 0)
                        {
                            TextBox txt = (TextBox)c;
                            at[process_no - 1] = Int32.Parse(txt.Text);
                        }
                        else if (tableLayoutPanel2.GetColumn(c) == 1)
                        {
                            TextBox txt = (TextBox)c;
                            bt[process_no - 1] = Int32.Parse(txt.Text);
                            process_no++;
                        }
                    }
                }
                if (scheduler == "FCFS")
                {
                    FCFS.averageTime(size, bt, at, ref averageWT, ref averageTAT);
                    label10.Text = averageWT.ToString();
                    label11.Text = averageTAT.ToString();
                    FCFS.GanttView(size, at, bt, st);
                }
                else if (scheduler == "SJF")
                {
                    // hna ya Sisy
                    if (radioButton1.Checked)   //Preemptive
                    {
                        // SJF.preemptiveSort(size, at, bt, st, ps);
                    }
                    else if (radioButton2.Checked)  // Non Preemptive
                    {
                        // SJF.nonPreemptiveSort(size, at, bt, st, ps);
                    }
                    /* averageWT = SJF.avgWaiting(size, at, st);
                    averageTAT = SJF.avgTurnAround(size, at, st); */
                }
                else if (scheduler == "PRIORITY")
                {
                    for (int i = 1; i < tableLayoutPanel3.Controls.Count; i++)
                    {
                        prio[i - 1] = Int32.Parse(tableLayoutPanel3.Controls[i].Text);
                    }
                    if (radioButton1.Checked)
                    {

                    }
                    else if (radioButton2.Checked)
                    {
                        PRIORITY.prioritySort(size, at, bt, prio, st, ps);
                        averageWT = PRIORITY.avgWaiting(size, at, st);
                        averageTAT = PRIORITY.avgTurnAround(size, at, st);
                    }
                }
                else if (scheduler == "ROUND ROBIN")
                {
                    // hna ya Martina
                    Quantum = Int32.Parse(textBox2.Text);

                    /* ROUND_ROBIN.roundSort(size, at, bt, st, ps);
                    averageWT = ROUND_ROBIN.avgWaiting(size, at, st);
                    averageTAT = ROUND_ROBIN.avgTurnAround(size, at, st); */
                }
                label10.Text = averageWT.ToString();
                label11.Text = averageTAT.ToString();
                for (int i = 0; i <= size; i++)
                {
                    string str;
                    str = st[i].ToString();
                    var Label1 = new Label { BackColor = Color.White, Text = str, AutoSize = true };
                    tableLayoutPanel4.Controls.Add(Label1, i /* Column Index */, 1 /* Row index */);
                    if (i != size)
                    {
                        var Label2 = new Label { BackColor = Color.Orange, Text = "Process " + ps[i] };
                        tableLayoutPanel4.Controls.Add(Label2, i /* Column Index */, 0 /* Row index */);
                    }
                }
            }
            else if (button1.Text == "Clear")
            {
                button1.Text = "Generate";
                process_no = 1;
                //label7.Visible = false;
                //label9.Visible = false;
                label10.Text = "";
                label11.Text = "";
                //label10.Visible = false;
                //label11.Visible = false;
                while (tableLayoutPanel4.Controls.Count > 0)
                {
                    tableLayoutPanel4.Controls[0].Dispose();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            scheduler = comboBox1.SelectedItem.ToString();
            if (scheduler == "SJF" || scheduler == "PRIORITY")
            {
                tableLayoutPanel1.RowStyles[1].Height = tableLayoutPanel1.RowStyles[2].Height;
                label2.Visible = true;
                groupBox1.Visible = true;
                label6.Visible = false;
                textBox2.Visible = false;
                tableLayoutPanel1.RowStyles[3].Height = 0;
            }
            else if (scheduler == "ROUND ROBIN")
            {
                label2.Visible = false;
                groupBox1.Visible = false;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                tableLayoutPanel1.RowStyles[3].Height = tableLayoutPanel1.RowStyles[2].Height;
                label6.Visible = true;
                textBox2.Visible = true;
            }
            else
            {
                label2.Visible = false;
                groupBox1.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                tableLayoutPanel1.RowStyles[3].Height = 0;
            }
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                tableLayoutPanel2.Visible = true;
                tableLayoutPanel5.Visible = true;
                if (scheduler == "PRIORITY")
                {
                    tableLayoutPanel3.Visible = true;
                    if (tableLayoutPanel2.Controls.Count > 2)
                    {
                        size = Int32.Parse(textBox1.Text);
                        for (int i = 1; i <= size; i++)
                        {
                            tableLayoutPanel3.Controls.Add(new TextBox() { TextAlign = HorizontalAlignment.Center }, 0 /* Column Index */, i /* Row index */);
                        }
                    }
                }
                else
                    tableLayoutPanel3.Visible = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a scheduler");
            }
            else
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    while (tableLayoutPanel2.Controls.Count > 2)
                    {
                        tableLayoutPanel2.Controls[tableLayoutPanel2.Controls.Count-1].Dispose();
                    }
                    while (tableLayoutPanel5.Controls.Count > 1)
                    {
                        tableLayoutPanel5.Controls[tableLayoutPanel5.Controls.Count-1].Dispose();
                    }
                    if (scheduler == "PRIORITY")
                    {
                        while (tableLayoutPanel3.Controls.Count > 1)
                        {
                            tableLayoutPanel3.Controls[tableLayoutPanel3.Controls.Count-1].Dispose();
                        }
                    }
                }
                else
                {
                    tableLayoutPanel5.Visible = true;
                    tableLayoutPanel2.Visible = true;
                    if (scheduler == "PRIORITY")
                        tableLayoutPanel3.Visible = true;
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter no. of processes");
            }
            else
            {
                if (!int.TryParse(textBox1.Text, out size))
                {
                    MessageBox.Show("This is a number only field");
                    return;
                }
                if (tableLayoutPanel2.Controls.Count == 2)
                {
                    size = Int32.Parse(textBox1.Text);
                    for (int i = 1; i <= size; i++)
                    {
                        tableLayoutPanel2.Controls.Add(new TextBox() { TextAlign = HorizontalAlignment.Center }, 0 /* Column Index */, i /* Row index */);
                        tableLayoutPanel2.Controls.Add(new TextBox() { TextAlign = HorizontalAlignment.Center }, 1 /* Column Index */, i /* Row index */);
                        var Label = new Label { Text = "P" + i, Margin = new Padding(40, 6, 0, 7), AutoSize = true };
                        Label.Font = new Font(Label.Font.Name, 8, FontStyle.Bold);
                        tableLayoutPanel5.Controls.Add(Label, 0 /* Column Index */, i /* Row index */);
                        if (scheduler == "PRIORITY")
                            tableLayoutPanel3.Controls.Add(new TextBox() { TextAlign = HorizontalAlignment.Center }, 0 /* Column Index */, i /* Row index */);
                    }
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}