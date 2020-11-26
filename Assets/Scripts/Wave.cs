using UnityEngine;


[System.Serializable]
// kein monobehavior, diese Klasse wird  für den inspektor gebraucht
public class Wave
{
    public GameObject enemy;
    public int amountEnemy;
    // zwischen den Enemies 
    public float rate;
    public float extraWavetime;
    public Waypoints waypoints;
}

