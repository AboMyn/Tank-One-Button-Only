using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public TankOneButtonController tank;

    [Header("Targets")]
    public GameObject targetPrefab;
    public int targetCount = 10;
    public Vector3 spawnCenter = new Vector3(0, 0, 0);
    public Vector3 spawnSize = new Vector3(30, 0, 30);
    public float spawnY = 0.5f;

    [Header("Moving Targets")]
    [Range(0f, 1f)] public float movingChance = 0.5f; // 50% целей двигаются
    public float movingSpeedMin = 1.5f;
    public float movingSpeedMax = 3.5f;
    public float movingRangeMin = 2f;
    public float movingRangeMax = 5f;

    [Header("Restart")]
    public float restartDelay = 3f;

    private int remainingTargets;
    private bool gameEnded;
    private string endText = "";
    private float restartTimer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        gameEnded = false;
        endText = "";
        restartTimer = 0f;

        SpawnTargets();
    }

    void SpawnTargets()
    {
        if (targetPrefab == null)
        {
            Debug.LogError("GameManager: targetPrefab не назначен!");
            return;
        }

        remainingTargets = targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            Vector3 pos = GetRandomSpawnPos();

            GameObject obj = Instantiate(targetPrefab, pos, Quaternion.identity);

            MovingTarget mt = obj.GetComponent<MovingTarget>();
            if (mt != null)
            {
                bool shouldMove = Random.value < movingChance;
                mt.move = shouldMove;

                if (shouldMove)
                {
                    mt.speed = Random.Range(movingSpeedMin, movingSpeedMax);
                    mt.range = Random.Range(movingRangeMin, movingRangeMax);

                    // Рандомно: движение либо по X, либо по Z
                    mt.direction = (Random.value < 0.5f) ? Vector3.right : Vector3.forward;
                }
            }
        }
    }

    Vector3 GetRandomSpawnPos()
    {
        float x = Random.Range(spawnCenter.x - spawnSize.x / 2f, spawnCenter.x + spawnSize.x / 2f);
        float z = Random.Range(spawnCenter.z - spawnSize.z / 2f, spawnCenter.z + spawnSize.z / 2f);
        return new Vector3(x, spawnY, z);
    }

    public void OnTargetDestroyed()
    {
        remainingTargets--;

        if (!gameEnded && remainingTargets <= 0)
        {
            EndGame("WIN");
        }
    }

    void Update()
    {
        if (tank == null) return;

        // LOSE: патроны закончились, а таргеты ещё есть
        if (!gameEnded && tank.ammo <= 0 && remainingTargets > 0)
        {
            EndGame("LOSE");
        }

        // авто-рестарт после конца игры
        if (gameEnded)
        {
            restartTimer -= Time.deltaTime;
            if (restartTimer <= 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void EndGame(string text)
    {
        gameEnded = true;
        endText = text;
        restartTimer = restartDelay;
    }

    void OnGUI()
    {
        // HUD слева сверху
        GUIStyle hud = new GUIStyle(GUI.skin.label);
        hud.fontSize = 22;

        // Тень + текст (чтобы было читаемо)
        DrawShadowLabel(new Rect(15, 15, 300, 30), "Ammo: " + tank.ammo, hud);

        // Можно оставить, если хочешь видеть прогресс
        DrawShadowLabel(new Rect(15, 45, 300, 30), "Targets left: " + remainingTargets, hud);

        // Концовка по центру
        if (gameEnded)
        {
            GUIStyle big = new GUIStyle(GUI.skin.label);
            big.fontSize = 60;
            big.alignment = TextAnchor.MiddleCenter;

            DrawShadowLabel(new Rect(0, Screen.height / 2 - 60, Screen.width, 80), endText, big);
        }
    }

    void DrawShadowLabel(Rect rect, string text, GUIStyle style)
    {
        Color old = GUI.color;

        GUI.color = Color.black;
        GUI.Label(new Rect(rect.x + 2, rect.y + 2, rect.width, rect.height), text, style);

        GUI.color = Color.white;
        GUI.Label(rect, text, style);

        GUI.color = old;
    }
}