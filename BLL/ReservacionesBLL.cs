using Servidor.DAL;
using System;
using Servidor.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Servidor.BLL
{
    public class ReservacionesBLL
    {
        public static Reservaciones Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Reservaciones Reservaciones;

            try
            {
                Reservaciones = contexto.Reservaciones.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Reservaciones;
        }
        public static List<Reservaciones> GetReservacion()
        {
            Contexto contexto = new Contexto();
            List<Reservaciones> lista = new List<Reservaciones>();
            try
            {
                lista = contexto.Reservaciones.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
        public static bool ReservacionValida(DateTime Fecha)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                paso = contexto.Reservaciones.Any(e => e.Fecha.Hour == Fecha.Hour && e.Fecha.Day == Fecha.Day);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                paso = contexto.Reservaciones.Any(e => e.id == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Insertar(Reservaciones reservaciones)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (ReservacionValida(reservaciones.Fecha))
                {
                    Console.WriteLine("Usted acaba de poner un huevo con su fecha, intente de nuevo mejor sera...");
                }
                else
                {
                    contexto.Reservaciones.Add(reservaciones);
                    paso = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Reservaciones reservaciones)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(reservaciones).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;

        }
        public static bool Guardar(Reservaciones reservaciones)
        {
            if (!Existe(reservaciones.id))
                return Insertar(reservaciones);
            else
                return Modificar(reservaciones);
        }
       

    }   
}
