using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PongGame.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

/*Declaro el espacio de nombres
  Invocamos una funcionalidad nativa de la plataforma xamarin para usar herramientas de android
  Registramos una implementacion de una interfaz*/
[assembly: Xamarin.Forms.Dependency(typeof(NativePages))]
namespace PongGame.Droid
{

    public class NativePages : INativePages
    {

        //Creo un recurso para hacer sonar la cancion en android
        MediaPlayer songTheme = MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.songtheme);

        //Metodo para iniciar la cancion
        public void PlaySong()
        {

            //Establezco un volumen
            songTheme.SetVolume(150, 150);

            //Establezco su propiedad a true para que este sonando todo el tiempo
            songTheme.Looping = true;

            //Le indico que comience la cancion
            songTheme.Start();

        }

        //Metodo para parar la cancion
        public void StopSong()
        {

            songTheme.Pause();

        }

        //Constructor de la clase
        public NativePages() { }
        //Metodo para iniciar una nueva activity
        public void StartActivityInAndroid()
        {
            //Declaro un nuevo intent hacia la activity game que es donde funcionara la aplicacion
            var intent = new Intent(Android.App.Application.Context, typeof(GameActivity));

            //Inicio el intent hacia la nueva activity
            Forms.Context.StartActivity(intent);

        }


    }
}