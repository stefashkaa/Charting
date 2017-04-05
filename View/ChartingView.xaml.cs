using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Charting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ChartingView : Window
    {
        private ChartingController controller;

        private System.Windows.Forms.SaveFileDialog save;
        private System.Windows.Forms.OpenFileDialog open;

        public ChartingView()
        {
            InitializeComponent();
        }

        public ChartingView(ChartingController c)
            : this()
        {
            controller = c;
        }

        #region ЭЛЕМЕНТЫ МЕНЮ
        private void addFunction_Click(object sender, RoutedEventArgs e)
        {
            AddFunction f = new AddFunction() { Title = "Добавление" }; 
            f.ShowDialog();
            if (!f.IsOK)
                return;
            if (!controller.addFunction(f.Name, f.Function, f.Step, f.Min, f.Max))
            {
                MessageBox.Show("Невозможно добавить график: непредвиденное исключение!");
                return;
            }
            addGraphic(controller.getAllChartings().Count,
                        f.Name, f.Function, f.Step, f.Min, f.Max);
            MessageBox.Show("График функции '"+f.Name+"' добавлен!");
        }

        private void alterFunction_Click(object sender, RoutedEventArgs e)
        {
            InputIndex i = new InputIndex(controller) { Title = "Изменение по индексу" };
            i.ShowDialog();
            if (!i.IsOK)
                return;
            AddFunction alter = new AddFunction() { Title = "Изменение" };
            
            ChartingObject obj = controller.getAllChartings()[i.Index - 1];
            alter.name_txt.Text = obj.Name;
            alter.function_txt.Text = obj.Function;
            alter.step_txt.Text = obj.Step.ToString();
            alter.min_txt.Text = obj.Min.ToString();
            alter.max_txt.Text = obj.Max.ToString();
            alter.ShowDialog();
            if (!alter.IsOK)
                return;
            if (!controller.alterFunction(i.Index - 1, alter.Name, alter.Function,
                                         alter.Step, alter.Min, alter.Max))
            {
                MessageBox.Show("Не могу изменить функцию: индекс = " + i.Index + 
                                " за пределами диапазона!");
                return;
            }
            elementsPanel.Children.RemoveAt(i.Index - 1);
            GraphicsElement ge = showFunction(i.Index,
                                              alter.Name, alter.Function, alter.Step,
                                              alter.Min, alter.Max);
            elementsPanel.Children.Insert(i.Index - 1, ge);
            MessageBox.Show("График функции '" + alter.Name + "' изменен!");
        }

        private void newFile_Click(object sender, RoutedEventArgs e)
        {
            closeScheme();
            schemeItem.IsEnabled = true;
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            closeScheme();
            schemeItem.IsEnabled = true;
            open = new System.Windows.Forms.OpenFileDialog();
            open.Filter = "(*.xml)|*.xml|All files(*.*)|*.*";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.FileName == null)
                    return;

                if (!controller.openSchema(open.FileName))
                {
                    MessageBox.Show("Не удалось открыть файл!", "Error",
                                     MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                List<ChartingObject> tmp = controller.getAllChartings();
                for (int i = 0; i < tmp.Count; i++)
                {
                    addGraphic(i+1, tmp[i].Name, tmp[i].Function, tmp[i].Step, tmp[i].Min, tmp[i].Max);
                }
            }
        }

        private void saveFile_Click(object sender, RoutedEventArgs e)
        {
            if (controller.getAllChartings().Count == 0)
                return;
            string s = "tmp.xml";
            if (open != null)
            {
                s = open.FileName;
            }
            controller.saveScheme(s);
            MessageBox.Show("Файл сохранен!");
        }

        private void saveAsFile_Click(object sender, RoutedEventArgs e)
        {
            save = new System.Windows.Forms.SaveFileDialog();
            save.Filter = "XML format (*.xml)|*.xml";
            save.FileName = "Новая схема";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                controller.saveScheme(save.FileName);
                MessageBox.Show("Файл сохранен!");
            }
        }

        private void closeSchema_Click(object sender, RoutedEventArgs e)
        {
            closeScheme();
            schemeItem.IsEnabled = false;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void aboutProgram_Click(object sender, RoutedEventArgs e)
        {
            new View.AboutProgram().ShowDialog();
        }

        private void deleteFunction_Click(object sender, RoutedEventArgs e)
        {
            InputIndex i = new InputIndex(controller) { Title = "Удаление по индексу" };
            i.ShowDialog();
            if (!i.IsOK)
                return;
            ChartingObject tmp = controller.getAllChartings()[i.Index - 1];
            controller.deleteFunctionById(i.Index - 1);
            elementsPanel.Children.RemoveAt(i.Index - 1);
            List<ChartingObject> list = controller.getAllChartings();
            for (int j = i.Index - 1; j < list.Count; j++)
            {
                elementsPanel.Children.RemoveAt(j);
                GraphicsElement ge = showFunction(j+1,
                                                  list[j].Name, list[j].Function, list[j].Step,
                                                  list[j].Min, list[j].Max);
                elementsPanel.Children.Insert(j, ge);
            }
            MessageBox.Show("График функции '" + tmp.Name + "' удалён!");

        }
        #endregion

        private void addGraphic(int id, string name, string function, int step, double min, double max)
        {
            GraphicsElement ge = showFunction(id, name, function, step, min, max);
            elementsPanel.Children.Add(ge);
        }

        private GraphicsElement showFunction(int id, string name, string function, int step, double min, double max)
        {
            GraphicsElement ge = new GraphicsElement(id, name, function, step, min, max);
            ge.Margin = new Thickness(1);
            ge.Width = elementsPanel.Width - scrollViewer.Width;
            return ge;
        }

        private void closeScheme()
        {
            if (controller.getAllChartings().Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Сохранить текущую схему?", "Warning",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                saveAsFile_Click(null, null);
            }
            clearAllObjects();
            controller.clearObjects();
        }

        private void clearAllObjects()
        {
            elementsPanel.Children.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть программу?", "Закрытие программы", MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
                Application.Current.Shutdown();
        }
    }
}
