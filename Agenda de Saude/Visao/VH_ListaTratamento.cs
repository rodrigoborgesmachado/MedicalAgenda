namespace Agenda_de_Saude.Visao
{
    using Android.Widget;

    /// <summary>
    /// A View Holder da lista de tratamento.
    /// </summary>
    class VH_ListaTratamento : Java.Lang.Object
    {
        public TextView Codigo { get; set; }
        public TextView Nome { get; set; }
        public TextView Finalizado { get; set; }
        public TextView Data_Inicio { get; set; }
        public TextView Data_Fim { get; set; }
        public TextView Periodicidade { get; set; }
        public TextView Comentario { get; set; }
    }
}
