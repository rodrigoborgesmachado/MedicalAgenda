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
using static Agenda_de_Saude.Modelo.Enumerator;

namespace Agenda_de_Saude.Visao
{
    [Activity(Label = "AC_CadastroEnfermidade")]
    public class AC_CadastroEnfermidade : Activity
    {
        #region Extras

        /// <summary>
        /// Intent from tratament's code
        /// </summary>
        string CODIGO_ENFERMIDADE = "AC_CadastroMedicamento.CODIGO_ENFERMIDADE ";

        #endregion Extras

        #region Atributos

        /// <summary>
        /// O que está sendo efetuado
        /// </summary>
        Tarefa work = Tarefa.INCLUINDO;

        /// <summary>
        /// Atributo referente à enfermidade
        /// </summary>
        MD_Enfermidade enfermidade;

        #endregion Atributos

        #region Componentes Visuais

        /// <summary>
        /// Compomente de texto do nome da tela
        /// </summary>
        EditText edt_la_ac_cadastro_enfermidade_nome;

        /// <summary>
        /// Compomente de texto do comentário da tela
        /// </summary>
        EditText edt_la_ac_cadastro_enfermidade_comentario;

        /// <summary>
        /// Compomente de texto da situação da tela
        /// </summary>
        EditText edt_la_ac_cadastro_enfermidade_situacao;

        /// <summary>
        /// Compomente de texto do tipo da tela
        /// </summary>
        EditText edt_la_ac_cadastro_enfermidade_tipo;

        /// <summary>
        /// Botão de confirmar da tela
        /// </summary>
        Button btn_la_ac_cadastro_enfermidade_Confirmar;

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
        /// Evento lançado no clique do botão de incluir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_la_ac_cadastro_enfermidade_Confirmar_Click(object sender, EventArgs e)
        {
            Incluir();
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Method that inicialize the activity
        /// </summary>
        public void InicializeActivity()
        {
            SetContentView(Resource.Layout.AC_CadastroEnfermidades);

            CarregaComponentesVisuais();

            if (Intent.GetStringExtra("CODIGO_ENFERMIDADE") != null)
            {
                CODIGO_ENFERMIDADE = Intent.GetStringExtra("CODIGO_ENFERMIDADE");
                enfermidade = new MD_Enfermidade(int.Parse(CODIGO_ENFERMIDADE));
                work = Tarefa.EDITANDO;
                PreencherTratamento();
            }
        }

        /// <summary>
        /// Method that creates all visual components
        /// </summary>
        public void CarregaComponentesVisuais()
        {
            edt_la_ac_cadastro_enfermidade_nome = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_enfermidade_nome);
            edt_la_ac_cadastro_enfermidade_comentario = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_enfermidade_comentario);
            edt_la_ac_cadastro_enfermidade_situacao = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_enfermidade_situacao);
            edt_la_ac_cadastro_enfermidade_tipo = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_enfermidade_tipo);
            btn_la_ac_cadastro_enfermidade_Confirmar = FindViewById<Button>(Resource.Id.btn_la_ac_cadastro_enfermidade_Confirmar);

            btn_la_ac_cadastro_enfermidade_Confirmar.Click += Btn_la_ac_cadastro_enfermidade_Confirmar_Click;
        }

        /// <summary>
        /// Método para preencher os dados do tratamento
        /// </summary>
        public void PreencherTratamento()
        {
            if(enfermidade != null)
            {
                this.edt_la_ac_cadastro_enfermidade_nome.Text = string.IsNullOrEmpty(enfermidade.Nome) ? "" : enfermidade.Nome;
                this.edt_la_ac_cadastro_enfermidade_comentario.Text = string.IsNullOrEmpty(enfermidade.Comentario) ? "" : enfermidade.Comentario;
                this.edt_la_ac_cadastro_enfermidade_situacao.Text = string.IsNullOrEmpty(enfermidade.Situacao) ? "" : enfermidade.Situacao;
                this.edt_la_ac_cadastro_enfermidade_tipo.Text = string.IsNullOrEmpty(enfermidade.Tipo) ? "" : enfermidade.Tipo;
            }
        }

        /// <summary>
        /// Método que valida se os campos do formulário foram preenchidos corretamente
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool ValidaCampos(ref string mensagem)
        {
            bool valido = true;
            mensagem = "";
            if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_enfermidade_nome.Text))
            {
                valido = false;
                mensagem = "Campo nome está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_enfermidade_comentario.Text))
            {
                valido = false;
                mensagem = "Campo comentário está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_enfermidade_situacao.Text))
            {
                valido = false;
                mensagem = "Campo situação está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_enfermidade_tipo.Text))
            {
                valido = false;
                mensagem = "Campo tipo está vazio!";
            }
            return valido;
        }

        /// <summary>
        /// Método que faz a inserção da enfermidade
        /// </summary>
        public void Incluir()
        {
            string mensagem = "";
            if(!ValidaCampos(ref mensagem))
            {
                Util.Global.Alerta_Class.ApresentaAlerta(mensagem, this);
                return;
            }
            else
            {
                if(work == Tarefa.INCLUINDO)
                    enfermidade = new MD_Enfermidade(MD_Enfermidade.NovoCodigo());
                enfermidade.Nome = edt_la_ac_cadastro_enfermidade_nome.Text;
                enfermidade.Comentario = edt_la_ac_cadastro_enfermidade_comentario.Text;
                enfermidade.Situacao = edt_la_ac_cadastro_enfermidade_situacao.Text;
                enfermidade.Tipo = edt_la_ac_cadastro_enfermidade_tipo.Text;
                bool insert = work == Tarefa.INCLUINDO ? enfermidade.Insert() : enfermidade.Update();
                if (insert)
                {
                    Util.Global.Alerta_Class.ApresentaToast("Enfermidade " + (work == Tarefa.EDITANDO ? "editado" : "incluído") + " com sucesso!", this);
                    this.SetResult(Result.Ok);
                    this.Finish();
                }
                else
                {
                    Util.Global.Alerta_Class.ApresentaAlerta("Enfermidade " + (work == Tarefa.EDITANDO ? "editado" : "incluído") + " com ERRO!", this);
                }
            }
        }

        #endregion Métodos
    }
}