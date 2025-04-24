using System;
using System.Collections.Generic;

namespace XVIBE_TextRPG
{
    public class Shop
    {
        public static List<Equipment.Weapon> storeWeapons = new List<Equipment.Weapon>()
        {
            new Equipment.Weapon("강철 검", Equipment.WeaponType.Sword, 10, 100),
            new Equipment.Weapon("은 단검", Equipment.WeaponType.Dagger, 7, 80),
            new Equipment.Weapon("마법 지팡이", Equipment.WeaponType.Staff, 12, 150),
        };

        public static void EnterShop()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== 상점 =====");
                Console.WriteLine($"보유 골드: {Player.Gold} G\n");
                Console.WriteLine("1. 무기 구매");
                Console.WriteLine("2. 무기 판매");
                Console.WriteLine("3. 소모품 구매");
                Console.WriteLine("4. 소모품 판매");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하는 작업을 선택하세요: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": // 무기 구매
                        BuyWeapons();
                        break;
                    case "2": // 무기 판매
                        Console.WriteLine("\n[미구현] 무기 판매 기능은 아직 구현되지 않았습니다.");
                        Console.WriteLine("\nEnter를 눌러 계속...");
                        Console.ReadLine();
                        break;
                    case "3": // 소모품 구매
                        Console.WriteLine("\n[미구현] 소모품 구매 기능은 아직 구현되지 않았습니다.");
                        Console.WriteLine("\nEnter를 눌러 계속...");
                        Console.ReadLine();
                        break;
                    case "4": // 소모품 판매
                        Console.WriteLine("\n[미구현] 소모품 판매 기능은 아직 구현되지 않았습니다.");
                        Console.WriteLine("\nEnter를 눌러 계속...");
                        Console.ReadLine();
                        break;
                    case "0": // 나가기
                        return;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다.");
                        Console.WriteLine("\nEnter를 눌러 계속...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // 기존 무기 구매 로직을 별도 메서드로 분리
        private static void BuyWeapons()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== 무기 구매 =====");
                Console.WriteLine($"보유 골드: {Player.Gold} G\n");

                for (int i = 0; i < storeWeapons.Count; i++)
                {
                    var w = storeWeapons[i];
                    Console.WriteLine($"{i + 1}. {w}");
                }

                Console.WriteLine("0. 뒤로가기");
                Console.Write("\n구매할 무기 번호를 입력하세요: ");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= storeWeapons.Count)
                {
                    var selected = storeWeapons[choice - 1];

                    // 중복 구매 방지
                    if (Equipment.Inventory.Contains(selected))
                    {
                        Console.WriteLine("\n이미 보유 중인 무기입니다.");
                    }
                    else if (Player.Gold >= selected.Price)
                    {
                        Equipment.Inventory.Add(selected);
                        Player.Gold -= selected.Price;
                        Console.WriteLine($"\n{selected.Name}을(를) 구매했습니다!");
                    }
                    else
                    {
                        Console.WriteLine("\n골드가 부족합니다!");
                    }
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                }

                Console.WriteLine("\nEnter를 눌러 계속...");
                Console.ReadLine();
            }
        }
    }
}
