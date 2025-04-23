using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    public class Enemy
    {
        public string Name { get; private set; }
        public int Type { get; private set; } // 적의 타입
        public int Level { get; private set; } // 적의 레벨
        public int MaxHP { get; private set; }
        public int CurrentHP { get; private set; }
        public int ATK { get; private set; } // 공격력
        public int DEF { get; private set; } // 방어력

        public Enemy(int type, int level)
        {
            Type = type;
            Level = level;

            // 타입과 레벨에 따라 적의 속성 설정
            switch (type)
            {
                case 0: // 미니언
                    Name = "미니언";
                    MaxHP = 15 + (level * 1);
                    ATK = 5 + level;
                    DEF = 1;
                    break;
                case 1: // 대포미니언
                    Name = "대포미니언";
                    MaxHP = 15 + (level * 2);
                    ATK = 10 + level;
                    DEF = 2;
                    break;
                case 2: // 공허충
                    Name = "공허충";
                    MaxHP = 10;
                    ATK = 10 + (level * 3);
                    DEF = 0;
                    break;
                case 3: // 중급 몬스터
                    Name = "중급 몬스터";
                    MaxHP = 50 + (level * 5);
                    ATK = 15 + (level * 2);
                    DEF = 5;
                    break;
                case 4: // 고급 몬스터
                    Name = "고급 몬스터";
                    MaxHP = 100 + (level * 10);
                    ATK = 30 + (level * 3);
                    DEF = 10;
                    break;
                default:
                    Name = "알 수 없는 몬스터";//예외 처리
                    MaxHP = 10;
                    ATK = 0;
                    DEF = 0;
                    break;
            }

            CurrentHP = MaxHP; // 현재 HP는 최대 HP로 초기화
        }

        public static class Combat // 전투시 확률이 필요할때 난수 생성하는 메서드
        {
            private static Random randomNumber = new Random();

            public static bool IsCriticalHit()
            {
                return randomNumber.NextDouble() < 0.15; // 0~1미만의 실수를 무작위 생성해서 0.15보다 작으면 치명타 판정 (기본공격, 스킬공격에 적용)
            }

            public static bool IsMiss()
            {
                return randomNumber.NextDouble() < 0.10; // 0~1미만의 실수를 무작위 생성해서 0.10보다 작으면 회피 판정 ('기본공격'에만 적용)
            }
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(damage - DEF, 0); // 방어력을 고려한 데미지 계산
            CurrentHP = Math.Max(CurrentHP - actualDamage, 0); // HP는 0 이하로 내려가지 않음
            Console.WriteLine($"{Name}이(가) {actualDamage}의 피해를 입었습니다. 남은 HP: {CurrentHP}");
        }

        public int TakeCriticalDamage(int Damage)
        {
            int baseDamage = Math.Max(Damage - DEF, 0);
            int criticalDamage = (int)(baseDamage * 1.6f); // 방어력을 고려한 데미지 계산
            CurrentHP = Math.Max(CurrentHP - criticalDamage, 0); // HP는 0 이하로 내려가지 않음
            Console.WriteLine($"{Name}이(가) {criticalDamage}의 피해를 입었습니다. 남은 HP: {CurrentHP}");
            return criticalDamage;
        }

        public bool IsDead()
        {
            return CurrentHP <= 0;
        }
    }
}
