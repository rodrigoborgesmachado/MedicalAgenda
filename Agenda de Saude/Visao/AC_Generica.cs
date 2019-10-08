using System;
using System.Collections.Generic;
using Agenda_de_Saude.Modelo;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Mono.Data.Sqlite;
using static Agenda_de_Saude.Modelo.Enumerator;

namespace Agenda_de_Saude.Visao
{
    [Activity(Label = "AC_Generica")]
    public class AC_Generica : Activity
    {
        #region Constantes

        /// <summary>
        /// Constante referente ao cadastro de cliente
        /// </summary>
        const int CADASTRAR = 0;

        #endregion Constantes

        #region Extras

        /// <summary>
        /// Intent from user's cpf 
        /// </summary>
        Tela TELA;
        
        /// <summary>
        /// O Itent que será instanciado de acordo com a tela que será exibida.
        /// </summary>
        private Intent intent;

        private SQLiteDatabase dataBase;

        private CursorAdapter dataSource;

        #endregion Extras

        #region Componentes Visuais

        /// <summary>
        /// Text view do título da tela
        /// </summary>
        TextView txv_ac_generica_nomeTela;

        FloatingActionButton btn_adiciona_item;

        /// <summary>
        /// List View que será carregada.
        /// </summary>
        private ListView ltv_ac_generica;

        #endregion Componentes Visuais

        #region Eventos

        /// <summary>
        /// Evento lançado na criação da atividade
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InicializeActivity();
        }

        /// <summary>
        /// Evento lançado na volta de uma activitie
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            this.ControlaCarregaListView();
        }

        /// <summary>
        /// Evento lançado no clique longo do item do list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ltv_ac_generica_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            AbreOpcoes(e.Id.ToString());   
        }

        /// <summary>
        /// Clique no botão para adicionar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewItemClick(object sender, EventArgs e)
        {
            AbreNovaTelaCadastro();
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Método que inicializa a activity
        /// </summary>
        public void InicializeActivity()
        {
            SetContentView(Resource.Layout.LA_AC_Generica);
            CarregaComponentesVisuais();

            if(Intent.GetIntExtra("TELA", -1) == -1)
            {
                Util.Global.Alerta_Class.ApresentaToast("Erro", this);
                this.SetResult(Result.Canceled);
            }

            TELA = (Tela)Intent.GetIntExtra("TELA", 0);

            ControlaCarregaListView();
        }

        /// <summary>
        /// Método que carrega os componentes visuais
        /// </summary>
        public void CarregaComponentesVisuais()
        {
            txv_ac_generica_nomeTela = FindViewById<TextView>(Resource.Id.txv_ac_generica_nomeTela);
            btn_adiciona_item = FindViewById<FloatingActionButton>(Resource.Id.fla_ac_generica_fab);
            ltv_ac_generica = FindViewById<ListView>(Resource.Id.ltv_ac_generica_listview);

            btn_adiciona_item.Click += OnNewItemClick;
            
            ltv_ac_generica.ItemLongClick += Ltv_ac_generica_ItemLongClick;
            this.ControlaCarregaListView();
        }

        /// <summary>
        /// Método que carrega o list view da tela
        /// </summary>
        private void ControlaCarregaListView()
        {
            switch (TELA)
            {
                case Tela.ENFERMIDADES:
                    txv_ac_generica_nomeTela.Text = "Enfermidades";
                    List<MD_Enfermidade> lista = MD_Enfermidade.ListaEnfermidades();
                    ltv_ac_generica.SetAdapter(new Ad_ListaEnfermidade(lista, this));
                    break;

                case Tela.EXAMES:
                    txv_ac_generica_nomeTela.Text = "Exames";
                    List<MD_Exame> lista1 = MD_Exame.ListaExame();
                    ltv_ac_generica.SetAdapter(new Ad_ListaExames(lista1, this));
                    break;

                case Tela.MEDICAMENTOS:
                    txv_ac_generica_nomeTela.Text = "Medicamentos";
                    List<MD_Medicamento> lista2 = MD_Medicamento.ListaMedicamento();
                    ltv_ac_generica.SetAdapter(new Ad_Lista_Medicamento(lista2, this));
                    break;

                case Tela.TRATAMENTOS:
                    txv_ac_generica_nomeTela.Text = "Tratamentos";
                    List<MD_Tratamento> lista3 = MD_Tratamento.ListaTratamento();
                    ltv_ac_generica.SetAdapter(new Ad_ListaTratamentos(lista3, this));
                    break;

                case Tela.VACINAS:
                    txv_ac_generica_nomeTela.Text = "Vacinas";
                    // Cria a lista de vacinas e seta o adapter para exibir a lista:
                    List<MD_Vacina> lista4 = MD_Vacina.ListaVacinas();
                    ltv_ac_generica.SetAdapter(new AD_ListaVacina(lista4, this));
                    break;
            }
        }

        /// <summary>
        /// Método que abre uma nova tela
        /// </summary>
        public void AbreNovaTelaCadastro(Tarefa tarefa = Tarefa.INCLUINDO, string o = "")
        {
            Activity ac = null;
            string extra = "";
            switch (TELA)
            {
                case Tela.TRATAMENTOS:
                    ac = new AC_CadastroTratamento();
                    extra = "CODIGO_TRATAMENTO";
                    break;
                case Tela.MEDICAMENTOS:
                    ac = new AC_CadastroMedicamento();
                    extra = "CODIGO_MEDICAMENTO";
                    break;
                case Tela.ENFERMIDADES:
                    ac = new AC_CadastroEnfermidade();
                    extra = "CODIGO_ENFERMIDADE";
                    break;
                case Tela.EXAMES:
                    ac = new AC_CadastroExame();
                    extra = "CODIGO_EXAME";
                    break;
                case Tela.VACINAS:
                    // Prepara a Activity de Cadastro de vacinas caso o botão inserir seja acionado.
                    ac = new AC_CadastroVacina();
                    extra = "CODIGO";
                    break;
                default:
                    ac = new AC_Generica();
                    break;
            }

            Intent intent = new Intent(this, ac.GetType());
            if (tarefa != Tarefa.INCLUINDO)
            {
                intent.PutExtra(extra, o);
            }
            
            StartActivityForResult(intent, CADASTRAR);
        }

        /// <summary>
        /// Cria um Dialog para solicitar ao usuário que confirme ou cancele a exclusão
        /// </summary>
        /// <param name="codigo">O código.</param>
        private void CriaAlertaExcluir(string codigo)
        {
            AlertDialog.Builder alerta = new AlertDialog.Builder(this);
            alerta.SetTitle("Exclusão " + txv_ac_generica_nomeTela.Text);
            alerta.SetMessage("Deseja realmente excluir " + (TELA == Tela.VACINAS || TELA == Tela.ENFERMIDADES ? "a " : "o ") + " " + txv_ac_generica_nomeTela.Text.Remove(txv_ac_generica_nomeTela.Text.Length-1, 1) + "? A Operação é irreversível!");
            alerta.SetPositiveButton("Sim", (senderAlert, args) =>
            {
                MDN_Model model = null;
                switch (TELA)
                {
                    case Tela.ENFERMIDADES:
                        model = new MD_Enfermidade(int.Parse(codigo));
                        break;
                    case Tela.EXAMES:
                        model = new MD_Exame(int.Parse(codigo));
                        break;
                    case Tela.MEDICAMENTOS:
                        model = new MD_Medicamento(int.Parse(codigo));
                        break;
                    case Tela.TRATAMENTOS:
                        model = new MD_Tratamento(int.Parse(codigo));
                        break;
                    case Tela.VACINAS:
                        model = new MD_Vacina(codigo);
                        break;
                }

                if (model.Delete())
                {
                    Toast.MakeText(this, "Exclusão realizada com sucesso!", ToastLength.Short).Show();
                    ControlaCarregaListView();
                }
                else
                {
                    Toast.MakeText(this, "Erro ao Excluir!", ToastLength.Short).Show();
                    this.SetResult(Result.Ok);
                    this.Finish();
                }
            });
            alerta.SetNegativeButton("Não", (senderAlert, args) => { });
            Dialog dialogo = alerta.Create();
            dialogo.Show();
        }

        /// <summary>
        /// Cria um Dialog para solicitar ao usuário que confirme ou cancele a edição
        /// </summary>
        /// <param name="codigoVacina">O código da vacina.</param>
        private void CriaAlertaEditar(string codigo)
        {
            AlertDialog.Builder alerta = new AlertDialog.Builder(this);
            alerta.SetTitle("Edição " + txv_ac_generica_nomeTela.Text);
            alerta.SetMessage("Deseja realmente editar " + (TELA == Tela.VACINAS || TELA == Tela.ENFERMIDADES ? "a " : "o ") + txv_ac_generica_nomeTela.Text.Remove(txv_ac_generica_nomeTela.Text.Length - 1, 1) + "?");
            alerta.SetPositiveButton("Sim", (senderAlert, args) =>
            {
                AbreNovaTelaCadastro(Tarefa.EDITANDO, codigo);
            });
            alerta.SetNegativeButton("Não", (senderAlert, args) => { });
            Dialog dialogo = alerta.Create();
            dialogo.Show();
        }

        /// <summary>
        /// Cria um Dialog para solicitar ao usuário que confirme ou cancele a edição
        /// </summary>
        /// <param name="codigoVacina">O código da vacina.</param>
        private void CriaAlertaEditarExcluir(string codigo)
        {
            string mensagem = "Item: ";
            switch (TELA)
            {
                case Tela.ENFERMIDADES:
                    mensagem = "Enfermidade " + (new MD_Enfermidade(int.Parse(codigo))).Nome;
                    break;
                case Tela.EXAMES:
                    mensagem = "Exame " + (new MD_Exame(int.Parse(codigo))).Nome;
                    break;
                case Tela.MEDICAMENTOS:
                    mensagem = "Medicamento " + (new MD_Medicamento(int.Parse(codigo))).Nome;
                    break;
                case Tela.TRATAMENTOS:
                    mensagem = "Tratamento " + (new MD_Tratamento(int.Parse(codigo))).Nome;
                    break;
                case Tela.VACINAS:
                    mensagem = "Vacina " + (new MD_Vacina(codigo)).Nome;
                    break;
            }

            AlertDialog.Builder alerta = new AlertDialog.Builder(this);
            alerta.SetTitle("Selecione");
            alerta.SetMessage(mensagem);
            alerta.SetPositiveButton("Editar", (senderAlert, args) =>
            {
                CriaAlertaEditar(codigo);
            });
            alerta.SetNegativeButton("Excluir", (senderAlert, args) => 
            {
                CriaAlertaExcluir(codigo);
            });
            Dialog dialogo = alerta.Create();
            dialogo.Show();
        }

        /// <summary>
        /// Método que abre as opções
        /// </summary>
        /// <param name="id">Id do item selecionado</param>
        public void AbreOpcoes(string id)
        {
            string codigo = "-1";
            if (!string.IsNullOrEmpty(id))
            {
                int i = 0;
                if (TELA == Tela.TRATAMENTOS)
                {
                    List<MD_Tratamento> tratamentos = MD_Tratamento.ListaTratamento();
                    
                    foreach (MD_Tratamento tart in tratamentos)
                    {
                        if (i == int.Parse(id))
                        {
                            codigo = tart.Codigo.ToString();
                            break;
                        }
                        i++;
                    }
                }
                else if (TELA == Tela.ENFERMIDADES)
                {
                    List<MD_Enfermidade> enfermidades = MD_Enfermidade.ListaEnfermidades();

                    foreach (MD_Enfermidade enf in enfermidades)
                    {
                        if (i == int.Parse(id))
                        {
                            codigo = enf.Codigo.ToString();
                            break;
                        }
                        i++;
                    }
                }
                else if (TELA == Tela.EXAMES)
                {
                    List<MD_Exame> exames = MD_Exame.ListaExame();

                    foreach (MD_Exame exa in exames)
                    {
                        if (i == int.Parse(id))
                        {
                            codigo = exa.Codigo.ToString();
                            break;
                        }
                        i++;
                    }
                }
                else if (TELA == Tela.MEDICAMENTOS)
                {
                    List<MD_Medicamento> medicamentos= MD_Medicamento.ListaMedicamento();

                    foreach (MD_Medicamento med in medicamentos)
                    {
                        if (i == int.Parse(id))
                        {
                            codigo = med.Codigo.ToString();
                            break;
                        }
                        i++;
                    }
                }
                else if (TELA == Tela.VACINAS)
                {
                    List<MD_Vacina> vacinas = MD_Vacina.ListaVacinas();

                    foreach (MD_Vacina vac in vacinas)
                    {
                        if (i == int.Parse(id))
                        {
                            codigo = vac.Nome.ToString();
                            break;
                        }
                        i++;
                    }
                }
                CriaAlertaEditarExcluir(codigo);
            }
        }

        #endregion Métodos
    }
}