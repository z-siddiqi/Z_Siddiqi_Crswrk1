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
            // Set the aerofoil text box to 2412
            textBox1.Text = "2412";

            // Determine aerofoil's p and limit parameters and assign them to a variable
            string aerofoil = textBox1.Text;
            double p = Convert.ToDouble(aerofoil.Substring(1, 1)) / 10;
            double limit = Math.Acos(1 - 2 * p);

            // Set the relevant text boxes to their calculated value
            textBox2.Text = aerofoil.Substring(0, 1) + "%";
            textBox3.Text = Convert.ToString(p * 10) + "%";
            textBox4.Text = aerofoil.Substring(2, 2) + "%";
            textBox5.Text = " ";
            textBox6.Text = Convert.ToString(limit);
            textBox14.Text = " ";
            textBox15.Text = "0";
            textBox16.Text = "Normal";
        }
    
    // Analytical Calculation
    private void button1_Click(object sender, EventArgs e)
        {    
            string aerofoil = textBox1.Text;

            // Set alpha to 0 if text box is empty, otherwise set it to the text box value converted into radians
            double alpha = String.IsNullOrEmpty(textBox15.Text) ? 0 : Convert.ToDouble(textBox15.Text) * Math.PI / 180;

            // Check to see if input is invalid
            if (!int.TryParse(aerofoil, out int num) || aerofoil.Length > 5)
            {
                // Prompt the user to enter a valid input
                System.Windows.Forms.MessageBox.Show("Please enter a four or five digit NACA aerofoil.");
            }
            
            // Check to see if input is a 4 digit aerofoil
            else if (aerofoil.Length == 4)
            {   
                // Create an instance of the NACA_4_Digit class
                var fourDigit = new NACA_4_Digit(aerofoil);
                fourDigit.m = Convert.ToDouble(fourDigit.aerofoil.Substring(0, 1)) / 100;
                fourDigit.p = Convert.ToDouble(fourDigit.aerofoil.Substring(1, 1)) / 10;
                fourDigit.limit = Math.Acos(1 - 2 * fourDigit.p);

                // Set the relevant text boxes to their calculated value
                textBox2.Text = fourDigit.aerofoil.Substring(0, 1) + "%";
                textBox3.Text = Convert.ToString(fourDigit.p * 10) + "%";
                textBox4.Text = fourDigit.aerofoil.Substring(2, 2) + "%";
                textBox5.Text = " ";
                textBox6.Text = Convert.ToString(fourDigit.limit);
                textBox14.Text = " ";
                textBox15.Text = Convert.ToString(alpha * 180 / Math.PI);

                // Calculate An values using relevant methods from the NACA_4_Digit class 
                double A0 = alpha - fourDigit.A0();
                double int_A0 = fourDigit.A0();
                double A1 = fourDigit.A1();
                double A2 = fourDigit.A2();

                // Set the relevant text boxes to their calculated value
                textBox7.Text = A0.ToString();
                textBox8.Text = A1.ToString();
                textBox9.Text = A2.ToString();

                // Calculate aerofoil characteristics using An values
                double CL = Math.PI * (2 * A0 + A1);
                double CMac = -(Math.PI / 4) * (A1 - A2);
                double CMle = -(Math.PI / 2) * (A0 + A1 - A2 / 2);
                double alpha_zl = (int_A0 - A1 / 2) * 180 / Math.PI;

                // Set the relevant text boxes to their calculated value
                textBox10.Text = CL.ToString();
                textBox11.Text = CMle.ToString();
                textBox12.Text = CMac.ToString();
                textBox13.Text = alpha_zl.ToString();
            }

            // Input must be a 5 digit aerofoil
            else
            {
                // Create an instance of the NACA_5_Digit class
                var fiveDigit = new NACA_5_Digit(aerofoil);
                fiveDigit.r = constants(Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2)), 1);
                fiveDigit.k = constants(Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2)), 2);
                fiveDigit.camber = Math.Acos(1 - 2 * fiveDigit.r);
                double maxCamber = Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2));

                // Set the relevant text boxes to their calculated value
                textBox2.Text = fiveDigit.aerofoil.Substring(0, 1) + "%";
                textBox3.Text = Convert.ToString(maxCamber / 2) + "%";
                textBox4.Text = fiveDigit.aerofoil.Substring(3, 2) + "%";
                textBox5.Text = Convert.ToString(fiveDigit.k);
                textBox6.Text = Convert.ToString(fiveDigit.camber);
                textBox14.Text = Convert.ToString(fiveDigit.r);
                textBox15.Text = Convert.ToString(alpha * 180 / Math.PI);

                // Calculate An values using relevant methods from the NACA_5_Digit class 
                double A0 = alpha - fiveDigit.A0();
                double int_A0 = fiveDigit.A0();
                double A1 = fiveDigit.A1();
                double A2 = fiveDigit.A2();

                // Set the relevant text boxes to their calculated value
                textBox7.Text = A0.ToString();
                textBox8.Text = A1.ToString();
                textBox9.Text = A2.ToString();

                // Calculate aerofoil characteristics using An values
                double CL = Math.PI * (2 * A0 + A1);
                double CMac = -(Math.PI / 4) * (A1 - A2);
                double CMle = -(Math.PI / 2) * (A0 + A1 - A2 / 2);
                double alpha_zl = (int_A0 - A1 / 2) * 180 / Math.PI;

                // Set the relevant text boxes to their calculated value
                textBox10.Text = CL.ToString();
                textBox11.Text = CMle.ToString();
                textBox12.Text = CMac.ToString();
                textBox13.Text = alpha_zl.ToString();
            }
        }

        // Numerical Calculation
        private void button2_Click(object sender, EventArgs e)
        {
            string aerofoil = textBox1.Text;

            // Set alpha to 0 if text box is empty, otherwise set it to the text box value converted into radians
            double alpha = String.IsNullOrEmpty(textBox15.Text) ? 0 : Convert.ToDouble(textBox15.Text) * Math.PI / 180;

            // Check to see if input is invalid
            if (!int.TryParse(aerofoil, out int num) || aerofoil.Length > 5)
            {
                // Prompt the user to enter a valid input
                MessageBox.Show("Please enter a four or five digit NACA aerofoil.");
            }

            // Check to see if input is a 4 digit aerofoil
            else if (aerofoil.Length == 4)
            {
                // Create an instance of the NACA_4_Digit class
                var fourDigit = new NACA_4_Digit(aerofoil);
                fourDigit.m = Convert.ToDouble(fourDigit.aerofoil.Substring(0, 1)) / 100;
                fourDigit.p = Convert.ToDouble(fourDigit.aerofoil.Substring(1, 1)) / 10;
                fourDigit.limit = Math.Acos(1 - 2 * fourDigit.p);

                // Set the relevant text boxes to their calculated value
                textBox2.Text = fourDigit.aerofoil.Substring(0, 1) + "%";
                textBox3.Text = Convert.ToString(fourDigit.p * 10) + "%";
                textBox4.Text = fourDigit.aerofoil.Substring(2, 2) + "%";
                textBox5.Text = " ";
                textBox6.Text = Convert.ToString(fourDigit.limit);
                textBox14.Text = " ";
                textBox15.Text = Convert.ToString(alpha * 180 / Math.PI);

                // Calculate An values using Simpsons rule function
                double A0 = alpha - (1 / Math.PI) * (SimpsonsRule(0, fourDigit.limit, 0, fourDigit.dzdxf, fourDigit.m, fourDigit.p) + 
                            SimpsonsRule(fourDigit.limit, Math.PI, 0, fourDigit.dzdxa, fourDigit.m, fourDigit.p));
                double int_A0 = (1 / Math.PI) * (SimpsonsRule(0, fourDigit.limit, 0, fourDigit.dzdxf, fourDigit.m, fourDigit.p) + 
                                SimpsonsRule(fourDigit.limit, Math.PI, 0, fourDigit.dzdxa, fourDigit.m, fourDigit.p));
                double A1 = (2 / Math.PI) * (SimpsonsRule(0, fourDigit.limit, 1, fourDigit.dzdxf, fourDigit.m, fourDigit.p) + 
                            SimpsonsRule(fourDigit.limit, Math.PI, 1, fourDigit.dzdxa, fourDigit.m, fourDigit.p));
                double A2 = (2 / Math.PI) * (SimpsonsRule(0, fourDigit.limit, 2, fourDigit.dzdxf, fourDigit.m, fourDigit.p) + 
                            SimpsonsRule(fourDigit.limit, Math.PI, 2, fourDigit.dzdxa, fourDigit.m, fourDigit.p));

                // Set the relevant text boxes to their calculated value
                textBox7.Text = A0.ToString();
                textBox8.Text = A1.ToString();
                textBox9.Text = A2.ToString();

                // Calculate aerofoil characteristics using An values
                double CL = Math.PI * (2 * A0 + A1);
                double CMac = -(Math.PI / 4) * (A1 - A2);
                double CMle = -(Math.PI / 2) * (A0 + A1 - A2 / 2);
                double alpha_zl = (int_A0 - A1 / 2) * 180 / Math.PI;

                // Set the relevant text boxes to their calculated value
                textBox10.Text = CL.ToString();
                textBox11.Text = CMle.ToString();
                textBox12.Text = CMac.ToString();
                textBox13.Text = alpha_zl.ToString();
            }

            // Input must be a 5 digit aerofoil
            else 
            {
                // Create an instance of the NACA_5_Digit class
                var fiveDigit = new NACA_5_Digit(aerofoil);
                fiveDigit.r = constants(Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2)), 1);
                fiveDigit.k = constants(Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2)), 2);
                fiveDigit.camber = Math.Acos(1 - 2 * fiveDigit.r);
                double maxCamber = Convert.ToDouble(fiveDigit.aerofoil.Substring(1, 2));

                // Set the relevant text boxes to their calculated value
                textBox2.Text = fiveDigit.aerofoil.Substring(0, 1) + "%";
                textBox3.Text = Convert.ToString(maxCamber / 2) + "%";
                textBox4.Text = fiveDigit.aerofoil.Substring(3, 2) + "%";
                textBox5.Text = Convert.ToString(fiveDigit.k);
                textBox6.Text = Convert.ToString(fiveDigit.camber);
                textBox14.Text = Convert.ToString(fiveDigit.r);
                textBox15.Text = Convert.ToString(alpha * 180 / Math.PI);

                // Calculate An values using Simpsons rule function
                double A0 = alpha - (1 / Math.PI) * (SimpsonsRule(0, fiveDigit.camber, 0, fiveDigit.dzdxf, fiveDigit.k, fiveDigit.r) + 
                            SimpsonsRule(fiveDigit.camber, Math.PI, 0, fiveDigit.dzdxa, fiveDigit.k, fiveDigit.r));
                double int_A0 = (1 / Math.PI) * (SimpsonsRule(0, fiveDigit.camber, 0, fiveDigit.dzdxf, fiveDigit.k, fiveDigit.r) + 
                                SimpsonsRule(fiveDigit.camber, Math.PI, 0, fiveDigit.dzdxa, fiveDigit.k, fiveDigit.r));
                double A1 = (2 / Math.PI) * (SimpsonsRule(0, fiveDigit.camber, 1, fiveDigit.dzdxf, fiveDigit.k, fiveDigit.r) + 
                            SimpsonsRule(fiveDigit.camber, Math.PI, 1, fiveDigit.dzdxa, fiveDigit.k, fiveDigit.r));
                double A2 = (2 / Math.PI) * (SimpsonsRule(0, fiveDigit.camber, 2, fiveDigit.dzdxf, fiveDigit.k, fiveDigit.r) + 
                            SimpsonsRule(fiveDigit.camber, Math.PI, 2, fiveDigit.dzdxa, fiveDigit.k, fiveDigit.r));

                // Set the relevant text boxes to their calculated value
                textBox7.Text = A0.ToString();
                textBox8.Text = A1.ToString();
                textBox9.Text = A2.ToString();

                // Calculate aerofoil characteristics using An values
                double CL = Math.PI * (2 * A0 + A1);
                double CMac = -(Math.PI / 4) * (A1 - A2);
                double CMle = -(Math.PI / 2) * (A0 + A1 - A2 / 2);
                double alpha_zl = (int_A0 - A1 / 2) * 180 / Math.PI;

                // Set the relevant text boxes to their calculated value
                textBox10.Text = CL.ToString();
                textBox11.Text = CMle.ToString();
                textBox12.Text = CMac.ToString();
                textBox13.Text = alpha_zl.ToString();
            }
        }
        
        // Exit button
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // This function returns k and r values for a a 5 digit aerofoil
        public double constants(double x, double y)
        {
            // Create table of constants with each row representing an aerofoil
            DataTable constants = new DataTable();
            constants.Columns.Add("Digits", typeof(double));
            constants.Columns.Add("r", typeof(double));
            constants.Columns.Add("k", typeof(double));
            constants.Rows.Add(10, 0.0580, 361.400);
            constants.Rows.Add(20, 0.1260, 51.640);
            constants.Rows.Add(30, 0.2025, 15.957);
            constants.Rows.Add(40, 0.2900, 6.643);
            constants.Rows.Add(50, 0.3910, 3.230);

            // Iterate over each row
            foreach (DataRow dr in constants.Rows)
            {
                // Check to see if input x matches the "Digits" column of a row
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

        // This function integrates using Simpsons rule
        public double SimpsonsRule(double ll, double ul, double n, Func<double, double, double, double, double> func, double m, double p)
        {
            // Number of segments
            int nn = 8;

            // Calculate delta x
            double h = (ul - ll) / nn;

            // Create arrays for x and fx
            double[] x = new double[10];
            double[] fx = new double[10];

            // Iterate over segments
            for (int i = 0; i <= nn; i++)
            {
                // Calculate x and fx for each segment
                x[i] = ll + i * h;
                fx[i] = func(m, p, x[i], n);
            }

            double res = 0;

            // Iterate over segments
            for (int i = 0; i <= nn; i++)
            {
                // Check to see if first or last index
                if (i == 0 || i == nn)
                    res += fx[i];
                
                // Check to see if modulo of index is uneven
                else if (i % 2 != 0)
                    res += 4 * fx[i];
                
                // Index must be even
                else
                    res += 2 * fx[i];
            }

            // Multiply calculated result by delta x over 3
            res = res * (h / 3);
            return res;
        }
    }

    // This class contains functions for 4 digit NACA aerofoils
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
            return (1 / Math.PI) * (A0_fore(m, p, limit) - A0_fore(m, p, 0) + A0_aft(m, p, Math.PI) - A0_aft(m, p, limit));
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
    }

    // This class contains functions for 5 digit NACA aerofoils
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
            return (1 / Math.PI) * (A0_fore(k, r, camber) + A0_aft(k, r, camber));
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
    }
}
