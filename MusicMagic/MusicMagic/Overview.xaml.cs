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

namespace MusicMagic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Overview : Page
    {


        private void NewPiano_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PianoPage));
        }

        private void NewDrum_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DrumPage));
        }

        private void NewGuitar_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GuitarPage));
        }

        private void NewUploaded_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
