using System.Collections;
using System.Collections.Generic;
using System;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

public class CommonUtil
{
	//For getting random values
	private static Random random = new Random ();

	/// <summary>
	/// Shuffles the contents of a list
	/// </summary>
	/// <typeparam name="T">The type of the list to sort</typeparam>
	/// <param name="listToShuffle">The list to shuffle</param>
	/// <param name="numberOfTimesToShuffle">How many times to shuffle the list
	/// by default 3 times</param>
	public static void Shuffle<T> (List<T> listToShuffle, int numberOfTimesToShuffle = 3)
	{
		//Make a new list of the wanted type
		List<T> newList = new List<T> ();

		//For each time we want to shuffle
		for (int i = 0; i < numberOfTimesToShuffle; i++) {
			//While there are still items in our list
			while (listToShuffle.Count > 0) {
				//get a random number within the list
				int index = random.Next (listToShuffle.Count);

				//Add the item at that position to the new list
				newList.Add (listToShuffle [index]);

				//And remove it from the old list
				listToShuffle.RemoveAt (index);
			}

			//Then copy all the items back in the old list again
			listToShuffle.AddRange (newList);

			//And clear the new list ,to make ready for next shuffling
			newList.Clear ();
		}
	}

	/// <summary>
	/// Converts bool value true/false to int value 0/1.
	/// </summary>
	/// <returns>The int value.</returns>
	/// <param name="value">The bool value.</param>
	public static int TrueFalseBoolToZeroOne (bool value)
	{
		if (value) {
			return 1;
		}
		return 0;
	}

	/// <summary>
	/// Converts int value 0/1 to bool value true/false.
	/// </summary>
	/// <returns>The bool value.</returns>
	/// <param name="value">The int value.</param>
	public static bool ZeroOneToTrueFalseBool (int value)
	{
		if (value == 1) {
			return true;
		} else {
			return false;
		}
	}
}