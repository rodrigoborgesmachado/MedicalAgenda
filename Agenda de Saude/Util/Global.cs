using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agenda_de_Saude.Modelo;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mono.Data.Sqlite;
using static Agenda_de_Saude.Modelo.Enumerator;

namespace Agenda_de_Saude.Util
{
    public static class Global
    {

        #region Atributos e Propriedades

        /// <summary>
        /// CPF do usuário logado
        /// </summary>
        static string cpf = "";

        static MD_Usuario usuario;
        /// <summary>
        /// Usuário logado no sistema
        /// </summary>
        public static MD_Usuario Usuario_Logado
        {
            get
            {
                usuario = new MD_Usuario(cpf);
                return usuario;
            }
        }

        /// <summary>
        /// Método referente aos alertas do sistema
        /// </summary>
        private static Alerta alerta = null;
        public static Alerta Alerta_Class
        {
            get
            {
                if (alerta == null)
                {
                    alerta = new Alerta();
                }
                return alerta;
            }
        }

        #endregion Atributos e Propriedades

        #region Métodos

        /// <summary>
        /// Method that inicialize the system
        /// </summary>
        /// <returns></returns>
        public static bool InicializeSystem()
        {
            try
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Inicializando ");
                Util.CL_Files.CreateMainDirectories();
                Util.Conection.OpenConnection();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Method that finalize the system
        /// </summary>
        /// <returns></returns>
        public static bool FinalizeSystem()
        {
            try
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Finalizando");
                Util.Conection.CloseConnection();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Método que efetua o login
        /// </summary>
        /// <param name="login">Login do usuário (cpf, email, login)</param>
        /// <param name="password">senha do usuário</param>
        /// <returns>True - Logado; False: não logou</returns>
        public static bool Logar(string login, string password)
        {
            string senteca = "SELECT DSCPF FROM MEDUSU WHERE DSSENH = '" + password + "' AND ( DSCPF = '" + login + "' OR DSMAIL = '" + login + "' OR DSLOG = '" + login + "')";

            SqliteDataReader reader = Conection.Select(senteca);
            if (reader.Read())
            {
                cpf = reader.GetString(0);
                usuario = new MD_Usuario(cpf);

                return true;
            }

            usuario = null;
            return false;
        }

        #endregion Métodos
    }
}