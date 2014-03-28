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
using SharpDX.XAudio2;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicMagic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PianoPage : Page
    {
        XAudio2 device;
        INoteStream stream;

        int counter = 0;

        public void Initialize() {
            device = new XAudio2();
            var sources = new List<INoteSource>();
            // Add sources.
            stream = new NoteStream() {
                Sources = sources,
                Type = NoteType.Piano,
            };
        }

        //Given two lists, or however we want to store the notes value, draw the notes.
        //Requires much testing after ability to run code on my machine(i.e. sharpDX)
        //in order to decide on spacing @Andy
        private void Redraw(List<int> VertPlace, List<int> HorizPlace)
        {
            //Canvas.get/set(obj)
        }
      

        private void TapStarted(object sender, RoutedEventArgs e) {
            // TODO: Change key color to pressed color
            // TODO: Play tone until done
            // TODO: Save start time if recording
        }

        private void TapRelease(object sender, RoutedEventArgs e)
        {
            // TODO: Change key color to default color

            var key = (Rectangle)sender;
            var pitch = (int)key.DataContext;

            // TODO: If recording {
                // Create the new note
                var note = new Note() {
                    Device = device,
                    Parent = stream, // Saves the note to the parent stream
                    Pitch = pitch,
                    Start = 0, // TODO: Get saved start time
                    Length = 0, // TODO: Calculate length
                };
            // }
        }
    }
}