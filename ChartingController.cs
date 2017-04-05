using System.Collections.Generic;

namespace Charting
{
    public class ChartingController
    {
        private ChartingModel model;
        private ChartingView view;

        public ChartingController()
        {
            model = new ChartingModel();
            view = new ChartingView(this);
            view.Show();
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
                return false;
            model.setObject(id, newName, newFunction, newStep, newMin, newMax);
            return true;
        }

        public bool deleteFunctionById(int id)
        {
            if (id > getAllChartings().Count)
                return false;
            model.removeObject(id);
            return true;
        }

        public bool saveScheme(string fi)
        {
            try
            {
                System.IO.FileStream file = System.IO.File.Create(fi);
                System.Xml.Serialization.XmlSerializer writer = new
                    System.Xml.Serialization.XmlSerializer(typeof(ChartingModel));
                writer.Serialize(file, model);
                file.Close();
            }
            catch (System.Xml.XmlException)
            {
                return false;
            }
            return true;
        }

        public List<ChartingObject> getAllChartings()
        {
            return model.Objects;
        }

        public void clearObjects()
        {
            model.clearObjects();
        }

        public bool openSchema(string file)
        {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(ChartingModel));

            try
            {
                var document = System.Xml.Linq.XDocument.Load(file);

                model = (ChartingModel)xmlSerializer.Deserialize(document.CreateReader());
            }
            catch (System.Xml.XmlException)
            { 
                return false; 
            }

            return true;
        }
    }
}
