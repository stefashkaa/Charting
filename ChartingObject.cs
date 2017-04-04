using System.Xml.Serialization;

namespace Charting
{
    public class ChartingObject
    {
        [XmlElement("Id")]
        private int id;
        [XmlElement("Name")]
        private string name;
        [XmlElement("Step")]
        private int step;
        [XmlElement("Min")]
        private double min;
        [XmlElement("Max")]
        private double max;
        [XmlElement("Function")]
        private string function;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public int Step
        {
            get { return step; }
            set { step = value; }
        }        

        public double Min
        {
            get { return min; }
            set { min = value; }
        }
        
        public double Max
        {
            get { return max; }
            set { max = value; }
        }
        
        public string Function
        {
            get { return function; }
            set { function = value; }
        }

        public ChartingObject() { }

        public ChartingObject(int id, string name, string function, int step, double min, double max)
        {
            this.id = id;
            this.name = name;
            this.function = function;
            this.step = step;
            this.min = min;
            this.max = max;
        }
    }
}
