using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using Unity.VisualScripting;
//using UnityEditor.U2D.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, mode);
    }

    private Camera cam;

    [SerializeField]
    private float minCamSize = 0.5f;

    [SerializeField]
    private float maxCamSize = 2;

    public GameObject player;

    [SerializeField]
    private GameObject[] selectedUnits = new GameObject[8];

    [SerializeField]
    private int whichLevel = 1;

    [Header("UI Menus")]
    [SerializeField]
    private GameObject mainMenuCanvas;

    [SerializeField]
    private GameObject unitSelectCanvas;

    [SerializeField]
    private GameObject levelSelectCanvas;

    [SerializeField]
    private GameObject creditsCanvas;

    [SerializeField]
    private GameObject playingCanvas;

    [SerializeField]
    private GameObject pausedCanvas;

    [SerializeField]
    private GameObject defeatCanvas;

    [SerializeField]
    private GameObject victoryCanvas;

    [Header("Audio Manager")]
    public AudioManager audioManager;

    private enum GameState
    {
        MainMenu,
        UnitSelect,
        LevelSelect,
        Credits,
        Playing,
        Paused,
        GameOver,
        Victory
    };

    private GameState state;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Playing)
        {
            doCameraMovement();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance != this)
        {
            return;
        }

        if (scene.name == "MainMenu")
        {
            audioManager.BGMenuTheme();
            Debug.Log("main menu!");
            state = GameState.MainMenu;
            ToggleUI(mainMenuCanvas);
        }

        if (scene.name == "LevelSetup")
        {
            audioManager.BGMenuTheme();
            state = GameState.UnitSelect;
            ToggleUI(unitSelectCanvas);
        }

        if (
            scene.name == "LevelTestThomas"
            || scene.name == "LevelTestThomas 2"
            || scene.name == "FinalLevel1"
            || scene.name == "FinalLevel2"
            || scene.name == "FinalLevel3"
        )
        {
            audioManager.BGMainTheme();
            state = GameState.Playing;
            ToggleUI(playingCanvas);
            cam = Camera.main;
            player = GameObject.Find("Goblin King");
            spawnUnits();
        }
    }

    private void ToggleUI(GameObject canvas)
    {
        List<GameObject> canvases = new List<GameObject>
        {
            mainMenuCanvas,
            unitSelectCanvas,
            playingCanvas,
            pausedCanvas,
            creditsCanvas,
            levelSelectCanvas,
            defeatCanvas,
            victoryCanvas
        };

        canvas.SetActive(true);

        foreach (GameObject c in canvases)
        {
            if (c == canvas)
            {
                c.SetActive(true);
                //Debug.Log(c.name + "active state" + c.activeSelf);
            }
            else
            {
                c.SetActive(false);
                //Debug.Log(c.name + "active state" + c.activeSelf);
            }
        }
    }

    public void ToggleCredits()
    {
        if (state == GameState.Credits)
        {
            state = GameState.MainMenu;
            ToggleUI(mainMenuCanvas);
        }
        else if (state == GameState.MainMenu)
        {
            state = GameState.Credits;
            ToggleUI(creditsCanvas);
        }
    }

    public void ToggleLevelSelect()
    {
        if (state == GameState.LevelSelect)
        {
            state = GameState.MainMenu;
            ToggleUI(mainMenuCanvas);
        }
        else if (state == GameState.MainMenu)
        {
            state = GameState.LevelSelect;
            ToggleUI(levelSelectCanvas);
        }
    }

    public void TogglePause()
    {
        audioManager.ToggleDuck();

        if (state == GameState.Paused)
        {
            state = GameState.Playing;
            ToggleUI(playingCanvas);
        }
        else
        {
            state = GameState.Paused;
            //ToggleUI(pausedCanvas);
        }
    }

    public bool IsPaused()
    {
        return state == GameState.Paused;
    }

    private void getSelectedUnits()
    {
        selectedUnits = new GameObject[8];

        GameObject[] unitSlots = GameObject.FindGameObjectsWithTag("UnitSlot");

        int i = 0;
        foreach (GameObject slot in unitSlots)
        {
            if (slot.GetComponent<UnitSlot>().GetUnit() != null)
            {
                selectedUnits[i] = slot.GetComponent<UnitSlot>()
                    .GetUnit()
                    .GetComponent<DragDrop>()
                    .getPrefab();
            }

            i++;
        }
    }

    private void spawnUnits()
    {
        Vector3[] relativePositions =
        {
            new Vector3(-1, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(-1, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(-1, -1, 0),
            new Vector3(0, -1, 0),
            new Vector3(1, -1, 0)
        };

        int i = 0;
        foreach (GameObject unit in selectedUnits)
        {
            if (unit != null)
                Instantiate(
                    unit,
                    (relativePositions[i] / 2) + player.transform.position,
                    unit.transform.rotation
                );

            i++;
        }
    }

    public void loadSetup(int level)
    {
        whichLevel = level;
        //SceneManager.LoadScene("FinalLevel1");
        switch (whichLevel)
        {
            case 1:
                SceneManager.LoadScene("FinalLevel1");
                break;
            case 2:
                SceneManager.LoadScene("FinalLevel2");
                break;
            default:
                SceneManager.LoadScene("FinalLevel3");
                break;
        }
    }

    public void loadLevelFromSelect()
    {
        getSelectedUnits();

        foreach (GameObject unit in selectedUnits)
        {
            UnityEngine.Debug.Log(unit?.name);
        }

        switch (whichLevel)
        {
            case 1:
                SceneManager.LoadScene("LevelTestThomas");
                break;
            case 2:
                SceneManager.LoadScene("LevelTestThomas 2");
                break;
            default:
                SceneManager.LoadScene("LevelTestThomas");
                break;
        }
    }

    public void loadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    private void doCameraMovement()
    {
        Vector3 playerPos = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            cam.transform.position.z
        );
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cameraPos = playerPos - ((playerPos - mousePos) * 0.1f);
        cam.transform.position = cam.transform.position = new Vector3(
            cameraPos.x,
            cameraPos.y,
            cam.transform.position.z
        );

        if (Input.mouseScrollDelta != Vector2.zero)
        {
            cam.orthographicSize += -.1f * Input.mouseScrollDelta.y;

            if (cam.orthographicSize >= maxCamSize)
                cam.orthographicSize = maxCamSize;

            if (cam.orthographicSize <= minCamSize)
                cam.orthographicSize = minCamSize;
        }
    }

    public void GameOver(bool winningSon) //ie did you win the level
    {
        if (winningSon)
        {
            //Toggle Victory Screen
            ToggleUI(victoryCanvas);
            audioManager.FXVictory();
        }
        else
        {
            //Toggle Defeat Screen
            ToggleUI(defeatCanvas);
            audioManager.FXDefeat();
        }
        
    }
}
