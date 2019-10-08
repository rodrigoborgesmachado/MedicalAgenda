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
    class Ad_ListaTratamentos : BaseAdapter<MD_Tratamento>
    {
        /// <summary>
        /// A lista do tratamento a ser exibido.
        /// </summary>
        private List<MD_Tratamento> tratamento;

        TextView txv_ltv_ac_tratamento_codigo_nome;
        TextView txv_ltv_ac_tratamento_data_inicio;
        TextView txv_ltv_ac_tratamento_data_fim;
        TextView txv_ltv_ac_tratamento_finalizado;
        TextView txv_ltv_ac_tratamento_periodicidade;
        
        private AC_Generica acGenerica;

        MD_Tratamento trat = null;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="enfermidade">Lista de Vacinas.</param>
        public Ad_ListaTratamentos(List<MD_Tratamento> tratamento, AC_Generica acGenerica)
        {
            this.tratamento = tratamento;
            this.acGenerica = acGenerica;
        }

        /// <summary>
        /// Recupera a posição do elemento na lista.
        /// </summary>
        /// <param name="position">A posição</param>
        /// <returns>Um objeto tipo MD_Vacina.</returns>
        public override MD_Tratamento this[int position]
        {
            get
            {
                return tratamento[position];
            }
        }

        /// <summary>
        /// Conta o número de elementos que a lista possui.
        /// </summary>
        public override int Count
        {
            get
            {
                return tratamento.Count;
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
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LTV_AC_Tratamento, parent, false);


                txv_ltv_ac_tratamento_codigo_nome = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_tratamento_codigo_nome);
                txv_ltv_ac_tratamento_data_inicio = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_tratamento_data_inicio);
                txv_ltv_ac_tratamento_data_fim = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_tratamento_data_fim);
                txv_ltv_ac_tratamento_finalizado = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_tratamento_finalizado);
                txv_ltv_ac_tratamento_periodicidade = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_tratamento_periodicidade);
                
                view.Tag = new VH_ListaTratamento() { Nome = txv_ltv_ac_tratamento_codigo_nome, Data_Inicio = txv_ltv_ac_tratamento_data_inicio, Data_Fim = txv_ltv_ac_tratamento_data_fim, Finalizado = txv_ltv_ac_tratamento_finalizado, Periodicidade = txv_ltv_ac_tratamento_periodicidade};
            }

            var holder = (VH_ListaTratamento)view.Tag;
            holder.Nome.Text = tratamento[position].Codigo.ToString() + "- " + tratamento[position].Nome;
            holder.Data_Inicio.Text = "Início: " + tratamento[position].Data_Inicio.ToShortDateString();
            holder.Data_Fim.Text = "Fim: " + tratamento[position].Data_Fim.ToShortDateString();
            holder.Finalizado.Text = tratamento[position].Finalizado == "1" ? "Finalizado" : "Em andamento";
            holder.Periodicidade.Text = tratamento[position].Periodicidade + " em " + tratamento[position].Periodicidade + "dias";

            return view;
        }
    }
}