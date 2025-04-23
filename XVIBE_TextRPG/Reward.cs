using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    public abstract class Reward // 보상 추상 클래스
    {
        public abstract void GetReward();
    }

    public class GoldReward : Reward
    {
        public int GetGold { get; }

        public GoldReward(int getGold) // 생성자
        {
            GetGold = getGold;
        }

        public override void GetReward()
        {
            Player.Gold += GetGold; // 던전 클리어 골드를 플레이어 골드에 합침
        }
    }

}
