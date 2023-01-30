using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{  
    public static int score = 0;
    public static int coinsCollected = 0;
    public static int level = 1;
    public static int lives = 10;
    public static int stars = 0;
    public static GameManager _instance;
    public static GameObject selectedObject;
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }
    public List<IPaddle> activePaddles;
    private Color red_color;
	 private Color blue_color;
    private Color yellow_color;
    public LevelManager levelManager;
    
    public static int crushedBricks = 0;

    public static Dictionary<int, float> LevelStarMapping = new Dictionary<int, float>();
    private void Awake()
{
        _instance = this;
        activePaddles = new List<IPaddle>();
        DontDestroyOnLoad(gameObject);
        red_color = (Color) (Color32) new Color(149f/255f,73f/255f,62f/255f,1);
        blue_color = (Color) (Color32) new Color(60f/255f,75f/255f,161f/255f,1);
        yellow_color = (Color) (Color32) new Color(178f/255f,150f/255f,53f/255f,1);
        
       SceneManager.sceneLoaded += OnLevelLoaded;
    } 
    private void Start() {
        NewGame(); 
    }

    private void Update() {
        LevelManager.timeLeft -= Time.deltaTime;
    }
    public void SetBallColor(Color color){
        ball.SetColor(color);
    }
    private void NewGame() {
        // this.score = 0;
        if(level == 1){
            AnalyticsManager.instance.process_analytics_four(0);
        }
        UnregisterPaddles();
		score = 0;
        lives = 10;
        stars = 0;
        crushedBricks = 0;
        
        foreach(var entry in LevelManager.levelMapping){
            LevelStarMapping[entry.Value] = 0f;
            Debug.Log(LevelStarMapping[1]);
        }
        
        SceneManager.LoadScene("Home");
		//LoadLevel(2);
    }

    private void LoadLevel(int level) {
        UnregisterPaddles();
        GameManager.level = level;
        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode) {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
    }

    public void Hit(Brick brick) {
		score += brick.points;
        crushedBricks += 1;
        if(LevelManager.timeLeft > 0 ){
            AnalyticsManager.instance.brick_hit(level);
        }

        if (Cleared()) 
		{
            AnalyticsManager.instance.process_analytics_one();
            AnalyticsManager.instance.process_analytics_two( lives, level);

            
            var endtime = DateTime.Now;

            AnalyticsManager.instance.process_analytics_three((int)(endtime - LevelManager.starttime).TotalSeconds, level);
            AnalyticsManager.instance.level_completed(level);
            AnalyticsManager.instance.process_analytics_four(level);

            AnalyticsManager.instance.process_analytics_five();
            AnalyticsManager.instance.process_analytics_six();

            SceneManager.LoadScene("WinScreen");
        }
    }

    private bool Cleared() {
        for (int i = 0; i < bricks.Length; i++) {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable) {
                return false;
            }
        }

        return true;
    }

    public int[] NumerOfBrickCleared() 
	{
        int[] num = new int[3];

        for (int i = 0; i < bricks.Length; i++) {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable) {
                if((Color) (Color32) bricks[i].gameObject.GetComponent<SpriteRenderer>().color == red_color){
                    num[0]+=1;
                }
                else if((Color) (Color32) bricks[i].gameObject.GetComponent<SpriteRenderer>().color == blue_color){
                    num[1]+=1;
                }else{
                    num[2]+=1;
                }
            }
        }

        return num;
    }

    public void RegisterPaddles(IPaddle paddle) {
        activePaddles.Add(paddle);
    }

    public void UnregisterPaddles() {
        activePaddles = new List<IPaddle>();
    }

    public static GameManager Instance
    {
        get
        {
            if(_instance == null) {
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }

    private void LostScreen() {
        // AnalyticsManager.instance.Send(level, 0, lives);
        // NewGame();
        UnregisterPaddles();
        
        SceneManager.LoadScene("LostScreen");
    }

    public void Miss() {
        lives--;

        if(lives == 0) {
            LostScreen();
        } 
    }
    private int SumArray(int[] toBeSummed)
 {
     int sum = 0;
     foreach (int item in toBeSummed)
     {
         sum += item;
     }
     return sum;
 }
}