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
    [Activity(Label = "AC_CadastroExame")]
    public class AC_CadastroExame : Activity
    {
        #region Extras

        /// <summary>
        /// Intent from exame's code
        /// </summary>
        string CODIGO_EXAME = "AC_CadastroMedicamento.CODIGO_EXAME";

        #endregion Extras

        #region Atributos

        /// <summary>
        /// O que está sendo efetuado
        /// </summary>
        Tarefa work = Tarefa.INCLUINDO;

        /// <summary>
        /// Atributo referente ao medicamento
        /// </summary>
        MD_Exame exame;

        #endregion Atributos

        #region Componentes Visuais

        /// <summary>
        /// Compomente de texto do nome da tela
        /// </summary>
        EditText edt_la_ac_cadastro_exame_nome;

        /// <summary>
        /// Compomente de texto do comentário da tela
        /// </summary>
        EditText edt_la_ac_cadastro_exame_comentario;

        /// <summary>
        /// Botão de anexo 
        /// </summary>
        Button btn_la_ac_cadastro_exame_anexo;

        /// <summary>
        /// Botão de confirmar da tela
        /// </summary>
        Button btn_la_ac_cadastro_exame_Confirmar;

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
        /// Evento lançado no clique do botão anexar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_la_ac_cadastro_exame_anexo_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Evento lançado no clique de confirmar no cadastro do exame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_la_ac_cadastro_exame_Confirmar_Click(object sender, EventArgs e)
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
            SetContentView(Resource.Layout.AC_CadastroExame);

            CarregaComponentesVisuais();

            if (Intent.GetStringExtra("CODIGO_EXAME") != null)
            {
                CODIGO_EXAME = Intent.GetStringExtra("CODIGO_EXAME");
                exame = new MD_Exame(int.Parse(CODIGO_EXAME));
                work = Tarefa.EDITANDO;
                PreencherTratamento();
            }
        }

        /// <summary>
        /// Method that creates all visual components
        /// </summary>
        public void CarregaComponentesVisuais()
        {
            edt_la_ac_cadastro_exame_nome = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_exame_nome);
            edt_la_ac_cadastro_exame_comentario = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_exame_comentario);
            btn_la_ac_cadastro_exame_anexo = FindViewById<Button>(Resource.Id.btn_la_ac_cadastro_exame_anexo);
            btn_la_ac_cadastro_exame_Confirmar = FindViewById<Button>(Resource.Id.btn_la_ac_cadastro_exame_Confirmar);

            btn_la_ac_cadastro_exame_anexo.Click += Btn_la_ac_cadastro_exame_anexo_Click;
            btn_la_ac_cadastro_exame_Confirmar.Click += Btn_la_ac_cadastro_exame_Confirmar_Click;
        }

        /// <summary>
        /// Método para preencher os dados do tratamento
        /// </summary>
        public void PreencherTratamento()
        {
            if (exame != null)
            {
                this.edt_la_ac_cadastro_exame_nome.Text = string.IsNullOrEmpty(exame.Nome) ? "" : exame.Nome;
                this.edt_la_ac_cadastro_exame_comentario.Text = string.IsNullOrEmpty(exame.Comentario) ? "" : exame.Comentario;
                this.btn_la_ac_cadastro_exame_anexo.Text = string.IsNullOrEmpty(exame.Caminho_Arquivo) ? "" : exame.Caminho_Arquivo;
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
            if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_exame_nome.Text))
            {
                valido = false;
                mensagem = "Campo nome está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_exame_comentario.Text))
            {
                valido = false;
                mensagem = "Campo comentário está vazio!";
            }

            return valido;
        }

        /// <summary>
        /// Método que faz a inserção da enfermidade
        /// </summary>
        public void Incluir()
        {
            string mensagem = "";
            if (!ValidaCampos(ref mensagem))
            {
                Util.Global.Alerta_Class.ApresentaAlerta(mensagem, this);
                return;
            }
            else
            {
                if (work == Tarefa.INCLUINDO)
                    exame = new MD_Exame(MD_Exame.NovoCodigo());
                exame.Nome = edt_la_ac_cadastro_exame_nome.Text;
                exame.Comentario = edt_la_ac_cadastro_exame_comentario.Text;
                exame.Caminho_Arquivo= btn_la_ac_cadastro_exame_anexo.Text;
                
                bool insert = work == Tarefa.INCLUINDO ? exame.Insert() : exame.Update();
                if (insert)
                {
                    Util.Global.Alerta_Class.ApresentaToast("Exame " + (work == Tarefa.EDITANDO ? "editado" : "incluído") + " com sucesso!", this);
                    this.SetResult(Result.Ok);
                    this.Finish();
                }
                else
                {
                    Util.Global.Alerta_Class.ApresentaAlerta("Exame " + (work == Tarefa.EDITANDO ? "editado" : "incluído") + " com ERRO!", this);
                }
            }
        }

        #endregion Métodos
    }
}