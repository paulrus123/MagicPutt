using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Referenced from: https://unity3d.college/2016/04/11/baseball-bat-physics-unity/
 */

public class ClubHeadSpawner : MonoBehaviour
{
    [SerializeField]
    ClubHeadFollower _followerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var follower = Instantiate(_followerPrefab);
        follower.transform.position = transform.position;
        follower.SetTarget(this);
    }
}
