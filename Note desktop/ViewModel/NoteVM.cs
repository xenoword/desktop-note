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
        }


        #region Edit File
        public IRelayCommand BtnOpenEdit { get; }

        public void OpenEditNote(NoteView noteView)
        {
            ((App)Application.Current).EditingNoteVM.Index = NoteList.IndexOf((Note)noteView.DataContext);
            new EditNoteWindow().Show();
        }
        #endregion

        #region Save File
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

        #region Delete Note
        public IRelayCommand BtnRemove { get; }

        public void RemoveNote(NoteView noteView)
        {
            MessageBoxResult result = MessageBox.Show(App.Current.MainWindow, "Supprimer la note \"" + ((Note)noteView.DataContext).Title + "\"?","Supprimer la note", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                NoteList.Remove((Note)noteView.DataContext);
                SaveNoteList();
            }
        }
        #endregion

        #region Add Note
        public IRelayCommand BtnAdd { get; }
        public void AddNote()
        {
            Note newNote = new Note();
            NoteList.Add(newNote);
            SaveNoteList();
        }
        #endregion

        #region Close Window
        public IRelayCommand BtnClose { get; }

        private void CloseWindow(Window window)
        {
            SaveNoteList();
            window.Close();
        }
        #endregion
    }
}
