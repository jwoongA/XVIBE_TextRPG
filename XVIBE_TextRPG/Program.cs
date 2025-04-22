namespace XVIBE_TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Logo.LogoPrint(); // 로고 출력 메서드 호출
            //Logo.Intro(); // 인트로 출력 메서드 호출
            //Select_Scene scene = new Select_Scene(); // 화면전환 인스턴스 선언

            Character character = new Character("플레이어", "마법사"); // 테스트용 캐릭터 인스턴스 생성
            Select_Scene scene = new Select_Scene(character); // 화면전환 인스턴스 생성
            scene.Scene(); // 게임 시작 화면으로 이동

        }
    }
}
