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

        
            Icon0Grid.Opacity=0.2;
            Icon1Grid.Opacity=0.2;
            Icon2Grid.Opacity=0.2;
            Icon3Grid.Opacity=0.2;
            Icon4Grid.Opacity=0.2;
            Icon5Grid.Opacity=0.2;
            Icon6Grid.Opacity=0.2;

            if ((historyValue - 1000000) >= 0)
            {
                Icon6Grid.Opacity = 100;
                historyValue -= 1000000;
            }
            if ((historyValue - 100000) >= 0)
            {
                Icon5Grid.Opacity = 100;
                historyValue -= 100000;
            }
            if ((historyValue - 10000) >= 0)
            {
                Icon4Grid.Opacity = 100;
                historyValue -= 10000;
            }
            if ((historyValue - 1000) >= 0)
            {
                Icon3Grid.Opacity = 100;
                historyValue -= 1000;
            }
            if ((historyValue - 100) >= 0)
            {
                Icon2Grid.Opacity = 100;
                historyValue -= 100;
            }
            if ((historyValue - 10) >= 0)
            {
                Icon1Grid.Opacity = 100;
                historyValue -= 10;
            }
            if ((historyValue - 1) >= 0)
            {
                Icon0Grid.Opacity = 100;
                historyValue -= 1;
            }

        }
    }
}
