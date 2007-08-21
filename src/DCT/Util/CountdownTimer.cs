using System;
using System.Windows.Forms;

namespace DCT.Util
{
    internal class CountDownTimer : Timer
    {
        internal event EventHandler Started; // event that occurs when Timer started
        internal event EventHandler Stopped; // event that occurs when Timer stopped

        private int downCounter = -1; // variable will keep information about initial count down value
        private int currentDownCounter = 0; // will keep the current count down value

        private bool stopped = false; // indicates if timer is stopped, helper variable for sleep mode

        internal int Countdown // property, gives access to downCounter
        {
            get { return this.downCounter; }
            set { this.downCounter = value; }
        }

        internal int CurrentCountdown // property, gives access to currentCountDown
        {
            get { return this.currentDownCounter; }
            set { this.currentDownCounter = value; }
        }

        // default constructor
        internal CountDownTimer()
        {
        }

        // constructor to initialize the countdown timer
        internal CountDownTimer(int countDown)
        {
            this.downCounter = countDown;
            this.currentDownCounter = countDown;
            base.Tick += new EventHandler(this.countdown);
            // subscribe to the Tick event of the Timer base class, to be able to count down
        }

        protected virtual void OnStarted(EventArgs e) // will be called when Timer started
        {
            if (Started != null)
            {
                //Invokes the delegates.
                Started(this, e);
            }
        }

        protected virtual void OnStopped(EventArgs e) // will be called when Timer stopped
        {
            if (Stopped != null)
            {
                //Invokes the delegates.
                Stopped(this, e);
            }
        }

        internal new void Start() // will start the timer, overwrites the base Start method
        {
            this.Enabled = true;
        }

        internal new void Stop() // will stop the timer, overwrites the base Stop method
        {
            this.Enabled = false;
        }

        internal bool Sleep // will set the countDownTimer to sleep mode
        {
            set
            {
                // only set, if timer not stopped; will just stop the base timer
                if (!this.stopped) base.Enabled = !value;
            }
            get { return !base.Enabled; }
        }

        public override bool Enabled // overwrites the base Enabled property
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;

                if (value)
                {
                    this.stopped = false;
                    this.currentDownCounter = this.downCounter;
                    OnStarted(new EventArgs());
                }
                else
                {
                    this.stopped = true;
                    OnStopped(new EventArgs());
                }
            }
        }

        private void countdown(object sender, EventArgs e)
            // will be called if base class fires a Tick event, counts down and stops if 0 reached
        {
            if (this.downCounter == -1)
            {
                // run forever
                return;
            }
            else if (this.currentDownCounter > 0)
            {
                this.currentDownCounter--;
            }

            if (this.currentDownCounter == 0)
            {
                this.Enabled = false;
            }
        }
    }
}