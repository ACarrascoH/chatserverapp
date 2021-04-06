
using ChatSocketApp.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSocketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket serverSocket = new ServerSocket(puerto);
            if (serverSocket.Iniciar())
            {
                //puedo esperar un cliente
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Esperando cliente...");
                    if (serverSocket.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Cliente recibido!");
                        Console.WriteLine("S:Hola cliente!");
                        serverSocket.Escribir("Hola Cliente");
                        Console.WriteLine("Escriba un mensaje para el cliente:");
                        string mensaje = Console.ReadLine().Trim();
                        Console.WriteLine("S:{0}", mensaje);
                        serverSocket.Escribir(mensaje);

                        //mensaje recibido del cliente
                        string respuesta = serverSocket.Leer();
                        Console.WriteLine("C:{0}", respuesta);
                        serverSocket.CerrarConexion();
                    }
                }
            }
            else
            {
                //por permisos no puedo levantar la comunicacion en el puerto
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se puede levantar el servidor, puerto ocupado o sin privilegios");
                Console.ReadKey();
            }
        }
    }
}
