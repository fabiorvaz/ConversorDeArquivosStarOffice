using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olvebra.ConversorArquivosApp.pesquisa.filtros
{
    public class FiltroParser
    {
        public enum eEstado
        {
            eNenhum,
            eIndentificador,
        }

        protected string m_InputBuffer;
        protected int m_InputPos;
        protected eEstado m_state;
        protected StringBuilder m_token;

        public Filtro Parse(string entrada)
        {
            return null;
        }

        protected char GetChar()
        {
            return m_InputBuffer[m_InputPos++];
        }


        protected void ParseString(string entrada)
        {
            while (true)
            {
                char c = GetChar();
                if (m_state == eEstado.eIndentificador)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        m_token.Append(c);
                        continue;
                    }
                }
            }
        }
    }
}
