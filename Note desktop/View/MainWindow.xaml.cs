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

        private void this_LocationChanged(object sender, EventArgs e)
        {
            //Top = Top > SystemParameters.PrimaryScreenHeight - MinHeight - 20 ? Top = SystemParameters.PrimaryScreenHeight - MinHeight - 20 : Top;
            if (Top > SystemParameters.PrimaryScreenHeight - MinHeight - 20)
            {
                Top = SystemParameters.PrimaryScreenHeight - MinHeight - 20;
            }
            MaxHeight = SystemParameters.PrimaryScreenHeight - Top - 10;
        }

        #endregion

        #region Test
        private void header_Loaded(object sender, RoutedEventArgs e)
        {
            InitHeader(sender as Grid);
        }
        private void InitHeader(Grid header)
        {
            var restoreIfMove = false;

            header.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ClickCount == 2)
                {
                    if ((ResizeMode == ResizeMode.CanResize) ||
                        (ResizeMode == ResizeMode.CanResizeWithGrip))
                    {
                        SwitchState();
                    }
                }
                else
                {
                    if (WindowState == WindowState.Maximized)
                    {
                        restoreIfMove = true;
                    }

                    DragMove();
                }
            };
            header.MouseLeftButtonUp += (s, e) =>
            {
                restoreIfMove = false;
            };
            header.MouseMove += (s, e) =>
            {
                if (restoreIfMove)
                {
                    restoreIfMove = false;
                    var mouseX = e.GetPosition(this).X;
                    var width = RestoreBounds.Width;
                    var x = mouseX - width / 2;

                    if (x < 0)
                    {
                        x = 0;
                    }
                    else
                    if (x + width > SystemParameters.PrimaryScreenWidth)
                    {
                        x = SystemParameters.PrimaryScreenWidth - width;
                    }

                    WindowState = WindowState.Normal;
                    Left = x;
                    Top = 0;
                    DragMove();
                }
            };
        }
        private void SwitchState()
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    {
                        WindowState = WindowState.Maximized;
                        break;
                    }
                case WindowState.Maximized:
                    {
                        WindowState = WindowState.Normal;
                        break;
                    }
            }
        }

        #endregion
    }
}
