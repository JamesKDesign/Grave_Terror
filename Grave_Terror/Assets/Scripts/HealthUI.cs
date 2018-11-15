using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    public float uvRectW1 = 0f;
    public RawImage sizzle;
    public RawImage chunk;
    public PlayerHealth Sizhealth;
    public PlayerHealth Chuhealth;
    private Rect rect;
    private Rect rect2;
    private float uvRectW = 0.33f;

    // Use this for initialization
    void Awake () {
        uvRectW = sizzle.uvRect.width;
        uvRectW1 = sizzle.uvRect.x;
        uvRectW = chunk.uvRect.width;
        uvRectW1 = chunk.uvRect.x;

        rect2 = chunk.uvRect;
        rect = sizzle.uvRect;
	}

    // Update is called once per frame
    void Update()
    {
        SizzleChange();
        ChunkChange();
    }

    void SizzleChange() {

        if (Sizhealth.currentHealth < 3) {
            rect.x = 0.247f;
            sizzle.uvRect = rect;
        }
        if (Sizhealth.currentHealth < 2)
        {
            rect.x = 0.493f;
            sizzle.uvRect = rect;
        }
        if (Sizhealth.currentHealth < 1) {
            rect.x = 0.493f;
            sizzle.uvRect = rect;
        }
        if (Sizhealth.currentHealth <= 0)
        {
            rect.x = 0.74f;
            sizzle.uvRect = rect;
        }
        if (Sizhealth.currentHealth >= 3) {
            rect.x = 0;
            sizzle.uvRect = rect;
        }
    }

    void ChunkChange()
    {

        if (Chuhealth.currentHealth < 3)
        {
            rect.x = 0.247f;
            chunk.uvRect = rect;
        }
        if (Chuhealth.currentHealth < 2)
        {
            rect.x = 0.493f;
            chunk.uvRect = rect;
        }
        if (Chuhealth.currentHealth < 1) {
            rect.x = 0.493f;
            chunk.uvRect = rect;
        }
        if (Chuhealth.currentHealth <= 0)
        {
            rect.x = 0.74f;
            chunk.uvRect = rect;
        }
        if (Chuhealth.currentHealth >= 3)
        {
            rect.x = 0;
            chunk.uvRect = rect;
        }
    }
}
