using System;
using System.Collections.Generic;
using Agenda_de_Saude.Modelo;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using static Agenda_de_Saude.Modelo.Enumerator;

namespace Agenda_de_Saude.Visao
{
    class Ad_Lista_Medicamento : BaseAdapter<MD_Medicamento>
    {
        /// <summary>
        /// A lista de medicamentos à ser exibidos.
        /// </summary>
        private List<MD_Medicamento> medicamentos;

        TextView txv_ltv_ac_medicamentos_codigo_nome;
        TextView txv_ltv_ac_medicamentos_formato;
        TextView txv_ltv_ac_medicamentos_quantidade_periodicidade_medida_tempo;
        TextView txv_ltv_ac_medicamentos_medida_peso;

        private AC_Generica acGenerica;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="medicamento">Lista de Medicamentos.</param>
        public Ad_Lista_Medicamento(List<MD_Medicamento> medicamento, AC_Generica acGenerica)
        {
            this.medicamentos = medicamento;
            this.acGenerica = acGenerica;
        }

        /// <summary>
        /// Recupera a posição do elemento na lista.
        /// </summary>
        /// <param name="position">A posição</param>
        /// <returns>Um objeto tipo MD_Medicamento.</returns>
        public override MD_Medicamento this[int position]
        {
            get
            {
                return medicamentos[position];
            }
        }

        /// <summary>
        /// Conta o número de elementos que a lista possui.
        /// </summary>
        public override int Count
        {
            get
            {
                return medicamentos.Count;
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
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LTV_AC_Medicamento, parent, false);

                txv_ltv_ac_medicamentos_codigo_nome = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_medicamentos_codigo_nome);
                txv_ltv_ac_medicamentos_formato = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_medicamentos_formato);
                txv_ltv_ac_medicamentos_quantidade_periodicidade_medida_tempo = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_medicamentos_quantidade_periodicidade_medida_tempo);
                txv_ltv_ac_medicamentos_medida_peso = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_medicamentos_medida_peso);
                
                view.Tag = new VH_ListaMedicamento() { Nome = txv_ltv_ac_medicamentos_codigo_nome, Formato = txv_ltv_ac_medicamentos_formato, Quantidade = txv_ltv_ac_medicamentos_quantidade_periodicidade_medida_tempo, Medida_Peso = txv_ltv_ac_medicamentos_medida_peso };
            }

            var holder = (VH_ListaMedicamento)view.Tag;

            holder.Nome.Text = medicamentos[position].Codigo + "- " + medicamentos[position].Nome;
            holder.Formato.Text = "Formato: " + medicamentos[position].Formato.ToString();
            holder.Quantidade.Text = medicamentos[position].Quantidade.ToString() + " de " + medicamentos[position].Periodicidade.ToString() + " em " + medicamentos[position].Periodicidade.ToString() + " " + medicamentos[position].Medida_Tempo;
            holder.Medida_Peso.Text = medicamentos[position].Medida_Peso.ToString();

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