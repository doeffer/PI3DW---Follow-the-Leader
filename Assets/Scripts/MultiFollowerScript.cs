using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiFollowerScript : MonoBehaviour
{
    public Transform leader;
    public GameObject followerPrefab;
    public float initialLagSeconds = 0.5f;
    public float lagIncrement = 0.5f;
    public int numberOfFollowers = 3;

    private List<GameObject> followers = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfFollowers; i++)
        {
            GameObject follower = Instantiate(followerPrefab, leader.position, Quaternion.identity);
            FollowerScript followerScript = follower.GetComponent<FollowerScript>();
            followerScript.leader = leader;
            followerScript.lagSeconds = initialLagSeconds + (i * lagIncrement);
            followers.Add(follower);
        }
    }
}
