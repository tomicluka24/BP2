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
    /// Interaction logic for PlejmejkerView.xaml
    /// </summary>
    public partial class PlejmejkerView : UserControl
    {
        KosarkaDbContainerContext _context;
        CollectionViewSource plejmejkerViewSource;
        CollectionViewSource kosarkasViewSource;

        public PlejmejkerView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                kosarkasViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["kosarkasViewSource"];
                plejmejkerViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["plejmejkerViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new KosarkaDbContainerContext();
                _context.Kosarkas.Load();
                _context.Kosarkas_Plejmejker.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                plejmejkerViewSource.Source = _context.Kosarkas_Plejmejker.Local;
            }
        }

        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            Kosarkas kosarkas = new Kosarkas();
            Plejmejker plejmejker = new Plejmejker();

            if (kosarkas != null)
            {
                try
                {
                    plejmejker.ImeKosarkasa = imeKosarkasaTextBox.Text;
                    plejmejker.PrezimeKosarkasa = imeKosarkasaTextBox.Text;
                    plejmejker.BrojDresaKosarkasa = Convert.ToInt32(brojDresaKosarkasaTextBox.Text);
                    plejmejker.Klub_IdKluba = Convert.ToInt32(idKlubaKosarkasaTextBox.Text);
                    plejmejker.ApgPlejmejkera = Convert.ToInt32(apgPlejmejkeraTextBox.Text);


                    _context.Kosarkas.Add(plejmejker);
                    _context.Kosarkas_Plejmejker.Add(plejmejker);
                    _context.SaveChanges();
                    _context.Kosarkas_Plejmejker.Load();

                    plejmejkerViewSource.Source = _context.Kosarkas_Plejmejker.Local;
                    plejmejkerViewSource.View.Refresh();

                }
                catch (Exception)
                {
                    MessageBox.Show("Greska pri dodavanju kosarkasa.", "Error");
                    return;
                }

            }

        }
        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = plejmejkerViewSource.View.CurrentItem as Kosarkas;

            var kosarkas = (from c in _context.Kosarkas
                            where c.IdKosarkasa == current.IdKosarkasa
                            select c).FirstOrDefault();

            if (kosarkas != null)
            {
                try
                {
                    _context.Kosarkas.Remove(kosarkas);
                    _context.SaveChanges();
                    _context.Kosarkas.Load();

                    plejmejkerViewSource.Source = _context.Kosarkas_Plejmejker.Local;
                    plejmejkerViewSource.View.Refresh();


                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri brisanju kosarkasa.", "Error");
                    return;
                }

            }

        }
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = plejmejkerViewSource.View.CurrentItem as Kosarkas;

            var plejmejker = (from c in _context.Kosarkas_Plejmejker
                            where c.IdKosarkasa == current.IdKosarkasa
                            select c).FirstOrDefault();

            if (plejmejker != null)
            {
                try
                {
                    plejmejker.ImeKosarkasa = imeKosarkasaTextBox.Text;
                    plejmejker.PrezimeKosarkasa = imeKosarkasaTextBox.Text;
                    plejmejker.BrojDresaKosarkasa = Convert.ToInt32(brojDresaKosarkasaTextBox.Text);
                    plejmejker.Klub_IdKluba = Convert.ToInt32(idKlubaKosarkasaTextBox.Text);
                    plejmejker.ApgPlejmejkera = Convert.ToInt32(apgPlejmejkeraTextBox.Text);

                    _context.SaveChanges();
                    _context.Kosarkas.Load();

                    plejmejkerViewSource.Source = _context.Kosarkas_Plejmejker.Local;
                    plejmejkerViewSource.View.Refresh();


                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri azuriranju kosarkasa.", "Error");
                    return;
                }

            }

        }

    }
}
