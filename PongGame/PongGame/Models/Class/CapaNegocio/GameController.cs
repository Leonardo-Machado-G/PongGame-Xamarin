//Importamos las librerias que vamos a utilizar
using System;
using System.Collections.Generic;
using System.Text;

//Declaro el namespace
namespace Pong_Game.Modelos.Clases.CapaNegocio
{

    //Declaro la clase
    public class GameController
    {

        //Declaro las velocidades de la pelota
        public static int[] ballSpeed = new int[] { 4, 2, 1 };

        //Declaro los puntos del juego
        public static int[] gamePoints = new int[] { 3, 6, 9 };

        //Declaro el tamaño del mapa
        public static int[] IASpeed = new int[] { 3, 2, 1 };

        //Declaro los modos de handicap
        public static String[] handicapPlayer = new String[] { "None", "P1", "IA" };

        //Variable que indica la velocidad de la pelota
        public static int currentBallSpeed = ballSpeed[1];

        //Variable que indica los puntos del juego
        public static int currentGamePoints = gamePoints[0];

        //Variable que indica el tamaño del mapa actual
        public static int currentIASpeed = IASpeed[1];

        //Variable que indica el modo de handicap actual
        public static String currentHandicapPlayer = handicapPlayer[0];

    }

}
