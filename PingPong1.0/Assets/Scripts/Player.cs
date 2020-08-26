using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    float height;

    public bool Right;
    string input;
    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y; //definimos a altura
        //Debug.Log(height);
    }
    /*Funcao que inicializa o lado onde os players vai dar load*/
    public void Init(bool IsRight)
    {
        Right = IsRight;
        Vector2 pos = Vector2.zero;//Vector auxiliar que nos vai permitir guardar a posicao onde devemos spawnar o Player
        //Direita
        if (IsRight)
        {
            pos = new Vector2(Gamemanager.topRight.x, 0);//atribuimos a posicao 
            pos -= Vector2.right * transform.localScale;

            input = "Player1";
        }
        //Esquerda
        else
        {
            pos = new Vector2(Gamemanager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale;

            input = "Player2";
        }

        transform.position = pos;//Definimos a posicao do Player como sendo a da variavel pos que controla se vai dar spawn na direita ou esquerda.
    }

    void Update()
    {
        /*Player Movement*/
        float move = Input.GetAxisRaw(input) * Time.deltaTime * speed;/*Devolve valor < 0 ou > 0 */

        /*Condicoes para que o Player nao passe do limite da camera*/
        if((transform.position.y > Gamemanager.topRight.y - height /2 ) && move > 0)
        {
            move = 0;
        }
        if((transform.position.y < Gamemanager.bottomLeft.y + height / 2) && move < 0) 
        {
            move = 0;
        }
        transform.Translate(move * Vector2.up);//movimentar na vertical -> Vector2.up = (0,1)
    }
    
}
