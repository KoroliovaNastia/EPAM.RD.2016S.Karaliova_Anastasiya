﻿using System.Threading;

namespace Monitor
{
    // TODO: Use SpinLock to protect this structure.
    public class AnotherClass
    {
        private int _value;
        SpinLock spLock = new SpinLock();


        public int Counter
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public void Increase()
        {
            bool lockTaken = false;
            try
            {
                if(!spLock.IsHeldByCurrentThread)
                spLock.Enter(ref lockTaken);

                _value++;
            }
            finally
            {
                if (lockTaken) spLock.Exit();
            }
        }

        public void Decrease()
        {
            bool lockTaken = false;
            try
            {
                if (!spLock.IsHeldByCurrentThread)
                    spLock.Enter(ref lockTaken);

                _value--;
            }
            finally
            {
                if (lockTaken) spLock.Exit();
            }
        }
    }
}
