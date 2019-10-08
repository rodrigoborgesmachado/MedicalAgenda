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
    /// [MEDMEDICAMENTO] Classe que define o medicamento
    /// </summary>
    class MD_Medicamento : MDN_Model
    {
        #region Atributes and Properties

        int codigo;
        /// <summary>
        /// [CODIGO] Código do medicamento
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
        /// [NOME] Nome do medicamento
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

        string medida_tempo = "";
        /// <summary>
        /// [MEDIDATEMPO] Medida de tempo a ser utilizada
        /// </summary>
        public string Medida_Tempo
        {
            get
            {
                return medida_tempo;
            }
            set
            {
                this.medida_tempo = value;
            }
        }

        string formato = "";
        /// <summary>
        /// [FORMATO] formato do medicamento
        /// </summary>
        public string Formato
        {
            get
            {
                return formato;
            }
            set
            {
                formato = value;
            }
        }

        float quantidade = 0;
        /// <summary>
        /// [QUANTIDADE] Quantidade do medicamento necessária
        /// </summary>
        public float Quantidade
        {
            get
            {
                return quantidade;
            }
            set
            {
                quantidade = value;
            }
        }

        string medida_peso;
        /// <summary>
        /// [MEDIDAPESO] Medida de peso do medicamento
        /// </summary>
        public string Medida_Peso
        {
            get
            {
                return medida_peso;
            }
            set
            {
                medida_peso = value;
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

        #endregion Atributes and Properties

        #region Constructors

        /// <summary>
        /// Construtor base da classe
        /// </summary>
        MD_Medicamento()
            : base()
        {
            base.table = new MDN_Table("MEDMEDICAMENTO");
            this.table.Fields_Table.Add(new MDN_Field("CODIGO", true, Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Field("NOME", true, Enumerator.DataType.STRING, null, false, false, 70, 0));
            this.table.Fields_Table.Add(new MDN_Field("MEDIDATEMPO", true, Enumerator.DataType.STRING, null, false, false, 45, 0));
            this.table.Fields_Table.Add(new MDN_Field("FORMATO", true, Enumerator.DataType.STRING, null, false, false, 45, 0));
            this.table.Fields_Table.Add(new MDN_Field("QUANTIDADE", true, Enumerator.DataType.INT, null, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Field("MEDIDAPESO", true, Enumerator.DataType.STRING, null, false, false, 45, 0));
            this.table.Fields_Table.Add(new MDN_Field("PERIODICIDADE", true, Enumerator.DataType.INT, null, false, false, 0, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);
        }

        /// <summary>
        /// Main constructor from user
        /// </summary>
        /// <param name="codigo">Código da enfermidade</param>
        public MD_Medicamento(int codigo)
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
            string sentenca = "SELECT MAX(CODIGO) FROM MEDMEDICAMENTO";
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
            string sentenca = "INSERT INTO " + base.table.Table_Name + " ( CODIGO, NOME, MEDIDATEMPO, FORMATO, QUANTIDADE, MEDIDAPESO, PERIODICIDADE) VALUES (" + this.codigo + ", '" + this.nome + "', '" + this.medida_tempo + "', '" + this.formato + "', " + this.quantidade + ", '" + medida_peso + "', " + this.periodicidade + ")";

            return Util.Conection.Insert(sentenca);
        }

        /// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            string sentenca = "SELECT NOME, MEDIDATEMPO, FORMATO, QUANTIDADE, MEDIDAPESO, PERIODICIDADE FROM " + base.table.Table_Name + " WHERE CODIGO = " + codigo;
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            if (reader.Read())
            {
                nome = reader.GetString(0);
                medida_tempo= reader.GetString(1);
                formato = reader.GetString(2);
                quantidade = reader.GetInt16(3);
                medida_peso = reader.GetString(4);
                periodicidade = reader.GetInt16(5);

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
                              ", MEDIDATEMPO   = '" + this.Medida_Tempo + "'" +
                              ", FORMATO       = '" + this.formato + "'" +
                              ", QUANTIDADE    = " + this.quantidade +
                              ", MEDIDAPESO    = '" + this.medida_peso + "'" +
                              ", PERIODICIDADE = " + this.periodicidade + 
                              " WHERE CODIGO = '" + this.codigo + "'";

            return Util.Conection.Update(sentenca);
        }

        /// <summary>
        /// Método para recuperar as vacinas cadastradas no banco.
        /// </summary>
        /// <returns>Uma Lista de Vacinas.</returns>
        public static List<MD_Medicamento> ListaMedicamento()
        {
            List<MD_Medicamento> lista = new List<MD_Medicamento>();

            string sentenca = "SELECT CODIGO, COALESCE(NOME, '-'), COALESCE(MEDIDATEMPO, '-'), COALESCE(FORMATO, '-'), COALESCE(QUANTIDADE, 0), COALESCE(MEDIDAPESO , '-'), COALESCE(PERIODICIDADE, 0) FROM MEDMEDICAMENTO";

            SqliteDataReader reader = Util.Conection.Select(sentenca);

            if (reader != null)
            {
                while (reader.Read())
                {
                    MD_Medicamento medicamento = new MD_Medicamento(reader.GetInt16(0));
                    medicamento.Nome = reader.GetString(1);
                    medicamento.Medida_Tempo= reader.GetString(2);
                    medicamento.Formato = reader.GetString(3);
                    medicamento.Quantidade = reader.GetInt16(4);
                    medicamento.Medida_Peso = reader.GetString(5);
                    medicamento.Periodicidade = reader.GetInt16(6);

                    lista.Add(medicamento);
                }
            }

            return lista;
        }

        #endregion Methods
    }
}