using System;
using Assets.Scripts.Utilities.TileManagement;
using UnityEngine;

namespace Assets.Scripts.Utilities.UI
{
    public class MarketUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;

        private void Awake()
        {
            _panel.gameObject.SetActive(false);
        }

        public void OpenMarketSystemUI()
        {
            bool enabled = _panel.gameObject.activeSelf;
            _panel.gameObject.SetActive(!enabled);
            TileMapManager.instance.EnabledTileMap = !TileMapManager.instance.EnabledTileMap;
        }
    }
}