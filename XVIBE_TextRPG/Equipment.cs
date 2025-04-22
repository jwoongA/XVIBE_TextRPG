using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIBE_TextRPG
{
    internal class Equipment//장비는 아직 구현이 안됨 착용 장비에 맞춰 ATK, DEF 보너스가 추가됨
    {
        public static int ATKBonus { get; set; } = 0; // 공격력 보너스
        public static int DEFBonus { get; set; } = 0; // 방어력 보너스
        //장비의 보유량, 상점 재고 등의 int 변수 추가 예정
        //무기 착용 여부, 방어구 착용 여부 등의 bool 변수 추가 예정

        // 무기의 종류를 enum으로 정의
        public enum WeaponType
        {
            Sword,  // 무기
            Dagger, // 단검
            Staff,  // 지팡이
        }


        // 장비 목록을 스태틱 리스트로 관리 예정
        // 장비를 착용하는 메서드, 장비를 해제하는 메서드, 상점 재고를 관리하는 메서드 등을 추가할 예정

    }
}
