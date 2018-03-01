using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Olvebra.ConversorArquivosApp.components
{
    public class CountZeroEvent
    {
        protected ManualResetEvent m_WaitHandler;
        protected object m_conterLock = new object();
        protected int m_counter;

        public WaitHandle WaitHandle { get { return m_WaitHandler; } }

        public CountZeroEvent()
        {
            m_WaitHandler = new ManualResetEvent(true);
            m_counter = 0;
        }

        public void Add()
        {
            lock (m_conterLock)
            {
                m_counter++;
                if (m_counter == 1)
                {
                    m_WaitHandler.Reset();
                }
            }
        }

        public void Remove()
        {
            lock (m_conterLock)
            {
                m_counter--;
                if (m_counter == 0)
                {
                    m_WaitHandler.Set();
                }
            }
        }

        public void Clear()
        {
            m_counter = 0;
            m_WaitHandler.Set();
        }

        public void Wait()
        {
            m_WaitHandler.WaitOne();
        }

    }
}
