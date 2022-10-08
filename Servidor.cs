using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;
using Servidor.Models;
using Servidor.BLL;

namespace ServidorSocketPrimary
{
    class Servidor
    {
        /*        
            TcpListener--------> Espera la conexion del Cliente.        
            TcpClient----------> Proporciona la Conexion entre el Servidor y el Cliente.        
            NetworkStream------> Se encarga de enviar mensajes a traves de los sockets.        
        */

        private TcpListener Server;
        private IPEndPoint Puerto = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
        private TcpClient Cliente = new TcpClient();
        private List<Connection> ListaClientes = new List<Connection>();

        Connection Conexion;

        private struct Connection
        {
            public NetworkStream stream;
            public StreamWriter streamEscritor;
            public StreamReader streamLector;
        }

        public Servidor()
        {
            Inicio();
        }

        public void Inicio()
        {

            Console.WriteLine("Servidor Activo");
            Server = new TcpListener(Puerto);
            Server.Start();

            while (true)
            {
                Cliente = Server.AcceptTcpClient();

                EstablecerConexion();

                ListaClientes.Add(Conexion);
                Console.WriteLine("Un cliente se ha conectado.");

                Thread hilo = new Thread(Escuchar_conexion);

                hilo.Start();
            }

        }

        void EstablecerConexion()
        {
            Conexion = new Connection();
            Conexion.stream = Cliente.GetStream();
            Conexion.streamLector = new StreamReader(Conexion.stream);
            Conexion.streamEscritor = new StreamWriter(Conexion.stream);
        }

        void Escuchar_conexion()
        {
            Connection hcon = Conexion;
            string[] agregar ;
            do
            {
                try
                {
                    string tmp = hcon.streamLector.ReadLine();

                    if (tmp.Contains("@reservacionH@"))
                    {
                        agregar = tmp.Split(',');
                        AgregarCita(hcon, agregar);

                    }
                    else if (tmp.Contains("@reservacionG@"))
                        LeerCitas(hcon);
                }
                catch
                {
                    ListaClientes.Remove(hcon);
                    Console.WriteLine("Un cliente se ha desconectado.");
                    break;
                }
            } while (true);
        }

        private void AgregarCita(Connection hcon, string[] agregar)
        {
            Reservaciones reservacion = new Reservaciones()
            {
                id = 0,
                Nombre = agregar[1],
                Fecha = DateTime.Parse(agregar[3] + " " + agregar[5]),
                Cedula = agregar[2],
                Mesa = agregar[4],
            };

            try
            {
                bool guardo = ReservacionesBLL.Guardar(reservacion);
                if (guardo)
                {
                    Console.WriteLine("La reservacion de " + reservacion.Nombre + " se ha realizado exitosamente");
                    hcon.streamEscritor.WriteLine("Estimado/a " + reservacion.Nombre + " A la fecha de: " + reservacion.Fecha.ToString() + " su reservacion se ha realizado de manera exitosa.");
                    hcon.streamEscritor.Flush();

                }
                else
                {
                    Console.WriteLine("No se pudo guardar su reservacion...");
                    hcon.streamEscritor.WriteLine("Estimado/a " + reservacion.Nombre + " A la fecha de: " + reservacion.Fecha.ToString() + " su reservacion no se pudo completar...");
                    hcon.streamEscritor.Flush();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("No se pudo guardar");
            }

        }

        private void LeerCitas(Connection hcon)
        {
            List<Reservaciones> lista = ReservacionesBLL.GetReservacion();
            foreach (Reservaciones x in lista)
            {
                hcon.streamEscritor.WriteLine("Id: " + x.id);
                hcon.streamEscritor.Flush();

                hcon.streamEscritor.WriteLine("Nombre: " + x.Nombre);
                hcon.streamEscritor.Flush();

                hcon.streamEscritor.WriteLine("Fecha: " + x.Fecha);
                hcon.streamEscritor.Flush();

                hcon.streamEscritor.WriteLine("Cedula: " + x.Cedula);
                hcon.streamEscritor.Flush();

                hcon.streamEscritor.WriteLine("Mesa: " + x.Mesa);
                hcon.streamEscritor.Flush();
            }
        }
    }
}
