using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Note_desktop.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Note_desktop.ViewModel
{
    public class EditingNoteVM : ObservableObject
    {
        private int index;
        public EditingNoteVM()
        {
            BtnValidate = new RelayCommand<Window>(SaveAndCloseEdition);
        }

        #region Properties
        public int Index 
        { 
            get => index;
            set
            {
                index = value;
                EditingNote = ((App)Application.Current).NoteVM.NoteList[Index].Clone();
            }
        }

        public Note EditingNote { get; set; }
        public string Title
        {
            get => EditingNote.Title;
            set
            {
                EditingNote.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Text
        {
            get => EditingNote.Text;
            set
            {
                EditingNote.Text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public string Color
        {
            get => EditingNote.Color.ToString();
            set
            {
                if (value.Substring(0,1) == "#")
                {
                    try
                    {
                        Color color = new Color();
                        color.A = Convert.ToByte(value.Substring(1, 2),16);
                        color.R = Convert.ToByte(value.Substring(3, 2),16);
                        color.G = Convert.ToByte(value.Substring(5, 2),16);
                        color.B = Convert.ToByte(value.Substring(7, 2),16);
                        EditingNote.Color = color;
                    }
                    catch 
                    {
                        
                    }
                    OnPropertyChanged(nameof(Color));
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }
        public SolidColorBrush BackgroundColor
        {
            get => EditingNote.BackgroundColor;
        }

        #endregion

        #region validate changes

        public IRelayCommand BtnValidate { get; }

        public void SaveAndCloseEdition(Window window)
        {
            ((App)Application.Current).NoteVM.NoteList[Index] = EditingNote;
            ((App)Application.Current).NoteVM.SaveNoteList();
            window.Close();
        }

        #endregion
    }
}
