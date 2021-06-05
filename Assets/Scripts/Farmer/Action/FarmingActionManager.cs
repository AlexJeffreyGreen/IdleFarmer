using Assets.Scripts.Utilities.UI;
using Farmer.Action;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Farmer.Action.FarmingActions;
using Assets.Scripts.Utilities.DayAndWeather;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Scripts.Farmer.Action
{
    public class FarmingActionManager : MonoBehaviour
    {
        public static FarmingActionManager instance;

        private Queue<KeyValuePair<ActionBase, FarmingActionUI>> _actionQueue;

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

            _actionQueue = new Queue<KeyValuePair<ActionBase, FarmingActionUI>>();
        }

        public void EnqueueNewAction(ActionBase newAction)
        {
            //check percentages, if meets percentages, add additional event action
           
            FarmingActionUI newUIEle = Instantiate<FarmingActionUI>(this._farmingActionUI);
            newUIEle.UpdateText(newAction.GetName(), newAction.GetPosition());
            newUIEle.ActionId = newAction.Id;
            newUIEle.transform.SetParent(this._rect);
            this._actionQueue.Enqueue(new KeyValuePair<ActionBase, FarmingActionUI>(newAction, newUIEle));
            Debug.Log($"Enqueued new action, count: {this._actionQueue.Count}");
        }

        public void DequeueAction(bool allActions)
        {
            if (!this._actionQueue.Any())
                return;
                
            if(!allActions)
            {
                KeyValuePair<ActionBase,FarmingActionUI> pairing = this._actionQueue.Dequeue();
                Destroy(pairing.Value.gameObject);
                return;
            }
            else
            {
                while (this._actionQueue.Count > 0)
                {
                    KeyValuePair<ActionBase,FarmingActionUI> pairing = this._actionQueue.Dequeue();
                    Destroy(pairing.Value.gameObject);
                }
            }
        }

        public void DequeueActionsAsync()
        {
            StartCoroutine(AsyncDequeue());
        }
        
        IEnumerator AsyncDequeue()
        {
            while(this._actionQueue.Count > 0)
            {
                this.DequeueAction(false);
                yield return new WaitForSeconds(1f);
            }
            DayAndWeatherManager.instance.ChangeStateOfDequeueActionButton();
            yield break;
        }

        public IEnumerable<KeyValuePair<ActionBase, FarmingActionUI>> ActionsAtLocation(Vector3Int location)
        {
            //PlantSeedAction a = ActionFactory.Create<PlantSeedAction>(location,)
            return this._actionQueue.Where(x => x.Key.GetPosition() == location);
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
