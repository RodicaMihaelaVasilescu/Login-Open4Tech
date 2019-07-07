using System.Windows.Controls;

namespace Open4Tech.View
{
    /// <summary>
    /// Interaction logic for ConfirmationCodeControl.xaml
    /// </summary>
    public partial class ConfirmationCodeControl : UserControl
    {
        public ConfirmationCodeControl()
        {
            InitializeComponent();
            ConfirmationCode.Focus();
        }
    }
}
