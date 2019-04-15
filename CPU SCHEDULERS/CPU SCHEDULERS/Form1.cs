using System;
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
        private int prev_size = 0;
        private decimal d;
        private int time;
        private int duration = 0;
        private int Quantum;
        private bool stop = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
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

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (button1.Text == "Generate")
            {
                size = Int32.Parse(textBox1.Text);
                int[] at = new int[size];
                int[] bt = new int[size];
                int[] st = new int[size + 1];
                int[] ps = new int[size];
                IList<int> stp = new List<int>();
                IList<int> psp = new List<int>();
                int[] prio = new int[size];
                process_no = 1;
                foreach (Control c in tableLayoutPanel2.Controls)
                {
                    if (c is TextBox)
                    {
                        TextBox txt = (TextBox)c;
                        if (String.IsNullOrEmpty(txt.Text))
                        {
                            MessageBox.Show("Please fill all the fields");
                            return;
                        }
                        else if (!decimal.TryParse(txt.Text, out d))
                        {
                            MessageBox.Show("Please enter numbers only");
                            return;
                        }
                        else
                        {
                            if (!int.TryParse(txt.Text, out time))
                            {
                                MessageBox.Show("Please enter integer value");
                                return;
                            }
                            else if (Int32.Parse(txt.Text) < 0)
                            {
                                MessageBox.Show("Please enter a positive value");
                                return;
                            }
                        }
                        ps[process_no - 1] = process_no;
                        if (tableLayoutPanel2.GetColumn(c) == 0)
                        {
                            at[process_no - 1] = Int32.Parse(txt.Text);
                        }
                        else if (tableLayoutPanel2.GetColumn(c) == 1)
                        {
                            if (Int32.Parse(txt.Text) == 0)
                            {
                                MessageBox.Show("Burst should be bigger than zero");
                                return;
                            }
                            bt[process_no - 1] = Int32.Parse(txt.Text);
                            process_no++;
                        }
                    }
                }
                if (scheduler == "FCFS")
                {
                    FCFS.averageTime(ps, size, bt, at, ref averageWT, ref averageTAT);
                    FCFS.GanttView(size, at, bt, ps, stp, psp);
                }
                else if (scheduler == "SJF")
                {
                    if (radioButton1.Checked)   //Preemptive
                        SJF.sjf_prmptive(stp, psp, size, at, bt, ps);
                    else if (radioButton2.Checked)  // Non Preemptive
                        SJF.sjf_nonprmptive(stp, psp, size, at, bt, ps);
                }
                else if (scheduler == "PRIORITY")
                {
                    for (int i = 1; i < tableLayoutPanel3.Controls.Count; i++)
                    {
                        if (String.IsNullOrEmpty(tableLayoutPanel3.Controls[i].Text))
                        {
                            MessageBox.Show("Please fill priority field");
                            return;
                        }
                        else if (!decimal.TryParse(tableLayoutPanel3.Controls[i].Text, out d))
                        {
                            MessageBox.Show("Please enter numbers only");
                            return;
                        }
                        else
                        {
                            if (!int.TryParse(tableLayoutPanel3.Controls[i].Text, out time))
                            {
                                MessageBox.Show("Please enter integer value");
                                return;
                            }
                            else if (Int32.Parse(tableLayoutPanel3.Controls[i].Text) < 0)
                            {
                                MessageBox.Show("Please enter a positive value");
                                return;
                            }
                        }
                        prio[i - 1] = Int32.Parse(tableLayoutPanel3.Controls[i].Text);
                    }
                    if (radioButton1.Checked)
                        PRIORITY.PrioritySort(true, size, at, bt, prio, ps, stp, psp);
                    else if (radioButton2.Checked)
                        PRIORITY.PrioritySort(false, size, at, bt, prio, ps, stp, psp);
                }
                else if (scheduler == "ROUND ROBIN")
                {
                    if (String.IsNullOrEmpty(textBox2.Text))
                    {
                        MessageBox.Show("Please enter time slice");
                        return;
                    }
                    else if (!decimal.TryParse(textBox2.Text, out d))
                    {
                        MessageBox.Show("Please enter time slice numbers only");
                        return;
                    }
                    else
                    {
                        if (!int.TryParse(textBox2.Text, out time))
                        {
                            MessageBox.Show("Please enter time slice integer value");
                            return;
                        }
                        else if (Int32.Parse(textBox2.Text) <= 0)
                        {
                            MessageBox.Show("Please enter time slice a positive non-zero value");
                            return;
                        }
                    }

                    Quantum = Int32.Parse(textBox2.Text);

                    ROUND_ROBIN.RobinSort(Quantum, size, at, bt, ps, stp, psp);
                }
                averageWT = TIME.avgWaiting(size, at, bt, stp, psp);
                averageTAT = TIME.avgTurnAround(size, at, stp, psp);
                button1.Text = "Clear";
                label7.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                string str;
                var result = MessageBox.Show("Run Animated Show?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result != DialogResult.Cancel)
                {
                    // user clicked yes
                    stop = false;
                    for (int i = 0; i <= psp.Count; i++)
                    {
                        str = stp[i].ToString();
                        var Label1 = new Label { BackColor = Color.White, Text = str, AutoSize = true };
                        tableLayoutPanel4.Controls.Add(Label1, i /* Column Index */, 1 /* Row index */);
                        if (i != psp.Count)
                        {
                            int pad = 10 * (stp[i + 1] - stp[i]);
                            var Label2 = new Label { BackColor = Color.Orange, AutoSize = true };
                            Label2.Font = new Font("Arial", 12, FontStyle.Bold);
                            if (psp[i] == 0)
                            {
                                Label2.Text = "IDLE";
                                tableLayoutPanel4.Controls.Add(Label2, i /* Column Index */, 0 /* Row index */);
                            }
                            else
                            {
                                Label2.Text = "Process " + psp[i];
                                tableLayoutPanel4.Controls.Add(Label2, i /* Column Index */, 0 /* Row index */);
                            }
                            if (result == DialogResult.Yes)
                            {
                                duration = 0;
                                while (duration <= pad)
                                {
                                    await Task.Delay(78);
                                    if (stop)
                                        return;
                                    duration++;
                                    Label2.Padding = new Padding(duration, 0, duration, 0);

                                }
                            }
                            else
                                Label2.Padding = new Padding(pad, 0, pad, 0);
                        }
                    }
                    label10.Text = averageWT.ToString();
                    label11.Text = averageTAT.ToString();
                }
                else
                    button1.Text = "Generate";
            }
            else if (button1.Text == "Clear")
            {
                stop = true;
                button1.Text = "Generate";
                label10.Text = "";
                label11.Text = "";
                tableLayoutPanel4.Controls.Clear();
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
                if (scheduler == "PRIORITY")
                    tableLayoutPanel3.Visible = true;
                else
                    tableLayoutPanel3.Visible = false;
            }
            else if (scheduler == "ROUND ROBIN")
            {
                label2.Visible = false;
                groupBox1.Visible = false;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                tableLayoutPanel1.RowStyles[3].Height = tableLayoutPanel1.RowStyles[2].Height;
                label6.Visible = true;
                textBox2.Visible = true;
                tableLayoutPanel3.Visible = false;
            }
            else
            {
                label2.Visible = false;
                groupBox1.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                tableLayoutPanel1.RowStyles[3].Height = 0;
                tableLayoutPanel3.Visible = false;
            }
            tableLayoutPanel2.Visible = true;
            tableLayoutPanel5.Visible = true;
            /*if (!String.IsNullOrEmpty(textBox1.Text))
            {
                if (scheduler == "PRIORITY")
                {
                    tableLayoutPanel3.Visible = true;
                    if (tableLayoutPanel2.Controls.Count > 2 && tableLayoutPanel2.Controls.Count != 2*tableLayoutPanel3.Controls.Count)
                    {
                        size = Int32.Parse(textBox1.Text);
                        for (int i = 1; i <= size; i++)
                        {
                            tableLayoutPanel3.Controls.Add(new TextBox() { TextAlign = HorizontalAlignment.Center }, 0 , i );
                        }
                    }
                }
                else
                    tableLayoutPanel3.Visible = false;
            }*/
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a scheduler");
            }
            else if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter no. of processes");
            }
            else
            {
                if (!decimal.TryParse(textBox1.Text, out d))
                {
                    MessageBox.Show("This is a number only field");
                    return;
                }
                else
                {
                    if (!int.TryParse(textBox1.Text, out size))
                    {
                        MessageBox.Show("Please enter integer value");
                        return;
                    }
                    else if (Int32.Parse(textBox1.Text) < 0)
                    {
                        MessageBox.Show("Please enter a positive value");
                        return;
                    }
                    else if (Int32.Parse(textBox1.Text) > 200)
                    {
                        if (MessageBox.Show("Progrman may be irresponsive :(", "Run", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    }
                }
                DialogResult result = DialogResult.No;
                size = Int32.Parse(textBox1.Text);
                if (size != 0)
                    button2.Enabled = false;
                if (!(prev_size == 0 || size == 0))
                {
                    int j,test;
                    if (size >= prev_size)
                        test = prev_size;
                    else
                        test = size;
                    for (j = 1; j <= test; j++)
                    {
                        if (!String.IsNullOrEmpty(tableLayoutPanel2.Controls[2*j].Text))
                            break;
                        else if (!String.IsNullOrEmpty(tableLayoutPanel2.Controls[2*j + 1].Text))
                            break;
                        else if (!String.IsNullOrEmpty(tableLayoutPanel3.Controls[j].Text))
                        {
                            if (scheduler == "PRIORITY")
                                break;
                            else
                                tableLayoutPanel3.Controls[j].Text = "";
                        }
                    }
                    if(j <= test)
                        result = MessageBox.Show("Clear previous values?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                if (size >= prev_size)
                {
                    if (result == DialogResult.Yes)
                    {
                        for (int j = 1; j <= prev_size; j++)
                        {
                            tableLayoutPanel2.Controls[2*j].Text = "";
                            tableLayoutPanel2.Controls[2*j + 1].Text = "";
                            tableLayoutPanel3.Controls[j].Text = "";
                        }
                    }
                    for (int i = tableLayoutPanel5.Controls.Count; i <= size; i++)
                    {
                        tableLayoutPanel2.Controls.Add(new TextBox() { TextAlign = HorizontalAlignment.Center }, 0 /* Column Index */, i /* Row index */);
                        tableLayoutPanel2.Controls.Add(new TextBox() { TextAlign = HorizontalAlignment.Center }, 1 /* Column Index */, i /* Row index */);
                        var Label = new Label { Text = "P" + i, Margin = new Padding(40, 6, 0, 7), AutoSize = true };
                        Label.Font = new Font(Label.Font.Name, 8, FontStyle.Bold);
                        tableLayoutPanel5.Controls.Add(Label, 0 /* Column Index */, i /* Row index */);
                        //if (scheduler == "PRIORITY")
                        tableLayoutPanel3.Controls.Add(new TextBox() { TextAlign = HorizontalAlignment.Center }, 0 /* Column Index */, i /* Row index */);
                    }
                }
                else if (size < prev_size)
                {
                    for (int i = tableLayoutPanel5.Controls.Count-1; i > size; i--)
                    {
                        tableLayoutPanel2.Controls[tableLayoutPanel2.Controls.Count - 1].Dispose();
                        tableLayoutPanel2.Controls[tableLayoutPanel2.Controls.Count - 1].Dispose();
                        tableLayoutPanel5.Controls[tableLayoutPanel5.Controls.Count - 1].Dispose();
                        tableLayoutPanel3.Controls[tableLayoutPanel3.Controls.Count - 1].Dispose();
                    }
                    if (result == DialogResult.Yes)
                    {
                        for (int j = tableLayoutPanel5.Controls.Count - 1; j > 0; j--)
                        {
                            tableLayoutPanel3.Controls[j].Text = "";
                            tableLayoutPanel2.Controls[2 * j + 1].Text = "";
                            tableLayoutPanel2.Controls[2*j].Text = "";
                        }
                    }
                }
                prev_size = size;
                if (size == 0)
                    MessageBox.Show("Enter bigger than zero processes to\nenable <Generate> button");
                else
                    button1.Enabled = true;
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
