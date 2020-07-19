using System.Collections.Generic;

namespace WpfLogin
{
    public interface IRepository
    {
        IEnumerable<Person> Persons { get; }

     
        Repository Repository
        {
            get;
            set;
        }

        Person Person
        {
            get;
            set;
        }

        Personalizer Personalizer
        {
            get;
            set;
        }
    
        void Add(Person person);
    }
}