using System.Collections.Generic;

namespace Charting
{
    class ChartingModel
    {
        private List<ChartingObject> all = new List<ChartingObject>();

        public List<ChartingObject> getObjects()
        {
            return all;
        }

        public void setObjects(List<ChartingObject> newList)
        {
            all = newList;
        }

        public ChartingObject getObject(int index)
        {
            return all[index];
        }

        public void setObject(int id, string newName, string newFunction, int newStep,
                                    double newMin, double newMax)
        {
            all[id] = new ChartingObject(id, newName, newFunction, newStep, newMin, newMax);
        }

        public void addObject(string name, string function, int step,
                                    double min, double max)
        {
            all.Add(new ChartingObject(all.Count, name, function, step, min, max));
        }

        public void removeObject(int id) 
        {
            all.RemoveAt(id-1);
        }
    }
}
