using AutoMapper;
using MyPhotosDatabase;
using MyPhotosDatabase.Models.DTO;
using System;
using System.Collections.Generic;

namespace PhotoManagerWCF
{
    public class PhotoManagerService : IPhotoManagerService
    {
        public bool AlreadyInDatabaseAndNotDeleted(string path)
        {
            Console.WriteLine("Recived AlreadyInDatabase({0})", path);
            var result = MyPhotosAPI.AlreadyInDatabaseAndNotDeleted(path);
            Console.WriteLine("Response: {0}", result);
            return result;
        }

        public void DeleteMedia(MediaDTO mediaDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MediaDTO, Media>());
            var mapper = config.CreateMapper();

            Media media = mapper.Map<Media>(mediaDTO);

            MyPhotosAPI.DeleteMedia(media);
        }

        public void DeleteMediaById(Guid id)
        {
            MyPhotosAPI.DeleteMediaById(id);
        }

        public List<MediaDTO> GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Media, MediaDTO>());
            var mapper = config.CreateMapper();

            return mapper.Map<List<Media>, List<MediaDTO>>(MyPhotosAPI.GetAll());
        }

        public List<MediaDTO> GetAllWhere(string toSearch, bool byDate, bool byEvent, bool byPeople, bool byLocation, bool byTags, bool byDescription)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Media, MediaDTO>());
            var mapper = config.CreateMapper();

            Func<Media, bool> predicate = media =>
                !media.Deleted && (media.Name.ToLower().Contains(toSearch.ToLower()) ||
                (media.CreatedDate.ToString().ToLower().Contains(toSearch.ToLower()) && byDate) ||
                (media.Event.ToLower().Contains(toSearch.ToLower()) && byEvent) ||
                (media.People.ToLower().Contains(toSearch.ToLower()) && byPeople) ||
                (media.Location.ToLower().Contains(toSearch.ToLower()) && byLocation) ||
                (media.Tags.ToLower().Contains(toSearch.ToLower()) && byTags) ||
                (media.Description.ToLower().Contains(toSearch.ToLower()) && byDescription));

            return mapper.Map<List<Media>, List<MediaDTO>>(MyPhotosAPI.GetAll(predicate));
        }

        public MediaDTO GetMediaById(Guid id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Media, MediaDTO>());
            var mapper = new Mapper(config);

            Console.WriteLine("Recived GetMediaById({0})", id);
            var result = mapper.Map<MediaDTO>(MyPhotosAPI.GetMediaById(id));
            Console.WriteLine("Respoonse: \n\tMedia Name: {0}", result.Name);

            return result;
        }

        public void SaveMedia(MediaDTO mediaDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MediaDTO, Media>());
            var mapper = config.CreateMapper();

            Console.WriteLine("Recived SaveMedia({0})", mediaDTO.Name);

            MyPhotosAPI.SaveMedia(mapper.Map<Media>(mediaDTO));

            Console.WriteLine("Media saved");
        }

        public void UpdateMedia(Guid id, MediaDTO mediaDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MediaDTO, Media>());
            var mapper = config.CreateMapper();

            MyPhotosAPI.UpdateMedia(id, mapper.Map<Media>(mediaDTO));
        }
    }
}
