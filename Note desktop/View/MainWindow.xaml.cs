using Note_desktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = System.Windows.Application;

namespace Note_desktop.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Screen ActualScreen { get => Screen.FromHandle(new WindowInteropHelper(this).Handle); }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).NoteVM;
        }

        #region WindowUpdate

        private void this_LocationChanged(object sender, EventArgs e)
        {
            Top = Top > ActualScreen.Bounds.Bottom - MinHeight - 20 ? Top = ActualScreen.Bounds.Bottom - MinHeight - 20 : Top;
            MaxHeight = MaxHeight != MinHeight ? ActualScreen.Bounds.Height - Top - ActualScreen.Bounds.Height * 0.204 : MaxHeight;
        }
        private void ToogleReduce(object sender, RoutedEventArgs e)
        {
            MaxHeight = MaxHeight == MinHeight ? ActualScreen.Bounds.Height - Top - ActualScreen.Bounds.Height * 0.204 : MinHeight;
            SizeToContent = SizeToContent.Height;
            ((System.Windows.Controls.Button)Template.FindName("BtnReduce", this)).Content = ((System.Windows.Controls.Button)Template.FindName("BtnReduce", this)).Content.ToString() == "-" ? "+" : "-";
        }
        private void Expand(object sender, RoutedEventArgs e)
        {
            MaxHeight = ActualScreen.Bounds.Height - Top - ActualScreen.Bounds.Height * 0.204;
            SizeToContent = SizeToContent.Height;
            ((System.Windows.Controls.Button)Template.FindName("BtnReduce", this)).Content = "-";
        }

        #endregion

        #region dragable and fullscreen
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

        private void Save(object sender, RoutedEventArgs e)
        {
            ((System.Windows.Controls.Button)Template.FindName("BtnSave", this)).Content = "💾";
        }
    }
}
