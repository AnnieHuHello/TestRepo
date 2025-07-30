using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class item_collector : MonoBehaviour
{
    private int kiwis = 0;

    [SerializeField] private TMP_Text kiwisText;
    [SerializeField] private AudioSource collectsoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kiwi"))
        {
            collectsoundEffect.Play();
            Destroy(collision.gameObject);
            kiwis++;
            kiwisText.text = "Kiwi: " + kiwis;
        }
    }
}
