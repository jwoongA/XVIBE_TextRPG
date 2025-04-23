namespace XVIBE_TextRPG
{
    internal class Shop
    {
        static List<Equipment.Weapon> storeWeapons = new List<Equipment.Weapon>()
        {
            new Equipment.Weapon("강철 검", Equipment.WeaponType.Sword, 10, 100),
            new Equipment.Weapon("은 단검", Equipment.WeaponType.Dagger, 7, 80),
            new Equipment.Weapon("마법 지팡이", Equipment.WeaponType.Staff, 12, 150),
        };

        public static int Gold = 500;

        public static void EnterShop()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== 무기 상점 ===");
                Console.WriteLine($"보유 골드: {Gold}G");
                for (int i = 0; i < storeWeapons.Count; i++)
                {
                    var w = storeWeapons[i];
                    Console.WriteLine($"{i + 1}. {w}");
                }
                Console.WriteLine("0. 나가기");
                Console.Write("구매할 무기 번호를 입력하세요: ");
                string input = Console.ReadLine();

                if (input == "0") break;

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= storeWeapons.Count)
                {
                    var selected = storeWeapons[choice - 1];
                    if (Gold >= selected.Price)
                    {
                        Equipment.Inventory.Add(selected);
                        Gold -= selected.Price;
                        Console.WriteLine($"{selected.Name}을(를) 구매했습니다!");
                    }
                    else Console.WriteLine("골드가 부족합니다!");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadLine();
            }
        }
    }
}
