using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pais : MonoBehaviour
{
    // cada país tem um ID
    public string nome;

    // influencia as ações dos países
    public int respeito;

    // Quantidades produzidas e gastas por cada país
    public int ouroP;
    private int ouroPinicial;
    public int ouroG;

    public int carvaoP;
    private int carvaoPinicial;
    public int carvaoG;

    public int petroleoP;
    private int petroleoPinicial;
    public int petroleoG;

    public int madeiraP;
    private int madeiraPinicial;
    public int madeiraG;

    private Renderer objectRenderer;
    private Color originalColor;
    public Color hoverColor = Color.green;

    void UpdateCountry(){
        if(defesas<defesasInicias){
            defesas+=(int)Math.Round(defesasInicias*0.05f);
            if(defesas>defesasInicias){
                defesas=defesasInicias;
            }
        }
        if(ouroP<ouroPinicial){
            ouroP+=(int)Math.Round(ouroPinicial*0.05f);
            if(ouroP>ouroPinicial){
                ouroP=ouroPinicial;
            }
        }
        if(carvaoP<carvaoPinicial){
            carvaoP+=(int)Math.Round(carvaoPinicial*0.05f);
            if(carvaoP>carvaoPinicial){
                carvaoP=carvaoPinicial;
            }
        }

        if(petroleoP<petroleoPinicial){
            petroleoP+=(int)Math.Round(petroleoPinicial*0.05f);
            if(petroleoP>petroleoPinicial){
                petroleoP=petroleoPinicial;
            }
        }

        if(madeiraP<madeiraPinicial){
            madeiraP+=(int)Math.Round(madeiraPinicial*0.05f);
            if(madeiraP>madeiraPinicial){
                madeiraP=madeiraPinicial;
            }
        }
    }

    public int defesas;

    private int defesasInicias;

    public List<Pais> vizinhos = new();

    // Start is called before the first frame update
    void Start()
    {
        defesasInicias=defesas;
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }
}