using MyPhotosDatabase.Models.DTO;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace PhotoManagerWCF.Interfaces
{
    [ServiceContract]
    public interface IMedia
    {
        //C
        [OperationContract]
        void SaveMedia(MediaDTO mediaDTO);
        //R
        [OperationContract]
        MediaDTO GetMediaById(Guid id);
        [OperationContract]
        List<MediaDTO> GetAll();
        [OperationContract]
        List<MediaDTO> GetAllWhere(string toSearch, bool byDate, bool byEvent, bool byPeople, bool byLocation, bool byTags, bool byDescription);
        //U
        [OperationContract]
        void UpdateMedia(Guid id, MediaDTO mediaDTO);
        //D
        [OperationContract]
        void DeleteMedia(MediaDTO mediaDTO);
        [OperationContract]
        void DeleteMediaById(Guid id);
        //ANOTHERS
        [OperationContract]
        bool AlreadyInDatabaseAndNotDeleted(string path);
    }
}
