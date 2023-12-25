using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;
    public float time;
    public float timeCounter;

    private void Awake()
    {
        time = timeCounter;
    }

    private void Update()
    {
        if(healthDelayImage.fillAmount>healthImage.fillAmount)
        {
            time-=Time.deltaTime;
            if(time>=0)
            {
                healthDelayImage.fillAmount = time/Time.deltaTime;
            }
            healthDelayImage.fillAmount-=Time.deltaTime;
        }
    }

    /// <summary>
    /// ����Health�ı���ٷֱ�
    /// </summary>
    /// <param name="persentage">�ٷֱȣ�Current/Max</param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount= persentage;
    }
}
