﻿using FribergCarRentals.Data;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentals.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly IBooking bookingRepository;

        public BookingController(IBooking bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        //GET: Admin/Booking/AllBookings
        public ActionResult AllBookings()
        {
            return View(bookingRepository.GetAllBookings());
        }


        //GET: Admin/Booking/RemoveBooking/5
        public ActionResult RemoveBooking(int bookingId)
        {
            var booking = bookingRepository.GetBookingById(bookingId);

            if (booking == null)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            {
                return View(booking);
            }
        }


        //POST: Admin/Booking/RemoveBooking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveBooking(Booking booking)
        {
            try
            {
                bookingRepository.Delete(booking);
                return RedirectToAction("AllBookings");
            }
            catch
            {
                return View();
            }
        }

    }
}
