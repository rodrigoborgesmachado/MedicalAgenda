using System;
using Agenda_de_Saude.Modelo;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Agenda_de_Saude.Visao
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class AC_Login : AppCompatActivity
    {
        #region Constantes

        /// <summary>
        /// Constante referente ao cadastro de cliente
        /// </summary>
        const int INCLUIR_CLIENTE = 0;

        #endregion Constantes

        #region Componentes Visuais

        /// <summary>
        /// Edit text from login
        /// </summary>
        EditText edt_ac_login_login;

        /// <summary>
        /// Edit text from password
        /// </summary>
        EditText edt_ac_login_pass;

        /// <summary>
        /// Button from login
        /// </summary>
        Button btn_ac_login_logar;

        /// <summary>
        /// FabIcon from butoon to add user
        /// </summary>
        FloatingActionButton fab;

        bool lockchange = false;

        #endregion Componentes Visuais

        #region Eventos

        /// <summary>
        /// Event taken when the screen is created
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Util.Global.InicializeSystem();

            base.OnCreate(savedInstanceState);
            InicializeActivity();
        }

        /// <summary>
        /// Event taken when the screen is closed. When the app will be closed allways gonna be on this screen, so need to be close the connection with the database.
        /// </summary>
        protected override void OnDestroy()
        {
            Util.Global.FinalizeSystem();
            base.OnDestroy();
        }

        /// <summary>
        /// Event taken when a option from the menu is selected
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
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
            StartActivityForResult(intent, INCLUIR_CLIENTE);
        }

        /// <summary>
        /// Evento lançado no clique do botão logar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ac_login_logar_Click(object sender, EventArgs e)
        {
            Logar();
        }

        /// <summary>
        /// Evento lançado quando o texto do login alterar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edt_ac_login_login_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if (lockchange) return;

            lockchange = true;

            if ((edt_ac_login_login.Text.ToString().Contains('.') && !(edt_ac_login_login.Text.ToString().Contains('@'))) || edt_ac_login_login.Text.ToString().Contains('-'))
            {
                edt_ac_login_login.Text = edt_ac_login_login.Text.ToString().Substring(0, edt_ac_login_login.Text.ToString().Length - 1);
            }

            lockchange = false;
        }

        /// <summary>
        /// Evento acionado no retorno de alguma activity
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            edt_ac_login_login.Text = edt_ac_login_pass.Text = "";
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Method that inicialize the activity
        /// </summary>
        public void InicializeActivity()
        {
            SetContentView(Resource.Layout.AC_Login);

            CarregaComponentesVisuais();
        }

        /// <summary>
        /// Method that creates all visual components
        /// </summary>
        public void CarregaComponentesVisuais()
        { 
            fab = FindViewById<FloatingActionButton>(Resource.Id.fla_ac_login_fab);
            edt_ac_login_login = FindViewById<EditText>(Resource.Id.edt_ac_login_login);
            edt_ac_login_pass = FindViewById<EditText>(Resource.Id.edt_ac_login_pass);
            btn_ac_login_logar = FindViewById<Button>(Resource.Id.btn_ac_login_logar);

            btn_ac_login_logar.Click += Btn_ac_login_logar_Click;
            edt_ac_login_login.AfterTextChanged += Edt_ac_login_login_AfterTextChanged;
            fab.Click += FabOnClick;
        }

        /// <summary>
        /// Método que valida se os campos estão preenchidos corretaamente
        /// </summary>
        /// <param name="mensagem">Mensagem para retornar o problema, se ocorrer</param>
        /// <returns>True - Campos corretos; False- Campos com problema</returns>
        public bool ValidaCampos(ref string mensagem)
        {
            bool retorno = true;
            mensagem = "";
            if (string.IsNullOrEmpty(edt_ac_login_login.Text))
            {
                mensagem = "Campo \"Login\" vazio!";
                retorno = false;
            }
            else if (string.IsNullOrEmpty(edt_ac_login_pass.Text))
            {
                mensagem = "Campo \"Password\" vazio!";
                retorno = false;
            }
            return retorno;
        }

        /// <summary>
        /// Método que faz o login do sistema
        /// </summary>
        public void Logar()
        {
            string mensagem = "";
            if(!ValidaCampos(ref mensagem))
            {
                return;
            }

            string login = edt_ac_login_login.Text;
            string pass = edt_ac_login_pass.Text;

            // Just to create the table if is the first login and don't exists a user in the system.
            MD_Usuario user = new MD_Usuario(string.Empty);

            if (Util.Global.Logar(login, pass))
            {
                AC_Menu ac = new AC_Menu();
                Intent intent = new Intent(this, ac.GetType());
                StartActivityForResult(intent, INCLUIR_CLIENTE);
                Util.Global.Alerta_Class.ApresentaToast("Bem vindo!", this);
            }
            else
            {
                if (user.ExisteLogin(login))
                {
                    edt_ac_login_pass.Text = "";
                    Util.Global.Alerta_Class.ApresentaToast("Usuário ou senha incorretos!", ToastLength.Long, this);
                }
                else
                {
                    edt_ac_login_pass.Text = "";
                    Util.Global.Alerta_Class.ApresentaToast("Usuário não cadastrado no sistema!", ToastLength.Long, this);
                }
                
            }
        }

        #endregion Métodos
    }
}

