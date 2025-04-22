using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace XVIBE_TextRPG
{
    

    class Select_Scene // 화면 전환
    {
        Character player; // 전달받은 캐릭터 저장용

        public Select_Scene(Character player) // 생성자에서 캐릭터 받아오기
        {
            this.player = player;
        }

        public void Scene() // 1. 게임 시작 화면
        {
            bool loop = true; // 반복

            do
            {
                Console.Clear(); // 화면 초기화

                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.\n"); // 환영 문구
                Console.WriteLine("1. 상태 보기\n2. 전투 시작\n"); // 행동 선택
                Console.Write("원하시는 행동을 입력해주세요.\n>> "); // 입력 문구

                string input = Console.ReadLine(); // 입력 받기

                switch (input)
                {
                    case "1": // 1번 누를 시
                        player.ShowStatus(); // 상태 보기 메서드 호출
                        loop = true; // 반복 종료
                                      //test1(); // 상태 보기 창으로 이동 (임의로 이름 붙임)
                        break;

                    case "2": // 2번 누를 시
                        loop = false; // 반복 종료
                                      //test2(); // 전투 시작 창으로 이동 (임의로 이름 붙임)
                        break;

                    default: // 나머지 입력
                        Message("잘못된 입력입니다 ");
                        break;
                }
            } while (loop);
        }

        public string CharacterNameScene() // 2. 캐릭터 생성 화면 
        {
            Console.Clear(); // 화면 초기화

            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("캐릭터를 생성하겠습니다. (8글자 이내)\n");


            while (true) // 제대로 된 값을 입력하기 전까지는 반복문 돌림
            {
                Console.Write("캐릭터의 이름을 입력해주세요.\n>> ");
                string input = Console.ReadLine(); // 문자열로 입력 받기

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("입력이 비어있거나 공백입니다.");
                    continue;     // 반복문 다시 돌리기 위한 continue
                }
                else if (!Regex.IsMatch(input, "^[가-힣a-zA-Z0-9]{1,8}$")) // 8글자 이내 생성 제한 조건
                {
                    Console.WriteLine("이름은 한글/영문/숫자 조합으로 1~8자 이내로 입력해주세요. (공백/특수문자 불가)");
                    continue; // 반복문 다시 돌리기 위한 continue
                }
                else
                {
                    string name = input;
                    Console.WriteLine($"입력된 이름은 {name}입니다!");
                    return name;       // Character 클래스에서 사용할 name을 반환한다!
                }
            }
        }

        public string JobSelectionScene() // 3. 직업 선택 화면
        {
            bool loop = true; // 반복

            while(true) // do - while로는 다른 값 반환이 안되어서 수정
            {
                Console.Clear();

                Console.WriteLine("직업 선택\n");
                Console.WriteLine($"(사용자 지정 닉네임)을 환영합니다."); // 추후에 {name} 넣어야함
                Console.WriteLine($"원하시는 직업을 선택해 주세요\n");

                Console.WriteLine("1. 전사\n 2. 마법사\n 3. 도적"); 
                Console.Write("원하시는 행동을 입력해주세요.\n>> "); 

                string input = Console.ReadLine(); // 입력 받기
                string job = ""; // 직업을 저장할 문자열 변수 선언

                switch (input)
                {
                    case "1": 
                        loop = false; 
                        job = "전사";    
                        return job; // Character 클래스에서 사용할 job을 반환한다!

                    case "2": 
                        loop = false;
                        job = "마법사";
                        return job; // Character 클래스에서 사용할 job을 반환한다!

                    case "3": 
                        loop = false;
                        job = "도적";
                        return job; // Character 클래스에서 사용할 job을 반환한다!

                    default: // 나머지 입력 (예외처리)
                        Message("잘못된 입력입니다 ");
                        job = "";
                        return job; // Character 클래스에서 사용할 job을 반환한다!
                }
            } 
        }
        public void StatusScene() // 4. 상태 보기 화면
        {
            bool loop = true; // 반복

            do
            {
                Console.Clear(); // 화면 초기화

                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.\n"); // 환영 문구
                Console.WriteLine("1. 상태 보기\n2. 전투 시작\n"); // 행동 선택
                Console.Write("원하시는 행동을 입력해주세요.\n>> "); // 입력 문구

                string input = Console.ReadLine(); // 입력 받기

                switch (input)
                {
                    case "1": // 1번 누를 시
                        loop = false; // 반복 종료
                                      //test1(); // 상태 보기 창으로 이동 (임의로 이름 붙임)
                        break;

                    case "2": // 2번 누를 시
                        loop = false; // 반복 종료
                                      //test2(); // 전투 시작 창으로 이동 (임의로 이름 붙임)
                        break;

                    default: // 나머지 입력
                        Message("잘못된 입력입니다 ");
                        break;
                }
            } while (loop);
        }

        static void Message(string msg) // 메시지 출력 메서드 (입력을 제대로 했는지 확인용)
        {
            bool loop = true; // 반복

            Console.WriteLine($"\n{msg}\n"); // Message() 괄호 안에 적힌 문구를 출력
            Console.WriteLine("Enter키를 눌러 다시 시도해주세요.");

            do
            {
                string input = Console.ReadLine(); // 입력

                if (string.IsNullOrEmpty(input))
                {
                    loop = false; // 루프 탈출
                    continue; // 조건문 탈출 (다음으로 넘어감)
                }
            } while (loop);
        }
    }
}
