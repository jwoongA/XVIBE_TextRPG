using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    public enum QuestStatus { NotAccepted, InProgress, Completed } // 퀘스트 상태를 정의하는 열거형, 퀘스트 상태를 표시하는데 사용함
    public class Quest
    {
        public int Id;
        public string QuestName;
        public string Description;
        public QuestStatus Status;
        public int RequiredKillCount; // 필요 몬스터 처치 수 (조건)
        public int CurrentKillCount; // 현재 처치한 몬스터
        public int RewardGold;
        public int RewardExp;
        public bool IsRepeatable;
        public bool IsCompleted => CurrentKillCount >= RequiredKillCount; // IsCompleted는 현재 잡은 몬스터가 목표 몬스터보다 크거나 같을때 true를 반환한다는 람다식

        public void ResetIfRepeatable()
        {
            if (IsRepeatable && Status == QuestStatus.Completed) // 반복가능하고 완료한 퀘스트의 경우에는
            {
                Status = QuestStatus.NotAccepted; // 퀘스트의 상태를 미수락 상태로 리셋한다
                CurrentKillCount = 0; // 현재 처치한 몬스터도 초기화한다
            }
        }

    }
}
