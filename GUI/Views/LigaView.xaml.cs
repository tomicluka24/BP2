using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for LigaView.xaml
    /// </summary>
    public partial class LigaView : UserControl
    {

        KosarkaDbContainerContext _context;
        CollectionViewSource ligaViewSource;
        public LigaView()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                ligaViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["ligaViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new KosarkaDbContainerContext();
                _context.Takmicenjes.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                ligaViewSource.Source = _context.Takmicenjes.Local;
            }
        }




        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            Takmicenje takmicenje = new Takmicenje();
            Liga liga = new Liga();

            if (takmicenje != null)
            {
                try
                {
                    takmicenje.NazivTakmicenja = nazivTakmicenjaTextBox.Text;
                    takmicenje.MestoTakmicenja = mestoTakmicenjaTextBox.Text;

                    liga.BrojKolaLige = Convert.ToInt32(brojKolaLigeTextBox.Text);
                    liga.BrojTimovaUPlayOffuLige = Convert.ToInt32(brojTimovaUPlayOffuLigeTextBox.Text);

                    // insert into takmicenja
                    _context.Takmicenjes.Add(takmicenje);
                    _context.SaveChanges();
                    _context.Takmicenjes.Load();

                    ligaViewSource.Source = _context.Takmicenjes.Local;
                    ligaViewSource.View.Refresh();

                    // insert into ligaovi

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
            var current = ligaViewSource.View.CurrentItem as Takmicenje;

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

                    ligaViewSource.Source = _context.Takmicenjes.Local;
                    ligaViewSource.View.Refresh();
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
            var current = ligaViewSource.View.CurrentItem as Takmicenje;

            var takmicenje = (from c in _context.Takmicenjes
                              where c.IdTakmicenja == current.IdTakmicenja
                              select c).FirstOrDefault();

            if (takmicenje != null)
            {
                try
                {
                    takmicenje.NazivTakmicenja = nazivTakmicenjaTextBox.Text;
                    takmicenje.MestoTakmicenja = mestoTakmicenjaTextBox.Text;
                    _context.SaveChanges();
                    _context.Takmicenjes.Load();

                    ligaViewSource.Source = _context.Takmicenjes.Local;
                    ligaViewSource.View.Refresh();
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
