using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable
{
	void Consume(out float eatTime, out float foodValue, out string effect, out float effectValue);
}
