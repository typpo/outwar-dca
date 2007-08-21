using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace DCT.Threading
{
    public class ThreadEngine
    {
        private const int DEFAULT_MAX = 20;
        private const int DEFAULT_DELAY = 100;

        private static ThreadEngine mDefaultInstance;
        public static ThreadEngine DefaultInstance
        {
            get { return mDefaultInstance; }
        }

        public static void Sleep(int ms)
        {
            Thread.Sleep(ms);
        }

        private int mMax;
        public int Max
        {
            get { return mMax; }
            set { mMax = value; }
        }

        private int mDelay;
        public int Delay
        {
            get { return mDelay; }
            set { mDelay = value; }
        }

        private Queue<Thread> mThreads;

        static ThreadEngine()
        {
            mDefaultInstance = new ThreadEngine(DEFAULT_MAX, DEFAULT_DELAY);
        }

        public ThreadEngine(int max, int delay)
        {
            mMax = max;
            mDelay = delay;
            mThreads = new Queue<Thread>();
        }

        public delegate void ThreadHandler();

        public void Enqueue(ThreadHandler h)
        {
            Enqueue(new Thread(new ThreadStart(h)));
        }

        public void Enqueue(Thread t)
        {
            mThreads.Enqueue(t);
        }

        public void Do(Thread t)
        {
            t.Start();
            while (t.IsAlive)
            {
                Application.DoEvents();
            }
        }

        public void Do(ThreadHandler h)
        {
            Thread t = new Thread(new ThreadStart(h));
            Do(t);
        }

        public void DoNonBlocking(Thread t)
        {
            t.Start();
        }

        public void DoNonBlocking(ThreadHandler h)
        {
            Thread t = new Thread(new ThreadStart(h));
            DoNonBlocking(t);
        }

        public void DoParameterized(Thread t, object o)
        {
            t.Start(o);
            while (t.IsAlive)
            {
                Application.DoEvents();
            }
        }

        public delegate void ParameterizedThreadHandler(object o);

        public void DoParameterized(ParameterizedThreadHandler h, object o)
        {
            Thread t = new Thread(new ParameterizedThreadStart(h));
            DoParameterized(t, o);
        }

        public void DoParameterizedNonBlocking(Thread t, object o)
        {
            t.Start(o);
        }

        public void DoParameterizedNonBlocking(ParameterizedThreadHandler h, object o)
        {
            Thread t = new Thread(new ParameterizedThreadStart(h));
            DoParameterizedNonBlocking(t, o);
        }

        public void ProcessAll()
        {
            Process(mThreads.Count);
        }

        public void Process(int num)
        {
            Process(num, true);
        }

        public void ProcessNonBlocking(int num)
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

        public void Clear()
        {
            mThreads.Clear();
        }
    }
}