using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public struct NetworkMovement : INetworkInput
{
  public Vector3 direction;

  public Vector3 lookDirection;

  public bool isJumping;
}
