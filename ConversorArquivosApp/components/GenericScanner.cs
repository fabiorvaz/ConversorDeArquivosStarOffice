using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Olvebra.ConversorArquivosApp.components
{
    [Flags]
    public enum eScannerStateModifiers
    {
        eNenhum = 0,
        e01ReturnToken = (1 << 30),
        e02ClearToken = (1 << 29),
        e03InsertToken = (1 << 28),
        e04ReturnToken = (1 << 27),
        e05ClearToken = (1 << 26),
        e06InsertToken = (1 << 25),
        e07ReturnToken = (1 << 24),
        e08ClearToken = (1 << 23),
        e09InsertToken = (1 << 22),
        e10PushBack = (1 << 21),
    }

    public class StreamScanner
    {
        public class ASCIITableState
        {
            public const int MAX_INPUTS = 256;

            public int State;

            protected int[] Vetor;
            protected int[] Id;

            public ASCIITableState()
            {
                Vetor = new int[MAX_INPUTS];
                Id = new int[MAX_INPUTS];

                for (int p = 0; p < MAX_INPUTS; p++)
                {
                    Vetor[p] = StreamScanner.INVALID_STATE;
                    Id[p] = StreamScanner.NO_ID;
                }
            }

            public void SetState(int toState, int input, eScannerStateModifiers modifiers, int returnId)
            {
                int dest = toState | (int)modifiers;
                Vetor[input] = dest;
                Id[input] = returnId;
            }

            public void SetState(int toState, eScannerStateModifiers modifiers, int returnId)
            {
                int dest = toState | (int)modifiers;
                for (int p = 0; p < MAX_INPUTS; p++)
                {
                    Vetor[p] = dest;
                    Id[p] = returnId;
                }
            }

            public void SetState(int toState, string chars, eScannerStateModifiers modifiers, int returnId)
            {
                int dest = toState | (int)modifiers;
                int lastChar = 0;
                for (int pos = 0; pos < chars.Length; pos++)
                {
                    int c = (int)chars[pos];
                    if ((c == '-') && (pos > 0) && (pos + 1 < chars.Length))
                    {
                        for (int range = lastChar; range < (int)chars[pos + 1]; range++)
                        {
                            Vetor[range] = dest;
                            Id[range] = returnId;
                        }
                    }
                    else
                    {
                        Vetor[c] = dest;
                        Id[c] = returnId;
                    }
                    lastChar = c;
                }
            }

            public int GetTarget(int input)
            {
                if (input < 0 || input >= MAX_INPUTS)
                    return StreamScanner.INVALID_INPUT;
                return Vetor[input];
            }

            public int GetId(int input)
            {
                if (input < 0 || input >= MAX_INPUTS)
                    return StreamScanner.NO_ID;
                return Id[input];
            }
        }

        public const int INVALID_INPUT = -1000;

        public const int INVALID_STATE = 0;
        public const int INITIAL_STATE = 1;
        public const int SUCCESS_STATE = 2;
        public const int FAILED_STATE = 3;

        public const int INPUT_EOF = 0;
        public const int STREAM_EOF = -1;
        public const int NO_ID = -1;
        public const int TARGET_STATE_MASK = 0xFFFF;

        protected List<ASCIITableState> m_tabela;

        protected StringBuilder CurrentToken;
        protected StringBuilder LastToken;
        protected Stream InputStream;
        protected int LastState;
        protected int CurrentState;
        protected int NextState;
        protected int PushbackInput;

        public StreamScanner()
        {
            Clear();
        }

        public void Clear()
        {
            m_tabela = new List<ASCIITableState>();
            ClearState();
        }

        public void ClearState()
        {
            CurrentToken = new StringBuilder();
            LastToken = null;
            InputStream = null;
            LastState = INVALID_STATE;
            CurrentState = INVALID_STATE;
            NextState = INVALID_STATE;
            PushbackInput = INVALID_INPUT;
        }

        private ASCIITableState GetState(int index)
        {
            while (m_tabela.Count <= index)
                m_tabela.Add(null);
            if (m_tabela[index] == null)
                m_tabela[index] = new ASCIITableState();
            return m_tabela[index];
        }

        public void SetState(int fromState, int toState, eScannerStateModifiers modifiers)
        {
            SetState(fromState, toState, modifiers, NO_ID);
        }

        public void SetState(int fromState, int toState, eScannerStateModifiers modifiers, int returnId)
        {
            ASCIITableState state = GetState(fromState);
            state.SetState(toState, modifiers, returnId);
        }

        public void SetState(int fromState, int toState, string chars, eScannerStateModifiers modifiers)
        {
            SetState(fromState, toState, chars, modifiers, NO_ID);
        }

        public void SetState(int fromState, int toState, string chars, eScannerStateModifiers modifiers, int returnId)
        {
            ASCIITableState state = GetState(fromState);
            state.SetState(toState, chars, modifiers, returnId);
        }

        public void SetStateSuccess(int stateIndex)
        {
            ASCIITableState state = GetState(stateIndex);
            state.SetState(SUCCESS_STATE, eScannerStateModifiers.eNenhum, NO_ID);
        }

        public void SetStateFail(int stateIndex)
        {
            ASCIITableState state = GetState(stateIndex);
            state.SetState(FAILED_STATE, eScannerStateModifiers.eNenhum, NO_ID);
        }

        public void SetStateSuccessEOF(int stateIndex)
        {
            ASCIITableState state = GetState(stateIndex);
            state.SetState(SUCCESS_STATE, INPUT_EOF, eScannerStateModifiers.eNenhum, NO_ID);
        }

        protected int GetInput()
        {
            int ret;
            if (PushbackInput != INVALID_INPUT)
            {
                ret = PushbackInput;
                PushbackInput = INVALID_INPUT;
            }
            else
            {
                ret = InputStream.ReadByte();
                if (ret == STREAM_EOF) ret = INPUT_EOF;
            }
            return ret;
        }

        public bool ProccessInput(string input)
        {
            return ProccessInput(input, INITIAL_STATE);
        }

        public bool ProccessInput(string input, int initialState)
        {
            byte[] bInput = ASCIIEncoding.ASCII.GetBytes(input);
            using (MemoryStream ms = new MemoryStream(bInput))
            {
                ms.Position = 0;
                InputStream = ms;
                return ProccessInput(initialState);
            }
        }

        public bool ProccessInput(int initialState)
        {
            LastState = INVALID_STATE;
            CurrentState = initialState;
            return ProcessInput();
        }

        public bool ProcessInput()
        {
            while (true)
            {
                int input = GetInput();

                if ((CurrentState < 0 || CurrentState >= m_tabela.Count) || (m_tabela[CurrentState] == null))
                {
                    if (input == INPUT_EOF)
                        Debug.WriteLine(String.Format("Parada por EOF"));
                    else
                        Debug.WriteLine(String.Format("Parada por estado inválido: {0}", CurrentState));
                    break;
                }

                ASCIITableState estadoAtual = m_tabela[CurrentState];
                int target = estadoAtual.GetTarget(input);
                if (target <= 0)
                {
                    Debug.WriteLine(String.Format("Parada por estado inválido: {0}", target));
                    break;
                }

                NextState = target & TARGET_STATE_MASK;

                char dc = '?';
                if (input >= 32 && input < 127) dc = (char)input;

                Debug.WriteLine("{0} -> '{1}'({2}) -> {3}", CurrentState, dc, input, NextState);

                if (NextState == SUCCESS_STATE)
                {
                    Debug.WriteLine("Parada por SUCCESS_STATE");
                    return true;
                }
                else if (NextState == FAILED_STATE)
                {
                    Debug.WriteLine("Parada por FAILED_STATE");
                    break;
                }

                // ----------------------------

                if ((target & (int)eScannerStateModifiers.e01ReturnToken) != 0)
                    ReturnToken(estadoAtual.GetId(input));

                if ((target & (int)eScannerStateModifiers.e02ClearToken) != 0)
                    ClearToken();

                if ((target & (int)eScannerStateModifiers.e03InsertToken) != 0)
                    InsertToken(input);

                // ----------------------------

                if ((target & (int)eScannerStateModifiers.e04ReturnToken) != 0)
                    ReturnToken(estadoAtual.GetId(input));

                if ((target & (int)eScannerStateModifiers.e05ClearToken) != 0)
                    ClearToken();

                if ((target & (int)eScannerStateModifiers.e06InsertToken) != 0)
                    InsertToken(input);

                // ----------------------------

                if ((target & (int)eScannerStateModifiers.e07ReturnToken) != 0)
                    ReturnToken(estadoAtual.GetId(input));

                if ((target & (int)eScannerStateModifiers.e08ClearToken) != 0)
                    ClearToken();

                if ((target & (int)eScannerStateModifiers.e09InsertToken) != 0)
                    InsertToken(input);

                // ----------------------------

                if ((target & (int)eScannerStateModifiers.e10PushBack) != 0)
                    SetInput(input);

                // ----------------------------

                LastState = CurrentState;
                CurrentState = NextState;
            }
            return false;
        }

        private void SetInput(int input)
        {
            PushbackInput = input;
        }

        private void InsertToken(int input)
        {
            if (CurrentToken == null) CurrentToken = new StringBuilder();
            CurrentToken.Append((char)input);
        }

        private void ClearToken()
        {
            if (CurrentToken == null) CurrentToken = new StringBuilder();
            CurrentToken.Clear();
        }

        private void ReturnToken(int id)
        {
            if (CurrentToken == null) CurrentToken = new StringBuilder();
            OnReturntoken(id);
        }

        private void OnReturntoken(int id)
        {
            Debug.WriteLine(String.Format("OnReturntoken({0}) token='{1}'", id, CurrentToken.ToString()));
        }

    }
}
