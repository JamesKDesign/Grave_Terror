// Author: Elisha Anagnostakis
// Date Modified: 20/11/18
// Purpose: This script managers the two UI portraits of chunk and sizzle. When a player gets hit the portrait will 
// change which shows the state at which the player is at

using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    // references to the canvas
    [HideInInspector]
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
        // checks if the health is less than 3 to then change the UV of the portrait to snap to the more hurt image
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
        // checks if the health is less than 3 to then change the UV of the portrait to snap to the more hurt image
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