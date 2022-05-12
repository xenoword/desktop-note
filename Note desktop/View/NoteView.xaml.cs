using System;
using System.Collections.Generic;
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

namespace Note_desktop.View
{
    /// <summary>
    /// Logique d'interaction pour NoteView.xaml
    /// </summary>
    public partial class NoteView : UserControl
    {

        public NoteView()
        {
            InitializeComponent();
        }


        private void ReduceOne(object sender, RoutedEventArgs e)
        {
            Content.Visibility = Visibility.Visible == Content.Visibility ? Visibility.Collapsed : Visibility.Visible;
            BtnReduceOneNote.Content = BtnReduceOneNote.Content.ToString() == "-" ? "+" : "-";
        }
    }
}
