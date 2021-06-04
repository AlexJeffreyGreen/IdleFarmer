using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities.UI
{
    public class FarmingActionUI : MonoBehaviour
    {
        [SerializeField] private Text FarmingActionText;
        [SerializeField] private Text FarmingActionValue;


        public void UpdateText(string _farmingActionText, string _farmingActionValue)
        {
            this.FarmingActionText.text = _farmingActionText;
            this.FarmingActionValue.text = _farmingActionValue;
        }

        public void OnDestroy()
        {
            
        }
    }
}
