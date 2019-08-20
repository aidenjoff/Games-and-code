    

    //the car would be dissabled every time it was customied so that it could run this when it was renabled to apply changes and send them to the server
    void OnEnable()
    {
        ColourCar();
    }

    public void ColourCar()
    {
        //loading would load the saved variables from an xml file. All customisation were saved to an xml file and reloaded before making changes so that the old upgrades would be overritten
        carName = Loading().carName;

        //kicks other players out so that it will only change on the player that made the changes then have them tell the server to update everyone else.
        if (!isLocalPlayer)
            return;
   
        //the car used a caesar shift for encrypting variables.
        int.TryParse(jimbleJamble(Loading().speed), out speed);
       
        //the car uesd the defult unity car script for control, but allowed for upgrading and customising colours
        GetComponent<CarController>().m_Topspeed = speed;

        float.TryParse(jimbleJamble(Loading().torque), out torque);
        GetComponent<CarController>().m_FullTorqueOverAllWheels = torque;
        
        int.TryParse(jimbleJamble(Loading().boost), out boost);
        GetComponent<Booster>().curBoost = boost;
        GetComponent<Booster>().maxBoost = boost;

        //this region tag consisted of taking the saved R,G and B string components and combining them into a color and then sending that info to the server
        //its inside a region tag because it is mostly the same thing running over again with new variable names and made it easier to navigate the script
        #region ColourPasser
        //passer is the variable being passed through the caesar shift.
        float passer;
        
        Color newCol = new Color(0, 0, 0);

        float.TryParse(jimbleJamble(Loading().carBodyR), out passer);
        newCol.r = passer;
        float.TryParse(jimbleJamble(Loading().carBodyG), out passer);
        newCol.g = passer;
        float.TryParse(jimbleJamble(Loading().carBodyB), out passer);
        newCol.b = passer;
        CmdColourPick(0, newCol);

        float.TryParse(jimbleJamble(Loading().wheelColorFRR), out passer);
        newCol.r = passer;
        float.TryParse(jimbleJamble(Loading().wheelColorFRG), out passer);
        newCol.g = passer;
        float.TryParse(jimbleJamble(Loading().wheelColorFRB), out passer);
        newCol.b = passer;
        CmdColourPick(1, newCol);

        float.TryParse(jimbleJamble(Loading().wheelColorFLR), out passer);
        newCol.r = passer;
        float.TryParse(jimbleJamble(Loading().wheelColorFLG), out passer);
        newCol.g = passer;
        float.TryParse(jimbleJamble(Loading().wheelColorFLB), out passer);
        newCol.b = passer;
        CmdColourPick(2, newCol);

        float.TryParse(jimbleJamble(Loading().wheelColorBRR), out passer);
        newCol.r = passer;
        float.TryParse(jimbleJamble(Loading().wheelColorBRG), out passer);
        newCol.g = passer;
        float.TryParse(jimbleJamble(Loading().wheelColorBRB), out passer);
        newCol.b = passer;
        CmdColourPick(3, newCol);

        float.TryParse(jimbleJamble(Loading().wheelColorBLR), out passer);
        newCol.r = passer;
        float.TryParse(jimbleJamble(Loading().wheelColorBLG), out passer);
        newCol.g = passer;
        float.TryParse(jimbleJamble(Loading().wheelColorBLB), out passer);
        newCol.b = passer;
        CmdColourPick(4, newCol);

        float.TryParse(jimbleJamble(Loading().spoilerR), out passer);
        newCol.r = passer;
        float.TryParse(jimbleJamble(Loading().spoilerG), out passer);
        newCol.g = passer;
        float.TryParse(jimbleJamble(Loading().spoilerB), out passer);
        newCol.b = passer;
        CmdColourPick(5, newCol);

        float.TryParse(jimbleJamble(Loading().rearBumperR), out passer);
        newCol.r = passer;
        float.TryParse(jimbleJamble(Loading().rearBumperG), out passer);
        newCol.g = passer;
        float.TryParse(jimbleJamble(Loading().rearBumperB), out passer);
        newCol.b = passer;
        CmdColourPick(6, newCol);

        float.TryParse(jimbleJamble(Loading().lightCaseR), out passer);
        newCol.r = passer;
        float.TryParse(jimbleJamble(Loading().lightCaseG), out passer);
        newCol.g = passer;
        float.TryParse(jimbleJamble(Loading().lightCaseB), out passer);
        newCol.b = passer;
        CmdColourPick(7, newCol);
        #endregion

        //what the players points are after spending them on uppgrades
        float.TryParse(jimbleJamble(Loading().points), out points);

        //what the players current multiplier upgrade is
        float.TryParse(jimbleJamble(Loading().multiplier), out multiplier);
    }

    [Command]
    void CmdColourPick(int i, Color Col)
    {
        //the number sent along witht he colour would be applied on the server then sent to the clients to update
        partList[i].material.color = Col;
        RpcColourPick(i, Col);
    }

    [ClientRpc]
    void RpcColourPick(int i, Color Col)
    {
        //this would update the car for everyone in game.
        partList[i].material.color = Col;
    }