using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GW2_Win10.Helpers
{
    public sealed class RwLock<T> where T : new()
    {
        private T _value;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly ReadHandle _readHandle;
        private readonly WriteHandle _writeHandle;

        public RwLock()
        {
            _value = new T();
            _readHandle = new ReadHandle(this);
            _writeHandle = new WriteHandle(this);
        }

        public RwLock(T value)
        {
            _value = value;
            _readHandle = new ReadHandle(this);
            _writeHandle = new WriteHandle(this);
        }

        public ReadHandle Read()
        {
            _lock.EnterUpgradeableReadLock();
            return _readHandle;
        }

        public WriteHandle Write()
        {
            _lock.EnterWriteLock();
            return _writeHandle;
        }

        public sealed class ReadHandle : IDisposable
        {
            private readonly RwLock<T> _rwLock;

            public T Value => _rwLock._value;

            public ReadHandle(RwLock<T> rwLock)
            {
                _rwLock = rwLock;
            }

            public void Dispose()
            {
                _rwLock._lock.ExitUpgradeableReadLock();
            }

            public WriteHandle Upgrade()
            {
                return _rwLock.Write();
            }
        }

        public sealed class WriteHandle : IDisposable
        {
            private readonly RwLock<T> _rwLock;

            public T Value
            {
                get { return _rwLock._value; }
                set { _rwLock._value = value; }
            }

            public WriteHandle(RwLock<T> rwLock)
            {
                _rwLock = rwLock;
            }

            public void Dispose()
            {
                _rwLock._lock.ExitWriteLock();
            }
        }
    }
}
