using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Z_Siddiqi_Crswrk1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "2412";

            string aerofoil = textBox1.Text;
            double location = Convert.ToDouble(aerofoil.Substring(1, 1));
            double limit = Math.Acos(1 - 2 * location / 10);

            textBox2.Text = aerofoil.Substring(0, 1) + "%";
            textBox3.Text = aerofoil.Substring(1, 1) + "%";
            textBox4.Text = aerofoil.Substring(2, 2) + "%";
            textBox5.Text = " ";
            textBox6.Text = Convert.ToString(limit);
            textBox14.Text = " ";
            textBox16.Text = "Normal";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aerofoil = textBox1.Text;

            if (aerofoil.Length == 4)
            {
                double alpha = 4 * Math.PI / 180;
                textBox15.Text = Convert.ToString(alpha * 180 / Math.PI);

                var fourDigit = new NACA_4_Digit(aerofoil);
                fourDigit.m = Convert.ToDouble(fourDigit.aerofoil.Substring(0, 1)) / 100;
                fourDigit.p = Convert.ToDouble(fourDigit.aerofoil.Substring(1, 1)) / 10;
                fourDigit.limit = Math.Acos(1 - 2 * fourDigit.p);

                textBox2.Text = fourDigit.aerofoil.Substring(0, 1) + "%";
                textBox3.Text = fourDigit.aerofoil.Substring(1, 1) + "%";
                textBox4.Text = fourDigit.aerofoil.Substring(2, 2) + "%";
                textBox5.Text = " ";
                textBox6.Text = Convert.ToString(fourDigit.limit);
                textBox14.Text = " ";

                double A0 = alpha + fourDigit.A0();
                double int_A0 = - fourDigit.A0();
                double A1 = fourDigit.A1();
                double A2 = fourDigit.A2();

                textBox7.Text = A0.ToString();
                textBox8.Text = A1.ToString();
                textBox9.Text = A2.ToString();

                double CL = Math.PI * (2 * A0 + A1);
                double CMac = -(Math.PI / 4) * (A1 - A2);
                double CMle = -(Math.PI / 2) * (A0 + A1 - A2 / 2);
                double alpha_zl = (int_A0 - A1 / 2) * 180 / Math.PI;

                textBox10.Text = CL.ToString();
                textBox11.Text = CMle.ToString();
                textBox12.Text = CMac.ToString();
                textBox13.Text = alpha_zl.ToString();
            }

            else if (aerofoil.Length == 5)
            {
                double alpha = 0 * Math.PI / 180;
                textBox15.Text = Convert.ToString(alpha * 180 / Math.PI);

                var fiveDigit = new NACA_5_Digit(aerofoil);
                fiveDigit.r = constants(Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2)), 1);
                fiveDigit.k = constants(Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2)), 2);
                fiveDigit.camber = Math.Acos(1 - 2 * fiveDigit.r);

                double maxCamber = Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2));

                textBox2.Text = fiveDigit.aerofoil.Substring(0, 1) + "%";
                textBox3.Text = Convert.ToString(maxCamber / 2) + "%";
                textBox4.Text = fiveDigit.aerofoil.Substring(3, 2) + "%";
                textBox5.Text = Convert.ToString(fiveDigit.k);
                textBox6.Text = Convert.ToString(fiveDigit.camber);
                textBox14.Text = Convert.ToString(fiveDigit.r);

                double A0 = alpha + fiveDigit.A0();
                double int_A0 = -fiveDigit.A0();
                double A1 = fiveDigit.A1();
                double A2 = fiveDigit.A2();

                textBox7.Text = A0.ToString();
                textBox8.Text = A1.ToString();
                textBox9.Text = A2.ToString();

                double CL = Math.PI * (2 * A0 + A1);
                double CMac = -(Math.PI / 4) * (A1 - A2);
                double CMle = -(Math.PI / 2) * (A0 + A1 - A2 / 2);
                double alpha_zl = (int_A0 - A1 / 2) * 180 / Math.PI;

                textBox10.Text = CL.ToString();
                textBox11.Text = CMle.ToString();
                textBox12.Text = CMac.ToString();
                textBox13.Text = alpha_zl.ToString();
            }
        }

        public static double constants(double x, double y)
        {
            DataTable constants = new DataTable();
            constants.Columns.Add("Digits", typeof(double));
            constants.Columns.Add("r", typeof(double));
            constants.Columns.Add("k", typeof(double));
            constants.Rows.Add(10, 0.0580, 361.400);
            constants.Rows.Add(20, 0.1260, 51.640);
            constants.Rows.Add(30, 0.2025, 15.957);
            constants.Rows.Add(40, 0.2900, 6.643);
            constants.Rows.Add(50, 0.3910, 3.230);

            foreach (DataRow dr in constants.Rows)
            {
                if ((double)dr["Digits"] == x)
                {
                    if (y == 1)
                    {
                        return (double)dr["r"];
                    }

                    else if (y == 2)
                    {
                        return (double)dr["k"];
                    }
                }
            }
            return 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string aerofoil = textBox1.Text;

            if (aerofoil.Length == 4)
            {
                double alpha = 4 * Math.PI / 180;
                textBox15.Text = Convert.ToString(alpha * 180 / Math.PI);

                var fourDigit = new NACA_4_Digit(aerofoil);
                fourDigit.m = Convert.ToDouble(fourDigit.aerofoil.Substring(0, 1)) / 100;
                fourDigit.p = Convert.ToDouble(fourDigit.aerofoil.Substring(1, 1)) / 10;
                fourDigit.limit = Math.Acos(1 - 2 * fourDigit.p);

                textBox2.Text = fourDigit.aerofoil.Substring(0, 1) + "%";
                textBox3.Text = fourDigit.aerofoil.Substring(1, 1) + "%";
                textBox4.Text = fourDigit.aerofoil.Substring(2, 2) + "%";
                textBox5.Text = " ";
                textBox6.Text = Convert.ToString(fourDigit.limit);
                textBox14.Text = " ";

                double A0 = alpha - (1 / Math.PI) * (fourDigit.SimpsonsRule(0, fourDigit.limit, 0, fourDigit.dzdxf) + fourDigit.SimpsonsRule(fourDigit.limit, Math.PI, 0, fourDigit.dzdxa));
                double int_A0 = (1 / Math.PI) * (fourDigit.SimpsonsRule(0, fourDigit.limit, 0, fourDigit.dzdxf) + fourDigit.SimpsonsRule(fourDigit.limit, Math.PI, 0, fourDigit.dzdxa));
                double A1 = (2 / Math.PI) * (fourDigit.SimpsonsRule(0, fourDigit.limit, 1, fourDigit.dzdxf) + fourDigit.SimpsonsRule(fourDigit.limit, Math.PI, 1, fourDigit.dzdxa));
                double A2 = (2 / Math.PI) * (fourDigit.SimpsonsRule(0, fourDigit.limit, 2, fourDigit.dzdxf) + fourDigit.SimpsonsRule(fourDigit.limit, Math.PI, 2, fourDigit.dzdxa));

                textBox7.Text = A0.ToString();
                textBox8.Text = A1.ToString();
                textBox9.Text = A2.ToString();

                double CL = Math.PI * (2 * A0 + A1);
                double CMac = -(Math.PI / 4) * (A1 - A2);
                double CMle = -(Math.PI / 2) * (A0 + A1 - A2 / 2);
                double alpha_zl = (int_A0 - A1 / 2) * 180 / Math.PI;

                textBox10.Text = CL.ToString();
                textBox11.Text = CMle.ToString();
                textBox12.Text = CMac.ToString();
                textBox13.Text = alpha_zl.ToString();
            }

            else if (aerofoil.Length == 5)
            {
                double alpha = 0 * Math.PI / 180;
                textBox15.Text = Convert.ToString(alpha * 180 / Math.PI);

                var fiveDigit = new NACA_5_Digit(aerofoil);
                fiveDigit.r = constants(Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2)), 1);
                fiveDigit.k = constants(Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2)), 2);
                fiveDigit.camber = Math.Acos(1 - 2 * fiveDigit.r);
                
                double maxCamber = Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2));

                textBox2.Text = fiveDigit.aerofoil.Substring(0, 1) + "%";
                textBox3.Text = Convert.ToString(maxCamber / 2) + "%";
                textBox4.Text = fiveDigit.aerofoil.Substring(3, 2) + "%";
                textBox5.Text = Convert.ToString(fiveDigit.k);
                textBox6.Text = Convert.ToString(fiveDigit.camber);
                textBox14.Text = Convert.ToString(fiveDigit.r);

                double A0 = alpha - (1 / Math.PI) * (fiveDigit.SimpsonsRule(0, fiveDigit.camber, 0, fiveDigit.dzdxf) + fiveDigit.SimpsonsRule(fiveDigit.camber, Math.PI, 0, fiveDigit.dzdxa));
                double int_A0 = (1 / Math.PI) * (fiveDigit.SimpsonsRule(0, fiveDigit.camber, 0, fiveDigit.dzdxf) + fiveDigit.SimpsonsRule(fiveDigit.camber, Math.PI, 0, fiveDigit.dzdxa));
                double A1 = (2 / Math.PI) * (fiveDigit.SimpsonsRule(0, fiveDigit.camber, 1, fiveDigit.dzdxf) + fiveDigit.SimpsonsRule(fiveDigit.camber, Math.PI, 1, fiveDigit.dzdxa));
                double A2 = (2 / Math.PI) * (fiveDigit.SimpsonsRule(0, fiveDigit.camber, 2, fiveDigit.dzdxf) + fiveDigit.SimpsonsRule(fiveDigit.camber, Math.PI, 2, fiveDigit.dzdxa));

                textBox7.Text = A0.ToString();
                textBox8.Text = A1.ToString();
                textBox9.Text = A2.ToString();

                double CL = Math.PI * (2 * A0 + A1);
                double CMac = -(Math.PI / 4) * (A1 - A2);
                double CMle = -(Math.PI / 2) * (A0 + A1 - A2 / 2);
                double alpha_zl = (int_A0 - A1 / 2) * 180 / Math.PI;

                textBox10.Text = CL.ToString();
                textBox11.Text = CMle.ToString();
                textBox12.Text = CMac.ToString();
                textBox13.Text = alpha_zl.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }

    public class NACA_4_Digit
    {
        public string aerofoil;
        public double m;
        public double p;
        public double limit;

        public NACA_4_Digit(string aerofoil)
        {
            this.aerofoil = aerofoil;
        }

        public double A0()
        {
            return -(1 / Math.PI) * (A0_fore(m, p, limit) - A0_fore(m, p, 0) + A0_aft(m, p, Math.PI) - A0_aft(m, p, limit));
        }
        public double A1()
        {
            return (2 / Math.PI) * (A1_fore(m, p, limit) - A1_fore(m, p, 0) + A1_aft(m, p, Math.PI) - A1_aft(m, p, limit));
        }
        public double A2()
        {
            return (2 / Math.PI) * (A2_fore(m, p, limit) - A2_fore(m, p, 0) + A2_aft(m, p, Math.PI) - A2_aft(m, p, limit));
        }

        public double A0_fore(double m, double p, double x)
        {
            return m * x * (2 * p - 1) / Math.Pow(p, 2) + (m / Math.Pow(p, 2)) * Math.Sin(x);
        }
        public double A0_aft(double m, double p, double x)
        {
            return m * x * (2 * p - 1) / Math.Pow(1 - p, 2) + (m / Math.Pow(1 - p, 2)) * Math.Sin(x);
        }
        public double A1_fore(double m, double p, double x)
        {
            return m * (2 * p - 1) * Math.Sin(x) / Math.Pow(p, 2) + (m / Math.Pow(p, 2)) * (x / 2 + Math.Sin(2 * x) / 4);
        }
        public double A1_aft(double m, double p, double x)
        {
            return m * (2 * p - 1) * Math.Sin(x) / Math.Pow(1 - p, 2) + (m / Math.Pow(1 - p, 2)) * (x / 2 + Math.Sin(2 * x) / 4);
        }
        public double A2_fore(double m, double p, double x)
        {
            return m * (2 * p - 1) * Math.Sin(2 * x) / (2 * Math.Pow(p, 2)) + (m / Math.Pow(p, 2)) * (Math.Sin(3 * x) / 6 + Math.Sin(x) / 2);
        }
        public double A2_aft(double m, double p, double x)
        {
            return m * (2 * p - 1) * Math.Sin(2 * x) / (2 * Math.Pow(1 - p, 2)) + (m / Math.Pow(1 - p, 2)) * (Math.Sin(3 * x) / 6 + Math.Sin(x) / 2);
        }

        public double dzdxf(double m, double p, double x, double n)
        {
            return m / Math.Pow(p, 2) * (2 * p - 1 + Math.Cos(x)) * Math.Cos(n * x);
        }
        public double dzdxa(double m, double p, double x, double n)
        {
            return m / Math.Pow(1 - p, 2) * (2 * p - 1 + Math.Cos(x)) * Math.Cos(n * x);
        }

        public double SimpsonsRule(double ll, double ul, double n, Func<double, double, double, double, double> func)
        {
            int nn = 6;

            double h = (ul - ll) / nn;

            double[] x = new double[10];
            double[] fx = new double[10];

            for (int i = 0; i <= nn; i++)
            {
                x[i] = ll + i * h;
                fx[i] = func(m, p, x[i], n);
            }

            double res = 0;
            for (int i = 0; i <= nn; i++)
            {
                if (i == 0 || i == nn)
                    res += fx[i];
                else if (i % 2 != 0)
                    res += 4 * fx[i];
                else
                    res += 2 * fx[i];
            }

            res = res * (h / 3);
            return res;
        }
    }

    public class NACA_5_Digit
    {
        public string aerofoil;
        public double r;
        public double k;
        public double camber;

        public NACA_5_Digit(string aerofoil)
        {
            this.aerofoil = aerofoil;
        }

        public double A0()
        {
            return -(1 / Math.PI) * (A0_fore(k, r, camber) + A0_aft(k, r, camber));
        }
        public double A1()
        {
            return (2 / Math.PI) * (A1_fore(k, r, camber) + A1_aft(k, r, camber));
        }
        public double A2()
        {
            return (2 / Math.PI) * (A2_fore(k, r, camber) + A2_aft(k, r, camber));
        }

        public double A0_fore(double k, double r, double x)
        {
            double A = k / 8 - k * r / 2 + (k * Math.Pow(r, 2) * (3 - r)) / 6;
            double B = k * r / 2 - k / 4;
            double C = k / 8;

            return A * x + B * Math.Sin(x) + C * (x / 2 + Math.Sin(2 * x) / 4);
        }
        public double A0_aft(double k, double r, double x)
        {
            return (-k * Math.Pow(r, 3) * Math.PI / 6) - (-k * Math.Pow(r, 3) * x / 6);
        }
        public double A1_fore(double k, double r, double x)
        {
            double A = k / 8 - k * r / 2 + (k * Math.Pow(r, 2) * (3 - r)) / 6;
            double B = k * r / 2 - k / 4;
            double C = k / 8;

            return A * Math.Sin(x) + B * (x / 2 + Math.Sin(2 * x) / 4) + C * (Math.Sin(x) - Math.Pow(Math.Sin(x), 3) / 3);
        }
        public double A1_aft(double k, double r, double x)
        {
            return (-k * Math.Pow(r, 3) * Math.Sin(Math.PI) / 6) - (-k * Math.Pow(r, 3) * Math.Sin(x) / 6);
        }
        public double A2_fore(double k, double r, double x)
        {
            double A = k / 8 - k * r / 2 + (k * Math.Pow(r, 2) * (3 - r)) / 6;
            double B = k * r / 2 - k / 4;
            double C = k / 8;

            return A * Math.Sin(2 * x) / 2 + B * (Math.Sin(3 * x) / 6 + Math.Sin(x) / 2) + C * (x / 4 + Math.Sin(2 * x) / 4 + Math.Sin(4 * x) / 16);
        }
        public double A2_aft(double k, double r, double x)
        {
            return (-k * Math.Pow(r, 3) * Math.Sin(2 * Math.PI) / 12) - (-k * Math.Pow(r, 3) * Math.Sin(2 * x) / 12);
        }

        public double dzdxf(double k, double r, double x, double n)
        {
            double A = k / 8 - k * r / 2 + (k * Math.Pow(r, 2) * (3 - r)) / 6;
            double B = k * r / 2 - k / 4;
            double C = k / 8;

            return (A + B * Math.Cos(x) + C * Math.Pow(Math.Cos(x), 2)) * Math.Cos(n * x);
        }
        public double dzdxa(double k, double r, double x, double n)
        {
            return (-k * Math.Pow(r, 3) / 6) * Math.Cos(n * x);
        }

        public double SimpsonsRule(double ll, double ul, double n, Func<double, double, double, double, double> func)
        {
            int nn = 6;

            double h = (ul - ll) / nn;

            double[] x = new double[10];
            double[] fx = new double[10];

            for (int i = 0; i <= nn; i++)
            {
                x[i] = ll + i * h;
                fx[i] = func(k, r, x[i], n);
            }

            double res = 0;
            for (int i = 0; i <= nn; i++)
            {
                if (i == 0 || i == nn)
                    res += fx[i];
                else if (i % 2 != 0)
                    res += 4 * fx[i];
                else
                    res += 2 * fx[i];
            }

            res = res * (h / 3);
            return res;
        }
    }
}
