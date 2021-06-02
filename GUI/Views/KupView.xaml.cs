using DatabaseModel;
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
        //CollectionViewSource takmicenjeViewSource;

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

                // Load is an extension method on IQueryable,
                // defined in the System.Data.Entity namespace.
                // This method enumerates the results of the query,
                // similar to ToList but without creating a list.
                // When used with Linq to Entities this method
                // creates entity objects and adds them to the context.
                _context = new KosarkaDbContainerContext();
                _context.Takmicenjes.Load();


                //SqlConnection conn = new SqlConnection("connectionString");
                //SqlCommand command = new SqlCommand("SELECT * FROM dbo.Takmicenjes_Kup", conn);
                //conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ToString();
               // SqlCommand comm = new SqlCommand("SELECT * FROM dbo.Takmicenjes_Kup");
                //DataSet ds = new DataSet();
                //SqlDataAdapter da = new SqlDataAdapter(command);
                //da.Fill(ds);


                // After the data is loaded call the DbSet<T>.Local property
                // to use the DbSet<T> as a binding source.
                kupViewSource.Source = _context.Takmicenjes.Local;
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
                    takmicenje.NazivTakmicenja = nazivTakmicenjaTextBox.Text;
                    takmicenje.MestoTakmicenja = mestoTakmicenjaTextBox.Text;

                    kup.BrojDanaTrajanjaKupa = Convert.ToInt32(brojDanaTrajanjaKupaTextBox.Text);

                    // insert into takmicenja
                    _context.Takmicenjes.Add(takmicenje);
                    _context.SaveChanges();
                    _context.Takmicenjes.Load();

                    kupViewSource.Source = _context.Takmicenjes.Local;
                    kupViewSource.View.Refresh();

                    // insert into kupovi
                    
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

                    kupViewSource.Source = _context.Takmicenjes.Local;
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

                    kupViewSource.Source = _context.Takmicenjes.Local;
                    kupViewSource.View.Refresh();
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
