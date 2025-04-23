namespace XVIBE_TextRPG
{
    internal class Equipment
    {
        public static int ATKBonus { get; set; } = 0;
        public static int DEFBonus { get; set; } = 0;
        
        public enum WeaponType { Sword, Dagger, Staff }

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
        }

        public static List<Weapon> Inventory = new List<Weapon>();
        public static Weapon EquippedWeapon = null;

        public static void Equip(Weapon weapon)
        {
            EquippedWeapon = weapon;
            ATKBonus = weapon.ATK;
            Console.WriteLine($"{weapon.Name}을(를) 장착했습니다!");
        }

        public static void Unequip()
        {
            if (EquippedWeapon != null)
            {
                Console.WriteLine($"{EquippedWeapon.Name}을(를) 해제했습니다!");
                EquippedWeapon = null;
                ATKBonus = 0;
            }
            else Console.WriteLine("장착된 무기가 없습니다.");
        }

        public static void ShowInventory()
        {
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
                    Console.WriteLine($"{i + 1}. {w}");
                }
            }

            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("장착할 무기 번호를 입력하거나 해제하려면 U를 입력하세요.");
            Console.Write(">> ");
            var input = Console.ReadLine();

            if (input == "U")
            {
                Unequip();
            }
            else if (int.TryParse(input, out int index) && index > 0 && index <= Inventory.Count)
            {
                Equip(Inventory[index - 1]);
            }
        }
    }
}
