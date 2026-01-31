
// Install-Package Newtonsoft.Json
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public static class JsonNetFlattener
{
	public static List<Dictionary<string, object?>> ToListOfDictionaries(string json, string delimiter = ".")
	{
		var token = JToken.Parse(json);
		var list = new List<Dictionary<string, object?>>();

		if (token is JArray arr)
		{
			foreach (var item in arr)
				list.Add(Flatten(item, delimiter));
		}
		else
		{
			list.Add(Flatten(token, delimiter));
		}

		return list;
	}

	private static Dictionary<string, object?> Flatten(JToken token, string delimiter, string prefix = "")
	{
		var dict = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

		switch (token.Type)
		{
			case JTokenType.Object:
				foreach (var prop in ((JObject)token).Properties())
				{
					var key = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}{delimiter}{prop.Name}";
					var child = Flatten(prop.Value, delimiter, key);
					foreach (var kv in child) dict[kv.Key] = kv.Value;
				}
				break;

			case JTokenType.Array:
				int i = 0;
				foreach (var item in (JArray)token)
				{
					var key = $"{prefix}[{i}]";
					var child = Flatten(item, delimiter, key);
					foreach (var kv in child) dict[kv.Key] = kv.Value;
					i++;
				}
				if (i == 0 && !string.IsNullOrEmpty(prefix))
					dict[prefix] = Array.Empty<object?>();
				break;

			default:
				dict[prefix] = (token as JValue)?.Value;
				break;
		}

		return dict;
	}
}
