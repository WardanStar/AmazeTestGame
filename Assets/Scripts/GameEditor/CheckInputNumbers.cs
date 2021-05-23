using UnityEngine;

namespace DefaultNamespace
{
	public class CheckInputNumbers
	{
		public bool CheckNumber(int number)
		{
			if (number < 0 || number == 0)
			{
				Debug.Log("Number cannot be negative or should not be equal to zero");
				return true;
			}
			var remainder = number % 2;
			if (remainder == 0)
			{
				Debug.Log("Enter a number that is not a multiple of 2");
				return true;
			}

			return false;
		}
	}
}