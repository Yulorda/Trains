using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Zenject;

public class SaveLoadSystem
{
    [Inject]
    Manager manager;

    private SaveLoadSystem()
    {

    }

    public SaveSnapshot GetSave()
    {
        int id = 0;
        var connectionPoints = manager.models
            .Where(sortDictionary => typeof(IConnectionPoint).IsAssignableFrom(sortDictionary.Key))
            .Select(saveKeyValues =>
            {
                return saveKeyValues.Value
                    .Select(saveKeyValue =>
                    {
                        var key = (IConnectionPoint)saveKeyValue;
                        return new KeyValuePair<IConnectionPoint, SaveSnapshot.ConnectionPoint>(
                            key,
                            new SaveSnapshot.ConnectionPoint()
                            {
                                id = id++,
                                obj = new SaveSnapshot.SerializeToType()
                                {
                                    obj = key,
                                    type = saveKeyValues.Key
                                }
                            });
                    });
            })
            .SelectMany(u => u)
            .ToDictionary(x => x.Key, x => x.Value);

        var connections = manager.models
            .Where(sortDictionary => typeof(IConnection).IsAssignableFrom(sortDictionary.Key))
            .Select(saveKeyValues =>
            {
                return saveKeyValues.Value
                   .Select(saveKeyValue =>
                   {
                       var key = (IConnection)saveKeyValue;
                       return new KeyValuePair<IConnection, SaveSnapshot.Connection>(
                           key,
                           new SaveSnapshot.Connection()
                           {
                               id = id++,
                               obj = new SaveSnapshot.SerializeToType()
                               {
                                   type = saveKeyValues.Key,
                                   obj = key
                               },
                               idStartPoint = connectionPoints[key.PointStart.Value].id,
                               idEndPoint = connectionPoints[key.PointEnd.Value].id,
                           });
                   });
            })
            .SelectMany(u => u)
            .ToDictionary(x => x.Key, x => x.Value);

        var pointOnConnection = manager.models
            .Where(sortDictionary => typeof(IPointOnConnection).IsAssignableFrom(sortDictionary.Key))
            .Select(saveKeyValues =>
            {
                return saveKeyValues.Value
                   .Select(saveKeyValue =>
                   {
                       var key = (IPointOnConnection)saveKeyValue;
                       return new KeyValuePair<IPointOnConnection, SaveSnapshot.PointOnConnection>(
                           key,
                           new SaveSnapshot.PointOnConnection()
                           {
                               id = id++,
                               obj = new SaveSnapshot.SerializeToType()
                               {
                                   obj = key,
                                   type = saveKeyValues.Key
                               },
                               idConnection = connections[key.Connection.Value].id,
                           });
                   });
            })
            .SelectMany(u => u)
            .ToDictionary(x => x.Key, x => x.Value);

        var result = new SaveSnapshot();

        result.models.AddRange(connectionPoints.Select(x => new SaveSnapshot.SerializeToType()
        {
            obj = x.Value,
            type = typeof(SaveSnapshot.ConnectionPoint)
        }));

        result.models.AddRange(connections.Select(x => new SaveSnapshot.SerializeToType()
        {
            obj = x.Value,
            type = typeof(SaveSnapshot.Connection)
        }));

        result.models.AddRange(pointOnConnection.Select(x => new SaveSnapshot.SerializeToType()
        {
            obj = x.Value,
            type = typeof(SaveSnapshot.PointOnConnection)
        }));

        return result;
    }

    public void Load(SaveSnapshot saveSnapshot)
    {
        manager.models
            .Where(x => typeof(IConnectionPoint).IsAssignableFrom(x.Key))
            .Select(y => y.Value).SelectMany(u => u)
            .ToList()
            .ForEach(z => manager.Remove(z, z.GetType()));

        var dicConnectionPoint = saveSnapshot.models.Where(x => x.type == typeof(SaveSnapshot.ConnectionPoint)).
            Select(y =>
            {
                var coonectionPoint = (SaveSnapshot.ConnectionPoint)y.obj;
                manager.RegistrateModel(coonectionPoint.obj.obj, coonectionPoint.obj.type);
                return new KeyValuePair<int, IConnectionPoint>(coonectionPoint.id, (IConnectionPoint)coonectionPoint.obj.obj);
            })
            .ToDictionary(x => x.Key, x => x.Value);

        var dicConnection = saveSnapshot.models.Where(x => x.type == typeof(SaveSnapshot.Connection)).
           Select(y =>
           {
               var saveCoonection = (SaveSnapshot.Connection)y.obj;
               var connection = (IConnection)saveCoonection.obj.obj;
               connection.PointStart.Value = dicConnectionPoint[saveCoonection.idStartPoint];
               connection.PointEnd.Value = dicConnectionPoint[saveCoonection.idEndPoint];
               manager.RegistrateModel(saveCoonection.obj.obj, saveCoonection.obj.type);
               return new KeyValuePair<int, IConnection>(saveCoonection.id, connection);
           })
           .ToDictionary(x => x.Key, x => x.Value);

        saveSnapshot.models.Where(x => x.type == typeof(SaveSnapshot.PointOnConnection)).ToList()
           .ForEach(y =>
           {
               var savePointOnConnection = (SaveSnapshot.PointOnConnection)y.obj;
               var pointOnConnection = (IPointOnConnection)savePointOnConnection.obj.obj;
               pointOnConnection.Connection.Value = dicConnection[savePointOnConnection.idConnection];
               manager.RegistrateModel(savePointOnConnection.obj.obj, savePointOnConnection.obj.type);
           });
    }
}

[Serializable]
public class SaveSnapshot
{
    public List<SerializeToType> models = new List<SerializeToType>();

    [Serializable]
    public class ConnectionPoint
    {
        public int id;
        public SerializeToType obj;
    }

    [Serializable]
    public class Connection
    {
        public int id;
        public int idStartPoint;
        public int idEndPoint;
        public SerializeToType obj;
    }

    [Serializable]
    public class PointOnConnection
    {
        public int id;
        public int idConnection;
        public SerializeToType obj;
    }

    [Serializable]
    public class SerializeToType : ISerializable
    {
        public Type type;
        public object obj;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("obj", obj, type);
            info.AddValue("type", type.ToString());
        }

        public SerializeToType()
        {
        }

        private SerializeToType(SerializationInfo info, StreamingContext context)
        {
            type = Type.GetType(info.GetString("type"));
            obj = info.GetValue("obj", type);
        }
    }
}