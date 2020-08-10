using System;
using System.Collections.Generic;
using System.Text;

namespace FileMonitor.DataContracts
{
	public class FileEventData
	{
		public string FileName { get; set; }

		public DateTime DateTime { get; set; }

		public string EventType { get; set; }
	}
}
