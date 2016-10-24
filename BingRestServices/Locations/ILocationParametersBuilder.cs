using RestSharp;

namespace BingRestServices.Locations
{
    public interface ILocationParametersBuilder<T>
    {
        void Build(T parameters, IRestRequest request);
    }

    public interface ILocationParametersBuilder
    {
        void Build(FindLocationParameters parameters, IRestRequest request);
    }
}