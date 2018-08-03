using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using System.Data.Common;
using System.Configuration;

namespace MediaTracker
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private string provider = ConfigurationManager.AppSettings["provider"];

      private string connectionString = ConfigurationManager.AppSettings["connectionString"];

      public MainWindow()
      {
         InitializeComponent();



         //DbProviderFactory factory =
         //   DbProviderFactories.GetFactory(provider);

         //using (DbConnection connection = factory.CreateConnection())
         //{
         //   if (connection == null)
         //   {
         //      Debug.WriteLine("Connection Error");
         //   }

         //   connection.ConnectionString = connectionString;

         //   connection.Open();

         //   DbCommand command = factory.CreateCommand();

         //   if (command == null)
         //   {
         //      Debug.WriteLine("Command Error");
         //   }

         //   command.Connection = connection;
         //   command.CommandText = "Select * From Media";

         //   using (DbDataReader dataReader = command.ExecuteReader())
         //   {
         //      while (dataReader.Read())
         //      {
         //         Debug.WriteLine($"{ dataReader["Media_id"]}" + $"{dataReader["Title"]}");
         //      }
         //   }
         //}

         // Add childer elements to parent elements 
         //Button b2 = new Button();
         //b2.Name = "b2";
         //b2.Content = "Hello";
         //b2.Background = Brushes.Black;
         //b2.Foreground = Brushes.White;
         //b2.Width = 50;
         //b2.Height = 50;

         //NewItemGrid.Children.Add(b2);
      }

      private void AddMedia_Click(object sender, RoutedEventArgs e)
      {

         DbProviderFactory factory =
            DbProviderFactories.GetFactory(provider);

         using (DbConnection connection = factory.CreateConnection())
         {
            if (connection == null)
            {
               Debug.WriteLine("Connection Error");
            }

            connection.ConnectionString = connectionString;

            connection.Open();

            DbCommand command = factory.CreateCommand();

            if (command == null)
            {
               Debug.WriteLine("Command Error");
            }

            command.Connection = connection;
            command.CommandText = "Select * From Media";

            using (DbDataReader dataReader = command.ExecuteReader())
            {
               while (dataReader.Read())
               {
                  text1.Text += ($"\n{ dataReader["Media_id"]}" + $"{dataReader["Title"]} " + $"{dataReader["Status"]}");

                  TableRow tableR = new TableRow();
                  group.Rows.Add(tableR);
                  tableR.Cells.Add(new TableCell(new Paragraph(new Run($"{dataReader["Media_id"]}"))));
                  tableR.Cells.Add(new TableCell(new Paragraph(new Run($"{dataReader["Title"]}"))));

               }

            }
         }
      }

      private void menuExit_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }


      private void new_window_Click(object sender, RoutedEventArgs e)
      {
         NewItem media = new NewItem();
         media.ShowDialog();
      }
   }
}
