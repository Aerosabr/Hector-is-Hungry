using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUp : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private TutorialInteraction interaction;
    [SerializeField] private int Step;
    private void Update()
    {
        if (!item.isDropped)
        {
            if (Step == 4)
                interaction.Step4TaskComplete();
            else if (Step == 6)
                interaction.Step6TaskComplete();
            Destroy(this);
        }
    }
}
