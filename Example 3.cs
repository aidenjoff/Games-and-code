using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChatHandle : NetworkBehaviour {
    //This script handles the in game chat messages.
    //The messages are displayed in the corner of the screen for a few seconds with the users name to tell who said it and then is removed.
    //messages are also dispalyed along with system messages so telling them appart is important.

    bool TInUse; //a bool for checking key presses
    public InputField ipt; //the text box
    string send; //for the message being sent

   
    void Awake () {

        TInUse = false;
       
    }
	
    //when the message being typed is being input it is sent as the string recieve and send then copies the input.
    public void SetSend(string recieve)
    {
        send = recieve;
    }

	// Update is called once per frame
	void FixedUpdate ()
    { //Fixed updates are important since this is an online game
		
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (ipt.text != "")
                //if the input message isn't blank
                {

                CmdSend(send);
                //sends the message as a command so the server recieves the input
                
                    

                }
            else
                {
                //if it is empty do nothing
                    
                }

                //blanks the field and deactivates it.
              ipt.text = "";
              ipt.DeactivateInputField();


        }


        

        if (!ipt.isFocused && Input.GetAxisRaw("ChatType") == 1) //this is checking if the input text box is in use if it isn't and the chat button is pressed bring up the input.
            //without this if the user was to press T as they were typing it would remove their message
        {
            if (TInUse == false)
            {
                //T in use is just a check that will make the button only read the initial press. It's important since get axis raw doesn't use a get button down system and will read the input every frame
                ipt.ActivateInputField();
                
                
                Debug.Log("Ipt is active");
            }

            TInUse = true;

        }

        if (Input.GetAxisRaw("ChatType") == 0)
        {
            TInUse = false;
        }

        
    }

    [Command]
    void CmdSend(string send)
    {
        //a Command is so that the client can tell the server what it wants to be done.

        //The command sends the input string to each client so they can all display the input message
        RpcSend(send);
    }

    [ClientRpc]
    void RpcSend(string send)
    {
        //the RPC means that it is script that is to be run on all clients. It's called by the command to the server can tell all clients to run it otherwise only the machine running the code will do the change

        //the client took the gamemanagers script to call a system message.
        //the system message call was reworked to take some other inputs.
        //the First one was if the messgae was called by the system. This instance it's called by a player so it's false.
        // next is the game object that sent the messages name to display the username instead of the word SYSTEM.
        //next is something for system messages. it's sent for the nature of the message like if a player leaves or joins the online game. It doesn't matter here so it's put as 0.
        //and lastly the message to be displayed
        GameManager.instance.systemMessageCall.Invoke(false, gameObject.name, 0, send);

    }
}

