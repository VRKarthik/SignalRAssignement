using FileMonitor.DataContracts;
using FileMonitor.Service;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SignalRConsoleHost
{
	public class FileEventService
	{
		#region Global declarations

		private readonly IHubContext<FileMonitorHub> _hubContext;
		private readonly IFileMonitorService _fileMonitorService;

		#endregion

		#region Constructor

		public FileEventService(IHubContext<FileMonitorHub> hubContext, IFileMonitorService fileMonitorService)
		{
			_hubContext = hubContext;
			_fileMonitorService = fileMonitorService;
			_fileMonitorService.ContentAppendedEvent += this.OnContentAppended;
			_fileMonitorService.FileCreatedEvent += this.OnFileCreated;
			_fileMonitorService.FileRenamedEvent += this.OnFileRenamed;
			_fileMonitorService.FileDeletedEvent += this.OnFileDeleted;
		}

		#endregion

		#region Public methods

		public void StartWatcher()
		{
			_fileMonitorService.StartFileWatcher();
		}

		#endregion

		#region Private methods

		private void OnFileDeleted(object sender, EventArgs e)
		{
			var fileSystemEventArgs = e as FileSystemEventArgs;
			var fileEventData = new FileEventData
			{
				DateTime = DateTime.Now,
				EventType = EventType.Deleted.ToString(),
				FileName = fileSystemEventArgs?.Name
			};

			var fileEventJson = JsonConvert.SerializeObject(fileEventData);
			_hubContext.Clients.All.SendAsync("OnFileChanged", fileEventJson);
		}

		private void OnFileRenamed(object sender, EventArgs e)
		{
			var renamedEventArgs = e as RenamedEventArgs;
			var fileEventData = new FileEventData
			{
				DateTime = DateTime.Now,
				EventType = EventType.Renamed.ToString(),
				FileName = renamedEventArgs?.Name
			};

			var fileEventJson = JsonConvert.SerializeObject(fileEventData);
			_hubContext.Clients.All.SendAsync("OnFileChanged", fileEventJson);
		}

		private void OnFileCreated(object sender, EventArgs e)
		{
			var fileSystemEventArgs = e as FileSystemEventArgs;
			var fileEventData = new FileEventData
			{
				DateTime = DateTime.Now,
				EventType = EventType.Created.ToString(),
				FileName = fileSystemEventArgs?.Name
			};

			var fileEventJson = JsonConvert.SerializeObject(fileEventData);
			_hubContext.Clients.All.SendAsync("OnFileChanged", fileEventJson);
		}

		private void OnContentAppended(object sender, EventArgs e)
		{
			var fileSystemEventArgs = e as FileSystemEventArgs;
			var fileEventData = new FileEventData
			{
				DateTime = DateTime.Now,
				EventType = EventType.Appended.ToString(),
				FileName = fileSystemEventArgs?.Name
			};

			var fileEventJson = JsonConvert.SerializeObject(fileEventData);
			_hubContext.Clients.All.SendAsync("OnFileChanged", fileEventJson);
		}

		#endregion
	}
}
