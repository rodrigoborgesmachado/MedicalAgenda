using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agenda_de_Saude.Modelo;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using static Agenda_de_Saude.Modelo.Enumerator;

namespace Agenda_de_Saude.Visao
{
    [Activity(Label = "AC_CadastroUsuario")]
    public class AC_CadastroUsuario : Activity
    {
        #region Extras

        /// <summary>
        /// Intent from user's cpf 
        /// </summary>
        string CPF_USUARIO = "AC_AC_CadastroUsuario.CPF_USUARIO";

        #endregion Extras

        #region Atributos

        /// <summary>
        /// O que está sendo efetuado
        /// </summary>
        Tarefa work = Tarefa.INCLUINDO;

        /// <summary>
        /// Atributo referente ao usuário
        /// </summary>
        MD_Usuario user = null;

        #endregion Atributos

        #region Componentes Visuais

        /// <summary>
        /// EditText from login
        /// </summary>
        EditText edt_la_ac_cadastrousuario_login;

        /// <summary>
        /// EditText from password
        /// </summary>
        EditText edt_la_ac_cadastrousuario_pass;

        /// <summary>
        /// EditText from name
        /// </summary>
        EditText edt_la_ac_cadastrousuario_name;

        /// <summary>
        /// EditText from CPF
        /// </summary>
        EditText edt_la_ac_cadastrousuario_cpf;

        /// <summary>
        /// EditText from birthday
        /// </summary>
        EditText edt_la_ac_cadastrousuario_bithday;

        /// <summary>
        /// EditText from email
        /// </summary>
        EditText edt_la_ac_cadastrousuario_email;

        /// <summary>
        /// Button from confirm button
        /// </summary>
        Button btn_la_ac_cadastrousuario_Confirmar;

        /// <summary>
        /// Controle de eventos da classe
        /// </summary>
        bool lockchange = false;

        #endregion Componentes Visuais 

        #region Eventos

        /// <summary>
        /// Event taken when the activity starts
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InicializeActivity();
        }

        /// <summary>
        /// Event taken when the button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_la_ac_cadastrousuario_Confirmar_Click(object sender, EventArgs e)
        {
            Inserir(sender);
        }

        /// <summary>
        /// Event taked when the text from Edt_la_ac_cadastrousuario_bithday changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edt_la_ac_cadastrousuario_cpf_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if (lockchange) return;

            if (string.IsNullOrEmpty(edt_la_ac_cadastrousuario_cpf.Text))
                return;

            lockchange = true;

            if (!(edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('0') || 
                edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('1') || 
                edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('2') || 
                edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('3') || 
                edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('4') || 
                edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('5') || 
                edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('6') || 
                edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('7') || 
                edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('8') || 
                edt_la_ac_cadastrousuario_cpf.Text.ElementAt(edt_la_ac_cadastrousuario_cpf.Text.Length - 1).ToString().Contains('9')))
            {
                edt_la_ac_cadastrousuario_cpf.Text = edt_la_ac_cadastrousuario_cpf.Text.ToString().Substring(0, edt_la_ac_cadastrousuario_cpf.Text.ToString().Length - 1);
            }

            lockchange = false;
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Method that inicialize the activity
        /// </summary>
        public void InicializeActivity()
        {
            SetContentView(Resource.Layout.LA_AC_CadastroUsuario);

            CarregaComponentesVisuais();

            if (Intent.GetStringExtra("CPF_USUARIO") != null)
            {
                CPF_USUARIO = Intent.GetStringExtra("CPF_USUARIO");
                work = Tarefa.EDITANDO;
                PreencherUsu();
            }
        }

        /// <summary>
        /// Método para preencher os dados do usuário
        /// </summary>
        public void PreencherUsu()
        {
            user = new MD_Usuario(CPF_USUARIO);
            edt_la_ac_cadastrousuario_cpf.Enabled =
                edt_la_ac_cadastrousuario_email.Enabled = false;

            edt_la_ac_cadastrousuario_login.Text = user.Login_usuario;
            edt_la_ac_cadastrousuario_pass.Text = user.Senha_usuario;
            edt_la_ac_cadastrousuario_name.Text = user.Nome_usuario;
            edt_la_ac_cadastrousuario_cpf.Text = user.CPF_Usuario;
            edt_la_ac_cadastrousuario_bithday.Text = user.Data_aniversario.ToShortDateString();
            edt_la_ac_cadastrousuario_email.Text = user.Email_usuario;
        }

        /// <summary>
        /// Method that creates all visual components
        /// </summary>
        public void CarregaComponentesVisuais()
        {
            edt_la_ac_cadastrousuario_login = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrousuario_login);
            edt_la_ac_cadastrousuario_pass = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrousuario_pass);
            edt_la_ac_cadastrousuario_name = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrousuario_name);
            edt_la_ac_cadastrousuario_cpf = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrousuario_cpf);
            edt_la_ac_cadastrousuario_bithday = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrousuario_bithday);
            edt_la_ac_cadastrousuario_email = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrousuario_email);
            btn_la_ac_cadastrousuario_Confirmar = FindViewById<Button>(Resource.Id.btn_la_ac_cadastrousuario_Confirmar);

            edt_la_ac_cadastrousuario_cpf.AfterTextChanged += Edt_la_ac_cadastrousuario_cpf_AfterTextChanged; 
            btn_la_ac_cadastrousuario_Confirmar.Click += Btn_la_ac_cadastrousuario_Confirmar_Click;

        }

        /// <summary>
        /// Método que valida os valores a inserem inseridos
        /// </summary>
        /// <returns>True - Válido; False - Inválido</returns>
        private bool ValidaValores(ref string mensagem)
        {
            mensagem = "";
            bool retorno = true;

            if (string.IsNullOrEmpty(edt_la_ac_cadastrousuario_login.Text.ToString()))
            {
                retorno = false;
                mensagem = "Campo \"Login\" vazio!";
            }
            else if (string.IsNullOrEmpty(edt_la_ac_cadastrousuario_pass.Text.ToString()))
            {
                retorno = false;
                mensagem = "Campo \"Password\" vazio!";
            }
            else if (string.IsNullOrEmpty(edt_la_ac_cadastrousuario_name.Text.ToString()))
            {
                retorno = false;
                mensagem = "Campo \"Password\" vazio!";
            }
            else if (string.IsNullOrEmpty(edt_la_ac_cadastrousuario_cpf.Text.ToString()))
            {
                retorno = false;
                mensagem = "Campo \"CPF\" vazio!";
            }
            else if (string.IsNullOrEmpty(edt_la_ac_cadastrousuario_bithday.Text.ToString()))
            {
                retorno = false;
                mensagem = "Campo \"CPF\" vazio!";
            }
            else if (string.IsNullOrEmpty(edt_la_ac_cadastrousuario_email.Text.ToString()))
            {
                retorno = false;
                mensagem = "Campo \"CPF\" vazio!";
            }
            else if(edt_la_ac_cadastrousuario_pass.Text.Length < 5)
            {
                retorno = false;
                mensagem = "A senha precisa ter no mínimo 5 caracteres!";
            }

            return retorno;
        }

        /// <summary>
        /// Método que insere no banco o usuário
        /// </summary>
        private void Inserir(Object sender)
        {
            string mensagem = "";
            if (!ValidaValores(ref mensagem))
            {
                Util.Global.Alerta_Class.ApresentaToast(mensagem, ToastLength.Long, this);
                return;
            }

            if (user == null)
                user = new MD_Usuario(edt_la_ac_cadastrousuario_cpf.Text.ToString());
            user.Login_usuario = edt_la_ac_cadastrousuario_login.Text;
            user.Nome_usuario = edt_la_ac_cadastrousuario_name.Text;
            user.Senha_usuario = edt_la_ac_cadastrousuario_pass.Text;
            user.Email_usuario = edt_la_ac_cadastrousuario_email.Text;
            user.Data_aniversario = DateTime.Parse(edt_la_ac_cadastrousuario_bithday.Text.ToString());

            if (user.Insert())
            {
                lockchange = true;
                edt_la_ac_cadastrousuario_email.Text =
                edt_la_ac_cadastrousuario_bithday.Text =
                edt_la_ac_cadastrousuario_cpf.Text =
                edt_la_ac_cadastrousuario_name.Text =
                edt_la_ac_cadastrousuario_pass.Text =
                edt_la_ac_cadastrousuario_login.Text = "";
                lockchange = false;

                if (work == Tarefa.INCLUINDO)
                    Util.Global.Alerta_Class.ApresentaAlerta("Sucesso", "Usuário cadastrado!", "OK", this, delegate { this.SetResult(Result.Ok); });
                else
                {
                    Util.Global.Alerta_Class.ApresentaToast("Usuário alterado com sucesso!", this);
                    SetResult(Result.Ok);
                }
                    

            }
            else
            {
                Util.Global.Alerta_Class.ApresentaAlerta("Erro ao inserir o usuário!", this);
            }
        }

        #endregion Métodos
    }
}