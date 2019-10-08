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
    [Activity(Label = "AC_CadastroTratamento")]
    public class AC_CadastroTratamento : Activity
    {
        #region Extras

        /// <summary>
        /// Intent from tratament's code
        /// </summary>
        string CODIGO_TRATAMENTO = "AC_CadastroTratamento.CODIGO_TRATAMENTO ";

        #endregion Extras

        #region Componentes Visuais

        /// <summary>
        /// Compomente de texto do nome da tela
        /// </summary>
        EditText edt_la_ac_cadastro_tratamento_nome;

        /// <summary>
        /// Componente de checkbox 
        /// </summary>
        CheckBox ckb_la_ac_cadastro_tratamento_finalizado;

        /// <summary>
        /// Compomente de texto de data de início da tela
        /// </summary>
        EditText edt_la_ac_cadastro_tratamento_datainicio;

        /// <summary>
        /// Compomente de texto de data fiinal da tela
        /// </summary>
        EditText edt_la_ac_cadastro_tratamento_datafim;

        /// <summary>
        /// Compomente de texto do quantidade da tela
        /// </summary>
        EditText edt_la_ac_cadastro_tratamento_periodicidade;

        /// <summary>
        /// Compomente de texto de comentário da tela
        /// </summary>
        EditText edt_la_ac_cadastro_tratamento_comentario;

        /// <summary>
        /// Botão de confirmar da tela
        /// </summary>
        Button btn_la_ac_cadastro_tratamento_Confirmar;

        #endregion Componentes Visuais

        #region Atributos

        /// <summary>
        /// O que está sendo efetuado
        /// </summary>
        Tarefa work = Tarefa.INCLUINDO;

        /// <summary>
        /// Atributo referente ao tratamento
        /// </summary>
        MD_Tratamento tratamento;

        #endregion Atributos

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
        /// Evento lançado no clique do botão confirmar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_la_ac_cadastro_tratamento_Confirmar_Click(object sender, EventArgs e)
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
            SetContentView(Resource.Layout.AC_CadastroTratamentos);

            CarregaComponentesVisuais();

            if (Intent.GetStringExtra("CODIGO_TRATAMENTO") != null)
            {
                CODIGO_TRATAMENTO = Intent.GetStringExtra("CODIGO_TRATAMENTO");
                tratamento = new MD_Tratamento(int.Parse(CODIGO_TRATAMENTO));
                work = Tarefa.EDITANDO;
                PreencherTratamento();
            }
        }

        /// <summary>
        /// Method that creates all visual components
        /// </summary>
        public void CarregaComponentesVisuais()
        {
            int idDataInicio = Resource.Id.edt_la_ac_cadastro_tratamento_datainicio;
            int idDataFim = Resource.Id.edt_la_ac_cadastro_tratamento_datafim;

            edt_la_ac_cadastro_tratamento_nome = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_tratamento_nome);
            ckb_la_ac_cadastro_tratamento_finalizado = FindViewById<CheckBox>(Resource.Id.ckb_la_ac_cadastro_tratamento_finalizado);
            edt_la_ac_cadastro_tratamento_datainicio = FindViewById<EditText>(idDataInicio);
            edt_la_ac_cadastro_tratamento_datafim = FindViewById<EditText>(idDataFim);
            edt_la_ac_cadastro_tratamento_periodicidade = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_tratamento_periodicidade);
            edt_la_ac_cadastro_tratamento_comentario = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_tratamento_comentario);
            btn_la_ac_cadastro_tratamento_Confirmar = FindViewById<Button>(Resource.Id.btn_la_ac_cadastro_tratamento_Confirmar);

            btn_la_ac_cadastro_tratamento_Confirmar.Click += Btn_la_ac_cadastro_tratamento_Confirmar_Click;
            edt_la_ac_cadastro_tratamento_datainicio.FocusChange += delegate { OnClickDataEditText(edt_la_ac_cadastro_tratamento_datainicio, idDataInicio); };
            edt_la_ac_cadastro_tratamento_datafim.FocusChange += delegate { OnClickDataEditText(edt_la_ac_cadastro_tratamento_datafim, idDataFim); };
        }

        /// <summary>
        /// Evento disparado no onclick do campo de data.
        /// </summary>
        private void OnClickDataEditText(EditText data, int idEditText)
        {
            if (data.IsFocused)
            {
                DatePickerDialog datePicker = new DatePickerDialog(this);
                datePicker.Show();
                datePicker.DateSet += delegate { OnDataSet(datePicker.DatePicker.DateTime.Year, datePicker.DatePicker.DateTime.Month, datePicker.DatePicker.DateTime.Day, idEditText); };
            }
        }

        /// <summary>
        /// Evento disparado ao confirmar a data selecionada. //TODO => Acho interessante passar para uma biblioteca.
        /// </summary>
        /// <param name="year">O ano selecionado.</param>
        /// <param name="month">O mês selecionado.</param>
        /// <param name="dayOfMonth">O dia selecionado.</param>
        private void OnDataSet(int ano, int mes, int dia, int editText)
        {
            DateTime data = new DateTime(ano, mes, dia);
            string dataFormatada = data.Day.ToString("00") + "/" + data.Month.ToString("00") + "/" + data.Year;
            FindViewById<EditText>(editText).Text = dataFormatada;
        }

        /// <summary>
        /// Método para preencher os dados do tratamento
        /// </summary>
        public void PreencherTratamento()
        {
            if (tratamento != null)
            {
                edt_la_ac_cadastro_tratamento_nome.Text = tratamento.Nome;
                edt_la_ac_cadastro_tratamento_datainicio.Text = tratamento.Data_Inicio.ToString();
                edt_la_ac_cadastro_tratamento_datafim.Text = tratamento.Data_Fim.ToString();
                edt_la_ac_cadastro_tratamento_periodicidade.Text = tratamento.Periodicidade.ToString();
                edt_la_ac_cadastro_tratamento_comentario.Text = tratamento.Comentario;
                ckb_la_ac_cadastro_tratamento_finalizado.Checked = tratamento.Finalizado == "1";
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
            if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_tratamento_nome.Text))
            {
                valido = false;
                mensagem = "Campo nome está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_tratamento_datainicio.Text))
            {
                valido = false;
                mensagem = "Campo Data de Início de tempo está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_tratamento_periodicidade.Text))
            {
                valido = false;
                mensagem = "Campo periodicidade está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_tratamento_comentario.Text))
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
                    tratamento = new MD_Tratamento(MD_Tratamento.NovoCodigo());
                tratamento.Nome = edt_la_ac_cadastro_tratamento_nome.Text;
                tratamento.Finalizado = ckb_la_ac_cadastro_tratamento_finalizado.Checked ? "1" : "0"; 
                tratamento.Data_Inicio = DateTime.Parse(edt_la_ac_cadastro_tratamento_datainicio.Text);
                tratamento.Data_Fim = DateTime.Parse(edt_la_ac_cadastro_tratamento_datafim.Text);
                tratamento.Periodicidade = int.Parse(edt_la_ac_cadastro_tratamento_periodicidade.Text);
                tratamento.Comentario = edt_la_ac_cadastro_tratamento_comentario.Text;

                bool insert = work == Tarefa.INCLUINDO ? tratamento.Insert() : tratamento.Update();
                if (insert)
                {
                    Util.Global.Alerta_Class.ApresentaToast("Tratamento " + (work == Tarefa.EDITANDO ? "editado" : "incluído") + " com sucesso!", this);
                    this.SetResult(Result.Ok);
                    this.Finish();
                }
                else
                {
                    Util.Global.Alerta_Class.ApresentaAlerta("Tratamento " + (work == Tarefa.EDITANDO ? "editado" : "incluído") + " com ERRO!", this);
                }
            }
        }

        #endregion Métodos
    }
}