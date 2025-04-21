namespace Character;

using System;

// 캐릭터 클래스 정의
class Character
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

    // 생성자
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

class Program
{
    static Character player;

    static void Main(string[] args)
    {
        Console.WriteLine("캐릭터 이름을 입력하세요:");
        string name = Console.ReadLine();

        Console.WriteLine("직업을 선택하세요 (전사 / 마법사 / 도적):");
        string job = Console.ReadLine();

        player = new Character(name, job);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== 메인 메뉴 ====");
            Console.WriteLine("1. 캐릭터 상태 보기");
            Console.WriteLine("2. 종료");
            Console.Write("선택: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    player.ShowStatus();
                    break;
                case "2":
                    Console.WriteLine("게임을 종료합니다.");
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 눌러 재시작.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
