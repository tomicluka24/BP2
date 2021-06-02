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
    /// Interaction logic for KosarkasView.xaml
    /// </summary>
    public partial class KlubView : UserControl
    {
        KosarkaDbContainerContext _context;
        CollectionViewSource klubViewSource;


        public KlubView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                klubViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["klubViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new KosarkaDbContainerContext();
                _context.Klubs.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                klubViewSource.Source = _context.Klubs.Local;
            }
        }

        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            Klub klub = new Klub();

            if (klub != null)
            {
                try
                {
                    klub.ImeKluba = imeKlubaTextBox.Text;
                    klub.VlasnikKluba = vlasnikKlubaTextBox.Text;
                    klub.TrenerKluba = trenerKlubaTextBox.Text;
                    klub.NavijaciKluba = navijaciKlubaTextBox.Text;
                    _context.Klubs.Add(klub);
                    _context.SaveChanges();
                    _context.Klubs.Load();

                    klubViewSource.Source = _context.Klubs.Local;
                    klubViewSource.View.Refresh();

                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri dodavanju kluba.", "Error");
                    return;
                }

            }

        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = klubViewSource.View.CurrentItem as Klub;

            var klub = (from c in _context.Klubs
                        where c.IdKluba == current.IdKluba
                        select c).FirstOrDefault();

            if (klub != null)
            {
                try
                {
                    _context.Klubs.Remove(klub);
                    _context.SaveChanges();
                    _context.Klubs.Load();

                    klubViewSource.Source = _context.Klubs.Local;
                    klubViewSource.View.Refresh();

                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri brisanju kluba.", "Error");
                    return;
                }

            }

        }

        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = klubViewSource.View.CurrentItem as Klub;

            var klub = (from c in _context.Klubs
                        where c.IdKluba == current.IdKluba
                        select c).FirstOrDefault();

            if (klub != null)
            {
                try
                {
                    if(!imeKlubaTextBox.Text.Equals(""))
                    {
                        klub.ImeKluba = imeKlubaTextBox.Text;
                    }
                    if(!vlasnikKlubaTextBox.Text.Equals(""))
                    {
                        klub.VlasnikKluba = vlasnikKlubaTextBox.Text;
                    }
                    if(!trenerKlubaTextBox.Text.Equals(""))
                    {
                        klub.TrenerKluba = trenerKlubaTextBox.Text;
                    }
                    if(!navijaciKlubaTextBox.Text.Equals(""))
                    {
                        klub.NavijaciKluba = navijaciKlubaTextBox.Text;
                    }

                    _context.SaveChanges();
                    _context.Klubs.Load();

                    klubViewSource.Source = _context.Klubs.Local;
                    klubViewSource.View.Refresh();

                }
                catch (Exception)
                {

                    MessageBox.Show("Greska pri azuriranju kluba.", "Error");
                    return;
                }

            }

        }

    }
}

