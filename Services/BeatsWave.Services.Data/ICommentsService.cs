namespace BeatsWave.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(int beatId, string userId, string content);
    }
}
