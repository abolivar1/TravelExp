using System.IO;

namespace TravelExp.Common.Helpers
{
    public interface IFilesHelper
    {
        byte[] ReadFully(Stream input);
    }

}
