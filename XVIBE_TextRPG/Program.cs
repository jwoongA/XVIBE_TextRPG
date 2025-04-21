namespace XVIBE_TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Select_Scene scene = new Select_Scene(); // 화면전환 인스턴스 선언

            scene.Scene(); // 게임 시작 화면으로 이동

        }
    }
}
