//Importo las librerias necesarias
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Pong_Game.Modelos.Clases.CapaNegocio;
using PongGame.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Declaro el namespace
namespace Pong_Game.Droid
{
    //Declaro la clase y heredo
    [Activity(Label = "PongView")]
    public class PongView: SurfaceView
    {
        //Declaro el hilo que ejecutara nuestras instrucciones
        Thread mainThread = null;

        //Surface para pintar canvas
        ISurfaceHolder mainHolder = null;

        //Variable que dira si el juego esta ejecutandose
        bool gameRunning;

        //Variable que nos indica si el juego esta en pausa
        bool gamePaused = true;

        //Declaramos un canvas para dibujar y un paint que representa como seran
        Canvas mainCanvas;
        Paint drawPaint;

        //Representa los FPS del juego, las actualizaciones por segundo
        long FPS;

        // The size of the screen in pixels
        int mainDisplayX;
        int mainDisplayY;

        //Declaramos nuestra barra del jugador y un bot
        Bar playerBar;
        Bar IABar;

        //Declaramos la pelota
        Ball ball;

        //Declaramos los puntos de cada jugador
        int playerPoints = 0;
        int IAPoints = 0;
        bool pointPlayer = false;
        bool pointIA = false;
        bool winnerIA = false;
        bool winnderPlayer = false;

        public PongView(Context context, int x, int y) : base(context)
        {

            //Obtenemos el tamaño de la pantalla
            this.mainDisplayX = x;
            this.mainDisplayY = y;

            //Inicializamos la paint que lo usaremos para pintar los elementos
            this.drawPaint = new Paint();

            //Obtenemos el holder de esta surfaceview
            this.mainHolder = this.Holder;

            //Instanciamos nuestra barra del jugador
            this.playerBar = new Bar(this.mainDisplayX, this.mainDisplayY,2,2, false);
            this.IABar = new Bar(this.mainDisplayX, this.mainDisplayY, 2, 6, true);

            //Establecemos el handicap
            if (GameController.currentHandicapPlayer.Equals("IA"))
            {

                this.IAPoints = 2;

            } else if (GameController.currentHandicapPlayer.Equals("P1")) {
            
            
                this.playerPoints = 2;
            
            }

            //Instanciamos la pelota
            this.ball = new Ball(this.mainDisplayX, this.mainDisplayY);

            //Situamos la pelota en el centro
            RestartBall();

        }

        //Metodo para posicionar la pelota, sumar puntos y detener el juego
        public void RestartBall()
        {
            this.ball.Reset(this.mainDisplayX, this.mainDisplayY);

            if (this.pointIA)
            {
                this.IAPoints++;
            }

            if (this.pointPlayer)
            {
                this.playerPoints++;
            }

            this.pointIA = false;
            this.pointPlayer = false;

            if(this.IAPoints == GameController.currentGamePoints)
            {
                this.winnerIA = true;
            }

            if(this.playerPoints == GameController.currentGamePoints)
            {
                this.winnderPlayer = true;
            }

        }

        
        //Obtener milisegundos en tiempo real
        private static readonly DateTime Jan1st1970 = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        //Devolvemos los segundos en tiempo real
        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        //Metodo donde se ejecuta el juego
        public void Run()
        {

            //El bucle continua mientras el juego siga en este modo
            while (this.gameRunning)
            {

                //Llamo al metodo para obtener los milisegundos
                long startFrameTime = CurrentTimeMillis();

                //Actualizamos el frame si el juego no esta en pausa
                if (!this.gamePaused)
                {
                    Update();
                }

                //Actualizamos las posiciones de los elementos en pantalla
                Draw();

                //Obtengo la diferencia
                long timeThisFrame = CurrentTimeMillis() - startFrameTime;

                //Si la diferencia es mayor o igual que 1 establecemos el valor de los FPS
                if (timeThisFrame >= 1)
                {
                    this.FPS = 1000 / timeThisFrame;
                }

            }

        }

        //Actualizo todo los elementos
        public void Update()
        {

            //Muevo la posicion del jugador
            playerBar.UpdateBar(this.FPS);
            IABar.UpdateBar(this.FPS);

            //Muevo la posicion de la pelota
            ball.Update(this.FPS);

            //Detectamos la colision entre la pelota y el jugador
            if (RectF.Intersects(this.playerBar.GetRect(), this.ball.GetRect()))
            {
                //Movemos la pelota y cambiamos su direccion
                this.ball.ReverseVelocityY();
                this.ball.MoveY(this.playerBar.GetRect().Top - 2);

            }

            //Detectamos la colision entre la pelota y el bot
            if (RectF.Intersects(this.IABar.GetRect(), this.ball.GetRect()))
            {
                //Movemos la pelota y cambiamos su direccion
                this.ball.ReverseVelocityY();
                this.ball.MoveY(this.IABar.GetRect().Top - 2);

            }

            //Si la pelota colisiona con la pantalla inferior
            if (this.ball.GetRect().Bottom >= this.mainDisplayY)
            {
                this.ball.ReverseVelocityY();
                this.ball.MoveY(mainDisplayY - 2);
                this.pointIA = true;
                RestartBall();
            }

            //Si la pelota colisiona con la pantalla superior
            if (this.ball.GetRect().Top <= 0)
            {
                this.ball.ReverseVelocityY();
                this.ball.MoveY(12);
                this.pointPlayer = true;
                RestartBall();
            }

            //Si la pelota colisiona con la izquierda
            if (this.ball.GetRect().Left <= 0)
            {
                this.ball.ReverseVelocityX();
                this.ball.MoveX(2);

            }

            //Si la pelota colisiona con la derecha
            if (this.ball.GetRect().Right >= mainDisplayX)
            {
                this.ball.ReverseVelocityX();
                this.ball.MoveX(mainDisplayX - 22);
            }

        }

        //Metodo que se encarga de dibujar todos los elementos
        public void Draw()
        {

            //Si tenemos un holder podemos dibujar
            if (this.mainHolder.Surface.IsValid)
            {

                //Indicamos que vamos a escribir con el canvas
                this.mainCanvas = this.mainHolder.LockCanvas();

                //Establecemos un fondo de pantalla
                this.mainCanvas.DrawColor(Color.Argb(255, 0, 0, 0));

                //Cambiamos nuestro color con el que queremos dibujar
                this.drawPaint.SetARGB(255, 255, 255, 255);

                //Dibujamos al jugador segun su rectF
                this.mainCanvas.DrawRect(playerBar.GetRect(), this.drawPaint);

                //Dibujamos al jugador segun su rectF
                this.mainCanvas.DrawRect(IABar.GetRect(), this.drawPaint);

                //Dibujamos la pelota segun su rectF
                this.mainCanvas.DrawRect(ball.GetRect(), this.drawPaint);

                // Change the drawing color to white
                this.drawPaint.SetARGB(255, 255, 255, 255);

                // Draw the mScore
                this.drawPaint.TextSize = 100;

                //Declaro una fuenta y la asocio al drawpaint
                Typeface typeface = this.Resources.GetFont(Resource.Font.font);
                Typeface bold = Typeface.Create(typeface, TypefaceStyle.Normal);
                this.drawPaint.SetTypeface(bold);

                //Dibujo los contadores
                this.mainCanvas.Rotate(-90);

                if (this.winnerIA)
                {

                    this.mainCanvas.DrawText("IA WINS", -800, 100, this.drawPaint);
                    this.playerPoints = 0;
                }
                else if (this.winnderPlayer)
                {

                    this.mainCanvas.DrawText("P1 WINS", -800, 100, this.drawPaint);
                    this.IAPoints = 0;
                } else
                {

                    this.mainCanvas.DrawText("" + this.playerPoints, -800, 100, this.drawPaint);
                    this.mainCanvas.DrawText("" + this.IAPoints, -450, 100, this.drawPaint);

                }


                //Indicamos que ya no estamos dibujando
                this.mainHolder.UnlockCanvasAndPost(this.mainCanvas);
            }
        }

        // Si la actividad esta pausada detiene el hilo
        public void Pause()
        {
            this.gameRunning = false;
            try
            {
                this.mainThread.Join();
            }
            catch (InterruptedException e)  { }

        }

        //Si el juego comienza o empieza
        public void Resume()
        {
            this.gameRunning = true;
            this.mainThread = new Thread(Run);
            this.mainThread.Start();
        }

        //Este metodo controla los toques en la pantalla
        public override bool OnTouchEvent(MotionEvent motionEvent)
        {

            switch (motionEvent.Action)
            {

                //Si la pantalla es presionada el juego pasa a funcionar
                case MotionEventActions.Down:

                    this.gamePaused = false;

                    //Detecta la posicion donde hemos tocado la pantalla
                    if (motionEvent.GetX() >= this.mainDisplayY / 2)
                    {
                        this.playerBar.SetMoveState(Bar.RIGHTSTATE);
                    }
                    else
                    {
                        this.playerBar.SetMoveState(Bar.LEFTSTATE);
                    }

                    break;

                    //Si se levanta el dedo de la pantalla la barra se detiene
                case MotionEventActions.Up:

                    this.playerBar.SetMoveState(Bar.STOPSTATE);
                    break;
            }

            return true;

        }

    }

}