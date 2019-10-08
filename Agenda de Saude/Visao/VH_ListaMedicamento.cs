namespace Agenda_de_Saude.Visao
{
    using Android.Widget;

    /// <summary>
    /// A View Holder da lista vacina.
    /// </summary>
    class VH_ListaMedicamento : Java.Lang.Object
    {
        public TextView Codigo { get; set; }
        public TextView Nome { get; set; }
        public TextView Medida_tempo { get; set; }
        public TextView Formato { get; set; }
        public TextView Quantidade { get; set; }
        public TextView Medida_Peso { get; set; }
        public TextView Periodicidade { get; set; }
    }
}