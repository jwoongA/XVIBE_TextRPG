using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static XVIBE_TextRPG.Enemy;

namespace XVIBE_TextRPG
{
    public class Deonseon // 부모 클래스 이녀석을 자식 클래스인 초급 중급 고급 던전 클래스에 상속시킬거임
    {
        protected List<Enemy> monsters; // 몬스터 리스트
        protected List<string> battleLog; // 배틀 로그 리스트

        public virtual List<Enemy> GenerateMonsters() // 자식이 정의할 수 있도록 가상 메서드 만들기
        {
            var random = new Random();
            var monsterList = new List<Enemy>();

            for (int i = 0; i < 3; i++) // 3마리 생성
            {
                int type = random.Next(0, 3); // 0~2 타입 랜덤 생성
                int level = random.Next(1, 4); // 1~3 레벨 랜덤 생성
                monsterList.Add(new Enemy(type, level));
            }

            return monsterList;
        }

        public virtual void ShowEnterMessage() // 자식이 정의함
        {
            Console.WriteLine("초급 던전에 입장합니다...");
        }

        public void Enter()
        {
            if (monsters == null) // 몬스터 생성이 안된다고 오류나서 방어코드 작성
                monsters = GenerateMonsters();
            if (battleLog == null)
                battleLog = new List<string>();

            ShowEnterMessage();
            StartDungeon();
        }
        
        // 던전 시작 메서드
        public void StartDungeon()
        {
            if (monsters == null || monsters.Count == 0)
            {
                Console.WriteLine("에러: 몬스터가 생성되지 않았습니다.");
                return;
            }

            while (true)
            {
                // 콘솔 클리어 후 플레이어 상태 출력
                Console.Clear();
                DisplayPlayerStatus();

                // 몬스터 상태 출력
                DisplayMonsters();

                // 공격 대상 선택
                Console.WriteLine("공격할 몬스터를 선택하세요 (1-4). 0을 누르면 던전에서 후퇴합니다.");
                Console.Write(">> ");
                string targetInput = Console.ReadLine();
                if (targetInput == "0")
                {
                    Console.WriteLine("던전에서 후퇴합니다.");
                    Player.EndTurn(); // 전투 종료 시 공격력 버프 초기화
                    break;
                }

                if (!int.TryParse(targetInput, out int targetIndex) || targetIndex < 1 || targetIndex > monsters.Count)
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                    continue;
                }

                Enemy target = monsters[targetIndex - 1];

                // 행동 선택
                Console.WriteLine("행동을 선택하세요:");
                Console.WriteLine("1. 기본 공격");
                Console.WriteLine("2. 스킬 사용");
                Console.WriteLine("0. 대상 재지정");
                Console.Write(">> ");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    BasicAttack(target);
                }
                else if (action == "2")
                {
                    UseSkill(target);
                }
                else if (action == "0")
                {
                    Console.WriteLine("대상을 재지정합니다.");
                    continue;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                    continue;
                }

                // 몬스터 공격 처리
                MonsterAttack();

                // 배틀 로그 출력
                DisplayBattleLog();

                // 전투 결과 확인
                if (Player.CurrentHP <= 0 && monsters.TrueForAll(m => m.IsDead()))
                {
                    Console.WriteLine("플레이어와 몬스터가 동시에 쓰러졌습니다. 플레이어의 승리로 간주됩니다!");
                    BattleVictory();
                    Player.EndTurn(); // 전투 종료 시 공격력 초기화
                    break;
                }
                else if (Player.CurrentHP <= 0)
                {
                    BattleDefeat();
                    Player.EndTurn(); // 전투 종료 시 공격력 초기화
                    break;
                }
                else if (monsters.TrueForAll(m => m.IsDead()))
                {
                    BattleVictory();
                    break;
                }
            }
        }

        // 플레이어 상태 출력
        private void DisplayPlayerStatus()
        {
            Console.WriteLine($"플레이어 상태: HP: {Player.CurrentHP}/{Player.MaxHP}, MP: {Player.CurrentMP}/{Player.MaxMP}");
            Console.WriteLine($"직업: {Player.Job}, 레벨: {Player.Level}, 경험치: {Player.Exp}");
            Console.WriteLine($"공격력: {Player.GetCurrentATK()}, 방어력: {Player.TotalDEF}");
            Console.WriteLine(new string('-', 40)); // 구분선
        }

        // 몬스터 상태 출력
        private void DisplayMonsters()
        {
            Console.WriteLine("현재 몬스터 상태:");
            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];
                Console.WriteLine($"{i + 1}. {monster.Name} (레벨: {monster.Level}, HP: {monster.CurrentHP}/{monster.MaxHP}, DEF: {monster.DEF})");
            }
            Console.WriteLine(new string('-', 40)); // 구분선
        }

        // 기본 공격 메서드
        private void BasicAttack(Enemy target)
        {
            int damage = Player.GetCurrentATK();

            if (target.IsDead())
            {
                Console.WriteLine($"{target.Name}은(는) 이미 쓰러졌습니다.");
                return;
            }
            else if(Combat.IsMiss())
            {
                battleLog.Add($"{target.Name}을(를) 공격했지만 아무일도 일어나지 않았습니다....");
            }
            else if (Combat.IsCriticalHit()) // 조건문 걸어서 치명타 터지는 상황 아닌 상황 나누기
            {
                int criticalDamage = target.TakeCriticalDamage(damage);// 몬스터에게 치명타 데미지 피해
                battleLog.Add($"플레이어가 {target.Name}에게 {criticalDamage}의 [치명타] 피해를 입혔습니다!!!");
            }
            else
            {
                target.TakeDamage(damage); // 일반 공격
                battleLog.Add($"플레이어가 {target.Name}에게 {damage}의 피해를 입혔습니다.");
            }
        }

        // 스킬 사용 메서드
        private void UseSkill(Enemy target)
        {
            if (Player.Job == "전사")
            {
                Player.WarriorSkill();
                battleLog.Add("플레이어가 전사의 스킬을 사용하여 이번 턴 동안 공격력을 증가시켰습니다!");
            }
            else if (Player.Job == "도적")
            {
                Player.ThiefSkill(target);
                battleLog.Add($"플레이어가 도적의 스킬을 사용하여 {target.Name}에게 큰 피해를 입혔습니다!");
            }
            else if (Player.Job == "마법사")
            {
                Player.MageSkill(monsters.ToArray());
                battleLog.Add("플레이어가 마법사의 스킬을 사용하여 모든 적에게 피해를 입혔습니다!");
            }
            else
            {
                Console.WriteLine("스킬 없음");
            }
        }

        // 몬스터 공격 처리
        private void MonsterAttack()
        {
            int totalDamage = 0;

            foreach (var monster in monsters)
            {
                if(monster.IsDead())
                {
                    continue;   // 몬스터가 죽으면 그냥 진행해라
                }
                else if(Combat.IsMiss()) // 10퍼센트 확률로 회피하는 경우 회피 판정해라
                {
                    battleLog.Add($"플레이어가 {monster.Name}의 공격을 [회피]했습니다!");
                }
                else if (!monster.IsDead()) // 죽지도 않고 회피 판정도 필요없으면 데미지를 추가해라!
                {
                    totalDamage += monster.ATK;
                }
            }
            Player.CurrentHP = Math.Max(Player.CurrentHP - totalDamage, 0);
            battleLog.Add($"몬스터들이 공격하여 플레이어가 {totalDamage}의 피해를 입었습니다. 남은 HP: {Player.CurrentHP}");
        }

        // 배틀 로그 출력
        private void DisplayBattleLog()
        {
            Console.WriteLine(new string('-', 40)); // 구분선
            Console.WriteLine("배틀 로그:");
            foreach (var log in battleLog)
            {
                Console.WriteLine(log);
            }
            battleLog.Clear(); // 로그 초기화
            Console.WriteLine(new string('-', 40)); // 구분선
            Console.WriteLine("아무 키나 눌러 다음 턴으로 진행하세요...");
            Console.ReadLine();
        }

        // 전투 승리 메서드
        public void BattleVictory()
        {
            Console.WriteLine("[알림] 전투 승리 메서드는 아직 구현되지 않았습니다.");
            Console.WriteLine("아무 키나 눌러주세요.");
            Console.ReadLine();
        }

        // 전투 패배 메서드
        public void BattleDefeat()
        {
            Console.WriteLine("[알림] 전투 패배 메서드는 아직 구현되지 않았습니다.");
            Console.WriteLine("아무 키나 눌러주세요.");
            Console.ReadLine();
        }

    }

    public class EasyDeonseon : Deonseon // 초급 던전
    {
        public override void ShowEnterMessage()
        {
            Console.WriteLine("초급 던전에 입장합니다!");
        }
          
        public override List<Enemy> GenerateMonsters()
        {
            var random = new Random();
            var monsterList = new List<Enemy>();

            for (int i = 0; i < 3; i++) // 3마리 생성
            {
                int type = random.Next(0, 3); // 0~2 타입 랜덤 생성
                int level = random.Next(1, 4); // 1~3 레벨 랜덤 생성
                monsterList.Add(new Enemy(type, level));
            }

            return monsterList;
        }

        public EasyDeonseon() // 출력 메시지와 상속받은 던전 입장 메서드 실행
        {
            Enter();
        }
    }

    public class NormalDeonseon : Deonseon // 중급 던전
    {
        public override void ShowEnterMessage()
        {
            Console.WriteLine("중급 던전에 입장합니다!");
        }

        public override List<Enemy> GenerateMonsters()
        {
            var random = new Random();
            var monsterList = new List<Enemy>();

            for (int i = 0; i < 4; i++) // 4마리 생성
            {
                int type = random.Next(1, 4); // 1~3 타입 랜덤 생성
                int level = random.Next(2, 5); // 2~4 레벨 랜덤 생성
                monsterList.Add(new Enemy(type, level));
            }

            return monsterList;
        }

        public NormalDeonseon() // 출력 메시지와 상속받은 던전 입장 메서드 실행
        {
            Enter();
        }
    }

    public class HardDeonseon : Deonseon // 고급 던전
    {
        public override void ShowEnterMessage()
        {
            Console.WriteLine("고급 던전에 입장합니다!");
        }

        public override List<Enemy> GenerateMonsters()
        {
            var random = new Random();
            var monsterList = new List<Enemy>();

            for (int i = 0; i < 4; i++) // 4마리 생성
            {
                int type = random.Next(2, 5); // 2~4 타입 랜덤 생성
                int level = random.Next(1, 4); // 1~3 레벨 랜덤 생성
                monsterList.Add(new Enemy(type, level));
            }

            return monsterList;
        }

        public HardDeonseon() // 출력 메시지와 상속받은 던전 입장 메서드 실행
        {
            Enter();
        }
    }
}
