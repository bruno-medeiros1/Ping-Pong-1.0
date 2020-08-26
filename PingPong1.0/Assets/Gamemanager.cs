using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public int EndGame;//variavel que define a que score vai acabar o jogo
    public AudioManager audio_Manager;
    public Bola bola;
    public Player player;
    public Canvas Score;
    public ParticleSystem Fx;

    /*O public permite-nos que seja acessível em qualquer outro script.
     O static permite-nos aceder a esta informação sem sequer ter uma referência ao GameManager.*/
    public static Vector2 bottomLeft;
    public static Vector2 topRight;


    private int player1_score = 0;
    private int player2_score = 0;

    private GameObject score1_text;//referencia à child do gameobject Canvas, neste caso ao texto do score do player 1
    private GameObject score2_text;
    private GameObject match_txt;
    private GameObject wintxt;
    public GameObject speedballtxt;

    private GameObject button_play;
    private GameObject button_quit;
    
    
    void Start()
    {
        //converte o pixel 0,0 para uma posição do mundo.
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));//lado esquerdo do limiar da camera
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));//lado direito do limiar da camera
        Instantiate(bola);
        
        /*Inicializa dois objectos do tipo Player*/
        Player player_1 = Instantiate(player) as Player; 
        Player player_2 = Instantiate(player) as Player;
        player_2.GetComponent<SpriteRenderer>().color = new Color(0.388235229f, 1f, 1f);//Alteramos a cor do player 2

        player_1.Init(true);//o player 1 vai estar à direita
        player_2.Init(false);//Enquanto que o player dois vai dar spawn na esquerda

        Canvas _score = Instantiate(Score) as Canvas;
        /*Inicializamos os gameobjects do prefab do Menu(Canvas) para depois podermos aceder ao seu estado*/
        score1_text = _score.transform.Find("Score1").gameObject;
        score2_text = _score.transform.Find("Score2").gameObject;
        match_txt = _score.transform.Find("MatchPoint").gameObject;
        wintxt = _score.transform.Find("Win").gameObject;
        button_play = _score.transform.Find("PlayButton").gameObject;
        button_quit = _score.transform.Find("QuitButton").gameObject;
        speedballtxt = _score.transform.Find("SpeedBall").gameObject;
        
        
        button_play.SetActive(false);
        button_quit.SetActive(false);
        match_txt.SetActive(false);/*Texto do match point e win e botao começam desativados*/
        wintxt.SetActive(false);

        
        AudioManager audio = Instantiate(audio_Manager) as AudioManager;
    }
    public void PlayFx(Transform pos)
    {
        
        Instantiate(Fx, pos);
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.yellow, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        var col = Fx.colorOverLifetime;
        col.color = grad;
    }
    public void Player1_Score()
    {
        
        player1_score++;
        score1_text.GetComponent<Text>().text = player1_score.ToString();
        if(player1_score == EndGame)
        {
            //Debug.Log("AZUL WIN");
            wintxt.GetComponent<Text>().text = "BLUE WIN";
            PlayFx(transform);
            wintxt.GetComponent<Text>().color = new Color(0.32f, 1f, 1f);

            End();
        }
        if (player1_score == EndGame - 1)
        {
            FindObjectOfType<Gamemanager>().ShowMatchPoint();
        }
    }
    public void Player2_Score()
    {
        player2_score++;
        score2_text.GetComponent<Text>().text = player2_score.ToString();
        if (player2_score == EndGame)
        {
            //Debug.Log("LARANJA WIN");
            wintxt.GetComponent<Text>().text = "ORANGE WIN";
            PlayFx(transform);
            wintxt.GetComponent<Text>().color = new Color(0.98f, 0.72f, 0.14f);
            End();

        }
        if (player2_score == EndGame-1)
        {
            FindObjectOfType<Gamemanager>().ShowMatchPoint();
        }
    }
    public void End()
    {
        FindObjectOfType<AudioManager>().EndSound("Theme");
        FindObjectOfType<AudioManager>().Play("GameOver");
        match_txt.SetActive(false);
        wintxt.SetActive(true);
        button_play.SetActive(true);
        button_quit.SetActive(true);
        FindObjectOfType<Bola>().gameObject.SetActive(false);
    }
    public void ShowMatchPoint()
    {
        match_txt.SetActive(true);
    }
}
