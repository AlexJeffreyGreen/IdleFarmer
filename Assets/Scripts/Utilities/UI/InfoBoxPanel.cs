using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBoxPanel : MonoBehaviour
{
    public static InfoBoxPanel instance;
    [SerializeField] private Text WalletInfo;
    [SerializeField] private Text DayInfo;
    [SerializeField] private Text WeatherReportInfo;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWalletInfo(decimal amount)
    {
        this.WalletInfo.text = amount.ToString("F");
    }

    public void UpdateWeatherReportInfo(Text weatherText)
    {
        this.WeatherReportInfo.text = "";
    }

    public void UpdateDayInfo(int dayCount)
    {
        this.DayInfo.text = dayCount.ToString();
    }
}
