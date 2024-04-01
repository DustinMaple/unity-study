using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public List<string> conversion;

    public void Talk()
    {
        ChatController.Inst.Show(conversion);
    }
}
