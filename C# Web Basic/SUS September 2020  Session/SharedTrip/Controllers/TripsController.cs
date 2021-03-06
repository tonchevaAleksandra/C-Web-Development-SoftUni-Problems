﻿using System;
using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrEmpty(model.StartPoint))
            {
                return this.Error("Startpoint can not be empty.");
            }
            if (String.IsNullOrEmpty(model.EndPoint))
            {
                return this.Error("Endpoint can not be empty.");
            }
            if (String.IsNullOrEmpty(model.DepartureTime))
            {
                return this.Error("Departure time can not be empty.");
            }

            if (!this.tripsService.IsDepartureTimeValid(model.DepartureTime))
            {
                return this.Error("Departure time is not valid date.");
            }

            if (model.Seats < 2 || model.Seats > 6)
            {
                return this.Error("Seats can be not less then 2 and  not more then 6.");
            }

            if (model.Description.Length == 0 || model.Description.Length > 80)
            {
                return this.Error("Description should be less then 80 characters.");
            }

            this.tripsService.Create(model.StartPoint, model.EndPoint, model.DepartureTime, model.ImagePath, model.Seats, model.Description);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.tripsService.GetAllTrips();

            return this.View(viewModel);
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }


            return this.View();
        }
    }
}
