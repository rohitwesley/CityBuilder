
using System.Collections;
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
        Factory,
        Pawns,
    }
    
    /// <summary>
    /// All agents have there properties in this class
    /// Should be added to the part of the Agent that has the collider 
    /// </summary>
    public class Agents : MonoBehaviour
    {

        public UnitType unitType = UnitType.Home;
        public GameObject unitIcon;

        /// <summary>
        /// Hide Icons on start
        /// </summary>
        private void Start()
        {
            unitIcon.SetActive(false);
        }

        /// <summary>
        /// used to activat Icon coroutine
        /// </summary>
        /// <param name="sec"></param>
        public void ShowIcon(float sec)
        {
            StartCoroutine(ShowIconAnimation(sec));
        }

        /// <summary>
        /// Show Icon for given time using Coroutine
        /// </summary>
        /// <param name="sec">seconds to show icons</param>
        /// <returns>pause</returns>
        IEnumerator ShowIconAnimation(float sec)
        {
    
            yield return new WaitForSeconds(0.1f);
            unitIcon.SetActive(true);
            yield return new WaitForSeconds(sec);
            unitIcon.SetActive(false);
        }
        

        public static UnitType GetAgentFromString(string agentType)
        {
            switch (agentType)
            {
                case "Floor": 
                    return UnitType.Floor;
                case "Home": 
                    return UnitType.Home;
                case "Player": 
                    return UnitType.Player;
                case "Factory": 
                    return UnitType.Factory;
                case "Pawns": 
                    return UnitType.Pawns;
                default:
                    return UnitType.Floor;
            }
        }
    
    }

}
