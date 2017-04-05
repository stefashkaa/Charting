using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Charting.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private int count;

        public SplashScreen()
        {
            InitializeComponent();
              
            //grid.Background
            count = 0;
            var timer = new Timer() { Interval = 50 };
            timer.Tick += new EventHandler(timer_Tick);

            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var timer = sender as Timer;

            count++;
            progressBar.Value = count;

            if (timer != null && count == 100)
            {
                timer.Stop();
                new ChartingView().Show();
                Close();
            }
        }
    }
}
