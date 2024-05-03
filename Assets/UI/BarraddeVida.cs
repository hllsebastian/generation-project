using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BarraddeVida : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider=GetComponent<Slider>();
    }
    public void Cambiarvidamaxima(float Vidamaxima){
        slider.maxValue=Vidamaxima;
    }
    public void Cambiarvidaactial(float Vidaactial){
        slider.value=Vidaactial;
    }
    public void iniciarBarra(float cantidadvida){
        Cambiarvidaactial(cantidadvida);
        Cambiarvidamaxima(cantidadvida);

    }

}
