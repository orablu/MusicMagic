using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MusicMagic {
    public sealed partial class Track : UserControl {
        public Track() {
            this.InitializeComponent();
        }
        private const int WIDTH_OFFSET = 100;
        private readonly int[] HEIGHT_OFFSETS = new int[] { 25, 50, 75, 110, 135, 160, 185, 215 };
        private readonly string[] NOTE_PATHS = new string[] {
            @"Resources\piano-a.wav",
        };
        // Length, loop start, loop length
        private readonly int[,] NOTE_INFO = new int[,] {
            { 1540, 800, 50 },
        };

        INoteStream stream;
        Canvas notesBar = new Canvas();
        private void RedrawNotes()
        {
            notesBar.Children.Clear();
            foreach (INote note in stream)
            {
                Ellipse newNote = new Ellipse();
                notesBar.Children.Add(newNote);
                newNote.Fill = new SolidColorBrush(Colors.White);
                Canvas.SetLeft(newNote, note.Start * WIDTH_OFFSET);
                Canvas.SetTop(newNote, HEIGHT_OFFSETS[note.Pitch]);
                newNote.Height = 25;
                newNote.Width = note.Length * 5;
                newNote.DataContext = note;
            }

        }
    }
    
}
