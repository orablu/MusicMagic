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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicMagic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PianoPage : Page
    {


        //Given two lists, or however we want to store the notes value, draw the notes.
        //Requires much testing after ability to run code on my machine(i.e. sharpDX)
        //in order to decide on spacing @Andy
        private void Redraw(List<int> VertPlace, List<int> HorizPlace)
        {
            //Canvas.get/set(obj)
        }
      
        private void a_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush newColor= new SolidColorBrush();
            newColor.Color = Color.FromArgb(0, 0, 255, 255);
            newColor.Color = Color.FromArgb(0, 255, 255, 255);
            pianoA.Play();
        }

        private void b_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush newColor = new SolidColorBrush();
            newColor.Color = Color.FromArgb(0, 0, 255, 255);
            newColor.Color = Color.FromArgb(0, 255, 255, 255);
            pianoB.Play();

        }

        private void c_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush newColor = new SolidColorBrush();
            newColor.Color = Color.FromArgb(0, 0, 255, 255);
            newColor.Color = Color.FromArgb(0, 255, 255, 255);
            pianoC.Play();
        }

        private void d_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush newColor = new SolidColorBrush();
            newColor.Color = Color.FromArgb(0, 0, 255, 255);
            newColor.Color = Color.FromArgb(0, 255, 255, 255);
            pianoD.Play();
        }

        private void e_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush newColor = new SolidColorBrush();
            newColor.Color = Color.FromArgb(0, 0, 255, 255);
            newColor.Color = Color.FromArgb(0, 255, 255, 255);
            pianoE.Play();
        }

        private void f_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush newColor = new SolidColorBrush();
            newColor.Color = Color.FromArgb(0, 0, 255, 255);
            newColor.Color = Color.FromArgb(0, 255, 255, 255);
            pianoF.Play();
        }

        private void g_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SolidColorBrush newColor = new SolidColorBrush();
            newColor.Color = Color.FromArgb(0, 0, 255, 255);
            newColor.Color = Color.FromArgb(0, 255, 255, 255);
            pianoG.Play();
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
