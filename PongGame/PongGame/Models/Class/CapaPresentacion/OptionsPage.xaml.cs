//Declaramos las librerias que vamos a utilizar
using Pong_Game.Modelos.Clases.CapaDatos;
using Pong_Game.Modelos.Clases.CapaNegocio;
using PongGame;
using Xamarin.Forms;

//Declaramos el namespace
namespace Pong_Game.Models.Class.CapaPresentacion
{
    //Definimos la clase y heredamos, esta podra ser declarada en varios lugares gracias al partial
    public partial class OptionsPage : ContentPage
    {

        //Declaramos los estilos que vamos a utilizar en botones, frames, imagenes
        private Style buttonStyle = new Style(typeof(Button)) { BaseResourceKey = "buttonStyle" };
        private Style buttonStyleHandicap = new Style(typeof(Button)) { BaseResourceKey = "buttonStyleHandicap" };
        private Style titleImage = new Style(typeof(Image)) { BaseResourceKey = "imageMenu" };
        private Style labelOptions = new Style(typeof(Button)) { BaseResourceKey = "labelOptions" };
        private Style frameBackground = new Style(typeof(Frame)) { BaseResourceKey = "frameBackground" };

        //Declaramos los labels que vamos a utilizar
        private Label sizeGameLabel;
        private Label speedGameLabel;
        private Label handicapGameLabel;
        private Label pointsGameLabel;

        //Declaramos el frame que ocupara el fondo de las opciones
        private Frame backGroundColor;

        //Declaramos el titulo que es una image
        private Image titleOptions;

        //Declaro las columnas laterales que cambiara de tamaño
        private ColumnDefinition leftColumn = new ColumnDefinition { Width = new GridLength(50) };
        private ColumnDefinition rightColumn = new ColumnDefinition { Width = new GridLength(50) };

        //Declaramos la cuadricula que contendra la pagina
        private Grid grid;

        //Declaramos tres cuadriculas para contener los botones para cambiar las opciones 
        private Grid[] gridButtons = new Grid[3];

        //Declaramos un array de tres botones para el tamaño del mapa
        private Button[] buttonsSize = new Button[3];

        //Declaramos un array de tres botones para la velocidad de la pelota
        private Button[] buttonsSpeed = new Button[3];

        //Declaramos un array de tres botones para la cantidad de puntos a marcar
        private Button[] buttonsPoints = new Button[3];

        //Declaramos tres botones, una para el handicap, otro para cancelar y otro para guardar las opciones
        private Button buttonSave;
        private Button buttonCancel;
        private Button buttonHandicap;

        //Utilizaremos estas variables para cambiar las opciones antes de guardarlas
        private int sizeOptions = 4;
        private int speedOptions = 4;
        private int pointsOptions = 4;

        //Declaramos el constructor
        public OptionsPage()
        {

            //Desactivo la barra de arriba
            NavigationPage.SetHasNavigationBar(this, false);

            //Asociamos el constructor con su layout
            InitializeComponent();

            //Instancio todos los objetos
            InstantiateObjects();

            //Inserto todos los objetos en las cuadriculas
            InsertObjects();

            //Cargo las opciones para cambiar la interfaz
            LoadOptions();

            //Creo los comportamientos de los botones
            SetBehaviors();

            //Creamos un scrollview y le añadimos el grid
            ScrollView scrollView = new ScrollView { Content = grid };

            //Añadimos el scrollview al contenido de esta pagina
            Content = scrollView;

        }

        //Metodo para cargar las opciones y cambiar la interfaz
        private void LoadOptions() {
            
            if(GameController.currentGamePoints == GameController.gamePoints[0])
            {
                this.buttonsPoints[0].BackgroundColor = Color.White;
                this.buttonsPoints[1].BackgroundColor = Color.Black;
                this.buttonsPoints[2].BackgroundColor = Color.Black;
            }
            else if (GameController.currentGamePoints == GameController.gamePoints[1])
            {
                this.buttonsPoints[0].BackgroundColor = Color.White;
                this.buttonsPoints[1].BackgroundColor = Color.White;
                this.buttonsPoints[2].BackgroundColor = Color.Black;
            }
            else if (GameController.currentGamePoints == GameController.gamePoints[2])
            {
                this.buttonsPoints[0].BackgroundColor = Color.White;
                this.buttonsPoints[1].BackgroundColor = Color.White;
                this.buttonsPoints[2].BackgroundColor = Color.White;
            }

            if (GameController.currentBallSpeed == GameController.ballSpeed[0])
            {
                this.buttonsSpeed[0].BackgroundColor = Color.White;
                this.buttonsSpeed[1].BackgroundColor = Color.Black;
                this.buttonsSpeed[2].BackgroundColor = Color.Black;
            }
            else if (GameController.currentBallSpeed == GameController.ballSpeed[1])
            {
                this.buttonsSpeed[0].BackgroundColor = Color.White;
                this.buttonsSpeed[1].BackgroundColor = Color.White;
                this.buttonsSpeed[2].BackgroundColor = Color.Black;
            }
            else if (GameController.currentBallSpeed == GameController.ballSpeed[2])
            {
                this.buttonsSpeed[0].BackgroundColor = Color.White;
                this.buttonsSpeed[1].BackgroundColor = Color.White;
                this.buttonsSpeed[2].BackgroundColor = Color.White;
            }

            if (GameController.currentIASpeed == GameController.IASpeed[0])
            {
                this.buttonsSize[0].BackgroundColor = Color.White;
                this.buttonsSize[1].BackgroundColor = Color.Black;
                this.buttonsSize[2].BackgroundColor = Color.Black;
            }
            else if (GameController.currentIASpeed == GameController.IASpeed[1])
            {
                this.buttonsSize[0].BackgroundColor = Color.White;
                this.buttonsSize[1].BackgroundColor = Color.White;
                this.buttonsSize[2].BackgroundColor = Color.Black;
            }
            else if (GameController.currentIASpeed == GameController.IASpeed[2])
            {
                this.buttonsSize[0].BackgroundColor = Color.White;
                this.buttonsSize[1].BackgroundColor = Color.White;
                this.buttonsSize[2].BackgroundColor = Color.White;
            }

            if (GameController.currentHandicapPlayer.Equals(GameController.handicapPlayer[0]))
            {
                this.buttonHandicap.Text = GameController.handicapPlayer[0];
            } 
            else if (GameController.currentHandicapPlayer.Equals(GameController.handicapPlayer[1])) {
                this.buttonHandicap.Text = GameController.handicapPlayer[1];
            }
            else if (GameController.currentHandicapPlayer.Equals(GameController.handicapPlayer[2]))
            {
                this.buttonHandicap.Text = GameController.handicapPlayer[2];
            }

        }

        //Metodo para insertar los objetos en las cuadriculas
        private void InsertObjects() {

            //Inserto en la cuadricula el frame que sera el fondo y aumento lo que ocupa
            this.grid.Children.Add(this.backGroundColor, 1, 1);
            Grid.SetColumnSpan(this.backGroundColor, 3);
            Grid.SetRowSpan(this.backGroundColor, 5);

            //Inserto la imagen del titulo en la cuadricula y aumento lo que ocupa
            this.grid.Children.Add(this.titleOptions, 1, 0);
            Grid.SetColumnSpan(this.titleOptions, 3);

            //Inserto el label del tamaño del mapa
            this.grid.Children.Add(this.sizeGameLabel, 1, 1);
            Grid.SetColumnSpan(this.sizeGameLabel, 2);

            //Inserto el label de la velocidad de la pelota
            this.grid.Children.Add(this.speedGameLabel, 1, 2);
            Grid.SetColumnSpan(this.speedGameLabel, 2);

            //Inserto el label del handicap
            this.grid.Children.Add(this.handicapGameLabel, 1, 3);
            Grid.SetColumnSpan(this.handicapGameLabel, 2);

            //Inserto el label de los puntos
            this.grid.Children.Add(this.pointsGameLabel, 1, 4);
            Grid.SetColumnSpan(this.pointsGameLabel, 2);

            //Inserto el boton para cancelar las opciones que hemos cambiado
            this.grid.Children.Add(this.buttonCancel, 1, 5);
            Grid.SetColumnSpan(this.buttonCancel, 2);

            //Inserto el boton para guardar los datos cambiados
            this.grid.Children.Add(this.buttonSave, 2, 5);
            Grid.SetColumnSpan(this.buttonSave, 2);

            //Inserto el boton del handicap
            this.grid.Children.Add(this.buttonHandicap, 3, 4);

            //Inserto cada boton en las cuadriculas de las opciones
            for (int i = 0; i < 3; i++)
            {

                this.gridButtons[0].Children.Add(this.buttonsSize[i], i, 1);
                this.gridButtons[1].Children.Add(this.buttonsSpeed[i], i, 1);
                this.gridButtons[2].Children.Add(this.buttonsPoints[i], i, 1);

            }

            //Inserto las cuadriculas que contiene ahora los botones en la cuadricula general
            for (int i = 0; i < 3; i++)
            {

                this.grid.Children.Add(this.gridButtons[i], 3, i + 1);

            }

        }

        //Metodo para instanciar los objetos
        private void InstantiateObjects()
        {

            //Instanciamos la cuadricula general que contendra todos los elementos
            this.grid = new Grid
            {
                RowDefinitions = {

                new RowDefinition { Height = new GridLength(200) },
                new RowDefinition{ Height = new GridLength(100) },
                new RowDefinition{ Height = new GridLength(100) },
                new RowDefinition{ Height = new GridLength(100) },
                new RowDefinition{ Height = new GridLength(100) },
                new RowDefinition{ Height = new GridLength(100) },
                new RowDefinition()},

                ColumnDefinitions = {

                    leftColumn,
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    rightColumn}
            };

            //Instancio un frame que contendra las opciones y le asociamos sus estilos
            this.backGroundColor = new Frame { Style = frameBackground };

            //Instancio la imagen que sera el titulo de la pagina
            this.titleOptions = new Image { Style = titleImage, Source = "OptionsTitle" };

            //Instancio los labels de las opciones y les asocio estilos
            this.sizeGameLabel = new Label { Text = "IA speed", Style = labelOptions };
            this.speedGameLabel = new Label { Text = "Ball speed", Style = labelOptions };
            this.handicapGameLabel = new Label { Text = "Game points", Style = labelOptions };
            this.pointsGameLabel = new Label { Text = "Handicap", Style = labelOptions };

            //Instancio tres botones y les asocio estilos
            this.buttonHandicap = new Button { Style = buttonStyleHandicap, Text = "None"};
            this.buttonSave = new Button { Text = "  OK  ", Style = buttonStyle, FontSize = 10, Padding = 10, WidthRequest = 200, Margin = 20, HorizontalOptions = LayoutOptions.End };
            this.buttonCancel = new Button { Text = "Cancel", Style = buttonStyle, FontSize = 10, Padding = 10, WidthRequest = 200, Margin = 20, HorizontalOptions = LayoutOptions.Start };

            //Creo un bucle para instanciar las cuadriculas de los botones
            for (int i = 0; i < this.gridButtons.Length; i++)  {

                gridButtons[i] = new Grid  {

                    RowDefinitions = {
                    new RowDefinition{ Height = new GridLength(20) },
                    new RowDefinition{ Height = new GridLength(20) },
                    new RowDefinition{ Height = new GridLength(20) } },
                    ColumnDefinitions = {
                    new ColumnDefinition{ Width = new GridLength(20) },
                    new ColumnDefinition{ Width = new GridLength(20) },
                    new ColumnDefinition{ Width = new GridLength(20) }
                }
                };

            }

            //Creo un bucle para instanciar todos los botones de cada opcion
            for (int i = 0; i < 3; i++)  {

                this.buttonsSize[i] = new Button { Style = buttonStyle, BorderWidth = 2 };
                this.buttonsSpeed[i] = new Button { Style = buttonStyle, BorderWidth = 2 };
                this.buttonsPoints[i] = new Button { Style = buttonStyle, BorderWidth = 2 };

            }

        }

        //Metodo para establecer el comportamiento de los botones
        private void SetBehaviors()
        {

            //Creamos un comportamiento para el primer boton de Size
            this.buttonsSize[0].Clicked += (sender, args) =>
            {

                this.buttonsSize[0].BackgroundColor = Color.White;
                this.buttonsSize[1].BackgroundColor = Color.Black;
                this.buttonsSize[2].BackgroundColor = Color.Black;
                this.sizeOptions = 1;

            };

            //Creamos un comportamiento para el segundo boton de Size
            this.buttonsSize[1].Clicked += (sender, args) =>
            {

                this.buttonsSize[0].BackgroundColor = Color.White;
                this.buttonsSize[1].BackgroundColor = Color.White;
                this.buttonsSize[2].BackgroundColor = Color.Black;
                this.sizeOptions = 2;

            };

            //Creamos un comportamiento para el tercer boton de Size
            this.buttonsSize[2].Clicked += (sender, args) =>
            {

                this.buttonsSize[0].BackgroundColor = Color.White;
                this.buttonsSize[1].BackgroundColor = Color.White;
                this.buttonsSize[2].BackgroundColor = Color.White;
                this.sizeOptions = 3;

            };

            //Creamos un comportamiento para el primer boton de Speed
            this.buttonsSpeed[0].Clicked += (sender, args) =>
            {

                this.buttonsSpeed[0].BackgroundColor = Color.White;
                this.buttonsSpeed[1].BackgroundColor = Color.Black;
                this.buttonsSpeed[2].BackgroundColor = Color.Black;
                this.speedOptions = 1;

            };

            //Creamos un comportamiento para el segundo boton de Speed
            this.buttonsSpeed[1].Clicked += (sender, args) =>
            {

                this.buttonsSpeed[0].BackgroundColor = Color.White;
                this.buttonsSpeed[1].BackgroundColor = Color.White;
                this.buttonsSpeed[2].BackgroundColor = Color.Black;
                this.speedOptions = 2;

            };

            //Creamos un comportamiento para el tercer boton de Speed
            this.buttonsSpeed[2].Clicked += (sender, args) =>
            {

                this.buttonsSpeed[0].BackgroundColor = Color.White;
                this.buttonsSpeed[1].BackgroundColor = Color.White;
                this.buttonsSpeed[2].BackgroundColor = Color.White;
                this.speedOptions = 3;

            };

            //Creamos un comportamiento para el primer boton de Points
            this.buttonsPoints[0].Clicked += (sender, args) =>
            {

                this.buttonsPoints[0].BackgroundColor = Color.White;
                this.buttonsPoints[1].BackgroundColor = Color.Black;
                this.buttonsPoints[2].BackgroundColor = Color.Black;
                this.pointsOptions = 1;

            };

            //Creamos un comportamiento para el segundo boton de Points
            this.buttonsPoints[1].Clicked += (sender, args) =>
            {

                this.buttonsPoints[0].BackgroundColor = Color.White;
                this.buttonsPoints[1].BackgroundColor = Color.White;
                this.buttonsPoints[2].BackgroundColor = Color.Black;
                this.pointsOptions = 2;

            };

            //Creamos un comportamiento para el tercer boton de Points
            this.buttonsPoints[2].Clicked += (sender, args) =>
            {

                this.buttonsPoints[0].BackgroundColor = Color.White;
                this.buttonsPoints[1].BackgroundColor = Color.White;
                this.buttonsPoints[2].BackgroundColor = Color.White;
                this.pointsOptions = 3;

            };

            //Creamos un comportamiento al acceder al boton de handicap
            this.buttonHandicap.Clicked += (sender, args) =>
            {

                if (buttonHandicap.Text.Equals("None"))
                {

                    buttonHandicap.Text = "P1";

                }
                else if (buttonHandicap.Text.Equals("P1"))
                {

                    buttonHandicap.Text = "IA";

                }
                else if (buttonHandicap.Text.Equals("IA"))
                {

                    buttonHandicap.Text = "None";

                }

            };

            //Creamos un comportamiento para el boton cancelar
            this.buttonCancel.Clicked += (sender, args) =>
            {

                Application.Current.MainPage.Navigation.PopAsync();

            };

            //Creamos un comportamiento para el boton guardar
            this.buttonSave.Clicked += (sender, args) =>
            {

                //Si hemos cambiado la variable guardamos la variable
                if (this.sizeOptions != 4)
                {

                    if (this.sizeOptions == 1)
                    {
                        GameController.currentIASpeed = GameController.IASpeed[0];
                    }
                    else if (this.sizeOptions == 2)
                    {
                        GameController.currentIASpeed = GameController.IASpeed[1];
                    }
                    else if (this.sizeOptions == 3)
                    {
                        GameController.currentIASpeed = GameController.IASpeed[2];
                    }


                }

                //Si hemos cambiado la variable guardamos la variable
                if (this.speedOptions != 4)
                {
                    if (this.speedOptions == 1)
                    {
                        GameController.currentBallSpeed = GameController.ballSpeed[0];
                    }
                    else if (this.speedOptions == 2)
                    {
                        GameController.currentBallSpeed = GameController.ballSpeed[1];
                    }
                    else if (this.speedOptions == 3)
                    {
                        GameController.currentBallSpeed = GameController.ballSpeed[2];
                    }

                }

                //Si hemos cambiado la variable guardamos la variable
                if (this.pointsOptions != 4)
                {
                    if (this.pointsOptions == 1)
                    {
                        GameController.currentGamePoints = GameController.gamePoints[0];
                    }
                    else if (this.pointsOptions == 2)
                    {
                        GameController.currentGamePoints = GameController.gamePoints[1];
                    }
                    else if (this.pointsOptions == 3)
                    {
                        GameController.currentGamePoints = GameController.gamePoints[2];
                    }

                }

                //Guardamos la variable de handicap
                if (this.buttonHandicap.Text.Equals(GameController.handicapPlayer[0]))
                {
                    GameController.currentHandicapPlayer = GameController.handicapPlayer[0];
                }
                else if (this.buttonHandicap.Text.Equals(GameController.handicapPlayer[1]))
                {
                    GameController.currentHandicapPlayer = GameController.handicapPlayer[1];
                }
                else if (this.buttonHandicap.Text.Equals(GameController.handicapPlayer[2]))
                {
                    GameController.currentHandicapPlayer = GameController.handicapPlayer[2];
                }

                //Llamamos al metodo para guardar los datos
                SaveSettings.SaveConfiguration();

                //Cerramos la pagina actual
                Application.Current.MainPage.Navigation.PopAsync();

            };

        }

        //Metodo que cuando cambia la anchura del telefono hace una cosa u otra
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            //Al detectar que la anchura es mayor de la debida cambia el fondo
            if (width < height)
            {

                //Establecemos una imagen de fondo vertical
                this.BackgroundImageSource = "Background";

                //Cambio la anchura de las columnas laterales
                this.leftColumn.Width = 50;
                this.rightColumn.Width = 50;

                //Cambiamos la anchura de los botones cancelar y start
                this.buttonCancel.WidthRequest = 125;
                this.buttonSave.WidthRequest = 125;

            }
            else
            {

                //Cambio la anchura de las columnas laterales
                this.leftColumn.Width = 75;
                this.rightColumn.Width = 75;

                //Cambiamos la anchura de los botones cancelar y start
                this.buttonCancel.WidthRequest = 220;
                this.buttonSave.WidthRequest = 220;

                //Establecemos una imagen de fondo horizontal
                this.BackgroundImageSource = "BackgroundLand";

            }

        }

        //Metodo que se ejecuta al mostrarse la pagina
        protected override void OnAppearing()
        {

            //Registramos la implementacion de la plataforma para que xamarin la localice
            DependencyService.Register<INativePages>();

            //Llamo al metodo de la interfaz para inicar la cancion desde android
            DependencyService.Get<INativePages>().PlaySong();

        }

    }

}