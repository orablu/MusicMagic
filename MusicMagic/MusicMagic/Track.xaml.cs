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
        private const    int   WIDTH_OFFSET   = 100;
        private readonly int[] HEIGHT_OFFSETS = new int[] { 25, 50, 75, 110, 135, 160, 185, 215 };

        private int? _start;
        public int Start {
            get {
                return _start.Value;
            }
            set {
                _start = value;
                Redraw();
            }
        }

        private int? _end;
        public int End {
            get {
                return _end.Value;
            }
            set {
                _end = value;
                Redraw();
            }
        }

        public Track() {
            this.InitializeComponent();
        }

        private void ContextChanged(object sender, RoutedEventArgs e) {
            Redraw();
        }

        public void Redraw() {
            if (DataContext != null && _start.HasValue && _end.HasValue) {
                DrawStaff();
                DrawNotes();
            }
        }

        private void DrawNotes()
        {
            INoteStream stream = DataContext as INoteStream;
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

        private void DrawStaff() {
            l1.Y1 = l1.Y2 =       Staff.ActualHeight / 6.0;
            l2.Y1 = l2.Y2 = 2.0 * Staff.ActualHeight / 6.0;
            l3.Y1 = l3.Y2 = 3.0 * Staff.ActualHeight / 6.0;
            l4.Y1 = l4.Y2 = 4.0 * Staff.ActualHeight / 6.0;
            l5.Y1 = l5.Y2 = 5.0 * Staff.ActualHeight / 6.0;
        }
    }
    
}
