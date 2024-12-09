using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowerScript : MonoBehaviour
{
    public static event Action onGameOver;
    public Transform leader;
    public float lagSeconds = 0.5f;
    private Vector3[] _positionBuffer;
    private float[] _timeBuffer;
    private int _oldestIndex;
    private int _newestIndex;
    private float recordInterval = 0.01f; // Record positions every 0.01 seconds
    private float recordTimer;

    void Start()
    {
        _positionBuffer = new Vector3[2000]; // Increase buffer size
        _timeBuffer = new float[2000]; // Increase buffer size
        _oldestIndex = 0;
        _newestIndex = 0;
        recordTimer = 0f;
    }

    void Update()
    {
        recordTimer += Time.deltaTime;
        if (recordTimer >= recordInterval)
        {
            recordTimer = 0f;

            int newIndex = (_newestIndex + 1) % _positionBuffer.Length;
            if (newIndex != _oldestIndex) 
                _newestIndex = newIndex;

            _positionBuffer[_newestIndex] = leader.position;
            _timeBuffer[_newestIndex] = Time.time;

            // Skip ahead in the buffer to the segment containing our target time.
            float targetTime = Time.time - lagSeconds;
            int nextIndex;
            while (_timeBuffer[nextIndex = (_oldestIndex + 1) % _timeBuffer.Length] < targetTime)
                _oldestIndex = nextIndex;

            // Interpolate between the two samples on either side of our target time.
            float span = _timeBuffer[nextIndex] - _timeBuffer[_oldestIndex];
            float progress = 0f;
            if (span > 0f)
            {
                progress = (targetTime - _timeBuffer[_oldestIndex]) / span;
            }

            Vector3 targetPosition = Vector3.Lerp(_positionBuffer[_oldestIndex], _positionBuffer[nextIndex], progress);

            // Set the follower's position to the interpolated target position
            transform.position = targetPosition;
        }

        // Check for game over condition
        CheckGameOver();
    }

    void CheckGameOver()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 1f) 
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        // Display game over screen
        Debug.Log("Game Over");
        onGameOver?.Invoke();
    }

    void OnDrawGizmos()
    {
        if (_positionBuffer == null || _positionBuffer.Length == 0)
            return;

        Gizmos.color = Color.grey;

        Vector3 oldPosition = _positionBuffer[_oldestIndex];
        int next;
        for (int i = _oldestIndex; i != _newestIndex; i = next)
        {
            next = (i + 1) % _positionBuffer.Length;
            Vector3 newPosition = _positionBuffer[next];
            Gizmos.DrawLine(oldPosition, newPosition);
            oldPosition = newPosition;
        }
    }

    private void OnEnable()
    {
        FollowerScript.onGameOver += DisableFollowerScript;
    }

    private void OnDisable()
    {
        FollowerScript.onGameOver -= DisableFollowerScript;
    }

    private void DisableFollowerScript()
    {
        this.enabled = false;
    }
}