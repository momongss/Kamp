using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_CharacterSpawn : MonoBehaviour
{
    public Character character;

    public void Spawn_Character()
    {
        Vector3 pos = transform.position;
        pos.y += 1;
        Instantiate(character, pos, transform.rotation);
    }
}
