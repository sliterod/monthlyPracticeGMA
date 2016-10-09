using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using System.Collections;
using System;
using System.Collections.Generic;

public class multiplayerMenuController : MonoBehaviour
{
    private Text roomNameInput;
    private controller multiplayerController;
    void Start()
    {
        this.roomNameInput = GameObject.Find("roomNameInput/Text").GetComponent<Text>();
        this.multiplayerController = GameObject.Find("multiplayer").GetComponent<controller>();
    }
    public void createNewRoomClickEvent()
    {
        Debug.Log(this.roomNameInput.text);
        this.multiplayerController.createNewRoomEx(this.roomNameInput.text);
    }
    public void searchForRoomClickEvent()
    {
        Debug.Log(this.roomNameInput.text);
        this.multiplayerController.getAllRooms();
    }
    public void joinRoomClickEvent()
    {
        Debug.Log(this.roomNameInput.text);
        this.multiplayerController.joinRoom(this.roomNameInput.text);
    }
}
