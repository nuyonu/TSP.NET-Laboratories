using PhotoManagerWCF.Interfaces;
using System.ServiceModel;

namespace PhotoManagerWCF
{
    [ServiceContract]
    public interface IPhotoManagerService : IMedia
    { }
}
