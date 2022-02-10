//Añado las librerias necesarias
using Xamarin.Forms;
using Pong_Game.Modelos.Clases.CapaDatos;
using PongGame;

//Declaro un namespace
namespace Pong_Game.Models.Class.CapaPresentacion
{

    //Declaro la clase principal y heredo
    public partial class MainPage : ContentPage
    {
       
        //Declaro los estilos que voy a utilizar
        private Style buttonStyle = new Style(typeof(Button)) { BaseResourceKey = "buttonStyle"};
        private Style titleImage = new Style(typeof(Image)) { BaseResourceKey = "imageMenu" };
        
        //Declaro la cuadricula que voy a utilizar, las columnas y filas
        private RowDefinition topRow = new RowDefinition { Height = new GridLength(250) };
        private Grid grid;

        //Declaro los botones
        private Button startButton; 
        private Button optionsButton;
        private Button exitButton;

        //Declaro la imagen del titulo del videojuego
        private Image titleMenu;

        //Constructor de la pagina
        public MainPage() {

            //Establezco la variable para controlar el lifecycle en false
            App.onSetGame = false;

            //Cargamos los datos de las variables
            SaveSettings.LoadSettings();

            //Asocio el XML a la clase
            InitializeComponent();

            //Desactivo la barra de arriba
            NavigationPage.SetHasNavigationBar(this, false);

            //Instancio los elementos que voy a utilizar para la interfaz
            InstantiateObjects();

            //Inserto los elementos en la cuadricula
            InsertObjects();

            //Le doy un comportamiento a los botones del menu
            SetBehaviors();

            //Creamos un scrollview y le añadimos el grid
            ScrollView scrollView = new ScrollView { Content = grid };

            //Añadimos el scrollview al contenido de esta pagina
            Content = scrollView;

        }

        //Metodo para establecer el funcionamiento de los botones
        private void SetBehaviors()
        {
            //Listener que se ejecuta al pulsar start
            this.startButton.Clicked += (sender, args) =>
            {

                //Si estamos en un dispositivo android accedemos
                if (Device.RuntimePlatform.Equals("Android"))
                {

                    //Impido que deje de sonar la cancion al pasar de una application a una activity
                    App.onSetGame = true;

                    //Registramos la implementacion de la plataforma para que xamarin la localice
                    DependencyService.Register<INativePages>();

                    //Llamo al metodo de la interfaz para inicar una activity de android
                    DependencyService.Get<INativePages>().StartActivityInAndroid();

                }

            };

            //Listener que se ejecuta al pulsar options
            this.optionsButton.Clicked += (sender, args) =>
            {

                //Abrimos una nueva pagina hacia opciones
                Navigation.PushAsync(new OptionsPage());

            };

            //Listener que se ejecuta al pulsar exit
            this.exitButton.Clicked += (sender, args) =>
            {

                //Cerramos la aplicacion
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            };
        }

        //Metodo para insertar objetos en la cuadricula
        private void InsertObjects()
        {
            //Añadimos todos los elementos al grid
            this.grid.Children.Add(titleMenu, 1, 0);
            this.grid.Children.Add(startButton, 1, 2);
            this.grid.Children.Add(optionsButton, 1, 3);
            this.grid.Children.Add(exitButton, 1, 4);
        }

        //Metodo para instaciar objetos
        private void InstantiateObjects()
        {
            //Iniciamos los botones e imagenes que vamos a usar y le asociamos estilos
            this.startButton = new Button { Style = buttonStyle, Text = "Start" };
            this.optionsButton = new Button { Style = buttonStyle, Text = "Options" };
            this.exitButton = new Button { Style = buttonStyle, Text = "Exit" };
            this.titleMenu = new Image { Style = titleImage, Source = "Title" };

            //Instancio la cuadricula
            this.grid = new Grid
            {
                RowDefinitions = { topRow,
                new RowDefinition{ Height = new GridLength(40) },
                new RowDefinition{ Height = new GridLength(65) },
                new RowDefinition{ Height = new GridLength(65) },
                new RowDefinition{ Height = new GridLength(65) },
                new RowDefinition{ Height = new GridLength(40) }},
                ColumnDefinitions = {
                new ColumnDefinition(),
                new ColumnDefinition{ Width = new GridLength(300)},
                new ColumnDefinition()}
            };

        }

        //Metodo que cuando  cambia la anchura del telefono hace una cosa u otra
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            
            //Al detectar que la anchura es mayor de la debida cambia el fondo
            if (width >= 400 & width <= 600)
            {
                this.BackgroundImageSource = "Background";
                this.topRow.Height = 250;


            } else if (width >= 600){

                this.BackgroundImageSource = "BackgroundLand";
                this.topRow.Height = 200;
            }

        }

        protected override void OnAppearing()
        {

            //Establezco la variable para controlar el lifecycle en false
            App.onSetGame = false;

            //Registramos la implementacion de la plataforma para que xamarin la localice
            DependencyService.Register<INativePages>();

            //Llamo al metodo de la interfaz para inicar la cancion desde android
            DependencyService.Get<INativePages>().PlaySong();

        }

    }

}