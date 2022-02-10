using Pong_Game.Models.Class.CapaPresentacion;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PongGame
{

    //Declaro la interfaz que voy a utilizar
    public interface INativePages
    {
        //Definimos metodos para la interfaz que hay que heredar
        //Metodo para abrir una activity en android del tipo GameActivity
        void StartActivityInAndroid();

        //Metodo para hacer sonar la cancion
        void PlaySong();

        //Metodo para parar la cancion
        void StopSong();
    }

    public partial class App : Application
    {
        //Variable para controlar cuando pasamos al modo de juego
        //Porque hay un conflicto entre el lifecycle de application y activity
        public static bool onSetGame;

        public App()
        {
            //Desactivo la barra de navegacion
            NavigationPage.SetHasNavigationBar(this, false);

            //Llama a loadfromxaml de global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml, o sea a su XML
            InitializeComponent();

            //Iniciamos una nueva pagina
            MainPage = new NavigationPage(new MainPage());

        }

        //Metodos del ciclo de vida de la application
        protected override void OnStart()
        {
            //Registramos la implementacion de la plataforma para que xamarin la localice
            DependencyService.Register<INativePages>();

            //Llamo al metodo de la interfaz para inicar la cancion desde android
            DependencyService.Get<INativePages>().PlaySong();
        }
        protected override void OnSleep()
        {

            //Limitamos que deje de sonar la cancion si pasamos desde la application a la activity en android
            if (!onSetGame)
            {
                //Registramos la implementacion de la plataforma para que xamarin la localice
                DependencyService.Register<INativePages>();

                //Llamo al metodo de la interfaz para parar la cancion desde android
                DependencyService.Get<INativePages>().StopSong();

            }

        }
        protected override void OnResume()
        {
            //Registramos la implementacion de la plataforma para que xamarin la localice
            DependencyService.Register<INativePages>();

            //Llamo al metodo de la interfaz para inicar la cancion desde android
            DependencyService.Get<INativePages>().PlaySong();

        }

    }
}
