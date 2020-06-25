using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

    Player player;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    public void onPointerDownMoveLeft() {
        player.SetControlledByTouch(true);
        player.SetControlThrow(-1);
    }
    public void onPointerUpMoveLeft() {
        player.SetControlThrow(0);
        player.SetControlledByTouch(false);
    }
    public void onPointerDownMoveRight() {
        player.SetControlledByTouch(true);
        player.SetControlThrow(1);
    }
    public void onPointerUpMoveRight() {
        player.SetControlThrow(0);
        player.SetControlledByTouch(false);
    }
    public void Shoot() {
        player.Shoot();
    }

}
