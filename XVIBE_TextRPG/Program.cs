namespace XVIBE_TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 주석 부분 지우면 로고를 볼 수 있어요! Logo.LogoPrint(); // 로고 출력 메서드 호출
            // 주석 부분 지우면 인트로를 볼 수 있어요! Logo.Intro(); // 인트로 출력 메서드 호출
            Select_Scene scene = new Select_Scene(); // 화면전환 인스턴스 선언

            scene.Scene(); // 게임 시작 화면으로 이동

        }
    }
}
