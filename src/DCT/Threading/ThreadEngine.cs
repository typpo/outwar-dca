using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace DCT.Threading
{
    internal class ThreadEngine
    {
        private const int DEFAULT_MAX = 20;
        private const int DEFAULT_DELAY = 100;

        private static ThreadEngine mDefaultInstance;
        internal static ThreadEngine DefaultInstance
        {
            get { return mDefaultInstance; }
        }

        internal static void Sleep(int ms)
        {
            Thread.Sleep(ms);
        }

        private int mMax;
        internal int Max
        {
            get { return mMax; }
            set { mMax = value; }
        }

        private int mDelay;
        internal int Delay
        {
            get { return mDelay; }
            set { mDelay = value; }
        }

        private Queue<Thread> mThreads;

        static ThreadEngine()
        {
            mDefaultInstance = new ThreadEngine(DEFAULT_MAX, DEFAULT_DELAY);
        }

        internal ThreadEngine(int max, int delay)
        {
            mMax = max;
            mDelay = delay;
            mThreads = new Queue<Thread>();
        }

        internal delegate void ThreadHandler();

        internal void Enqueue(ThreadHandler h)
        {
            Enqueue(new Thread(new ThreadStart(h)));
        }

        internal void Enqueue(Thread t)
        {
            mThreads.Enqueue(t);
        }

        internal void Do(Thread t)
        {
            t.Start();
            while (t.IsAlive)
            {
                Application.DoEvents();
            }
        }

        internal void Do(ThreadHandler h)
        {
            Thread t = new Thread(new ThreadStart(h));
            Do(t);
        }

        internal void DoNonBlocking(Thread t)
        {
            t.Start();
        }

        internal void DoNonBlocking(ThreadHandler h)
        {
            Thread t = new Thread(new ThreadStart(h));
            DoNonBlocking(t);
        }

        internal void DoParameterized(Thread t, object o)
        {
            t.Start(o);
            while (t.IsAlive)
            {
                Application.DoEvents();
            }
        }

        internal delegate void ParameterizedThreadHandler(object o);

        internal void DoParameterized(ParameterizedThreadHandler h, object o)
        {
            Thread t = new Thread(new ParameterizedThreadStart(h));
            DoParameterized(t, o);
        }

        internal void DoParameterizedNonBlocking(Thread t, object o)
        {
            t.Start(o);
        }

        internal void DoParameterizedNonBlocking(ParameterizedThreadHandler h, object o)
        {
            Thread t = new Thread(new ParameterizedThreadStart(h));
            DoParameterizedNonBlocking(t, o);
        }

        internal void ProcessAll()
        {
            Process(mThreads.Count);
        }

        internal void Process(int num)
        {
            Process(num, true);
        }

        internal void ProcessNonBlocking(int num)
        {
            Process(num, false);
        }

        private void Process(int num, bool block)
        {
            Thread[] places = new Thread[mMax];
            for (int i = 0; i < num; i++)
            {
                if (mThreads.Count == 0)
                    break;

                Thread t = mThreads.Dequeue();
                int place = 0;
                while (!t.IsAlive)
                {
                    if (places[place] == null || places[place].ThreadState == ThreadState.Stopped)
                    {
                        places[place] = t;
                        t.Start();
                    }
                    else if (++place == places.Length)
                        place = 0;

                    Application.DoEvents();
                }

                Thread.Sleep(mDelay);
                Application.DoEvents();
            }

            if (block)
            {
                foreach (Thread t in places)
                {
                    if (t == null)
                        continue;

                    while (t.IsAlive)
                    {
                        Application.DoEvents();
                    }
                }
            }
        }

        internal void Clear()
        {
            mThreads.Clear();
        }
    }
}