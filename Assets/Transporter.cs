using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transporter : MonoBehaviour
{
    public string EscenaIr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComebackToMap()
    {
        SceneManager.LoadScene(0);
    }

    public void ToEducation()
    {
        SceneManager.LoadScene("Educativo_Scene");
    }

    public void ToInfo()
    {
        SceneManager.LoadScene("Inform�tica");
    }

    public void ToArch()
    {
        SceneManager.LoadScene("Arquitectura");
    }

    public void ToMedicine()
    {
        SceneManager.LoadScene("SalaHospital");
    }

    public void ToDemo()
    {
        SceneManager.LoadScene("Demo");
    }

    public void toScene() {
        SceneManager.LoadScene(EscenaIr);
    }

}
