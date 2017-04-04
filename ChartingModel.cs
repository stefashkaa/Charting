using System.Collections.Generic;
using System.Xml.Serialization;

namespace Charting
{
    public class ChartingModel
    {
        [XmlArray("ChartingsList"), XmlArrayItem("Charting")]
        public List<ChartingObject> Objects { get; set; }

        public ChartingModel() 
        {
            Objects = new List<ChartingObject>();
        }

        public ChartingObject getObject(int index)
        {
            return Objects[index];
        }

        public void setObject(int id, string newName, string newFunction, int newStep,
                                    double newMin, double newMax)
        {
            Objects[id] = new ChartingObject(id, newName, newFunction, newStep, newMin, newMax);
        }

        public void addObject(string name, string function, int step,
                                    double min, double max)
        {
            Objects.Add(new ChartingObject(Objects.Count, name, function, step, min, max));
        }

        public void removeObject(int id) 
        {
            Objects.RemoveAt(id-1);
        }
    }
}
