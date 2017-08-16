using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {

        CalcGLSTyp GLS = new CalcGLSTyp();   // GLS-Objekt istanzieren
        int Max;

        // *** Methoden Array-Tranfer
        public void ClearGrid()
        {
            int i;
            int j;

            try
            {
                for (i = 0; i <= Max; i++)
                {
                    for (j = 0; j <= Max; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = " ";
                    }
                    dataGridView1.Rows[i].Cells[7].Value = " ";
                }
            }
            catch
            {
            }

            dataGridView1.Rows.Clear();
            
        }

        public void ArrayToGrid(int Max)
        {
            int i;
            int j;

            for (i = 0; i <= Max; i++)
            {
                for (j = 0; j <= Max; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = GLS.GetXVal(i, j);
                }
                dataGridView1.Rows[i].Cells[7].Value = GLS.GetKVal(i);
            }
        }

        public void GridToArray(int Max)
        {
            int i;
            int j;

            try
            {
                for (i = 0; i <= Max; i++)
                {
                    for (j = 0; j <= Max; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != " ")
                        {
                            GLS.SetXVal(i, j, Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value));
                        }
                    }
                    if (dataGridView1.Rows[i].Cells[7].Value != " ")
                    {
                        GLS.SetKVal(i, Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value));
                    }
                }
            }
            catch
            {
            }
        }

        public void GridToFile(int Max)
        {
            int i;
            int j;
            string fName;
            
            fName="c:\\Temp\\GLS.xml";
            // Datei zum schreiben öffnen
            SaveFileDialog openFileDialog1 = new SaveFileDialog();
            openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                fName = openFileDialog1.FileName;
                        
            //MessageBox.Show("Datei: "+tmpFile);
            FileStream fs = new FileStream(fName,FileMode.Create,FileAccess.Write);
            TextWriter tw = new StreamWriter(fs);

            tw.WriteLine("<GLS>");
            for (i = 0; i <= Max; i++)
            {
                tw.WriteLine("<Line>");
                for (j = 0; j <= Max; j++)
                {
                    tw.WriteLine("<XVal>");
                    tw.WriteLine(dataGridView1.Rows[i].Cells[j].Value);
                    tw.WriteLine("</XVal>");
                }
                tw.WriteLine("<KVal>");
                tw.WriteLine(dataGridView1.Rows[i].Cells[7].Value);
                tw.WriteLine("</KVal>");
                tw.WriteLine("</Line>");
            }
            tw.WriteLine("</GLS>");
            tw.Close();         // schließen der Datei

            // Dateiname andrucken
            textBox1.Text = fName;
       
        }

        public int FileToGrid(string fName)
        {
            int i;
            int j;            
            string fLine;

            fLine = "";
            if (fName == "")
            {
                // Datei zum lesen öffnen
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    fName = openFileDialog1.FileName;
            }
            //MessageBox.Show("Datei: "+tmpFile);
            FileStream fs = new FileStream(fName, FileMode.Open, FileAccess.Read);
            TextReader tr = new StreamReader(fs);

            i = -1; j = -1;
            fLine = tr.ReadLine();    // Startzeile lesen
            while (fLine != null)
            {
                fLine = tr.ReadLine();  // Zeilenstart lesen

                if ((fLine == "<Line>") || (fLine == "<XVal>") || (fLine == "<KVal>"))
                {
                    if (fLine == "<Line>")
                    {
                        i = i + 1;      // Row zählen
                        if (i+2>dataGridView1.Rows.Count) {
                            dataGridView1.Rows.Add();
                        }
                        fLine = tr.ReadLine();   // Wertstart lesen
                    }
                    
                    if (fLine == "<XVal>")
                    {
                        j = j + 1; //Elemente zählen
                        fLine = tr.ReadLine();
                        dataGridView1.Rows[i].Cells[j].Value = fLine;
                    }
                    if (fLine == "<KVal>")
                    {
                        fLine = tr.ReadLine();
                        dataGridView1.Rows[i].Cells[7].Value = fLine;
                        fLine = tr.ReadLine();  // Zeilenende mit lesen
                        j = -1;                 // j zurücksetzen
                    }
                    fLine = tr.ReadLine();   // WertEnde lesen
                }
                            
            } // while
            tr.Close();  // schließen der Datei
              
            // Dateiname andrucken
            textBox1.Text = fName;
            
            return (i);         // Zeilenzahl zurückgeben
        }

        public void ResToList(int Max)
        {
            int i;

            listBox1.Items.Clear();
            for (i = 0; i <= Max; i++)
            {
                listBox1.Items.Add(Convert.ToString(GLS.GetRes(i)));
            }
           
        }

        // *** Ende Array-Transfer

        public void DemoToGird()
        {
            if (dataGridView1.Rows.Count<3) {
                dataGridView1.Rows.Add();
                dataGridView1.Rows.Add();
                dataGridView1.Rows.Add();
            }
            dataGridView1.Rows[0].Cells[0].Value = 1;
            dataGridView1.Rows[1].Cells[0].Value = 2;
            dataGridView1.Rows[2].Cells[0].Value = 4;

            dataGridView1.Rows[0].Cells[1].Value = 1;
            dataGridView1.Rows[1].Cells[1].Value = -3;
            dataGridView1.Rows[2].Cells[1].Value = -5;

            dataGridView1.Rows[0].Cells[2].Value = 1;
            dataGridView1.Rows[1].Cells[2].Value = -1;
            dataGridView1.Rows[2].Cells[2].Value = -1;

            dataGridView1.Rows[0].Cells[7].Value = 3;
            dataGridView1.Rows[1].Cells[7].Value = 2;
            dataGridView1.Rows[2].Cells[7].Value = 1;

        }

        public int ChkMax()
        {
            int i;
            int j;

            j = 0;
            for (i = 0; i <= dataGridView1.Rows.Count-1; i++)
            {
                if ((dataGridView1.Rows[i].Cells[0].Value != "") && (dataGridView1.Rows[i].Cells[0].Value != " "))
                {
                    j = j + 1;
                }
            }
            return(j-2);
        }
        
        public Form1()
        {
            InitializeComponent();

            // Demo-Werte setzen
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            // Initialisierung GLS-Objekt
            GLS.SetMax(2);
            GLS.ResetErg(2);
            GLS.InitArrays();
            GLS.SetDemo();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Max = ChkMax();
            GLS.SetMax(Max);
            // Berechne
                                                            
            GLS.ResetErg(Max);
            GridToArray(Max);
            GLS.CalcGls();
            ResToList(Max);
            if (checkBox1.Checked==true) {
                ArrayToGrid(Max);
            }
     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Demowerte setzen
            listBox1.Items.Clear();
            Max = 2;
            ClearGrid();
            GLS.InitArrays();           
            GLS.ResetErg(2);
            dataGridView1.AutoResizeColumn(2);
            DemoToGird();
            GridToArray(2);
            GLS.SetMax(2);
            GLS.CalcGls();
            ResToList(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            // Speichern des GLS
            Max = ChkMax();

            // Speichern in Datei
            GridToFile(Max);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            ClearGrid();

            // Gls von Datei laden
     
            GLS.InitArrays();
            listBox1.Items.Clear();

            Max=FileToGrid(""); // Einlesen
            GLS.SetMax(Max);
            GLS.ResetErg(Max);
            dataGridView1.AutoResizeColumn(Max);

            GridToArray(Max);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Lösche Grid
            textBox1.Text = "";
            ClearGrid();
            listBox1.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                FileToGrid(textBox1.Text);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Setze Tooltip
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.Info, "von Michael Graßmann");
        }
    }

    public class CalcGLSTyp
    {
        double[,] fkt = new double[,] { { 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0 },
                                    { 0, 0, 0, 0, 0, 0 } };		//Fkt.-Array
        double[] knt = new double[6] { 0, 0, 0, 0, 0, 0 };			//Konstanten-Array
        double[] erg = new double[6] { 0, 0, 0, 0, 0, 0 };			//Ergebnis-Array

        int j;					// Zeilen
        int k2;					// Spalten

        double result;

        public void InitArrays()
        {
            int i;
            int k;

            //fkt init
            for (k = 0; k <= 6; k++)
            {
                for (i = 0; i <= 6; i++)
                {
                    //fkt[k,i]=0;                      
                }
            }
            // knt init
            for (i = 0; i <= 6; i++)
            {
                //knt[i]=0;
            }
            //erg init
            for (i = 0; i <= 6; i++)
            {
                //erg[i]=0;
            }
        }

        public void SetDemo()
        {
            k2 = 2; j = 2;

            fkt[0, 0] = 1;
            fkt[1, 0] = 2;
            fkt[2, 0] = 4;

            fkt[0, 1] = 1;
            fkt[1, 1] = -3;
            fkt[2, 1] = -5;

            fkt[0, 2] = 1;
            fkt[1, 2] = -1;
            fkt[2, 2] = -1;

            knt[0] = 3;
            knt[1] = 2;
            knt[2] = 1;

        }

        // *** Getter-/Setter-Methoden
        public void SetMax(int Max)
        {
            j = Max;
            k2 = Max;
        }

        public double GetXVal(int x, int y)
        {
            return (fkt[x, y]);
        }

        public void SetXVal(int x, int y, double XVal)
        {
            fkt[x, y] = XVal;
        }

        public double GetKVal(int y)
        {
            return (knt[y]);
        }

        public void SetKVal(int y, double KVal)
        {
            knt[y] = KVal;
        }


        public double GetRes(int y)
        {
            return (erg[y]);
        }

        public void ResetErg(int Max)
        {
            int i;

            for (i = 0; i <= Max; i++)
            {
                erg[i] = 0;
            }
        }

        // *** Ende Getter/Setter-Methoden

        public void CGls(int s)
        {
            int z;
            double fakt;
            int id;
            z = s + 1;
            id = 0;
            for (z = s + 1; z <= j; z++)
            {
                if (fkt[z, s] != 0)
                {
                    fakt = (-1 * fkt[z, s]) / fkt[s, s];
                    for (id = 0; id <= k2; id++)
                    {
                        fkt[z, id] = fkt[z, id] + (fkt[s, id] * fakt);
                    }
                    knt[z] = knt[z] + (knt[s] * fakt);
                }
            }
        }

        public void CFkt()
        {
            int z;
            int id;
            double sum;


            for (z = j; z >= 0; z--)
            {
                sum = 0;
                for (id = 0; id <= k2; id++)
                {
                    sum = sum + (erg[id] * fkt[z, id]);
                }
                erg[z] = (knt[z] + sum * -1) / fkt[z, z];
                result = erg[z];
            }
        }

        public void CalcGls()
        {
            int i;

            for (i = 0; i <= k2; i++)
            {
                CGls(i);
            }
            CFkt();
        }

        public void ShowRes()
        {
            int i;

            Console.WriteLine("Ergebnisse:");
            for (i = 0; i <= k2; i++)
            {
                Console.WriteLine("X " + (i + 1) + " = {0:F2}", erg[i]);
            }
        }

    } //Ende Klasse CalcGLSTyp

}
