using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using static Agenda_de_Saude.Modelo.Enumerator;

namespace Agenda_de_Saude.Visao
{
    [Activity(Label = "AC_Menu")]
    public class AC_Menu : Activity
    {
        #region Constantes

        /// <summary>
        /// Constante referente ao cadastro de cliente
        /// </summary>
        const int EDITAR_CLIENTE = 0;

        const int LISTA_GENERICA = 1;

        #endregion Constantes

        #region Componentes Visuais

        /// <summary>
        /// TextView referente ao nome do Usuário
        /// </summary>
        TextView txv_ac_menu_nomeUsu;

        /// <summary>
        /// Button referente à opção de enfermidades
        /// </summary>
        Button btn_ac_menu_enfermidades;

        /// <summary>
        /// Button referente à opção de exames
        /// </summary>
        Button btn_ac_menu_exames;

        /// <summary>
        /// Button referente à opção de medicamentos
        /// </summary>
        Button btn_ac_menu_medicamentos;

        /// <summary>
        /// Button referente à opção de tratamentos
        /// </summary>
        Button btn_ac_menu_tratamentos;

        /// <summary>
        /// Button referente à opção de vacinas
        /// </summary>
        Button btn_ac_menu_vacinas;

        /// <summary>
        /// FloatingActionButton referente à opção de ~edição do usuário
        /// </summary>
        FloatingActionButton fla_ac_menu_fab;

        #endregion Componentes Visuais

        #region Eventos

        /// <summary>
        /// Evento lançado quando a tela é chamada
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InicializeActivity();
        }

        /// <summary>
        /// Event taken when there is a result from the activity
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if(resultCode == Result.Ok)
            {
                if(requestCode == EDITAR_CLIENTE)
                {
                    this.txv_ac_menu_nomeUsu.Text = Util.Global.Usuario_Logado.Nome_usuario;
                    Util.Global.Alerta_Class.ApresentaToast("Usuário alterado", this);
                }
                else if(requestCode == LISTA_GENERICA)
                {

                }
            }
        }

        /// <summary>
        /// Event taken when the fab icon is taken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            /*
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show*/
            AC_CadastroUsuario ac = new AC_CadastroUsuario();
            Intent intent = new Intent(this, ac.GetType());
            intent.PutExtra("CPF_USUARIO", Util.Global.Usuario_Logado.CPF_Usuario);
            StartActivityForResult(intent, EDITAR_CLIENTE);
        }

        /// <summary>
        /// Evento lançado no clique do botão de enfermidades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ac_menu_enfermidades_Click(object sender, EventArgs e)
        {
            AbrirTelaGenerica(Tela.ENFERMIDADES);
        }

        /// <summary>
        /// Evento lançado no clique do botão de exames
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ac_menu_exames_Click(object sender, EventArgs e)
        {
            AbrirTelaGenerica(Tela.EXAMES);
        }

        /// <summary>
        /// Evento lançado no clique do botão de medicamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ac_menu_medicamentos_Click(object sender, EventArgs e)
        {
            AbrirTelaGenerica(Tela.MEDICAMENTOS);
        }

        /// <summary>
        /// Evento lançado no clique do botão de tratamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ac_menu_tratamentos_Click(object sender, EventArgs e)
        {
            AbrirTelaGenerica(Tela.TRATAMENTOS);
        }

        /// <summary>
        /// Evento lançado no clique do botão de vacinas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ac_menu_vacinas_Click(object sender, EventArgs e)
        {
            AbrirTelaGenerica(Tela.VACINAS);
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Método que faz a inicialização da Activity
        /// </summary>
        public void InicializeActivity()
        {
            SetContentView(Resource.Layout.LA_AC_Menu);
            CarregaComponentesVisuais();
            
            txv_ac_menu_nomeUsu.Text = Util.Global.Usuario_Logado.Nome_usuario;
        }

        /// <summary>
        /// Método que abre uma nova tela genérica
        /// </summary>
        /// <param name="tela">Tela a ser aberta</param>
        private void AbrirTelaGenerica(Tela tela)
        {
            AC_Generica v = new AC_Generica();
            Intent intent = new Intent(this, v.GetType());
            intent.PutExtra("TELA", (int)tela);
            StartActivityForResult(intent, LISTA_GENERICA);
        }

        /// <summary>
        /// Método que carrega os componentes visuais da tela
        /// </summary>
        public void CarregaComponentesVisuais()
        {
            txv_ac_menu_nomeUsu = FindViewById<TextView>(Resource.Id.txv_ac_menu_nomeUsu);
            btn_ac_menu_enfermidades = FindViewById<Button>(Resource.Id.btn_ac_menu_enfermidades);
            btn_ac_menu_exames = FindViewById<Button>(Resource.Id.btn_ac_menu_exames);
            btn_ac_menu_medicamentos = FindViewById<Button>(Resource.Id.btn_ac_menu_medicamentos);
            btn_ac_menu_tratamentos = FindViewById<Button>(Resource.Id.btn_ac_menu_tratamentos);
            btn_ac_menu_vacinas = FindViewById<Button>(Resource.Id.btn_ac_menu_vacinas);
            fla_ac_menu_fab = FindViewById<FloatingActionButton>(Resource.Id.fla_ac_menu_fab);

            fla_ac_menu_fab.Click += FabOnClick;
            btn_ac_menu_enfermidades.Click += Btn_ac_menu_enfermidades_Click;
            btn_ac_menu_exames.Click += Btn_ac_menu_exames_Click;
            btn_ac_menu_medicamentos.Click += Btn_ac_menu_medicamentos_Click;
            btn_ac_menu_tratamentos.Click += Btn_ac_menu_tratamentos_Click;
            btn_ac_menu_vacinas.Click += Btn_ac_menu_vacinas_Click;
        }

        #endregion Métodos
    }
}