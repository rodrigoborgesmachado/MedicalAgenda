using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Agenda_de_Saude.Util
{
    public class Alerta
    {
        #region Métodos

        public delegate void MeuDelegate();

        /// <summary>
        /// Método que apresenta um alerta 
        /// </summary>
        /// <param name="mensagem">Mensagem a ser apresentada</param>
        public void ApresentaAlerta(string titulo, string mensagem, string texto_botao, Context context, MeuDelegate fun)
        {
            //define o alerta para executar a tarefa
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(context);
            Android.App.AlertDialog alerta = builder.Create();
            //Define o Titulo
            alerta.SetTitle(titulo);
            alerta.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            alerta.SetMessage(mensagem);
            alerta.SetButton(texto_botao, (s, ev) =>
            {
                fun();
                //Toast.MakeText(context, "Legal, vamos continuar... !", ToastLength.Short).Show();
            });
            alerta.Show();
        }

        /// <summary>
        /// Método que apresenta um alerta 
        /// </summary>
        /// <param name="mensagem">Mensagem a ser apresentada</param>
        public void ApresentaAlerta(string mensagem, Context context)
        {
            ApresentaAlerta("Alerta", mensagem, "OK", context, delegate { });
        }

        /// <summary>
        /// Método que apresenta um toast
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="context"></param>
        public void ApresentaToast(string mensagem, Context context)
        {
            Toast.MakeText(context, mensagem, ToastLength.Short).Show();
        }

        /// <summary>
        /// Método que apresenta um toast
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="context"></param>
        public void ApresentaToast(string mensagem, ToastLength tempo, Context context)
        {
            Toast.MakeText(context, mensagem, tempo).Show();
        }

        #endregion Métodos
    }
}