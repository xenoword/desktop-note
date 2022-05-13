using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using Note_desktop.View;
using Note_desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Note_desktop.ViewModel
{
    public class NoteVM : ObservableObject
    {
        private ObservableCollection<Note> noteList;
        public ObservableCollection<Note> NoteList
        {
            get => noteList;
            set
            {
                noteList = value;
                OnPropertyChanged(nameof(NoteList));
            }
        }

        public NoteVM()
        {
            LoadNoteList();
            BtnClose = new RelayCommand<Window>(CloseWindow);
            BtnAdd = new RelayCommand(AddNote);
            BtnRemove = new RelayCommand<NoteView>(RemoveNote);
            BtnOpenEdit = new RelayCommand<NoteView>(OpenEditNote);
            BtnSave = new RelayCommand(SaveNoteList);

            //check if data is up to date
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(2000);
            timer.Tick += CheckDataUpToDate;
            timer.Start();
        }


        #region Check Data Up To Date

        public bool IsDataUpToDate { get => JsonConvert.SerializeObject(NoteList) == File.ReadAllText("main.json"); }

        public void CheckDataUpToDate(object sender, EventArgs e)
        {
            ((Button)((MainWindow)((App)Application.Current).MainWindow).Template.FindName("BtnSave", ((App)Application.Current).MainWindow)).Content = IsDataUpToDate ? "💾" : "💾*";
        }

        #endregion

        #region Edit Note
        public IRelayCommand BtnOpenEdit { get; }

        public void OpenEditNote(NoteView noteView)
        {
            ((App)Application.Current).EditingNoteVM.Index = NoteList.IndexOf((Note)noteView.DataContext);
            new EditNoteWindow().ShowDialog();
        }
        
        #endregion

        #region Delete Note
        public IRelayCommand BtnRemove { get; }

        public void RemoveNote(NoteView noteView)
        {
            MessageBoxResult result = MessageBox.Show(App.Current.MainWindow, "Supprimer la note \"" + ((Note)noteView.DataContext).Title + "\"?","Supprimer la note", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                NoteList.Remove((Note)noteView.DataContext);
            }
        }
        #endregion

        #region Add Note
        public IRelayCommand BtnAdd { get; }
        public void AddNote()
        {
            NoteList.Add(new Note());
        }
        #endregion

        #region Save And Load File

        public IRelayCommand BtnSave { get; }
        public void SaveNoteList()
        {
            string json = JsonConvert.SerializeObject(NoteList);
            File.WriteAllText("main.json", json);
        }
        public void LoadNoteList()
        {
            try
            {
                string json = File.ReadAllText("main.json");
                NoteList = JsonConvert.DeserializeObject<ObservableCollection<Note>>(json);
            }
            catch
            {
               NoteList = new ObservableCollection<Note>();
            }
        }
        #endregion

        #region Close Window
        public IRelayCommand BtnClose { get; }

        private void CloseWindow(Window window)
        {
            if (!IsDataUpToDate)
            {
                MessageBoxResult result = MessageBox.Show(App.Current.MainWindow, "Voulez vous sauvegarder vos notes avant de quitter ?", "Sauvegarder", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.None:
                        break;
                    case MessageBoxResult.OK:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                    case MessageBoxResult.Yes:
                        SaveNoteList();
                        window.Close();
                        break;
                    case MessageBoxResult.No:
                        window.Close();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                window.Close();
            }
        }
        #endregion
    }
}
