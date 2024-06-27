using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caution : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer caution;

    private void FixedUpdate()
    {
        if (player == null)
            return;
        float distance = player.position.x - transform.position.x;
        
        if (distance <= 10)
        {
            distance *= 10;
            float tempDist = ((100f - distance) / 100f) * .5f;
            Color temp = caution.color;
            temp.a = tempDist;
            caution.color = temp;
        }
        else if (caution.color.a != 0f)
        {
            Color temp = caution.color;
            temp.a = 0f;
            caution.color = temp;
        }
    }
}
