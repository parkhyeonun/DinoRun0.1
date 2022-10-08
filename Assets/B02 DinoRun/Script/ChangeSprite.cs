using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite[] sprites;
    SpriteRenderer spriter;

    // Start is called before the first frame update
    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        Change();
    }

    public void Change()
    {
        int ran = Random.Range(0, sprites.Length);
        spriter.sprite = sprites[ran];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
