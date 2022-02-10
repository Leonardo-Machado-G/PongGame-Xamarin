//Importamos las librerias necesarias
using Pong_Game.Modelos.Clases.CapaNegocio;
using Pong_Game.Models.Class.CapaPresentacion;
using System;
using System.IO;

//Declaramos el namespace
namespace Pong_Game.Modelos.Clases.CapaDatos
{

    //Clase para guardar y cargar datos
    public class SaveSettings
    {

        //Metodo para guardar las opciones
        public static void SaveConfiguration(){

            // Establecemos la ruta donde vamos a guardar la configuracion de nuestro juego
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "configuration.txt");

            //Si no existe el archivo capturamos la exception para evitar fallos, sino, lo borramos
            try
            {

                File.Delete(fileName);

            }
            catch (Exception) { }

            //Guardamos en un string toda la informacion al respecto de nuestra configuracion
            String content = GameController.currentHandicapPlayer + ":" + 
                             GameController.currentIASpeed + ":" +
                             GameController.currentGamePoints + ":" + 
                             GameController.currentBallSpeed;

            //Creamos un archivo con una ruta y escribimos el contenido en el
            using (StreamWriter file = new StreamWriter(fileName, true))
            {

                //Escribimos el contenido y cerramos el archivo
                file.WriteLine(content);
                file.Close();

            }

        }

        //Metodo para cargar las opciones
        public static void LoadSettings() {

            //Establecemos la ruta
            string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "configuration.txt");
            string text = "";

            //Declaramos el vector donde vamos a introducir la informacion
            String[] data;

            //Si el archivo existe lo leemos
            if (File.Exists(file))
            {

                text = File.ReadAllText(file);

                //Introducimos en un array todos los datos separados por comas
                data = text.Split(':');

                //Cargamos las variables
                GameController.currentHandicapPlayer = data[0];
                GameController.currentIASpeed = int.Parse(data[1]);
                GameController.currentGamePoints = int.Parse(data[2]);
                GameController.currentBallSpeed = int.Parse(data[3]);

            }

        }

    }

}
