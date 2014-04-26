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
                if (!_start.HasValue) {
                    return 0;
                }
                return _start.Value;
            }
            set {
                _start = value;
            }
        }

        private int? _end;
        public int End {
            get {
                if (!_end.HasValue) {
                    return 0;
                }
                return _end.Value;
            }
            set {
                _end = value;
            }
        }

        public Track() {
            this.InitializeComponent();
        }

        public void Redraw() {
            Redraw(Start, End);
        }

        public void Redraw(int start, int end) {
            DrawStaff();
            if (DataContext != null && _start.HasValue && _end.HasValue) {
                DrawNotes(start, end);
            }
        }

        private void DrawNotes(int start, int end)
        {
            INoteStream stream = DataContext as INoteStream;
            notesBar.Children.Clear();
            var brush = new SolidColorBrush(Colors.White);
            var border = new SolidColorBrush(Colors.Black);
            foreach (INote note in stream.NotesInRange(start, end))
            {
                var newNote = new Rectangle();
                newNote.Fill = brush;
                newNote.Stroke = border;
                newNote.RadiusX = 2;
                newNote.RadiusY = 2;
                Canvas.SetLeft(newNote, (float)(note.Start - start) / (float)(end - start) * ActualWidth);
                Canvas.SetTop(newNote, ActualHeight * (1f - (float)(note.Pitch + 1) / 23f));
                Canvas.SetZIndex(newNote, 20);
                newNote.Height = ActualHeight / 25f;
                newNote.Width = (float)note.Length / (float)(end - start) * ActualWidth;
                newNote.DataContext = note;
                notesBar.Children.Add(newNote);
            }
            notesBar.InvalidateMeasure();
            notesBar.UpdateLayout();
        }

        private bool staffDrawn = false;
        private void DrawStaff() {
            if (staffDrawn) {
                return;
            }
            l1.Y1 = l1.Y2 =       Staff.ActualHeight / 6.0;
            l2.Y1 = l2.Y2 = 2.0 * Staff.ActualHeight / 6.0;
            l3.Y1 = l3.Y2 = 3.0 * Staff.ActualHeight / 6.0;
            l4.Y1 = l4.Y2 = 4.0 * Staff.ActualHeight / 6.0;
            l5.Y1 = l5.Y2 = 5.0 * Staff.ActualHeight / 6.0;
            staffDrawn = true;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) {
            staffDrawn = false;
            Redraw();
        }
    }
}
