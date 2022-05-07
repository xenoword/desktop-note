using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Note_desktop.Model
{
    public class Note
    {
        private const byte MAX = 255;
        private const byte MIN = 0;
        private const byte DIFFCOLOR = 10;
        private static readonly Color DEFAULTCOLOR = ((SolidColorBrush)((App)Application.Current).Resources["Tertiary"]).Color;

        private Color color;
        public Note()
            :this("Titre", "Contenu de la note")
        {

        }

        public Note(string title,string text)
            :this(title, text, DEFAULTCOLOR)
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
        public Color Color 
        {
            get { return color; }
            set 
            {
                color.A = MAX;
                color.R = value.R < MAX - DIFFCOLOR ? value.R : (byte)(MAX - DIFFCOLOR);
                color.G = value.G < MAX - DIFFCOLOR ? value.G : (byte)(MAX - DIFFCOLOR);
                color.B = value.B < MAX - DIFFCOLOR ? value.B : (byte)(MAX - DIFFCOLOR);

            } 
        }
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
                color.A = MAX;
                color.R = Color.R > DIFFCOLOR ? (byte)(Color.R - DIFFCOLOR) : (byte)MIN;
                color.G = Color.G > DIFFCOLOR ? (byte)(Color.G - DIFFCOLOR) : (byte)MIN;
                color.B = Color.B > DIFFCOLOR ? (byte)(Color.B - DIFFCOLOR) : (byte)MIN;
                brush.Color = color;
                return brush;
            }
        }

        public Note Clone()
        {
            return (Note)MemberwiseClone();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new Note(Title, Text));
        }
    }
}
