using Assets.Scripts.Utilities.UI;
using Farmer.Action;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Scripts.Farmer.Action
{
    public class FarmingActionManager : MonoBehaviour
    {
        public static FarmingActionManager instance;

        private Queue<ActionBase> _actionQueue;

        [SerializeField] RectTransform _rect;
        [SerializeField] FarmingActionUI _farmingActionUI;


        //public BasicAction BasicFarmingAction;

        private void Awake()
        {
            if(instance == null)
                instance = this;
            else if(instance != null)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            _actionQueue = new Queue<ActionBase>();
        }

        public void EnqueueNewAction(ActionBase newAction)
        {
            //check percentages, if meets percentages, add additional event action
            this._actionQueue.Enqueue(newAction);
            FarmingActionUI newUIEle = Instantiate<FarmingActionUI>(this._farmingActionUI);
            newUIEle.UpdateText(newAction.GetName(), newAction.GetPosition().ToString());
            newUIEle.transform.SetParent(this._rect);

            Debug.Log($"Enqueued new action, count: {this._actionQueue.Count}");
        }

        public void DequeueAction(bool allActions)
        {
            if(!allActions)
            {
                this._actionQueue.Dequeue();
                return;
            }
            else
            {
                while(this._actionQueue.Count > 0)
                    this._actionQueue.Dequeue();
            }
        }

        public IEnumerable<ActionBase> ActionsAtLocation(Vector3Int location)
        {
            return this._actionQueue.Where(x => x.GetPosition() == location);
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
