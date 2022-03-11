using Note_desktop.ViewModel;
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
using System.Windows.Shapes;

namespace Note_desktop.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).NoteVM;
        }

        #region WindowUpdate

        private bool CanMove => Mouse.LeftButton == MouseButtonState.Pressed;
        private bool CanResize { get; set; }
        private bool IsOverMainGrid => MainGrid.IsMouseOver && !NoteListView.IsMouseOver && !BtnAdd.IsMouseOver && !BtnClose.IsMouseOver && !BtnReduce.IsMouseOver;

        private void this_LocationChanged(object sender, EventArgs e)
        {
            if (Top > SystemParameters.PrimaryScreenHeight - MinHeight - 20)
            {
                Top = SystemParameters.PrimaryScreenHeight - MinHeight - 20;
            }
            MaxHeight = SystemParameters.PrimaryScreenHeight - Top - 10;
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CanMove && IsOverMainGrid)
            {
                DragMove();
            }
        }
        #endregion

    }
}
