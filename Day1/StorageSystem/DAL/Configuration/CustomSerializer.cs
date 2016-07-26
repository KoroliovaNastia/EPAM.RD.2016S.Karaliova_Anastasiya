using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL.Configuration
{
    public class CustomSerializer:IXmlSerializable
    {
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
