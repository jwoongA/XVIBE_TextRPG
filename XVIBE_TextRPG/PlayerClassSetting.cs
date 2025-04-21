using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    internal class Character
    {
        public string Name;
        public string Job;
        public int Level;
        public int Exp;
        public int MaxHP;
        public int CurrentHP;
        public int Attack;
        public int Defense;
        public int Gold;

        // 캐릭터 기본 설정
        public Character(string name, string job)
        {
            Name = name;
            Job = job;
            Level = 1;
            Exp = 0;
            Gold = 1500;

            // 직업에 따라 초기 능력치 설정
            switch (job)
            {
                case "전사":
                    MaxHP = 100;
                    Attack = 10;
                    Defense = 5;
                    break;
                case "마법사":
                    MaxHP = 60;
                    Attack = 15;
                    Defense = 3;
                    break;
                case "도적":
                    MaxHP = 80;
                    Attack = 12;
                    Defense = 4;
                    break;
                default:
                    MaxHP = 100;
                    Attack = 10;
                    Defense = 5;
                    break;
            }

            CurrentHP = MaxHP;
        }

        // 상태 출력 메서드
        public void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("[캐릭터 상태 보기]");
            Console.WriteLine($"이름     : {Name}");
            Console.WriteLine($"직업     : {Job}");
            Console.WriteLine($"레벨     : {Level}");
            Console.WriteLine($"경험치   : {Exp}");
            Console.WriteLine($"체력     : {CurrentHP} / {MaxHP}");
            Console.WriteLine($"공격력   : {Attack}");
            Console.WriteLine($"방어력   : {Defense}");
            Console.WriteLine($"보유 골드: {Gold} G");
            Console.WriteLine("\n아무 키나 누르면 이전 화면으로 돌아갑니다.");
            Console.ReadLine();
        }
    }
}
