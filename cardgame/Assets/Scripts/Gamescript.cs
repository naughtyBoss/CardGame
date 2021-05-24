using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamescript : MonoBehaviour
{
    public static Gamescript s_instance = null;
    public static GameData gameData = new GameData();

    // boss相关
    public Image boss;
    public Text nextAtk;
    public Text allowHuiHeCount;
    public Text boss_restBlood;
    public Text boss_reduceBlood;
    public Text boss_dizzy;                 // boss眩晕标记
    public Transform boss_blood_front;
    public List<Transform> shield_list = new List<Transform>();

    // 玩家相关
    public Image player;
    public Text player_restBlood;
    public Text player_reduceBlood;
    public Text player_dodge;               // 玩家闪避标记
    public Text player_defense;             // 玩家防御值
    public Text giveUpCardCount;
    public Text restActionCount;
    public Transform handCardsArea;
    public Transform player_blood_front;
    public Transform dodgeArea;

    public GameObject demo_card;

    private void Awake()
    {
        s_instance = this;
    }

    void Start()
    {
        init();
        startGame();
    }

    void init()
    {
        boss_reduceBlood.text = "";
        boss_dizzy.transform.localScale = new Vector3(0, 0, 0);
        boss_restBlood.text = gameData.bossFullBlood.ToString();
        gameData.bossCurBlood = gameData.bossFullBlood;

        player_dodge.transform.localScale = new Vector3(0, 0, 0);
        player_reduceBlood.text = "";
        player_restBlood.text = gameData.playerFullBlood.ToString();
        gameData.playerCurBlood = gameData.playerFullBlood;
    }

    void startGame()
    {
        bossRound();
    }

    void setBossIsDizzy(bool isDizzy)
    {
        boss_dizzy.transform.localScale = new Vector3(isDizzy ? 1 : 0, 1, 1);

        for (int i = 0; i < shield_list.Count; i++)
        {
            shield_list[i].GetChild(0).localScale = new Vector3(isDizzy ? 1 : 0, 1, 1);
        }

        gameData.bossRestDizzyCount = isDizzy ? 2 : 0;
    }

    void bossRound()
    {
        ToastScript.show("Boss回合");

        gameData.isPlayerRound = false;

        // 检测boss眩晕是否结束
        if (gameData.bossRestDizzyCount > 0)
        {
            --gameData.bossRestDizzyCount;

            // boss眩晕结束
            if (gameData.bossRestDizzyCount <= 0)
            {
                setBossIsDizzy(false);
            }
        }

        // 检测护盾是否重生
        {
            if (gameData.getBossDefense() == 0)
            {
                for (int i = 0; i < gameData.bossShieldState.Count; i++)
                {
                    gameData.bossShieldState[i] = 2;
                }

                for (int i = 0; i < shield_list.Count; i++)
                {
                    shield_list[i].localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}
