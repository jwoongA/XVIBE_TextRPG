using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    internal class Player
    {
        public static int MaxHP { get; set; } = 100; // 직업에 따라 결정되도록 수정 필요
        public static int CurrentHP { get; set; } = MaxHP;
        public static int MaxMP { get; set; } = 50; // 직업에 따라 결정되도록 수정 필요
        public static int CurrentMP { get; set; } = MaxMP;
        public static int TotalATK { get; set; } = 10; // 장비와 레벨에 따라 결정되도록 수정 필요
        public static int TotalDEF { get; set; } = 5; // 장비와 레벨에 따라 결정되도록 수정 필요
        public static string ClassType { get; set; } = "전사"; // 임의로 설정한 클래스 타입

        // 전투 턴 동안 추가 공격력
        private static int TemporaryATKBoost { get; set; } = 0;

        // 전사 스킬: 전투 턴 동안 공격력 증가
        public static void WarriorSkill()
        {
            if (CurrentHP >= 6 && CurrentMP >= 20)
            {
                CurrentHP -= 5;
                CurrentMP -= 20;
                TemporaryATKBoost = 10; // 턴 동안 추가 공격력
                Console.WriteLine("전사의 분노를 사용했습니다! 이번 턴 동안 공격력이 10 증가합니다.");
            }
            else
            {
                Console.WriteLine("스킬을 사용할 수 있는 HP 또는 MP가 부족합니다.");
            }
        }

        // 현재 공격력 계산 메서드
        public static int GetCurrentATK()
        {
            return TotalATK + TemporaryATKBoost;
        }

        // 턴 종료 시 추가 공격력 초기화
        public static void EndTurn()
        {
            TemporaryATKBoost = 0;
        }

        // 도적 스킬: 단일 대상에게 1.5배 피해
        public static void ThiefSkill(Enemy target)
        {
            if (CurrentMP >= 10)
            {
                CurrentMP -= 10;
                int damage = (int)(TotalATK * 1.5);
                target.TakeDamage(damage);
                Console.WriteLine($"도적의 스킬을 사용했습니다! {target.Name}에게 {damage}의 피해를 입혔습니다.");
                Console.WriteLine($"남은 MP: {CurrentMP}");
            }
            else
            {
                Console.WriteLine("스킬을 사용할 수 있는 MP가 부족합니다.");
            }
        }

        // 마법사 스킬: 전체 대상에게 공격력만큼 피해
        public static void MageSkill(Enemy[] targets)
        {
            if (CurrentMP >= 20)
            {
                CurrentMP -= 20;
                foreach (var target in targets)
                {
                    target.TakeDamage(TotalATK);
                    Console.WriteLine($"마법사의 스킬을 사용했습니다! {target.Name}에게 {TotalATK}의 피해를 입혔습니다.");
                }
                Console.WriteLine($"남은 MP: {CurrentMP}");
            }
            else
            {
                Console.WriteLine("스킬을 사용할 수 있는 MP가 부족합니다.");
            }
        }
    }
}
