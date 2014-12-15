using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ConfigParser {

	// Properties
	private Dictionary<String, String> values;

	// Constructor
	public ConfigParser(string fileData) {
		// Reads a file into a dictionary

		string[] lines = fileData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
		int equalPosition;
		values = new Dictionary<String, String>();

		foreach (var line in lines) {
			// Skip comments
			if (line.Trim()[0] == '#') continue;

			// Parse field, allows "=" in the field value but not in the field name
			equalPosition = line.IndexOf("=");
			if (equalPosition > 0) {
				values.Add(line.Substring(0, equalPosition).Trim(), line.Substring(equalPosition + 1).Trim());
			}
		}
	}

	// Public interface
	public string getString(string fieldName) {
		return values.ContainsKey(fieldName) ? values[fieldName] : null;
	}

	public float? getFloat(string fieldName) {
		return values.ContainsKey(fieldName) ? float.Parse(getString(fieldName), CultureInfo.InvariantCulture) : (float?)null;
	}

	public int? getInt(string fieldName) {
		return values.ContainsKey(fieldName) ? int.Parse(getString(fieldName), CultureInfo.InvariantCulture) : (int?)null;
	}
}
