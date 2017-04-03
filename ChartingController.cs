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

        public bool addFunction( string name, string function, 
                                  int step, double min, double max)
        {           
            model.addObject(name, function, step, min, max);
            return true;   
        }

        public bool alterFunction(int id, string newName, string newFunction, int newStep,
                                    double newMin, double newMax)
        {
            if (id > getAllChartings().Count)
                return false;//"Не могу изменить функцию: индекс = " + id + " за пределами диапазона!";
            model.setObject(id, newName, newFunction, newStep, newMin, newMax);
            return true;//"Функция изменена!";
        }

        public bool deleteFunctionById(int id)
        {
            if (id > getAllChartings().Count)
                return false;//"Не могу удалить функцию: индекс = "+id+" за пределами диапазона!";
            model.removeObject(id);
            return true;//"Функция удалена!";
        }

        public List<ChartingObject> getAllChartings()
        {
            return model.getObjects();
        }
    }
}
