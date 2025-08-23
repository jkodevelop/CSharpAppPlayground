namespace CSharpAppPlayground.Classes
{
    public class AsyncManualResetEvent
    {
        private volatile TaskCompletionSource<bool> _tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public AsyncManualResetEvent(bool initialState = false)
        {
            if (initialState)
                Set();
        }

        public Task WaitAsync() => _tcs.Task;

        public void Set()
        {
            _tcs.TrySetResult(true);
        }

        public void Reset()
        {
            if (_tcs.Task.IsCompleted)
                _tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        }

        public bool IsSet => _tcs.Task.IsCompleted;
    }
}
