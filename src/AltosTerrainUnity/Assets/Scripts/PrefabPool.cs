using System.Collections.Generic;
using UnityEngine;

public class PrefabPool<T>
	where T : Component
{
	public T Prefab;

	public List<T> Pool = new();
	private int currentGrabIndex;

	public T Grab(Transform parent)
	{
		if (Pool.Count == currentGrabIndex)
		{
			ExpandPool(parent);
		}

		var item = Pool[currentGrabIndex];
		item.gameObject.SetActive(true);
		if (item.transform.parent != parent)
		{
			item.transform.SetParent(parent);
		}
		else
		{
			item.transform.SetAsLastSibling();
		}
		currentGrabIndex++;

		return item;
	}

	public void ReturnAll()
	{
		foreach (var item in Pool)
		{
			item.gameObject.SetActive(false);
		}

		currentGrabIndex = 0;
	}

	public void Return(T item)
	{
		int itemIndex = Pool.IndexOf(item);

		if (itemIndex == -1)
		{
			Debug.LogError("Item being returned to the pool doesn't belong in it.");
		}

		Pool.RemoveAt(itemIndex);
		Pool.Add(item);
		item.gameObject.SetActive(false);
		currentGrabIndex--;
	}

	private void ExpandPool(Transform parent)
	{
		var clone = Object.Instantiate(Prefab, parent);
		Pool.Add(clone);
	}
}
