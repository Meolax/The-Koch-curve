using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Koch
{
    public partial class mainForm : Form
    {
        public mainForm ()
        {
            InitializeComponent();
        }

        static Pen pen1;
        static Graphics g;
        static Pen pen2;

        PointF point1 = new PointF(240, 160);
        PointF point2 = new PointF(720, 160);
        PointF point3 = new PointF(480, 480);

        
        static int Fractal (PointF p1, PointF p2, PointF p3, int lvl)
        {
            //n -количество итераций
            if (lvl > 0)  //условие выхода из рекурсии
            {
                //средняя треть отрезка
                var p4 = new PointF((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new PointF((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                //координаты вершины угла
                var ps = new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new PointF((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);
                //рисуем его
                g.DrawLine(pen1, p4, pn);
                g.DrawLine(pen1, p5, pn);
                g.DrawLine(pen2, p4, p5);


                //рекурсивно вызываем функцию нужное число раз
                Fractal(p4, pn, p5, lvl - 1);
                Fractal(pn, p5, p4, lvl - 1);
                Fractal(p1, p4, new PointF((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), lvl - 1);
                Fractal(p5, p2, new PointF((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), lvl - 1);

            }
            return lvl;
        }

        private void Draw (int lvl)
        {
            SetOption();
            DrawRectangle(ref g);
            AllFractal(lvl);
        }

        private void AllFractal (int lvl)
        {
            Fractal(point1, point2, point3, lvl);
            Fractal(point2, point3, point1, lvl);
            Fractal(point3, point1, point2, lvl);
        }

        private void SetOption ()
        {
            SetPens();
            ClearPicBox();
        }

        private void SetPens ()
        {
            pen1 = new Pen(Color.Red, 1);
            pen2 = new Pen(Color.Yellow, 1);
        }

        private void ClearPicBox()
        {
            g = picBox.CreateGraphics();
            g.Clear(Color.White);
        }

        private void DrawRectangle (ref Graphics g)
        {
            g.DrawLine(pen1, point1, point2);
            g.DrawLine(pen1, point2, point3);
            g.DrawLine(pen1, point3, point1);
        } 

        private void btnDraw_Click (object sender, EventArgs e)
        {
            Draw(Convert.ToInt32(LevelCount.Value));
        }
    }

   
    
}
