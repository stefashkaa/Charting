using System;
using System.Windows.Forms.DataVisualization.Charting;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            controller = App.Controller;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

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

        private void addGraphic(int id, string name, string function, int step, double min, double max) 
        {
            GraphicsElement ge = showFunction(id, name, function, step, min, max);
            elementsPanel.Children.Add(ge);
        }

        private GraphicsElement showFunction(int id, string name, string function, int step, double min, double max)
        {
            GraphicsElement ge = new GraphicsElement(id, name, function, step, min, max);
            //frontPanel.Height += ge.Height;
            //elementsPanel.Height += ge.Height;
            ge.Margin = new Thickness(1);
            ge.Width = elementsPanel.Width - scrollViewer.Width;
            return ge;
        }

        private void alterFunction_Click(object sender, RoutedEventArgs e)
        {
            InputIndex i = new InputIndex() { Title = "Изменение по индексу" };
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
            //TODO: очистка окна
            elementsPanel.Children.Clear();
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            closeScheme();
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
            }
            MessageBox.Show("Файл сохранен!");
        }

        private void closeSchema_Click(object sender, RoutedEventArgs e)
        {
            closeScheme();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void aboutProgram_Click(object sender, RoutedEventArgs e)
        {

        }

        private void aboutAuthor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteFunction_Click(object sender, RoutedEventArgs e)
        {
            InputIndex i = new InputIndex() { Title = "Удаление по индексу" };
            i.ShowDialog();
            if (!i.IsOK)
                return;
            controller.deleteFunctionById(i.Index - 1);
            elementsPanel.Children.RemoveAt(i.Index - 1);
            List<ChartingObject> list = controller.getAllChartings();
            for (int j = i.Index - 1; j < list.Count; j++)
            {
                elementsPanel.Children.RemoveAt(j);
                GraphicsElement ge = showFunction(j + 1,
                                                  list[j].Name, list[j].Function, list[j].Step,
                                                  list[j].Min, list[j].Max);
                elementsPanel.Children.Insert(j, ge);
            }
        }
    }


}
