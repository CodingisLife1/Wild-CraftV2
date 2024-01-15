using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UIManager uIManager;
    public Player player;
    public LootZone lootZone;
    public Transform cam_transform;
    public QuestManager questManager;
    public SpawnLootZones spawnLootZones;
    public int animalIndex;
    public Vector3 lastPos;
    public EnemySpawnSystem spawn;
    public CinemachineFreeLook cm;

    public void Awake()
    {
        instance = this;
    }
}
