//Importo las librerias necesarias
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Pong_Game.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

//Declaro el namespace
namespace PongGame.Droid
{

    //Declaro la activity
    [Activity(Label = "GameActivity", ScreenOrientation = ScreenOrientation.Landscape)]
    public class GameActivity : Activity
    {

        //Declaro mi surfaceview
        PongView pongView = null;

        //Metodo que se ejecuta segun el ciclo de vida de una activity
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Declaro el tamaño de la pantalla
            var metrics = Resources.DisplayMetrics;
            var height = metrics.HeightPixels;
            var width = metrics.WidthPixels;

            //Inserto en el punto el tamaño
            Android.Graphics.Point size = new Android.Graphics.Point();
            size.X = width;
            size.Y = height;

            //Creo la surfaceview
            pongView = new PongView(this, size.X, size.Y);
            SetContentView(pongView);

        }

        //Metodos que se ejecutan segun cerremos o abramos la aplicacion
        protected override void OnResume()
        {
            base.OnResume();
            //Registramos la implementacion de la plataforma para que xamarin la localice
            DependencyService.Register<INativePages>();

            //Llamo al metodo de la interfaz para inicar la cancion desde android
            DependencyService.Get<INativePages>().PlaySong();
            pongView.Resume();
        }

        // This method executes when the player quits the game
        protected override void OnPause()
        {
            base.OnPause();
            //Registramos la implementacion de la plataforma para que xamarin la localice
            DependencyService.Register<INativePages>();

            //Llamo al metodo de la interfaz para parar la cancion desde android
            DependencyService.Get<INativePages>().StopSong();
            pongView.Pause();
        }

    }

}