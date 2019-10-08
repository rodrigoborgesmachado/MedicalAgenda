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
    [Activity(Label = "AC_CadastroMedicamento")]
    public class AC_CadastroMedicamento : Activity
    {
        #region Extras

        /// <summary>
        /// Intent from tratament's code
        /// </summary>
        string CODIGO_MEDICAMENTO = "AC_CadastroMedicamento.CODIGO_MEDICAMENTO ";

        #endregion Extras

        #region Componentes Visuais

        /// <summary>
        /// Compomente de texto do nome da tela
        /// </summary>
        EditText edt_la_ac_cadastro_medicamento_nome;

        /// <summary>
        /// Compomente de texto do medida de tempo da tela
        /// </summary>
        EditText edt_la_ac_cadastro_medicamento_medida_tempo;

        /// <summary>
        /// Compomente de texto do formato da tela
        /// </summary>
        EditText edt_la_ac_cadastro_medicamento_formato;

        /// <summary>
        /// Compomente de texto do quantidade da tela
        /// </summary>
        EditText edt_la_ac_cadastro_medicamento_quantidade;

        /// <summary>
        /// Compomente de texto do medida de peso da tela
        /// </summary>
        EditText edt_la_ac_cadastro_medicamento_medida_peso;

        /// <summary>
        /// Compomente de texto periodicidade da tela
        /// </summary>
        EditText edt_la_ac_cadastro_medicamento_periodicidade;
        
        /// <summary>
        /// Botão de confirmar da tela
        /// </summary>
        Button btn_la_ac_cadastro_medicamento_Confirmar;

        #endregion Componentes Visuais

        #region Atributos

        /// <summary>
        /// O que está sendo efetuado
        /// </summary>
        Tarefa work = Tarefa.INCLUINDO;

        /// <summary>
        /// Atributo referente ao medicamento
        /// </summary>
        MD_Medicamento medicamento;

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
        private void Btn_la_ac_cadastro_medicamento_Confirmar_Click(object sender, EventArgs e)
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
            SetContentView(Resource.Layout.AC_CadastroMedicamento);

            CarregaComponentesVisuais();

            if (Intent.GetStringExtra("CODIGO_MEDICAMENTO") != null)
            {
                CODIGO_MEDICAMENTO = Intent.GetStringExtra("CODIGO_MEDICAMENTO");
                medicamento = new MD_Medicamento(int.Parse(CODIGO_MEDICAMENTO));
                work = Tarefa.EDITANDO;
                PreencherTratamento();
            }
        }

        /// <summary>
        /// Method that creates all visual components
        /// </summary>
        public void CarregaComponentesVisuais()
        {
            edt_la_ac_cadastro_medicamento_nome = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_medicamento_nome);
            edt_la_ac_cadastro_medicamento_medida_tempo = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_medicamento_medida_tempo);
            edt_la_ac_cadastro_medicamento_formato = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_medicamento_formato);
            edt_la_ac_cadastro_medicamento_quantidade = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_medicamento_quantidade);
            edt_la_ac_cadastro_medicamento_medida_peso = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_medicamento_medida_peso);
            edt_la_ac_cadastro_medicamento_periodicidade = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastro_medicamento_periodicidade);
            btn_la_ac_cadastro_medicamento_Confirmar = FindViewById<Button>(Resource.Id.btn_la_ac_cadastro_medicamento_Confirmar);

            btn_la_ac_cadastro_medicamento_Confirmar.Click += Btn_la_ac_cadastro_medicamento_Confirmar_Click;
        }

        /// <summary>
        /// Método para preencher os dados do tratamento
        /// </summary>
        public void PreencherTratamento()
        {
            if(medicamento != null)
            {
                edt_la_ac_cadastro_medicamento_nome.Text = medicamento.Nome;
                edt_la_ac_cadastro_medicamento_medida_tempo.Text = medicamento.Medida_Tempo;
                edt_la_ac_cadastro_medicamento_formato.Text = medicamento.Formato;
                edt_la_ac_cadastro_medicamento_quantidade.Text = medicamento.Quantidade.ToString();
                edt_la_ac_cadastro_medicamento_medida_peso.Text = medicamento.Medida_Peso;
                edt_la_ac_cadastro_medicamento_periodicidade.Text = medicamento.Periodicidade.ToString();
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
            if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_medicamento_nome.Text))
            {
                valido = false;
                mensagem = "Campo nome está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_medicamento_medida_tempo.Text))
            {
                valido = false;
                mensagem = "Campo medida de tempo está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_medicamento_formato.Text))
            {
                valido = false;
                mensagem = "Campo formato está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_medicamento_quantidade.Text))
            {
                valido = false;
                mensagem = "Campo quantidade está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_medicamento_medida_peso.Text))
            {
                valido = false;
                mensagem = "Campo medida de peso está vazio!";
            }
            else if (string.IsNullOrEmpty(this.edt_la_ac_cadastro_medicamento_periodicidade.Text))
            {
                valido = false;
                mensagem = "Campo periodicidade de peso está vazio!";
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
                    medicamento = new MD_Medicamento(MD_Medicamento.NovoCodigo());
                medicamento.Nome = edt_la_ac_cadastro_medicamento_nome.Text;
                medicamento.Medida_Tempo = edt_la_ac_cadastro_medicamento_medida_tempo.Text;
                medicamento.Formato = edt_la_ac_cadastro_medicamento_formato.Text;
                medicamento.Quantidade = int.Parse(edt_la_ac_cadastro_medicamento_quantidade.Text);
                medicamento.Medida_Peso = edt_la_ac_cadastro_medicamento_medida_peso.Text;
                medicamento.Periodicidade = int.Parse(edt_la_ac_cadastro_medicamento_periodicidade.Text);

                bool insert = work == Tarefa.INCLUINDO ? medicamento.Insert() : medicamento.Update();
                if (insert)
                {
                    Util.Global.Alerta_Class.ApresentaToast("Medicamento " + (work == Tarefa.EDITANDO ? "editado" : "incluído") + " com sucesso!", this);
                    this.SetResult(Result.Ok);
                    this.Finish();
                }
                else
                {
                    Util.Global.Alerta_Class.ApresentaAlerta("Medicamento " + (work == Tarefa.EDITANDO ? "editado" : "incluído") + " com ERRO!", this);
                }
            }
        }

        #endregion Métodos
    }
}