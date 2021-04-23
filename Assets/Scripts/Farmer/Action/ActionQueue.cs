using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

namespace Farmer.Action
{
    public sealed class ActionQueue
    {
        private static ActionQueue _actionQueue = null;
        private static readonly object _lock = new object();
        private readonly Queue<ActionBase> AQueue = new Queue<ActionBase>();
        public ActionQueue(){}

        public static ActionQueue ActionQueueManager
        {
            get
            {
                lock (_lock)
                {
                    return _actionQueue ?? (_actionQueue = new ActionQueue());
                }
            }
        }

        public void EnqueueAction(ActionBase action)
        {
            this.AQueue.Enqueue(action);
        }

        public ActionBase DequeueAction()
        {
            return this.AQueue.Dequeue();
        }

        public int Count
        {
            get { return this.AQueue.Count; }
        }


        public bool AnyAtPosition(Vector3Int gridPosition)
        {
            ActionBase[] queue = this.AQueue.ToArray();
            return queue.Any(x => x.GetPosition() == gridPosition);
        }
    }
}