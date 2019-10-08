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
    class Ad_ListaExames : BaseAdapter<MD_Exame>
    {
        /// <summary>
        /// A lista de exames à ser exibida.
        /// </summary>
        private List<MD_Exame> exame;

        TextView txv_ltv_ac_exames_codigo_nome;
        TextView txv_ltv_ac_exames_comentario;
        TextView txv_ltv_ac_exames_arquivo;
        
        private AC_Generica acGenerica;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="enfermidade">Lista de Vacinas.</param>
        public Ad_ListaExames(List<MD_Exame> exame, AC_Generica acGenerica)
        {
            this.exame = exame;
            this.acGenerica = acGenerica;
        }

        /// <summary>
        /// Recupera a posição do elemento na lista.
        /// </summary>
        /// <param name="position">A posição</param>
        /// <returns>Um objeto tipo MD_Vacina.</returns>
        public override MD_Exame this[int position]
        {
            get
            {
                return exame[position];
            }
        }

        /// <summary>
        /// Conta o número de elementos que a lista possui.
        /// </summary>
        public override int Count
        {
            get
            {
                return exame.Count;
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
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LTV_AC_Exames, parent, false);


                txv_ltv_ac_exames_codigo_nome = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_exames_codigo_nome);
                txv_ltv_ac_exames_comentario = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_exames_comentario);
                txv_ltv_ac_exames_arquivo = view.FindViewById<TextView>(Resource.Id.txv_ltv_ac_exames_arquivo);
                view.Tag = new VH_ListaExame() { Nome = txv_ltv_ac_exames_codigo_nome, Comentario = txv_ltv_ac_exames_comentario, Caminho_Arquivo = txv_ltv_ac_exames_comentario };
            }

            var holder = (VH_ListaExame)view.Tag;

            holder.Nome.Text = exame[position].Codigo.ToString() + "- " + exame[position].Nome;
            holder.Comentario.Text = "Comentário: " + exame[position].Comentario;
            holder.Caminho_Arquivo.Text = "Arquivo: " + exame[position].Caminho_Arquivo;

            return view;
        }

        /// <summary>
        /// Método para controlar se o código será inserido ou excluído.
        /// </summary>
        /// <param name="codigoExame">O código do exame</param>
        private void EditaVacina(string codigoExame)
        {
            AC_CadastroExame cadastroVacina = new AC_CadastroExame();
            Bundle extras = new Bundle();
            extras.PutString("CODIGO_EXAME", codigoExame);

            // Cria um Intent
            Intent intent = new Intent(acGenerica, cadastroVacina.GetType());
            intent.PutExtras(extras);
            acGenerica.StartActivityForResult(intent, (int)Tarefa.EDITANDO);
        }
    }
}