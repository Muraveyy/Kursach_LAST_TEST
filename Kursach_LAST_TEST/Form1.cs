using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Kursach_LAST_TEST
{
    public partial class Form1 : Form
    {


        int p, f;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Press PLAY to play!!!!! ";


            Timer timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000;
            timer1.Start();


        }
        //Button[,] bt2 = new Button[31, 31];


        int[,] sss = new int[33, 33];


        Button[,] bt = new Button[32, 32];
        TextBox[,] tb = new TextBox[32, 32];

        //int[,] set2 = new int[33,33];

        private void button1_Click(object sender, EventArgs e)//создание минного поля
        {
            int wid = (int)numericUpDown1.Value;
            int hei = (int)numericUpDown2.Value;

            int x = -FIELD.Width / wid, y = 0;
            button1.Dispose();

            textBox2.Text = "Enjoy Your Game";


            //создаем массив содержащий местоположения мин
            Random rnd = new Random();

            int[,] set = new int[wid + 2, hei + 2];
            int rand;

            for (int i = 1; i < wid + 1; i++)
            {
                for (int j = 1; j < hei + 1; j++)
                {
                    rand = rnd.Next(0, 100);
                    if (rand < (int)numericUpDown3.Value)
                        set[i, j] = 9;
                    textBox1.Text = textBox1.Text + set[i, j].ToString();
                }
            }

            //изменяем массив для описания мест бомб
            for (int i = 1; i < wid + 1; i++)//Номер столбца
            {
                for (int j = 1; j < hei + 1; j++)//номер строки
                {


                    if ((i != 0 || j != 0) && (set[i, j] != 9) && (set[i - 1, j - 1] == 9))//лево верх
                        set[i, j]++;
                    if ((i != 0) && (set[i, j] != 9) && (set[i - 1, j] == 9))//лево
                        set[i, j]++;
                    if ((i != 0 || j != hei) && (set[i, j] != 9) && (set[i - 1, j + 1] == 9))//лево низ
                        set[i, j]++;

                    if ((j != 0) && (set[i, j] != 9) && (set[i, j - 1] == 9))//верх
                        set[i, j]++;
                    if ((j != wid) && (set[i, j] != 9) && (set[i, j + 1] == 9))//низ
                        set[i, j]++;

                    if ((i != wid || j != 0) && (set[i, j] != 9) && (set[i + 1, j - 1] == 9))//право верх
                        set[i, j]++;
                    if ((i != wid) && (set[i, j] != 9) && (set[i + 1, j] == 9))//право
                        set[i, j]++;
                    if ((i != wid || j != wid) && (set[i, j] != 9) && (set[i + 1, j + 1] == 9))//право низ
                        set[i, j]++;
                }
            }


            int k = 0;

            for (int i = 1; i < wid + 1; i++)
            {
                for (int j = 1; j < hei + 1; j++)
                {
                    //Создаем новую кнопку
                    Button temp = new Button();
                    TextBox te = new TextBox();


                    if (set[i, j] != 0)
                        te.Text = set[i, j].ToString();// для текста
                    if (set[i, j] == 9)
                    {
                        te.BackColor = Color.Black;
                        te.ForeColor = Color.OrangeRed;
                    }

                    temp.Width = FIELD.Width / wid;
                    temp.Height = FIELD.Height / (hei + 1);
                    //  temp.FlatStyle = FlatStyle.Flat;//изменение режима отображения на плоский

                    te.Multiline = true;

                    te.Width = FIELD.Width / wid;
                    te.Height = FIELD.Height / (hei + 1);

                    te.Enabled = false;
                    //Размещаем ее правее(на x) (и ниже на y) кнопки, на которую мы нажали
                    //te.Location = new Point(b.Location.X + b.Width + x, b.Location.Y + y);
                    te.Location = new Point(FIELD.Location.X + FIELD.Width / wid + x, FIELD.Location.Y + FIELD.Height / (hei + 1) + y);
                    temp.Location = new Point(FIELD.Location.X + FIELD.Width / wid + x, FIELD.Location.Y + FIELD.Height / (hei + 1) + y);

                    //Добавляем событие нажатия на новую кнопку 
                    bt[i, j] = temp;
                    tb[i, j] = te;
                    p = i; f = j;

                    if (set[i, j] == 0)
                    {
                        bt[i, j].Click += new EventHandler(free_Click);
                        bt[i, j].MouseUp += new MouseEventHandler(free_MouseUp);
                    }

                    if ((set[i, j] != 0) && (set[i, j] != 9))
                    {
                        bt[i, j].Click += new EventHandler(button2_Click);
                        bt[i, j].MouseUp += new MouseEventHandler(free_MouseUp);
                    }

                    if (set[i, j] == 9)
                    {

                        bt[i, j].Click += new EventHandler(BOMB_NOT_Click);
                        bt[i, j].MouseUp += new MouseEventHandler(free_MouseUp);

                    }

                    //Добавляем элемент на форму

                    this.Controls.Add(temp);
                    this.Controls.Add(te);
                    k++;
                    y += FIELD.Height / (hei + 1);
                }

                x += FIELD.Width / wid;
                y = 0;


            }

            //  button1.Dispose();


            for (int i = 1; i < wid + 1; i++)
                for (int j = 1; j < hei + 1; j++)
                    sss[i, j] = set[i, j];

            // numericUpDown1.Dispose();
            numericUpDown1.Hide();
            // numericUpDown2.Dispose();
            numericUpDown2.Hide();

            //numericUpDown3.Dispose();
            numericUpDown3.Hide();

            label1.Dispose();
            label2.Dispose();
            label3.Dispose();


            refresh.Visible = true;
        }

        private void Mouse_RightClick(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            temp.ForeColor = Color.Violet;
            temp.FlatStyle = FlatStyle.Flat;
            temp.BackColor = Color.Gold;
        }

        private void BOMBB(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BOMB_NOT_Click(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            if (temp.BackColor != Color.Gold)
            {
                temp.ForeColor = Color.White;
                temp.FlatStyle = FlatStyle.Flat;
                temp.BackColor = Color.Empty;

                for (int i = 1; i < 31; i++)
                    for (int j = 1; j < 31; j++)
                        if (sss[i, j] == 9)
                        {

                            bt[i, j].Visible = false;
                            bt[i, j].Enabled = false;

                        }





                textBox2.Text = "YOU GOT KILLED.";
                Button b = (Button)sender;
                //Создаем новую кнопку
                Button temp2 = new Button();
                temp2.Width = 50;
                temp2.Height = 25;
                temp2.Text = "Exit";
                temp2.ForeColor = Color.Gold;
                temp2.FlatStyle = FlatStyle.Flat;
                temp2.BackColor = Color.Black;
                temp2.Location = new Point(0, 0);


                this.Controls.Add(temp2);
                temp2.Click += new EventHandler(BOMBB);

            }
            else if (temp.BackColor == Color.Gold)
            {
                temp.ForeColor = Color.Black;
                temp.FlatStyle = FlatStyle.Standard;
                temp.BackColor = Color.Empty;
            }
        }

        private void free_Click(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            if (temp.BackColor != Color.Gold)
            {
                for (int i = 1; i < 31; i++)
                {
                    for (int j = 1; j < 31; j++)
                    {
                        if (bt[i, j] == temp)
                        {
                            temp.Visible = false;
                            temp.Enabled = false;
                            // temp.Dispose();

                            if (bt[i, j + 1] != null && bt[i, j + 1].BackColor != Color.Gold)
                                bt[i, j + 1].PerformClick();

                            if (bt[i + 1, j] != null && bt[i + 1, j].BackColor != Color.Gold)
                                bt[i + 1, j].PerformClick();

                            if (bt[i - 1, j] != null && bt[i - 1, j].BackColor != Color.Gold)
                                bt[i - 1, j].PerformClick();

                            if (bt[i, j - 1] != null && bt[i, j - 1].BackColor != Color.Gold)
                                bt[i, j - 1].PerformClick();


                            if (bt[i + 1, j + 1] != null && bt[i + 1, j + 1].BackColor != Color.Gold)
                                bt[i + 1, j + 1].PerformClick();

                            if (bt[i + 1, j - 1] != null && bt[i + 1, j - 1].BackColor != Color.Gold)
                                bt[i + 1, j - 1].PerformClick();

                            if (bt[i - 1, j + 1] != null && bt[i - 1, j + 1].BackColor != Color.Gold)
                                bt[i - 1, j + 1].PerformClick();

                            if (bt[i - 1, j - 1] != null && bt[i - 1, j - 1].BackColor != Color.Gold)
                                bt[i - 1, j - 1].PerformClick();
                        }
                    }
                }
            }
            else if (temp.BackColor == Color.Gold)
            {
                temp.ForeColor = Color.Black;
                temp.FlatStyle = FlatStyle.Standard;
                temp.BackColor = Color.Empty;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime ThToday = DateTime.Now;
            string ThData = ThToday.ToString();
            this.textBox1.Text = ThData;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            if (temp.BackColor != Color.Gold)
            {
                // temp.Dispose();
                temp.Visible = false;
                temp.Enabled = false;
            }
            else if (temp.BackColor == Color.Gold)
            {
                temp.ForeColor = Color.Black;
                temp.FlatStyle = FlatStyle.Standard;
                temp.BackColor = Color.Empty;
            }
        }

        private void free_MouseUp(object sender, MouseEventArgs e)
        {
            Button temp = (Button)sender;
            if (e.Button == MouseButtons.Right)
            {
                temp.ForeColor = Color.Violet;
                temp.FlatStyle = FlatStyle.Flat;
                temp.BackColor = Color.Gold;
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            int wid = (int)numericUpDown1.Value;
            int hei = (int)numericUpDown2.Value;

            int x = -FIELD.Width / wid, y = 0;
            textBox2.Text = "Wait a little bit, please.";
            FIELD.Visible = true;


            for (int i = 1; i < wid + 1; i++)
            {
                for (int j = 1; j < hei + 1; j++)
                {


                    //    if(bt[i,j].Enabled==false)
                    //  bt[i, j].Visible = false;

                    // tb[i, j].Visible = false;

                    bt[i, j].Width = FIELD.Width / wid;
                    bt[i, j].Height = FIELD.Height / (hei + 1);

                    tb[i, j].Width = FIELD.Width / wid;
                    tb[i, j].Height = FIELD.Height / (hei + 1);

                    tb[i, j].Location = new Point(FIELD.Location.X + FIELD.Width / wid + x, FIELD.Location.Y + FIELD.Height / (hei + 1) + y);

                    bt[i, j].Location = new Point(FIELD.Location.X + FIELD.Width / wid + x, FIELD.Location.Y + FIELD.Height / (hei + 1) + y);

                    y += FIELD.Height / (hei + 1);


                }

                x += FIELD.Width / wid;
                y = 0;


            }


            FIELD.Visible = false;

            textBox2.Text = "Enjoy Your Game";





        }


        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.Text = "Changed";
        }

     













    }
}

