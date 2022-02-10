//Importo las librerias utilizadas
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Pong_Game.Modelos.Clases.CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Declaro el namespace
namespace Pong_Game.Droid
{
    //Declaro la clase
    public class Bar
    {
        //Posee cuatro coordenadas para detectar colisiones
        private RectF barRect;

        //Posiciones de la barra
        private float positionX;
        private float positionY;

        //Definimos la altura y anchura de la barra
        private float barWidth;
        private float barHeight;

        //Velocidad de desplazamiento de la barra
        private float barSpeed;

        //Estados en los que estara la barra
        public static readonly int STOPSTATE = 0;
        public static readonly int LEFTSTATE = 1;
        public static readonly int RIGHTSTATE = 2;

        //Indica movimiento y hacia donde se dirige
        private int barState = STOPSTATE;

        //Dimensiones de la pantalla
        private int withDisplay;
        private int heightDisplay;

        //Variable para controlar a la maquina
        private bool isIA;
        private bool moveAuto;
        int counter = 0;

        //Constructor de la clase al que le transferimos las dimensiones de la pantalla
        public Bar(int x, int y, int position_X, int position_Y, bool IA)
        {

            //Insertamos si es una IA o no
            this.isIA = IA;

            //Insertamos los valores de la pantalla
            this.withDisplay = x;
            this.heightDisplay = y;

            //La anchura de la barra sera en funcion de la pantalla
            this.barWidth = this.withDisplay / 8;

            //La altura de la barra sera en funcion de la pantalla
            this.barHeight = this.heightDisplay / 25;

            //Establecemos la posicion de la barra en el centro de la pantalla
            this.positionX = this.withDisplay / position_X;
            this.positionY = this.heightDisplay/ position_Y + this.heightDisplay/ (position_Y*2);

            //Establecemos su zona de colision
            this.barRect = new RectF(this.positionX, this.positionY, this.positionX + this.barWidth, this.positionY + this.barHeight);

            //Establecemos la velocidad de movimiento de la barra
            this.barSpeed = this.withDisplay;

        }

        //Devolvemos la zona de colision de la barra
        public RectF GetRect()
        {
            return this.barRect;
        }

        //Establecemos el estado de la barra
        public void SetMoveState(int state)
        {
            this.barState = state;
        }

        //Metodo que en funcion de su estado haremos una cosa u otra
        public void UpdateBar(long fps)
        {

            //Desplazamos la barra
            if (this.barState == LEFTSTATE && !this.isIA)
            {
                if(this.barRect.Left <= 0)
                {


                } else {
                    
                    this.positionY = this.positionY - this.barSpeed / fps;

                }

            }

            if (this.barState == RIGHTSTATE && !this.isIA)
            {

                if (this.barRect.Right >= this.withDisplay)
                {

                } else
                {
                    this.positionY = this.positionY + this.barSpeed / fps;

                }

            }

            //La IA se desplazara sola
            if (this.isIA)
            {
               
                if(counter == 100)
                {

                    this.moveAuto = false;

                }
                
                if(counter == 0)
                {
                    this.moveAuto = true;

                }

                if (this.moveAuto)
                {
                    counter++;
                    this.positionY = this.positionY + this.barSpeed / (fps * GameController.currentIASpeed);


                } else  {
                    this.positionY = this.positionY - this.barSpeed / (fps * GameController.currentIASpeed);
                    counter--;

                }

            }

            //Establecemos la zona de colision
            this.barRect.Left = positionY;
            this.barRect.Right = positionY + barWidth;

        }

    }

}