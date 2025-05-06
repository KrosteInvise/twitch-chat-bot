using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ShowEditorAttribute : DrawerAttribute
	{
		public ShowEditorAttribute()
		{
		}
	}
}