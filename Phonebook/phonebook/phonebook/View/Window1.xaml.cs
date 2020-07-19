using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfLogin;
using MahApps.Metro.Controls.Dialogs;

namespace phonebook
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly Personalizer personalizer = new Personalizer(new Repository());
    
        public Window1()
        {
            InitializeComponent();
           
        }
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            bool successLogin = personalizer.Login(LoginTb.Text, PasswordTb.Text);
            MessageBox.Show(successLogin ? "Вы успешно вошли" : "Ошибка при входе");
            if (personalizer.Login(LoginTb.Text, PasswordTb.Text))
            {
                MainWindow mw = new MainWindow();
             
                mw.labelka.Content = LoginTb.Text.ToString();
                mw.Show();
             
        
                this.Close();
               
            }
              
      
            
           
           
           
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            bool successRegister = personalizer.Register(LoginTb.Text, PasswordTb.Text);
            MessageBox.Show(successRegister ? "Вы успешно зарегестрировались" : "Пользовтель с таким логином уже существует");
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
      
    }
}
