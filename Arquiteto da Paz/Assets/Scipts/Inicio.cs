using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public GameObject flags;
    public GameObject mats;
    public GameObject choices;

    public TMP_Text text;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue(){
        i++;
        if(i == 1){
            text.text = "Vais ver bandeiras e cores a representar os países da união, a verde, e os inimigos, a vermelho.";
            flags.SetActive(true);
        }
        else if(i == 2){
            text.text = "Vais ver as reservas dos materiais, no canto superior esquerdo.";
            flags.SetActive(false);
            mats.SetActive(true);
        }
        else if(i == 3){
            text.text = "Vais ver decisões a tomar no canto inferior direito. Agora que sabes tudo o necessário, podemos continuar!";
            mats.SetActive(false);
            choices.SetActive(true);
        }
        else if(i==4){
            SceneManager.LoadScene(1);
        }
    }
}
