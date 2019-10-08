using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace Agenda_de_Saude.Modelo
{
    /// <summary>
    /// [MEDVACINA] Classe que define a vacina
    /// </summary>
    public class MD_Vacina : MDN_Model
    {
        #region Atributes and Properties

        string dsnome;
        /// <summary>
        /// [DSNOME] Nome da vacina.
        /// </summary>
        public string Nome
        {
            get
            {
                return dsnome;
            }
            set
            {
                dsnome = value;
            }
        }

        string dtapl;
        /// <summary>
        /// [DATA_APLICACAO] Data da aplicação.
        /// </summary>
        public string Data_aplicacao
        {
            get
            {
                return dtapl;
            }
            set
            {
                dtapl = value;
            }
        }

        int dose = 1;
        /// <summary>
        /// [DOSE] Quantidade de dose à ser administrada.
        /// </summary>
        public int Dose
        {
            get
            {
                return dose;
            }
            set
            {
                dose = value;
            }
        }

        #endregion Atributes and Properties

        #region Constructors

        /// <summary>
        /// Construtor base da classe
        /// </summary>
        MD_Vacina()
            : base()
        {
            base.table = new MDN_Table("MEDVACINA");
            this.table.Fields_Table.Add(new MDN_Field("DSNOME", true, Enumerator.DataType.STRING, null, true, true, 15, 0));
            this.table.Fields_Table.Add(new MDN_Field("DATA_APLICACAO", true, Enumerator.DataType.STRING, null, false, false, 25, 0));
            this.table.Fields_Table.Add(new MDN_Field("DOSE", true, Enumerator.DataType.INT, null, false, false, 25, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);
        }

        /// <summary>
        /// Construtor para a classe vacina.
        /// </summary>
        /// <param name="nome_vacina">Nome da Vacina.</param>
        public MD_Vacina(string nome_vacina)
            : this()
        {
            this.dsnome = nome_vacina;
            this.Load();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Método que deleta o usuário do banco
        /// </summary>
        /// <returns>True - excluído com sucesso; False - Erro</returns>
        public override bool Delete()
        {
            string sentenca = "DELETE FROM " + base.table.Table_Name + " WHERE DSNOME = '" + dsnome + "'";
            return Util.Conection.Delete(sentenca);
        }

        /// <summary>
        /// Método que verifica se a vacina já existe
        /// </summary>
        /// <returns></returns>
        public bool VerificaExiste(string nomeVacina)
        {
            string sentenca = "SELECT 1 FROM " + base.table.Table_Name + " WHERE DSNOME = '" + nomeVacina + "'";
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            return reader.Read();
        }

        /// <summary>
        /// Método que faz o insert no banco do usuário
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            string sentenca = "INSERT INTO " + base.table.Table_Name + " ( DSNOME, DATA_APLICACAO, DOSE ) VALUES ('" + dsnome + "', '" + dtapl + "', '" + dose + "')";

            return Util.Conection.Insert(sentenca);
        }

        /// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            string sentenca = "SELECT DSNOME, DATA_APLICACAO, DOSE FROM " + base.table.Table_Name + " WHERE DSNOME = '" + dsnome + "'";
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            if (reader.Read())
            {
                dsnome = reader.GetString(0);
                dtapl = reader.GetString(1);
                dose = reader.GetInt32(2);
                reader.Close();

                Empty = false;
            }
            else
                Empty = true;
        }

        /// <summary>
        /// Método que faz update da classe
        /// </summary>
        /// <returns>True - update executado com sucesso; False - erro</returns>
        public override bool Update()
        {
            string sentenca = " UPDATE " + base.table.Table_Name + " SET " +
                              " DSNOME =  '" + dsnome + "'," +
                              " DATA_APLICACAO = '" + dtapl + "'," +
                              " DOSE = '" + dose + "'" +
                              " WHERE DSNOME = '" + dsnome + "'";

            return Util.Conection.Update(sentenca);
        }

        /// <summary>
        /// Método para recuperar as vacinas cadastradas no banco.
        /// </summary>
        /// <returns>Uma Lista de Vacinas.</returns>
        public static List<MD_Vacina> ListaVacinas()
        {
            List<MD_Vacina> lista = new List<MD_Vacina>();
            
            string sentenca = "SELECT * FROM MEDVACINA";
            
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            if (reader != null)
            {
                while (reader.Read())
                {
                    MD_Vacina vacina = new MD_Vacina(reader.GetString(0));
                    vacina.dsnome = reader.GetString(0);
                    vacina.dtapl = reader.GetString(1);
                    vacina.dose = reader.GetInt32(2);
                    lista.Add(vacina);
                }
            }
            
            return lista;
        }

        #endregion Methods
    }
}