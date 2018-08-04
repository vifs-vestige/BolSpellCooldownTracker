using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace BolSpellCooldownTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Timer> Timers;
        public static bool Update = false;

        public MainWindow()
        {
            InitializeComponent();
            MainWindow.Timers = new List<Timer>();
            MainWindow.Timers.Add(new Timer(0, true));
            MainWindow.Timers.Add(new Timer(2, true));
            MainWindow.Timers.Add(new Timer(0, false));
            AddToGrid();
            UpdatePos();
            var dTimer = new DispatcherTimer();
            dTimer.Interval = TimeSpan.FromMilliseconds(100);
            dTimer.Tick += new EventHandler(Loop);
            dTimer.Start();

        }

        private void Loop(object sender, EventArgs e)
        {
            if (Update)
            {
                Update = false;
                while(Holder.Children.Count > 0)
                {
                    Holder.Children.RemoveAt(0);
                }
                AddToGrid();
                UpdatePos();
            }
        }

        public static void RemoveTimer(Timer timer)
        {
            Timers.Remove(timer);
            Update = true;
        }

        private void AddToGrid()
        {
            foreach (var item in MainWindow.Timers)
            {
                Holder.Children.Add(item.GetGrid());
            }
        }


        public void UpdatePos()
        {
            var counter = 0;
            foreach (var item in Timers.Where(x => x.Left))
            {
                item.UpdatePos(counter);
                counter++;
            }
            counter = 0;
            foreach (var item in Timers.Where(x => !x.Left))
            {
                item.UpdatePos(counter);
                counter++;
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            MainWindow.Timers.Add(new Timer(10, true));
            Update = true;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {

            MainWindow.Timers.Add(new Timer(10, false));
            Update = true;
        }
    }
}
