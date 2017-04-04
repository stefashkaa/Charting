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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
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
        public GraphicsElement()
        {
            InitializeComponent();
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
            //рисование функций
            drawFunction(function, step, min, max);
        }

        public void drawFunction( string function,
                               int step, double min, double max)
        {
            chart.ChartAreas.Add(new ChartArea("Default"));
            
            chart.Series.Add(new Series("Series1"));

            chart.Series["Series1"].ChartArea = "Default";
            chart.Series["Series1"].ChartType = SeriesChartType.FastLine;
            chart.Series["Series1"].BorderWidth = 2;
            double delta = (max - min) / step;
            double tmp = min;
            double[] xData = new double[step];
            double[] yData = new double[step];
            List<VariableValue> values = new List<VariableValue>();

            for (int i = 0; i < step; i++)
            {
                values.Add(new VariableValue(tmp, "x"));
                PreparedExpression preparedExpression = ToolsHelper.Parser.Parse(function);
                CompiledExpression compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
                xData[i] = tmp;
                yData[i] = ToolsHelper.Calculator.Calculate(compiledExpression, values);
                values.Clear();
                tmp += delta;
            }
           
            chart.Series["Series1"].Points.DataBindXY(xData, yData);
        }

    }
}
