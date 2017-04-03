using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charting
{
    public class ChartingController
    {
        private ChartingModel model;

        public ChartingController()
        {
            model = new ChartingModel();
            //create VIEW!!!!!!!
        }

        public string addFunction( string name, string function, 
                                  int step, double min, double max)
        {
            model.addObject(name, function, step, min, max);
            return "Функция добавлена!";            
        }

        public string alterFunction(int id, string newName, string newFunction, int newStep,
                                    double newMin, double newMax)
        {
            if (id > getAllChartings().Count)
                return "Не могу изменить функцию: индекс = " + id + " за пределами диапазона!";
            model.setObject(id, newName, newFunction, newStep, newMin, newMax);
            return "Функция изменена!";
        }

        public string deleteFunctionById(int id)
        {
            if (id > getAllChartings().Count)
                return "Не могу удалить функцию: индекс = "+id+" за пределами диапазона!";
            model.removeObject(id);
            return "Функция удалена!";
        }

        public List<ChartingObject> getAllChartings()
        {
            return model.getObjects();
        }
    }
}
