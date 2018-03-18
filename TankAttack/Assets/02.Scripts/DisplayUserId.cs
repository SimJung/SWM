using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUserId : MonoBehaviour {


    public Text userId;
    private PhotonView pv = null;

    void Start () {
        pv = GetComponent<PhotonView>();
        if(pv.owner != null)
            userId.text = pv.owner.name;
	}
	
}
