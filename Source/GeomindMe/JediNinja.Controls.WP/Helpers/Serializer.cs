using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace GeoLocationPicker.Helpers
{
    public class Serializer<T>  where T : class
    {
        public static T DeserializeFromXMLString(string xmlString)
        {

            T t;
            StringReader stringReader = new StringReader(xmlString);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlReader xmlReader = XmlReader.Create(stringReader);


            //serializing
            try
            {
                t = (T)serializer.Deserialize(xmlReader);
                return t;
            }
            finally
            {
                xmlReader.Close();
                stringReader.Close();
            }
        }
    }
}
