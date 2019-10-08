namespace Agenda_de_Saude.Visao
{
    using Android.Widget;

    /// <summary>
    /// A View Holder da lista exame.
    /// </summary>
    class VH_ListaExame : Java.Lang.Object
    {
        public TextView Codigo { get; set; }
        public TextView Nome { get; set; }
        public TextView Comentario { get; set; }
        public TextView Caminho_Arquivo { get; set; }
    }
}