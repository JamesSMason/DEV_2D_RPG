using UnityEngine;

namespace JSM.Utilities
{
    public static class DiceRoller
    {
        public static int RollDice(int numberOfDice, int numberOfSides)
        {
            int runningTotal = 0;
            for (int i = 0; i < numberOfDice; i++)
            {
                int roll = Random.Range(1, numberOfSides + 1);
                runningTotal += roll;
            }
            return runningTotal;
        }
    }
}