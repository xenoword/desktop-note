using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using Note_desktop.View;
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
        private List<Note> saveNoteList;
        private ObservableCollection<Note> noteList;
        private double actualMaxHeight;

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
            BtnToggleReduce = new RelayCommand<Window>(ToogleReduce);
            BtnRemove = new RelayCommand<NoteView>(RemoveNote);
        }

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
                saveNoteList = JsonConvert.DeserializeObject<List<Note>>(json);
            }
            catch
            {
                saveNoteList = new List<Note>();
            }
            NoteList = new ObservableCollection<Note>(saveNoteList);
        }
        #endregion

        #region Delete Note
        public IRelayCommand BtnRemove { get; }

        public void RemoveNote(NoteView noteView)
        {
            saveNoteList.Remove((Note)noteView.DataContext);
            NoteList.Remove((Note)noteView.DataContext);
            SaveNoteList();
        }
        #endregion

        #region Reduce Window
        public IRelayCommand BtnToggleReduce { get; }
        public void ToogleReduce(Window window)
        {
            if (NoteList.Count == 0)
            {
                NoteList = new ObservableCollection<Note>(saveNoteList);
                window.MaxHeight = SystemParameters.PrimaryScreenHeight - window.Top - 10; ;
                //window.SizeToContent = SizeToContent.Height;
            }
            else
            {
                NoteList = new ObservableCollection<Note>();
                window.MaxHeight = 20;
                //window.SizeToContent = SizeToContent.Manual;
                //window.Height = window.MinHeight;
            }
        }
        #endregion

        #region Add Note
        public IRelayCommand BtnAdd { get; }
        public void AddNote()
        {
            if (NoteList.Count == 0)
            {
                NoteList = new ObservableCollection<Note>(saveNoteList);
            }
            Note newNote = new Note();
            NoteList.Add(newNote);
            saveNoteList.Add(newNote);
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
