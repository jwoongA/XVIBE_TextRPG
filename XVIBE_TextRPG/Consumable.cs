using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    internal class Consumable//체력, 마나 포션들과 회피율을 올리는 섬광탄만 구현
    {
        public string Name { get; set; } = "";
        public int Price { get; set; } = 0;
        public int HealHP { get; set; } = 0;
        public int HealMP { get; set; } = 0;

        public int Amount { get; set; } = 0; // 보유량
        public ConsumableType Type { get; set; } // 포션, 섬광탄 타입

        public string Description { get; set; } = ""; // 소모품 설명

        public enum ConsumableType { Potion, FlashGrenade }

        public Consumable(string name, int price, int healHP, int healMP, int amount, ConsumableType type, string description = "")
        {
            Name = name;
            Price = price;
            HealHP = healHP;
            HealMP = healMP;
            Amount = amount;
            Type = type;
            Description = description;
        }

        public static List<Consumable> consumable = new List<Consumable>()
        {
            new Consumable("체력 포션(소)", 500, 50, 0, 5, ConsumableType.Potion, "체력을 50 회복합니다."),
            new Consumable("마나 포션(소)", 500, 0, 50, 5, ConsumableType.Potion, "마나를 50 회복합니다."),
            new Consumable("체력 포션(중)", 2000, 150, 0, 0, ConsumableType.Potion, "체력을 150 회복합니다."),
            new Consumable("마나 포션(중)", 2000, 0, 150, 0, ConsumableType.Potion, "마나를 150 회복합니다."),
            new Consumable("체력 포션(대)", 5000, 300, 0, 0, ConsumableType.Potion, "체력을 300 회복합니다."),
            new Consumable("마나 포션(대)", 5000, 0, 300, 0, ConsumableType.Potion, "마나를 300 회복합니다."),
            new Consumable("복합 포션(소)", 1000, 50, 50, 0, ConsumableType.Potion, "체력과 마나를 각각 50 회복합니다."),
            new Consumable("복합 포션(중)", 3000, 150, 150, 0, ConsumableType.Potion, "체력과 마나를 각각 150 회복합니다."),
            new Consumable("복합 포션(대)", 7000, 300, 300, 0, ConsumableType.Potion, "체력과 마나를 각각 300 회복합니다."),
            new Consumable("섬광탄", 1500, 0, 0, 5, ConsumableType.FlashGrenade, "섬광탄을 사용하여 회피율을 30% 증가시킵니다.(중첩 안됨)"),
        };

        static public void UseConsumable()
        {
            Console.WriteLine("===== 사용할 소모품 선택 =====");

            // 보유량이 있는 소모품만 따로 리스트로 관리
            var availableConsumables = consumable.Where(item => item.Amount > 0).ToList();

            // 보유량이 있는 소모품만 출력
            for (int i = 0; i < availableConsumables.Count; i++)
            {
                var item = availableConsumables[i];
                Console.WriteLine($"{i + 1}. {item.Name}, 효과: {item.Description}, 보유량: {item.Amount}");
            }

            // 보유량이 있는 소모품이 없는 경우
            if (availableConsumables.Count == 0)
            {
                Console.WriteLine("보유 중인 소모품이 없습니다.");
                return;
            }

            Console.WriteLine("숫자 키를 눌러 사용할 소모품을 선택하세요.");
            Console.WriteLine("0. 뒤로가기");
            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    return;
                default:
                    if (int.TryParse(input, out int index) && index > 0 && index <= availableConsumables.Count)
                    {
                        var selectedConsumable = availableConsumables[index - 1];
                        Console.WriteLine($"{selectedConsumable.Name}을(를) 사용했습니다!");
                        Console.WriteLine($"선택된 소모품: {selectedConsumable.Name}, 사용전 보유량: {selectedConsumable.Amount}");
                        selectedConsumable.Amount--;

                        // 포션과 섬광탄 구분 처리
                        if (selectedConsumable.Type == ConsumableType.Potion)
                        {
                            Player.CurrentHP = Math.Min(Player.CurrentHP + selectedConsumable.HealHP, Player.MaxHP);
                            Player.CurrentMP = Math.Min(Player.CurrentMP + selectedConsumable.HealMP, Player.MaxMP);
                            Console.WriteLine($"체력: {Player.CurrentHP}/{Player.MaxHP}, 마나: {Player.CurrentMP}/{Player.MaxMP}");
                        }
                        else if (selectedConsumable.Type == ConsumableType.FlashGrenade)
                        {
                            Player.AdditionalEvasionRate = 0.3f; // 추가회피율 30%로 설정
                            Console.WriteLine("섬광탄을 사용하여 회피율이 30% 증가했습니다!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    break;
            }
        }
    }
}
