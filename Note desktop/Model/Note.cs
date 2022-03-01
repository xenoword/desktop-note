using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Note_desktop.View
{
    public class Note
    {
        public Note()
            :this("Titre", "Contenu de la note")
        {

        }

        public Note(string title,string text)
        {
            Title = title;
            Text = text;
        }

        public string Text { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new Note(Title, Text));
        }
    }
}
