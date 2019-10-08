using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mono.Data.Sqlite;

namespace Agenda_de_Saude.Modelo
{
    /// <summary>
    /// [MEDTRATAMENTO] Classe que define o medicamento
    /// </summary>
    class MD_Tratamento : MDN_Model
    {
        #region Atributes and Properties

        int codigo;
        /// <summary>
        /// [CODIGO] Código do tratamento
        /// </summary>
        public int Codigo
        {
            get
            {
                return codigo;
            }
        }

        string nome;
        /// <summary>
        /// [NOME] Nome do tratamento
        /// </summary>
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
            }
        }

        string finalizado = "";
        /// <summary>
        /// [FINALIZADO] Verifica se o tratamento já foi finalizado
        /// </summary>
        public string Finalizado
        {
            get
            {
                return finalizado;
            }
            set
            {
                this.finalizado = value;
            }
        }

        DateTime data_inicio = DateTime.Now;
        /// <summary>
        /// [DATAINICIO] formato do medicamento
        /// </summary>
        public DateTime Data_Inicio
        {
            get
            {
                return data_inicio;
            }
            set
            {
                data_inicio = value;
            }
        }

        DateTime datafim = DateTime.Now;
        /// <summary>
        /// [DATAFIM] Data fim 
        /// </summary>
        public DateTime Data_Fim
        {
            get
            {
                return datafim;
            }
            set
            {
                datafim = value;
            }
        }

        int periodicidade;
        /// <summary>
        /// [PERIODICIDADE] Periodicidade 
        /// </summary>
        public int Periodicidade
        {
            get
            {
                return periodicidade;
            }
            set
            {
                periodicidade = value;
            }
        }

        string comentario = "";
        /// <summary>
        /// [COMENTARIO] Comentário sobre o tratamento
        /// </summary>
        public string Comentario
        {
            get
            {
                return comentario;
            }
            set
            {
                comentario = value;
            }
        }

        #endregion Atributes and Properties

        #region Constructors

        /// <summary>
        /// Construtor base da classe
        /// </summary>
        MD_Tratamento()
            : base()
        {
            base.table = new MDN_Table("MEDTRATAMENTO");
            this.table.Fields_Table.Add(new MDN_Field("CODIGO", true, Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Field("NOME", true, Enumerator.DataType.STRING, null, false, false, 45, 0));
            this.table.Fields_Table.Add(new MDN_Field("FINALIZADO", true, Enumerator.DataType.STRING, null, false, false, 1, 0));
            this.table.Fields_Table.Add(new MDN_Field("DATAINICIO", true, Enumerator.DataType.INT, null, false, false, 45, 0));
            this.table.Fields_Table.Add(new MDN_Field("DATAFIM", true, Enumerator.DataType.INT, null, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Field("PERIODICIDADE", true, Enumerator.DataType.INT, null, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Field("COMENTARIO", true, Enumerator.DataType.STRING, null, false, false, 250, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);
        }

        /// <summary>
        /// Main constructor from user
        /// </summary>
        /// <param name="codigo">Código da enfermidade</param>
        public MD_Tratamento(int codigo)
            : this()
        {
            this.codigo = codigo;
            this.Load();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Método que trás o novo código
        /// </summary>
        public static int NovoCodigo()
        {
            string sentenca = "SELECT COALESCE(MAX(CODIGO), 0) FROM MEDTRATAMENTO";
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            int codigo = 1;

            if (reader != null)
                if (reader.Read())
                {
                    codigo = reader.GetInt16(0);
                    codigo++;
                }

            return codigo;
        }

        /// <summary>
        /// Método que deleta o usuário do banco
        /// </summary>
        /// <returns>True - excluído com sucesso; False - Erro</returns>
        public override bool Delete()
        {
            string sentenca = "DELETE FROM " + base.table.Table_Name + " WHERE CODIGO = " + this.codigo + "";
            return Util.Conection.Delete(sentenca);
        }

        /// <summary>
        /// Método que verifica se o medicamento já existe
        /// </summary>
        /// <returns></returns>
        public bool VerificaExiste()
        {
            string sentenca = "SELECT 1 FROM " + base.table.Table_Name + " WHERE CODIGO = " + codigo + "";
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            return reader.Read();
        }

        /// <summary>
        /// Método que faz o insert no banco do usuário
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            string sentenca = "INSERT INTO " + base.table.Table_Name + " ( CODIGO, NOME, FINALIZADO, DATAINICIO, DATAFIM, PERIODICIDADE, COMENTARIO) VALUES (" + this.codigo + ", '" + this.nome + "', '" + this.finalizado + "', " + Util.Conection.Date_to_Int(this.data_inicio) + ", " + Util.Conection.Date_to_Int(this.datafim) + ", " + this.periodicidade + ", '" + comentario + "')";

            return Util.Conection.Insert(sentenca);
        }

        /// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            string sentenca = "SELECT NOME, FINALIZADO, DATAINICIO, DATAFIM, PERIODICIDADE, COMENTARIO FROM " + base.table.Table_Name + " WHERE CODIGO = " + codigo;
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            if (reader.Read())
            {
                nome = reader.GetString(0);
                finalizado = reader.GetString(1);
                data_inicio = Util.Conection.Int_to_Date(reader.GetInt16(2));
                datafim = Util.Conection.Int_to_Date(reader.GetInt16(3));
                periodicidade = reader.GetInt16(4);
                comentario = reader.GetString(5);

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
                              "  NOME          = '" + this.nome + "'" +
                              ", FINALIZADO   = '" + this.finalizado + "'" +
                              ", DATAINICIO       = " + Util.Conection.Date_to_Int(this.data_inicio) + 
                              ", DATAFIM    = " + Util.Conection.Date_to_Int(this.datafim) +
                              ", PERIODICIDADE    = " + this.periodicidade + 
                              ", COMENTARIO = '" + this.comentario + "'" +
                              " WHERE CODIGO = '" + this.codigo + "'";

            return Util.Conection.Update(sentenca);
        }

        /// <summary>
        /// Método para recuperar os taratmentos cadastrados no banco.
        /// </summary>
        /// <returns>Uma Lista de Vacinas.</returns>
        public static List<MD_Tratamento> ListaTratamento()
        {
            List<MD_Tratamento> lista = new List<MD_Tratamento>();

            string sentenca = "SELECT CODIGO, COALESCE(NOME, '-'), COALESCE(FINALIZADO, '0'), COALESCE(DATAINICIO, 0), COALESCE(DATAFIM, 0), COALESCE(COMENTARIO , '-'), COALESCE(PERIODICIDADE, 0) FROM MEDTRATAMENTO";

            SqliteDataReader reader = Util.Conection.Select(sentenca);

            if (reader != null)
            {
                while (reader.Read())
                {
                    MD_Tratamento tratamento = new MD_Tratamento(reader.GetInt16(0));
                    tratamento.Nome = reader.GetString(1);
                    tratamento.Finalizado = reader.GetString(2);
                    tratamento.Data_Inicio = Util.Conection.Int_to_Date(reader.GetInt16(3));
                    tratamento.Data_Fim = Util.Conection.Int_to_Date(reader.GetInt16(4));
                    tratamento.Comentario = reader.GetString(5);
                    tratamento.Periodicidade = reader.GetInt16(6);

                    lista.Add(tratamento);
                }
            }

            return lista;
        }

        #endregion Methods
    }
}