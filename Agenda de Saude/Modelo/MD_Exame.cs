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
    /// [MEDEXAMES] Classe que define o exame
    /// </summary>
    class MD_Exame : MDN_Model
    {
        #region Atributes and Properties

        int codigo;
        /// <summary>
        /// [CODIGO] Código do exame
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
        /// [NOME] Nome do exame
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

        string comentario = "";
        /// <summary>
        /// [COMENTARIO] Comentario do exame
        /// </summary>
        public string Comentario
        {
            get
            {
                return comentario;
            }
            set
            {
                this.comentario = value;
            }
        }

        string caminho_arquivo = "";
        /// <summary>
        /// [ARQUIVO] Caminho do arquivo em anexo
        /// </summary>
        public string Caminho_Arquivo
        {
            get
            {
                return caminho_arquivo;
            }
            set
            {
                caminho_arquivo = value;
            }
        }

        #endregion Atributes and Properties

        #region Constructors

        /// <summary>
        /// Construtor base da classe
        /// </summary>
        MD_Exame()
            : base()
        {
            base.table = new MDN_Table("MEDEXAMES");
            this.table.Fields_Table.Add(new MDN_Field("CODIGO", true, Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Field("NOME", true, Enumerator.DataType.STRING, null, false, false, 45, 0));
            this.table.Fields_Table.Add(new MDN_Field("COMENTARIO", true, Enumerator.DataType.STRING, null, false, false, 200, 0));
            this.table.Fields_Table.Add(new MDN_Field("ARQUIVO", true, Enumerator.DataType.STRING, null, false, false, 200, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);
        }

        /// <summary>
        /// Main constructor from user
        /// </summary>
        /// <param name="codigo">Código da enfermidade</param>
        public MD_Exame(int codigo)
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
            string sentenca = "SELECT MAX(CODIGO) FROM MEDEXAMES";
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
        /// Método que verifica se o usuário já existe
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
            string sentenca = "INSERT INTO " + base.table.Table_Name + " ( CODIGO, NOME, COMENTARIO, ARQUIVO) VALUES (" + this.codigo + ", '" + this.nome + "', '" + comentario + "', '" + this.caminho_arquivo + "')";

            return Util.Conection.Insert(sentenca);
        }

        /// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            string sentenca = "SELECT NOME, COMENTARIO, ARQUIVO FROM " + base.table.Table_Name + " WHERE CODIGO = " + codigo;
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            if (reader.Read())
            {
                nome = reader.GetString(0);
                comentario = reader.GetString(1);
                caminho_arquivo = reader.GetString(2);
                
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
                              " NOME =  '" + this.nome + "'," +
                              " COMENTARIO = '" + this.comentario + "'," +
                              " ARQUIVO = '" + this.caminho_arquivo + "' " +
                              " WHERE CODIGO = '" + this.codigo + "'";

            return Util.Conection.Update(sentenca);
        }

        /// <summary>
        /// Método para recuperar as vacinas cadastradas no banco.
        /// </summary>
        /// <returns>Uma Lista de Vacinas.</returns>
        public static List<MD_Exame> ListaExame()
        {
            List<MD_Exame> lista = new List<MD_Exame>();

            string sentenca = "SELECT CODIGO, COALESCE(NOME, '-'), COALESCE(COMENTARIO, '-'), COALESCE(ARQUIVO, '-') FROM MEDEXAMES";

            SqliteDataReader reader = Util.Conection.Select(sentenca);

            if (reader != null)
            {
                while (reader.Read())
                {
                    MD_Exame exame = new MD_Exame(reader.GetInt16(0));
                    exame.Nome = reader.GetString(1);
                    exame.Comentario = reader.GetString(2);
                    exame.caminho_arquivo = reader.GetString(3);
                    lista.Add(exame);
                }
            }

            return lista;
        }

        #endregion Methods
    }
}