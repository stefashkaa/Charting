using System.Collections.Generic;
using System.Xml.Serialization;

namespace Charting
{
    public class ChartingModel
    {
        [XmlArray("ChartingsList"), XmlArrayItem("Charting")]
        private List<ChartingObject> objects;

        public List<ChartingObject> Objects
        {
            get { return objects; }
        }

        public ChartingModel() 
        {
            objects = new List<ChartingObject>();
        }

        public ChartingObject getObject(int index)
        {
            return objects[index];
        }

        public void setObject(int id, string newName, string newFunction, int newStep,
                                    double newMin, double newMax)
        {
            objects[id] = new ChartingObject(id, newName, newFunction, newStep, newMin, newMax);
        }

        public void addObject(string name, string function, int step,
                                    double min, double max)
        {
            objects.Add(new ChartingObject(Objects.Count, name, function, step, min, max));
        }

        public void removeObject(int id) 
        {
            objects.RemoveAt(id);
        }

        public void clearObjects()
        {
            objects.Clear();
        }
    }
}
