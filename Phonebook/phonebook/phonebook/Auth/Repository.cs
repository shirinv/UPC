using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace WpfLogin
{
    public class Repository : IRepository
    {
        private const string Filename = "rep.xml";

        static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Person[]));

        public IEnumerable<Person> Persons
        {
            get
            {
                return DeserializePersons();
            }
        }

        private static IEnumerable<Person> DeserializePersons()
        {
            try
            {
                using (var fileStream = new FileStream(Filename, FileMode.Open))
                {
                    return (IEnumerable<Person>) Serializer.Deserialize(fileStream);
                }
            }
            catch
            {
                return Enumerable.Empty<Person>();
            }
        }
        
        public void Add(Person person)
        {
            var persons = DeserializePersons().ToList();
            persons.Add(person);
            using (var fileStream = new FileStream(Filename, FileMode.Create))
            {
                Serializer.Serialize(fileStream, persons.ToArray());
            }
        }


   

        Repository IRepository.Repository
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public Person Person
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public Personalizer Personalizer
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
    }
}