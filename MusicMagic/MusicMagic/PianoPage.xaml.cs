using MusicMagic.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.Storage.Pickers;
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
        private readonly int[] SHARPS = new int[] { 1, 3, 6, 8, 10, 13, 15, 18, 20, 22 };
        private readonly string[] NOTE_PATHS = new string[] {
            @"Resources\Piano\lower-c.wav",
            @"Resources\Piano\lower-c#.wav",
            @"Resources\Piano\lower-d.wav",
            @"Resources\Piano\lower-ef.wav",
            @"Resources\Piano\lower-e.wav",
            @"Resources\Piano\lower-f.wav",//
            @"Resources\Piano\lower-f#.wav",
            @"Resources\Piano\lower-g.wav", //
            @"Resources\Piano\lower-g#.wav",//
            @"Resources\Piano\low-a.wav", 

            @"Resources\Piano\low-bf.wav",
            @"Resources\Piano\low-b.wav",
            @"Resources\Piano\low-c.wav",
            @"Resources\Piano\low-c#.wav",
            @"Resources\Piano\low-d.wav",            
            @"Resources\Piano\low-ef.wav",
            @"Resources\Piano\low-e.wav",
            @"Resources\Piano\low-f.wav",
            @"Resources\Piano\low-f#.wav", //
            @"Resources\Piano\low-g.wav", //fix it
            @"Resources\Piano\low-g#.wav",
            @"Resources\Piano\another a.wav",
            @"Resources\Piano\bflat.wav",
            @"Resources\Piano\another b.wav",
           // @"Resources\Piano\piano-c.wav",

            
            
            
        };

        // Length, loop start, loop length
        private const int NOTE_LENGTH = 0;
        private const int LOOP_BEGIN  = 1;
        private const int LOOP_LENGTH = 2;
        private readonly int[,] NOTE_INFO = new int[,] {
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 },
            { 1540, 0, 0 },
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 },
            { 1540, 0, 0 },
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 }, 
            { 1540, 0, 0 },
            { 1540, 0, 0 },
        };

        XAudio2 device;
        MasteringVoice master;
        INoteStream stream;

        int CurrentTime = 0;
        int StartTime = 0;
        bool isRecording = false;
        DispatcherTimer timer = new DispatcherTimer();
        Canvas notesBar = new Canvas();

        public void Initialize() {
            device = new XAudio2();
            master = new MasteringVoice(device);
            var sources = getSources();
            // Add sources.
            //stream = ((App)Application.Current).CurrentNoteStream;
            stream = new NoteStream() {
                Sources = sources,
                Type = NoteType.Piano,
            };
            PianoTrack.DataContext = stream;
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            notesBar.Width = 1000;
            notesBar.Height = 300;
            timer.Start();
        }

        
        //Given two lists, or however we want to store the notes value, draw the notes.
        //FLAGGED NOT WORKING AS INTENDED
        private void RedrawNotes()
        {
            // TODO: Change these to center over current time.
            PianoTrack.Start = 0;
            PianoTrack.End = 1000;
            PianoTrack.Redraw();
        }
      
        //run on charms bar's play button click
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            stream.Play();
        }

        //run on Charms bar's record button click
        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            isRecording = true;
            //timer.Start();
        }

        //run after each second has passed while recording
        void timer_Tick(object sender, object e)
        {
            CurrentTime++;
        }

        //Run on Charms bar's stop button click
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            isRecording = false;
            timer.Stop();
            CurrentTime = 0;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveDlg.Visibility = Windows.UI.Xaml.Visibility.Visible;
            SaveOK.Visibility = Windows.UI.Xaml.Visibility.Visible;
            
        }
        private void SaveOK_Click(object sender, RoutedEventArgs e)
        {
            stream.saveStream(SaveDlg.Text);
            SaveDlg.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            SaveOK.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDlg.Visibility = Windows.UI.Xaml.Visibility.Visible;
            LoadOK.Visibility = Windows.UI.Xaml.Visibility.Visible;
            
        }
        private void LoadOK_Click(object sender, RoutedEventArgs e)
        {
            stream.loadStream(LoadDlg.Text);
            LoadDlg.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            LoadOK.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        private void TapStarted(object sender, RoutedEventArgs e) {
            //Change key color to pressed color
            var key = (Rectangle)sender;
            key.Fill = new SolidColorBrush(Colors.LightYellow);

            // Play tone
            var pitch = Convert.ToInt32(key.DataContext);
            stream.PlayPitch(pitch);
            
           // if(isRecording){
                StartTime = CurrentTime;
            //}
        }

        private void TapRelease(object sender, RoutedEventArgs e)
        {
            var key = (Rectangle)sender;
            var pitch = Convert.ToInt32(key.DataContext);
            //Change key color to default color
            key.Fill = new SolidColorBrush(SHARPS.Contains(pitch) ? Colors.Black : Colors.WhiteSmoke);
            stream.StopPitch(pitch);
            //if (isRecording) {
                // Create the new note
                var note = new Note() {
                    Device = device,
                    Parent = stream,
                    Pitch = pitch,
                    Start = StartTime, //Get saved start time
                    Length = CurrentTime - StartTime,//calculate length
                };
                stream.UpdateNote(note);
                RedrawNotes();
           // }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditPage));
        }
        private List<INoteSource> getSources() {
            List<INoteSource> sources = new List<INoteSource>();
            for (int i = 0; i < NOTE_PATHS.Length; i++) {
                sources.Add(new NoteSource() {
                    Device     = device,
                    Path       = NOTE_PATHS[i],
                    NoteLength = NOTE_INFO[i, NOTE_LENGTH],
                    LoopBegin  = NOTE_INFO[i, LOOP_BEGIN],
                    LoopLength = NOTE_INFO[i, LOOP_LENGTH],
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

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            Initialize();
        }


    }
}