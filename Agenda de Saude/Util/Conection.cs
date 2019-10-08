using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Agenda_de_Saude.Util
{
    public class Conection
    {
        /// <summary>
        /// Checker is the connection is opened
        /// </summary>
        private static bool is_open = false;

        /// <summary>
        /// Checker if the transaction is opened
        /// </summary>
        private static bool is_in_transaction = false;

        /// <summary>
        /// Class of the connection with the data_base
        /// </summary>
        private static SqliteConnection m_dbConnection;

        /// <summary>
        /// Name of the data_base of tests
        /// </summary>
        private static string name_table_test = "TESTE";

        /// <summary>
        /// Method that open the transaction with the data_base
        /// </summary>
        /// <returns>True - Connection established; False - CAn't make the connection</returns>
        public static bool OpenConnection(string directory_database = "")
        {
            if (is_open && !string.IsNullOrEmpty(directory_database))
                return true;

            if (string.IsNullOrEmpty(directory_database))
                directory_database = Agenda_de_Saude.Util.CL_Files.camArqBD;

            // If the database don't exists it is create
            if (!Agenda_de_Saude.Util.CL_Files.Exists(directory_database))
            {
                SqliteConnection.CreateFile(directory_database);
            }

            try
            {
                m_dbConnection = new SqliteConnection("Data Source=" + directory_database + ";Version=3");
                m_dbConnection.Open();
                is_open = true;
            }
            catch(System.Data.DataException e)
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Conection.OpenConnection. Error: " + e.Message);
                is_open = false;
            }
            catch (System.Data.DBConcurrencyException e)
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Conection.OpenConnection. Error: " + e.Message);
                is_open = false;
            }
            catch (Exception e)
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Conection.OpenConnection. Error: " + e.Message);
                is_open = false;
            }
            return is_open;
        }

        /// <summary>
        /// Method that close the connection with the database if it is open
        /// </summary>
        /// <returns>True - Connection closed; False: Problem to close the connection</returns>
        public static bool CloseConnection(string caminho_banco = "")
        {
            if (!is_open && string.IsNullOrEmpty(caminho_banco))
            {
                return true;
            }

            try
            {
                m_dbConnection.Close();
                m_dbConnection = null;
                is_open = false;

                return true;
            }
            catch(Exception e)
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Conection.CloseConnection. Error: " + e.Message);
                is_open = true;
            }
            return false;
        }

        /// <summary>
        /// Method that makes a select on the database
        /// </summary>
        /// <param name="command_sql">Command to be executed</param>
        /// <returns>Returns a DataReader that provides the returns of consult</returns>
        public static SqliteDataReader Select(string command_sql)
        {
            try
            {
                if(!is_open)
                {
                    OpenConnection();
                }

                SqliteCommand command = new Mono.Data.Sqlite.SqliteCommand(command_sql, m_dbConnection);
                SqliteDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch(Exception e)
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Problem on the sentence. Sentence: " + command_sql + " Error: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Method that execute a create table
        /// </summary>
        /// <param command_sql="">Command command_sql</param>
        /// <returns>True - Sucess; False: Error</returns>
        public static bool CreateTable(string command_sql)
        {
            try
            {
                return Execute(command_sql);
            }
            catch(Exception e)
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Error create table: " + command_sql + ". Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Method that provide the insert on the data_base
        /// </summary>
        /// <param name="command_sql">Command sql with the sentence</param>
        /// <returns>True - Sucess; False: Error</returns>
        public static bool Insert(string command_sql)
        {
            try
            {
                return Execute(command_sql);
            }
            catch(Exception e)
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Error insert: " + command_sql + ". Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Method that makes a update on the data base
        /// </summary>
        /// <param name="command">Command sql</param>
        /// <returns>True - Sucess; False: Error</returns>
        public static bool Update(string command)
        {
            try
            {
                return Execute(command);
            }
            catch (Exception e)
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Error update: " + command + ". Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Method that make the delete on data base
        /// </summary>
        /// <param name="command">Command sql</param>
        /// <returns>True - Sucess; False: Error</returns>
        public static bool Delete(string command)
        {
            try
            {
                return Execute(command);
            }
            catch (Exception e)
            {
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Erro delete: " + command + ". Erro: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Method that execute the sentence on the data base
        /// </summary>
        /// <param name="command_sql">Command</param>
        /// <returns>True - Sucesso; False: erro</returns>
        public static bool Execute(string command_sql)
        {
            if (!is_open)
                OpenConnection();

            SqliteCommand command = new SqliteCommand(command_sql, m_dbConnection);
            try
            {
                command.Transaction = m_dbConnection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                command.Transaction.InitializeLifetimeService();
                command.ExecuteNonQuery();
                command.Transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                command.Transaction.Rollback();
                Agenda_de_Saude.Util.CL_Files.WriteOnTheLog("Erro no execute: " + command_sql + ". Erro: " + e.Message);
                return false;
            }
        }

        /// <summary>
        ///  Method that converts date to int
        /// </summary>
        /// <param name="date">Date to be converted</param>
        /// <returns>Value integer that represents the date</returns>
        public static int Date_to_Int(DateTime date)
        {
            string sentenca =  "SELECT strftime('%s','" + date.ToString("DD/MM/YYYY hh:mm:ss") + "') FROM " + Tests_table();
            SqliteDataReader reader = Select(sentenca);

            int num_date = 0;
            if (reader.Read())
            {
                num_date = reader.GetInt16(0);
            }

            return num_date;
        }

        /// <summary>
        ///  Method that converts the value integer to date
        /// </summary>
        /// <param name="num_date">Value integer to be converted</param>
        /// <returns>Date</returns>
        public static DateTime Int_to_Date(int num_date)
        {
            
            string sentenca = "SELECT datetime(" + num_date + ",'unixepoch') AS TESTE FROM " + Tests_table();
            SqliteDataReader reader = Select(sentenca);

            DateTime date = DateTime.Now;
            if (reader.Read())
            {
                date = reader.GetDateTime(0);
            }

            return date;
        }

        /// <summary>
        /// Method that returns the name of table of tests
        /// </summary>
        /// <returns>name of table</returns>
        public static string Tests_table()
        {
            string sql = "select 1 as TEST from " + name_table_test;
            SqliteDataReader reader = Select(sql);

            if(reader == null)
            {
                sql = "CREATE TABLE " + name_table_test + "(IDTESTE INT)";
                CreateTable(sql);
                reader = Select(sql);
            }

            return name_table_test;
        }

        /// <summary>
        /// Method that verify if the data base exists
        /// </summary>
        /// <param name="table">Name of the table</param>
        /// <returns>True - exists; False - don't exists</returns>
        public static bool ExistsTable(string table)
        {
            return Select("SELECT 1 FROM " + table) != null;
        }
    }
}
