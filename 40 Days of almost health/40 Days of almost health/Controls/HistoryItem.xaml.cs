using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace _40_Days_of_almost_health
{
    public sealed partial class HistoryItem : UserControl
    {
        int historyValue;
        long historyDate;

        public HistoryItem()
        {
            this.InitializeComponent();
        }
        public HistoryItem(int historyValue, long historyDate)
        {
            this.InitializeComponent();

            this.historyDate = historyDate;
            this.historyValue = historyValue;

            UpdateData(historyValue, historyDate);
        }

        public void UpdateData(int historyValue, long historyDate)
        {
            DateValue.Text = new DateTime(historyDate).ToString("d.M.yyyy");

            Icon0.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Icon1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Icon2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Icon3.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Icon4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Icon5.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Icon6.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if ((historyValue - 1000000) >= 0)
            {
                Icon6.Visibility = Windows.UI.Xaml.Visibility.Visible;
                historyValue -= 1000000;
            }
            if ((historyValue - 100000) >= 0)
            {
                Icon5.Visibility = Windows.UI.Xaml.Visibility.Visible;
                historyValue -= 100000;
            }
            if ((historyValue - 10000) >= 0)
            {
                Icon4.Visibility = Windows.UI.Xaml.Visibility.Visible;
                historyValue -= 10000;
            }
            if ((historyValue - 1000) >= 0)
            {
                Icon3.Visibility = Windows.UI.Xaml.Visibility.Visible;
                historyValue -= 1000;
            }
            if ((historyValue - 100) >= 0)
            {
                Icon2.Visibility = Windows.UI.Xaml.Visibility.Visible;
                historyValue -= 100;
            }
            if ((historyValue - 10) >= 0)
            {
                Icon1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                historyValue -= 10;
            }
            if ((historyValue - 1) >= 0)
            {
                Icon0.Visibility = Windows.UI.Xaml.Visibility.Visible;
                historyValue -= 1;
            }

        }
    }
}
