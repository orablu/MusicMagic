﻿using System;
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
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void EditTracks_Clicked(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(EditPage));
        }

        private void NewGuitar_Clicked(object sender, RoutedEventArgs e) {
            ((App)Application.Current).NewGuitar();
            this.Frame.Navigate(typeof(GuitarPage));
        }

        private void NewPiano_Clicked(object sender, RoutedEventArgs e) {
            ((App)Application.Current).NewPiano();
            this.Frame.Navigate(typeof(PianoPage));
        }
    }
}
