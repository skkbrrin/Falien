

using UnityEngine;
using UnityEngine.UI;


public class ResultSceneManager : MonoBehaviour
{
    public Text TimeThatPlayerSpend;


    public void ShowPlayerUseTime(float time)
    {
        TimeThatPlayerSpend.text = "Time that player spend: " + time.ToString("F2") + " seconds";
    }








    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
