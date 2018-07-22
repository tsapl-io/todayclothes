using System;
using System.Globalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class WeatherScript : MonoBehaviour
{
    public GameObject DirectionalLight;

    public Text TodayText;
    public Text TomorrowText;
    
    public RawImage TodayWeather;
    public RawImage TomorrowWeather;

    public Texture SunTexture;
    public Texture CloudTexture;
    public Texture RainTexture;
    public Texture SnowTexture;

    public Text TodayTempWeather;
    public Text TodayMaxTempWeather;
    public Text TodayMinTempWeather;
    public Text TomorrowTempWeather;

    public RawImage ShortSleeve;    // 半袖
    public RawImage Shorts;         // 半ズボン
    public GameObject LongSleeve;   // 長袖1
    public GameObject LongSleeve2;  // 長袖2
    public GameObject LongTrousers; // 長ズボン
    public GameObject Boots;        // 長靴
    public GameObject Umbrella;     // 傘
    public GameObject Jumper;       // ジャンバー
    public GameObject WaterBottle;  // 水筒＆帽子

    public RawImage ShortSleeve1;   // 半袖
    public RawImage Shorts1;        // 半ズボン
    public GameObject LongSleeve1;  // 長袖1
    public GameObject LongSleeve21; // 長袖2
    public GameObject LongTrousers1;// 長ズボン
    public GameObject Boots1;       // 長靴
    public GameObject Umbrella1;    // 傘
    public GameObject Jumper1;      // ジャンバー
    public GameObject WaterBottle1; // 水筒＆帽子

    public Text ShortsText;
    public Text ShortsText1;

    public Texture ShortSleeveM;
    public Texture ShortSleeveW;
    public Texture ShortsM;
    public Texture ShortsW;

    public Text LoadingText;
    public Text DetailsLoadingText;
    public Text NowPostCodeText;
    public Text WeatherTitle;
    public Text WindInformation;
    public GameObject CloudSystem;
    public GameObject RainWater;
    public GameObject SnowWater;
    public GameObject LoadingPanel;
    public GameObject SettingPanel;
    public Toggle SettingToggle;
    public InputField PostInput;
    public Button PostButton;
    public Button ReloadButton;
    public Button ResetButton;

    public GameObject StartPanel;
    public InputField StartPanelField;
    public Button StartPanelButton;

    public Slider SexSlider;
    public Slider SettingSexSlider;
    public Button SettingSexButton;

    public SCS SCS;
    public LeafSpawnerScript LeafSpawnerScript;

    /*
    private AudioSource audioSource;
    public AudioClip SummerSound;
    */

    float timeCounter;
    public string sex;
    bool ClothHave = false;
    bool GetWeather;
    bool ShowType;
    string ClothSentence;
    bool ClothType;
    public bool RainType = false;
    public bool Rain;
    public string postcode;
    int ListNumber;

    // YOLP 郵便番号チェック
    [Serializable]
    class PostCheckClass
    {
        public CResultInfoClass ResultInfo;
    }
    [Serializable]
    class CResultInfoClass
    {
        public int Total;
    }
    // YOLP 郵便番号から座標系抽出
    [Serializable]
    class PostClass
    {
        public PFeatureClass[] Feature;
    }
    [Serializable]
    class PFeatureClass
    {
        public PGeometryClass Geometry;
        public PPropertyClass Property;
        public string Name;
    }
    [Serializable]
    class PGeometryClass
    {
        public string Coordinates;
    }
    [Serializable]
    class PPropertyClass
    {
        public string Address;
    }
    // OpenWeatherMap
    [Serializable]
    class WeatherClass
    {
        public WWeatherClass[] weather;
        public WMainClass main;
        public WWindClass wind;
    }
    [Serializable]
    class WWeatherClass
    {
        public string main;
    }
    [Serializable]
    class WMainClass
    {
        public string temp;
        public string temp_min;
        public string temp_max;
    }
    [Serializable]
    class WWindClass
    {
        public float speed;
    }
    // OpenWeatherMap_Forecast
    [Serializable]
    class ForecastClass
    {
        public FListClass[] list;
    }
    [Serializable]
    class FListClass
    {
        public FWeatherClass[] weather;
        public FMainClass main;
        public string dt_txt;
    }
    [Serializable]
    class FMainClass
    {
        public string temp;
        public string temp_min;
        public string temp_max;
    }
    [Serializable]
    class FWeatherClass
    {
        public string main;
    }

    // Use this for initialization
    void Start()
    {
        LoadingText.text = "準備中...";
        LoadingPanel.SetActive(true);
        GetWeather = true;
        ReloadButton.interactable = false;
        SettingPanel.SetActive(false);

        sex = PlayerPrefs.GetString("Sex", "Error. Sex is not found.");
        postcode = PlayerPrefs.GetString("PostNumber", "None");
        if (postcode == "None") {
            GetWeather = false;
            StartPanel.SetActive(true);
        }
        PostButton.onClick.AddListener(() =>
        {
            if (PostInput.text == "") {

            } else {
                postcode = PostInput.text;
                PlayerPrefs.SetString("PostNumber", PostInput.text);
                PlayerPrefs.Save();
                Start();
            }
        });
        ReloadButton.onClick.AddListener(() =>
        {
            Start();
        });
        StartPanelButton.onClick.AddListener(() =>
        {
            StartPanel.SetActive(false);
            if (StartPanelField.text == "") {

            } else {
                postcode = StartPanelField.text;
                PlayerPrefs.SetString("PostNumber", StartPanelField.text);
                PlayerPrefs.Save();
                Start();
            }
            if (SexSlider.value == 0f) {
                sex = "m";
                PlayerPrefs.SetString("Sex", sex);
            } else {
                sex = "w";
                PlayerPrefs.SetString("Sex", sex);
            }

        });
        ResetButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString("PostNumber", "None");
            PlayerPrefs.Save();
            Start();
        });
        SettingSexButton.onClick.AddListener(() =>
        {
            if (SettingSexSlider.value == 0f) {
                sex = "m";
                PlayerPrefs.SetString("Sex", sex);
            } else {
                sex = "w";
                PlayerPrefs.SetString("Sex", sex);
            }
            Start();
        });
        
        if (sex == "m") {
            ShortSleeve.texture = ShortSleeveM;
            ShortSleeve1.texture = ShortSleeveM;
            Shorts.texture = ShortsM;
            Shorts1.texture = ShortsM;
            ShortsText.text = "半ズボン";
            ShortsText1.text = "半ズボン";
        } else {
            ShortSleeve.texture = ShortSleeveW;
            ShortSleeve1.texture = ShortSleeveW;
            Shorts.texture = ShortsW;
            Shorts1.texture = ShortsW;
            ShortsText.text = "スカート";
            ShortsText1.text = "スカート";
        }

        if (Application.internetReachability == NetworkReachability.NotReachable) {
            GetWeather = false;
            LoadingText.text = "インターネットに接続できません。インターネット接続を確認してください。";
        }
        if (GetWeather) {
            StartCoroutine(CoStart());
        }

    }


    IEnumerator CoStart()
    {
        // 取得&解析処理
        DetailsLoadingText.text = "郵便番号を検索しています...";
        LoadingText.text = "ダウンロード中... (0%)";
        WWW dl = new WWW("https://map.yahooapis.jp/search/zip/V1/zipCodeSearch?query=" + postcode + "&appid=XXXXXXXX&output=json");
        while (dl.isDone == false)
        {
            yield return null;
        }
        LoadingText.text = "ダウンロード中... (25%)";
        DetailsLoadingText.text = "郵便番号が正しいかチェックしています...";
        PostCheckClass poscheck = JsonUtility.FromJson<PostCheckClass>(dl.text);
        if (poscheck.ResultInfo.Total == 0) {
            LoadingText.text = "郵便番号が正しくありません。";
            DetailsLoadingText.text = "郵便番号が正しくありません。左下の設定から設定しなおしてください。";
        } else {
            DetailsLoadingText.text = "郵便番号から緯度・経度を抽出しています...";
            PostClass post = JsonUtility.FromJson<PostClass>(dl.text);
            string[] coordinates = post.Feature[0].Geometry.Coordinates.Split(',');
            DetailsLoadingText.text = "緯度・経度から天気情報を取得しています...";
            WWW dl_ = new WWW("http://api.openweathermap.org/data/2.5/weather?lat=" + coordinates[1] + "&lon=" + coordinates[0] + "&APPID=XXXXXXXX&units=metric");
            while (dl_.isDone == false)
            {
                yield return null;
            }
            LoadingText.text = "ダウンロード中... (50%)";
            DetailsLoadingText.text = "天気情報を整理しています...";
            WeatherClass weather = JsonUtility.FromJson<WeatherClass>(dl_.text);
            DetailsLoadingText.text = "緯度・経度から予測天気情報を取得しています...";
            WWW dl__ = new WWW("http://api.openweathermap.org/data/2.5/forecast?lat=" + coordinates[1] + "&lon=" + coordinates[0] + "&APPID=XXXXXXXX&units=metric");
            while (dl__.isDone == false)
            {
                yield return null;
            }
            LoadingText.text = "ダウンロード中... (100%)";
            DetailsLoadingText.text = "天気情報を整理しています...";
            ForecastClass forecast = JsonUtility.FromJson<ForecastClass>(dl__.text);

            CultureInfo ci = new CultureInfo("ja-JP");
            DateTime Tomorrow = DateTime.Today + new TimeSpan(1, 6, 0, 0);

            LoadingPanel.SetActive(false);
            for (int i = 0; i < forecast.list.Length; i++)
            {
                if (forecast.list[i].dt_txt == Tomorrow.ToString("yyyy-MM-dd HH:mm:ss"))
                {
                    ListNumber = i;
                }
            }
            // 現在天気
            if (weather.weather[0].main == "Clear")
            {
                TodayWeather.texture = SunTexture;
                CloudSystem.SetActive(false);
            }
            else if (weather.weather[0].main == "Clouds")
            {
                ClothHave = true;
                TodayWeather.texture = CloudTexture;
                CloudSystem.SetActive(true);
            }
            else if (weather.weather[0].main == "Rain")
            {
                ClothHave = true;
                RainType = true;
                TodayWeather.texture = RainTexture;
                CloudSystem.SetActive(true);
            }
            else
            {
                ClothHave = true;
                RainType = false;
                TodayWeather.texture = SnowTexture;
                CloudSystem.SetActive(true);
            }

            if (forecast.list[ListNumber].weather[0].main == "Clear")
            {
                TomorrowWeather.texture = SunTexture;
            }
            else if (forecast.list[ListNumber].weather[0].main == "Clouds")
            {
                ClothHave = true;
                TomorrowWeather.texture = CloudTexture;
            }
            else if (forecast.list[ListNumber].weather[0].main == "Rain")
            {
                ClothHave = true;
                RainType = true;
                TomorrowWeather.texture = RainTexture;
            }
            else
            {
                ClothHave = true;
                RainType = false;
                TomorrowWeather.texture = SnowTexture;
            }
            TodayTempWeather.text = Math.Floor(Convert.ToDouble(weather.main.temp)) + "℃";
            TodayMaxTempWeather.text = Math.Floor(Convert.ToDouble(weather.main.temp_max)) + "℃";
            TodayMinTempWeather.text = Math.Floor(Convert.ToDouble(weather.main.temp_min)) + "℃";
            TomorrowTempWeather.text = Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp)) + "℃";
            TodayText.text = "今日 (" + DateTime.Today.ToString("M/d dddd", ci) + ")";
            TomorrowText.text = "明日 (" + Tomorrow.ToString("M/d dddd", ci) + ")";
            WeatherTitle.text = post.Feature[0].Property.Address + " の天気";
            NowPostCodeText.text = "現在の位置 : " + post.Feature[0].Property.Address;

            // 洋服選択プログラム (今日)
            if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 50)
            {
                ShortSleeve.gameObject.SetActive(true);
                Shorts.gameObject.SetActive(true);
                WaterBottle.SetActive(true);
            }
            if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 25)
            {
                ShortSleeve.gameObject.SetActive(true);
                Shorts.gameObject.SetActive(true);
            }
            if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 23)
            {
                if (weather.weather[0].main == "Clear")
                {
                    ShortSleeve.gameObject.SetActive(true);
                    Shorts.gameObject.SetActive(true);
                }
                else
                {
                    LongSleeve.SetActive(true);
                    Shorts.gameObject.SetActive(true);
                }
            }
            if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 20)
            {
                LongSleeve.SetActive(true);
                LongSleeve2.SetActive(true);
                LongTrousers.SetActive(true);
            }
            if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 15)
            {
                LongSleeve.SetActive(true);
                LongSleeve2.SetActive(true);
                LongTrousers.SetActive(true);
                Jumper.SetActive(true);
            }
            if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 10)
            {
                LongSleeve.SetActive(true);
                LongSleeve2.SetActive(true);
                LongTrousers.SetActive(true);
                Jumper.SetActive(true);
            }
            if (weather.weather[0].main == "Rain")
            {
                Umbrella.SetActive(true);
                Boots.SetActive(true);
            }
            // 洋服選択プログラム (明日)
            if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 50)
            {
                ShortSleeve1.gameObject.SetActive(true);
                Shorts1.gameObject.SetActive(true);
                WaterBottle1.SetActive(true);
            }
            if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 26)
            {
                ShortSleeve1.gameObject.SetActive(true);
                Shorts1.gameObject.SetActive(true);
            }
            if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 23)
            {
                if (forecast.list[ListNumber].weather[0].main == "Clear")
                {
                    ShortSleeve1.gameObject.SetActive(true);
                    Shorts1.gameObject.SetActive(true);
                }
                else
                {
                    LongSleeve1.SetActive(true);
                    Shorts1.gameObject.SetActive(true);
                }
            }
            if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 20)
            {
                LongSleeve1.SetActive(true);
                LongSleeve21.SetActive(true);
                LongTrousers1.SetActive(true);
            }
            if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 15)
            {
                LongSleeve1.SetActive(true);
                LongSleeve21.SetActive(true);
                LongTrousers1.SetActive(true);
                Jumper1.SetActive(true);
            }
            if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 10)
            {
                LongSleeve1.SetActive(true);
                LongSleeve21.SetActive(true);
                LongTrousers1.SetActive(true);
                Jumper1.SetActive(true);
            }
            if (forecast.list[ListNumber].weather[0].main == "Rain")
            {
                Umbrella1.SetActive(true);
                Boots1.SetActive(true);
            }

            // 風判別プログラム
            if (weather.wind.speed < 50f)
            {
                SCS.CloudsSpeed = 10f;
                LeafSpawnerScript.LeafWind = true;
                WindInformation.text = "風情報 : 風がすごく強いです！外出を控えてください！";
            }
            if (weather.wind.speed < 30f)
            {
                SCS.CloudsSpeed = 6f;
                LeafSpawnerScript.LeafWind = true;
                WindInformation.text = "風情報 : 風がすごく強いです！気をつけてください！";
            }
            if (weather.wind.speed < 15f)
            {
                SCS.CloudsSpeed = 4f;
                LeafSpawnerScript.LeafWind = true;
                WindInformation.text = "風情報 : 風が強いです。";
            }
            if (weather.wind.speed < 9f)
            {
                SCS.CloudsSpeed = 2.5f;
                LeafSpawnerScript.LeafWind = false;
                WindInformation.text = "風情報 : 風がすこし吹いています。";
            }
            if (weather.wind.speed < 3f)
            {
                SCS.CloudsSpeed = 1.5f;
                LeafSpawnerScript.LeafWind = false;
                WindInformation.text = "風情報 : 風はほとんど吹きません。";
            }

            if (DateTime.Now.Hour < 24) { // 夜中
                DirectionalLight.transform.rotation = Quaternion.Euler(185f, 0f, 0f);
            }
            if (DateTime.Now.Hour < 16) { // 夕方
                DirectionalLight.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
            }
            if (DateTime.Now.Hour < 15) { // 昼
                DirectionalLight.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            }
            if (DateTime.Now.Hour < 10) { // 朝
                DirectionalLight.transform.rotation = Quaternion.Euler(15f, 0f, 0f);
            }
            if (DateTime.Now.Hour < 5) { // 夜中
                DirectionalLight.transform.rotation = Quaternion.Euler(-5, 0f, 0f);
            }

            /*
            if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) >= 30) {
                audioSource = GetComponent<AudioSource>();
                audioSource.clip = SummerSound;
                audioSource.Play();
            }*/

            /*
                    if (DateTime.Now.Month < 9 && DateTime.Now.Month > 4)
                    {
                        ClothType = true; // 半袖
                    }
                    else
                    {
                        ClothType = false; // 長袖
                    }*/

            ReloadButton.interactable = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SettingToggle.isOn == false)
        {
            SettingPanel.SetActive(false);
        }
        else if (SettingToggle.isOn == true)
        {
            SettingPanel.SetActive(true);
        }

        timeCounter += Time.deltaTime;
        if (timeCounter > 0.1f)
        {
            timeCounter = 0f;
            RainGenerate();
        }
    }
    void RainGenerate()
    {
        if (Rain)
        {
            for (int i = 0; i < 2 * 25; i++)
            {
                if (RainType == true)
                {
                    Instantiate(RainWater, new Vector3(UnityEngine.Random.Range(-10f, 10f), 50f, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
                }
                else
                {
                    Instantiate(SnowWater, new Vector3(UnityEngine.Random.Range(-10f, 10f), 50f, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
                }
            }
        }

    }
}
