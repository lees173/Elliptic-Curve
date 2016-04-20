using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ZedGraph;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            GraphPane Zd_Pen = zedGraphControl1.GraphPane;

            Zd_Pen.Title.Text = "Elliptic Curves";
            Zd_Pen.XAxis.Title.Text = "X";
            Zd_Pen.XAxis.Scale.MinorStep = 0.5F;
            Zd_Pen.XAxis.Scale.MajorStep = 0.5f;
            Zd_Pen.XAxis.Scale.Min = -5.0F;
            Zd_Pen.XAxis.Scale.Max = 5.0F;

            Zd_Pen.YAxis.Title.Text = "Y";
            Zd_Pen.YAxis.Scale.MinorStep = 0.5F;
            Zd_Pen.YAxis.Scale.MajorStep = 0.5f;
            Zd_Pen.YAxis.Scale.Min = -5.0f;
            Zd_Pen.YAxis.Scale.Max = 5.0f;


        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
        GraphPane Zd_Pen;
        GraphPane Zd_Pen1;
        private void button1_Click(object sender, EventArgs e)
        {
            double x, y;
            double a = double.Parse(textBoxA.Text);
            double b = double.Parse(textBoxB.Text);
            
            Zd_Pen = zedGraphControl1.GraphPane;
            Zd_Pen.CurveList.Clear();
            Zd_Pen.GraphObjList.Clear();
            PointPairList list1 = new PointPairList();
            for (double i = -300; i < 300; i = i + 0.01)
            {
                x = (double)i;
                y = Math.Pow(Math.Pow(x, 3) + a * x + b, 0.5);
                list1.Add(x, y);

            }
            for (double i = -300; i < 300; i = i + 0.01)
            {
                x = (double)i;
                y = Math.Pow(Math.Pow(x, 3) + a * x + b, 0.5);

                list1.Add(x, -y);
            }




            LineItem Curve1 = Zd_Pen.AddCurve("Elliptic Curves", list1, Color.Red, SymbolType.None);
            Curve1.Line.Width = 1.0F;

            //draw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();

            zedGraphControl1.MasterPane.GetImage().Save(textBoxSave.Text+".png", System.Drawing.Imaging.ImageFormat.Png);



        }

        private void textBoxA_TextChanged(object sender, EventArgs e)
        {

        }
        
        private bool zedGraphControl1_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e)
        {

          
            
            object nearestObject;
            int index;
            this.zedGraphControl1.GraphPane.FindNearestObject(new PointF(e.X, e.Y), this.CreateGraphics(), out nearestObject, out index);
            if (nearestObject != null && nearestObject.GetType() == typeof(LineItem))
            {
                LineItem lineItem = (LineItem)nearestObject;

                if(textBoxX1.Text.Equals("")){
                    textBoxX1.Text = Convert.ToString(lineItem[index].X);
                    textBoxY1.Text = Convert.ToString(lineItem[index].Y);
                }
                else if (textBoxX2.Text.Equals(""))
                {
                    textBoxX2.Text = Convert.ToString(lineItem[index].X);
                    textBoxY2.Text = Convert.ToString(lineItem[index].Y);
                }
                


                zedGraphControl1.Invalidate();
            }
            
            return default(bool);
        }

        private void ResetButton_click(object sender, EventArgs e)
        {


            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxY1.Text = "";
            textBoxY2.Text = "";
        }

        private void GetRButton_Click(object sender, EventArgs e)
        {
            double x, y,x3,y3;
            double x1 = Convert.ToDouble(textBoxX1.Text);
            double y1 = Convert.ToDouble(textBoxY1.Text);
            double x2 = Convert.ToDouble(textBoxX2.Text);
            double y2 = Convert.ToDouble(textBoxY2.Text);
            string R = "";
            double m = (y2 - y1) / (x2 - x1);
            x3 = 0;
            y3 = 0;


            Zd_Pen1 = zedGraphControl1.GraphPane;
           
            PointPairList list1 = new PointPairList();
            for (double i = -300; i < 300; i = i + 0.01)
            {
                x = (double)i;
                y = m*(x-x1)+y1;
                list1.Add(x, y);

            }


            LineItem Curve1 = Zd_Pen1.AddCurve("Line", list1, Color.Blue, SymbolType.None);
            Curve1.Line.Width = 1.0F;

            //draw
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();
            if (x1 == x2 && y1 == y2)
            {
                x3 = Math.Pow(m, 2) - x1*2;
                y3 = m * (2 * x1 - Math.Pow(m, 2) - x2) - y1;

            }
            else if (x1 == x2 && -y1 == y2)
            {
                R = "infinity O";
            }
            else
            {

                x3 = Math.Pow(m, 2) - x1 - x2;
                y3 = m * (x1 - x3) - y1;
            }

            if (R.Equals(""))
            {

                textBoxRX.Text = Convert.ToString(x3);
                textBoxRY.Text=Convert.ToString(y3);
            }
            else
            {
                textBoxRX.Text = R;
            }

            


            

        }

    
    }
}
