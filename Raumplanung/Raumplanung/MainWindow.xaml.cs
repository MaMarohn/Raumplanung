using System;
using System.Windows;
using Raumplanung.Database;

namespace Raumplanung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHandler databaseHandler = new DatabaseHandler();
            Console.WriteLine(databaseHandler.GetAllRooms().Count);
            //Lösunghttp://stackoverflow.com/questions/7055962/entity-framework-4-1-invalid-column-name            
        }
    }
}
