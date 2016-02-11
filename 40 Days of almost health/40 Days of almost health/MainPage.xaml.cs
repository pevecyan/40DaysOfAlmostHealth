using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace _40_Days_of_almost_health
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            //Settings
            var settings = ApplicationData.Current.LocalSettings;

            if (!settings.Values.ContainsKey("StartDate"))
            {
                Current.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                NotYet.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                NewGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                DateTime dateTime = new DateTime((long)settings.Values["StartDate"]);
                int length = (int)settings.Values["StartLength"];

                DaysLeftTextBlock.Text = ""+dateTime.AddDays(length).Subtract(DateTime.Now.ToUniversalTime()).Days;

                if (DateTime.Now.ToUniversalTime().Subtract(dateTime).Ticks >= 0)
                {
                    NewGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    NotYet.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    Current.Visibility = Windows.UI.Xaml.Visibility.Visible;

                    var todayDate = DateTime.Now.Date.Ticks;

                    long[] historyDateTab = (long[])settings.Values["HistoryDate"];
                    int [] historyValueTab = (int[])settings.Values["HistoryValue"];

                    #region
                    if (historyDateTab.Last() == todayDate)
                    {
                        var todayValue = historyValueTab.Last();

                        if ((todayValue - 1000000) >= 0)
                        {
                            ToggleButton6.IsChecked = true;
                            todayValue -= 1000000;
                        }
                        if ((todayValue - 100000) >= 0)
                        {
                            ToggleButton5.IsChecked = true;
                            todayValue -= 100000;
                        }
                        if ((todayValue - 10000) >= 0)
                        {
                            ToggleButton4.IsChecked = true;
                            todayValue -= 10000;
                        }
                        if ((todayValue - 1000) >= 0)
                        {
                            ToggleButton3.IsChecked = true;
                            todayValue -= 1000;
                        }
                        if ((todayValue - 100) >= 0)
                        {
                            ToggleButton2.IsChecked = true;
                            todayValue -= 100;
                        }
                        if ((todayValue - 10) >= 0)
                        {
                            ToggleButton1.IsChecked = true;
                            todayValue -= 10;
                        }
                        if ((todayValue - 1) >= 0)
                        {
                            ToggleButton0.IsChecked = true;
                            todayValue -= 1;
                        }

                    }
                    #endregion load current

                    #region load timeline
                    for (int i = historyDateTab.Length-1; i >= 0; i--)
                    {
                        ItemsStackPanel.Children.Add(new HistoryItem(historyValueTab[i], historyDateTab[i]));
                    }
                    #endregion
                }
                else
                {
                    GetDatePicker.Date = dateTime.ToLocalTime();
                    NewGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    Current.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    NotYet.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
            NotificationsSetup();

        }

        public void NotificationsSetup()
        {
            var settings = ApplicationData.Current.LocalSettings;
            if (!settings.Values.ContainsKey("Notifications"))
            {
                settings.Values["Notifications"] = true;
            }

            var toasts = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications();
            foreach (var toast in toasts)
            {
                ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(toast);
            }

            bool isEnabled = (bool)settings.Values["Notifications"];
            if (isEnabled)
            {

                var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
                var strings = toastXml.GetElementsByTagName("text");
                strings[0].AppendChild(toastXml.CreateTextNode("Si se že danes pregrešil?"));
                strings[1].AppendChild(toastXml.CreateTextNode(""));
                Random r = new Random();

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        // Create the toast notification object.
                        var idNumber = Math.Floor(r.NextDouble() * 100000000);
                        var toast = new ScheduledToastNotification(toastXml, new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0).AddDays(i)));
                        toast.Id = "Toast" + idNumber;

                        var idNumber1 = Math.Floor(r.NextDouble() * 100000000);
                        var toast1 = new ScheduledToastNotification(toastXml, new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0).AddDays(i)));
                        toast1.Id = "Toast" + idNumber1;

                        ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
                        ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast1);
                    }
                    catch (Exception ex)
                    {
                        //Break because wrong date :)
                    }
                }
            }
            else
            {
                
                
            }



        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void MainPivot_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (ContentGrid.ActualWidth != 0)
            {
                double workableWidth = (BackgroundGrid.ActualWidth - ContentGrid.ActualWidth) / (MainPivot.Items.Count - 1);

                background1MoveAnimation.To = -workableWidth * MainPivot.SelectedIndex;

                background2MoveAnimation.To = -(workableWidth / 2.0) * MainPivot.SelectedIndex;
                background3MoveAnimation.To = -(workableWidth / 4.0) * MainPivot.SelectedIndex;

                background1Move.Begin();
                background2Move.Begin();
                background3Move.Begin();
            }
            
            
        	
        }

        private void SetStartButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var settings = ApplicationData.Current.LocalSettings;
            DateTime dateTime = SetDatePicker.Date.UtcDateTime;
            var length = int.Parse(SetLengthTextBox.Text);

            settings.Values["StartDate"] = dateTime.Ticks;
            settings.Values["StartLength"] = length;

            if (DateTime.Now.ToUniversalTime().Subtract(dateTime).Ticks >= 0)
            {
                NewGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Current.Visibility = Windows.UI.Xaml.Visibility.Visible;
                DaysLeftTextBlock.Text = "" + dateTime.AddDays(length).Subtract(DateTime.Now.ToUniversalTime()).Days;
            }
            else
            {
                NewGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                NotYet.Visibility = Windows.UI.Xaml.Visibility.Visible;
                GetDatePicker.Date = dateTime.ToLocalTime();
                DaysLeftTextBlock.Text = "∞";
            }


            
            
        }

        private void ToggleButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var settings = ApplicationData.Current.LocalSettings;

            long[] historyDateTab;
            int[] historyValueTab;

            if (settings.Values.ContainsKey("HistoryDate"))
            {
                historyDateTab = (long[])settings.Values["HistoryDate"];
                historyValueTab = (int[])settings.Values["HistoryValue"];
            }
            else
            {
                historyDateTab = new long[1];
                historyValueTab = new int[1];
            }

            var historyDate = historyDateTab.ToList();
            var historyValue = historyValueTab.ToList();

            

            if (sender == ToggleButton0)
            {
                ToggleButton1.IsChecked = false;
                ToggleButton2.IsChecked = false;
                ToggleButton3.IsChecked = false;
                ToggleButton4.IsChecked = false;
                ToggleButton5.IsChecked = false;
                ToggleButton6.IsChecked = false;
            }
            else
            {
                ToggleButton0.IsChecked = false;
            }

            var todayDate = DateTime.Now.Date.Ticks;
            var todayValue = 0;
            if ((bool)ToggleButton0.IsChecked) todayValue += 1;
            if ((bool)ToggleButton1.IsChecked) todayValue += 10;
            if ((bool)ToggleButton2.IsChecked) todayValue += 100;
            if ((bool)ToggleButton3.IsChecked) todayValue += 1000;
            if ((bool)ToggleButton4.IsChecked) todayValue += 10000;
            if ((bool)ToggleButton5.IsChecked) todayValue += 100000;
            if ((bool)ToggleButton6.IsChecked) todayValue += 1000000;

            if (historyDate.Last() == todayDate || historyDate.Last() == 0)
            {
                
                if (historyDate.Last() == 0)
                {
                    historyDate.RemoveAt(historyDate.Count - 1);
                    historyDate.Add(todayDate);

                    ItemsStackPanel.Children.Insert(0, new HistoryItem(todayValue, todayDate));

                }
                else
                {
                    ((HistoryItem)ItemsStackPanel.Children.First()).UpdateData(todayValue, todayDate);
                }

                historyValue.RemoveAt(historyValue.Count - 1);
                historyValue.Add(todayValue);

                
            }
            else
            {
                historyDate.Add(todayDate);
                historyValue.Add(todayValue);

                ItemsStackPanel.Children.Insert(0, new HistoryItem(todayValue, todayDate));
            }

            settings.Values["HistoryDate"] = historyDate.ToArray();
            settings.Values["HistoryValue"] = historyValue.ToArray();

            

        }

        private void DeleteButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var settings = ApplicationData.Current.LocalSettings;

            settings.Values.Remove("StartDate");
            settings.Values.Remove("StartLength");
            settings.Values.Remove("HistoryDate");
            settings.Values.Remove("HistoryValue");

            ItemsStackPanel.Children.Clear();
            NewGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            NotYet.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Current.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void NotificationsSwitch_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (NotificationsSwitch != null)
            {
                var settings = ApplicationData.Current.LocalSettings;
                settings.Values["Notifications"] = NotificationsSwitch.IsOn;

                NotificationsSetup();
            }
        }
    }
}
