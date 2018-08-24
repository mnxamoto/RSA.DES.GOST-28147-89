using System;
using System.Text;
using System.Windows.Forms;

public class ConvertTextIn2
{
    public byte[] kod;
    public string[] blokiIn2;

    public DataGridView GetDataGridView(string text, DataGridView format)
    {
        kod = Encoding.Default.GetBytes(text);
        blokiIn2 = new string[kod.Length];

        format.Rows.Clear();
        format.Rows.Add(kod.Length);

        for (int i = 0; i < kod.Length; i++)
        {
            blokiIn2[i] = Convert.ToString(kod[i], 2);

            while (blokiIn2[i].Length < 8)
            {
                blokiIn2[i] = "0" + blokiIn2[i];
            }

            format.Rows[i].Cells[0].Value = text[i];
            format.Rows[i].Cells[1].Value = kod[i];
            format.Rows[i].Cells[2].Value = blokiIn2[i];
        }

        return format;
    }
}