namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Xml.Serialization;
    using System.Runtime.Serialization;
    using System.Xml.Linq;
    using System.Xml;

    /// <summary>
    /// User class
    /// </summary>
    [Serializable]
    [DataContract]
    public class User : IXmlSerializable
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime DateOfBirth { get; set; }
        [DataMember]
        public Gender Gender { get; set; }
        [DataMember]
        public List<Records> VisaRecords { get; set; }

        /// <summary>
        /// User ctor
        /// </summary>
        public User()
        {
            VisaRecords = new List<Records>();
            DateOfBirth = DateTime.Now;
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
                hash = hash * (DateOfBirth.DayOfYear ^ 777) + Gender.GetHashCode();
                return hash;
            }
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {

            while (reader.Read())
            {

                Id = Convert.ToInt32(reader["Id"]);
                reader.ReadToFollowing("FirstName");
                FirstName = reader.ReadElementContentAsString();

                LastName = reader.ReadElementContentAsString();

                Gender = reader.ReadElementContentAsString() == "Male" ? Gender.Male : Gender.Female;

                DateOfBirth = Convert.ToDateTime(reader.GetAttribute("Birthday"));
                if (reader.ReadElementString() != null)
                {

                    VisaRecords = reader.GetAttribute("Records").Select(vr => new Records
                    {
                        Country = reader.GetAttribute("Country"),
                        Start = Convert.ToDateTime(reader.GetAttribute("Start")),
                        End = Convert.ToDateTime(reader.GetAttribute("End"))
                    }).ToList();

                }
            }
        }

        /// <summary> 
        /// Converts an object into its XML representation. 
        /// </summary> 
        /// <param name="xmlWriter"> XmlWriter instance.</param> 
        public void WriteXml(XmlWriter writer)
        {

            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteElementString("FirstName", FirstName);
            writer.WriteElementString("LastName", LastName);
            writer.WriteElementString("Gender", Gender.ToString());
            if (DateOfBirth != DateTime.MinValue)
            {
                writer.WriteElementString("Birthday",
                    DateOfBirth.ToString("yyyy-MM-dd"));
            }

            if (VisaRecords != null)
            {
                foreach (var item in VisaRecords)
                {
                    writer.WriteAttributeString("Country", item.Country);
                    writer.WriteAttributeString("VisaRecords Start", item.Start.ToString());
                    writer.WriteAttributeString("VisaRecords End", item.End.ToString());
                }
            }
            else
            {
                writer.WriteAttributeString("VisaRecords ", "none");
            }

        }
    }

    [Serializable]
    [DataContract]
    public enum Gender
    {
        [EnumMember]
        Male,
        [EnumMember]
        Female
    }

    [Serializable]
    [DataContract]
    public struct Records
    {
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public DateTime Start { get; set; }
        [DataMember]
        public DateTime End { get; set; }
    }
}
