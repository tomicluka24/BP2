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
    /// Interaction logic for TrofejView.xaml
    /// </summary>
    public partial class TrofejView : UserControl
    {
        KosarkaDbContainerContext _context;
        CollectionViewSource trofejViewSource;


        public TrofejView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //  Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                trofejViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["trofejViewSource"];

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new KosarkaDbContainerContext();
                _context.Trofejs.Load();

                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                trofejViewSource.Source = _context.Trofejs.Local;
            }
        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = trofejViewSource.View.CurrentItem as Trofej;

            var trofej = (from c in _context.Trofejs
                              where c.IdTrofeja == current.IdTrofeja
                              select c).FirstOrDefault();

            if (trofej != null)
            {
                try
                {
                    _context.Trofejs.Remove(trofej);
                    _context.SaveChanges();
                    _context.Trofejs.Load();

                    trofejViewSource.Source = _context.Trofejs.Local;
                    trofejViewSource.View.Refresh();


                }
                catch (Exception)
                {

                    MessageBox.Show("Trenutno nije moguce obrisati trofej.", "Error");
                    return;
                }

            }

        }

        // dopuni
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            var current = trofejViewSource.View.CurrentItem as Trofej;

            var trofej = (from c in _context.Trofejs
                              where c.IdTrofeja == current.IdTrofeja
                              select c).FirstOrDefault();

            if (trofej != null)
            {
                try
                {
                    trofej.IdTrofeja = Convert.ToInt32(idTrofejaTextBox.Text);
                    _context.SaveChanges();
                    _context.Trofejs.Load();

                    trofejViewSource.Source = _context.Trofejs.Local;
                    trofejViewSource.View.Refresh();


                }
                catch (Exception)
                {

                    MessageBox.Show("Trenutno nije moguce izmeniti trofej.", "Error");
                    return;
                }

            }

        }

        // dopuni
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            _context = new KosarkaDbContainerContext();
            Trofej trofej = new Trofej();

            if (trofej != null)
            {
                try
                {
                    trofej.IdTrofeja = Convert.ToInt32(idTrofejaTextBox.Text);
                    _context.Trofejs.Add(trofej);
                    _context.SaveChanges();
                    _context.Trofejs.Load();

                    trofejViewSource.Source = _context.Trofejs.Local;
                    trofejViewSource.View.Refresh();


                }
                catch (Exception)
                {

                    MessageBox.Show("Trenutno nije moguce dodati trofej.", "Error");
                    return;
                }

            }

        }

    }
}
