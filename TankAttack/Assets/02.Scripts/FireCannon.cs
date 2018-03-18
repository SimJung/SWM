using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour {

    //cannon 프리팹을 연결할 변수
    private GameObject cannon = null;
    //포탄 발사 사운드 파일
    private AudioClip fireSfx = null;
    //AudioSource 컴포넌트를 할당할 변수
    private AudioSource sfx = null;
    //cannon 발사 지점
    public Transform firePos;
    //PhotonView 컴포넌트를 할당할 변수
    private PhotonView pv = null;

    //MouseHover 객체를 할당할 변ㅅ
    public MouseHover mouseHover = null;

    void Awake()
    {
        //Canvas 객체에 있는 MouseHover 스크립트 할당
        mouseHover = GameObject.Find("Canvas").GetComponent<MouseHover>();

        //cannon 프리팹을 Resources 폴더에서 불러와 변수에 할당
        cannon = (GameObject)Resources.Load("cannon");
        //포탄 발사 사운드 파일을 Resources 폴더에서 불러와 변수에 할당
        fireSfx = Resources.Load<AudioClip>("CannonFire");
        //AudioSource 컴포넌트를 할당
        sfx = GetComponent<AudioSource>();
        //PhotonView 컴포넌트를 pv 변수에 할당
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update () {
        //UI 항목에 마우스 Hover가 아니고 PhotonView가 자신의 것이고 마우스 왼쪽 버튼 클릭 시 발사 로직을 수행
        if( pv.isMine && Input.GetMouseButtonDown(0))
        {
            //자신의 탱크일 경우는 로컬 함수를 호출해 포탄을 발사
            Fire();
            //원격 네트워크 플레이어의 탱크에 RPC로 원격으로 Fire 함수를 호출
            pv.RPC("Fire", PhotonTargets.Others, null);
        }
	}

    [PunRPC]
    void Fire()
    {
        //발사 사운드 발생
        sfx.PlayOneShot(fireSfx, 1.0f);
        StartCoroutine(this.CreateCannon());
    }

    IEnumerator CreateCannon()
    {
        GameObject _cannon = (GameObject)Instantiate(cannon, firePos.position, firePos.rotation);
        _cannon.GetComponent<Cannon>().playerId = pv.ownerId;
        yield return null;
    }
}
