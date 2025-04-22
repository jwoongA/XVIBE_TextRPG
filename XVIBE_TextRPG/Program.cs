namespace XVIBE_TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Logo.LogoPrint(); // 로고 출력 메서드 호출
            //Logo.Intro(); // 인트로 출력 메서드 호출
            
            // 임시로 Select Scene 인스턴스 생성
            Select_Scene setupScene = new Select_Scene(null); // 초기엔 캐릭터 없음

            string name = setupScene.CharacterNameScene(); // 캐릭터 이름 입력 받기
            string job = setupScene.JobSelectionScene(name); // 캐릭터 직업 입력 받기

            Character player = new Character(name, job); // 캐릭터 생성

            Select_Scene scene = new Select_Scene(player); // 캐릭터를 인자로 전달하여 Select_Scene 인스턴스 생성
            scene.Scene(); // 게임 시작 화면 호출
        }
    }
}
