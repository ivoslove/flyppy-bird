using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameBehaviour
{
    void Jump();
    void Score();

    void GameOver();
    /// <summary>
    /// 暂停游戏
    /// </summary>
    void Pause();


}
