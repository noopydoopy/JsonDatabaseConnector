using JsonDatabaseConnector.Common;
using JsonDatabaseConnector.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace JsonDatabaseConnector
{
    public class DataAccessAdapter
    {

        private string databaseDir = ConfigurationManager.AppSetting["databaseDir"];

        /// <summary>
        /// Return collection of entities.
        /// </summary>
        public List<T> GetEntities<T>(string tableName) where T : EntityBase
        {
            try
            {
                string filePath = databaseDir + "\\" + tableName + ".json";
                if (Directory.Exists(databaseDir) && !File.Exists(filePath))
                {
                    // Prevent if create new table, the file should be create .
                    System.IO.File.WriteAllText(filePath, "");
                }
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    if (!string.IsNullOrEmpty(json))
                    {
                        var obj = JsonHelper.ReadToObject<List<T>>(json);
                        return obj;
                    }
                    else
                    {
                        return new List<T>();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Add the new row to collection.
        /// </summary>
        public void SaveEntity<T>(T entity, string tableName) where T : EntityBase
        {
            try
            {
                List<T> entities = GetEntities<T>(tableName);
                int maxId = entities.Count > 0 ? entities.Max(c => c.Id) : 0;

                entity.Id = maxId + 1;
                entities.Add(entity);

                // Rewrite all content in file
                JsonHelper.WriteFromObject(entities, databaseDir + "\\" + tableName + ".json");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Add multiple rows to collection.
        /// </summary>
        public void SaveEntities<T>(List<T> entity, string tableName) where T : EntityBase
        {
            try
            {
                List<T> entities = GetEntities<T>(tableName);
                int maxId = entities.Count > 0 ? entities.Max(c => c.Id) : 0;
                foreach (var item in entity)
                {
                    item.Id = maxId + 1;
                    maxId++;
                }
                entities.AddRange(entity);
                JsonHelper.WriteFromObject(entities, databaseDir + "\\" + tableName + ".json");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update entity contain in collection.
        /// </summary>
        public bool UpdateEntity<T>(T entity, string tableName) where T : EntityBase
        {
            try
            {
                List<T> entities = GetEntities<T>(tableName);
                int index = -1;
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].Id == entity.Id)
                    {
                        index = i;
                        break;
                    }

                }

                if (index >= 0)
                {
                    entities[index] = Clone<T>(entity);
                    // Rewrite all content in file
                    JsonHelper.WriteFromObject(entities, databaseDir + "\\" + tableName + ".json");
                    return true;
                }

                return false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove row from collection.
        /// </summary>
        public bool DeleteEntity<T>(T entity, string tableName) where T : EntityBase
        {
            try
            {
                List<T> entities = GetEntities<T>(tableName);
                int index = -1;
                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].Id == entity.Id)
                    {
                        index = i;
                        break;
                    }

                }

                if (index >= 0)
                {
                    entities.RemoveAt(index);
                    // Rewrite all content in file
                    JsonHelper.WriteFromObject(entities, databaseDir + "\\" + tableName + ".json");
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
