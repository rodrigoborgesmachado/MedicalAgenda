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
    /// <summary>
    /// Classe Adapter para instanciar a Lista de Enfermidade.
    /// </summary>
    class Ad_ListaEnfermidade : BaseAdapter<MD_Enfermidade>
    {
        /// <summary>
        /// A lista de vacinas à ser exibida.
        /// </summary>
        private List<MD_Enfermidade> enfermidades;

        TextView txv_ltv_ac_enfermidade_codigo_nome;
        TextView txv_ltv_ac_enfermidade_comentario;
        TextView txv_ltv_ac_enfermidade_situacao;
        TextView txv_ltv_ac_enfermidade_tipo;

        private AC_Generica acGenerica;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="enfermidade">Lista de Vacinas.</param>
        public Ad_ListaEnfermidade(List<MD_Enfermidade> enfermidade, AC_Generica acGenerica)
        {
            this.enfermidades = enfermidade;
            this.acGenerica = acGenerica;
        }

        /// <summary>
        /// Recupera a posição do elemento na lista.
        /// </summary>
        /// <param name="position">A posição</param>
        /// <returns>Um objeto tipo MD_Vacina.</returns>
        public override MD_Enfermidade this[int position]
        {
            get
            {
                return enfermidades[position];
            }
        }

        /// <summary>
        /// Conta o número de elementos que a lista possui.
        /// </summary>
        public override int Count
        {
            get
            {
                return enfermidades.Count;
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
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LTV_AC_Enfermidades, parent, false);


                txv_ltv_ac_enfermidade_codigo_nome = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_enfermidade_codigo_nome);
                txv_ltv_ac_enfermidade_comentario = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_enfermidade_comentario);
                txv_ltv_ac_enfermidade_situacao = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_enfermidade_situacao);
                txv_ltv_ac_enfermidade_tipo = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_enfermidade_tipo);
                
                view.Tag = new VH_ListaEnfermidade() {Nome = txv_ltv_ac_enfermidade_codigo_nome, Comentario = txv_ltv_ac_enfermidade_comentario, Situacao = txv_ltv_ac_enfermidade_situacao, Tipo = txv_ltv_ac_enfermidade_tipo };
            }

            var holder = (VH_ListaEnfermidade)view.Tag;

            holder.Nome.Text = enfermidades[position].Codigo.ToString() + "- " + enfermidades[position].Nome;
            holder.Comentario.Text = "Comentário: " + enfermidades[position].Comentario;
            holder.Situacao.Text = "Situação: " + enfermidades[position].Situacao;
            holder.Tipo.Text = "Tipo:" + enfermidades[position].Tipo;

            return view;
        }

        /// <summary>
        /// Método para controlar se a enfermidade será inserida ou excluída.
        /// </summary>
        /// <param name="codigoVacina">O código da enfermidade</param>
        private void EditaVacina(string codigoEnfermidade)
        {
            AC_CadastroEnfermidade cadastroVacina = new AC_CadastroEnfermidade();
            Bundle extras = new Bundle();
            extras.PutString("CODIGO_ENFERMIDADE", codigoEnfermidade);

            Intent intent = new Intent(acGenerica, cadastroVacina.GetType());
            intent.PutExtras(extras);
            acGenerica.StartActivityForResult(intent, (int)Tarefa.EDITANDO);
        }
    }
}