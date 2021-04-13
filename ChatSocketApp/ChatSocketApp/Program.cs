
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
                        string mensaje = "";
                        while (mensaje.ToLower() != "chao")
                        {
                            string respuesta = serverSocket.Leer().Trim();
                            Console.WriteLine("C:{0}", respuesta);
                            if (respuesta.ToLower() != "chao")
                            {
                                Console.WriteLine("Diga lo que quiere decir guruguru:");
                                mensaje = Console.ReadLine().Trim();
                                Console.WriteLine("C:{0}", mensaje);
                                serverSocket.Escribir(mensaje);
                            }
                            else
                            {
                                mensaje = "chao";
                            }
                        }
                        
                        
                        
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
