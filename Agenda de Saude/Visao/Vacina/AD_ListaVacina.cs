namespace Agenda_de_Saude.Visao
{
    using System;
    using System.Collections.Generic;
    using Agenda_de_Saude.Modelo;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Views;
    using Android.Widget;
    using static Agenda_de_Saude.Modelo.Enumerator;

    /// <summary>
    /// Classe Adapter para instanciar a Lista de Vacinas.
    /// </summary>
    public class AD_ListaVacina : BaseAdapter<MD_Vacina>
    {
        /// <summary>
        /// A lista de vacinas à ser exibida.
        /// </summary>
        private List<MD_Vacina> vacinas;

        TextView txv_ltv_nome;
        TextView txv_ltv_data_aplicacao;
        TextView txv_ltv_dose;

        //private Button btnListaVacinaEditar;

        //private Button btnListaVacinaExcluir;

        private AC_Generica acGenerica;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="vacinas">Lista de Vacinas.</param>
        public AD_ListaVacina(List<MD_Vacina> vacinas, AC_Generica acGenerica)
        {
            this.vacinas = vacinas;
            this.acGenerica = acGenerica;
        }

        /// <summary>
        /// Recupera a posição do elemento na lista.
        /// </summary>
        /// <param name="position">A posição</param>
        /// <returns>Um objeto tipo MD_Vacina.</returns>
        public override MD_Vacina this[int position]
        {
            get
            {
                return vacinas[position];
            }
        }

        /// <summary>
        /// Conta o número de elementos que a lista possui.
        /// </summary>
        public override int Count
        {
            get
            {
                return vacinas.Count;
            }
        }

        /// <summary>
        /// Recupera o Id do item.
        /// </summary>
        /// <param name="position">A posição do item na lista.</param>
        /// <returns>A posição do item na lista.</returns>
        public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        /// Infla o layout da lista de vacinas na classe pai.
        /// </summary>
        /// <param name="position">A posição.</param>
        /// <param name="convertView">A view.</param>
        /// <param name="parent">O componente pai.</param>
        /// <returns>Retorna o layout à ser inflado.</returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LA_LTV_CadastroVacina, parent, false);

                
                txv_ltv_nome = view.FindViewById<TextView>(Resource.Id.txv_ltv_nome);
                txv_ltv_data_aplicacao = view.FindViewById<TextView>(Resource.Id.txv_ltv_data_aplicacao);
                txv_ltv_dose = view.FindViewById<TextView>(Resource.Id.txv_ltv_dose);
                //btnListaVacinaEditar = view.FindViewById<Button>(Resource.Id.btn_cadastro_vacina_editar);
                //btnListaVacinaExcluir = view.FindViewById<Button>(Resource.Id.btn_cadastro_vacina_excluir);

                view.Tag = new VH_ListaVacina() { Nome = txv_ltv_nome, Data = txv_ltv_data_aplicacao, Dose = txv_ltv_dose };
            }

            var holder = (VH_ListaVacina)view.Tag;

            holder.Nome.Text = vacinas[position].Nome;
            holder.Data.Text = "Data: " + vacinas[position].Data_aplicacao.ToString();
            holder.Dose.Text = "Dose: " + vacinas[position].Dose.ToString();
            //btnListaVacinaEditar.Click += delegate { this.EditaVacina(vacinas[position].Nome); };
            //btnListaVacinaExcluir.Click += delegate { this.ExcluirVacina(vacinas[position].Nome); };

            return view;

        }

        /// <summary>
        /// Método para controlar se a vacina será inserida ou excluída.
        /// </summary>
        /// <param name="codigoVacina">O código da vacina</param>
        private void EditaVacina(string codigoVacina)
        {
            AC_CadastroVacina cadastroVacina = new AC_CadastroVacina();
            Bundle extras = new Bundle();
            extras.PutInt("ACAO", (int)Tarefa.EDITANDO);
            extras.PutString("CODIGO", codigoVacina);            

            // Cria um Intent
            Intent intent = new Intent(acGenerica, cadastroVacina.GetType());
            intent.PutExtras(extras);
            acGenerica.StartActivityForResult(intent, (int)Tarefa.EDITANDO);
        }

        /// <summary>
        /// Abre um dialog para confirmar a exclusão da vacina.
        /// </summary>
        /// <param name="codigoVacina">O código da vacina à ser excluído.</param>
        private void ExcluirVacina(string codigoVacina)
        {
            AC_CadastroVacina cadastroVacina = new AC_CadastroVacina();
            Bundle extras = new Bundle();
            extras.PutInt("ACAO", (int)Tarefa.EXCLUINDO);
            extras.PutString("CODIGO", codigoVacina);

            // Cria um Intent
            Intent intent = new Intent(acGenerica, cadastroVacina.GetType());
            intent.PutExtras(extras);
            acGenerica.StartActivityForResult(intent, (int)Tarefa.EXCLUINDO);
        }
    }
}