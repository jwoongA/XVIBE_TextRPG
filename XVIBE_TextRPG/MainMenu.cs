using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    internal class MainMenu
    {
        // Main menu class
        static public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("0. 종료하기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "4"://던전 입장 (던전 입장 이외의 메서드들은 생략)
                        Battlephase.EnterDungeon();
                        break;
                    case "0":
                        Console.WriteLine("게임을 종료합니다.");
                        return; // 프로그램 종료
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine("아무 키나 눌러주세요.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
    
    }

