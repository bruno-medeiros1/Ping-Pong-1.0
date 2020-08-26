using UnityEngine;
using UnityEngine.UI;

public class Bola : MonoBehaviour
{
    Vector2 direction;
    public float speed;/*SerializeField permite-nos aceder no inspector mas a variavel permanece privada para outros scripts*/
    float radius;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.one.normalized; //Vector (1,1)
        radius = transform.position.x / 2;//o raio vai ser usado como offset
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {   
        transform.Translate(direction * speed * Time.deltaTime);//movimenta a bola numa dada direcao neste caso (1,1)
        /*Condicoes para que se a bola bata no teto ou no chao inverta o Vector */
        if ((transform.position.y > Gamemanager.topRight.y - radius) && direction.y > 0)
        {
            direction.y = -direction.y;//Invertemos o vector no y -> (1,-1)
        }
        if ((transform.position.y < Gamemanager.bottomLeft.y + radius) && direction.y < 0)
        {
            direction.y = -direction.y;
        }

        //Condicao para marcar golo com offset do radius
        if((transform.position.x < Gamemanager.bottomLeft.x + radius) && direction.x <0)
        {
            //Debug.Log("GOLO");
            FindObjectOfType<AudioManager>().Play("Score");
            FindObjectOfType<Gamemanager>().Player2_Score();          
            RestartBall();
            /*Time.timeScale = 0;
            //enabled = false;//para de atualizar o script*/
        }
        if((transform.position.x > Gamemanager.topRight.x - radius) && direction.x > 0) 
        {
            //Debug.Log("GOLO");
            FindObjectOfType<AudioManager>().Play("Score");
            FindObjectOfType<Gamemanager>().Player1_Score();           
            RestartBall();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /*Definicao da cor gradiente do trail da bola*/
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(collision.gameObject.GetComponent<SpriteRenderer>().color, 0.0f), new GradientColorKey(collision.gameObject.GetComponent<SpriteRenderer>().color, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.2f, 1.0f) });
            gameObject.GetComponent<TrailRenderer>().colorGradient = grad;


            FindObjectOfType<Gamemanager>().PlayFx(collision.gameObject.transform);//Da play no Fx na posicao da col
            FindObjectOfType<AudioManager>().IncreasePitch("Theme");
            FindObjectOfType<AudioManager>().Play("Hit");
            bool isRight = collision.GetComponent<Player>().Right;
            speed++;
            /*Se a bola se estiver a movimentar para a direita e a direcao tambem esteja invertemos*/
            if(isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;
                GetComponent<SpriteRenderer>().color = collision.GetComponent<SpriteRenderer>().color;/*Na colisao da bola com o player, a bola fica com a cor do player*/
            }
            if(isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
                GetComponent<SpriteRenderer>().color = collision.GetComponent<SpriteRenderer>().color;
            }
            
           FindObjectOfType<Gamemanager>().speedballtxt.GetComponent<Text>().text = "Ball Speed:" + speed * 4 + "km/h";//referencia
        }
    }
    
    private void RestartBall()
    {
        transform.position = Vector2.one.normalized;
        speed = 10f;
        FindObjectOfType<AudioManager>().sounds[0].source.pitch = 1;
        FindObjectOfType<Gamemanager>().speedballtxt.GetComponent<Text>().text = "Ball Speed:" + speed * 4 + "km/h";

    }
}
