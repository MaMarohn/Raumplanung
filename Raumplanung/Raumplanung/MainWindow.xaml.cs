﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            

            


            ReservationContext db = new ReservationContext();
            //reservationContext.Rooms.Add(new Room("ser"));
            //reservationContext.SaveChanges();
            int i = db.Rooms.Count();
            Console.WriteLine(i);
            //Room r = new Room();
            //r.Name = "Raum-1";
            //reservationContext.Rooms.Add(r);

            //reservationContext.SaveChanges();
        }
    }
}
