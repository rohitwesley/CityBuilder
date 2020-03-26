
using UnityEngine;

namespace GameLogic
{

    public enum UnitType
    {
        Floor,
        Home,
        Player,
        Office,
        Pawns,
    }

    public class Agents : MonoBehaviour
    {

        public UnitType unitType = UnitType.Home;
        
    }

}
