using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace MusicMagic
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application {
        public XAudio2 Device;
        MasteringVoice master;
        List<INoteSource> pianoSources, guitarSources;

        public INoteStream CurrentNoteStream {
            get;
            set;
        }

        public List<INoteStream> Streams {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Streams = new List<INoteStream>();
            Device = new XAudio2();
            master = new MasteringVoice(Device);
            pianoSources = getPianoSources();
            guitarSources = getGuitarSources();
        }

        public void NewPiano() {
            var newstream = new NoteStream {
                Type = NoteType.Piano,
                Sources = pianoSources,
            };

            Streams.Add(newstream);
            CurrentNoteStream = newstream;
        }

        public void NewGuitar() {
            var newstream = new NoteStream {
                Type = NoteType.Guitar,
                Sources = guitarSources,
            };

            Streams.Add(newstream);
            CurrentNoteStream = newstream;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        private List<INoteSource> getPianoSources() {
            List<INoteSource> sources = new List<INoteSource>();
            for (int i = 0; i < PIANO_PATHS.Length; i++) {
                sources.Add(new NoteSource() {
                    Device     = Device,
                    Path       = PIANO_PATHS[i],
                    NoteLength = PIANO_INFO[i, NOTE_LENGTH],
                    LoopBegin  = PIANO_INFO[i, LOOP_BEGIN],
                    LoopLength = PIANO_INFO[i, LOOP_LENGTH],
                });
            }
            return sources;
        }
        
        private List<INoteSource> getGuitarSources() {
            List<INoteSource> sources = new List<INoteSource>();
            for (int i = 0; i < GUITAR_PATHS.Length; i++) {
                sources.Add(new NoteSource() {
                    Device     = Device,
                    Path       = GUITAR_PATHS[i],
                    NoteLength = GUITAR_INFO[i, NOTE_LENGTH],
                    LoopBegin  = GUITAR_INFO[i, LOOP_BEGIN],
                    LoopLength = GUITAR_INFO[i, LOOP_LENGTH],
                });
            }
            return sources;
        }

        private readonly string[] PIANO_PATHS = new string[] {
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
        private readonly int[,] PIANO_INFO = new int[,] {
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

        private readonly string[] GUITAR_PATHS = new string[] {
            //@"Resources\Guitar\Guitar_E_highest.wav",
            //@"Resources\Guitar\Guitar_F_highest.wav",
            //@"Resources\Guitar\Guitar_F#_highest.wav",
            //@"Resources\Guitar\Guitar_G_highest.wav",
            //@"Resources\Guitar\Guitar_G#_highest.wav",
            ////@"Resources\Guitar\Guitar_A_highest.wav",
            
            //@"Resources\Guitar\Guitar_B_highest.wav",
            //@"Resources\Guitar\Guitar_C_highest.wav",
            //@"Resources\Guitar\Guitar_C#_highest.wav",
            //@"Resources\Guitar\Guitar_D_highest.wav",
            //@"Resources\Guitar\Guitar_Eb_highest.wav",

            //@"Resources\Guitar\Guitar_G_high.wav",
            //@"Resources\Guitar\Guitar_G#_high.wav",
            //@"Resources\Guitar\Guitar_A_high.wav",
            //@"Resources\Guitar\Guitar_Bb_high.wav",
            //@"Resources\Guitar\Guitar_B_high.wav",

            //@"Resources\Guitar\Guitar_D_high.wav",
            //@"Resources\Guitar\Guitar_Eb_high.wav",
            //@"Resources\Guitar\Guitar_E_high.wav",
            //@"Resources\Guitar\Guitar_F_high.wav",
            //@"Resources\Guitar\Guitar_F#_high.wav",

            //@"Resources\Guitar\Guitar_A_med.wav",
            //@"Resources\Guitar\Guitar_Bb_med.wav",
            //@"Resources\Guitar\Guitar_B_med.wav",
            //@"Resources\Guitar\Guitar_C_med.wav",
            //@"Resources\Guitar\Guitar_C#_med.wav",

            //@"Resources\Guitar\Guitar_E_low.wav",
            //@"Resources\Guitar\Guitar_F_low.wav",
            //@"Resources\Guitar\Guitar_F#_low.wav",
            //@"Resources\Guitar\Guitar_G_low.wav",
            //@"Resources\Guitar\Guitar_G#_low.wav",
        };

        private readonly int[,] GUITAR_INFO = new int[,] {
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 },
            //{ 1540, 0, 0 },
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 },
            //{ 1540, 0, 0 },
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 },
            //{ 1540, 0, 0 },
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 }, 
            //{ 1540, 0, 0 },
            //{ 1540, 0, 0 },
        };
    }
}
