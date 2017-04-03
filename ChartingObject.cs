namespace Charting
{
    public class ChartingObject
    {
        private int id;
        private string name;
        private int step;
        private double min;
        private double max;
        private string function;

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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
       
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
