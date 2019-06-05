using System;
using System.Globalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 400: ネット接続なし
// 401: APIキー使用不可

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
    public Texture ThunderTexture;
    public Texture MistTexture;
    public Texture TyphoonTexture;

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

    public Texture ShortSleeveM;
    public Texture ShortSleeveW;
    public Texture ShortsM;
    public Texture ShortsW;

    public Text LoadingText;
    public SimpleHealthBar loadingBar;
    public Text WeatherTitle;
    public Text WindInformation;
    public GameObject CloudSystem;
    public GameObject RainWater;
    public GameObject SnowWater;
    public GameObject LoadingPanel;
    public GameObject SettingPanel;
    public Button SettingButton;
    public Button SettingOKButton;
    public InputField PostInput;
    public Button PostErrorSettingShowButton;

    public GameObject StartPanel;
    public InputField StartPanelField;
    public Button StartPanelButton;

    public Toggle SexCheckM;
    public Toggle SexCheckW;
    public Toggle SettingSexCheckM;
    public Toggle SettingSexCheckW;

    public SCS SCS;
    public LeafSpawnerScript LeafSpawnerScript;

    public GameObject AvaterM; // 男アバター-今日
    public GameObject AvaterM1; // 男アバター-明日

    public GameObject AvaterMShortSleeve;  // 半袖-男アバター-今日
    public GameObject AvaterMShorts;       // 半ズボン-男アバター-今日
    public GameObject AvaterMLongSleeve;   // 長袖1-男アバター-今日
    public GameObject AvaterMLongSleeve2;  // 長袖2-男アバター-今日
    public GameObject AvaterMLongTrousers; // 長ズボン-男アバター-今日
    public GameObject AvaterMUmbrella;     // 傘-男アバター-今日
    public GameObject AvaterMJumper;       // ジャンバー-男アバター-今日

    public GameObject AvaterMShortSleeve1; // 半袖-男アバター-明日
    public GameObject AvaterMShorts1;      // 半ズボン-男アバター-明日
    public GameObject AvaterMLongSleeve1;  // 長袖1-男アバター-明日
    public GameObject AvaterMLongSleeve21; // 長袖2-男アバター-明日
    public GameObject AvaterMLongTrousers1;// 長ズボン-男アバター-明日
    public GameObject AvaterMUmbrella1;    // 傘-男アバター-明日
    public GameObject AvaterMJumper1;      // ジャンバー-男アバター-明日

    public GameObject AvaterW; // 女アバター-今日
    public GameObject AvaterW1; // 女アバター-明日

    public GameObject AvaterWShortSleeve;  // 半袖-女アバター-今日
    public GameObject AvaterWShorts;       // 半ズボン-女アバター-今日
    public GameObject AvaterWLongSleeve;   // 長袖1-女アバター-今日
    public GameObject AvaterWLongSleeve2;  // 長袖2-女アバター-今日
    public GameObject AvaterWLongTrousers; // 長ズボン-女アバター-今日
    public GameObject AvaterWUmbrella;     // 傘-女アバター-今日
    public GameObject AvaterWJumper;       // ジャンバー-女アバター-今日

    public GameObject AvaterWShortSleeve1; // 半袖-女アバター-明日
    public GameObject AvaterWShorts1;      // 半ズボン-女アバター-明日
    public GameObject AvaterWLongSleeve1;  // 長袖1-女アバター-明日
    public GameObject AvaterWLongSleeve21; // 長袖2-女アバター-明日
    public GameObject AvaterWLongTrousers1;// 長ズボン-女アバター-明日
    public GameObject AvaterWUmbrella1;    // 傘-女アバター-明日
    public GameObject AvaterWJumper1;      // ジャンバー-女アバター-明日

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
    bool isStartPanel;
    public bool isSetting;

    public string TodayAPIKey;
    public string TomorrowAPIKey;
    public string YahooAPIKey;

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
        public int cod;
        public WWeatherClass[] weather;
        public WMainClass main;
        public WWindClass wind;
    }
    [Serializable]
    class WWeatherClass
    {
        public string main;
        public int id;
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
        public int id;
    }
    [Serializable]
    class APIKeyClass
    {
        public string owm_today;
        public string owm_tomorrow;
        public string yahoo;
    }

    // Use this for initialization
    void Start()
    {
        StartPanelButton.onClick.AddListener(() =>
        {
            AvaterM.SetActive(false);
            AvaterM1.SetActive(false);
            AvaterW.SetActive(false);
            AvaterW1.SetActive(false);
            isStartPanel = false;
            StartPanel.SetActive(false);
            if (StartPanelField.text == "") {

            } else {
                postcode = StartPanelField.text;
                PlayerPrefs.SetString("PostNumber", StartPanelField.text);
                PlayerPrefs.Save();
                Start();
            }
            if (SexCheckM.isOn == true) {
                sex = "m";
                PlayerPrefs.SetString("Sex", sex);
            } else {
                sex = "w";
                PlayerPrefs.SetString("Sex", sex);
            }

        });
        SettingButton.onClick.AddListener(() =>
        {
            if (isSetting == false) {
                SettingPanel.SetActive(true);
                WeatherTitle.text = "設定";
                isSetting = true;
                postcode = PlayerPrefs.GetString("PostNumber");
                sex = PlayerPrefs.GetString("Sex");
                PostInput.text = postcode;
                if (sex == "m") {
                    SettingSexCheckM.isOn = true;
                    SettingSexCheckW.isOn = false;
                } else {
                    SettingSexCheckM.isOn = false;
                    SettingSexCheckW.isOn = true;
                }
            } else if (isSetting == true){
                SettingPanel.SetActive(false);
                isSetting = false;
                if (SettingSexCheckM.isOn == true) {
                    sex = "m";
                    PlayerPrefs.SetString("Sex", sex);
                } else {
                    sex = "w";
                    PlayerPrefs.SetString("Sex", sex);
                }
                postcode = PostInput.text;
                PlayerPrefs.SetString("PostNumber", PostInput.text);
                PlayerPrefs.Save();
                Restart();
            }
        });
        SettingOKButton.onClick.AddListener(() =>
        {
            SettingPanel.SetActive(false);
            isSetting = false;
            if (SettingSexCheckM.isOn == true) {
                sex = "m";
                PlayerPrefs.SetString("Sex", sex);
            } else {
                sex = "w";
                PlayerPrefs.SetString("Sex", sex);
            }
            postcode = PostInput.text;
            PlayerPrefs.SetString("PostNumber", PostInput.text);
            PlayerPrefs.Save();
            Restart();
        });
        PostErrorSettingShowButton.onClick.AddListener(() =>
        {
            SettingPanel.SetActive(true);
            isSetting = true;
            postcode = PlayerPrefs.GetString("PostNumber");
            sex = PlayerPrefs.GetString("Sex");
            PostInput.text = postcode;
            if (sex == "m") {
                SettingSexCheckM.isOn = true;
                SettingSexCheckW.isOn = false;
            } else {
                SettingSexCheckM.isOn = false;
                SettingSexCheckW.isOn = true;
            }

        });
        Restart();
    }
    void Restart()
    {
        loadingBar.UpdateColor(Color.green);
        LoadingPanel.SetActive(true);
        GetWeather = true;

        TodayAPIKey = "XXXXXXXX";
        TomorrowAPIKey = "XXXXXXXX";
        YahooAPIKey = "XXXXXXXX";

        Rain = false;
        VariableReset();

        SettingPanel.SetActive(false);

        sex = PlayerPrefs.GetString("Sex", "Error");
        postcode = PlayerPrefs.GetString("PostNumber", "None");
        if (postcode == "None") {
            GetWeather = false;
            isSetting = false;
            isStartPanel = true;
            StartPanel.SetActive(true);
        }

        if (Application.internetReachability == NetworkReachability.NotReachable) {
            GetWeather = false;
            LoadingText.text = "インターネット接続を確認してください。(エラー 400)";
            loadingBar.UpdateBar(100f, 100f);
            loadingBar.UpdateColor(new Color(1f, 94f / 255f, 87f / 255f));
        }

        if (GetWeather) StartCoroutine(CoStart());

    }

    IEnumerator CoStart()
    {
        loadingBar.UpdateBar(0f, 100f);
        isSetting = false;
        PostErrorSettingShowButton.gameObject.SetActive(false);
        WWW dl = new WWW("https://map.yahooapis.jp/search/zip/V1/zipCodeSearch?query=" + postcode + "&appid=" + YahooAPIKey + "&output=json");
        while (dl.isDone == false)
        {
            yield return null;
        }
        loadingBar.UpdateBar(5f, 100f);
        PostCheckClass poscheck = JsonUtility.FromJson<PostCheckClass>(dl.text);
        if (poscheck.ResultInfo.Total == 0) {
            LoadingText.text = "郵便番号が正しくありません。設定し直してください。(エラー 402)";
            PostErrorSettingShowButton.gameObject.SetActive(true);
            loadingBar.UpdateBar(100f, 100f);
            loadingBar.UpdateColor(new Color(1f, 94f / 255f, 87f / 255f));
        } else {
            loadingBar.UpdateBar(10f, 100f);
            PostClass post = JsonUtility.FromJson<PostClass>(dl.text);
            loadingBar.UpdateBar(15f, 100f);
            string[] coordinates = post.Feature[0].Geometry.Coordinates.Split(',');
            WWW dl_ = new WWW("http://api.openweathermap.org/data/2.5/weather?lat=" + coordinates[1] + "&lon=" + coordinates[0] + "&APPID=" + TodayAPIKey + "&units=metric");
            while (dl_.isDone == false)
            {
                yield return null;
            }
            loadingBar.UpdateBar(20f, 100f);
            WeatherClass weather = JsonUtility.FromJson<WeatherClass>(dl_.text);
            if (weather.cod == 429)
            {
                LoadingText.text = "申し訳ございません。現在使用できません。(エラー 401)";
                loadingBar.UpdateBar(100f, 100f);
                loadingBar.UpdateColor(new Color(1f, 94f / 255f, 87f / 255f));
            }
            else
            {
                loadingBar.UpdateBar(25f, 100f);
                WWW dl__ = new WWW("http://api.openweathermap.org/data/2.5/forecast?lat=" + coordinates[1] + "&lon=" + coordinates[0] + "&APPID=" + TomorrowAPIKey + "&units=metric");
                while (dl__.isDone == false)
                {
                    yield return null;
                }
                loadingBar.UpdateBar(30f, 100f);
                ForecastClass forecast = JsonUtility.FromJson<ForecastClass>(dl__.text);
                loadingBar.UpdateBar(35f, 100f);

                CultureInfo ci = new CultureInfo("ja-JP");
                DateTime Tomorrow = DateTime.Today + new TimeSpan(1, 6, 0, 0);
                loadingBar.UpdateBar(40f, 100f);

                for (int i = 0; i < forecast.list.Length; i++)
                {
                    if (forecast.list[i].dt_txt == Tomorrow.ToString("yyyy-MM-dd HH:mm:ss"))
                    {
                        ListNumber = i;
                    }
                }
                loadingBar.UpdateBar(50f, 100f);

                // 現在天気
                if (weather.weather[0].id == 800)
                { // 晴れ(800)
                    TodayWeather.texture = SunTexture;
                    CloudSystem.SetActive(false);
                }
                else if (weather.weather[0].id >= 800)
                { // 曇り(800番台)
                    ClothHave = true;
                    TodayWeather.texture = CloudTexture;
                    CloudSystem.SetActive(true);
                }
                else if ((weather.weather[0].id >= 500 && weather.weather[0].id <= 531) || (weather.weather[0].id >= 300 && weather.weather[0].id <= 321))
                { // 雨(500,300番台)
                    ClothHave = true;
                    RainType = true;
                    Rain = true;
                    TodayWeather.texture = RainTexture;
                    CloudSystem.SetActive(true);
                }
                else if (weather.weather[0].id >= 200 && weather.weather[0].id <= 232)
                { // 雷(200番台)
                    ClothHave = true;
                    RainType = true;
                    Rain = true;
                    TodayWeather.texture = ThunderTexture;
                    CloudSystem.SetActive(true);
                }
                else if (weather.weather[0].id >= 600 && weather.weather[0].id <= 622)
                { // 雪(600番台)
                    ClothHave = true;
                    RainType = false;
                    Rain = true;
                    TodayWeather.texture = SnowTexture;
                    CloudSystem.SetActive(true);
                }
                else if (weather.weather[0].id == 781)
                { // 台風(781)
                    ClothHave = true;
                    RainType = true;
                    Rain = true;
                    TodayWeather.texture = TyphoonTexture;
                    CloudSystem.SetActive(true);
                }
                else if (weather.weather[0].id >= 700 && weather.weather[0].id <= 780)
                { // 大気(700番台)
                    ClothHave = true;
                    TodayWeather.texture = MistTexture;
                    CloudSystem.SetActive(true);
                }
                loadingBar.UpdateBar(55f, 100f);

                if (forecast.list[ListNumber].weather[0].id == 800)
                { // 晴れ(800)
                    TomorrowWeather.texture = SunTexture;
                }
                else if (forecast.list[ListNumber].weather[0].id >= 800)
                { // 曇り(800番台)
                    TomorrowWeather.texture = CloudTexture;
                }
                else if ((forecast.list[ListNumber].weather[0].id >= 500 && forecast.list[ListNumber].weather[0].id <= 531) || (forecast.list[ListNumber].weather[0].id >= 300 && forecast.list[ListNumber].weather[0].id <= 321))
                { // 雨(500,300番台)
                    TomorrowWeather.texture = RainTexture;
                }
                else if (forecast.list[ListNumber].weather[0].id >= 200 && forecast.list[ListNumber].weather[0].id <= 232)
                { // 雷(200番台)
                    TomorrowWeather.texture = ThunderTexture;
                }
                else if (forecast.list[ListNumber].weather[0].id >= 600 && forecast.list[ListNumber].weather[0].id <= 622)
                { // 雪(600番台)
                    TomorrowWeather.texture = SnowTexture;
                }
                else if (forecast.list[ListNumber].weather[0].id == 781)
                { // 台風(781)
                    TomorrowWeather.texture = TyphoonTexture;
                }
                else if (forecast.list[ListNumber].weather[0].id >= 700 && forecast.list[ListNumber].weather[0].id <= 780)
                { // 大気(700番台)
                    TomorrowWeather.texture = MistTexture;
                }
                loadingBar.UpdateBar(60f, 100f);

                TodayMaxTempWeather.text = Math.Floor(Convert.ToDouble(weather.main.temp_max)) + "℃";
                TodayMinTempWeather.text = Math.Floor(Convert.ToDouble(weather.main.temp_min)) + "℃";
                TomorrowTempWeather.text = Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp)) + "℃";
                TodayText.text = DateTime.Today.ToString("M/d dddd", ci);
                TomorrowText.text = Tomorrow.ToString("M/d dddd", ci);
                WeatherTitle.text = post.Feature[0].Property.Address + " の天気";
                loadingBar.UpdateBar(65f, 100f);

                // 洋服選択プログラム (今日)
                if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 50)
                {
                    VariableReset();
                    ShortSleeve.gameObject.SetActive(true);
                    Shorts.gameObject.SetActive(true);
                    WaterBottle.gameObject.SetActive(true);
                    AvaterMShortSleeve.gameObject.SetActive(true);
                    AvaterMShorts.gameObject.SetActive(true);
                    AvaterWShortSleeve.gameObject.SetActive(true);
                    AvaterWShorts.gameObject.SetActive(true);
                }
                if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 25)
                {
                    VariableReset();
                    ShortSleeve.gameObject.SetActive(true);
                    Shorts.gameObject.SetActive(true);
                    AvaterMShortSleeve.SetActive(true);
                    AvaterMShorts.SetActive(true);
                    AvaterWShortSleeve.SetActive(true);
                    AvaterWShorts.SetActive(true);
                }
                if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 23)
                {
                    if (weather.weather[0].main == "Clear")
                    {
                        VariableReset();
                        ShortSleeve.gameObject.SetActive(true);
                        Shorts.gameObject.SetActive(true);
                        AvaterMShortSleeve.SetActive(true);
                        AvaterMShorts.SetActive(true);
                        AvaterWShortSleeve.SetActive(true);
                        AvaterWShorts.SetActive(true);
                    }
                    else
                    {
                        VariableReset();
                        LongSleeve.SetActive(true);
                        Shorts.gameObject.SetActive(true);
                        AvaterMLongSleeve.SetActive(true);
                        AvaterMShorts.SetActive(true);
                        AvaterWLongSleeve.SetActive(true);
                        AvaterWShorts.SetActive(true);
                    }
                }
                if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 20)
                {
                    VariableReset();
                    LongSleeve.SetActive(true);
                    LongSleeve2.SetActive(true);
                    LongTrousers.SetActive(true);
                    AvaterMLongSleeve2.SetActive(true);
                    AvaterMLongTrousers.SetActive(true);
                    AvaterWLongSleeve2.SetActive(true);
                    AvaterWLongTrousers.SetActive(true);
                }
                if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 15)
                {
                    VariableReset();
                    LongSleeve.SetActive(true);
                    LongSleeve2.SetActive(true);
                    LongTrousers.SetActive(true);
                    Jumper.SetActive(true);
                    AvaterMLongTrousers.SetActive(true);
                    AvaterMLongSleeve2.SetActive(true);
                    AvaterMJumper.SetActive(true);
                    AvaterWLongTrousers.SetActive(true);
                    AvaterWLongSleeve2.SetActive(true);
                    AvaterWJumper.SetActive(true);
                }
                if (Math.Floor(Convert.ToDouble(weather.main.temp_max)) < 10)
                {
                    VariableReset();
                    LongSleeve.SetActive(true);
                    LongSleeve2.SetActive(true);
                    LongTrousers.SetActive(true);
                    Jumper.SetActive(true);
                    AvaterMLongTrousers.SetActive(true);
                    AvaterMJumper.SetActive(true);
                    AvaterWLongTrousers.SetActive(true);
                    AvaterWJumper.SetActive(true);
                }
                if (weather.weather[0].main == "Rain")
                {
                    Umbrella.SetActive(true);
                    Boots.SetActive(true);
                    AvaterMUmbrella.SetActive(true);
                    AvaterWUmbrella.SetActive(true);
                }
                loadingBar.UpdateBar(75f, 100f);

                // 洋服選択プログラム (明日)
                if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 50)
                {
                    VariableReset1();
                    ShortSleeve1.gameObject.SetActive(true);
                    Shorts1.gameObject.SetActive(true);
                    WaterBottle1.SetActive(true);
                    AvaterMShortSleeve1.SetActive(true);
                    AvaterMShorts1.SetActive(true);
                    AvaterWShortSleeve1.SetActive(true);
                    AvaterWShorts1.SetActive(true);
                }
                if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 26)
                {
                    VariableReset1();
                    ShortSleeve1.gameObject.SetActive(true);
                    Shorts1.gameObject.SetActive(true);
                    AvaterMShortSleeve1.SetActive(true);
                    AvaterMShorts1.SetActive(true);
                    AvaterWShortSleeve1.SetActive(true);
                    AvaterWShorts1.SetActive(true);
                }
                if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 23)
                {
                    if (forecast.list[ListNumber].weather[0].main == "Clear")
                    {
                        VariableReset1();
                        ShortSleeve1.gameObject.SetActive(true);
                        Shorts1.gameObject.SetActive(true);
                        AvaterMShortSleeve1.SetActive(true);
                        AvaterMShorts1.SetActive(true);
                        AvaterWShortSleeve1.SetActive(true);
                        AvaterWShorts1.SetActive(true);
                    }
                    else
                    {
                        VariableReset1();
                        LongSleeve1.SetActive(true);
                        Shorts1.gameObject.SetActive(true);
                        AvaterMLongSleeve1.SetActive(true);
                        AvaterMShorts1.SetActive(true);
                        AvaterWLongSleeve1.SetActive(true);
                        AvaterWShorts1.SetActive(true);
                    }
                }
                if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 20)
                {
                    VariableReset1();
                    LongSleeve1.SetActive(true);
                    LongSleeve21.SetActive(true);
                    LongTrousers1.SetActive(true);
                    AvaterMLongSleeve21.SetActive(true);
                    AvaterMLongTrousers1.SetActive(true);
                    AvaterWLongSleeve21.SetActive(true);
                    AvaterWLongTrousers1.SetActive(true);
                }
                if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 15)
                {
                    VariableReset1();
                    LongSleeve1.SetActive(true);
                    LongSleeve21.SetActive(true);
                    LongTrousers1.SetActive(true);
                    Jumper1.SetActive(true);
                    AvaterMLongTrousers1.SetActive(true);
                    AvaterMLongSleeve21.SetActive(true);
                    AvaterMJumper1.SetActive(true);
                    AvaterWLongTrousers1.SetActive(true);
                    AvaterWLongSleeve21.SetActive(true);
                    AvaterWJumper1.SetActive(true);
                }
                if (Math.Floor(Convert.ToDouble(forecast.list[ListNumber].main.temp_max)) < 10)
                {
                    VariableReset1();
                    LongSleeve1.SetActive(true);
                    LongSleeve21.SetActive(true);
                    LongTrousers1.SetActive(true);
                    Jumper1.SetActive(true);
                    AvaterMLongTrousers1.SetActive(true);
                    AvaterMJumper1.SetActive(true);
                    AvaterWLongTrousers1.SetActive(true);
                    AvaterWJumper1.SetActive(true);
                }
                if (forecast.list[ListNumber].weather[0].main == "Rain")
                {
                    Umbrella1.SetActive(true);
                    Boots1.SetActive(true);
                    AvaterMUmbrella1.SetActive(true);
                    AvaterWUmbrella1.SetActive(true);
                }
                loadingBar.UpdateBar(85f, 100f);

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
                loadingBar.UpdateBar(90f, 100f);

                if (DateTime.Now.Hour < 24)
                { // 夜中
                    DirectionalLight.transform.rotation = Quaternion.Euler(185f, 0f, 0f);
                }
                if (DateTime.Now.Hour < 16) { // 夕方
                    DirectionalLight.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
                }
                if (DateTime.Now.Hour < 15)
                { // 昼
                    DirectionalLight.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                }
                if (DateTime.Now.Hour < 10)
                { // 朝
                    DirectionalLight.transform.rotation = Quaternion.Euler(15f, 0f, 0f);
                }
                if (DateTime.Now.Hour < 5)
                { // 夜中
                    DirectionalLight.transform.rotation = Quaternion.Euler(-5, 0f, 0f);
                }
                if (PlayerPrefs.GetString("Sex") == "m")
                {
                    ShortSleeve.texture = ShortSleeveM;
                    ShortSleeve1.texture = ShortSleeveM;
                    Shorts.texture = ShortsM;
                    Shorts1.texture = ShortsM;
                    AvaterM.SetActive(true);
                    AvaterM1.SetActive(true);
                }
                else
                {
                    ShortSleeve.texture = ShortSleeveW;
                    ShortSleeve1.texture = ShortSleeveW;
                    Shorts.texture = ShortsW;
                    Shorts1.texture = ShortsW;
                    AvaterW.SetActive(true);
                    AvaterW1.SetActive(true);
                }
                loadingBar.UpdateBar(100f, 100f);
                LoadingPanel.SetActive(false);
            }
        }

    }
    // Update is called once per frame
    void Update() {
        if (isStartPanel == false) {
            if (sex == "m") {
                SexCheckM.isOn = true;
                SexCheckW.isOn = false;
                AvaterM.SetActive(true);
                AvaterM1.SetActive(true);
                AvaterW.SetActive(false);
                AvaterW1.SetActive(false);
            } else if (sex == "w") {
                SexCheckM.isOn = false;
                SexCheckW.isOn = true;
                AvaterM.SetActive(false);
                AvaterM1.SetActive(false);
                AvaterW.SetActive(true);
                AvaterW1.SetActive(true);
            }
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
    void VariableReset(){
        Shorts.gameObject.SetActive(false);
        ShortSleeve.gameObject.SetActive(false);
        LongSleeve.gameObject.SetActive(false);
        LongSleeve2.gameObject.SetActive(false);
        LongTrousers.gameObject.SetActive(false);
        Boots.gameObject.SetActive(false);
        Umbrella.gameObject.SetActive(false);
        WaterBottle.gameObject.SetActive(false);
        AvaterMShortSleeve.gameObject.SetActive(false);
        AvaterMShorts.gameObject.SetActive(false);
        AvaterMLongSleeve.gameObject.SetActive(false);
        AvaterMLongSleeve2.gameObject.SetActive(false);
        AvaterMLongTrousers.gameObject.SetActive(false);
        AvaterMUmbrella.gameObject.SetActive(false);
        AvaterMJumper.gameObject.SetActive(false);
        AvaterWShortSleeve.gameObject.SetActive(false);
        AvaterWShorts.gameObject.SetActive(false);
        AvaterWLongSleeve.gameObject.SetActive(false);
        AvaterWLongSleeve2.gameObject.SetActive(false);
        AvaterWLongTrousers.gameObject.SetActive(false);
        AvaterWUmbrella.gameObject.SetActive(false);
        AvaterWJumper.gameObject.SetActive(false);
    }
    void VariableReset1() {
        Shorts1.gameObject.SetActive(false);
        ShortSleeve1.gameObject.SetActive(false);
        LongSleeve1.gameObject.SetActive(false);
        LongSleeve21.gameObject.SetActive(false);
        LongTrousers1.gameObject.SetActive(false);
        Boots1.gameObject.SetActive(false);
        Umbrella1.gameObject.SetActive(false);
        WaterBottle1.gameObject.SetActive(false);
        AvaterMShortSleeve1.gameObject.SetActive(false);
        AvaterMShorts1.gameObject.SetActive(false);
        AvaterMLongSleeve1.gameObject.SetActive(false);
        AvaterMLongSleeve21.gameObject.SetActive(false);
        AvaterMLongTrousers1.gameObject.SetActive(false);
        AvaterMUmbrella1.gameObject.SetActive(false);
        AvaterMJumper1.gameObject.SetActive(false);
        AvaterWShortSleeve1.gameObject.SetActive(false);
        AvaterWShorts1.gameObject.SetActive(false);
        AvaterWLongSleeve1.gameObject.SetActive(false);
        AvaterWLongSleeve21.gameObject.SetActive(false);
        AvaterWLongTrousers1.gameObject.SetActive(false);
        AvaterWUmbrella1.gameObject.SetActive(false);
        AvaterWJumper1.gameObject.SetActive(false);
    }
}
