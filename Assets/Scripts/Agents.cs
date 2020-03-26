
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// Agent Type ID to recognise type of Agent
    /// </summary>
    public enum UnitType
    {
        Floor,
        Home,
        Player,
        Office,
        Pawns,
    }
    
    /// <summary>
    /// All agents have there properties in this class
    /// </summary>
    public class Agents : MonoBehaviour
    {

        public UnitType unitType = UnitType.Home;
        
    }

}
