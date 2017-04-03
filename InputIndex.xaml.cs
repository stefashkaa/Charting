using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Charting
{
    /// <summary>
    /// Логика взаимодействия для InputIndex.xaml
    /// </summary>
    public partial class InputIndex : Window
    {
        public bool IsOK;
        public int Index;

        public InputIndex()
        {
            InitializeComponent();
            IsOK = false;
        }

        private void ok_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(index_txt.Text, out Index) || Index <= 0) 
            {
                MessageBox.Show("Поле 'индекс' введено не корректно!");
                return;
            }
            IsOK = true;
            this.Close();
        }
    }
}
