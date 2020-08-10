using FileMonitor.DataContracts;
using FileMonitor.Service;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;

namespace SignalRConsoleHost
{
	public class FileMonitorHub : Hub
	{
		#region Global declarations

		private readonly IFileMonitorService _fileMonitorService;

		#endregion

		#region Constructors

		public FileMonitorHub(IFileMonitorService fileMonitorService)
		{
			_fileMonitorService = fileMonitorService;
		}

		#endregion

		#region Public methods

		public void ManageFile(string fileNameOrContent, EventType eventType)
		{
			this.ManipulateFile(fileNameOrContent, eventType);
		}

		#endregion

		#region Private methods

		private void ManipulateFile(string fileNameOrContent, EventType eventType)
		{
			switch (eventType)
			{
				case EventType.Appended:
					_fileMonitorService.Append(fileNameOrContent);
					break;
				case EventType.Created:
					_fileMonitorService.CreateFile(fileNameOrContent);
					break;
				case EventType.Deleted:
					_fileMonitorService.DeleteFile();
					break;
				case EventType.Renamed:
					_fileMonitorService.RenameFile(fileNameOrContent);
					break;
			}
		}

		#endregion
	}
}
