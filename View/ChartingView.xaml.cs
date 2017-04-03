﻿using System;
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

        public ChartingView()
        {
            InitializeComponent();
            controller = new ChartingController();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //// Все графики находятся в пределах области построения ChartArea, создадим ее
            //chart.ChartAreas.Add(new ChartArea("Default"));

            //// Добавим линию, и назначим ее в ранее созданную область "Default"
            //chart.Series.Add(new Series("Series1"));
            //chart.Series["Series1"].ChartArea = "Default";
            //chart.Series["Series1"].ChartType = SeriesChartType.Line;

            //// добавим данные линии
            //string[] axisXData = new string[] { "a", "b", "c" };
            //double[] axisYData = new double[] { 0.1, 1.5, 1.9 };
            //chart.Series["Series1"].Points.DataBindXY(axisXData, axisYData);

        }

        private void addFunction_Click(object sender, RoutedEventArgs e)
        {
            AddFunction f = new AddFunction() { Title = "Добавление" }; 
            f.ShowDialog();
            if (!f.IsOK)
                return;
            if (!controller.addFunction(f.Name, f.Function,
                                       f.Step, f.Min, f.Max))
            {
                MessageBox.Show("Невозможно добавить график: непредвиденное исключение!");
                return;
            }
            GraphicsElement ge = new GraphicsElement(controller.getAllChartings().Count, 
                                                     f.Name, f.Function, f.Step, f.Min, f.Max);
            
            //frontPanel.Height += ge.Height;
            //elementsPanel.Height += ge.Height;
            ge.Margin = new Thickness(1);
            ge.Width = elementsPanel.Width - scrollViewer.Width;
            elementsPanel.Children.Add(ge);
            MessageBox.Show("График функции '"+f.Name+"' добавлен!");
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
            GraphicsElement ge = new GraphicsElement(i.Index,
                                                     alter.Name, alter.Function, alter.Step,
                                                     alter.Min, alter.Max);
            elementsPanel.Children.Insert(i.Index - 1, ge);
            MessageBox.Show("График функции '" + alter.Name + "' изменен!");
        }

        
    }


}
