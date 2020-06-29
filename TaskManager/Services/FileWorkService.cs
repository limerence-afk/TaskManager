using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Services
{
     class FileWorkService
     {
          private readonly string PATH;
          public FileWorkService (string path)
          {
               PATH = path;
          }

          public BindingList<TaskModel> LoadData()
          {
               var FileExists = File.Exists(PATH);
               if (!FileExists)
               {
                    File.CreateText(PATH).Dispose();
                    return new BindingList<TaskModel>();
               }
               using (var reader = File.OpenText(PATH))
               {
                    var FileText = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<BindingList<TaskModel>>(FileText);
               }
          }
          public void SaveData(object taskData)
          {
               using (StreamWriter writer = File.CreateText(PATH))
               {
                    string output = JsonConvert.SerializeObject(taskData);
                    writer.Write(output);
               }
          }
     }
}
