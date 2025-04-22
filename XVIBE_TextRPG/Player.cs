using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    internal class Player
    {
        public static string Name { get; set; } = "플레이어"; // 플레이어 이름

        public static string Job { get; set; } = "전사"; // 전사, 도적, 마법사 중 선택 가능

        public static int Exp { get; set; } = 0; // 플레이어 경험치

        public static int Level { get; set; } = 1; // 플레이어 레벨 = 기본레벨 1 + 경헙치 100당 레벨업

        public static int Gold { get; set; } = 1500; // 플레이어 골드

        public static int MaxHP { get; set; } = 100; // 직업에 따라 결정되도록 수정 필요
        public static int CurrentHP { get; set; } = MaxHP;
        public static int MaxMP { get; set; } = 50; // 직업에 따라 결정되도록 수정 필요
        public static int CurrentMP { get; set; } = MaxMP;
        public static int TotalATK { get; set; } = 10; // 장비와 레벨에 따라 결정되도록 수정 필요
        public static int TotalDEF { get; set; } = 5; // 장비와 레벨에 따라 결정되도록 수정 필요
        

        // 전투 턴 동안 추가 공격력
        private static int TemporaryATKBoost { get; set; } = 0;

        // 직업별 능력치 계산 메서드
        public static void UpdateStats()
        {

            switch (Job)
            {
                case "전사":
                    MaxHP = 100;
                    TotalATK = 10 + Equipment.ATKBonus + Level;
                    TotalDEF = 5 + Equipment.DEFBonus + Level / 2;
                    break;
                case "마법사":
                    MaxHP = 60;
                    TotalATK = 15 + Equipment.ATKBonus + Level;
                    TotalDEF = 3 + Equipment.DEFBonus + Level / 2;
                    break;
                case "도적":
                    MaxHP = 80;
                    TotalATK = 12 + Equipment.ATKBonus + Level;
                    TotalDEF = 4 + Equipment.DEFBonus + Level / 2;
                    break;
                default:
                    MaxHP = 100;
                    TotalATK = 10;
                    TotalDEF = 5;
                    break;
            }

            // 현재 HP와 MP가 최대값을 초과하지 않도록 조정
            CurrentHP = Math.Min(CurrentHP, MaxHP);
            CurrentMP = Math.Min(CurrentMP, MaxMP);

            Console.WriteLine($"[스탯 업데이트] 직업: {Job}, HP: {MaxHP}, ATK: {TotalATK}, DEF: {TotalDEF}");
        }

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
        public static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("[캐릭터 상태 보기]");
            Console.WriteLine($"이름     : {Name}");
            Console.WriteLine($"직업     : {Job}");
            Console.WriteLine($"레벨     : {Level}");
            Console.WriteLine($"경험치   : {Exp}");
            Console.WriteLine($"체력     : {CurrentHP} / {MaxHP}");
            Console.WriteLine($"공격력   : {TotalATK} (+{Equipment.ATKBonus+Level})");
            Console.WriteLine($"방어력   : {TotalDEF} (+{Equipment.DEFBonus+Level/2})");
            Console.WriteLine($"보유 골드: {Gold} G");
            Console.WriteLine("\n아무 키나 누르면 이전 화면으로 돌아갑니다.");
            Console.ReadLine();
        }
    }

}
