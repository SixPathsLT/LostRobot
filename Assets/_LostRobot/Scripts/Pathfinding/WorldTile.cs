using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile {

    public readonly Vector3 position;
    public bool canWalk;

    public WorldTile(Vector3 position, bool canWalk) {
        this.canWalk = canWalk;
        this.position = position;
    }

}
