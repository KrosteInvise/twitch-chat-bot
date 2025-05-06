using UnityEngine;

namespace Test
{
	[CreateAssetMenu]
	public class Loot : ScriptableObject
	{
		[SerializeField]
		string lootName;

		[SerializeField]
		int lootWeight;

		public string ItemName => lootName;

		public int ItemWeight => lootWeight;
	}
}

