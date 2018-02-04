using System.Collections.ObjectModel;
using System.Windows;

namespace KODI_Controller
{
    public partial class MessageWindow : Window
    {
        public MessageWindow(ObservableCollection<string> messages)
        {
            InitializeComponent();
            this.DataContext = messages;
        }
    }
}
