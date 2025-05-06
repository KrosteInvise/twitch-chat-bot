using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Test;
using UnityEngine;

namespace ChatBot
{
	public class LootChanceCounter : MonoBehaviour
	{
		[SerializeField]
		List<Loot> lootList;
	
		[Button]
		void CountLoot()
		{
			int sumWeight       = lootList.Sum(x => x.ItemWeight);
			int weightDedicator = Random.Range(0, sumWeight);
		
			foreach (var loot in lootList)
			{
				if(weightDedicator <= loot.ItemWeight)
					Debug.Log($"Dropped: {loot.ItemName}");
			}
		}

		[Button()]
		void CheckLoot()
		{
			foreach (var item in lootList)
			{
				Debug.Log($"{item.ItemName}: {item.ItemWeight}");
			}

			int sumWeight = lootList.Sum(x => x.ItemWeight);

			Debug.Log(sumWeight);
		}
	}
}
