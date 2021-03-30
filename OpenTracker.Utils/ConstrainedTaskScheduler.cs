using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTracker.Utils
{
    public class ConstrainedTaskScheduler : TaskScheduler
    {
        [ThreadStatic]
        private static bool _currentThreadIsProcessingItems;

        private int _pendingTaskCount;

        private readonly int _concurrentTasks;
        private readonly LinkedList<Task> _tasks = new();

        public override int MaximumConcurrencyLevel => _concurrentTasks;

        public ConstrainedTaskScheduler() : this(Math.Max(1, Environment.ProcessorCount - 1))
        {
        }
        
        public ConstrainedTaskScheduler(int concurrentTasks)
        {
            if (concurrentTasks < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(concurrentTasks));
            }

            _concurrentTasks = concurrentTasks;
        }

        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>
            {
                _currentThreadIsProcessingItems = true;

                try
                {
                    while (true)
                    {
                        Task item;

                        lock (_tasks)
                        {
                            if (_tasks.Count == 0)
                            {
                                _pendingTaskCount--;
                                break;
                            }

                            item = _tasks.First!.Value;
                            _tasks.RemoveFirst();
                        }

                        TryExecuteTask(item);
                    }
                }
                finally
                {
                    _currentThreadIsProcessingItems = false;
                }
            }, null);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            bool lockTaken = false;

            try
            {
                Monitor.TryEnter(_tasks, ref lockTaken);

                if (lockTaken)
                {
                    return _tasks;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_tasks);
                }
            }
        }

        protected override void QueueTask(Task task)
        {
            lock (_tasks)
            {
                _tasks.AddLast(task);

                if (_pendingTaskCount < _concurrentTasks)
                {
                    _pendingTaskCount++;
                    NotifyThreadPoolOfPendingWork();
                }
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (!_currentThreadIsProcessingItems)
            {
                return false;
            }

            if (taskWasPreviouslyQueued)
            {
                if (TryDequeue(task))
                {
                    return TryExecuteTask(task);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return TryExecuteTask(task);
            }
        }

        protected override bool TryDequeue(Task task)
        {
            lock (_tasks)
            {
                if (_tasks.Remove(task))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
