using System;
using Mono.Data.Sqlite;

namespace Agenda_de_Saude.Modelo
{
    /// <summary>
    /// [MEDUSU] Classe que define o usuário
    /// </summary>
    public class MD_Usuario : MDN_Model
    {
        #region Atributes and Properties

        string dscpf;
        /// <summary>
        /// [DSCPF] CPF from the user. This will be use for primary key
        /// </summary>
        public string CPF_Usuario
        {
            get
            {
                return dscpf;
            }
        }

        string dsnome = "";
        /// <summary>
        /// [DSNOME] Descrição do nome do usuário
        /// </summary>
        public string Nome_usuario
        {
            get
            {
                return dsnome;
            }
            set
            {
                this.dsnome = value;
            }
        }

        string dssenh = "";
        /// <summary>
        /// [DSSENH] Senha do usuário
        /// </summary>
        public string Senha_usuario
        {
            get
            {
                return dssenh;
            }
            set
            {
                dssenh = value;
            }
        }

        string dsmail = "";
        /// <summary>
        /// [DSMAIL] Email do usuário
        /// </summary>
        public string Email_usuario
        {
            get
            {
                return dsmail;
            }
            set
            {
                dsmail = value;
            }
        }

        string dslogin = "";
        /// <summary>
        /// [DSLOG] Log do usuário
        /// </summary>
        public string Login_usuario
        {
            get
            {
                return dslogin;
            }
            set
            {
                dslogin = value;
            }
        }

        DateTime dtbirt = DateTime.Now;
        /// <summary>
        /// [DTBRIT] Data de Aniversário
        /// </summary>
        public DateTime Data_aniversario
        {
            get
            {
                return dtbirt;
            }
            set
            {
                dtbirt = value;
            }
        }

        #endregion Atributes and Properties

        #region Constructors

        /// <summary>
        /// Construtor base da classe
        /// </summary>
        MD_Usuario()
            : base()
        {
            base.table = new MDN_Table("MEDUSU");
            this.table.Fields_Table.Add(new MDN_Field("DSCPF", true, Enumerator.DataType.STRING, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Field("DSNOME", true, Enumerator.DataType.STRING, null, false, false, 25, 0));
            this.table.Fields_Table.Add(new MDN_Field("DSSENH", true, Enumerator.DataType.STRING, null, false, false, 25, 0));
            this.table.Fields_Table.Add(new MDN_Field("DSMAIL", true, Enumerator.DataType.STRING, null, false, false, 75, 0));
            this.table.Fields_Table.Add(new MDN_Field("DSLOG", true, Enumerator.DataType.STRING, null, false, false, 50, 0));
            this.table.Fields_Table.Add(new MDN_Field("DTBRIT", true, Enumerator.DataType.INT, null, false, false, 25, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);
        }

        /// <summary>
        /// Main constructor from user
        /// </summary>
        /// <param name="cpf_usuario">CPF from user</param>
        public MD_Usuario(string cpf_usuario)
            : this()
        {
            this.dscpf = cpf_usuario;
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
            string sentenca = "DELETE FROM " + base.table.Table_Name + " WHERE DSCPF = '" + this.dscpf + "'";
            return Util.Conection.Delete(sentenca);
        }

        /// <summary>
        /// Método que verifica se o usuário já existe
        /// </summary>
        /// <returns></returns>
        public bool VerificaExiste()
        {
            string sentenca = "SELECT 1 FROM " + base.table.Table_Name + " WHERE DSCPF = '" + dscpf + "'";
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            return reader.Read();
        }

        /// <summary>
        /// Método que verifica se existe o login informado.
        /// </summary>
        /// <param name="dsLogin">Login</param>
        /// <returns>Verdadeiro caso o login informado exista no sistema, falso caso contrário.</returns>
        public bool ExisteLogin(string dsLogin)
        {
            string sentenca = "SELECT 1 FROM " + base.table.Table_Name + " WHERE DSLOG = '" + dsLogin + "'";
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            return reader.Read();
        }

        /// <summary>
        /// Método que faz o insert no banco do usuário
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            if (VerificaExiste())
                return this.Update();

            string sentenca = "INSERT INTO " + base.table.Table_Name + " ( DSCPF, DSNOME, DSSENH, DSMAIL, DSLOG, DTBRIT ) VALUES ('" + this.CPF_Usuario + "', '" + dsnome + "', '" + dssenh + "', '" + dsmail + "', '" + dslogin + "', " + Util.Conection.Date_to_Int(Data_aniversario) + ")";

            return Util.Conection.Insert(sentenca);
        }

        /// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            string sentenca = "SELECT DSCPF, DSNOME, DSSENH, DSMAIL, DSLOG, DTBRIT FROM " + base.table.Table_Name + " WHERE DSCPF = '" + dscpf + "'";
            SqliteDataReader reader = Util.Conection.Select(sentenca);

            if (reader.Read())
            {
                dscpf = reader.GetString(0);
                dsnome = reader.GetString(1);
                dssenh = reader.GetString(2);
                dsmail = reader.GetString(3);
                dslogin = reader.GetString(4);
                dtbirt = Util.Conection.Int_to_Date(reader.GetInt32(5));
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
                              " DSCPF =  '" + dscpf + "'," +
                              " DSNOME = '" + dsnome + "'," +
                              " DSSENH = '" + dssenh + "'," +
                              " DSMAIL = '" + dsmail + "'," +
                              " DSLOG =  '" + dslogin + "'," +
                              " DTBRIT = "  + Util.Conection.Date_to_Int(dtbirt) + 
                              " WHERE DSCPF = '" + dscpf + "'";

            return Util.Conection.Update(sentenca);
        }

        #endregion Methods
    }
}