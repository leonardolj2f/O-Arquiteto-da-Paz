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

    public Renderer objectRenderer;
    public Color originalColor;
    private Color hoverColor;

    public UiManager uiManager;
    public AudioSource audioSource;

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
        hoverColor = Color.yellow;
        defesasInicias=defesas;
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
        respeito = UnityEngine.Random.Range(35,62);
        madeiraP = (int)Math.Round(UnityEngine.Random.Range(0,8)/0.00001f);
        madeiraG = (int)Math.Round(UnityEngine.Random.Range(0,5)/0.00001f);
        madeiraPinicial = madeiraP;
        carvaoP = (int)Math.Round(UnityEngine.Random.Range(0,8)/0.0001f);
        carvaoG = (int)Math.Round(UnityEngine.Random.Range(0,5)/0.0001f);
        carvaoPinicial = carvaoP;
        petroleoP = (int)Math.Round(UnityEngine.Random.Range(0,8)/0.001f);
        petroleoG = (int)Math.Round(UnityEngine.Random.Range(0,5)/0.001f);
        petroleoPinicial = petroleoP;
        ouroP = UnityEngine.Random.Range(0,8)/1;
        ouroG = UnityEngine.Random.Range(0,5)/1;
        ouroPinicial = ouroP;
        defesas = (int)Math.Round(UnityEngine.Random.Range(2,8)/0.001f);
        defesasInicias = defesas;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCountry();
    }

    void OnMouseDown()
    {
        
        if (objectRenderer != null)
        {
            if(uiManager.SetHoverText(this)){
                objectRenderer.material.color = hoverColor;
                audioSource.Play();

            }
        }
    }

    public void ChangeOgColor(int c){
        if(c==0){
            originalColor = new Color(Color.green.r, Color.green.g, Color.green.b, 0.5f);
        }
        else if(c==1){
            originalColor = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
        }
    }

    public void UpdateColor(){
        objectRenderer.material.color = originalColor;
    }

    // public void ClearColor(){
    //     objectRenderer.material.color = originalColor;
    // }

    // void OnMouseUp()
    // {
    //     if (objectRenderer != null)
    //     {
    //         objectRenderer.material.color = originalColor;
    //         uiManager.ClearHoverText();
    //     }
    // }
}