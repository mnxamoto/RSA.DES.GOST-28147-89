using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Resources;


namespace курсач_КМиСОИБИ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string alfavit = "";

        //GOST 28147-89
        string[,] sBloki = new string[16, 8];

        //RSA
        int fi_RSA, n_RSA, d_RSA;

        //DES
        string[,,] S = new string[8, 16, 4];

        private void button1_Click(object sender, EventArgs e)
        {
            //DES

            ConvertTextIn2 convertTextIn2 = new ConvertTextIn2();

            dataGridView1 = convertTextIn2.GetDataGridView(textBox1.Text, dataGridView1);
            string[] textbloki = convertTextIn2.blokiIn2;

            dataGridView2 = convertTextIn2.GetDataGridView(textBox2.Text, dataGridView2);
            string[] keybloki = convertTextIn2.blokiIn2;

            int[] perestav1 = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };

            dataGridView8.Rows.Clear();
            dataGridView9.Rows.Clear();
            dataGridView10.Rows.Clear();

            dataGridView8.Rows.Add(4);
            dataGridView9.Rows.Add(9);
            dataGridView10.Rows.Add(4);

            dataGridView8.Rows[0].Cells[0].Value = "номера битов";
            dataGridView8.Rows[1].Cells[0].Value = "биты";
            dataGridView8.Rows[2].Cells[0].Value = "перестановка";
            dataGridView8.Rows[3].Cells[0].Value = "результат";

            for (int i = 0; i < 64; i++)
            {
                dataGridView8.Rows[0].Cells[i + 1].Value = i + 1;
                dataGridView8.Rows[1].Cells[i + 1].Value = textbloki[i / 8].Substring(i % 8, 1);
                dataGridView8.Rows[2].Cells[i + 1].Value = perestav1[i];
            }

            for (int i = 0; i < 64; i++)
            {
                dataGridView8.Rows[3].Cells[i + 1].Value = dataGridView8.Rows[1].Cells[Convert.ToInt32(dataGridView8.Rows[2].Cells[i + 1].Value)].Value;
            }

            dataGridView9.Rows[0].Cells[0].Value = "L";
            dataGridView9.Rows[1].Cells[0].Value = "R";

            for (int i = 0; i < 32; i++)
            {
                dataGridView9.Rows[0].Cells[(i / 4) + 1].Value += dataGridView8.Rows[3].Cells[i + 1].Value.ToString();
                dataGridView9.Rows[1].Cells[(i / 4) + 1].Value += dataGridView8.Rows[3].Cells[i + 1 + 32].Value.ToString();
            }

            string key = keybloki[0] + keybloki[1] + keybloki[2] + keybloki[3] + keybloki[4] + keybloki[5];
            //string key = "100101011001010101001100100111100101100100111111";

            dataGridView9.Rows[2].Cells[0].Value = "расширение блоков";
            dataGridView9.Rows[3].Cells[0].Value = "K";
            dataGridView9.Rows[4].Cells[0].Value = "F(R, K).";

            for (int i = 0; i < 8; i++)
            {
                if (i == 0)
                {
                    dataGridView9.Rows[2].Cells[i + 1].Value = dataGridView9.Rows[1].Cells[8].Value.ToString().Substring(3, 1) + dataGridView9.Rows[1].Cells[1].Value + dataGridView9.Rows[1].Cells[2].Value.ToString().Substring(0, 1);
                }
                else
                {
                    dataGridView9.Rows[2].Cells[i + 1].Value = dataGridView9.Rows[1].Cells[i].Value.ToString().Substring(3, 1) + dataGridView9.Rows[1].Cells[i + 1].Value + dataGridView9.Rows[1].Cells[((i + 1) % 8) + 1].Value.ToString().Substring(0, 1);
                }

                dataGridView9.Rows[3].Cells[i + 1].Value = key.Substring(i * 6, 6);

                for (int k = 0; k < 6; k++)
                {
                    int R = Convert.ToInt32(dataGridView9.Rows[2].Cells[i + 1].Value.ToString().Substring(k, 1));
                    int K = Convert.ToInt32(dataGridView9.Rows[3].Cells[i + 1].Value.ToString().Substring(k, 1));
                    dataGridView9.Rows[4].Cells[i + 1].Value += ((R + K) % 2).ToString();
                }
            }

            dataGridView9.Rows[5].Cells[0].Value = "N строки";
            dataGridView9.Rows[6].Cells[0].Value = "N столбца";
            dataGridView9.Rows[7].Cells[0].Value = "значение";
            dataGridView9.Rows[8].Cells[0].Value = "результат";

            for (int i = 0; i < 8; i++)
            {
                dataGridView9.Rows[5].Cells[i + 1].Value = Convert.ToInt32(dataGridView9.Rows[4].Cells[i + 1].Value.ToString().Substring(0, 1) + dataGridView9.Rows[4].Cells[i + 1].Value.ToString().Substring(5, 1), 2);
                dataGridView9.Rows[6].Cells[i + 1].Value = Convert.ToInt32(dataGridView9.Rows[4].Cells[i + 1].Value.ToString().Substring(1, 4), 2);
                dataGridView9.Rows[7].Cells[i + 1].Value = S[i, Convert.ToInt32(dataGridView9.Rows[6].Cells[i + 1].Value), Convert.ToInt32(dataGridView9.Rows[5].Cells[i + 1].Value)];
                dataGridView9.Rows[8].Cells[i + 1].Value = Convert.ToString(Convert.ToInt32(dataGridView9.Rows[7].Cells[i + 1].Value), 2);

                while (dataGridView9.Rows[8].Cells[i + 1].Value.ToString().Length < 4)
                {
                    dataGridView9.Rows[8].Cells[i + 1].Value = "0" + dataGridView9.Rows[8].Cells[i + 1].Value;
                }
            }

            int[] perestav2 = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25 };

            dataGridView10.Rows[0].Cells[0].Value = "номера битов";
            dataGridView10.Rows[1].Cells[0].Value = "биты";
            dataGridView10.Rows[2].Cells[0].Value = "перестановка";
            dataGridView10.Rows[3].Cells[0].Value = "результат";

            for (int i = 0; i < 64; i++)
            {
                dataGridView10.Rows[0].Cells[i + 1].Value = i + 1;
                dataGridView10.Rows[2].Cells[i + 1].Value = perestav2[i];
            }

            for (int i = 0; i < 32; i++)
            {
                dataGridView10.Rows[1].Cells[i + 1].Value = dataGridView9.Rows[8].Cells[(i / 4) + 1].Value.ToString().Substring(i % 4, 1);
                dataGridView10.Rows[1].Cells[i + 1 + 32].Value = dataGridView9.Rows[0].Cells[(i / 4) + 1].Value.ToString().Substring(i % 4, 1);
            }

            for (int i = 0; i < 64; i++)
            {
                dataGridView10.Rows[3].Cells[i + 1].Value = dataGridView10.Rows[1].Cells[Convert.ToInt32(dataGridView10.Rows[2].Cells[i + 1].Value)].Value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (char i = 'А'; i <= 'я'; i++)
            {
                alfavit += i.ToString();
            }

            for (char i = 'A'; i <= 'z'; i++)
            {
                alfavit += i.ToString();
            }

            alfavit += " ";
            //RSA
            dataGridView3.Rows.Add(5);
            dataGridView3.Rows[0].Cells[0].Value = "p";
            dataGridView3.Rows[1].Cells[0].Value = "q";
            dataGridView3.Rows[2].Cells[0].Value = "M[ ]";
            dataGridView3.Rows[3].Cells[0].Value = "H[0]";
            dataGridView3.Rows[4].Cells[0].Value = "M[ ]";

            dataGridView3.Rows[0].Cells[1].Value = 17;
            dataGridView3.Rows[1].Cells[1].Value = 19;
            dataGridView3.Rows[2].Cells[1].Value = "ПМВ";
            dataGridView3.Rows[3].Cells[1].Value = 7;
            dataGridView3.Rows[4].Cells[1].Value = "Полковников";

            //Блок подстановки в алгоритме шифрования ГОСТ 28147-89
            dataGridView7.Rows.Add(17);

            string matrix = Properties.Resources.S_блоки.Replace("\r\n", "\t");

            for (int i = 0; i < 16; i++)
            {
                dataGridView7.Rows[i + 1].Cells[0].Value = i;

                for (int k = 0; k < 8; k++)
                {
                    dataGridView7.Rows[0].Cells[k + 1].Value = k + 1;

                    sBloki[i, k] = Convert.ToInt32(matrix.Substring((i * 24) + k * 3, 2)).ToString();
                    dataGridView7.Rows[i + 1].Cells[k + 1].Value = sBloki[i, k];
                }
            }
            //Преобразования S в алгоритме шифрования DES
            dataGridView11 = ConvertTXTinTabl(Properties.Resources.Преобразования_S1.Replace("\r\n", "\t"), dataGridView11, 0);
            dataGridView12 = ConvertTXTinTabl(Properties.Resources.Преобразования_S2.Replace("\r\n", "\t"), dataGridView12, 1);
            dataGridView13 = ConvertTXTinTabl(Properties.Resources.Преобразования_S3.Replace("\r\n", "\t"), dataGridView13, 2);
            dataGridView14 = ConvertTXTinTabl(Properties.Resources.Преобразования_S4.Replace("\r\n", "\t"), dataGridView14, 3);
            dataGridView15 = ConvertTXTinTabl(Properties.Resources.Преобразования_S5.Replace("\r\n", "\t"), dataGridView15, 4);
            dataGridView16 = ConvertTXTinTabl(Properties.Resources.Преобразования_S6.Replace("\r\n", "\t"), dataGridView16, 5);
            dataGridView17 = ConvertTXTinTabl(Properties.Resources.Преобразования_S7.Replace("\r\n", "\t"), dataGridView17, 6);
            dataGridView18 = ConvertTXTinTabl(Properties.Resources.Преобразования_S8.Replace("\r\n", "\t"), dataGridView18, 7);
        }

        public DataGridView ConvertTXTinTabl(string matrix, DataGridView format, int index_S)
        {
            format.Rows.Add(5);
            format.Rows[0].Cells[0].Value = "S" + (index_S + 1);

            for (int i = 0; i < 4; i++)
            {
                format.Rows[i + 1].Cells[0].Value = i;

                for (int k = 0; k < 16; k++)
                {
                    format.Rows[0].Cells[k + 1].Value = k;

                    S[index_S, k, i] = Convert.ToInt32(matrix.Substring((i * 48) + k * 3, 2)).ToString();
                    format.Rows[i + 1].Cells[k + 1].Value = S[index_S, k, i];
                }
            }

            return format;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int p_RSA = Convert.ToInt32(dataGridView3.Rows[0].Cells[1].Value);
                int q_RSA = Convert.ToInt32(dataGridView3.Rows[1].Cells[1].Value);

                fi_RSA = (p_RSA - 1) * (q_RSA - 1);
                n_RSA = p_RSA * q_RSA;

                textBox3.Text = "f(n) = (p - 1) * (q - 1) = " + (p_RSA - 1) + " * " + (q_RSA - 1) + " = " + fi_RSA + "\r\n";
                textBox3.Text += "n = p * q = " + p_RSA + " * " + q_RSA + " = " + n_RSA + "\r\n";

                listBox1.Items.Clear();
                bool check;
                for (int i = 2; i < fi_RSA; i++)
                {
                    check = true;
                    for (int k = 2; k <= i; k++)
                    {
                        if (((fi_RSA % k) == 0) && ((i % k) == 0))
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        listBox1.Items.Add(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                d_RSA = Convert.ToInt32(listBox1.SelectedItem);
                textBox1.Text += "d = " + d_RSA + "\r\n";
                listBox2.Items.Clear();
                for (int i = 1; i < fi_RSA; i++)
                {
                    listBox2.Items.Add((double)((fi_RSA * i) + 1) / (double)d_RSA);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                int e_RSA, S;
                int[] C_RSA, M, M2_RSA, H;
                string text;
                //RSA
                e_RSA = Convert.ToInt32(listBox2.SelectedItem);
                textBox3.Text += "e = " + e_RSA + "\r\n";
                text = Convert.ToString(dataGridView3.Rows[2].Cells[1].Value);
                textBox3.Text += "\r\n" + text + " = (";
                M = new int[text.Length];
                for (int i = 0; i < M.Length; i++)
                {
                    M[i] = alfavit.IndexOf(text.Substring(i, 1)) + 2;
                    textBox3.Text += M[i] + ", ";
                }
                textBox3.Text = textBox3.Text.Substring(0, (textBox3.Text.Length - 2)) + ")\r\n\r\n";

                C_RSA = new int[text.Length];
                for (int i = 0; i < C_RSA.Length; i++)
                {
                    C_RSA[i] = mod(M[i], e_RSA, n_RSA);
                    textBox3.Text += "C[" + i + "] = (" + M[i] + "^" + e_RSA + ") mod " + n_RSA + " = " + C_RSA[i] + "\r\n";
                }

                textBox3.Text += "\r\n";

                M2_RSA = new int[text.Length];
                for (int i = 0; i < M2_RSA.Length; i++)
                {
                    M2_RSA[i] = mod(C_RSA[i], d_RSA, n_RSA);
                    textBox3.Text += "M2[" + i + "] = (" + C_RSA[i] + "^" + d_RSA + ") mod " + n_RSA + " = " + M2_RSA[i] + "\r\n";
                }
                //hash
                text = Convert.ToString(dataGridView3.Rows[4].Cells[1].Value);
                textBox3.Text += "\r\n" + text + " = (";
                M = new int[text.Length];

                for (int i = 0; i < M.Length; i++)
                {
                    M[i] = alfavit.IndexOf(text.Substring(i, 1)) + 2;
                    textBox3.Text += M[i] + ", ";
                }

                textBox3.Text = textBox3.Text.Substring(0, (textBox3.Text.Length - 2)) + ")\r\n\r\n";

                H = new int[text.Length + 1];
                H[0] = Convert.ToInt32(dataGridView3.Rows[3].Cells[1].Value);

                for (int i = 1; i < H.Length; i++)
                {
                    H[i] = mod(H[i - 1] + M[i - 1], 2, n_RSA);
                    textBox3.Text += "H[" + i + "] = ((" + H[i - 1] + " + " + M[i - 1] + ")^2) mod " + n_RSA + " = " + H[i] + "\r\n";
                }
                //подпись
                S = mod(H[H.Length - 1], d_RSA, n_RSA);
                textBox3.Text += "\r\nS = (" + H[H.Length - 1] + "^" + d_RSA + ") mod " + n_RSA + " = " + S + "\r\n";
                textBox3.Text += "H = (" + S + "^" + e_RSA + ") mod " + n_RSA + " = " + mod(S, e_RSA, n_RSA) + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //GOST 28147-89
            ConvertTextIn2 convertTextIn2 = new ConvertTextIn2();

            dataGridView5 = convertTextIn2.GetDataGridView(textBox4.Text, dataGridView5);
            string[] textbloki = convertTextIn2.blokiIn2;

            dataGridView4 = convertTextIn2.GetDataGridView(textBox5.Text, dataGridView4);
            string[] keybloki = convertTextIn2.blokiIn2;

            dataGridView6.Rows.Clear();
            dataGridView6.Rows.Add(10);
            dataGridView6.Rows[0].Cells[0].Value = "L0";
            dataGridView6.Rows[1].Cells[0].Value = "R0";
            dataGridView6.Rows[2].Cells[0].Value = "X0";

            for (int i = 0; i < textbloki.Length / 2; i++)
            {
                dataGridView6.Rows[0].Cells[i * 2 + 1].Value = textbloki[i].Substring(0, 4);
                dataGridView6.Rows[0].Cells[i * 2 + 2].Value = textbloki[i].Substring(4, 4);
                dataGridView6.Rows[1].Cells[i * 2 + 1].Value = textbloki[i + (textbloki.Length / 2)].Substring(0, 4);
                dataGridView6.Rows[1].Cells[i * 2 + 2].Value = textbloki[i + (textbloki.Length / 2)].Substring(4, 4);
                dataGridView6.Rows[2].Cells[i * 2 + 1].Value = keybloki[i].Substring(0, 4);
                dataGridView6.Rows[2].Cells[i * 2 + 2].Value = keybloki[i].Substring(4, 4);
            }

            long R0 = Convert.ToInt64(textbloki[4] + textbloki[5] + textbloki[6] + textbloki[7], 2);
            long X0 = Convert.ToInt64(keybloki[0] + keybloki[1] + keybloki[2] + keybloki[3], 2);
            string R0plusX0 = Convert.ToString((R0 + X0) % Convert.ToInt64(Math.Pow(2, 32)), 2);

            dataGridView6.Rows[3].Cells[0].Value = "(R0+X0) mod (2^32)";
            dataGridView6.Rows[4].Cells[0].Value = "номера блоков";
            dataGridView6.Rows[5].Cells[0].Value = "значения блоков";
            dataGridView6.Rows[6].Cells[0].Value = "заполнение блоков";
            dataGridView6.Rows[7].Cells[0].Value = "результат";

            string f = "";

            for (int i = 0; i < textbloki.Length; i++)
            {
                dataGridView6.Rows[3].Cells[i + 1].Value = R0plusX0.Substring(i * 4, 4);
                dataGridView6.Rows[4].Cells[i + 1].Value = 8 - i;
                dataGridView6.Rows[5].Cells[i + 1].Value = Convert.ToInt32(dataGridView6.Rows[3].Cells[i + 1].Value.ToString(), 2);
                dataGridView6.Rows[6].Cells[i + 1].Value = sBloki[Convert.ToInt32(dataGridView6.Rows[5].Cells[i + 1].Value), Convert.ToInt32(dataGridView6.Rows[4].Cells[i + 1].Value) - 1];
                dataGridView6.Rows[7].Cells[i + 1].Value = Convert.ToString(Convert.ToInt32(dataGridView6.Rows[6].Cells[i + 1].Value), 2);

                while (dataGridView6.Rows[7].Cells[i + 1].Value.ToString().Length < 4)
                {
                    dataGridView6.Rows[7].Cells[i + 1].Value = "0" + dataGridView6.Rows[7].Cells[i + 1].Value;
                }

                f += dataGridView6.Rows[7].Cells[i + 1].Value;
            }

            f = f.Substring(11) + f.Substring(0, 11);

            dataGridView6.Rows[8].Cells[0].Value = "f(R0,X0)";
            dataGridView6.Rows[9].Cells[0].Value = "(f(R0,X0) + L0) mod 2";

            for (int i = 0; i < textbloki.Length; i++)
            {
                dataGridView6.Rows[8].Cells[i + 1].Value = f.Substring(i * 4, 4);

                for (int k = 0; k < 4; k++)
                {
                    int L0 = Convert.ToInt32(dataGridView6.Rows[0].Cells[i + 1].Value.ToString().Substring(k, 1));
                    int f2 = Convert.ToInt32(dataGridView6.Rows[8].Cells[i + 1].Value.ToString().Substring(k, 1));
                    dataGridView6.Rows[9].Cells[i + 1].Value += ((L0 + f2) % 2).ToString();
                }
            }
        }

        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1)
            {
                string itemS = (sender as ListBox).Items[e.Index].ToString();
                Brush textBrush = Brushes.Black;
                if ((Convert.ToDouble(itemS) % 1) == 0)
                {
                    e.Graphics.DrawRectangle(Pens.Red, e.Bounds);
                }

                e.Graphics.DrawString(itemS, e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
            }
        }

        private int mod(int delimoe, int stepen, int delitel)
        {
            int[] arr = new int[Int16.MaxValue];
            arr[0] = (delimoe % delitel);
            for (int k = 1; ; k++)
            {
                arr[k] = (delimoe * arr[k - 1]) % delitel;
                if (arr[k] == arr[0])
                {
                    return arr[(stepen % k) - 1];
                }
            }
        }
    }
}
