using Entities;

namespace Reposetories
{
    public interface IRatingReposetories
    {
        Task Post(Rating rating);
    }
}