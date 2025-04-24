using System;
using System.Collections.Generic;

namespace XVIBE_TextRPG
{
    public class Equipment
    {
        public static int ATKBonus { get; set; } = 0;
        public static int DEFBonus { get; set; } = 0;

        // 무기 타입 정의
        public enum WeaponType { Sword, Dagger, Staff }

        // 무기 클래스 정의
        public class Weapon
        {
            public string Name { get; }
            public WeaponType Type { get; }
            public int ATK { get; }
            public int Price { get; }

            public Weapon(string name, WeaponType type, int atk, int price)
            {
                Name = name;
                Type = type;
                ATK = atk;
                Price = price;
            }

            public override string ToString()
            {
                return $"{Name} (공격력: {ATK}, 가격: {Price}G)";
            }

            public override bool Equals(object obj)
            {
                if (obj is Weapon other)
                {
                    return Name == other.Name && Type == other.Type && ATK == other.ATK && Price == other.Price ;
                }
                return false ;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, Type, ATK, Price);
            }
        }

        // 인벤토리와 장비
        public static List<Weapon> Inventory = new List<Weapon>();
        public static Weapon EquippedWeapon = null;

        // 무기 장착
        public static void Equip(Weapon weapon)
        {
            if (EquippedWeapon != null && weapon.Equals(EquippedWeapon)) // 저장하고 불러왔을 때 참조가 달라 객체 비교로 변경
            {
                Console.WriteLine($"{weapon.Name}은(는) 이미 장착되어 있습니다!");
                return;
            }

            if (EquippedWeapon != null)
            {
                Console.WriteLine($"{EquippedWeapon.Name}을(를) 해제합니다.");
            }

            EquippedWeapon = weapon;
            ATKBonus = weapon.ATK;
            Console.WriteLine($"{weapon.Name}을(를) 새로 장착했습니다!");
        }

        // 무기 해제
        public static void Unequip()
        {
            if (EquippedWeapon != null)
            {
                Console.WriteLine($"{EquippedWeapon.Name}을(를) 해제했습니다!");
                EquippedWeapon = null;
                ATKBonus = 0;
            }
            else
            {
                Console.WriteLine("장착된 무기가 없습니다.");
            }
        }

        // 인벤토리 표시 및 장착 / 해제
        public static void ShowInventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== 인벤토리 =====");
                if (Inventory.Count == 0)
                {
                    Console.WriteLine("보유한 무기가 없습니다.");
                }
                else
                {
                    for (int i = 0; i < Inventory.Count; i++)
                    {
                        var w = Inventory[i];
                        string equippedTag = (EquippedWeapon != null && w.Equals(EquippedWeapon)) ? "[E] " : ""; // 이 부분도 참조가 달라 적용 안되던 부분 수정
                        Console.WriteLine($"{i + 1}. {equippedTag}{w}");
                    }
                }

                Console.WriteLine("\n0. 뒤로가기");
                Console.WriteLine("장착할 무기 번호를 입력하거나 해제하려면 U를 입력하세요.");
                Console.Write(">> ");
                var input = Console.ReadLine();

                if (input == "0") break;
                else if (input.ToUpper() == "U")
                {
                    Unequip();
                }
                else if (int.TryParse(input, out int index) && index > 0 && index <= Inventory.Count)
                {
                    Equip(Inventory[index - 1]);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

                Console.WriteLine("\n계속하려면 Enter...");
                Console.ReadLine();
            }
        }
    }
}
