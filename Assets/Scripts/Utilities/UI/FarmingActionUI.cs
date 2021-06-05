using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utilities.TileManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities.UI
{
    public class FarmingActionUI : MonoBehaviour
    {
        [SerializeField] private Text _farmingActionText;
        [SerializeField] private Text _farmingActionValue;
        [SerializeField] private Button _farmingActionButton;
        private Vector3Int _location;

        public Guid ActionId { get; set; }

        public void UpdateText(string _farmingActionText, Vector3Int location)
        {
            this._farmingActionText.text = _farmingActionText;
            this._farmingActionValue.text = location.ToString();
            this._location = location;
        }

        public void ActionSelected()
        {
            Debug.Log(($"Farming action selected at position {this._location}"));
            //TileMapManager.instance.HighlightTileAtPosition(this._location); // todo, fix this so I can highlight the tile!
        }

        public void OnDestroy()
        {
            
        }
    }
}
