using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uniao : MonoBehaviour
{
    public List<Pais> paises;
    private int reservaOuro;
    private int reservaCarvao;
    private int reservaPetroleo;
    private int reservaMadeira;

    private void CalcResOuro(){
        for (int i = 0; i < paises.count; i++){
            reservaOuro = reservaOuro + paises[i].ouroP - paises[i].ouroG;
        }
    }

    private void CalcResCarvao(){
        for (int i = 0; i < paises.count; i++){
            reservaCarvao = reservaCarvao + paises[i].carvaoP - paises[i].carvaoG;
        }
    }

    private void CalcResPetroleo(){
        for (int i = 0; i < paises.count; i++){
            reservaPetroleo = reservaPetroleo + paises[i].petroleoP - paises[i].petroleoG;
        }
    }

    private void CalcResMadeira(){
        for (int i = 0; i < paises.count; i++){
            reservaMadeira = reservaMadeira + paises[i].madeiraP - paises[i].madeiraG;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
