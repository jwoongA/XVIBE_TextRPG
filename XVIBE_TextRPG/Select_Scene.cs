namespace Team_textrpg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Select_Scene scene = new Select_Scene();

            scene.Scene(); // 테스트 용
        }
    }

    class Select_Scene() // 화면 전환
    {
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
                        loop = false; // 반복 종료
                        test1(); // 상태 보기 창으로 이동 (임의로 이름 붙임)
                        break;

                    case "2": // 2번 누를 시
                        loop = false; // 반복 종료
                        test2(); // 전투 시작 창으로 이동 (임의로 이름 붙임)
                        break;

                    default: // 나머지 입력
                        Message("잘못된 입력입니다 ");
                        break;
                }
            } while (loop);
        }

        public void test1() // 1. 게임 시작 화면
        {
            bool loop = true; // 반복

            do
            {
                Console.Clear(); // 화면 초기화

                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.\n"); // 환영 문구
                Console.WriteLine("1. Scene\n2. test2\n"); // 행동 선택
                Console.Write("원하시는 행동을 입력해주세요.\n>> "); // 입력 문구

                string input = Console.ReadLine(); // 입력 받기

                switch (input)
                {
                    case "1": // 1번 누를 시
                        loop = false; // 반복 종료
                        Scene(); // 상태 보기 창으로 이동 (임의로 이름 붙임)
                        break;

                    case "2": // 2번 누를 시
                        loop = false; // 반복 종료
                        test2(); // 전투 시작 창으로 이동 (임의로 이름 붙임)
                        break;

                    default: // 나머지 입력
                        Message("잘못된 입력입니다 ");
                        break;
                }
            } while (loop);
        }

        public void test2() // 1. 게임 시작 화면
        {
            bool loop = true; // 반복

            do
            {
                Console.Clear(); // 화면 초기화

                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.\n"); // 환영 문구
                Console.WriteLine("1. Scene\n2. test1\n"); // 행동 선택
                Console.Write("원하시는 행동을 입력해주세요.\n>> "); // 입력 문구

                string input = Console.ReadLine(); // 입력 받기

                switch (input)
                {
                    case "1": // 1번 누를 시
                        loop = false; // 반복 종료
                        Scene(); // 상태 보기 창으로 이동 (임의로 이름 붙임)
                        break;

                    case "2": // 2번 누를 시
                        loop = false; // 반복 종료
                        test1(); // 전투 시작 창으로 이동 (임의로 이름 붙임)
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
