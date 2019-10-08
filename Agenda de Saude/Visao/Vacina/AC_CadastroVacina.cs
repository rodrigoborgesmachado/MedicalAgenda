namespace Agenda_de_Saude.Visao
{
    using System;
    using Agenda_de_Saude.Modelo;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Widget;
    using Java.Lang;
    using static Agenda_de_Saude.Modelo.Enumerator;

    [Activity(Label = "AC_CadastroVacina")]
    public class AC_CadastroVacina : Activity
    {
        #region Componentes Visuais

        /// <summary>
        /// Componente Edit Text para o campo nome do cadastro de vacina.
        /// </summary>
        private EditText edt_la_ac_cadastrovacina_nome;

        /// <summary>
        /// Componente Edit Text para o campo data.
        /// </summary>
        private EditText edt_la_ac_cadastrovacina_data;

        /// <summary>
        /// Componente Edit Text para o campo dose.
        /// </summary>
        private EditText edt_la_ac_cadastrovacina_dose;

        /// <summary>
        /// Componente Button para confirmar o cadastro.
        /// </summary>
        private Button btn_la_ac_cadastrovacina;

        /// <summary>
        /// Nome da Vacina.
        /// </summary>
        private string nome;

        /// <summary>
        /// Data.
        /// </summary>
        private string data;

        /// <summary>
        /// Dose.
        /// </summary>
        private string dose;

        /// <summary>
        /// Mensagem de erro genérica.
        /// </summary>
        private string mensagemErro = string.Empty;

        /// <summary>
        /// Variável para a classe MD_Vacina.
        /// </summary>
        MD_Vacina vacina;

        #endregion Componentes Visuais

        #region Eventos

        /// <summary>
        /// Evento disparado no OnCreate da Activity.
        /// </summary>
        /// <param name="savedInstanceState">O estado da instância.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Intent.HasExtra("CODIGO"))
                nome = Intent.GetStringExtra("CODIGO");

            InicializaActivity();
        }

        /// <summary>
        /// Evento disparado no onclick do campo de data.
        /// </summary>
        private void OnClickDataEditText()
        {
            if (edt_la_ac_cadastrovacina_data.IsFocused)
            {
                DatePickerDialog datePicker = new DatePickerDialog(this);
                datePicker.Show();
                int ano = datePicker.DatePicker.DateTime.Year;
                int mes = datePicker.DatePicker.DateTime.Month;
                int dia = datePicker.DatePicker.DateTime.Day;
                datePicker.DateSet += delegate { OnDataSet(ano, mes, dia); };
            }
        }
        
        /// <summary>
        /// Evento disparado ao confirmar a data selecionada. //TODO => Acho interessante passar para uma biblioteca.
        /// </summary>
        /// <param name="year">O ano selecionado.</param>
        /// <param name="month">O mês selecionado.</param>
        /// <param name="dayOfMonth">O dia selecionado.</param>
        private void OnDataSet(int ano, int mes, int dia)
        {
            DateTime data = new DateTime(ano, mes, dia);
            string dataFormatada = data.Day.ToString("00") + "/" + data.Month.ToString("00") + "/" + data.Year;
            FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrousuario_data).Text = dataFormatada;
        }

        /// <summary>
        /// Evento disparado no clique do botão confirmar.
        /// </summary>
        private void OnConfirmarClick()
        {
            if (!ValidaCampos())
            {
                Util.Global.Alerta_Class.ApresentaToast(mensagemErro, ToastLength.Long, this);
                return;
            }

            InstanciaVacinaInsereAtualiza(Intent.HasExtra("CODIGO"));
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Método para inicializar a activity.
        /// </summary>
        private void InicializaActivity()
        {
            SetContentView(Resource.Layout.LA_AC_CadatroVacina);

            CarregaComponentesVisuais();
        }

        /// <summary>
        /// Método para carregar os componentes visuais.
        /// </summary>
        private void CarregaComponentesVisuais()
        {
            edt_la_ac_cadastrovacina_nome = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrovacina_nome);
            edt_la_ac_cadastrovacina_data = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrousuario_data);
            edt_la_ac_cadastrovacina_dose = FindViewById<EditText>(Resource.Id.edt_la_ac_cadastrovacina_dose);
            btn_la_ac_cadastrovacina = FindViewById<Button>(Resource.Id.btn_la_ac_cadastrousuario_Confirmar);

            if (Intent.HasExtra("CODIGO"))
            {
                vacina = new MD_Vacina(nome);
                edt_la_ac_cadastrovacina_nome.Text = vacina.Nome;
                edt_la_ac_cadastrovacina_data.Text = vacina.Data_aplicacao;
                edt_la_ac_cadastrovacina_dose.Text = vacina.Dose.ToString();
            }

            edt_la_ac_cadastrovacina_data.FocusChange += delegate { OnClickDataEditText(); };
            btn_la_ac_cadastrovacina.Click += delegate { OnConfirmarClick(); };
        }
        
        /// <summary>
        /// Verifica se todos os campos foram preenchidos corretamente.
        /// </summary>
        /// <returns>Verdadeiro, caso os campos estejam preenchidos corretamente. Falso caso contrário.</returns>
        private bool ValidaCampos()
        {
            nome = edt_la_ac_cadastrovacina_nome.Text.ToString();
            data = edt_la_ac_cadastrovacina_data.Text.ToString();
            dose = edt_la_ac_cadastrovacina_dose.Text.ToString();

            if (string.IsNullOrEmpty(nome))
            {
                mensagemErro = "O campo nome da vacina não foi preenchido!";
                return false;
            }

            if (string.IsNullOrEmpty(data))
            {
                mensagemErro = "O campo data não foi preenchido!";
                return false;
            }

            if (string.IsNullOrEmpty(dose))
            {
                mensagemErro = "O campo dose não foi preenchido!";
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Realiza a inserção / atualização da vacina.
        /// </summary>
        /// <param name="update">True se for atualizar. False se for inserir.</param>
        private void InstanciaVacinaInsereAtualiza(bool update)
        {
            vacina = new MD_Vacina(nome);
            vacina.Data_aplicacao = data;
            vacina.Dose = Integer.ParseInt(dose);

            string mensagemSucesso = update ? "Vacina atualizada com sucesso!" : "Vacina cadastrada com sucesso!";
            string mensagemFalha = update ? "Falha ao atualizar vacina!" : "Falha ao inserir a vacina!";

            if (update ? vacina.Update() : vacina.Insert())
            {
                Util.Global.Alerta_Class.ApresentaToast(
                    mensagemSucesso,
                    this);
                this.SetResult(Result.Ok);
            }
            else
            {
                Util.Global.Alerta_Class.ApresentaAlerta(
                    "Erro",
                    mensagemFalha,
                    "OK",
                    this,
                    delegate {
                        this.SetResult(Result.Canceled);
                        this.Finish();
                    });
            }
        }

        #endregion Métodos
    }
}