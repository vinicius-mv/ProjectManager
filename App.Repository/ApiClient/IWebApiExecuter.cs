using System.Threading.Tasks;

namespace App.Repository.ApiClient
{
    public interface IWebApiExecuter
    {
        Task<T> InvokeGet<T>(string uri);
        Task InvokeDelete(string uri);
        Task<T> InvokePost<T>(string uri, T obj);
        Task InvokePut<T>(string uri, T obj);
    }
}