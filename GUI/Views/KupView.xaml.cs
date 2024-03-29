﻿using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
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

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for KupView.xaml
    /// </summary>
    public partial class KupView : UserControl
    {
        KosarkaDbContainerContext _context;
        CollectionViewSource kupViewSource;
        CollectionViewSource takmicenjeViewSource;
        //static DbSet<Kup> Takmicenjes_Kup = _context.Set<Kup>();

        public KupView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                kupViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["kupViewSource"];
                takmicenjeViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["takmicenjeViewSource"];


                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new KosarkaDbContainerContext();
                _context.Takmicenjes.Load();
                _context.Takmicenjes_Kup.Load();


                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                kupViewSource.Source = _context.Takmicenjes_Kup.Local;
                //takmicenjeViewSource.Source = _context.Takmicenjes.Local;
            }
        }




        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            Takmicenje takmicenje = new Takmicenje();
            Kup kup = new Kup();

            if (takmicenje != null)
            {
                try
                {
                    kup.NazivTakmicenja = nazivTakmicenjaTextBox.Text;
                    kup.MestoTakmicenja = mestoTakmicenjaTextBox.Text;

                    kup.BrojDanaTrajanjaKupa = Convert.ToInt32(brojDanaTrajanjaKupaTextBox.Text);

                    // insert into takmicenja
                    _context.Takmicenjes.Add(kup);
                    // insert into kupovi
                    _context.Takmicenjes_Kup.Add(kup);
                    _context.SaveChanges();
                    _context.Takmicenjes_Kup.Load();

                    kupViewSource.Source = _context.Takmicenjes_Kup.Local;
                    kupViewSource.View.Refresh();

                   // takmicenjeViewSource.Source = _context.Takmicenjes.Local;
                    //takmicenjeViewSource.View.Refresh();
                    
                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri dodavanju takmicenje.", "Error");
                    return;
                }

            }
        }



        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = kupViewSource.View.CurrentItem as Takmicenje;

            var takmicenje = (from c in _context.Takmicenjes
                              where c.IdTakmicenja == current.IdTakmicenja
                              select c).FirstOrDefault();

            if (takmicenje != null)
            {
                try
                {
                    _context.Takmicenjes.Remove(takmicenje);
                    _context.SaveChanges();
                    _context.Takmicenjes.Load();

                    kupViewSource.Source = _context.Takmicenjes_Kup.Local;
                    kupViewSource.View.Refresh();
                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri brisanju takmicenja.", "Error");
                    return;
                }

            }


        }

        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = kupViewSource.View.CurrentItem as Takmicenje;

            var kup = (from c in _context.Takmicenjes_Kup
                              where c.IdTakmicenja == current.IdTakmicenja
                              select c).FirstOrDefault();

            if (kup != null)
            {
                try
                {
                    kup.NazivTakmicenja = nazivTakmicenjaTextBox.Text;
                    kup.MestoTakmicenja = mestoTakmicenjaTextBox.Text;

                    kup.BrojDanaTrajanjaKupa = Convert.ToInt32(brojDanaTrajanjaKupaTextBox.Text);

                    _context.SaveChanges();
                    _context.Takmicenjes.Load();

                   
                    kupViewSource.Source = _context.Takmicenjes_Kup.Local;
                    kupViewSource.View.Refresh();

                    //takmicenjeViewSource.Source = _context.Takmicenjes.Local;
                   // takmicenjeViewSource.View.Refresh();
                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri azuriranju takmicenja.", "Error");
                    return;
                }

            }
        }
    }
}
