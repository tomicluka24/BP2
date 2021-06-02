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
    public partial class KosarkasView : UserControl
    {
        KosarkaDbContainerContext _context;
        CollectionViewSource kosarkasViewSource;

        public KosarkasView()
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

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new KosarkaDbContainerContext();
                _context.Kosarkas.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                kosarkasViewSource.Source = _context.Kosarkas.Local;
            }
        }

        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            Kosarkas kosarkas = new Kosarkas();

            if (kosarkas != null)
            {
                try
                {
                    kosarkas.ImeKosarkasa = imeKosarkasaTextBox.Text;
                    kosarkas.PrezimeKosarkasa = imeKosarkasaTextBox.Text;
                    kosarkas.BrojDresaKosarkasa = Convert.ToInt32(brojDresaKosarkasaTextBox.Text);
                    kosarkas.Klub_IdKluba = Convert.ToInt32(idKlubaKosarkasaTextBox.Text); 

                    _context.Kosarkas.Add(kosarkas);
                    _context.SaveChanges();
                    _context.Kosarkas.Load();

                    kosarkasViewSource.Source = _context.Kosarkas.Local;
                    kosarkasViewSource.View.Refresh();

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
            var current = kosarkasViewSource.View.CurrentItem as Kosarkas;

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

                    kosarkasViewSource.Source = _context.Kosarkas.Local;
                    kosarkasViewSource.View.Refresh();


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
            var current = kosarkasViewSource.View.CurrentItem as Kosarkas;

            var kosarkas = (from c in _context.Kosarkas
                            where c.IdKosarkasa == current.IdKosarkasa
                            select c).FirstOrDefault();

            if (kosarkas != null)
            {
                try
                {
                    kosarkas.ImeKosarkasa = imeKosarkasaTextBox.Text;
                    kosarkas.PrezimeKosarkasa = imeKosarkasaTextBox.Text;
                    kosarkas.BrojDresaKosarkasa = Convert.ToInt32(brojDresaKosarkasaTextBox.Text);
                    kosarkas.Klub_IdKluba = Convert.ToInt32(idKlubaKosarkasaTextBox.Text);
                    _context.SaveChanges();
                    _context.Kosarkas.Load();

                    kosarkasViewSource.Source = _context.Kosarkas.Local;
                    kosarkasViewSource.View.Refresh();


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

