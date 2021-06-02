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
    /// Interaction logic for UcestvovanjeView.xaml
    /// </summary>
    public partial class UcestvovanjeView : UserControl
    {
        KosarkaDbContainerContext _context;
        CollectionViewSource ucestvovanjeViewSource;

        public UcestvovanjeView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                ucestvovanjeViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["ucestvovanjeViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new KosarkaDbContainerContext();
                _context.Ucestvujes.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                ucestvovanjeViewSource.Source = _context.Ucestvujes.Local;
            }
        }

        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            Ucestvuje ucestvovanje = new Ucestvuje();

            if (ucestvovanje != null)
            {
                try
                {
                    ucestvovanje.Klub_IdKluba = Convert.ToInt32(idKlubaTextBox.Text);
                    ucestvovanje.Takmicenje_IdTakmicenja = Convert.ToInt32(idTakmicenjaTextBox.Text);
                    _context.Ucestvujes.Add(ucestvovanje);
                    _context.SaveChanges();
                    _context.Ucestvujes.Load();

                    ucestvovanjeViewSource.Source = _context.Ucestvujes.Local;
                    ucestvovanjeViewSource.View.Refresh();
                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri dodavanju ucestvovanja.", "Error");
                    return;
                }

            }
        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = ucestvovanjeViewSource.View.CurrentItem as Ucestvuje;

            var ucestvuje = (from c in _context.Ucestvujes
                              where c.Klub_IdKluba == current.Klub_IdKluba && c.Takmicenje_IdTakmicenja == current.Takmicenje_IdTakmicenja
                              select c).FirstOrDefault();

            if (ucestvuje != null)
            {
                try
                {
                    _context.Ucestvujes.Remove(ucestvuje);
                    _context.SaveChanges();
                    _context.Ucestvujes.Load();

                    ucestvovanjeViewSource.Source = _context.Ucestvujes.Local;
                    ucestvovanjeViewSource.View.Refresh();
                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri brisanju ucestvovanja.", "Error");
                    return;
                }

            }

        }

        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = ucestvovanjeViewSource.View.CurrentItem as Ucestvuje;

            var ucestvuje = (from c in _context.Ucestvujes
                             where c.Klub_IdKluba == current.Klub_IdKluba && c.Takmicenje_IdTakmicenja == current.Takmicenje_IdTakmicenja
                             select c).FirstOrDefault();

            if (ucestvuje != null)
            {
                try
                {
                    ucestvuje.Klub_IdKluba = Convert.ToInt32(idKlubaTextBox.Text);
                    ucestvuje.Takmicenje_IdTakmicenja = Convert.ToInt32(idTakmicenjaTextBox.Text);

                    _context.SaveChanges();
                    _context.Takmicenjes.Load();

                    ucestvovanjeViewSource.Source = _context.Ucestvujes.Local;
                    ucestvovanjeViewSource.View.Refresh();
                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri azuriranju ucestvovanja.", "Error");
                    return;
                }

            }

        }
    }
}
