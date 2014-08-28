using System;
using System.Threading;
using System.Threading.Tasks;

namespace Exagreen.SolarPulse.Core.Helpers
{
    public class Timer
    {
        public event EventHandler Tick;

        private bool _continue = true;

        public Timer()
        {
            Task.Delay(100).ContinueWith(async (t, s) =>
            {
                while (true)
                {
                    await Task.Delay(3000).ConfigureAwait(false);
                    if (!_continue)
                        continue;

                    Task.Run(() =>
                    {
                        if (Tick != null)
                            Tick(this, EventArgs.Empty);
                    });


                }

            }, null, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }

        public void Start()
        {
            _continue = true;
        }

        public void Stop()
        {
            _continue = false;
        }

    }

    internal sealed class Timer2 : CancellationTokenSource
    {
        internal Timer2(Action<object> callback, object state, int millisecondsPeriod, bool waitForCallbackBeforeNextPeriod = false)
        {
            //Contract.Assert(period == -1, "This stub implementation only supports dueTime.");

            Task.Delay(100, Token).ContinueWith(async (t, s) =>
            {
                var tuple = (Tuple<Action<object>, object>)s;

                while (!IsCancellationRequested)
                {
                    if (waitForCallbackBeforeNextPeriod)
                        tuple.Item1(tuple.Item2);
                    else
                        Task.Run(() => tuple.Item1(tuple.Item2));

                    await Task.Delay(millisecondsPeriod, Token).ConfigureAwait(false);
                }

            }, Tuple.Create(callback, state), CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Cancel();

            base.Dispose(disposing);
        }
    }
}