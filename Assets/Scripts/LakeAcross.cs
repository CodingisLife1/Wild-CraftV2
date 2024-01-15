using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeAcross : MonoBehaviour
{
    [SerializeField] private GameObject questBoar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.questManager.activeQuests_bool[2])
        {
            questBoar.SetActive(true);
            GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[5];
            GameManager.instance.questManager.dialogWindow.SetActive(true);
            GameManager.instance.questManager.FinishQuest(2, true);

        }
    }
}
