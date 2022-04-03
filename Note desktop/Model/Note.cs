using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Note_desktop.View
{
    public class Note
    {
        public Note()
            :this("Titre", "Contenu de la note")
        {

        }

        public Note(string title,string text)
            :this(title, text, ((SolidColorBrush)((App)Application.Current).Resources["Tertiary"]).Color)
        {

        }
        public Note(string title, string text, Color color)
        {
            Title = title;
            Text = text;
            Color = color;
        }

        public string Text { get; set; }
        public string Title { get; set; }
        public Color Color { get; set; }
        public SolidColorBrush BackgroundColor
        {
            get
            {
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color;
                return brush;
            }
        }
        public SolidColorBrush BorderColor 
        { 
            get
            {
                SolidColorBrush brush = new SolidColorBrush();
                Color color = new Color();
                color.A = 255;
                color.R = Convert.ToByte(Color.R + 5);
                color.G = Convert.ToByte(Color.G + 5);
                color.B = Convert.ToByte(Color.B + 5);
                brush.Color = color;
                return brush;
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new Note(Title, Text));
        }
    }
}
