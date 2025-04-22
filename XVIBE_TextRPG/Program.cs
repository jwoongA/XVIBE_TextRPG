namespace XVIBE_TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logo.LogoPrint(); // 로고 출력
            PlayerSettings.InitializePlayer();

            MainMenu.ShowMainMenu(); // 메인 메뉴 표시
        }
    }
}
