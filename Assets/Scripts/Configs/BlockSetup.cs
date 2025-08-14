using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockSetup", menuName = "Config/BlockSetup")]
public class BlockSetup : ScriptableObject
{
    public List<BlockConfig> Blocks;
}