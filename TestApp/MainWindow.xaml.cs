using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestApp.Flowchart;

namespace TestApp
{
 	public partial class MainWindow : Window
	{
        private List<TabItem> _tabItems;
        private TabItem _tabAdd;
        private int nCurrentTabID;

		public MainWindow()
		{
			InitializeComponent();
            // initialize tabItem array
            _tabItems = new List<TabItem>();

            // add a tabItem with + in header 
            _tabAdd = new TabItem();
            _tabAdd.Header = "+";
            _tabItems.Add(_tabAdd);

            // add first tab
            nCurrentTabID = 0;
            this.AddTabItem();

            // bind tab control
            tabDynamic.DataContext = _tabItems;
            tabDynamic.SelectedIndex = 0;
		}

        private TabItem AddTabItem()
        {
            int count = _tabItems.Count;

            // create new tab item
            TabItem tab = new TabItem();

            tab.Header = string.Format("NFA{0}", nCurrentTabID);
            tab.Name = string.Format("tab{0}", nCurrentTabID);
            nCurrentTabID++;
            tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;

            // add controls to tab item, this case I added just a textbox
            FlowchartEditor flowEditor = new FlowchartEditor();
            flowEditor.Name = "content";

            tab.Content = flowEditor;

            // insert tab item right before the last (+) tab item
            _tabItems.Insert(count - 1, tab);

            return tab;
        }

        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tab = tabDynamic.SelectedItem as TabItem;
            if (tab == null) return;

            if (tab.Equals(_tabAdd))
            {
                // clear tab control binding
                tabDynamic.DataContext = null;

                TabItem newTab = this.AddTabItem();

                // bind tab control
                tabDynamic.DataContext = _tabItems;

                // select newly added tab item
                tabDynamic.SelectedItem = newTab;
            }
            else
            {
                // your code here...
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();
            var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();
            TabItem tab = item as TabItem;

            if (tab != null)
            {
                if (_tabItems.Count < 3)
                {
                    MessageBox.Show("Cannot delete last NFA Diagram.");
                }
                else if (MessageBox.Show(string.Format("Are are sure to delete {0} ?", tab.Header.ToString()),
                    "Delete NFA", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // get selected tab
                    TabItem selectedTab = tabDynamic.SelectedItem as TabItem;

                    // clear tab control binding
                    tabDynamic.DataContext = null;

                    _tabItems.Remove(tab);

                    // bind tab control
                    tabDynamic.DataContext = _tabItems;

                    // select previously selected tab. if that is removed then select first tab
                    if (selectedTab == null || selectedTab.Equals(tab))
                    {
                        selectedTab = _tabItems[0];
                    }
                    tabDynamic.SelectedItem = selectedTab;
                }
            }
        }
	}
}
