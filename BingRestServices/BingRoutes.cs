﻿using System;
using System.Globalization;
using System.Threading.Tasks;

using BingRestServices.Configuration;
using BingRestServices.DataContracts;
using BingRestServices.Extensions;
using BingRestServices.Routes;
using BingRestServices.Services;

using RestSharp;

namespace BingRestServices
{
    public class BingRoutes : BingService, IBingRoutes
    {
        public static string DateFormatMMddyyyy_HHmmss = "MM/dd/yyyy HH:mm:ss";

        public BingRoutes()
        {
        }

        public BingRoutes(BingConfiguration configuration)
            : base(configuration)
        {
        }

        public Task<Response> CalculateRoutesAsync(CalculateRoutesParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(
                    "parameters", 
                    "Provide valid options for Routes Calculation request.");
            }

            var request = new RestRequest("Routes", Method.GET);

            if (parameters.WayPoints != null)
            {
                for (int i = 0; i < parameters.WayPoints.Length; i++)
                {
                    if (parameters.WayPoints[i] != null)
                    {
                        request.AddQueryParameter(string.Format("wp.{0}", i), parameters.WayPoints[i].GetFormattedString());
                    }
                }
            }

            if (parameters.ViaWayPoints != null)
            {
                for (int i = 0; i < parameters.ViaWayPoints.Length; i++)
                {
                    if (parameters.ViaWayPoints[i] != null)
                    {
                        request.AddQueryParameter(string.Format("vwp.{0}", i), parameters.ViaWayPoints[i].GetFormattedString());
                    }
                }
            }

            if (parameters.AvoidRoadTypes != null)
            {
                request.AddQueryParameter("avoid", parameters.AvoidRoadTypes.ToCSVString());
            }

            if (parameters.RouteAttributes != null)
            {
                request.AddQueryParameter("ra", parameters.RouteAttributes.ToCSVString());
            }

            if (parameters.Tolerances != null)
            {
                request.AddQueryParameter("tl", string.Join(",", Array.ConvertAll(parameters.Tolerances, p => p.ToString(CultureInfo.InvariantCulture))));
            }

            if (parameters.DistanceBeforeFirstTurn != null)
            {
                request.AddQueryParameter("dbft", parameters.DistanceBeforeFirstTurn.Value.ToString());
            }

            if (parameters.Heading != null)
            {
                request.AddQueryParameter("hd", parameters.Heading.Value.ToString());
            }
            
            if (parameters.RouteOptimization != null)
            {
                request.AddQueryParameter("optmz", parameters.RouteOptimization.Key);
            }
            
            if (parameters.DistanceUnite != null)
            {
                request.AddQueryParameter("du", parameters.DistanceUnite.Key);
            }

            if (parameters.DesireTransiteTime != null)
            {
                request.AddQueryParameter("dt", parameters.DesireTransiteTime.Value.ToString(DateFormatMMddyyyy_HHmmss));
            }

            if (parameters.TransiteTimeType != null)
            {
                request.AddQueryParameter("tt", parameters.TransiteTimeType.Key);
            }

            if (parameters.MaxSolutions != null)
            {
                request.AddQueryParameter("maxSolns", parameters.MaxSolutions.Key);
            }

            if (parameters.TravelMode != null)
            {
                request.AddQueryParameter("travelMode", parameters.TravelMode.Key);
            }

            var response = ExecuteAsync<Response>(request);

            return response;
        }
    }
}