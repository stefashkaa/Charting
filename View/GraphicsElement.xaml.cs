﻿using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using ELW.Library.Math;
using ELW.Library.Math.Exceptions;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;

namespace Charting
{
    /// <summary>
    /// Логика взаимодействия для GraphicsElement.xaml
    /// </summary>
    public partial class GraphicsElement : System.Windows.Controls.UserControl
    {
        public bool isError {get; set;}

        public GraphicsElement()
        {
            InitializeComponent();
            isError = false;
        }

        public GraphicsElement(int id, string name, string function, 
                               int step, double min, double max) : this()
        {
            id_txt.Content = id;
            name_txt.Text = name;
            function_txt.Text = function;
            step_txt.Text = step.ToString();
            min_txt.Text = min.ToString();
            max_txt.Text = max.ToString();
            drawFunction(id, function, step, min, max);
        }

        public void drawFunction(int id, string function,
                               int step, double min, double max)
        {
            chart.ChartAreas.Add(new ChartArea("Default"));
            
            chart.Series.Add(new Series("Series1"));

            chart.Series["Series1"].ChartArea = "Default";
            chart.Series["Series1"].ChartType = SeriesChartType.FastLine;
            chart.Series["Series1"].BorderWidth = 2;
            
            double[] xData;
            double[] yData = calc(function, step, min, max, out xData);
            if(yData != null)
                chart.Series["Series1"].Points.DataBindXY(xData, yData);
        }

        private double[] calc(string function, int step, double min, double max, out double[] xData)
        {
            xData = new double[step];
            double[] yData = new double[step];
            try
            {
                double delta = (max - min) / step;
                double tmp = min;
                List<VariableValue> values = new List<VariableValue>();

                for (int i = 0; i < step; i++)
                {
                    values.Add(new VariableValue(tmp, "x"));

                    xData[i] = tmp;
 
                    PreparedExpression preparedExpression = ToolsHelper.Parser.Parse(function);
                    CompiledExpression compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
                    yData[i] = ToolsHelper.Calculator.Calculate(compiledExpression, values);
                    values.Clear();
                    tmp += delta;
                }
                return yData;
            }
            catch (MathProcessorException ex)
            {
                System.Windows.MessageBox.Show(String.Format("Error: {0}", ex.Message));
            }
            catch (ArgumentException)
            {
                System.Windows.MessageBox.Show("Error in input data.");
            }
            catch (ArithmeticException ex)
            {
                System.Windows.MessageBox.Show(String.Format("Error: {0}", ex.Message));
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Unexpected exception.");
                throw;
            }
            isError = true;
            return null;
        }
    }
}
