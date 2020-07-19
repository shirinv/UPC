using System.Linq;
using System.Security.Cryptography;

namespace WpfLogin
{
    public class Personalizer
    {
        private readonly IRepository repository;

        public Personalizer(IRepository repository)
        {
            this.repository = repository;
        }

        public bool Login(string login, string password)
        {
            var encodedPassword = Encode(password);
            return repository.Persons.Any(p => p.Password == encodedPassword);
        }

        public bool Register(string login, string password)
        {
            var encodedPassword = Encode(password);
            bool canRegister = repository.Persons.All(p => p.Login != login); // Не существует с таким логин
            if (canRegister)
                repository.Add(new Person {Login = login, Password = encodedPassword});
            return canRegister;
        }

        private static string Encode(string password)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(password.ToCharArray().Select(x => (byte) x).ToArray());
                return string.Join(string.Empty, hash);
            }
        }
    }
}
