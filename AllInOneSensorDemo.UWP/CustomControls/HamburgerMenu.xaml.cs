using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace AllInOneSensorDemo.UWP.CustomControls
{
    public sealed partial class HamburgerMenu : UserControl
    {
        public HamburgerMenu()
        {
            this.InitializeComponent();
        }

        public bool IsHamburgerMenuOpen
        {
            get
            {
                return ShellSplitView.IsPaneOpen;

            }
            set
            {
                ShellSplitView.IsPaneOpen = value;

            }
        }

        private void BackRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void HamburgerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // don't let the radiobutton check
            (sender as RadioButton).IsChecked = false;

            // toggle the splitview pane
            this.ShellSplitView.IsPaneOpen = !this.ShellSplitView.IsPaneOpen;
        }

        private void ShellSplitView_OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (VisualStateGroup.CurrentState != null && (ShellSplitView.IsPaneOpen && !VisualStateGroup.CurrentState.Name.Equals("VisualStateWide")))
                ShellSplitView.IsPaneOpen = false;
        }

        private void IsTypePresentStateTriggers_OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            Debug.WriteLine("Change");
        }
    }
}
