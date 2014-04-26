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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicMagic {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPage : Page {

        public List<INoteStream> Streams {
            get {
                return ((App)Application.Current).Streams;
            }
        }

        public EditPage() {
            this.InitializeComponent();
            DrawTracks();
        }

        private void DrawTracks() {
            Tracks.Children.Clear();
            foreach (var stream in Streams) {
                Tracks.Children.Add(new StreamView(stream) {
                    Height = 200,
                    Margin = new Thickness(10),
                });
            }
        }

        private void New_Piano_Click(object sender, RoutedEventArgs e) {
            ((App)Application.Current).NewPiano();
            this.Frame.Navigate(typeof(PianoPage));
        }

        private void New_Guitar_Click(object sender, RoutedEventArgs e) {
            ((App)Application.Current).NewGuitar();
            this.Frame.Navigate(typeof(GuitarPage));
        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            this.Frame.GoBack();
        }
    }
}
