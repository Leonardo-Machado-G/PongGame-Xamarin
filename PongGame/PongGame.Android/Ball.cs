//Importo las librerias
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Pong_Game.Modelos.Clases.CapaNegocio;

//Declaro el namespace
namespace Pong_Game.Droid
{
    //Declaro la clase
    public class Ball
    {
        //Declaro la zona de colision
        private RectF ballRect;

        //Declaro las velocidades
        private float speedX;
        private float speedY;

        //Declaro las dimensiones de la pelota
        private float widthBall;
        private float heightBall;

        //Declaro el constructor
        public Ball(int displayX, int displayY)
        {
            //Establezco las dimensiones de la pelota
            this.widthBall = displayX / 100;
            this.heightBall = this.widthBall;

            //Establezco las velocidades
            this.speedY = displayY / GameController.currentBallSpeed;
            this.speedX = this.speedY;

            //Definimo la zona de colision la pelota
            this.ballRect = new RectF();

        }

        //Obtengo el RectF de la pelota
        public RectF GetRect()
        {
            return this.ballRect;
        }

        //Cambio la posicion de la pelota cada frame
        public void Update(long fps)
        {
            this.ballRect.Left = this.ballRect.Left + (this.speedX / fps);
            this.ballRect.Top = this.ballRect.Top + (this.speedY / fps);
            this.ballRect.Right = this.ballRect.Left + this.widthBall;
            this.ballRect.Bottom = this.ballRect.Top - this.heightBall;
        }

        //Cambio la velocidad de la pelota
        public void ReverseVelocityY()
        {
            this.speedY = -this.speedY;
        }

        //Cambio la velocidad de la pelota
        public void ReverseVelocityX()
        {
            this.speedX = -this.speedX;
        }

        //Establezco una velocidad para la pelota
        public void RandomXVelocity()
        {
            Random generator = new Random();
            int answer = generator.Next(2);

            if (answer == 0)
            {
                ReverseVelocityX();
            }
        }

        //Metodos para reposicionar la pelota
        public void MoveY(float y)
        {
            this.ballRect.Bottom = y;
            this.ballRect.Top = y - this.heightBall;
        }
        public void MoveX(float x)
        {
            this.ballRect.Left = x;
            this.ballRect.Right = x + this.widthBall;
        }

        //Reseteamos la posicion de la pelota
        public void Reset(int x, int y)
        {
            this.ballRect.Left = x / 2;
            this.ballRect.Top = y/2;
            this.ballRect.Right = x / 2 ;
            this.ballRect.Bottom = y /2;
        }

    }

}