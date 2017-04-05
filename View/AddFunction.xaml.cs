﻿using System;
using System.Windows;

namespace Charting
{
    /// <summary>
    /// Логика взаимодействия для AddFunction.xaml
    /// </summary>
    public partial class AddFunction : Window
    {
        public string Name;
        public string Function;
        public int Step;
        public double Min;
        public double Max;
        public bool IsOK;

        public AddFunction()
        {
            InitializeComponent();
            IsOK = false;
        }
        
        private void ok_Button_Click(object sender, RoutedEventArgs e)
        {
            Name = name_txt.Text;
            Function = function_txt.Text;
            if (!Int32.TryParse(step_txt.Text, out Step))
            {
                MessageBox.Show("Поле 'шаг' задано не корректно!");
                return;
            }
            if (!Double.TryParse(min_txt.Text, out Min))
            {
                MessageBox.Show("Поле 'min' задано не корректно!");
                return;
            }
            if (!Double.TryParse(max_txt.Text, out Max))
            {
                MessageBox.Show("Поле 'max' задано не корректно!");
                return;
            }
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Function))
            {
                MessageBox.Show("Поля 'название' и 'функция' не заданы!");
                return;
            }
            IsOK = true;
            this.Close();
        }
    }
}
