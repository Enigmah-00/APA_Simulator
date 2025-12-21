using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI distannceTravelledText;
    CarHandler playerCarHandler;

    [SerializeField]
    TextMeshProUGUI gameOverText;

    [SerializeField]
    CanvasGroup gameOverCanvasGroup;



    void Awake()
    {
        playerCarHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<CarHandler>();
        playerCarHandler.OnPlayerCrashed += PlayerCarHandler_OnPlayerCrashed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverCanvasGroup.interactable = false;
        gameOverCanvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        distannceTravelledText.text = playerCarHandler.DistannceTravelled.ToString("000000"); 
    }
    IEnumerator StartGameOverAnimationCO()
    {
    yield return new WaitForSecondsRealtime(3.0f);

    gameOverCanvasGroup.interactable = true;

    while (gameOverCanvasGroup.alpha < 0.8f)
    {
        gameOverCanvasGroup.alpha = Mathf.MoveTowards(gameOverCanvasGroup.alpha, 1.0f, Time.deltaTime*2);

        yield return null;
    }
    }    
    void PlayerCarHandler_OnPlayerCrashed(CarHandler obj)
    {
        gameOverText.text = $"DISTANCE{distannceTravelledText.text}";
        StartCoroutine(StartGameOverAnimationCO());
    }
    public void OnRestartClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
