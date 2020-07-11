using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text liveText;

    // Update is called once per frame
    void Update()
    {
        liveText.text = PlayerStats.lives.ToString() + " Lives";
    }
}
