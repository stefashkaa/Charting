using System;
using System.Windows;

namespace Charting
{
    /// <summary>
    /// Логика взаимодействия для InputIndex.xaml
    /// </summary>
    public partial class InputIndex : Window
    {
        private ChartingController controller;

        public bool IsOK;
        public int Index;

        public InputIndex()
        {
            InitializeComponent();
            IsOK = false;
        }

        public InputIndex(ChartingController controller) : this()
        {
            this.controller = controller;
        }

        private void ok_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(index_txt.Text, out Index) || Index <= 0 || 
                Index > controller.getAllChartings().Count) 
            {
                MessageBox.Show("Поле 'индекс' введено не корректно!");
                return;
            }
            IsOK = true;
            this.Close();
        }
    }
}
