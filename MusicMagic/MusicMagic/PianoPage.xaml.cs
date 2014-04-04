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
        private readonly string[] NOTE_PATHS = new string[] {
            @"Resources\piano-a.wav",
        };

        // Length, loop start, loop end
        private readonly int[,] NOTE_INFO = new int[,] {
            { 0, 0, 0 },
        };

        XAudio2 device;
        MasteringVoice master;
        INoteStream stream;

        int startTime = 0;

        public void Initialize() {
            device = new XAudio2();
            master = new MasteringVoice(device);
            var sources = getSources();
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
            //Canvas.SetLeft(obj, x);
            //Canvas.SetTop(obj, y);
        }
      

        private void TapStarted(object sender, RoutedEventArgs e) {
            //Change key color to pressed color
            var key = (Rectangle)sender;
            key.Fill = new SolidColorBrush(Colors.LightYellow);

            // Play tone
            var pitch = (int)key.DataContext;
            stream.PlayPitch(pitch);
            
            // TODO: Save start time if recording
        }

        private void TapRelease(object sender, RoutedEventArgs e)
        {
            

            var key = (Rectangle)sender;
            var pitch = (int)key.DataContext;
            //Change key color to default color
            key.Fill = new SolidColorBrush(Colors.WhiteSmoke);

            // TODO: If recording {
                // Create the new note
                var note = new Note() {
                    Device = device,
                    Parent = stream, // Saves the note to the parent stream
                    Pitch = pitch,
                    Start = startTime, //Get saved start time
                    Length = 0, // TODO: Calculate length; CurrentTime - StartTime
                };
            // }
        }

        private List<INoteSource> getSources() {
            List<INoteSource> sources = new List<INoteSource>();
            for (int i = 0; i < NOTE_PATHS.Length; i++) {
                sources.Add(new NoteSource() {
                    Device     = device,
                    Path       = NOTE_PATHS[i],
                    NoteLength = NOTE_INFO[i, 0],
                    LoopBegin  = NOTE_INFO[i, 1],
                    LoopLength = NOTE_INFO[i, 2],
                });
            }
            return sources;
        }
    }
}