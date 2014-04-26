using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MusicMagic {
    public sealed partial class StreamView : UserControl {
        public int Start { get; set; }
        public int End { get; set; }

        public StreamView() {
            this.InitializeComponent();
        }

        public StreamView(INoteStream stream) : this() {
            DataContext = stream;
        }

        private void track_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args) {
            track.Start = ((NoteStream)DataContext).EarliestTime;
            track.End = ((NoteStream)DataContext).LatestTime;
            track.Redraw(Start, End);
        }
    }
}
