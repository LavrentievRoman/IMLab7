using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Lab7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double priceCoef = 0.002, competitorsCoef = 0.00003, staffCoef = 0.0001, speedCoef = 0.5;

        Random rnd = new Random();

        double price, profit, serviceSpeed;

        int population, competitors, staff, clientPerMonth;

        int month;

        double prevProfit;

        private void startButton_Click_1(object sender, EventArgs e)
        {
            price = (double)priceNum.Value;
            population = (int)populationNum.Value;
            clientPerMonth = 10;

            month = 0;
            prevProfit = 10;
            competitors = (int)(population * competitorsCoef);
            staff = (int)(population * staffCoef);
            serviceSpeed = staff * speedCoef;

            chart2.Series[0].Points.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            chart2.Series[0].Points.AddXY(0, population);
            chart1.Series[0].Points.AddXY(0, price);
            chart1.Series[1].Points.AddXY(0, clientPerMonth);

            timer1.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            month++;

            profit = clientPerMonth * price;
            price = prevProfit > profit ? price -= price * priceCoef : price += price * priceCoef;

            competitors += (int)(population * competitorsCoef * (rnd.NextDouble() - 0.5));
            staff += (int)(population * staffCoef * (rnd.NextDouble() - 0.5));
            serviceSpeed = staff * speedCoef;

            clientPerMonth += (int)(((price * 0.02) - (competitors * 0.4) + (serviceSpeed * 0.3)) * (rnd.NextDouble() - 0.45));

            clientPerMonth = clientPerMonth > 0 ? clientPerMonth : 0;

            prevProfit = profit;
            population += (int)((rnd.NextDouble() - 0.49) * population * 0.1);

            chart2.Series[0].Points.AddXY(month, population);
            chart1.Series[0].Points.AddXY(month, price);
            chart1.Series[1].Points.AddXY(month, clientPerMonth);

            populationNum.Value = (decimal)population;
            priceNum.Value = (decimal)price;
            InfLabel.Text = Convert.ToString(clientPerMonth);
        }
    }
}
