namespace Agenda_de_Saude.Visao
{
    using Android.Widget;

    /// <summary>
    /// A View Holder da lista vacina.
    /// </summary>
    public class VH_ListaVacina : Java.Lang.Object
    {
        public TextView Nome { get; set; }
        public TextView Data { get; set; }
        public TextView Dose { get; set; }
    }
}