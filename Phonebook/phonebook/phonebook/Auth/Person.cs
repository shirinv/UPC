using System.Xml.Serialization;

namespace WpfLogin
{
    [XmlType]
    public class Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}