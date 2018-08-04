using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BolSpellCooldownTracker
{
    public class Timer
    {
        private Button Delete;
        private TextBox SpellName;
        private TextBox CoolDown;
        private TextBox CountDown;
        private Button Cast;
        private Grid Holder;
        public bool Left;
        private DateTime TimeCasted;
        private TimeSpan CooldownTime;
        private DispatcherTimer DTimer;

        public Timer(int x, bool left)
        {
            Left = left;
            CreateGrid(x);

            DTimer = new DispatcherTimer();

        }

        public Grid GetGrid()
        {
            return Holder;
        }

        private void Loop(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var timeDifference = CooldownTime + (TimeCasted - now);
            CountDown.Text = "-" + timeDifference.Hours + ":" + timeDifference.Minutes + ":" + timeDifference.Seconds;
            if(timeDifference < TimeSpan.Zero)
            {
                Holder.Background = Brushes.White;
                CountDown.Text = "Ready";
                DTimer.Stop();

            }
        }

        public void CastClick(object sender, RoutedEventArgs e)
        {
            if (!DTimer.IsEnabled)
            {
                try
                {
                    var cooldownText = int.Parse(CoolDown.Text);
                    CooldownTime = TimeSpan.FromMinutes(cooldownText);
                    TimeCasted = DateTime.Now;
                    Holder.Background = Brushes.Black;
                    DTimer.Interval = TimeSpan.FromMilliseconds(100);
                    DTimer.Tick += new EventHandler(Loop);
                    DTimer.Start();
                }
                catch (Exception)
                {
                    CountDown.Text = "Not a number";
                }
            }
        }

        public void DeleteClicked(object sender, RoutedEventArgs e)
        {
            MainWindow.RemoveTimer(this);
            DTimer.Stop();
        }

        public void UpdatePos(int x)
        {
            Holder.Margin = new System.Windows.Thickness(0, 50 * (x + 1), 0, 0);
        }

        private void CreateGrid(int x)
        {
            Holder = new Grid();
            Holder.Height = 50;
            Holder.Width = 365;
            Holder.Margin = new System.Windows.Thickness(0, 50 * (x + 1), 0, 0);
            Holder.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            Holder.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            if (!Left)
                Holder.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;


            Delete = new Button();
            Delete.Height = 20;
            Delete.Width = 50;
            Delete.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            Delete.Margin = new System.Windows.Thickness(10, 0, 0, 0);
            Delete.Content = "Delete";
            Delete.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteClicked));

            SpellName = new TextBox();
            SpellName.Height = 15;
            SpellName.Width = 75;
            SpellName.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            SpellName.Margin = new System.Windows.Thickness(65, 0, 0, 0);
            SpellName.Text = "SpellName";

            CoolDown = new TextBox();
            CoolDown.Height = 15;
            CoolDown.Width = 75;
            CoolDown.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            CoolDown.Margin = new System.Windows.Thickness(145, 0, 0, 0);
            CoolDown.Text = "CoolDown";

            CountDown = new TextBox();
            CountDown.Height = 15;
            CountDown.Width = 75;
            CountDown.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            CountDown.Margin = new System.Windows.Thickness(225, 0, 0, 0);
            CountDown.Text = "Count Down";

            Cast = new Button();
            Cast.Height = 20;
            Cast.Width = 50;
            Cast.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            Cast.Margin = new System.Windows.Thickness(305, 0, 0, 0);
            Cast.Content = "Cast";
            Cast.AddHandler(Button.ClickEvent, new RoutedEventHandler(CastClick));

            Holder.Children.Add(Delete);
            Holder.Children.Add(SpellName);
            Holder.Children.Add(CoolDown);
            Holder.Children.Add(CountDown);
            Holder.Children.Add(Cast);


        } 
    }
}
