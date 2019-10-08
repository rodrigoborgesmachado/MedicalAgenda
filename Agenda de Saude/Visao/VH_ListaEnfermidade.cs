namespace Agenda_de_Saude.Visao
{
    using Android.Widget;

    /// <summary>
    /// A View Holder da lista enfermidade.
    /// </summary>
    class VH_ListaEnfermidade : Java.Lang.Object
    {
        public TextView Codigo { get; set; }
        public TextView Nome { get; set; }
        public TextView Comentario { get; set; }
        public TextView Situacao { get; set; }
        public TextView Tipo { get; set; }
    }
}