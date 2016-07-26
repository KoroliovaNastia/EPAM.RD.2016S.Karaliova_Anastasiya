using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAL.Infrastructure;

namespace DAL.Entities
{
    [Serializable]
    public class User:IXmlSerializable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public List<Records> VisaRecords { get; set; }

        public User()
        {
            VisaRecords = new List<Records>();
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            var user = obj as User;
            if (user == null)
                return false;
            return Equals(user);
        }

        public bool Equals(User other)
        {
            if ((String.CompareOrdinal(FirstName, other.FirstName) == 0) &&
                (String.CompareOrdinal(LastName, other.LastName) == 0)
                && (DateOfBirth == other.DateOfBirth) && (VisaRecords.SequenceEqual(other.VisaRecords)) &&
                (Gender == other.Gender))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (FirstName != null ? FirstName.GetHashCode() : 777) +
                           (LastName != null ? LastName.GetHashCode() : 777);
                hash = hash*(DateOfBirth.DayOfYear ^ 777)+Gender.GetHashCode();
            return hash;
            }
            
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.MoveToContent();
            Id = Convert.ToInt32(reader.GetAttribute("Id"));
            FirstName = reader.GetAttribute("FirstName");
            LastName = reader.GetAttribute("LastName");
            Gender = reader.GetAttribute("Gender")=="Male"?Gender.Male:Gender.Female;
            Boolean isEmptyElement = reader.IsEmptyElement; // (1)
            reader.ReadStartElement();
            if (!isEmptyElement) // (1)
            {
                DateOfBirth = DateTime.ParseExact(reader.
                    ReadElementString("DateOfBirth"), "yyyy-MM-dd", null);

                reader.ReadEndElement();
            }
            //reader.ReadStartElement("VisaRecords");
            //reader.MoveToAttribute("count");
            //int count = int.Parse(reader.Value);

            //var otherSer = new XmlSerializer(typeof(Records));
            //for (int i = 0; i < count; i++)
            //{
            //    var other = (Records)otherSer.Deserialize(reader);
            //    Records.Add(other);
            //}

            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("FirstName", FirstName);
            writer.WriteAttributeString("LastName", LastName);
            writer.WriteAttributeString("Gender", Gender.ToString());
            if (DateOfBirth != DateTime.MinValue)
                writer.WriteElementString("Birthday",
                    DateOfBirth.ToString("yyyy-MM-dd"));
            if (VisaRecords != null)
            {
                foreach (var item in VisaRecords)
                {
                    writer.WriteAttributeString("Country", item.Country);
                    writer.WriteAttributeString("VisaRecords Start", item.Start.ToString());
                    writer.WriteAttributeString("VisaRecords End", item.End.ToString());
                }
            }
        }
    }
    [Serializable]
    public enum Gender
    {
        Male,
        Female
    }
    [Serializable]
    public struct Records
    {
        public string Country { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

}
