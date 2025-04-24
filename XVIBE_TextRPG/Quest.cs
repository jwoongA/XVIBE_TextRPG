using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* [작성한 것들]
 * - 퀘스트 클래스 생성 및 속성 정의
 * - 퀘스트 완료 조건 판정 로직 
 * - 반복 퀘스트의 경우 상태 리셋 함수
 * - 퀘스트 정보 및 보상 조건 정리 리스트 생성
 * 
 * [작성해야 하는 것들]
 * - 퀘스트 수락/거절 시스템
 * - 퀘스트 목록 출력 기능 (UI용)
 * - 퀘스트 상세 정보 출력 기능
 * - 퀘스트 진행 조건 자동 체크 기능
 * - 퀘스트 보상 지급 처리
 * - 퀘스트 진행 중 목록 보기 기능
 * - 플레이어 행동과 연동하여 퀘스트 조건 달성 확인 기능
 * - 퀘스트 상태 저장/로드 기능
*/

namespace XVIBE_TextRPG
{
    public enum QuestStatus { NotAccepted, InProgress, Completed } // 퀘스트 상태를 정의하는 열거형, 퀘스트 상태를 표시하는데 사용함
    public class Quest
    {
        public int Index; // 퀘스트 번호
        public string QuestName; // 퀘스트 이름
        public string Description; // 상세 설명
        public QuestStatus Status; // 퀘스트 상태 (미수락, 진행중, 완료)
        public bool IsRepeatable; // 반복 퀘스트인가?

        //조건
        public int RequiredKillCount; // 필요 몬스터 처치 수 (조건)
        public int CurrentKillCount; // 현재 처치한 몬스터
        public int Required_Level; // 필요 레벨
        public int Required_TotalAtk; // 필요 공격력
        public int Required_TotalDef; // 필요 방어력

        //보상
        public int RewardGold; // 보상 골드
        public int RewardExp; // 보상 경험치
        public Equipment.Weapon RewardWeapon; // 장비보상 무기
        public int RewardWeapon_Count; // 보상 무기 수량

        public bool IsCompleted => // IsCompleted는 퀘스트 조건을 만족하면 true를 반환한다는 람다식
            (CurrentKillCount >= RequiredKillCount) ||
            (Required_Level > 0 && Player.Level >= Required_Level) ||
            (Required_TotalAtk > 0 && Player.TotalATK >= Required_TotalAtk) ||
            (Required_TotalDef > 0 && Player.TotalDEF >= Required_TotalDef); 

        public void ResetIfRepeatable()
        {
            if (IsRepeatable && Status == QuestStatus.Completed) // 반복가능하고 완료한 퀘스트의 경우에는
            {
                Status = QuestStatus.NotAccepted; // 퀘스트의 상태를 미수락 상태로 리셋한다
                CurrentKillCount = 0; // 현재 처치한 몬스터도 초기화한다
            }
        }

    }

    public class QuestManager
    {
        static List<Quest> questList = new List<Quest>(); // 퀘스트 정보 및 보상을 정리하는 리스트

        public void QuestListDB()
        {
            questList.Add(new Quest { 
                Index = 1,
                QuestName = "마을을 위협하는 몬스터 처치",
                Description = "이봐! 마을 근처에 몬스터들이 너무 많아졌다고 생각하지 않나? \n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고! \n모험가인 자네가 좀 처치해주게!",
                RequiredKillCount = 5, // 아무 몬스터나 5마리 처치하면 퀘스트 클리어
                CurrentKillCount = 0,
                RewardGold = 300,
                RewardExp = 200,
                RewardWeapon = Shop.storeWeapons[0], // 강철검을 보상으로 준다
                RewardWeapon_Count = 1, // 강철검 하나를 보상으로 준다
                Status = QuestStatus.NotAccepted,
                IsRepeatable = true // 반복 가능한 퀘스트임
            });

            questList.Add(new Quest
            {
                Index = 2,
                QuestName = "장비를 장착해보자",
                Description = "세상에.. 자네 아직도 장비 없이 맨손으로 싸우는 건 아니겠지?? \n어서 빨리, 상점에서 장비를 구매해서 장착해보게나 \n모험은 위험하니 철저히 준비하게!",
                RewardGold = 150,
                RewardExp = 50,
                Status = QuestStatus.NotAccepted, // public static Weapon EquippedWeapon = null 요놈 활용하여 장비 장착하면 클리어
                IsRepeatable = false
            });

            questList.Add(new Quest
            {
                Index = 3,
                QuestName = "더욱 더 강해지기!",
                Description = "강한 몬스터를 상대하기 위해서 수련을 게을리 하지 말게! \n상위 장비를 장착하거나 레벨을 올려서 강해지고 오게나 \n건승을 빌겠네...!",
                Required_TotalAtk = 30, // 장비 장착 포함 플레이어의 공격력이 30이상이면 클리어 가능
                RewardGold = 500,
                RewardExp = 300,
                Status = QuestStatus.NotAccepted,
                IsRepeatable = false
            });

            questList.Add(new Quest
            {
                Index = 4,
                QuestName = "아픈건 싫으니까 방어력에 올인하고자 합니다!",
                Description = "강한 자는 단순히 때리는 자가 아니라, 맞아도 쓰러지지 않는 자일세! \n상위 장비를 장착하거나 레벨을 올려서 단단해지고 오게나 \n건승을 빌겠네...!",
                Required_TotalDef = 20, // 장비 장착 포함 플레이어의 방어력이 20이상이면 클리어 가능
                RewardGold = 700,
                RewardExp = 200,
                Status = QuestStatus.NotAccepted,
                IsRepeatable = false
            });

            questList.Add(new Quest
            {
                Index = 5,
                QuestName = "니 렙에 잠이 오냐?",
                Description = "렙 2짜리가 잠을 자? \n지금 자면 내일도 몬스터한테 맞는다. \n지금 렙업하면, 내일은 자네가 몬스터를 때린다...!!",
                Required_Level = 3, // 플레이어의 레벨이 3이상이면 클리어 가능
                RewardGold = 1000,
                RewardExp = 50,
                Status = QuestStatus.NotAccepted,
                IsRepeatable = false
            });
        }
    }
}
