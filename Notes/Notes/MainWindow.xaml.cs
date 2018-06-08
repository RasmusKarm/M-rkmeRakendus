using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Notes
{
    public partial class MainWindow : Window
    {
        public Saving saver = new Saving();
        
        public MainWindow()
        {
            InitializeComponent();
            NotesScreen.Visibility = Visibility.Visible;
            AddNoteScreen.Visibility = Visibility.Collapsed;
            NoteScreen.Visibility = Visibility.Collapsed;

            if (!File.Exists("SavedNotes.xml"))
            {
                saver.CreateXml();
            }
            else
            {
                var reader = XmlReader.Create("SavedNotes.xml");
                Notes.Items.Clear();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "note")
                    {
                        Notes.Items.Add(reader.GetAttribute("header"));
                    }
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NotesScreen.Visibility = Visibility.Collapsed;
            AddNoteScreen.Visibility = Visibility.Visible;
            NoteTextHeader.Text = "";
            NoteTextNote.Text = "";
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            NotesScreen.Visibility = Visibility.Collapsed;
            NoteScreen.Visibility = Visibility.Visible;
            Header.Content = "";
            NoteText.Text = "";

            XmlReader reader = XmlReader.Create("SavedNotes.xml");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "note" && reader.GetAttribute("header") == Notes.SelectedItem.ToString())
                {
                    Header.Content = reader.GetAttribute("header");
                    NoteText.Text = reader.ReadElementContentAsString();
                }
            }
        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            saver.SaveNote(NoteTextHeader.Text, NoteTextNote.Text);
            AddNoteScreen.Visibility = Visibility.Collapsed;
            NotesScreen.Visibility = Visibility.Visible;

            var reader = XmlReader.Create("SavedNotes.xml");
            Notes.Items.Clear();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "note")
                {
                    Notes.Items.Add(reader.GetAttribute("header"));
                }
            }
        }

        private void Notes_GotFocus(object sender, RoutedEventArgs e)
        {
            OpenButton.IsEnabled = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddNoteScreen.Visibility == Visibility.Visible)
            {
                AddNoteScreen.Visibility = Visibility.Collapsed;
            }
            if (NoteScreen.Visibility == Visibility.Visible)
            {
                NoteScreen.Visibility = Visibility.Collapsed;
            }
            NotesScreen.Visibility = Visibility.Visible;

            var reader = XmlReader.Create("SavedNotes.xml");
            Notes.Items.Clear();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "note")
                {
                    Notes.Items.Add(reader.GetAttribute("header"));
                }
            }
        }
    }
}
