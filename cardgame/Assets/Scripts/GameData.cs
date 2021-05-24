using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // boss相关
    public int bossMinAtk = 8;              // 最小攻击力(可调)
    public int bossMaxAtk = 15;             // 最大攻击力(可调)
    public int bossFullBlood = 50;          // 满血数值(可调)
    public int bossCurBlood = 0;            // 当前血量
    public int nextRoundAtk = 0;            // 下一回合伤害值
    public int playerActionCount = 0;       // 下一回合玩家出牌次数
    public int bossRestDizzyCount = 0;      // 剩余眩晕回合
    public List<int> bossShieldState = new List<int>() { 2, 2, 2, 2 };       // 护盾状态

    // 玩家相关
    public int minActionCount = 3;          // 最小出牌次数(可调)
    public int maxActionCount = 8;          // 最大出牌次数(可调)
    public int playerFullBlood = 30;        // 满血数值(可调)
    public int playerCurBlood = 0;          // 当前血量
    public int restActionCount = 0;         // 剩余出牌次数
    public int defenseValue = 0;            // 防御值
    public int maxHandCardCount = 10;       // 玩家手牌最大数量(可调)
    public int cardMinAtk = 1;              // 卡牌最小攻击值(可调)
    public int cardMaxAtk = 5;              // 卡牌最大攻击值(可调)
    public int cardMinDefense = 1;          // 卡牌最小防御值(可调)
    public int cardMaxDefense = 5;          // 卡牌最大防御值(可调)
    public bool isActiveDodge = false;      // 是否激活闪避
    public bool isPlayerRound = false;      // 当前是否是玩家回合
    public int getBossDefense()
    {
        int defense = 0;
        for (int i = 0; i < bossShieldState.Count; i++)
        {
            if (bossShieldState[i] > 0)
            {
                defense += 1;
            }
        }

        return defense;
    }

    public void atkBossShield()
    {
        for (int i = 0; i < bossShieldState.Count; i++)
        {
            if (bossShieldState[i] > 0)
            {
                bossShieldState[i] -= 1;
                if (bossShieldState[i] <= 0)
                {
                    Gamescript.s_instance.shield_list[i].localScale = new Vector3(0, 0, 0);
                }
                break;
            }
        }
    }
}
