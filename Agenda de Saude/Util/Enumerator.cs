using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_de_Saude.Modelo
{
    public class Enumerator
    {

        /// <summary>
        /// Enum referente à tela a ser aberta
        /// </summary>
        public enum Tela
        {
            ENFERMIDADES = 0,
            EXAMES = 1,
            MEDICAMENTOS = 2,
            TRATAMENTOS = 3,
            VACINAS = 4
        }

        /// <summary>
        /// Tarefa sendo executada na tela
        /// </summary>
        public enum Tarefa
        {
            INCLUINDO = 0,
            EDITANDO = 1,
            EXCLUINDO = 2
        }

        /// <summary>
        /// Enumerator for type of data
        /// </summary>
        public enum DataType
        {
            DATE = 1,
            INT = 2,
            STRING = 3,
            CHAR = 4,
            DECIMAL
        }
    }
}
