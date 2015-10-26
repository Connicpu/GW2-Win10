using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GW2_Win10.Annotations;

namespace GW2_Win10.Helpers
{
    public class AsyncViewValue<T> : INotifyPropertyChanged
    {
        private bool _isFailed;
        private bool _isComplete;
        private T _result;

        public AsyncViewValue(Task<T> task)
        {
            var scheduler = (SynchronizationContext.Current == null)
                ? TaskScheduler.Current
                : TaskScheduler.FromCurrentSynchronizationContext();

            task.ContinueWith(
                OnCompletion,
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                scheduler
                );
        }

        public T Result
        {
            get { return _result; }
            private set
            {
                if (Equals(value, _result)) return;
                _result = value;
                OnPropertyChanged();
            }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
            private set
            {
                if (value == _isComplete) return;
                _isComplete = value;
                OnPropertyChanged();
            }
        }

        public bool IsFailed
        {
            get { return _isFailed; }
            private set
            {
                if (value == _isFailed) return;
                _isFailed = value;
                OnPropertyChanged();
            }
        }

        private void OnCompletion(Task<T> task)
        {
            if (task.IsFaulted)
            {
                IsFailed = false;
            }
            else
            {
                Result = task.Result;
                IsComplete = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
