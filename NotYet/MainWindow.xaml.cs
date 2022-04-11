using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NotYet
{
    public partial class MainWindow : Window
    {
        readonly System.Windows.Threading.DispatcherTimer Timer = new();

        public MainWindow()
        {
            this.Loaded += new RoutedEventHandler(Window_Loaded);

            Properties.Settings.Default.Day = DateTime.Now;
            if (((int)Properties.Settings.Default.Day.DayOfWeek == 0) && !Properties.Settings.Default.WeekendWork)
            {
                Properties.Settings.Default.Day = DateTime.Now.AddDays(1);
            }
            if (((int)Properties.Settings.Default.Day.DayOfWeek == 6) && !Properties.Settings.Default.WeekendWork)
            {
                Properties.Settings.Default.Day = DateTime.Now.AddDays(2);
            }
            Properties.Settings.Default.Save();

            InitializeComponent();

            SetDaySelector();
            SetDay();

            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 5;
            this.Top = desktopWorkingArea.Bottom - this.Height - 5;


        }

        private void Timer_Click(object sender, EventArgs e)
        {
            var calendar = DB.DbToClass(Properties.Settings.Default.Day, Properties.Settings.Default.Groupe);
            var currentH = TimeOnly.FromDateTime(DateTime.Now);
            var goal = calendar.GetLastHourClasses();
            if (goal > currentH)
            {
                var diff = goal - currentH;
                TimerH.Text = diff.Hours.ToString();
                TimerM.Text = diff.Minutes.ToString();
                TimerS.Text = diff.Seconds.ToString();
            }
            else
            {
                TimerH.Text = "00";
                TimerM.Text = "00";
                TimerS.Text = "00";
            }
            
        }

        private void SetDaySelector()
        {
            var refd = DateOnly.FromDateTime(Properties.Settings.Default.Day);
            var weekend = Properties.Settings.Default.WeekendWork;

            if (((int)refd.AddDays(-4).DayOfWeek == 6 || (int)refd.AddDays(-4).DayOfWeek == 0) && !weekend) {
                dayselectm4.IsEnabled = false;
                dayselectm4.FontWeight = FontWeights.ExtraBold;
            } else {
                dayselectm4.IsEnabled = true;
                dayselectm4.FontWeight = FontWeights.Normal;
            }
            dayselectm4.Content = refd.AddDays(-4).Day.ToString();
            dayselectm4.Tag = refd.AddDays(-4).ToString("yyyy-MM-dd");

            if (((int)refd.AddDays(-3).DayOfWeek == 6 || (int)refd.AddDays(-3).DayOfWeek == 0) && !weekend) {
                dayselectm3.IsEnabled = false;
                dayselectm3.FontWeight = FontWeights.ExtraBold;
            }
            else {
                dayselectm3.IsEnabled = true;
                dayselectm3.FontWeight = FontWeights.Normal;
            }
            dayselectm3.Content = refd.AddDays(-3).Day.ToString();
            dayselectm3.Tag = refd.AddDays(-3).ToString("yyyy-MM-dd");

            if (((int)refd.AddDays(-2).DayOfWeek == 6 || (int)refd.AddDays(-2).DayOfWeek == 0) && !weekend) {
                dayselectm2.IsEnabled = false;
                dayselectm2.FontWeight = FontWeights.ExtraBold;
            } else {
                dayselectm2.IsEnabled = true;
                dayselectm2.FontWeight = FontWeights.Normal;
            }
            dayselectm2.Content = refd.AddDays(-2).Day.ToString();
            dayselectm2.Tag = refd.AddDays(-2).ToString("yyyy-MM-dd");

            if (((int)refd.AddDays(-1).DayOfWeek == 6 || (int)refd.AddDays(-1).DayOfWeek == 0) && !weekend) {
                dayselectm1.IsEnabled = false;
                dayselectm1.FontWeight = FontWeights.ExtraBold;
            } else {
                dayselectm1.IsEnabled = true;
                dayselectm1.FontWeight = FontWeights.Normal;
            }
            dayselectm1.Content = refd.AddDays(-1).Day.ToString();
            dayselectm1.Tag = refd.AddDays(-1).ToString("yyyy-MM-dd");

            if (((int)refd.DayOfWeek == 6 || (int)refd.DayOfWeek == 0) && !weekend) {
                dayselect0.IsEnabled = false;
                dayselect0.FontWeight = FontWeights.ExtraBold;
            }
            else {
                dayselect0.IsEnabled = true;
                dayselect0.FontWeight = FontWeights.Normal;
            }
            dayselect0.Content = refd.Day.ToString();
            dayselect0.Tag = refd.ToString("yyyy-MM-dd");

            if (((int)refd.AddDays(1).DayOfWeek == 6 || (int)refd.AddDays(1).DayOfWeek == 0) && !weekend) {
                dayselect1.IsEnabled = false;
                dayselect1.FontWeight = FontWeights.ExtraBold;
            } else {
                dayselect1.IsEnabled = true;
                dayselect1.FontWeight = FontWeights.Normal;
            }
            dayselect1.Content = refd.AddDays(1).Day.ToString();
            dayselect1.Tag = refd.AddDays(1).ToString("yyyy-MM-dd");

            if (((int)refd.AddDays(2).DayOfWeek == 6 || (int)refd.AddDays(2).DayOfWeek == 0) && !weekend) {
                dayselect2.IsEnabled = false;
                dayselect2.FontWeight = FontWeights.ExtraBold;
            } else {
                dayselect2.IsEnabled = true;
                dayselect2.FontWeight = FontWeights.Normal;
            }
            dayselect2.Content = refd.AddDays(2).Day.ToString();
            dayselect2.Tag = refd.AddDays(2).ToString("yyyy-MM-dd");

            if (((int)refd.AddDays(3).DayOfWeek == 6 || (int)refd.AddDays(3).DayOfWeek == 0) && !weekend) {
                dayselect3.IsEnabled = false;
                dayselect3.FontWeight = FontWeights.ExtraBold;
            } else {
                dayselect3.IsEnabled = true;
                dayselect3.FontWeight = FontWeights.Normal;
            }
            dayselect3.Content = refd.AddDays(3).Day.ToString();
            dayselect3.Tag = refd.AddDays(3).ToString("yyyy-MM-dd");

            if (((int)refd.AddDays(4).DayOfWeek == 6 || (int)refd.AddDays(4).DayOfWeek == 0) && !weekend) {
                dayselect4.IsEnabled = false;
                dayselect4.FontWeight = FontWeights.ExtraBold;
            }
            else {
                dayselect4.IsEnabled = true;
                dayselect4.FontWeight = FontWeights.Normal;
            }
            dayselect4.Content = refd.AddDays(4).Day.ToString();
            dayselect4.Tag = refd.AddDays(4).ToString("yyyy-MM-dd");
        }

        private void SelectorChangeDay(object sender, RoutedEventArgs e)
        {
            if (sender is Button rb)
            {
                string? day = rb.Tag.ToString();
                if (day is not null)
                {
                    Properties.Settings.Default.Day = DateTime.Parse(day);
                    Properties.Settings.Default.Save();

                    SetDay();
                }
            }
        }

        private void SetlistClasses()
        {
            listClasses.ItemsSource = null;
            var data = new CalendarDayInterface(Properties.Settings.Default.Day, Properties.Settings.Default.Groupe);
            if (data.Count == 0)
            {
                NoClassesText.Visibility = Visibility.Visible;
            }
            else
            {
                NoClassesText.Visibility = Visibility.Hidden;
                listClasses.ItemsSource = data;
            }
        }

        private async void SetDay()
        {
            InternetError.Visibility = Visibility.Hidden;
            refreshText.Visibility = Visibility.Hidden;
            LastUpdateTextDate.Visibility = Visibility.Hidden;

            if (Properties.Settings.Default.Groupe == "none")
            {
                selectGp.Visibility = Visibility.Visible;
            }
            else
            {
                if (Groupe.Content.ToString() == "G R O U P E")
                {
                    Groupe.Content = Properties.Settings.Default.Groupe;
                }

                LastUpdateDate.Text = Properties.Settings.Default.LastUpdate.ToString();
                LastUpdateTextDate.Visibility = Visibility.Visible;

                if (Properties.Settings.Default.LastUpdate.Hour != DateTime.Now.Hour || DB.DbToClass(Properties.Settings.Default.Day, Properties.Settings.Default.Groupe).GetAllClasses().Count == 0)
                {
                    LastUpdateTextDate.Visibility = Visibility.Hidden;

                    refreshText.Visibility = Visibility.Visible;
                    var result = await Task.Run(()=> DB.RefreshDayData(Properties.Settings.Default.Day, Properties.Settings.Default.Groupe));
                    refreshText.Visibility = Visibility.Hidden;
                    
                    if (result == -1)
                    {
                        InternetError.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        LastUpdateDate.Text = Properties.Settings.Default.LastUpdate.ToString();
                        LastUpdateTextDate.Visibility = Visibility.Visible;
                    }
                }
                SetlistClasses();
            }
            DayText.Text = Properties.Settings.Default.Day.ToString("dddd, dd MMMM yyyy");
            SetDaySelector();
        }

        private void Button_setting(object sender, RoutedEventArgs e)
        {
            if (settingPage.Visibility == Visibility.Hidden)
            {
                timeSelector.Visibility = Visibility.Hidden;
                dayContent.Visibility = Visibility.Hidden;
                settingPage.Visibility = Visibility.Visible;
                if (DB.IsGpTableEmpty() || listGroupes.ItemsSource == null) {
                    BtnGetGroup.Visibility = Visibility.Visible;
                } else {
                    BtnGetGroup.Visibility = Visibility.Hidden;
                }
            } else
            {
                settingPage.Visibility = Visibility.Hidden;
                timeSelector.Visibility = Visibility.Visible;
                dayContent.Visibility = Visibility.Visible;
                BtnGetGroup.Visibility = Visibility.Hidden;
            }
        }

        private async void Setgroupe(object sender, RoutedEventArgs e)
        {
            if (sender is Button rb)
            {
                string? groupe = rb.Tag.ToString();
                if (groupe is not null)
                {
                    InternetError.Visibility = Visibility.Hidden;
                    refreshText.Visibility = Visibility.Hidden;
                    LastUpdateTextDate.Visibility = Visibility.Hidden;
                    selectGp.Visibility = Visibility.Hidden;

                    if (DB.IsGpTableEmpty())
                    {
                        LoadDataGp.Visibility = Visibility.Visible;
                        dayselectm4.IsEnabled = false;
                        dayselectm3.IsEnabled = false;
                        dayselectm2.IsEnabled = false;
                        dayselectm1.IsEnabled = false;
                        dayselect0.IsEnabled = false;
                        dayselect1.IsEnabled = false;
                        dayselect2.IsEnabled = false;
                        dayselect3.IsEnabled = false;
                        dayselect4.IsEnabled = false;
                        Task task = Task.Run(() => DB.RefreshGroupeTable());
                        await task;
                        dayselectm4.IsEnabled = true;
                        dayselectm3.IsEnabled = true;
                        dayselectm2.IsEnabled = true;
                        dayselectm1.IsEnabled = true;
                        dayselect0.IsEnabled = true;
                        dayselect1.IsEnabled = true;
                        dayselect2.IsEnabled = true;
                        dayselect3.IsEnabled = true;
                        dayselect4.IsEnabled = true;
                        LoadDataGp.Visibility = Visibility.Hidden;
                    }

                    Properties.Settings.Default.Groupe = groupe;
                    Groupe.Content = Properties.Settings.Default.Groupe;
                    Properties.Settings.Default.Save();
                    SetDay();
                }
            }
        }

        private void Toogle_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.WeekendWork = true;
            Properties.Settings.Default.Save();
            SetDaySelector();
        }

        private void Toogle_UnChecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.WeekendWork = false;
            Properties.Settings.Default.Save();
            SetDaySelector();
        }

        private void BtnWipeData(object sender, RoutedEventArgs e)
        {
            listGroupes.ItemsSource = null;
            BtnGetGroup.IsEnabled = false;
            WipeDataText.Visibility = Visibility.Visible;
            DB.WipeData();
            WipeDataText.Visibility = Visibility.Hidden;
            BtnGetGroup.IsEnabled = true;
            BtnGetGroup.Visibility = Visibility.Visible;
        }

        private async void BtnGetGroupe(object sender, RoutedEventArgs e)
        {
            BtnGetGroup.Visibility = Visibility.Hidden;
            BtnWipe.IsEnabled = false;
            RefreshGroupText.Visibility = Visibility.Visible;
            Task task = Task.Run(() => DB.RefreshGroupeTable());
            await task;
            RefreshGroupText.Visibility = Visibility.Hidden;

            loadGroupText.Visibility = Visibility.Visible;
            Task task2 = Task.Run(() => {
                var data = new GroupeInterface();

                Dispatcher.Invoke(() => {
                    listGroupes.ItemsSource = data;
                });
            });
            await task2;
            loadGroupText.Visibility = Visibility.Hidden;
            BtnWipe.IsEnabled = true;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void StateMinimize(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void StateExit(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
