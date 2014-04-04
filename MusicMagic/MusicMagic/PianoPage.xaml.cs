using MusicMagic.Common;
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
        INoteStream stream;

        int CurrentTime = 0;
        int SpaceSetter = 1;//temporary till stream is fixed
        bool isRecording = false;
        //private static System.Timers.Timer timer;
        //our project doesn't recognize System.timers as a valid using

        public void Initialize() {
            device = new XAudio2();
            var sources = getSources();//Add Sources
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

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
           //TODO: play saved stream
        }

        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            isRecording = !isRecording;
           //TODO:change Icon
            //starts the timer
            //if(recording)
            //timer = new Systems.Timer.Timer(1000);
            //timer.Enabled = true;
            //timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //
        }

        private void TapStarted(object sender, RoutedEventArgs e) {
            //Change key color to pressed color
            var key = (Rectangle)sender;
            key.Fill = new SolidColorBrush(Colors.LightYellow);

            // Play tone
            var pitch = Convert.ToInt32(key.DataContext);
            stream.PlayPitch(pitch);
            
            // TODO: Save start time if recording
        }

        private void TapRelease(object sender, RoutedEventArgs e)
        {
            

            var key = (Rectangle)sender;
            var pitch = Convert.ToInt32(key.DataContext);
            //Change key color to default color
            key.Fill = new SolidColorBrush(Colors.WhiteSmoke);

            // TODO: If recording {
                //Draw the note 
                //Currently uses even spacing
                //TEMP until starttime established
                //Also, this will change once we figure out which specific notes we want to use
                Ellipse newNote  = new Ellipse();
                newNote.Fill = new SolidColorBrush(Colors.White);
                Canvas.SetLeft(newNote, SpaceSetter * 10);
                newNote.Height = 25;
                newNote.Width = 5;
                switch (pitch)
                {
                    case 0: 
                        Canvas.SetTop(newNote, 25);
                        break;
                    case 1:
                        Canvas.SetTop(newNote, 50);
                        break;
                    case 2:
                        Canvas.SetTop(newNote, 75);
                        break;
                    case 3:
                        Canvas.SetTop(newNote, 110);
                        break;
                    case 4:
                        Canvas.SetTop(newNote, 135);
                        break;
                    case 5:
                        Canvas.SetTop(newNote, 160);
                        break;
                    case 6:
                        Canvas.SetTop(newNote, 185);
                        break;
                    case 7:
                        Canvas.SetTop(newNote, 215);
                        break;
                }
                SpaceSetter = SpaceSetter++;

                // Create the new note
                var note = new Note() {
                    Device = device,
                    Parent = stream, // Saves the note to the parent stream
                    Pitch = pitch,
                    Start = CurrentTime, //Get saved start time
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
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public PianoPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


    }
}