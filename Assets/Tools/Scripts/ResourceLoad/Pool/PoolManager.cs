using System.Collections.Generic;
using UnityEngine;

namespace ResourcesLoadSystem
{
	public class PoolManager
	{
		private Dictionary<string, Stack<Component>> _dictionaryStacksComponents = new Dictionary<string, Stack<Component>>();

		public TComponent GetComponent<TComponent>(string resourcePath) where TComponent : Component
		{
			var stack = GetStack(resourcePath);

			if (stack.Count == 0) return null;
			
			var component = stack.Pop();
			component.gameObject.SetActive(true);
			return (TComponent) component;
		}

		public void ReturnToPool<TComponent>(TComponent component, string resourcePath) where TComponent : Component
		{
			var stack = GetStack(resourcePath);
			stack.Push(component);
		}
		
		private Stack<Component> GetStack(string resourcePath)
		{
			if (!_dictionaryStacksComponents.TryGetValue(resourcePath, out Stack<Component> stack))
			{
				stack = new Stack<Component>();
				_dictionaryStacksComponents.Add(resourcePath, stack);
			}

			return stack;
		}
	}
}