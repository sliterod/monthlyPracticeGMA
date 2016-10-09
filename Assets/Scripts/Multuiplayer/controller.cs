using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class controller : MonoBehaviour
{

    private SocketIOComponent socket;
    public GameObject player;
    public GameObject playerOnline;
    private List<GameObject> playerLIst;
    public String playerid;
    public String roomid;
    Movement characterMovement;
    Movement characterMovementOnline;
    Attack characterAttackOnline;
    Shield shieldOnline;
    bool idleToRunSet;
    bool runToIdleSet;
    private float currentPositionX;
    private float currentPositionY;
    private float currentPositionZ;

    void Start()
    {
        player = GameObject.Find("Player");
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("position", showPosition);
        socket.On("newplayer", spanNewPlayer);
        socket.On("connected", setPlayerId);
        socket.On("roomid", setRoomId);
        socket.On("playeradded", setPlayerId);
        socket.On("roomlist", showRoomList);

        socket.On("joined", playerJoined);
        characterMovement = this.GetComponent<Movement>();
        this.playerLIst = new List<GameObject>();
    }
    public void createNewRoom(SocketIOEvent e)
    {
        //this.playerid = e.data.GetField("playerid").str;
        socket.Emit("createroom");
    }
    public void createNewRoomEx(string name)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["playerid"] = this.playerid;
        data["roomname"] = name;
        socket.Emit("createroom", new JSONObject(data));
        Debug.Log("createNewRoomEx: " + new JSONObject(data));
    }
    public void setRoomId(SocketIOEvent e)
    {
        this.roomid = e.data.GetField("roomid").str;
        Debug.Log("room list received: " + e.name + " " + e.data);
    }
    public void getAllRooms()
    {
        socket.Emit("getrooms");
    }
    public void showRoomList(SocketIOEvent e)
    {
        //Debug.Log("room list received: " + e.name + " " + e.data);
        Text roomListText = GameObject.Find("multiplayerMenu/Panel/Text").GetComponent<Text>();
        roomListText.text = e.data.GetField("roomlist").str;

        JSONObject roomList = e.data.GetField("roomlist");

        int roomListSize = e.data.GetField("roomlist").Count;
        for (int i = 0; i < roomListSize; i++)
        {
            roomListText.text = roomListText.text + " " + roomList[i] + " @";
        }
        roomListText.text = roomListText.text.Replace("@", System.Environment.NewLine);
        roomListText.text = roomListText.text.Replace('"', ' ');
    }
    public void setPlayerId(SocketIOEvent e)
    {
        Debug.Log("setPlayerId: " + e.name + " " + e.data);
        this.playerid = e.data.GetField("playerid").str;
    }
    public void playerJoined(SocketIOEvent e)
    {
        Debug.Log("playerJoined: " + e.name + " " + e.data);
        this.roomid = e.data.GetField("roomid").str;
        JSONObject playerList = e.data.GetField("otherplayers");

        int playerListSize = e.data.GetField("otherplayers").Count;
        for (int i = 0; i < playerListSize; i++)
        {
            spanNewPlayerEx(playerList[i].str);
            Debug.Log("playerid: " + playerList[i].str);
        }


    }

    public void showPosition(SocketIOEvent e)
    {
        
        Dictionary<string, string> data = e.data.ToDictionary();
        foreach (GameObject player in this.playerLIst)
        {
            if (player.GetComponent<onlinePlayerController>().playerid == e.data.GetField("playerid").str)
            {
                characterMovementOnline = player.GetComponent<Movement>();
                characterAttackOnline = this.GetComponent<Attack>();
                shieldOnline = this.GetComponent<Shield>();
                //Movement left - right
                if (String.Compare(data["key"],"a")==0)
                    {
                        SetIdleToRunTrigger(player);
                    characterMovementOnline.Move(Directions.left);
                    }
                if (String.Compare(data["key"], "d") == 0)
                    {
                        Debug.Log("Going to the right");
                        SetIdleToRunTrigger(player);
                    characterMovementOnline.Move(Directions.right);
                    }
                if (String.Compare(data["key"], "stop") == 0)
                {
                        SetRunToIdleTrigger(player);
                    }

                if (String.Compare(data["key"], "s") == 0)
                {
                        Debug.Log("Cancel jump");
                    characterMovementOnline.JumpCancel();
                }
                if (String.Compare(data["key"], "jump") == 0)
                {
                    characterMovementOnline.Jump();
                }
                if (String.Compare(data["key"], "attack") == 0)
                {
                    characterAttackOnline.AttackAction();
                }

                if (String.Compare(data["key"], "attackJump") == 0)
                {
                    characterAttackOnline.AttackJump();
                }

                if (String.Compare(data["key"], "ShowBarrier") == 0)
                {
                    shieldOnline.ShowBarrier(true);
                }

                if (String.Compare(data["key"], "HideBarrier") == 0)
                {
                    shieldOnline.ShowBarrier(false);
                }

                player.transform.position = new Vector3(float.Parse(data["x"]), float.Parse(data["y"]), float.Parse(data["z"]));
            }
        }
    }
    void SetIdleToRunTrigger(GameObject player)
    {
        if (!idleToRunSet)
        {
            player.GetComponent<Animator>().SetTrigger("idleToRun");
            idleToRunSet = true;
            runToIdleSet = false;
        }
    }


    void SetRunToIdleTrigger(GameObject player)
    {
        if (!runToIdleSet)
        {
            player.GetComponent<Animator>().SetTrigger("runToIdle");
            idleToRunSet = false;
            runToIdleSet = true;
        }
    }
    public void joinRoom(String room)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["playerid"] = this.playerid;
        data["roomname"] = room;
        socket.Emit("joinroom", new JSONObject(data));
        //Debug.Log("joinRoom: " + new JSONObject(data));
    }
    void Update()
    {
        //if (this.currentPositionX != player.transform.position.x || this.currentPositionY != player.transform.position.y || this.currentPositionZ != player.transform.position.z)
        //{
        //    Dictionary<string, string> data = new Dictionary<string, string>();
        //    data["roomid"] = this.roomid;
        //    data["playerid"] = this.playerid;
        //    data["x"] = player.transform.position.x.ToString("R");
        //    data["y"] = player.transform.position.y.ToString("R");
        //    data["z"] = player.transform.position.z.ToString("R");
        //    socket.Emit("position", new JSONObject(data));
        //    this.currentPositionX = player.transform.position.x;
        //    this.currentPositionY = player.transform.position.y;
        //    this.currentPositionZ = player.transform.position.z;
        //    //Debug.Log("[SocketIO] position: " + new JSONObject(data));
        //}
        MovementCapture();
    }
    void MovementCapture()
    {
        //Movement left - right
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["roomid"] = this.roomid;
        data["playerid"] = this.playerid;
            data["x"] = player.transform.position.x.ToString("R");
            data["y"] = player.transform.position.y.ToString("R");
            data["z"] = player.transform.position.z.ToString("R");

        if (Input.GetKey(KeyCode.A))
        {
            data["key"] = "a";
        }

        if (Input.GetKey(KeyCode.D))
        {
            data["key"] = "d";
        }

        if (Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.D)
            )
        {
            data["key"] = "stop";
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            data["key"] = "s";
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            data["key"] = "jump";
        }
                if (Input.GetMouseButtonDown(0) && 
            !characterMovement.isPlayerInTheAir)
        {
            data["key"] = "attack";
        }

        if (Input.GetMouseButtonDown(0) && 
            !characterMovement.isPlayerEvading &&
            characterMovement.isPlayerInTheAir)
        {
            data["key"] = "attackJump";
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            data["key"] = "ShowBarrier";
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            data["key"] = "HideBarrier";
        }
        socket.Emit("position", new JSONObject(data));
    }
    public void spanNewPlayer(SocketIOEvent e)
    {
        //Debug.Log("spanNewPlayer: " + e.name + " " + e.data);
        GameObject newPlayerOnline = Instantiate(Resources.Load("PlayerOnline", typeof(GameObject))) as GameObject;
        newPlayerOnline.GetComponent<onlinePlayerController>().playerid = e.data.GetField("playerid").str;
        this.playerLIst.Add(newPlayerOnline);
    }
    public void spanNewPlayerEx(string playerid)
    {
        GameObject newPlayerOnline = Instantiate(Resources.Load("PlayerOnline", typeof(GameObject))) as GameObject;
        newPlayerOnline.GetComponent<onlinePlayerController>().playerid = playerid;
        this.playerLIst.Add(newPlayerOnline);
    }
}