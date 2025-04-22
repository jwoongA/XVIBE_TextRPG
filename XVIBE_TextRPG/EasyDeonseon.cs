using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    internal class EasyDeonseon
    {
        private List<Enemy> monsters; // 몬스터 리스트
        private List<string> battleLog; // 배틀 로그 리스트

        public EasyDeonseon()
        {
            // 몬스터 생성
            monsters = GenerateMonsters();
            battleLog = new List<string>();

            // 던전 시작
            StartDungeon();
        }

        // 몬스터 생성 메서드
        private List<Enemy> GenerateMonsters()
        {
            var random = new Random();
            var monsterList = new List<Enemy>();

            for (int i = 0; i < 4; i++)
            {
                int type = random.Next(0, 3); // 0~2 타입 랜덤 생성
                int level = random.Next(1, 6); // 1~5 레벨 랜덤 생성
                monsterList.Add(new Enemy(type, level));
            }

            return monsterList;
        }

        // 던전 시작 메서드
        private void StartDungeon()
        {
            Console.WriteLine("초급 던전에 입장했습니다!");

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
            if (target.IsDead())
            {
                Console.WriteLine($"{target.Name}은(는) 이미 쓰러졌습니다.");
                return;
            }

            int damage = Player.GetCurrentATK(); // 공버프를 고려한 현재 공격력으로 계산
            target.TakeDamage(damage);
            battleLog.Add($"플레이어가 {target.Name}에게 {damage}의 피해를 입혔습니다.");
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
                if (!monster.IsDead())
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
}