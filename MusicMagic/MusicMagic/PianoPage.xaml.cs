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
    public sealed partial class PianoPage : Page
    {
  

        //the following methods will trigger when the corresponding key is pressed
        //probably where you want to put some music stuff
        //I named the white keys a-g, high_a-g
        //and if you want to do anything with the black keys, its the same naming but with _sharp
        //I don't know how music works but it seemed like a sensical naming scheme

        //TODO: generate note on staff when key is pressed.
            //Planned Implementation: when tapped event occurs, draw new ellipse at y-coordinate corresponding to 
            //key pressed, and x-coordinate corresponding to when it was pressed relative to when recording started
        //by planned I mean, I wrote the code, I just want to make sure all this stuff works with you guys before I put it in,
        //and its dependant on how we're keeping track of time

        //TODO: flash color when key is pressed
        private void a_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void b_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void c_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void d_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void e_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void f_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void g_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_a_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_b_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_c_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_d_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_e_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_f_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_g_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void a_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void b_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void c_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void d_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void e_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void f_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void g_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_a_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_b_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void high_c_sharp_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
