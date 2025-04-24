using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XVIBE_TextRPG.Enemy;

namespace XVIBE_TextRPG
{
    internal class Player
    {
        public static string Name { get; set; } = "플레이어"; // 플레이어 이름

        public static string Job { get; set; } = "전사"; // 전사, 도적, 마법사 중 선택 가능

        public static int Exp { get; set; } = 0; // 플레이어 경험치

        public static int Level { get; set; } = 1; // 플레이어 레벨 = 기본레벨 1 + 경헙치 100당 레벨업

        public static int Gold { get; set; } = 1500; // 플레이어 골드

        public static int MaxHP { get; set; } = 100; // 직업에 따라 결정되도록 수정 필요
        public static int CurrentHP { get; set; } = MaxHP;
        public static int MaxMP { get; set; } = 50; // 직업에 따라 결정되도록 수정 필요
        public static int CurrentMP { get; set; } = MaxMP;

        public static float TotalATK { get; set; } = 10; // 장비와 레벨에 따라 결정되도록 수정 필요
        public static int TotalDEF { get; set; } = 5; // 장비와 레벨에 따라 결정되도록 수정 필요

        // 전투 턴 동안 추가 공격력
        private static int TemporaryATKBoost { get; set; } = 0;

        // 직업별 능력치 계산 메서드
        public static void UpdateStats()
        {

            switch (Job)
            {
                case "전사":
                    MaxHP = 100;
                    TotalATK = 10 + Equipment.ATKBonus + Level;
                    TotalDEF = 5 + Equipment.DEFBonus + Level / 2;
                    break;
                case "마법사":
                    MaxHP = 60;
                    TotalATK = 15 + Equipment.ATKBonus + Level;
                    TotalDEF = 3 + Equipment.DEFBonus + Level / 2;
                    break;
                case "도적":
                    MaxHP = 80;
                    TotalATK = 12 + Equipment.ATKBonus + Level;
                    TotalDEF = 4 + Equipment.DEFBonus + Level / 2;
                    break;
                default:
                    MaxHP = 100;
                    TotalATK = 10;
                    TotalDEF = 5;
                    break;
            }

            // 현재 HP와 MP가 최대값을 초과하지 않도록 조정
            CurrentHP = Math.Min(CurrentHP, MaxHP);
            CurrentMP = Math.Min(CurrentMP, MaxMP);

            Console.WriteLine($"[스탯 업데이트] 직업: {Job}, HP: {MaxHP}, ATK: {TotalATK}, DEF: {TotalDEF}");
        }

        // 전사 스킬: 전투 턴 동안 공격력 증가
        public static void WarriorSkill()
        {
            if (CurrentHP >= 6 && CurrentMP >= 20) // 전사 스킬로 버프걸면 치명타 계산식에 반영되어 추가 코드 생략
            {
                CurrentHP -= 5;
                CurrentMP -= 20;
                TemporaryATKBoost = 10; // 턴 동안 추가 공격력
                Console.WriteLine("전사의 분노를 사용했습니다! 이번 턴 동안 공격력이 10 증가합니다.");
            }
            else
            {
                Console.WriteLine("스킬을 사용할 수 있는 HP 또는 MP가 부족합니다.");
            }
        }

        // 현재 공격력 계산 메서드
        public static float GetCurrentATK()
        {
            return TotalATK + TemporaryATKBoost;
        }

        // 턴 종료 시 추가 공격력 초기화
        public static void EndTurn()
        {
            TemporaryATKBoost = 0;
        }

        // 도적 스킬: 단일 대상에게 1.5배 피해
        public static void ThiefSkill(Enemy target)
        {
            if (CurrentMP >= 10)
            {
                int damage = (int)(TotalATK * 1.5);
                if (Combat.IsCriticalHit())
                {
                    CurrentMP -= 10;
                    int criticalDamage = target.TakeCriticalDamage(damage); // 단일적에게 도적 스킬로 강화된 공격에 크리티컬 데미지 입힘
                    Console.WriteLine($"도적의 스킬을 사용했습니다! {target.Name}에게 {criticalDamage}의 [치명타]피해를 입혔습니다!!!");
                    Console.WriteLine($"남은 MP: {CurrentMP}");
                }
                else
                {
                    CurrentMP -= 10;
                    target.TakeDamage(damage);
                    Console.WriteLine($"도적의 스킬을 사용했습니다! {target.Name}에게 {damage}의 피해를 입혔습니다.");
                    Console.WriteLine($"남은 MP: {CurrentMP}");
                }
            }
            else
            {
                Console.WriteLine("스킬을 사용할 수 있는 MP가 부족합니다.");
            }
        }

        // 마법사 스킬: 전체 대상에게 공격력만큼 피해
        public static void MageSkill(Enemy[] targets)
        {
            if (CurrentMP >= 20)
            {
                CurrentMP -= 20;
                if (Combat.IsCriticalHit())
                {
                    foreach (var target in targets)
                    {
                        int criticalDamage = target.TakeCriticalDamage((int)TotalATK); // 마법사 스킬로 모든 적에게 강려크한 크리티컬 데미지 입힘
                        Console.WriteLine($"마법사의 스킬을 사용했습니다! {target.Name}에게 {criticalDamage}의 [치명타]피해를 입혔습니다!!!");
                    }
                }
                else
                {
                    foreach (var target in targets)
                    {
                        target.TakeDamage((int)TotalATK);
                        Console.WriteLine($"마법사의 스킬을 사용했습니다! {target.Name}에게 {TotalATK}의 피해를 입혔습니다.");
                    }
                }
                Console.WriteLine($"남은 MP: {CurrentMP}");
            }
            else
            {
                Console.WriteLine("스킬을 사용할 수 있는 MP가 부족합니다.");
            }
        }
        public static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("[캐릭터 상태 보기]");
            Console.WriteLine($"이름     : {Name}");
            Console.WriteLine($"직업     : {Job}");
            Console.WriteLine($"레벨     : {Level}");
            Console.WriteLine($"경험치   : {Exp}");
            Console.WriteLine($"체력     : {CurrentHP} / {MaxHP}");
            Console.WriteLine($"공격력   : {TotalATK} (+{Equipment.ATKBonus + Level})");
            Console.WriteLine($"방어력   : {TotalDEF} (+{Equipment.DEFBonus + Level / 2})");
            Console.WriteLine($"보유 골드: {Gold} G");
            Console.WriteLine("\nEnter 키를 누르면 이전 화면으로 돌아갑니다.");
            Console.ReadLine();
        }

        // 플레이어 정보 data 에 저장
        public static void SavePlayerData()
        {
            GameData data = new GameData
            {
                Name = Player.Name,
                Job = Player.Job,
                Exp = Player.Exp,
                Level = Player.Level,
                Gold = Player.Gold,
                MaxHP = Player.MaxHP,
                CurrentHP = Player.CurrentHP,
                MaxMP = Player.MaxMP,
                CurrentMP = Player.CurrentMP,
                TotalATK = (int)Player.TotalATK,
                TotalDEF = Player.TotalDEF,

                Inventory = Equipment.Inventory.Select(w => new WeaponData
                {
                    Name = w.Name,
                    Type = w.Type.ToString(),
                    ATK = w.ATK,
                    Price = w.Price
                }).ToList(),

                // 장착한 물건이 있다면 WeponData에 저장, 없으면 null값
                EquippedWeapon = Equipment.EquippedWeapon != null ? new WeaponData
                {
                    Name = Equipment.EquippedWeapon.Name,
                    Type = Equipment.EquippedWeapon.Type.ToString(),
                    ATK = Equipment.EquippedWeapon.ATK,
                    Price = Equipment.EquippedWeapon.Price
                } : null
            };

            SaveSystem.Save(data);
        }

        // 저장된 게임 데이터 삭제
        public static void DeletePlayerData()
        {
            SaveSystem.DeleteSave();
            Console.WriteLine("Enter 키를 눌러주세요.");
            Console.ReadLine();
        }

        // data 에 있는 정보 플레이어에게 전달
        public static void LoadPlayerData(GameData data)
        {
            Name = data.Name;
            Job = data.Job;
            Exp = data.Exp;
            Level = data.Level;
            Gold = data.Gold;
            MaxHP = data.MaxHP;
            CurrentHP = data.CurrentHP;
            MaxMP = data.MaxMP;
            CurrentMP = data.CurrentMP;
            TotalATK = data.TotalATK;
            TotalDEF = data.TotalDEF;

            Equipment.Inventory = data.Inventory.Select(w => new Equipment.Weapon(
                w.Name,
                Enum.Parse<Equipment.WeaponType>(w.Type),
                w.ATK,
                w.Price
                )).ToList();

            if (data.EquippedWeapon != null)
            {
                var loadedWeapon = new Equipment.Weapon(
                    data.EquippedWeapon.Name,
                    Enum.Parse<Equipment.WeaponType>(data.EquippedWeapon.Type),
                    data.EquippedWeapon.ATK,
                    data.EquippedWeapon.Price
                    );

                Equipment.Equip(loadedWeapon);
            }
            else
            {
                Equipment.EquippedWeapon = null;
                Equipment.ATKBonus = 0;
            }

            UpdateStats();
        }

        public static void ResetAfterDeath()
        {
            //레벨, 경험치, 골드 초기화
            Level = 1;
            Exp = 0;
            Gold = 1500;

            //무기, 인벤토리 초기화
            Equipment.Inventory.Clear();
            Equipment.EquippedWeapon = null;
            Equipment.ATKBonus = 0;

            //직업에 따른 능력치 초기화
            UpdateStats();

            // 체력, 마나는 최대치로 회복
            CurrentHP = MaxHP;
            CurrentMP = MaxMP;
        }

        // 레벨업
        public static void LvUp()
        {
            int remainderExp = 0;

            if (Exp >= 10)
            {
                remainderExp = Exp - 10; // 레벨업 하고 남은 경험치
                Level++;
                TotalATK += 0.5f;
                TotalDEF += 1;
                Exp = remainderExp;

                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{Level - 1} {Name} -> Lv.{Level} {Name}");
                Console.WriteLine($"Hp.{CurrentHP} -> {MaxHP}");
                Console.WriteLine($"Mp.{CurrentMP} -> {MaxMP}");
                Console.WriteLine($"공격력: {TotalATK - 0.5f} -> {TotalATK}");
                Console.WriteLine($"방어력:{TotalDEF - 1} -> {TotalDEF}");
                Console.WriteLine($"Exp:{Exp + 10} -> {remainderExp}\n");
                Console.WriteLine("아무 키나 눌러주세요.\n");
                Console.ReadLine();
            }
        }
    }
}
