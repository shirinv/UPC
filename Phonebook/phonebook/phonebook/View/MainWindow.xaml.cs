using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections;
using Microsoft.Win32;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace phonebook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window,  INotifyPropertyChanged
    {
        string uri = Directory.GetCurrentDirectory() + "\\templateExcel.xlsx";
        Microsoft.Office.Interop.Excel.Application excelA = new Microsoft.Office.Interop.Excel.Application();
        pbDB db;
        public ObservableCollection<Category> categorya;
        public ObservableCollection<Address> addressa;
        private IEnumerable myDataSource;

        //public ObservableCollection<Phone> phone;
        public MainWindow()
        {
            InitializeComponent();
            categorya = new ObservableCollection<Category>();
            addressa = new ObservableCollection<Address>();
            cmboxcity.ItemsSource = categorya;
            cmboxcitypred.ItemsSource = categorya;
          
             db = new pbDB();
            db.Abonent.Load();
        
            db.Company.Load();
            db.Category.Load();
            db.Address.Load();
            var categories = db.Category.ToArray();
            foreach (var item in categories)
            {
                categorya.Add(item);
            }
            var adresses = db.Address.ToArray();
            foreach (var item in adresses)
            {
                addressa.Add(item);
            }
            
            this.dataGrid1.ItemsSource = db.Abonent.Local.ToBindingList();
            this.dataGrid1pred.ItemsSource = db.Company.Local.ToBindingList();
        }

       
       

       

       

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb.SelectedItem == abonentos)
                {

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "XLSX files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        Workbook excelB = excelA.Workbooks.Add(uri);
                        Worksheet excelS = (Worksheet)excelB.Sheets[1];
                        excelS.Columns.AutoFit();

                        for (int j = 0; j < dataGrid1.Columns.Count; j++)
                        {
                            Range myRange = (Range)excelS.Cells[1, j + 1];
                            excelS.Cells[1, j + 1].Font.Bold = true;
                            excelS.Columns[j + 1].ColumnWidth = 35;
                            myRange.Value2 = dataGrid1.Columns[j].Header;
                        }
                        for (int i = 0; i < dataGrid1.Columns.Count; i++)
                        {
                            for (int j = 0; j < dataGrid1.Items.Count; j++)
                            {
                                TextBlock b = dataGrid1.Columns[i].GetCellContent(dataGrid1.Items[j]) as TextBlock;
                                Range myRange = (Range)excelS.Cells[j + 2, i + 1];
                                myRange.Value2 = b.Text;
                            }
                        }
                        excelB.SaveAs(saveFileDialog.FileName);
                        excelA.Quit();
                    }
                }
                else if (tb.SelectedItem == pred)
                {

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "XLSX files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        Workbook excelB = excelA.Workbooks.Add(uri);
                        Worksheet excelS = (Worksheet)excelB.Sheets[1];
                        excelS.Columns.AutoFit();

                        for (int j = 0; j < dataGrid1pred.Columns.Count; j++)
                        {
                            Range myRange = (Range)excelS.Cells[1, j + 1];
                            excelS.Cells[1, j + 1].Font.Bold = true;
                            excelS.Columns[j + 1].ColumnWidth = 35;
                            myRange.Value2 = dataGrid1pred.Columns[j].Header;
                        }
                        for (int i = 0; i < dataGrid1pred.Columns.Count; i++)
                        {
                            for (int j = 0; j < dataGrid1pred.Items.Count; j++)
                            {
                                TextBlock b = dataGrid1pred.Columns[i].GetCellContent(dataGrid1pred.Items[j]) as TextBlock;
                                Range myRange = (Range)excelS.Cells[j + 2, i + 1];
                                myRange.Value2 = b.Text;
                            }
                        }
                        excelB.SaveAs(saveFileDialog.FileName);
                        excelA.Quit();
                    }
                }
               
            }
            catch
            {

            }

        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();



        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

            if (tb.SelectedItem == abonentos)
            {

                Abonent objToAdd;

                objToAdd = dataGrid1.SelectedItem as Abonent;



                var conn = db.Abonent.Where(c => c.Id_abonent == objToAdd.Id_abonent).FirstOrDefault();
                if (conn == null)
                {
                    db.Abonent.Add(objToAdd);
                }
                else
                {
                    conn.Id_abonent = objToAdd.Id_abonent;
                    conn.surname = objToAdd.surname;
                    conn.name = objToAdd.name;
                    conn.phone = objToAdd.phone;
                    conn.otchestvo = objToAdd.otchestvo;
                    conn.Id_address = objToAdd.Id_address;
                    db.Entry(conn).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
                MessageBox.Show("Изменения произведены");
            }
            else if (tb.SelectedItem == pred)
            {
                Company objToAdd2;
                objToAdd2 = dataGrid1pred.SelectedItem as Company;
                var conn2 = db.Company.Where(c => c.Id_company == objToAdd2.Id_company).FirstOrDefault();
                if (conn2 == null)
                {

                    conn2.Id_company = objToAdd2.Id_company;
                    conn2.name_company = objToAdd2.name_company;
                    conn2.department = objToAdd2.department;
                    conn2.phone = objToAdd2.phone;
                    conn2.Id_address = objToAdd2.Id_address;
                    db.Entry(conn2).State = System.Data.Entity.EntityState.Modified;


                    db.SaveChanges();
                    MessageBox.Show("Изменения произведены");
                }
            }
        }
            

             

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            db.Configuration.ProxyCreationEnabled = false;
            comboBox1.Items.Clear();

            comboBox1.Items.Add("фамилия");
            comboBox1.Items.Add("имя");
            comboBox1.Items.Add("отчество");
            comboBox1.Items.Add("населенный пункт");
            comboBox1.Items.Add("адрес");
            comboBox1.Items.Add("поселок");
            comboBox1.Items.Add("город");
            comboBox1.Items.Add("село");

        }
 
      
 
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
          //  UpdateDB();
        }
 
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try { 
            if (tb.SelectedItem == abonentos)
            {
              

                if (dataGrid1.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dataGrid1.SelectedItems.Count; i++)
                    {

                        Abonent abonent = dataGrid1.SelectedItems[i] as Abonent;

                        if (abonent != null)
                        {

                            db.Abonent.Remove(abonent);

                        }
                    }
                }

                db.SaveChanges();
               
            }
            else if (tb.SelectedItem == pred)
            {
                
                }

                if (dataGrid1pred.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dataGrid1pred.SelectedItems.Count; i++)
                    {

                        Company company = dataGrid1pred.SelectedItems[i] as Company;

                        if (company != null)
                        {

                            db.Company.Remove(company);

                        }
                    }
                }

                db.SaveChanges();
         
            }

            catch { }
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {



            Abonent abonent = new Abonent();
            Address ad = new Address();
            Category cat = new Category();
           
            Company company = new Company();
            try {
                
               
                if (tb.SelectedItem == abonentos)
                {
                   
                    ad.address1 = textBox8.Text;

                    abonent.surname = textBox1.Text;
                    abonent.name = textBox2.Text;
                    abonent.otchestvo = otchestvo.Text;
                    abonent.Id_address = ad.Id_address;
                    abonent.phone = tel.Text;
                    ad.address1 = textBox8.Text;
                    abonent.surname = textBox1.Text;
                    abonent.name = textBox2.Text;
                    abonent.otchestvo = otchestvo.Text;
                    abonent.Id_address = ad.Id_address;
                  
                
                
                    ad.Id_category = (cmboxcity.SelectedValue as Category).Id_category;
                    db.Address.Add(ad);
                    db.Abonent.Add(abonent);
             
                    db.SaveChanges();
                }
                else if (tb.SelectedItem == pred)
                {
                   
                    ad.address1 = textBox8.Text;

                    company.name_company = textBox1pred.Text;
                    company.department= textBox2preddep.Text;
                    company.Id_address = ad.Id_address;
                    company.phone = textBox2pred.Text;
                    ad.address1 = textBox8pred.Text;
                
                    company.Id_address = ad.Id_address;
                 

                   
                    ad.Id_category = (cmboxcitypred.SelectedValue as Category).Id_category;
                    db.Address.Add(ad);
                    db.Company.Add(company);
               
                    db.SaveChanges();
                }
               


                MessageBox.Show("Новый объект добавлен");
            }
            catch { }


        }

    

       

        private void Window_Closed(object sender, EventArgs e)
        {
        
            db.Dispose();
        }
        public event PropertyChangedEventHandler PropertyChanged;

    
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Window1 mw = new Window1();
            mw.Show();
            this.Close();
            
        }

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            comboBox1.Items.Clear();
      
            comboBox1.Items.Add("название");
            comboBox1.Items.Add("отдел");
            comboBox1.Items.Add("населенный пункт");
        }

        private void abonent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            comboBox1.Items.Clear();
           
            comboBox1.Items.Add("фамилия");
            comboBox1.Items.Add("имя");
            comboBox1.Items.Add("отчество");
            comboBox1.Items.Add("населенный пункт");
            comboBox1.Items.Add("адрес");
            comboBox1.Items.Add("поселок");
            comboBox1.Items.Add("город");
            comboBox1.Items.Add("село");
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {

            Filtr();


        }
       
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { 
                textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1);
            textBox2.Text = char.ToUpper(textBox2.Text[0]) + textBox2.Text.Substring(1);
            textBox3.Text = char.ToUpper(textBox3.Text[0]) + textBox3.Text.Substring(1);
            textBox8.Text= char.ToUpper(textBox8.Text[0]) + textBox8.Text.Substring(1);
            otchestvo.Text = char.ToUpper(otchestvo.Text[0]) + otchestvo.Text.Substring(1);
            }
            catch { }


            if (string.IsNullOrEmpty(this.textBox3.Text))
            {
                this.dataGrid1.ItemsSource = db.Abonent.Local.ToBindingList();
            }
            else
            {
                Filtr();
            }
        }
        public void Filtr()
        {
          
            if (comboBox1.SelectedIndex == 0)
            {
                var filteredData = db.Abonent.Local
                                     .Where(x => x.surname.Contains(this.textBox3.Text));
                this.dataGrid1.ItemsSource = filteredData;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                var filteredData = db.Abonent.Local
                                 .Where(x => x.name.Contains(this.textBox3.Text));
                this.dataGrid1.ItemsSource = filteredData;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                var filteredData = db.Abonent.Local
                                 .Where(x => x.otchestvo.Contains(this.textBox3.Text));
                this.dataGrid1.ItemsSource = filteredData;
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                var filteredData = db.Abonent.Local
                                 .Where(x => x.Address.Category.location.Contains(this.textBox3.Text));
                this.dataGrid1.ItemsSource = filteredData;
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                var filteredData = db.Abonent.Local
                                 .Where(x => x.Address.address1.Contains(this.textBox3.Text));
                this.dataGrid1.ItemsSource = filteredData;
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                var filteredData = db.Abonent.Local
                                 .Where(x => x.Address.Category.category1== "поселок");
                this.dataGrid1.ItemsSource = filteredData;
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                var filteredData = db.Abonent.Local
                                .Where(x => x.Address.Category.category1 == "город");
                this.dataGrid1.ItemsSource = filteredData;
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                var filteredData = db.Abonent.Local
                                .Where(x => x.Address.Category.category1 == "село");
                this.dataGrid1.ItemsSource = filteredData;
            }
        }
        private void cancalButton_Click(object sender, RoutedEventArgs e)
        {
            if (tb.SelectedItem == abonentos)
            {

                this.dataGrid1.ItemsSource = db.Abonent.Local.ToBindingList();
                textBox1.Text = "";
                tel.Text = "";
                textBox2.Text = "";
                otchestvo.Text = "";
                textBox3.Text = "";
                textBox8.Text = "";
                comboBox1.Text = "";
                cmboxcity.Text = "";
            } else if (tb.SelectedItem==pred)
            {
                textBox1pred.Text = "";
                textBox2preddep.Text = "";
                textBox2pred.Text = "";
                cmboxcitypred.Text = "";
                textBox8pred.Text = "";

                this.dataGrid1pred.ItemsSource = db.Abonent.Local.ToBindingList();

            }
        }
    }
}
