using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Farmer.Action;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities.DayAndWeather
{
    public class DayAndWeatherManager : MonoBehaviour
    {
        public static DayAndWeatherManager instance;
        [SerializeField] private Text WalletInfo;
        [SerializeField] private Text DayInfo;
        [SerializeField] private Text WeatherReportInfo;
        [SerializeField] private int DayCount = 1;
        [SerializeField] private Button SpendActionButton;
        //private bool spendActionDisabled = false;
        private Weather currentWeather;

        private void Awake()
        {
            if(instance == null)
                instance = this;
            else if(instance != null)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            currentWeather = Weather.Sunny;
            UpdateWalletInfo(0.00m);
            UpdateWeatherReportInfo(this.currentWeather);
            UpdateDayInfo(this.DayCount);
        }

        void Start()
        {
            //StartCoroutine(OnDayManagement());
        }

        //IEnumerator OnDayManagement()
        //{
        //    while(true)
        //    {
        //        Debug.Log("OnCoroutine: " + (int)Time.time);
        //        yield return new WaitForSeconds(5f);
        //    }
        //}

        private void Update()
        {
            
        }



        public void UpdateWalletInfo(decimal amount)
        {
            this.WalletInfo.text = amount.ToString("F");
        }

        public void UpdateWeatherReportInfo(Weather weather)
        {
            this.WeatherReportInfo.text = Enum.GetName(typeof(Weather), weather);
        }

        public void UpdateDayInfo(int dayCount)
        {
            this.DayInfo.text = dayCount.ToString();
        }

        public void IncrementDayAndProcessActions()
        {
            //make async
            DayCount++;
            ProcessRandomEvents();
            //UpdateWalletInfo(0.00m);
            this.currentWeather = (Weather)(typeof(Weather).GetRandomEnumValue());
            UpdateWeatherReportInfo(this.currentWeather);
            UpdateDayInfo(this.DayCount);
            DequeueAllActions();
        }

        private void ProcessRandomEvents()
        {

        }

        private void DequeueAllActions()
        {
            this.ChangeStateOfDequeueActionButton();
            FarmingActionManager.instance.DequeueActionsAsync();
        }

        public void ChangeStateOfDequeueActionButton()
        {
            this.SpendActionButton.enabled = !this.SpendActionButton.enabled;
            if (!this.SpendActionButton.enabled)
                this.SpendActionButton.GetComponentInChildren<Text>().text = "Processing Actions";
            else
                this.SpendActionButton.GetComponentInChildren<Text>().text = "Start Day";
            Debug.Log($"Action button is - {this.SpendActionButton.enabled}");
        }
        


    }
}
