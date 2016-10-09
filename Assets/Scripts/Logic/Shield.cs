using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    public Transform barrier;

    public void ShowBarrier(bool state) {
        if (state) {
            barrier.localScale = new Vector2 (1.5f,1.5f);
        }
        else
        {
            barrier.localScale = Vector2.zero;
        }
    }
}
