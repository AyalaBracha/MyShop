using Entities;

namespace Servicess
{
    public interface IRatingServicess
    {
        Task Post(Rating rating);
    }
}